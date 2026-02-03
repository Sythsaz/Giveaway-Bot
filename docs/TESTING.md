# Testing Guide for Giveaway Bot

This guide covers how to verify the functionality of the Giveaway Bot, both through automated tests (for developers)
and manual verification (for users).

## 1. Automated Test Suite (Developers)

The repository includes a comprehensive C# test suite designed to run outside of Streamer.bot using a Mock CPH environment.

### Prerequisites

- .NET 8.0 SDK (or compatible) installed.
- VS Code or Visual Studio.

### Running the Tests

1. Open a terminal in the `_tests` directory:

   ```powershell
   cd "_tests"
   ```

2. Run the tests:

   ```powershell
   dotnet run
   ```

### Test Categories

The suite covers:

- **ConfigSyncTests**: Verifies global variable synchronization and profile parsing.
- **ProfileTests**: Tests CRUD operations for profiles (`create`, `delete`, `clone`).
- **ProfileLogicTests**: Validates entry logic, ticket calculation, and state management.
- **ProfileStrictnessTests**: Verifies strictness rules (RequireFollower, RequireSubscriber).
- **ProfilePersistenceTests**: Ensures data survives restarts (File/GlobalVar persistence).
- **CoreTests**: Checks logging, metrics, and system health.
- **IntegrationTests**: Tests external integrations (Mock Wheel of Names, External Bots).

---

## 2. Manual Verification (Streamer.bot)

For end-users or final integration testing inside Streamer.bot.

### Setup

1. **Import Code**: Copy `GiveawayBot.cs` into a C# Code sub-action in Streamer.bot.
2. **References**: Ensure `System.Net.Http.dll` and `Newtonsoft.Json.dll` are referenced.
3. **Compile**: Click "Find Refs" and "Compile" to ensure no errors.

### Smoke Test Checklist

Run these commands in your Twitch/YouTube chat to verify basic health.

| Command                  | Action         | Expected Result                                 |
| :----------------------- | :------------- | :---------------------------------------------- |
| `!giveaway config check` | Health Check   | Bot replies "Report: Configuration is VALID ✅" |
| `!giveaway profile list` | List Profiles  | Bot lists available profiles (e.g., "Main")     |
| `!giveaway start`        | Start Giveaway | "Giveaway 'Main' is now OPEN!"                  |
| `!enter`                 | Enter          | "Entry accepted! Total tickets: 1"              |
| `!draw`                  | Pick Winner    | "Winner is [User]!"                             |
| `!giveaway end`          | End Giveaway   | "Giveaway 'Main' is now CLOSED!"                |

### Advanced Scenarios

#### 1. Configuration Sync

- Verify that changing Global Variables updates the bot.

1. Go to Streamer.bot -> **Settings** -> **Global Variables**.
2. Find `Giveaway Global TimerDuration`.
3. Change value from `10m` to `5m`.
4. Run `!giveaway config check` in chat.
5. Verify bot logs/replies confirm the update, or check `giveaway_config.json` to see if it persisted.

#### 2. Persistence / Restart

1. Start a giveaway and get some entries.
2. Restart Streamer.bot.
3. Run `!giveaway status`.
4. Verify the giveaway is **still active** and entry count is preserved.

#### 3. Strictness Testing

1. Configure `RequireFollower = true` for a profile.
2. Use a non-follower account (or mock in Streamer.bot).
3. Type `!enter`.
4. Verify entry is **rejected** (Check logs for "Rejected: Not Following").

## 3. Troubleshooting

### Enable Debug Logs

If tests fail or behavior is unexpected:

1. Set Global Variable `Giveaway Global LogLevel` to `DEBUG` (or `TRACE` for deep inspection).
2. Check Streamer.bot Logs tab for `[GiveawayBot]` entries.

### Common Issues

- **"System Health Check Failed"**: Check if `Giveaway Bot` folder is writable.
- **Global Variable Mismatch**: Ensure you are using the correct PascalCase keys (e.g., `TimerDuration` not `Timer Duration`).
- **Metrics Not Updating**: Ensure the giveaway profile is ACTIVE when testing entries.
