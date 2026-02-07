param (
    [Parameter(Mandatory=$true)]
    [string]$Version
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path $MyInvocation.MyCommand.Path
$BaseDir = (Get-Item $ScriptDir).Parent.FullName

Write-Output "Updating version to $Version..."

# 1. Update VERSION file
$VersionFile = Join-Path $BaseDir "VERSION"
Set-Content -Path $VersionFile -Value $Version -NoNewline
Write-Output "Updated VERSION file."

# 2. Update StreamerBot.csproj
$CsprojFile = Join-Path $BaseDir "StreamerBot.csproj"
if (Test-Path $CsprojFile) {
    (Get-Content $CsprojFile) -replace "<Version>.*?</Version>", "<Version>$Version</Version>" | Set-Content $CsprojFile
    Write-Output "Updated StreamerBot.csproj."
}

# 3. Update GiveawayBot.cs
$BotFile = Join-Path $BaseDir "GiveawayBot.cs"
if (Test-Path $BotFile) {
    (Get-Content $BotFile) -replace 'public const string Version = ".*?"; // Semantic Versioning', "public const string Version = ""$Version""; // Semantic Versioning" | Set-Content $BotFile
    Write-Output "Updated GiveawayBot.cs."
}

# 4. Update RELEASE_NOTES.md Header
$ReleaseNotesFile = Join-Path $BaseDir "RELEASE_NOTES.md"
if (Test-Path $ReleaseNotesFile) {
    $Content = Get-Content $ReleaseNotesFile
    if ($Content[0] -match "# Release Notes v.*") {
        $Content[0] = "# Release Notes v$Version"
        $Content | Set-Content $ReleaseNotesFile
        Write-Output "Updated RELEASE_NOTES.md header."
    }
}

# 5. Update Wiki (if exists)
$WikiDir = Join-Path (Split-Path $BaseDir) "Giveaway-Bot.wiki"
if (Test-Path $WikiDir) {
    Write-Output "Wiki directory found at $WikiDir. Updating docs..."
    $MdFiles = Get-ChildItem -Path $WikiDir -Filter "*.md" -Recurse
    foreach ($File in $MdFiles) {
        $Content = Get-Content $File.FullName
        $NewContent = $Content -replace 'Version\*\*: \d+\.\d+\.\d+', "**Version**: $Version" `
            -replace 'Version: \d+\.\d+\.\d+', "Version: $Version" `
            -replace 'New in v\d+\.\d+\.\d+', "New in v$Version"

        if ($Content -join "`n" -ne $NewContent -join "`n") {
            $NewContent | Set-Content $File.FullName
            Write-Output "  Updated $($File.Name)"
        }
    }
}
else {
    Write-Warning "Wiki directory not found at $WikiDir. Skipping wiki update."
}

Write-Output "Version update complete! Don't forget to update CHANGELOG.md manually."
