Param
(
    [Alias('dr')]
    [bool]$DryRun = $true,

    [Alias('ng')]
    [string]$NugetKey,

    [Alias('nv')]
    [string]$NextVersion,

    [Alias('b')]
    [string]$Branch="main",

    [Alias('cf')]
    [string]$PfxAsBase64,

    [Alias('pw')]
    [string]$PfxPassword,

    [Alias('bt')]
    [bool]$BuildAndTest =  $true,

    [Alias('id')]
    [bool]$InstallDependencies = $true
)

$ErrorActionPreference = "Stop"

$ROOT_DIR=$pwd
$GIT_SCRIPT="$PSScriptRoot" + "\ensure_git_clean.ps1"
$FRAMEWORK_PROJ_DIR="$ROOT_DIR" + "\Box.V2"
$FRAMEWORK_ASSEMBLY_NAME="Box.V2"
$SLN_PATH="$ROOT_DIR" + "\Box.V2.sln"
$NUGET_URL="https://api.nuget.org/v3/index.json"
$NET_FRAMEWORK_VER="net45"
$CHANGELOG_PATH="$ROOT_DIR" + "\CHANGELOG.md"

if($NextVersion -eq $null -Or $NextVersion -eq ''){
    $NextVersion = $env:NextVersion
    if($NextVersion -eq $null -Or $NextVersion -eq ''){
        $NextVersion = (Select-String -Pattern [0-9]+\.[0-9]+\.[0-9]+ -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
    }
}

$FRAMEWORK_NUPKG_PATH="$ROOT_DIR" + "\" + "$FRAMEWORK_ASSEMBLY_NAME" + "." + "$NextVersion" + ".nupkg"

###########################################################################
# Parameters validation
###########################################################################

if($NugetKey -eq $null -Or $NugetKey -eq ''){
    $NugetKey = $env:NugetKey
    if($NugetKey -eq $null -Or $NugetKey -eq ''){
        Write-Output "Nuget key not supplied. Aborting script."
        exit 1
    }
}

if($PfxAsBase64 -eq $null -Or $PfxAsBase64 -eq ''){
    $PfxAsBase64 = $env:PfxAsBase64
    if($PfxAsBase64 -eq $null -Or $PfxAsBase64 -eq ''){
        Write-Output "Pfx certifcate as base64 not supplied. Aborting script."
        exit 1
    }
}

if($PfxPassword -eq $null -Or $PfxPassword -eq ''){
    $PfxPassword = $env:PfxPassword
    if($PfxPassword -eq $null -Or $PfxPassword -eq ''){
        Write-Output "Pfx certificate password not supplied. Aborting script."
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
    Invoke-WebRequest https://github.com/honzajscz/SnInstallPfx/releases/download/0.1.2-beta/SnInstallPfx.exe -SkipCertificateCheck -OutFile SnInstallPfx.exe
}

###########################################################################
# Setup Pfx
###########################################################################

$Bytes = [Convert]::FromBase64String($PfxAsBase64)
$PfxPath = "$FRAMEWORK_PROJ_DIR" + "\AssemblySigningKey.pfx"
[IO.File]::WriteAllBytes($PfxPath, $Bytes)
.\SnInstallPfx.exe $PfxPath $PfxPassword
Remove-Item $PfxPath 

###########################################################################
# Build and Test
###########################################################################

if($BuildAndTest){
    msbuild $FRAMEWORK_PROJ_DIR /property:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Output "Compilation failed. Aborting script."
        exit 1
    }
    dotnet test -f $NET_FRAMEWORK_VER --verbosity normal
    if ($LASTEXITCODE -ne 0) {
        Write-Output "Some of the unit test failed. Aborting script."
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

nuget restore $SLN_PATH
nuget pack $FRAMEWORK_PROJ_DIR -Build -Prop Configuration=Release
if ($LASTEXITCODE -ne 0) {
    Write-Output "Package creation failed. Aborting script."
    exit 1
}

###########################################################################
# Publish Framework to the Nuget
###########################################################################

if ($DryRun) { 
    Write-Output "Running in Dry Run mode. Package will not be published"
}else{
    dotnet nuget push $FRAMEWORK_NUPKG_PATH -k $NugetKey -s $NUGET_URL
}

###########################################################################
# Clean all VS_KEY_* containers
###########################################################################

certutil -csp "Microsoft Strong Cryptographic Provider" -key | Select-String -Pattern "VS_KEY" | ForEach-Object{ $_.ToString().Trim()} | ForEach-Object{ certutil -delkey -csp "Microsoft Strong Cryptographic Provider" $_}

exit 0