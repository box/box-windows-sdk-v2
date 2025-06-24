Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Alias('gh')]
    [string]$GithubToken,

    [Alias('ng')]
    [string]$NugetKey,

    [Alias('nv')]
    [string]$NextVersion,

    [Alias('b')]
    [string]$Branch="main",

    [Alias('cf')]
    [string]$SnkAsBase64,

    [Alias('bt')]
    [bool]$BuildAndTest =  $true,

    [Alias('id')]
    [bool]$InstallDependencies = $true
)

function RemoveSensitiveData()
{
    Remove-Item $SNK_PATH
    Write-Output "Sensitive data removed."
}

. $PSScriptRoot\variables.ps1

$ErrorActionPreference = "Stop"

if($NextVersion -eq $null -Or $NextVersion -eq ''){
    $NextVersion = $env:NextVersion
    if($NextVersion -eq $null -Or $NextVersion -eq ''){
        $NextVersion = (Select-String -Pattern [0-9]+\.[0-9]+\.[0-9]+ -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
    }
}
$NextVersionTag = "v" + $NextVersion

$FRAMEWORK_NUPKG_PATH="$ROOT_DIR" + "\" + "$FRAMEWORK_ASSEMBLY_NAME" + "." + "$NextVersion" + ".nupkg"

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

if($NugetKey -eq $null -Or $NugetKey -eq ''){
    $NugetKey = $env:NugetKey
    if($NugetKey -eq $null -Or $NugetKey -eq ''){
        Write-Output "Nuget key not supplied. Aborting script."
        exit 1
    }
}

if($SnkAsBase64 -eq $null -Or $SnkAsBase64 -eq ''){
    $SnkAsBase64 = $env:SnkAsBase64
    if($SnkAsBase64 -eq $null -Or $SnkAsBase64 -eq ''){
        Write-Output "Strong-name key (.snk) not supplied. Aborting script."
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
# Setup Snk
###########################################################################

$Bytes = [Convert]::FromBase64String($SnkAsBase64)
[IO.File]::WriteAllBytes($SNK_PATH, $Bytes)

###########################################################################
# Build and Test
###########################################################################

if($BuildAndTest){
    dotnet build $FRAMEWORK_PROJ_DIR -c SignedRelease
    if ($LASTEXITCODE -ne 0) {
        Write-Output "Compilation failed. Aborting script."
        RemoveSensitiveData
        exit 1
    }
    dotnet test $TEST_PATH -f $NET_FRAMEWORK_VER --verbosity normal
    if ($LASTEXITCODE -ne 0) {
        Write-Output "Some of the unit tests failed. Aborting script."
        RemoveSensitiveData
        exit 1
    }
    dotnet clean $FRAMEWORK_PROJ_DIR
    if (test-path ("$FRAMEWORK_PROJ_DIR" + "\bin")) { Remove-Item ("$FRAMEWORK_PROJ_DIR" + "\bin") -r }
    if (test-path ("$FRAMEWORK_PROJ_DIR" + "\obj")) { Remove-Item ("$FRAMEWORK_PROJ_DIR" + "\obj") -r }
}else{
    Write-Output "Skipping build and test step."
}

###########################################################################
# Pack Framework
###########################################################################

dotnet pack $FRAMEWORK_PROJ_DIR -c SignedRelease
if ($LASTEXITCODE -ne 0) {
    Write-Output "Package creation failed. Aborting script."
    exit 1
}

###########################################################################
# Add package to GitHub release
###########################################################################

if ($DryRun) { 
    Write-Output "Dry run. Package will not be added to the release."
}else{
    $password = ConvertTo-SecureString "$GithubToken" -AsPlainText -Force
    $Cred = New-Object System.Management.Automation.PSCredential ("Release_Bot", $password)
    Set-GitHubAuthentication -SessionOnly -Credential $Cred

    $releases = Get-GitHubRelease -OwnerName $REPO_OWNER -RepositoryName $REPO_NAME
    $release = ($releases | Where-Object { $_.Name -eq $NextVersionTag })
    if($release -eq $null -Or $release -eq ''){
        Write-Output "Release with the name " + $NextVersionTag " not found. Aborting script"
        RemoveSensitiveData
        exit 1
    }

    $release | New-GitHubReleaseAsset -Path $FRAMEWORK_NUPKG_PATH
    $release | New-GitHubReleaseAsset -Path $FRAMEWORK_PDB_PATH

    Clear-GitHubAuthentication
}

###########################################################################
# Publish Framework to the Nuget
###########################################################################

if ($DryRun) { 
    Write-Output "Running in Dry Run mode. Package will not be published"
}else{
    dotnet nuget push $FRAMEWORK_NUPKG_PATH -k $NugetKey -s $NUGET_URL --skip-duplicate
    if ($LASTEXITCODE -ne 0) {
        Write-Output "Nuget push failed. Aborting script"
        RemoveSensitiveData
        exit 1
    }
}

###########################################################################
# Remove sensitive data
###########################################################################

RemoveSensitiveData
exit 0