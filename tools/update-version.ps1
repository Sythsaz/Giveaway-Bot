param (
    [Parameter(Mandatory=$true)]
    [string]$Version
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path $MyInvocation.MyCommand.Path
$BaseDir = (Get-Item $ScriptDir).Parent.FullName

Write-Host "Updating version to $Version..." -ForegroundColor Cyan

# 1. Update VERSION file
$VersionFile = Join-Path $BaseDir "VERSION"
Set-Content -Path $VersionFile -Value $Version -NoNewline
Write-Host "Updated VERSION file." -ForegroundColor Green

# 2. Update StreamerBot.csproj
$CsprojFile = Join-Path $BaseDir "StreamerBot.csproj"
if (Test-Path $CsprojFile) {
    (Get-Content $CsprojFile) -replace "<Version>.*?</Version>", "<Version>$Version</Version>" | Set-Content $CsprojFile
    Write-Host "Updated StreamerBot.csproj." -ForegroundColor Green
}

# 3. Update GiveawayBot.cs
$BotFile = Join-Path $BaseDir "GiveawayBot.cs"
if (Test-Path $BotFile) {
    (Get-Content $BotFile) -replace 'public const string Version = ".*?"; // Semantic Versioning', "public const string Version = ""$Version""; // Semantic Versioning" | Set-Content $BotFile
    Write-Host "Updated GiveawayBot.cs." -ForegroundColor Green
}

# 4. Update RELEASE_NOTES.md Header
$ReleaseNotesFile = Join-Path $BaseDir "RELEASE_NOTES.md"
if (Test-Path $ReleaseNotesFile) {
    $Content = Get-Content $ReleaseNotesFile
    if ($Content[0] -match "# Release Notes v.*") {
        $Content[0] = "# Release Notes v$Version"
        $Content | Set-Content $ReleaseNotesFile
        Write-Host "Updated RELEASE_NOTES.md header." -ForegroundColor Green
    }
}

# 5. Update Wiki (if exists)
$WikiDir = Join-Path (Split-Path $BaseDir) "Giveaway-Bot.wiki"
if (Test-Path $WikiDir) {
    Write-Host "Wiki directory found at $WikiDir. Updating docs..." -ForegroundColor Yellow
    $MdFiles = Get-ChildItem -Path $WikiDir -Filter "*.md" -Recurse
    foreach ($File in $MdFiles) {
        $Content = Get-Content $File.FullName
        $NewContent = $Content -replace 'Version\*\*: \d+\.\d+\.\d+', "**Version**: $Version" `
                               -replace 'Version: \d+\.\d+\.\d+', "Version: $Version" `
                               -replace 'New in v\d+\.\d+\.\d+', "New in v$Version"
        
        if ($Content -join "`n" -ne $NewContent -join "`n") {
            $NewContent | Set-Content $File.FullName
            Write-Host "  Updated $($File.Name)" -ForegroundColor Green
        }
    }
} else {
    Write-Host "Wiki directory not found at $WikiDir. Skipping wiki update." -ForegroundColor Yellow
}

Write-Host "Version update complete! Don't forget to update CHANGELOG.md manually." -ForegroundColor Cyan
