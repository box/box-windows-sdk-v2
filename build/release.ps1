Param
(
    #[Parameter(Mandatory)]
    [Alias('dr')]
    [bool]$DryRun = $true,

    #[Parameter(Mandatory)]
    [Alias('gh')]
    [string]$GithubToken
)

$ErrorActionPreference = "Stop"
#.\build\release.ps1
$ROOT_DIR=$pwd
$CHANGELOG_PATH="$ROOT_DIR" + "\CHANGELOG.md"
$BOX_SLN_PATH="$ROOT_DIR" + "\Box.V2.sln"
$BOX_PROJ_DIR="$ROOT_DIR" + "\Box.V2"
$BOX_CORE_PROJ_DIR="$ROOT_DIR" + "\Box.V2.Core"
$BOX_TEST_PROJ_DIR="$ROOT_DIR" + "\Box.V2.Test"
$NET_CORE_VER="netcoreapp2.0"
$NET_FRAMEWORK_VER="net45"
$FRAMEWORK_DLL_PATH="$ROOT_DIR" + "\Box.V2\bin\Release\Box.V2.dll"
$NET_CORE_CSPROJ_PATH="$BOX_CORE_PROJ_DIR" + "\Box.V2.Core.csproj"
$ASSEMBLYINFO_PATH="$BOX_PROJ_DIR" + "\Utility\AssemblyInfo.cs"
$NET_FRAMEWORK_NUSPEC_PATH="$BOX_PROJ_DIR" + "\Box.V2.nuspec"
$REPO_OWNER="box"
$REPO_NAME="box-windows-sdk-v2"

###########################################################################
# Install dependencies
###########################################################################

#curl -sL https://deb.nodesource.com/setup_14.x | sudo -E bash -
#sudo apt-get install -y nodejs
#sudo npm install -g standard-version
#setup sn.exe path
#setup msbuild and dotnet sdk
#Install-Module -Name PowerShellForGitHub

###########################################################################
# Ensure git tree is clean
###########################################################################

#exit 0
if (git status --porcelain) { echo "Not clean" }

###########################################################################
# Update changelog
###########################################################################

standard-version release --skip.commit --skip.tag
# $NEXT_VERSION = (Select-String -Pattern [0-9]+\.[0-9]+\.[0-9]+ -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
# $NEXT_VERSION_TAG = "v" + "$NEXT_VERSION"
# $RELEASE_DATE = (Select-String -Pattern "\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])" -Path $CHANGELOG_PATH | Select-Object -First 1).Matches.Value
# $RELEASE_NOTE_LINK = $NEXT_VERSION.Replace(".", "") + "-" + "$RELEASE_DATE"

###########################################################################
# Bump version files
###########################################################################

# (Get-Content $NET_CORE_CSPROJ_PATH) -replace '(?<=<Version>).*(?=</Version>)', $NEXT_VERSION | Set-Content $NET_CORE_CSPROJ_PATH
# (Get-Content $NET_CORE_CSPROJ_PATH) -replace '(?<=CHANGELOG\.md#).*(?=</PackageReleaseNotes>)', $RELEASE_NOTE_LINK | Set-Content $NET_CORE_CSPROJ_PATH
# (Get-Content $NET_CORE_CSPROJ_PATH) -replace '(?<=<version>).*(?=</version>)', $NEXT_VERSION | Set-Content $NET_CORE_CSPROJ_PATH
# (Get-Content $NET_FRAMEWORK_NUSPEC_PATH) -replace '(?<=CHANGELOG\.md#).*(?=</releaseNotes>)', $RELEASE_NOTE_LINK | Set-Content $NET_FRAMEWORK_NUSPEC_PATH
# (Get-Content $ASSEMBLYINFO_PATH) -replace '(?<=NuGetVersion = ").*(?=";)', $NEXT_VERSION | Set-Content $ASSEMBLYINFO_PATH

###########################################################################
# Commit and push version bump
###########################################################################

#todo release notes
# if($DryRun){
#     Write-Output "Running in dry run. Commit will not be made"
# }else{
#     git add .
#     git commit -am $NEXT_VERSION_TAG
#     git push --atomic origin HEAD $NEXT_VERSION_TAG
# }

###########################################################################
# Build and Test Framework
###########################################################################

# dotnet clean $BOX_PROJ_DIR
# if (test-path ("$BOX_PROJ_DIR" + "\bin")) { rm ("$BOX_PROJ_DIR" + "\bin") -r }
# if (test-path ("$BOX_PROJ_DIR" + "\obj")) { rm ("$BOX_PROJ_DIR" + "\obj") -r }
# # dotnet build $BOX_PROJ_DIR
# # dotnet test -f $NET_FRAMEWORK_VER
# # dotnet clean $BOX_PROJ_DIR
# if (test-path ("$BOX_PROJ_DIR" + "\bin")) { rm ("$BOX_PROJ_DIR" + "\bin") -r }
# if (test-path ("$BOX_PROJ_DIR" + "\obj")) { rm ("$BOX_PROJ_DIR" + "\obj") -r }

###########################################################################
# Build and Test Core
###########################################################################

# dotnet clean $BOX_CORE_PROJ_DIR
# if (test-path ("$BOX_CORE_PROJ_DIR" + "\bin")) { rm ("$BOX_CORE_PROJ_DIR" + "\bin") -r }
# if (test-path ("$BOX_CORE_PROJ_DIR" + "\obj")) { rm ("$BOX_CORE_PROJ_DIR" + "\obj") -r }
# # dotnet build $BOX_CORE_PROJ_DIR
# # dotnet test -f $NET_CORE_VER
# # dotnet clean $BOX_CORE_PROJ_DIR
# dir ("$BOX_CORE_PROJ_DIR" + "\bin") -ErrorAction SilentlyContinue  | Remove-Item -Recurse
# dir ("$BOX_CORE_PROJ_DIR" + "\obj") -ErrorAction SilentlyContinue  | Remove-Item -Recurse

###########################################################################
# Pack Framework
###########################################################################

# msbuild.exe $BOX_PROJ_DIR /property:Configuration=Release

###########################################################################
# Pack Core
###########################################################################

# dotnet pack $BOX_CORE_PROJ_DIR -c Release

###########################################################################
# Validate Framework signature
###########################################################################

# sn -v $FRAMEWORK_DLL_PATH

###########################################################################
# Publish Framework to nuget
###########################################################################

###########################################################################
# Publish Core to nuget
###########################################################################

###########################################################################
# Create git release
###########################################################################

# $password = ConvertTo-SecureString "$GithubToken" -AsPlainText -Force
# $Cred = New-Object System.Management.Automation.PSCredential ("Release_Bot", $password)
# Set-GitHubAuthentication -SessionOnly -Credential $Cred
# New-GitHubRelease -OwnerName $REPO_OWNER -RepositoryName $REPO_NAME -Tag $NEXT_VERSION_TAG -Name $NEXT_VERSION_TAG -Body "Release notes"
# echo $issues
# Clear-GitHubAuthentication

echo "DONE"
