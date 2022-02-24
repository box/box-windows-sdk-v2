Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Alias('gh')]
    [string]$GithubToken,

    [Alias('gu')]
    [string]$GithubUsername,

    [Alias('ge')]
    [string]$GithubEmail,

    [Alias('b')]
    [string]$Branch="main",

    [Alias('id')]
    [bool]$InstallDependencies = $true
)

. $PSScriptRoot\variables.ps1

$ErrorActionPreference = "Stop"

###########################################################################
# Parameters validation
###########################################################################

if($GithubToken -eq $null -Or $GithubToken -eq ''){
    $GithubToken = $env:GithubToken
    if($GithubToken -eq $null -Or $GithubToken -eq ''){
        Write-Output "Github token not supplied. Aborting script."
        exit 1
    }
}

if($GithubUsername -eq $null -Or $GithubUsername -eq ''){
    $GithubUsername = $env:GithubUsername
    if($GithubUsername -eq $null -Or $GithubUsername -eq ''){
        Write-Output "Github username not supplied. Aborting script."
        exit 1
    }
}

if($GithubEmail -eq $null -Or $GithubEmail -eq ''){
    $GithubEmail = $env:GithubEmail
    if($GithubEmail -eq $null -Or $GithubEmail -eq ''){
        Write-Output "Github email not supplied. Aborting script."
        exit 1
    }
}

###########################################################################
# Ensure git tree is clean
###########################################################################

Invoke-Expression "& `"$GIT_SCRIPT`" -b $Branch"
if ($LASTEXITCODE -ne 0) {
   exit 1
}

###########################################################################
# Install dependencies
###########################################################################

if ($InstallDependencies){
    npm install -g standard-version
    Install-Module -Name PowerShellForGitHub -Scope CurrentUser -Force
    $PathToAdd = ';' + $env:USERPROFILE + '\AppData\Roaming\npm'
    $env:Path += $PathToAdd
}

###########################################################################
# Update changelog
###########################################################################

standard-version --skip.commit --skip.tag
$NEXT_VERSION = (Select-String -Pattern [0-9]+\.[0-9]+\.[0-9]+ -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
$NEXT_VERSION_TAG = "v" + "$NEXT_VERSION"
$RELEASE_DATE = (Select-String -Pattern "\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])" -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
$RELEASE_NOTE_LINK = $NEXT_VERSION.Replace(".", "") + "-" + "$RELEASE_DATE"

###########################################################################
# Reorder changelog sections
###########################################################################

$sections = @()
$sections += ('### ⚠ BREAKING CHANGES')
foreach($line in Get-Content $VERSIONRC_PATH) {
    $found = $line -match '(?<=section": ").*(?=",)'
    if ($found) {
        $sections += $matches[0]
    }
}

$orderedSections = new-object string[] $sections.Length
$currentSection = $null
$previousSectionIndex = 0

foreach($line in Get-Content $CHANGELOG_PATH) {
    if($line -match "#{2,3} [[0-9]+\.[0-9]+\.[0-9]+]"){
        if($VersionFound){
            if(![string]::IsNullOrWhiteSpace($currentSection)){
                $orderedSections[$previousSectionIndex] = $currentSection
            }
            break
        }
        $VersionFound = $true
        continue
    }
    if($VersionFound){
        for ($i=0; $i -lt $sections.Length; $i++)
        {
            if($line -match [Regex]::Escape($sections[$i])){
                if(![string]::IsNullOrWhiteSpace($currentSection)){
                    $orderedSections[$previousSectionIndex] = $currentSection
                }
                $previousSectionIndex = $i
                $currentSection = $null
                continue
            }
        }
        $currentSection += "$line`n"
    }
}

$orderedSectionsAsString

foreach($orderedSection in $orderedSections){
    $orderedSectionsAsString += $orderedSection
}

$fileContent = Get-Content $CHANGELOG_PATH -Raw
$result = [regex]::match($fileContent, '(?s)(### [^\[].*?)#{2,3} [[0-9]+\.[0-9]+\.[0-9]+]').Groups[1].Value
$fileContent -replace [Regex]::Escape($result), $orderedSectionsAsString | Set-Content $CHANGELOG_PATH

###########################################################################
# Bump version files
###########################################################################

(Get-Content $CORE_CSPROJ_PATH) -replace '(?<=<Version>).*(?=</Version>)', $NEXT_VERSION | Set-Content $CORE_CSPROJ_PATH
(Get-Content $CORE_CSPROJ_PATH) -replace '(?<=CHANGELOG\.md#).*(?=</PackageReleaseNotes>)', $RELEASE_NOTE_LINK | Set-Content $CORE_CSPROJ_PATH
(Get-Content $FRAMEWORK_NUSPEC_PATH) -replace '(?<=<version>).*(?=</version>)', $NEXT_VERSION | Set-Content $FRAMEWORK_NUSPEC_PATH
(Get-Content $FRAMEWORK_NUSPEC_PATH) -replace '(?<=CHANGELOG\.md#).*(?=</releaseNotes>)', $RELEASE_NOTE_LINK | Set-Content $FRAMEWORK_NUSPEC_PATH
(Get-Content $ASSEMBLYINFO_PATH) -replace '(?<=NuGetVersion = ").*(?=";)', $NEXT_VERSION | Set-Content $ASSEMBLYINFO_PATH

###########################################################################
# Create PR with version bump
###########################################################################

if($DryRun){
    Write-Output "Dry run. PR with version bump will not be created."
}else{
    $RepoLink = "https://" + $REPO_OWNER + ":" + $GithubToken + "@github.com/" + $REPO_OWNER + "/" + $REPO_NAME + ".git"
    git remote set-url origin $RepoLink
    git config user.name $GithubUsername
    git config user.email $GithubEmail
    $lastBranch = git rev-parse --abbrev-ref HEAD
    git branch -D $NEXT_VERSION_TAG
    git checkout -b $NEXT_VERSION_TAG
    $commitTitle = "chore: release " + $NEXT_VERSION_TAG 
    git commit -am $commitTitle
    git push --set-upstream origin $NEXT_VERSION_TAG

    $password = ConvertTo-SecureString "$GithubToken" -AsPlainText -Force
    $Cred = New-Object System.Management.Automation.PSCredential ("Release_Bot", $password)
    Set-GitHubAuthentication -SessionOnly -Credential $Cred
    
    $prParams = @{
        OwnerName = $REPO_OWNER
        RepositoryName = $REPO_NAME
        Title = $commitTitle
        Head = $NEXT_VERSION_TAG
        Base = $Branch
        Body = "Bumping version files for the next release! " + $NEXT_VERSION_TAG
        MaintainerCanModify = $true
    }
    New-GitHubPullRequest @prParams

    Clear-GitHubAuthentication

    git checkout $lastBranch
    git branch -D $NEXT_VERSION_TAG

}

exit 0
