# Contributing to Giveaway Bot

Thank you for your interest in contributing to the Giveaway Bot! This document provides guidelines and information
to help you get started.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Environment](#development-environment)
- [Understanding C# 7.3 Constraints](#understanding-c-73-constraints)
- [EditorConfig Usage](#editorconfig-usage)
- [Making Changes](#making-changes)
- [Testing](#testing)
- [Versioning/Release process](#versioningrelease-process)
- [Submitting a Pull Request](#submitting-a-pull-request)
- [Reporting Issues](#reporting-issues)

## Code of Conduct

This project adheres to the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected
to uphold this code.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:

   ```bash
   git clone https://github.com/YOUR-USERNAME/Giveaway-Bot.git
   cd Giveaway-Bot
   ```

3. **Create a branch** for your changes:

   ```bash
   git checkout -b feature/my-awesome-feature
   ```

4. **Set up your development environment** (see below)

## Development Environment

### Requirements

- **Streamer.bot v0.2.3+** installed on your system
- **.NET Framework 4.8** (comes with Windows 10/11)
- **C# 7.3** compatible IDE (VS Code, Visual Studio, Rider)
- **Git** for version control

### Project Setup

The project references Streamer.bot DLLs which must be present on your machine.

1. **Ensure Streamer.bot is installed** at `C:\Streamer.bot` (default), or another location
2. **Configure the path** if your Streamer.bot install is elsewhere:
   - The project defaults to `C:\Streamer.bot`
   - If installed elsewhere (e.g., `D:\Streamer Bot`), create `StreamerBot.csproj.user`:

     ```xml
     <Project>
       <PropertyGroup>
         <StreamerBotPath>D:\Streamer Bot</StreamerBotPath>
       </PropertyGroup>
     </Project>
     ```

   - This file is `.gitignore`'d and won't affect others

3. **Build the project**:

   ```bash
   dotnet build
   ```

### Development Guide

For detailed information about the architecture, component design, and extension points, see
[docs/DEVELOPMENT.md](docs/DEVELOPMENT.md).

## Understanding C# 7.3 Constraints

**Critical**: This project targets **C# 7.3** and **.NET Framework 4.8** to maintain compatibility with Streamer.bot's
embedded scripting environment.

> [!IMPORTANT]
> **This constraint is tied to Streamer.bot's runtime**, not a design choice. When Streamer.bot upgrades to a newer
> .NET version, this project will adopt modern C# features accordingly.

### What This Means

- ❌ **No modern C# features**: No nullable reference types, pattern matching, target-typed new, using declarations,
  `??=` operator, etc.
- ❌ **No async Main**: Entry points must be synchronous
- ❌ **Limited LINQ**: Some .NET Core 2.0+ string methods unavailable (e.g., `string.Contains(char)`)
- ✅ **Use legacy syntax**: Explicit types, traditional using statements, verbose initialization

### Common Pitfalls

```csharp
// ❌ BAD - C# 9.0 target-typed new
List<string> items = new();

// ✅ GOOD - C# 7.3 compatible
List<string> items = new List<string>();

// ❌ BAD - C# 8.0 using declaration
using var stream = File.OpenRead("file.txt");

// ✅ GOOD - C# 7.3 traditional using
using (var stream = File.OpenRead("file.txt"))
{
    // ...
}

// ❌ BAD - C# 8.0 null-coalescing assignment
config ??= new Config();

// ✅ GOOD - C# 7.3 compatible
if (config == null) config = new Config();

// ❌ BAD - C# 8.0 switch expression
var result = value switch { 1 => "one", 2 => "two", _ => "other" };

// ✅ GOOD - C# 7.3 traditional switch
string result;
switch (value) {
    case 1: result = "one"; break;
    case 2: result = "two"; break;
    default: result = "other"; break;
}
```

## EditorConfig Usage

The project includes a **custom `.editorconfig`** file that suppresses hundreds of IDE warnings for modern C# features not
available in C# 7.3.

### Why We Need This

- **Modern IDEs** (VS Code, Visual Studio, Rider) default to suggesting C# 10+ features
- **Streamer.bot** uses an older compiler that doesn't support these features
- **Without suppressions**, the IDE would show 500+ false warnings

### What's Suppressed

The `.editorconfig` disables warnings for:

1. **Modern language features**: Target-typed new, using declarations, pattern matching, collection expressions, primary
   constructors
2. **Performance suggestions**: `string.Contains(char)`, static readonly arrays, member as static
3. **Style preferences**: var vs explicit type, expression bodies, braces on single-line statements
4. **Nullable reference types**: All CS86xx warnings (not supported in C# 7.3)

### Important Notes

> [!WARNING]
> **Do NOT rely on `.editorconfig` as a code style reference** for other projects.
>
> This configuration is **highly tailored** to suppress incompatibilities, not to enforce best practices. It prioritizes
> **eliminating noise** over standardizing style.

If you modify `.editorconfig`, you may need to:

1. Close and reopen your IDE, OR
2. Right-click solution → Reload Solution, OR
3. Restart your IDE

### Pre-Commit Hook

The repository includes a pre-commit hook (`.git/hooks/pre-commit`) that automatically scans staged C# files for C# 8.0+
features before allowing commits.

**Features detected**:

- `??=` null-coalescing assignment
- Switch expressions
- Index/range operators
- Using declarations
- Nullable reference type annotations
- Nullable pragmas

**Hook behavior**:

- ✅ Allows commit if no C# 8.0+ features detected
- ❌ Blocks commit and lists violations if incompatible features found

The hook runs automatically on `git commit`. To bypass (not recommended):

```bash
git commit --no-verify
```

## Making Changes

### Branch Naming

Use descriptive branch names:

- `feature/add-xyz` - New features
- `fix/issue-123` - Bug fixes
- `docs/update-readme` - Documentation changes
- `refactor/cleanup-abc` - Code refactoring

### Commit Messages

Write clear, concise commit messages:

```bash
# Good examples
git commit -m "feat: add multi-platform broadcasting support"
git commit -m "fix: resolve null reference in ConfigLoader"
git commit -m "docs: update USER_GUIDE with new commands"
git commit -m "refactor: extract validation logic to helper class"

# Use conventional commits format
# type(scope): description
# Types: feat, fix, docs, style, refactor, test, chore
```

### Code Style

- **Follow existing patterns** in the codebase
- **Use explicit types** instead of `var` where clarity helps
- **Add XML documentation** for public methods and classes
- **Keep compatibility** with C# 7.3 (see constraints above)
- **Avoid unnecessary complexity** - this runs in a constrained environment

## Testing

### Running Tests

The project includes a test suite in `_tests/`:

```bash
cd _tests
dotnet run
```

### Writing Tests

- Add new test cases to `_tests/TestRunner.cs`
- Use the `MockCPH` class to simulate Streamer.bot interactions
- Test both success and failure paths
- Verify C# 7.3 compatibility

### Manual Testing

1. **Copy `GiveawayBot.cs`** to Streamer.bot
2. **Import** into a C# action
3. **Configure** `giveaway_config.json` in `data/Giveaway Bot/config/`
4. **Run** `!giveaway system test` in chat
5. **Test your changes** with real commands

## Versioning/Release process

This repository uses a **single canonical version source**: the root `VERSION` file.

When preparing a release, bump all versioned artifacts together in one commit:

1. Update `VERSION` to the new semantic version (for example `1.5.4`).
2. Update the runtime constant in `GiveawayBot.cs` (`GiveawayManager.Version`).
   - `CPHInline.Version` must remain an alias to `GiveawayManager.Version` to avoid duplication.
3. Update `StreamerBot.csproj` `<Version>` to match `VERSION`.
4. Add a new release heading at the top of `CHANGELOG.md` (right below `## [Unreleased]`) using the same version number.
5. Run the version consistency check locally (or in CI) to confirm all values match.

The workflow `.github/workflows/version-consistency.yml` enforces this and fails if any of the following diverge:

- `CHANGELOG.md` latest release heading
- `GiveawayBot.cs` runtime version constant
- `StreamerBot.csproj` package metadata version
- `VERSION`

## Submitting a Pull Request

1. **Ensure your code compiles**:

   ```bash
   dotnet build
   ```

2. **Run the test suite**:

   ```bash
   cd _tests
   dotnet run
   ```

3. **Update documentation** if you changed behavior or added features
4. **Update CHANGELOG.md** under "Unreleased" section
5. **Push your branch**:

   ```bash
   git push origin feature/my-awesome-feature
   ```

6. **Open a Pull Request** on GitHub using the [PR template](.github/pull_request_template.md)
7. **Verify Version**: Ensure `VERSION`, `GiveawayBot.cs`, `StreamerBot.csproj`, and `CHANGELOG.md` match.

### Releasing a New Version

We use a streamlined **PR-based release workflow**. To release a new version:

1. **Prepare the Changelog**:
   - Ensure all notable changes are documented in `CHANGELOG.md` under the `[Unreleased]` section.
   - The script will automatically move these items to the new version section.

2. **Run the auto-release script**:

   ```powershell
   # Syntax: .\tools\auto-release.ps1 -Version <NEW_VERSION>
   .\tools\auto-release.ps1 -Version 1.6.0
   ```

   This script will automatically:
   - Bump version in `VERSION`, `.csproj`, `.cs`, and `.wiki`
   - Update `CHANGELOG.md` (preserving `[Unreleased]` section)
   - Create a temporary branch and open a Pull Request via GitHub CLI (`gh`)

3. **Merge the Pull Request**:
   - The script will wait for you to merge the PR.
   - Ensure all CI checks pass.
   - Merge the PR into `main`.

4. **Finalize**:
   - The script will detect the merge, pull `main`, and push the `v1.6.0` tag.
   - This tag triggers the final GitHub Release workflow.
   - **Cleanup**: The script automatically deletes both the local and remote release branches.

### Wiki Generation

If you modify configuration classes or commands, you must update the valid documentation.
The project includes a standalone tool in `tools/GenerateWiki.cs` to automate this.

**Prerequisites:**

1. **Clone the Wiki repository** as a sibling directory to your project (or verify your workspace structure):
   - `../Giveaway-Bot` (This repo)
   - `../Giveaway-Bot.wiki` (Wiki repo)

   The generator expects to output files to `../../Giveaway-Bot.wiki`.

**To compile and run:**

```bash
# 1. Compile (using standard Windows .NET compiler)
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:tools\GenerateWiki.exe tools\GenerateWiki.cs

# 2. Run the generator (MUST run from tools directory)
cd tools
.\GenerateWiki.exe
```

This will automatically update the markdown files in the sibling Wiki directory.

### PR Checklist

Your PR should include:

- ✅ Clear description of changes
- ✅ Type selected (bugfix, feature, docs, etc.)
- ✅ C# 7.3 compatibility verified
- ✅ Tests added/updated (if applicable)
- ✅ Documentation updated (if applicable)
- ✅ CHANGELOG.md updated
- ✅ No breaking changes (or clearly documented)

### Review Process

1. **Automated checks** will run (markdown linting)
2. **Maintainer review** within a few days
3. **Address feedback** if requested
4. **Merge** once approved

## Reporting Issues

### Bug Reports

Use the [Bug Report template](.github/ISSUE_TEMPLATE/bug_report.md) and provide:

- Bot version (`!giveaway config check` shows version)
- System test output (`!giveaway system test`)
- Configuration snippet (sanitized, no API keys!)
- Steps to reproduce
- Expected vs actual behavior
- Logs from `data/Giveaway Helper/logs/`

### Feature Requests

Use the [Feature Request template](.github/ISSUE_TEMPLATE/feature_request.md) and describe:

- The problem you're trying to solve
- Your proposed solution
- Alternative approaches considered
- Why this would benefit other users

### Security Vulnerabilities

**Do NOT open public issues for security vulnerabilities.**

See [SECURITY.md](SECURITY.md) for responsible disclosure guidelines.

## Questions?

- **Documentation**: See [docs/](docs/) folder
- **FAQ**: Check [docs/FAQ.md](docs/FAQ.md)
- **GitHub Issues**: Ask questions via [GitHub Issues](https://github.com/Sythsaz/Giveaway-Bot/issues)

---

Thank you for contributing to Giveaway Bot! 🎉
