param (
    [Parameter(Mandatory = $true)]
    [string]$Version,
    [switch]$DryRun,
    [switch]$SkipSafetyChecks
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path $MyInvocation.MyCommand.Path
$RepoRoot = (Get-Item $ScriptDir).Parent.FullName
$UpdateVersionScript = Join-Path $ScriptDir "update-version.ps1"

function Write-Step { param([string]$msg) Write-Host "`n=== $msg ===" -ForegroundColor Cyan }
function Write-Success { param([string]$msg) Write-Host "SUCCESS: $msg" -ForegroundColor Green }
function Write-Warning { param([string]$msg) Write-Host "WARNING: $msg" -ForegroundColor Yellow }
function Write-ErrorMsg { param([string]$msg) Write-Host "ERROR: $msg" -ForegroundColor Red }

# 1. Safety Checks
Write-Step "1. Safety Checks"

if (-not $SkipSafetyChecks) {
    # Check Git Status
    $status = git status --porcelain
    if ($status) {
        Write-ErrorMsg "Working directory is not clean. Commit or stash changes before releasing."
        Write-Host $status
        exit 1
    }
    Write-Success "Git working directory is clean."

    # Check Branch
    $branch = git branch --show-current
    if ($branch.Trim() -ne "main") {
        Write-Warning "You are on branch '$branch', not 'main'. Continue? (y/n)"
        $resp = Read-Host
        if ($resp -ne "y") { exit 1 }
    }
    Write-Success "Branch verified."

    # Check Secrets (Simple heuristic: look for unignored key files)
    $sensitiveFiles = @("config.json", ".env", "secrets.json")
    foreach ($file in $sensitiveFiles) {
        if (Test-Path (Join-Path $RepoRoot $file)) {
            $isIgnored = git check-ignore $file
            if (-not $isIgnored) {
                Write-ErrorMsg "Sensitive file '$file' is present and NOT ignored by git. Aborting security check."
                exit 1
            }
        }
    }
    Write-Success "Basic secrets check passed."
}

# 2. Update Version
Write-Step "2. updating Version to $Version"
if ($DryRun) {
    Write-Warning "[Dry Run] Would run: & `"$UpdateVersionScript`" -Version $Version"
}
else {
    & "$UpdateVersionScript" -Version $Version
}

# 3. Update Changelog
Write-Step "3. Updating Changelog"
$ChangelogPath = Join-Path $RepoRoot "CHANGELOG.md"
$Today = Get-Date -Format "yyyy-MM-dd"
if (Test-Path $ChangelogPath) {
    $content = Get-Content $ChangelogPath -Raw -Encoding UTF8
    # Replace [Unreleased] with [Version] - Date
    # Pattern: ## [Unreleased] -> ## [1.2.3] - 2024-01-01
    if ($content -match "## \[Unreleased\]") {
        if ($DryRun) {
            Write-Warning "[Dry Run] Would update CHANGELOG.md header to: ## [$Version] - $Today"
        }
        else {
            $content = $content -replace "## \[Unreleased\]", "## [$Version] - $Today"
            $content | Set-Content $ChangelogPath -Encoding UTF8
            Write-Success "CHANGELOG.md updated."
        }
    }
    else {
        Write-Warning "Could not find '## [Unreleased]' section in CHANGELOG.md"
    }
}

# 4. Confirmation
Write-Step "4. Ready to Release"
Write-Host "Version: $Version"
Write-Host "Branch:  $branch"
Write-Host "Action:  Commit, Tag, and Push"

if ($DryRun) {
    Write-Success "Dry Run Complete. No changes made."
    exit 0
}

Write-Host "Press any key to continue or Ctrl+C to abort..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

# 5. Git Operations
Write-Step "5. Git Operations"

Write-Host "Adding files..."
git add .

Write-Host "Committing..."
git commit -m "chore(release): v$Version"

Write-Host "Tagging..."
git tag "v$Version"

Write-Host "Pushing to remote..."
git push origin main
git push origin "v$Version"

Write-Success "Release v$Version pushed to origin!"
