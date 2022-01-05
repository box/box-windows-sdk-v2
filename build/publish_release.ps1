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
$NextVersionTag = "v" + $NextVersion

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
# Publish git release
###########################################################################

if($DryRun){
    Write-Output "Dry run. Release will not be update."
}else{
    $password = ConvertTo-SecureString "$GithubToken" -AsPlainText -Force
    $Cred = New-Object System.Management.Automation.PSCredential ("Release_Bot", $password)
    Set-GitHubAuthentication -SessionOnly -Credential $Cred

    $releases = Get-GitHubRelease -OwnerName $REPO_OWNER -RepositoryName $REPO_NAME
    $release = ($releases | Where-Object { $_.Name -eq $NextVersionTag })
    if($release -eq $null -Or $release -eq ''){
        Write-Output "Release with the name " + $NextVersionTag " not found. Aborting script"
        exit 1
    }

    $releaseParams = @{
        Release = $release."ID"
        OwnerName = $REPO_OWNER
        RepositoryName = $REPO_NAME
        Draft = $false
    }
    Set-GitHubRelease @releaseParams

    Clear-GitHubAuthentication
}

exit 0