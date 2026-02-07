$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path $MyInvocation.MyCommand.Path
$BaseDir = (Get-Item $ScriptDir).Parent.FullName
$HooksDir = Join-Path $BaseDir ".git\hooks"
$PreCommitFile = Join-Path $HooksDir "pre-commit"

if (-not (Test-Path $HooksDir)) {
    Write-Host "Error: .git/hooks directory not found. Is this a git repository?" -ForegroundColor Red
    exit 1
}

$HookContent = @'
#!/bin/sh
# Version Consistency Hook
# Blocks commits if VERSION file does not match StreamerBot.csproj

if [ -f "VERSION" ] && [ -f "StreamerBot.csproj" ]; then
    VERSION=$(cat VERSION | tr -d '[:space:]')
    CSPROJ_VERSION=$(grep "<Version>" StreamerBot.csproj | sed -e 's/.*<Version>\(.*\)<\/Version>.*/\1/' | tr -d '[:space:]')
    
    if [ "$VERSION" != "$CSPROJ_VERSION" ]; then
        echo "Error: Version mismatch detected!"
        echo "  VERSION file: $VERSION"
        echo "  .csproj file: $CSPROJ_VERSION"
        echo "Please run: .\tools\update-version.ps1 $VERSION"
        exit 1
    fi
fi
'@

Set-Content -Path $PreCommitFile -Value $HookContent -NoNewline
# No need for chmod on Windows, but git bash might care. 
# We'll stick to simple text file writing which works for Windows Git.

Write-Host "Pre-commit hook installed to $PreCommitFile" -ForegroundColor Green
