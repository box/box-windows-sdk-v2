Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Parameter(Mandatory)]
    [Alias('gh')]
    [string]$GithubToken,

    [Parameter(Mandatory)]
    [Alias('nv')]
    [string]$NextVersion,

    [Alias('rs')]
    [string]$ReleaseNotes="Release notes"
)

$ErrorActionPreference = "Stop"

$ROOT_DIR=$pwd
$FRAMEWORK_PROJ_DIR="$ROOT_DIR" + "\Box.V2"
$CORE_PROJ_DIR="$ROOT_DIR" + "\Box.V2.Core"
$REPO_OWNER="box"
$REPO_NAME="box-windows-sdk-v2"
$FRAMEWORK_NUPKG_PATH=$FRAMEWORK_PROJ_DIR + "\Box.V2." + $NextVersion + ".nupkg"
$FRAMEWORK_PDB_PATH=$FRAMEWORK_PROJ_DIR + "\bin\Release\Box.V2.pdb"
$CORE_NUPKG_PATH=$CORE_PROJ_DIR + "\bin\Release\Box.V2.Core." + $NextVersion + ".nupkg"
$CORE_PDB_PATH=$CORE_PROJ_DIR + "\bin\Release\netstandard2.0\Box.V2.Core.pdb"

###########################################################################
# Install dependencies
###########################################################################

Install-Module -Name PowerShellForGitHub

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