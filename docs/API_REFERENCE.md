# API Reference

Complete technical reference for the Giveaway Bot.

## Table of Contents

- [Configuration Classes](#configuration-classes)
- [Commands](#commands)
- [Trigger System](#trigger-system)
- [Global Variables](#global-variables)
- [Enums](#enums)

---

## Configuration Classes

### GiveawayBotConfig

Top-level configuration object for the entire giveaway system.

**File Location**: `giveaway_config.json`

**Properties**:

| Property   | Type                                        | Default             | Description                             |
| ---------- | ------------------------------------------- | ------------------- | --------------------------------------- |
| `Profiles` | `Dictionary<string, GiveawayProfileConfig>` | `{ "Main": {...} }` | Collection of giveaway profiles by name |
| `Globals`  | `GlobalSettings`                            | `{}`                | Global configuration settings           |

**Example**:

```json
{
  "Profiles": {
    "Main": { ... },
    "Weekly": { ... }
  },
  "Globals": {
    "RunMode": "Mirror",
    "LogLevel": "INFO"
  }
}
```

---

### GlobalSettings

System-wide configuration options.

**Properties**:

| Property                      | Type                          | Default    | Description                                  |
| ----------------------------- | ----------------------------- | ---------- | -------------------------------------------- |
| `RunMode`                     | `RunMode` (enum)              | `Mirror`   | Configuration persistence mode               |
| `StatePersistenceMode`        | `StatePersistenceMode` (enum) | `File`     | State file persistence mode                  |
| `StateSyncIntervalSeconds`    | `int`                         | `30`       | Sync frequency between file and globals      |
| `LogLevel`                    | `LogLevel` (enum)             | `INFO`     | Minimum log level to write                   |
| `LogMaxSizeKB`                | `int`                         | `100` (MB) | Maximum log file size before rotation        |
| `LogRetentionDays`            | `int`                         | `90`       | Days to keep old log files                   |
| `ImportGlobals`               | `bool`                        | `false`    | Auto-import settings to Streamer.bot globals |
| `CustomStrings`               | `Dictionary<string, string>`  | `{}`       | Override default bot response messages       |
| `FallbackPlatform`            | `string`                      | `"twitch"` | Platform to use when offline/unavailable     |
| `WheelOfNamesApiKey`          | `string`                      | `null`     | API key for Wheel of Names integration       |
| `ConfigReloadIntervalSeconds` | `int`                         | `5`        | Frequency to check for config updates        |
| `MinUsernameEntropy`          | `double`                      | `2.5`      | Minimum entropy score for bot detection      |
| `RegexTimeoutMs`              | `int`                         | `100`      | Max execution time for regex patterns        |
| `SpamWindowSeconds`           | `int`                         | `60`       | Time window for rate limiting                |
| `EncryptionSalt`              | `string`                      | `null`     | **Internal**: Salt for portable encryption   |

**Example**:

```json
{
  "Globals": {
    "RunMode": "Mirror",
    "LogLevel": "DEBUG",
    "ImportGlobals": true,
    "CustomStrings": {
      "entry_accepted": "✅ You're in the giveaway, {user}!",
      "entry_rejected_duplicate": "{user}, you're already entered!"
    }
  }
}
```

---

### GiveawayProfileConfig

Configuration for an individual giveaway profile.

**Properties**:

| Property                            | Type                         | Default         | Description                                  |
| ----------------------------------- | ---------------------------- | --------------- | -------------------------------------------- |
| `Triggers`                          | `Dictionary<string, string>` | See below       | Maps trigger patterns to actions             |
| `MaxEntriesPerMinute`               | `int`                        | `45`            | Rate limit for entries per minute            |
| `SubLuckMultiplier`                 | `int`                        | `2`             | Bonus tickets for subscribers                |
| `EnableWheel`                       | `bool`                       | `false`         | Enable Wheel of Names integration            |
| `EnableObs`                         | `bool`                       | `false`         | Enable OBS source control                    |
| `ObsScene`                          | `string`                     | `"Giveaway"`    | OBS scene name                               |
| `ObsSource`                         | `string`                     | `"WheelSource"` | OBS browser source name                      |
| `ExposeVariables`                   | `bool`                       | `false`         | Sync state to Streamer.bot globals           |
| `DumpFormat`                        | `DumpFormat` (enum)          | `TXT`           | Format for dump files                        |
| `DumpEntriesOnEnd`                  | `bool`                       | `true`          | Dump entries when giveaway ends              |
| `DumpEntriesOnEntry`                | `bool`                       | `false`         | Real-time entry dumping (throttled)          |
| `DumpEntriesOnEntryThrottleSeconds` | `int`                        | `10`            | Batch frequency for real-time dumps          |
| `DumpWinnersOnDraw`                 | `bool`                       | `true`          | Dump winner info after selection             |
| `UsernamePattern`                   | `string`                     | `null`          | Regex validation for usernames               |
| `MinAccountAgeDays`                 | `int`                        | `180`           | Minimum account age requirement (0=disabled) |
| `EnableEntropyCheck`                | `bool`                       | `true`          | Detect keyboard-smash usernames              |
| `AllowedExternalBots`               | `List<string>`               | `[]`            | Bot usernames allowed to trigger actions     |
| `ExternalListeners`                 | `List<BotListenerRule>`      | `[]`            | Regex rules for external bot parsing         |
| `ToastNotifications`                | `Dictionary<string, bool>`   | See below       | Toast notification toggles                   |
| `Messages`                          | `Dictionary<string, string>` | `null`          | Custom overrides for bot messages            |
| `TimerDuration`                     | `string`                     | `null`          | Auto-close duration (e.g., "10m", "1h")      |
| `WinChance`                         | `double`                     | `1.0`           | Win probability multiplier                   |
| `RequireSubscriber`                 | `bool`                       | `false`         | Subscriber-only entries                      |
| `RedemptionCooldownMinutes`         | `int`                        | `0`             | Cooldown between redemptions (0=disabled)    |
| `GameFilter`                        | `string`                     | `null`          | Game-specific validation preset (e.g. 'GW2') |
| `WheelSettings`                     | `WheelConfig`                | `{}`            | Wheel of Names configuration                 |

**Default Triggers**:

```json
{
  "command:!enter": "Enter",
  "command:!draw": "Winner",
  "command:!start": "Open",
  "command:!end": "Close"
}
```

**Default Toast Notifications**:

```json
{
  "EntryAccepted": false,
  "EntryRejected": false,
  "WinnerSelected": true,
  "GiveawayOpened": true,
  "GiveawayClosed": true,
  "UnauthorizedAccess": true
}
```

---

### GiveawayState

Runtime state for a giveaway profile. **Auto-managed** - do not edit manually.

**File Location**: `state/{ProfileName}.json`

**Properties**:

| Property            | Type                        | Description                                 |
| ------------------- | --------------------------- | ------------------------------------------- |
| `CurrentGiveawayId` | `string`                    | Unique ID for current giveaway session      |
| `IsActive`          | `bool`                      | Whether giveaway is open for entries        |
| `AutoCloseTime`     | `DateTime?`                 | Scheduled close time (if timed)             |
| `Entries`           | `Dictionary<string, Entry>` | All entries indexed by username (lowercase) |
| `HistoryLog`        | `List<string>`              | Event log for this profile                  |
| `LastWinnerName`    | `string`                    | Most recent winner's display name           |
| `LastWinnerUserId`  | `string`                    | Most recent winner's user ID                |
| `WinnerCount`       | `int`                       | Total winners drawn all-time                |
| `CumulativeEntries` | `long`                      | Total entries accepted all-time             |

**Persistence**: Determined by `StatePersistenceMode` (File, GlobalVar, or Both).

---

## Commands

### Entry Commands

| Command  | Permission | Description               |
| -------- | ---------- | ------------------------- |
| `!enter` | Everyone   | Enter the active giveaway |

**Usage**:

```bash
!enter
```

**Triggers**:

- Configured via `Triggers` in profile config
- Default: `"command:!enter": "Enter"`

---

### Management Commands

| Command  | Permission            | Description               |
| -------- | --------------------- | ------------------------- |
| `!start` | Moderator/Broadcaster | Open giveaway for entries |
| `!end`   | Moderator/Broadcaster | Close giveaway            |
| `!draw`  | Moderator/Broadcaster | Pick a winner             |
| `!clear` | Moderator/Broadcaster | Clear all entries         |

**Usage**:

```bash
!start
!end
!draw
!clear
```

---

### System Commands

#### `!giveaway system test`

**Description**: Comprehensive diagnostic check of bot configuration and environment.

**Permission**: Moderator/Broadcaster

**Checks**:

- File system permissions
- Configuration validity
- Trigger setup
- State file accessibility
- Log file writeability
- Wheel API connectivity (if enabled)

**Example Output**:

```text
=========================================
GIVEAWAY BOT - SYSTEM TEST
Version: 1.0.0
=========================================
✓ Config loaded successfully
✓ 2 profiles found: Main, Weekly
✓ File system access OK
✓ State files accessible
✓ Log files writable
✓ All checks passed!
=========================================
```

---

#### `!giveaway config check`

**Description**: Validates configuration and displays current settings.

**Permission**: Moderator/Broadcaster

**Output**:

- RunMode and persistence settings
- All profiles with trigger counts
- Anti-bot settings per profile
- Variable exposure status

---

#### `!giveaway config gen`

**Description**: Generates default configuration file.

**Permission**: Moderator/Broadcaster

**Behavior**:

- Creates `giveaway_config.json` with example profiles
- Includes inline documentation comments
- Does NOT overwrite existing file

---

### Profile Management Commands

#### `!giveaway profile list`

**Description**: Lists all configured profiles with status.

**Permission**: Everyone

**Output**:

```text
Available Profiles:
• Main [OPEN] - 15 entries
• Weekly [CLOSED] - 0 entries
```

---

#### `!giveaway profile create <name>`

**Description**: Creates a new profile with default settings.

**Permission**: Moderator/Broadcaster

**Usage**:

```bash
!giveaway profile create SubOnly
```

---

#### `!giveaway profile delete <name>`

**Description**: Removes profile from configuration.

**Permission**: Moderator/Broadcaster

**Warning**: Also deletes state file!

**Usage**:

```bash
!giveaway profile delete OldProfile
```

---

#### `!giveaway profile config <name>`

**Description**: Displays configuration for specified profile.

**Permission**: Moderator/Broadcaster

**Usage**:

```bash
!giveaway profile config Main
```

---

#### `!giveaway profile start <target>`

**Description**: Opens one or more profiles for entries.

**Permission**: Moderator/Broadcaster

**Targets**:

- `<ProfileName>` - Single profile
- `*` or `all` - All profiles
- `Profile1,Profile2` - Comma-separated list

**Usage**:

```bash
!giveaway profile start Main
!giveaway profile start *
!giveaway profile start Main,Weekly
```

---

#### `!giveaway profile end <target>`

**Description**: Closes one or more profiles.

**Permission**: Moderator/Broadcaster

**Usage**: Same as `profile start`

---

#### `!giveaway profile export <name>`

**Description**: Exports profile configuration to JSON file.

**Permission**: Moderator/Broadcaster

**Output**: `export/{ProfileName}_config.json`

**Usage**:

```bash
!giveaway profile export Main
```

---

#### `!giveaway profile import <name>`

**Description**: Imports profile from JSON file.

**Permission**: Moderator/Broadcaster

**Input**: `import/{ProfileName}.json`

**Usage**:

```bash
!giveaway profile import NewProfile
```

---

### Statistics Commands

#### `!giveaway stats`

**Description**: Shows statistics for active profile.

**Permission**: Everyone

**Output**:

```text
Main Giveaway Stats:
• Status: OPEN
• Entries: 15
• Total Tickets: 23
• Winners Drawn: 5
• Total Entries All-Time: 127
```

---

#### `!giveaway stats global`

**Description**: Shows aggregate statistics across all profiles.

**Permission**: Moderator/Broadcaster

**Output**:

- Total entries across all profiles
- Total winners drawn
- Unique users participated
- Active profiles count

---

### Data Management Commands

#### `!giveaway data delete <user>`

**Description**: GDPR-compliant data deletion.

**Permission**: Broadcaster

**Removes**:

- All entries for specified user (all profiles)
- History log mentions
- Global metrics
- User-specific variables

**Usage**:

```bash
!giveaway data delete BadUser123
```

---

## Trigger System

### Trigger Format

Triggers use the format: `"Type:Value": "Action"`

**Syntax**:

```json
{
  "Triggers": {
    "[Type]:[Value]": "[Action]"
  }
}
```

---

### Supported Trigger Types

| Type      | Description        | Example                       |
| --------- | ------------------ | ----------------------------- |
| `command` | Chat command       | `"command:!enter": "Enter"`   |
| `sd`      | Stream Deck button | `"sd:BUTTON-UUID": "Winner"`  |
| `name`    | Hotkey/Timer name  | `"name:DrawKey": "Winner"`    |
| `id`      | Event ID           | `"id:12345-ABC": "Open"`      |
| `regex`   | Pattern match      | `"regex:^!j(oin)?$": "Enter"` |

---

### Supported Actions

| Action   | Description              |
| -------- | ------------------------ |
| `Enter`  | Add user to giveaway     |
| `Winner` | Pick and announce winner |
| `Open`   | Enable entries           |
| `Close`  | Disable entries          |
| `Clear`  | Remove all entries       |

---

### Multi-Trigger Example

```json
{
  "Triggers": {
    "command:!enter": "Enter",
    "command:!join": "Enter",
    "sd:ENTER-BUTTON-UUID": "Enter",
    "name:EnterHotkey": "Enter",
    "command:!draw": "Winner",
    "sd:DRAW-BUTTON-UUID": "Winner",
    "name:DrawHotkey": "Winner",
    "command:!start": "Open",
    "command:!open": "Open",
    "command:!end": "Close",
    "command:!close": "Close"
  }
}
```

---

## Global Variables

When `ExposeVariables: true` is set for a profile, the bot creates Streamer.bot global variables.

### Variable Naming Convention

```text
GiveawayBot_{ProfileName}_{VariableName}
```

### Per-Profile Variables

| Variable                                    | Type      | Description                   | Access  |
| ------------------------------------------- | --------- | ----------------------------- | ------- |
| `GiveawayBot_{Profile}_IsActive`            | `boolean` | Giveaway open status          | **R/W** |
| `GiveawayBot_{Profile}_EntryCount`          | `int`     | Number of unique entries      | Read    |
| `GiveawayBot_{Profile}_TicketCount`         | `int`     | Total tickets (with sub luck) | Read    |
| `GiveawayBot_{Profile}_Id`                  | `string`  | Current giveaway session ID   | Read    |
| `GiveawayBot_{Profile}_WinnerName`          | `string`  | Last winner's display name    | Read    |
| `GiveawayBot_{Profile}_WinnerCount`         | `int`     | Total winners all-time        | Read    |
| `GiveawayBot_{Profile}_CumulativeEntries`   | `long`    | Total entries all-time        | Read    |
| `GiveawayBot_{Profile}_TimerDuration`       | `string`  | Current auto-close setting    | **R/W** |
| `GiveawayBot_{Profile}_Msg_{Key}`           | `string`  | Custom message override       | **R/W** |
| `GiveawayBot_{Profile}_MaxEntriesPerMinute` | `int`     | Rate limit override           | **R/W** |
| `GiveawayBot_{Profile}_RequireSubscriber`   | `bool`    | Subscriber-only toggle        | **R/W** |
| `GiveawayBot_{Profile}_SubLuckMultiplier`   | `decimal` | Subscriber luck bonus         | **R/W** |

### Configuration Variables

| Variable                              | Type   | Description                      |
| ------------------------------------- | ------ | -------------------------------- |
| `GiveawayBot_Profile_Config`          | `JSON` | Full profile configuration       |
| `GiveawayBot_Profile_Config_Triggers` | `JSON` | Trigger map only                 |
| `GiveawayBot_ExposeVariables`         | `Bool` | Global override for all profiles |

### OBS Integration

**Use in OBS Text Sources**:

```text
Entries: %GiveawayBot_Main_EntryCount%
Status: %GiveawayBot_Main_IsActive%
Last Winner: %GiveawayBot_Main_WinnerName%
```

**Dynamic Visibility** (show/hide based on status):

```text
%GiveawayBot_Main_IsActive%
```

Set text source visibility based on this boolean variable.

---

## Enums

### RunMode

Configuration persistence behavior.

| Value         | Description                                                |
| ------------- | ---------------------------------------------------------- |
| `FileSystem`  | Use local JSON file only (fast, local)                     |
| `GlobalVar`   | Use Streamer.bot global variable only (memory/SB internal) |
| `ReadOnlyVar` | Load from global variable, forbid writes                   |
| `Mirror`      | Bidirectional sync between file and global variable        |

**Recommendation**: Use `Mirror` for redundancy.

---

### StatePersistenceMode

State file persistence behavior.

| Value       | Description                                     |
| ----------- | ----------------------------------------------- |
| `File`      | Save state to JSON file only                    |
| `GlobalVar` | Save state to Streamer.bot global variable only |
| `Both`      | Save to both file and global variable           |

**Recommendation**: Use `Both` for maximum reliability.

---

### LogLevel

Minimum log severity to write.

| Value     | Description                                  |
| --------- | -------------------------------------------- |
| `TRACE`   | Verbose debugging (every entry, every check) |
| `VERBOSE` | Detailed flow information                    |
| `DEBUG`   | Development diagnostics                      |
| `INFO`    | Normal operation events                      |
| `WARN`    | Warnings and recoverable errors              |
| `ERROR`   | Errors that need attention                   |
| `FATAL`   | Critical failures                            |

**Recommendation**: Use `INFO` for production, `DEBUG` for troubleshooting.

---

### DumpFormat

Output format for entry/winner dumps.

| Value  | Description                    | Extension |
| ------ | ------------------------------ | --------- |
| `TXT`  | Plain text, one entry per line | `.txt`    |
| `CSV`  | Comma-separated values         | `.csv`    |
| `JSON` | Structured JSON array          | `.json`   |

**Example Output Locations**:

- Entries: `dump/{ProfileName}_entries_{timestamp}.{ext}`
- Winners: `dump/{ProfileName}_winners_{timestamp}.{ext}`

---

## Code Examples

### Basic Configuration

```json
{
  "Profiles": {
    "Main": {
      "Triggers": {
        "command:!enter": "Enter",
        "command:!draw": "Winner"
      },
      "MaxEntriesPerMinute": 100,
      "SubLuckMultiplier": 2,
      "ExposeVariables": true,
      "TimerDuration": "10m",
      "Messages": {
        "GiveawayOpened": "🚨 HYPE! A 10-minute giveaway has started!"
      }
    }
  },
  "Globals": {
    "RunMode": "Mirror",
    "LogLevel": "INFO"
  }
}
```

### Multi-Profile Setup

```json
{
  "Profiles": {
    "Daily": {
      "Triggers": {
        "command:!daily": "Enter",
        "command:!dailydraw": "Winner"
      },
      "MaxEntriesPerMinute": 60
    },
    "Weekly": {
      "Triggers": {
        "command:!weekly": "Enter",
        "command:!weeklydraw": "Winner"
      },
      "MaxEntriesPerMinute": 100,
      "SubLuckMultiplier": 5
    },
    "SubOnly": {
      "Triggers": {
        "command:!subenter": "Enter"
      },
      "RequireSubscriber": true
    }
  }
}
```

### Anti-Bot Configuration

```json
{
  "Profiles": {
    "Main": {
      "MinAccountAgeDays": 30,
      "EnableEntropyCheck": true,
      "UsernamePattern": "^[A-Za-z0-9_]{3,25}$",
      "MaxEntriesPerMinute": 60
    }
  }
}
```

### External Bot Integration

```json
{
  "Profiles": {
    "Main": {
      "AllowedExternalBots": ["Nightbot", "Moobot"],
      "ExternalListeners": [
        {
          "Pattern": "^(\\w+) entered the giveaway!$",
          "Action": "Enter",
          "UsernameGroup": 1
        }
      ]
    }
  }
}
```

### Wheel of Names Integration

```json
{
  "Profiles": {
    "Main": {
      "EnableWheel": true,
      "WheelSettings": {
        "Title": "GIVEAWAY WHEEL",
        "SpinTime": 10,
        "DisplayWinnerDuration": 5
      }
    }
  },
  "Globals": {
    "WheelOfNamesApiKey": "your-encrypted-key-here"
  }
}
```

---

## See Also

- [USER_GUIDE.md](USER_GUIDE.md) - Installation and basic usage
- [ADVANCED.md](ADVANCED.md) - Advanced features and customization
- [DEPLOYMENT.md](DEPLOYMENT.md) - First-time setup walkthrough
- [FAQ.md](FAQ.md) - Common questions and troubleshooting
