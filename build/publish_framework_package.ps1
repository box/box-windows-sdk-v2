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
    [string]$PfxAsBase64,

    [Alias('pw')]
    [string]$PfxPassword,

    [Alias('bt')]
    [bool]$BuildAndTest =  $true,

    [Alias('id')]
    [bool]$InstallDependencies = $true
)

function RemoveSensitiveData()
{
    Remove-Item $PFX_PATH
    Remove-Item SnInstallPfx.exe
    certutil -csp "Microsoft Strong Cryptographic Provider" -key | Select-String -Pattern "VS_KEY" | ForEach-Object{ $_.ToString().Trim()} | ForEach-Object{ certutil -delkey -csp "Microsoft Strong Cryptographic Provider" $_}
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
    Install-Module -Name PowerShellForGitHub -Scope CurrentUser -Force
    Invoke-WebRequest https://github.com/honzajscz/SnInstallPfx/releases/download/0.1.2-beta/SnInstallPfx.exe -SkipCertificateCheck -OutFile SnInstallPfx.exe
}

###########################################################################
# Setup Pfx
###########################################################################

$Bytes = [Convert]::FromBase64String($PfxAsBase64)
[IO.File]::WriteAllBytes($PFX_PATH, $Bytes)
.\SnInstallPfx.exe $PFX_PATH $PfxPassword 

###########################################################################
# Build and Test
###########################################################################

if($BuildAndTest){
    nuget restore $SLN_PATH
    msbuild $FRAMEWORK_PROJ_DIR /property:Configuration=SignedRelease
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

nuget restore $SLN_PATH
nuget pack $FRAMEWORK_PROJ_DIR -Build -Prop Configuration=SignedRelease
if ($LASTEXITCODE -ne 0) {
    Write-Output "Package creation failed. Aborting script."
    RemoveSensitiveData
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