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
$FRAMEWORK_PROJ_DIR="$ROOT_DIR" + "\Box.V2"
$FRAMEWORK_ASSEMBLY_NAME="Box.V2"
$NUGET_URL="https://api.nuget.org/v3/index.json"
$FRAMEWORK_NUPKG_PATH="$FRAMEWORK_PROJ_DIR" + "\bin\Release\" + "$FRAMEWORK_ASSEMBLY_NAME" + "." + "$NextVersion" + ".nupkg"

###########################################################################
# Ensure git tree is clean
###########################################################################

Invoke-Expression "& `"$GIT_SCRIPT`" -b $Branch"
if ($LASTEXITCODE -ne 0) {
    exit 1
}

###########################################################################
# Pack Framework
###########################################################################



###########################################################################
# Publish Framework to the Nuget
###########################################################################

if ($DryRun) { 
    Write-Output "Running in Dry Run mode. Package will not be published"
}else{
    dotnet nuget push $FRAMEWORK_NUPKG_PATH -k $NugetKey -s $NUGET_URL
}

exit 0