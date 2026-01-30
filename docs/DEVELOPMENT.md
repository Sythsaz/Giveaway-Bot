# Developer Guide

## Development Setup

### Prerequisites

- **Visual Studio 2022** (Community Edition works fine) or **VS Code** or **JetBrains Rider**.
- **.NET Framework 4.8 Developer Pack**.
- **Streamer.bot** (latest version recommended, minimum v0.2.3).

### Project Structure

```text
/
├── GiveawayBot.cs        # Main script file (The Bot)
├── _tests/               # Unit and integration tests
├── .github/              # CI/CD and templates
├── docs/                 # Documentation
├── examples/             # Configuration examples
└── StreamerBot.csproj    # Project file for IntelliSense
```

### Dependency Management

The project references Streamer.bot DLLs (`StreamerBot.Plugin.Interface.dll`, `StreamerBot.Common.dll`).
These are **not** included in the repo. You must adhere to the Setup instructions in
[CONTRIBUTING.md](../CONTRIBUTING.md) to point the project to your local Streamer.bot installation.

## C# 7.3 Constraints

Streamer.bot compiles C# code using Roslyn with a language version target of **7.3**. This is a hard constraint **tied to
Streamer.bot's runtime environment**. If Streamer.bot upgrades to a newer .NET runtime, this project will adopt modern
C# features accordingly.

### Enforcement

This project enforces C# 7.3 compatibility at multiple layers:

1. **Build Time**: `StreamerBot.csproj` sets `<LangVersion>7.3</LangVersion>` to prevent compilation of C# 8.0+ code
2. **IDE**: `.editorconfig` suppresses suggestions for C# 8.0+ features
3. **Pre-Commit**: Git hook (`.git/hooks/pre-commit`) scans staged files for C# 8.0+ features before allowing commits

### What works

- Async/await
- LINQ (Standard)
- Tuples (ValueTuple)
- Local functions

### What DOES NOT work

- `??=` (Null-coalescing assignment) → Use `if (x == null) x = value;`
- `new()` (Target-typed new) -> Use `new ClassName()`
- `record` types -> Use `class` or `struct`
- `using var` -> Use `using (...) { }`
- `switch` expressions -> Use `switch` statements
- Nullable reference types (`string?`) -> Just use `string` (and assume it can be null)
- File-scoped namespace -> Use block-scoped namespace
- `#nullable` pragma -> Not supported

The `.editorconfig` file is heavily tuned to hide suggestions for these modern features.

## Testing

We use a custom test runner in `_tests/` because standard testing frameworks (NUnit/xUnit) are hard to integrate with the
single-file script format and Streamer.bot's environment.

### Running Tests

Open the `_tests` folder in a terminal:

```bash
dotnet run
```

### Adding Tests

1. Open `_tests/TestRunner.cs`.
2. Add a new method `async Task YourTestName(MockCPH cph)`.
3. Register it in the `_tests` array in `Main`.

## Debugging

Since the bot runs _inside_ Streamer.bot, traditional debugging is difficult.

### Strategies

1. **Logging**: Use `CPH.LogInfo()` / `CPH.LogDebug()`. These appear in the Streamer.bot Log tab.
2. **File Logging**: The bot uses `FileLogger` to write to `Giveaway Helper/logs/`. Check these files for detailed
   persistence and logic traces.
3. **Unit Tests**: The `MockCPH` allows you to step through code in your IDE (VS Code/Visual Studio) without running
   Streamer.bot. This is the **best** way to debug logic.

### Attaching Process

To debug the code running in Streamer.bot:

1. Compile the code in Streamer.bot (click "Compile").
2. In VS/Rider, "Attach to Process".
3. Select `Streamer.bot.exe`.
4. _Note: usage of breakpoints might fail if the compiled assembly doesn't match source exactly._

## Release Process

1. Update `CHANGELOG.md`.
2. Update the `Version` constant in `GiveawayBot.cs`.
3. Push changes.
4. Tag the commit with `vX.Y.Z`.
5. GitHub Actions will auto-generate the release.
