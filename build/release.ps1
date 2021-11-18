Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Alias('gh')]
    [string]$GithubToken,

    [Alias('nv')]
    [string]$NextVersion,

    [Alias('id')]
    [bool]$InstallDependencies = $true
)

$ErrorActionPreference = "Stop"

. $PSScriptRoot\variables.ps1

if($NextVersion -eq $null -Or $NextVersion -eq ''){
    $NextVersion = $env:NextVersion
    if($NextVersion -eq $null -Or $NextVersion -eq ''){
        $NextVersion = (Select-String -Pattern [0-9]+\.[0-9]+\.[0-9]+ -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
    }
}

$FRAMEWORK_NUPKG_PATH=$FRAMEWORK_PROJ_DIR + "\" + $FRAMEWORK_ASSEMBLY_NAME + "." + $NextVersion + ".nupkg"
$CORE_NUPKG_PATH=$CORE_PROJ_DIR + "\bin\Release\" + $CORE_ASSEMBLY_NAME + "." + $NextVersion + ".nupkg"
$FRAMEWORK_PACKAGE_LINK = "https://www.nuget.org/packages/" + $FRAMEWORK_ASSEMBLY_NAME + "/" + $NextVersion
$CORE_PACKAGE_LINK = "https://www.nuget.org/packages/" + $CORE_ASSEMBLY_NAME + "/" + $NextVersion

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

###########################################################################
# Install dependencies
###########################################################################

if ($InstallDependencies){
    Install-Module -Name PowerShellForGitHub -Scope CurrentUser -Force
}

###########################################################################
# Extract release notes
###########################################################################

$VersionFound = $false
foreach($line in Get-Content $CHANGELOG_PATH) {
    if($line -match "## [[0-9]+\.[0-9]+\.[0-9]+]"){
        if($VersionFound){
            break
        }
        $VersionFound = $true
        continue
    }
    if($VersionFound){
        $ReleaseNotes += "$line`n"
    }
}
$ReleaseNotes = $ReleaseNotes.replace('### ' , '').trim()
$ReleaseNotes += "`n`n$FRAMEWORK_PACKAGE_LINK`n"
$ReleaseNotes += "$CORE_PACKAGE_LINK"

###########################################################################
# Create git release
###########################################################################

if($DryRun){
    Write-Output "Dry run. Release will not be created."
}else{
    $password = ConvertTo-SecureString "$GithubToken" -AsPlainText -Force
    $Cred = New-Object System.Management.Automation.PSCredential ("Release_Bot", $password)
    Set-GitHubAuthentication -SessionOnly -Credential $Cred
    $releaseParams = @{
        OwnerName = $REPO_OWNER
        RepositoryName = $REPO_NAME
        Tag = "v" + $NextVersion
        Name = "v" + $NextVersion
        Body = $ReleaseNotes
    }
    $release = New-GitHubRelease @releaseParams
 
    $release | New-GitHubReleaseAsset -Path $FRAMEWORK_NUPKG_PATH
    $release | New-GitHubReleaseAsset -Path $FRAMEWORK_PDB_PATH
    $release | New-GitHubReleaseAsset -Path $CORE_NUPKG_PATH
    $release | New-GitHubReleaseAsset -Path $CORE_PDB_PATH

    Clear-GitHubAuthentication
}

exit 0