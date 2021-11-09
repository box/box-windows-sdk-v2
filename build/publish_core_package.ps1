Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Parameter(Mandatory)]
    [Alias('ng')]
    [string]$NugetKey,

    [Parameter(Mandatory)]
    [Alias('nv')]
    [string]$NextVersion,

    [Alias('b')]
    [string]$Branch="main"
)

$ErrorActionPreference = "Stop"

$ROOT_DIR=$pwd
$GIT_SCRIPT="$PSScriptRoot" + "\ensure_git_clean.ps1"
$CORE_PROJ_DIR="$ROOT_DIR" + "\Box.V2.Core"
$CORE_ASSEMBLY_NAME="Box.V2.Core"
$NUGET_URL="https://api.nuget.org/v3/index.json"
$CORE_NUPKG_PATH="$CORE_PROJ_DIR" + "\bin\Release\" + "$CORE_ASSEMBLY_NAME" + "." + "$NextVersion" + ".nupkg"

###########################################################################
# Ensure git tree is clean
###########################################################################

Invoke-Expression "& `"$GIT_SCRIPT`" -b $Branch"
if ($LASTEXITCODE -ne 0) {
    exit 1
}

###########################################################################
# Pack Core
###########################################################################

dotnet pack $CORE_PROJ_DIR -c Release

###########################################################################
# Publish Core to the Nuget
###########################################################################

if ($DryRun) { 
    Write-Output "Dry run. Package will not be published"
}else{
    dotnet nuget push $CORE_NUPKG_PATH -k $NugetKey -s $NUGET_URL
}

exit 0