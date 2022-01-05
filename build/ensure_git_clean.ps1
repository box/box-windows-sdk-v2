Param
(
    [Alias('b')]
    [string]$ReleaseBranch="main"
)

$ErrorActionPreference = "Stop"

git remote update 
$currentBranch = ''
git branch | ForEach-Object {
    if ($_ -match "^\* (.*)") {
        $currentBranch += $matches[1]
    }
}
if(!($currentBranch -eq $ReleaseBranch)){
    Write-Output "Local branch " + $currentBranch  + " is not the same as the release branch " + $ReleaseBranch + ". Aborting script"
    exit 1
}
$NumberOfDifferentCommits = git rev-list HEAD...origin/$ReleaseBranch --count
if(!($NumberOfDifferentCommits -eq "0")){
    Write-Output "Different commits local and on remote. Aborting script"
    exit 1
} 
if (git status --porcelain) { 
    Write-Output "There are local changes that are not present on the remote. Aborting script."
    exit 1
}

exit 0