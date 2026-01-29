# Streamer.bot Giveaway Assistant - Comprehensive User Guide

> **Version**: 1.0.1
> **Compatibility**: Streamer.bot v0.2.3+ (Targeting .NET Framework 4.8 / C# 7.3)
> 📖 **Additional Documentation:**
>
> - **[FAQ.md](FAQ.md)** - Frequently asked questions and error messages
> - **[ADVANCED.md](ADVANCED.md)** - Security, performance, migration guides
> - **[README.md](README.md)** - Command cheat sheet and Setup Guide
> - **[ROADMAP.md](ROADMAP.md)** - Future enhancements and feature roadmap

---

## 📚 Table of Contents

1. [Introduction](#introduction)
2. [Installation & Verification](#installation--verification)
3. [Getting Started Tutorial](#getting-started-tutorial-your-first-giveaway) 🌟
4. [Quick Start Workflow](#quick-start-workflow)
5. [Common Workflows & Use Cases](#common-workflows--use-cases) 🌟
6. [Commands Reference](#commands-reference)
7. [Configuration Guide (JSON)](#configuration-guide-json)
   - [Global Settings](#global-settings)
   - [Profile Settings](#profile-settings)
   - [Toast Notifications](#toast-notifications)
8. [Advanced Features](#advanced-features)
   - [External Bot Integration](#external-bot-integration)
   - [Variables & Observability](#variables--observability)
   - [Performance & Limits](#performance--limits)
9. [Integrations (Wheel & OBS)](#integrations-wheel--obs)
10. [Troubleshooting](#troubleshooting)

---

## Introduction

The **Giveaway Assistant** is a robust, "enterprise-grade" giveaway system designed for high-volume broadcasts. It runs
entirely within Streamer.bot but offers features usually found in standalone applications.

**Why use this over simple random pickers?**

- **Multi-Profile**: Run a "Daily Gold" giveaway and a "Monthly Grand Prize" simultaneously with different rules.
- **Anti-Bot Security**: Validates account age, detects "keyboard smash" names (Entropy), and strictly prevents bot
  trigger loops.
- **Observability**: Exposes real-time metrics (entries/sec, errors) to your OBS overlay.
- **Visuals**: Automates **Wheel of Names** spins directly in OBS.
- **Feedback & Transparency**:
  - **Windows Toast Notifications** for critical events (winner selected, giveaway opened/closed)
  - **Chat Confirmations** for all management commands and entry submissions
  - **Automated File Exports** (entry lists, winner logs with timestamps)
  - **Real-Time Logs** with searchable debug files for complete audit trails

---

## Installation & Verification

### Step 1: Create the Action

1. Open **Streamer.bot**.
2. Go to the **Actions** tab.
3. Right-click -> **Add** -> Name it `Giveaway Core`.
4. In the `Giveaway Core` action, Right-click -> **Core** -> **C#** -> **Execute C# Code**.

### Step 2: Import the Code

1. Open the `GiveawayBot.cs` file provided in this package.
2. Copy the **entire contents**.
3. Paste it into the Streamer.bot C# editor window.
4. **References**: Ensure standard references are loaded (usually automatic).
5. Click **Refs**, then **Compile** to ensure there are no errors.
6. Click **Save and Close**.

### Step 3: Add Basic Triggers

Add separate **Command** triggers for the main controls:

- `!enter` (Everyone)
- `!giveaway` (Moderator+)
- `!start` (Moderator+)
- `!end` (Moderator+)
- `!draw` (Moderator+)
- `!giveaway data delete` (Broadcaster+)

### Step 4: System Verification (New!)

Before going live, run the built-in health check.

1. In your chat, type: `!giveaway system test`
2. The bot will run a full diagnostic:
   - ✅ File System Write Access
   - ✅ Configuration Integrity
   - ✅ Persistence Check (Global Vars)
   - ✅ Platform Connectivity (Twitch/YouTube/Kick)
   - ✅ API Key presence

If any step fails, the bot will tell you exactly what is wrong.

---

## Getting Started Tutorial: Your First Giveaway

**🎯 Goal**: Run a complete giveaway from start to finish in 5 minutes.

**Prerequisites:**

- ✅ Bot installed (see [Installation](#installation--verification))
- ✅ Triggers configured (`!enter`, `!start`, `!draw`, `!end`)
- ✅ System test passed (`!giveaway system test`)

### Step 1: Verify Setup (30 seconds)

**Action**: In your Twitch chat, type:

```bash
!giveaway system test
```

**Expected Output** (in chat):

```text
🧪 SYSTEM CHECK:
✅ File Write: OK
✅ Config: Valid
✅ Persistence: Working
✅ Platform: Twitch connected
⚠ Wheel API: No key set (optional)

Status: READY 🎉
```

**Common Mistakes:**

- ❌ Bot doesn't respond → Check that `Giveaway Core` action has triggers configured
- ❌ "File Write: FAILED" → Run Streamer.bot as administrator OR move it out of Program Files

---

### Step 2: Start the Giveaway (10 seconds)

**Action**: Type in chat:

```bash
!start
```

**Expected Output**:

```text
🎉 Giveaway 'Main' is now OPEN! Type !enter to join!
```

**Behind the Scenes:**

- Bot sets `IsActive = true` for Main profile
- Creates empty entry list
- If `ExposeVariables: true`, OBS variables update immediately

**Troubleshooting:**

- ❌ "Permission denied" → Only broadcaster/mods can use `!start`
- ❌ "Already active" → Type `!end` first to close previous giveaway

---

### Step 3: Collect Entries (1-2 minutes)

**Action**: Have viewers type `!enter` (you can test yourself):

```bash
!enter
```

**Expected Output** (per entry):

```text
✅ CoolViewer123 entered the giveaway! (Tickets: 1)
```

**What's Happening:**

- Bot validates username (checks entropy, account age if configured)
- Checks for duplicates (one entry per user)
- Applies sub luck (if user is subscribed)
- Logs to `logs/General/YYYY-MM-DD.log`

**Test Sub Luck:**
If a subscriber enters, they'll see:

```text
✅ SubUser456 entered the giveaway! (Tickets: 3)
```

_(Assumes `SubLuckMultiplier: 2` → 1 base + 2 bonus = 3 tickets)_

**Common Issues:**

- ❌ "Duplicate" message → User already entered (working as intended)
- ❌ Entry rejected silently → Check logs for reason (spam limit, entropy, etc.)

---

### Step 4: Close Entries (5 seconds)

**Action**: Type:

```bash
!end
```

**Expected Output**:

```text
🚫 Giveaway 'Main' is now CLOSED. No more entries accepted.
📊 Total Entries: 12 | Total Tickets: 18
```

**Behind the Scenes:**

- Bot sets `IsActive = false`
- If `DumpEntriesOnEnd: true`, creates `.../dumps/Main/YYYYMMDD_Entries.txt`
- Entry list preserved for drawing

---

### Step 5: Pick a Winner! (10 seconds)

**Action**: Type:

```bash
!draw
```

**Expected Output**:

```text
🎊 WINNER: CoolViewer123 (UserID: 123456789)
🎟 Drew ticket #7 out of 18 total tickets
```

**What Happens:**

1. Bot uses weighted RNG (tickets are "raffle tickets")
2. Selects winner
3. If `DumpWinnersOnDraw: true`, appends to `.../dumps/Main/YYYYMMDD_Winners.txt`
4. If `EnableWheel: true`, creates Wheel of Names and updates OBS source

**Advanced (Wheel Integration):**
If you have Wheel API configured, you'll also see:

```text
🎡 Wheel URL: https://wheelofnames.com/abc-def
🖥 OBS Browser Source updated: MyScene/WheelSource
```

---

### Step 6: Verify Results (1 minute)

**Check File Outputs:**

1. Open `Streamer.bot/data/Giveaway Helper/dumps/Main/`
2. You should see:
   - `20260128_Entries.txt` (list of all entrants)
   - `20260128_Winners.txt` (winner name + timestamp)

**Example `Entries.txt`:**

```text
[14:23:45] CoolViewer123 (123456789) - Tickets: 1
[14:23:47] SubUser456 (987654321) - Tickets: 3
[14:24:01] AnotherUser (555666777) - Tickets: 1
...
```

**Example `Winners.txt`:**

```text
[2026-01-28 14:30:15] CoolViewer123 (123456789) - Ticket #7/18
```

---

### 🎉 Congratulations

You've completed your first giveaway!

**Next Steps:**

- **Customize**: Edit `giveaway_config.json` to adjust settings
- **Multi-Giveaway**: Create profiles with `!giveaway profile create Weekly`
- **Wheel Visuals**: Get API key from [Wheel of Names](https://wheelofnames.com/api-doc)
- **OBS Integration**: See [Integrations section](#integrations-wheel--obs)

**Common Questions:**

- "Can I re-draw if I don't like the winner?" → Yes, use `!draw` again (pulls from same pool)
- "How do I reset for a new giveaway?" → Type `!start` again (clears old entries)
- "Can I export entries to Excel?" → Yes, the `.txt` files are tab-separated

---

## Quick Start Workflow

**For Experienced Users** (assumes installation complete):

1. **Start the Giveaway**:
   - Type `!start` (or `!giveaway start`).
   - Bot responds: "Giveaway 'Main' is OPEN!".
   - It begins accepting `!enter` commands.

2. **Collect Entries**:
   - Viewers type `!enter`.
   - Bot validates them (Age, Spam, etc.) and logs them.
   - Distinct entries + Sub luck are calculated automatically.

3. **End & Draw**:
   - Type `!end` to close entries.
   - Type `!draw` to pick a winner.
   - **Wheel Integration**: If enabled, this will generate a Wheel of Names link and update your OBS Browser Source automatically.

---

## Common Workflows & Use Cases

### 📅 Scenario 1: Daily Sub-Only Giveaway

**Goal**: Run a recurring giveaway exclusively for subscribers.

**Setup:**

1. Create dedicated profile:

   ```bash
   !giveaway profile create SubDaily
   ```

2. Configure subscriber requirement (edit `giveaway_config.json`):

   ```json
   "SubDaily": {
     "MaxEntriesPerMinute": 100,
     "SubLuckMultiplier": 0,  // All subs get 1 ticket (fair)
     "RequireSubscriber": true,  // Only subs can enter
     "Triggers": {
       "command:!subdaily": "Enter",
       "command:!subdraw": "Draw"
     }
   }
   ```

3. **Daily Routine:**
   - 09:00 AM: `!giveaway profile SubDaily start`
   - All day: Subs type `!subdaily`
   - 09:00 PM: `!giveaway profile SubDaily end`
   - 09:01 PM: `!subdraw`

---

### 🎰 Scenario 2: Multi-Tier Giveaway (Bronze/Silver/Gold)

**Goal**: Run 3 simultaneous giveaways with different prize values.

**Setup:**

```json
"Profiles": {
  "Bronze": {
    "Triggers": { "command:!bronze": "Enter" },
    "SubLuckMultiplier": 0
  },
  "Silver": {
    "Triggers": { "command:!silver": "Enter" },
    "MinAccountAgeDays": 30,
    "SubLuckMultiplier": 1
  },
  "Gold": {
    "Triggers": { "command:!gold": "Enter" },
    "MinAccountAgeDays": 90,
    "RequireSubscriber": true,
    "SubLuckMultiplier": 0
  }
}
```

**Workflow:**

1. Start all simultaneously:

   ```bash
   !giveaway profile Bronze start
   !giveaway profile Silver start
   !giveaway profile Gold start
   ```

2. Viewers choose tier:
   - New accounts: `!bronze` only
   - 30+ day accounts: `!bronze` OR `!silver`
   - 90+ day subs: Any tier

3. Draw winners:

   ```bash
   !giveaway profile Bronze draw
   !giveaway profile Silver draw
   !giveaway profile Gold draw
   ```

---

### 📺 Scenario 3: Cross-Platform Giveaway (Twitch + YouTube)

**Goal**: Accept entries from both Twitch and YouTube simultaneously.

**Setup:**

1. Enable both platforms:

   ```json
   "Globals": {
     "EnabledPlatforms": ["Twitch", "YouTube"]
   }
   ```

2. Configure multi-platform triggers:

   ```json
   "Main": {
     "Triggers": {
       "command:!enter": "Enter",
       "platform:youtube,command:!join": "Enter"
     },
     "ExposeVariables": true
   }
   ```

3. **OBS Setup:**
   - Text Source: `Entries: %GiveawayBot_Main_EntryCount%`
   - Shows combined count from all platforms

**Note**: Entries are de-duplicated by UserID, so users on both platforms can only enter once.

---

### 🏆 Scenario 4: Long-Running Raffle (Week-Long)

**Goal**: Keep giveaway open for 7 days, dumping entries daily.

**Setup:**

```json
"Weekly": {
  "DumpEntriesOnEntry": true,
  "DumpEntriesOnEntryThrottle": 300,  // 5 min batch writes
  "StatePersistenceMode": "Both",     // Critical for stability
  "Triggers": {
    "command:!raffle": "Enter"
  }
}
```

**Workflow:**

1. **Monday**: `!giveaway profile Weekly start`
2. **Monday-Sunday**: Entries collected 24/7
3. **Daily Check**: Review `.../dumps/Weekly/YYYYMMDD_Entries_Incremental.txt`
4. **Sunday**: `!giveaway profile Weekly end` → `!giveaway profile weekly draw`

**Why Real-Time Dumps?**

- Protects against Streamer.bot crashes
- Audit trail for transparency
- Can manually verify entries mid-week

---

### 🎮 Scenario 5: Tournament Bracket Seeding

**Goal**: Use giveaway to randomly seed tournament brackets.

**Setup:**

1. Collect entries normally
2. **Modify Draw Count**: Instead of 1 winner, draw multiple

**Manual Method:**

```bash
!draw  // 1st seed
!draw  // 2nd seed
!draw  // 3rd seed
... (repeat 8 times for 8 seeds)
```

**Advanced (Custom Code)**:
Edit `HandleDrawWinner` to add`:

```csharp
// Draw N winners
var winners = new List<Entry>();
for (int i = 0; i < 8; i++)
{
    var winner = SelectWinner(state.Entries);
    winners.Add(winner);
    state.Entries.Remove(winner.UserId); // Remove from pool
}
Messenger.Send($"Tournament Seeds: {string.Join(", ", winners.Select(w => w.UserName))}");
```

---

### 🌐 Scenario 6: External Bot Integration (Nightbot)

**Goal**: Let Nightbot open/close giveaway via timed messages.

**Setup:**

1. Whitelist Nightbot:

   ```json
   "Main": {
     "AllowedExternalBots": ["Nightbot"],
     "ExternalListeners": [
       {
         "Pattern": "(?i)🎉.*GIVEAWAY.*OPEN",
         "Action": "Open"
       },
       {
         "Pattern": "(?i)🚫.*GIVEAWAY.*CLOSED",
         "Action": "Close"
       }
     ]
   }
   ```

2. **Nightbot Timed Message**:

   ```bash
   !addcommand !hourly "🎉 HOURLY GIVEAWAY IS NOW OPEN! Type !enter to win!"
   ```

3. **Nightbot Timer**:
   - Every hour: Nightbot posts message
   - Bot detects pattern → Auto-opens giveaway
   - 55 min later: Nightbot posts "🚫 GIVEAWAY CLOSED"
   - Bot auto-closes

**Benefits:**

- Fully automated
- No manual intervention
- Consistent schedule

---

**💡 More Ideas?**

- Monthly grand prize (persistent across streams)
- VIP-only giveaways
- Charity stream donation tiers
- Chat game integration (!trivia winner gets auto-entry)

See [FAQ.md](FAQ.md) for common questions and [ADVANCED.md](ADVANCED.md) for technical details.

---

## Commands Reference

> **Tip**: You can use shorter aliases for all management commands!
>
> - `!giveaway` can be shortened to `!ga`
> - `profile` can be shortened to `p`
>
> **Example**: `!giveaway profile list` is exactly the same as `!ga p list`.

### User Commands

| Command  | Permission | Description                                                 |
| :------- | :--------- | :---------------------------------------------------------- |
| `!enter` | Everyone   | Enter the "Main" giveaway. One entry per user (+ Sub luck). |

### Management Commands (Mod / Broadcaster)

| Command                                 | Description                                                   |
| :-------------------------------------- | :------------------------------------------------------------ |
| `!start` / `!open`                      | Open the Main giveaway for entries.                           |
| `!end` / `!close`                       | Close the Main giveaway (stop accepting entries).             |
| `!draw` / `!winner`                     | Pick a winner. Triggers Wheel API if enabled.                 |
| `!giveaway system test`                 | **Recommended**: Runs the full diagnostic health check.       |
| `!giveaway config gen`                  | Force-generates a default `giveaway_config.json` if missing.  |
| `!giveaway config check`                | Validates your current JSON configuration for errors/typos.   |
| `!giveaway regex test <pattern> <text>` | Test regex pattern against sample text. Returns match result. |

### Profile Management Commands (Advanced)

Manage different giveaway profiles (e.g., "Weekly", "SubOnly") without editing files.

| Command                                             | Description                                                                                |
| :-------------------------------------------------- | :----------------------------------------------------------------------------------------- |
| `!giveaway profile list`                            | Show all active profiles.                                                                  |
| `!giveaway profile create <Name>`                   | Create a new profile (e.g. `!giveaway profile create ShinyHunt`).                          |
| `!giveaway profile delete <Name> confirm`           | Delete a profile and its data.                                                             |
| `!giveaway profile clone <Src> <Dst>`               | Clone settings from one profile to another.                                                |
| `!giveaway profile config <Target> <Key>=<Val>`     | Edit settings. **Target** can be a name, comma-separated list (`Main,Side`), or `*`/`all`. |
| `!giveaway profile trigger <Name> add <spec> <act>` | Map a trigger. Ex: `!giveaway profile trigger Main add command:!ticket Enter`              |
| `!giveaway profile start <Target>`                  | Start giveaway(s). Supports batch targets (`*`, `all`, `Main,Side`).                       |
| `!giveaway profile end <Target>`                    | End giveaway(s). Supports batch targets (`*`, `all`, `Main,Side`).                         |
| `!giveaway data delete <user>`                      | **GDPR**: Permanently delete user data from memory and logs. (Aliases: `!ga gdpr delete`)  |

---

## Configuration Guide (JSON)

The bot uses a JSON file located at `.../Streamer.bot/data/Giveaway Helper/config/giveaway_config.json`.
You can edit this file directly or use the `!giveaway profile config` commands.

### Global Settings

These affect the entire bot behavior (found under `"Globals"` key).

| Setting                | Type   | Default                 | Description                                                                           |
| :--------------------- | :----- | :---------------------- | :------------------------------------------------------------------------------------ |
| `RunMode`              | String | `Mirror`                | `FileSystem` (File only), `GlobalVar` (Memory only), `Mirror` (Syncs both - Recom'd). |
| `StatePersistenceMode` | String | `Both`                  | Where to save active entries: `File`, `GlobalVar`, `Both`.                            |
| `LogRetentionDays`     | Int    | `90`                    | How many days to keep logs before deletion.                                           |
| `LogSizeCapMB`         | Int    | `100`                   | Maximum size of the logs folder in MB (prunes oldest first).                          |
| `WheelApiKeyVar`       | String | `WheelOfNamesApiKey`    | Name of the _Streamer.bot Variable_ holding your API key.                             |
| `EnabledPlatforms`     | List   | `["Twitch", "YouTube"]` | specific platforms to listen to and broadcast on.                                     |
| `FallbackPlatform`     | String | `Twitch`                | Default platform for messages if bot is offline.                                      |
| `MinUsernameEntropy`   | Double | `2.5`                   | Sensitivity for "smash name" detection (higher = stricter).                           |
| `ImportGlobals`        | Dict   | `{}`                    | Auto-import specific global variables on startup (e.g. `{"MyKey": "123"}`).           |
| `CustomStrings`        | Dict   | `{}`                    | Override bot response messages. Key=ID, Value=Text.                                   |

### Profile Settings

Each Profile (e.g. `"Main"`) has its own independent settings.

| Setting                      | Type  | Default | Description                                                                     |
| :--------------------------- | :---- | :------ | :------------------------------------------------------------------------------ |
| `MaxEntriesPerMinute`        | Int   | `60`    | Global rate limit to prevent chat floods/DOS.                                   |
| `SubLuckMultiplier`          | Int   | `2`     | Bonus tickets for Subscribers (0 = disabled).                                   |
| `MinAccountAgeDays`          | Int   | `0`     | Rejects users whose Twitch account is newer than X days.                        |
| `UsernamePattern`            | Regex | `null`  | Reject usernames not matching this Regex (e.g. `^[A-Za-z0-9_]+$`).              |
| `EnableEntropyCheck`         | Bool  | `false` | Enable "smash name" detection (e.g. `asdfgh`).                                  |
| `EnableWheel`                | Bool  | `false` | Uploads entrants to Wheel of Names API on `!draw`.                              |
| `EnableObs`                  | Bool  | `false` | Updates OBS Browser Source on `!draw`.                                          |
| `ExposeVariables`            | Bool  | `false` | **New**: If true, publishes all live stats to `GiveawayBot_<Profile>_...`.      |
| `DumpEntriesOnEnd`           | Bool  | `true`  | Saves a `.txt` file of all entrants when closed.                                |
| `DumpEntriesOnEntry`         | Bool  | `false` | Writes entries to txt file as accepted (real-time, throttled).                  |
| `DumpEntriesOnEntryThrottle` | Int   | `10`    | Seconds between batch writes (min: 5, max: 300). Auto-clamped if out of bounds. |
| `DumpWinnersOnDraw`          | Bool  | `true`  | Saves a `.txt` file of winners when drawn.                                      |

### Toast Notifications

Get visual alerts on your Windows desktop for bot events. Add this object to your Profile config:

```json
"ToastNotifications": {
  "EntryAccepted": false,    // Alert on every entry (Use with caution!)
  "EntryRejected": false,    // Alert when spam/bot blocked
  "WinnerSelected": true,    // Alert when winner drawn
  "GiveawayOpened": true,    // Alert when giveaway starts
  "GiveawayClosed": true     // Alert when giveaway ends
}
```

> [!NOTE]
> Toasts use standard Windows notifications. Ensure "Streamer.bot" is allowed to send notifications in Windows Settings.

---

## Advanced Features

### External Bot Integration

Want **Nightbot** or **Moobot** to control your giveaway? You can whitelist external bots.

1. **AllowedExternalBots**: Add the bot's username to this list in your profile config.

   ```json
   "AllowedExternalBots": [ "Nightbot", "Moobot" ]
   ```

2. **ExternalListeners**: Define regex patterns to match messages from whitelisted bots.

   **How it works:**
   - Bot sends message → Check if bot in `AllowedExternalBots` → Match against patterns → Execute action
   - `Pattern`: .NET Regex pattern to match in bot's chat message
   - `Action`: `"Open"`, `"Close"`, `"Winner"`, or `"Enter"`

   **Example**: Nightbot announces "🎉 THE GIVEAWAY IS NOW OPEN! Type !enter to join"

   ```json
   {
     "Pattern": "(?i)GIVEAWAY IS NOW OPEN",
     "Action": "Open"
   }
   ```

   **Testing your pattern:**
   - Use `!giveaway regex test <pattern> <text>` to test in chat
   - Online tool: [regex101.com](https://regex101.com) (.NET flavor)

   **Common Patterns Library:**

   ```json
   // Case-insensitive "giveaway" + "open"
   { "Pattern": "(?i)giveaway.*open", "Action": "Open" }

   // Starts with command
   { "Pattern": "^!startgiveaway", "Action": "Open" }

   // Contains exact phrase
   { "Pattern": "THE GIVEAWAY HAS ENDED", "Action": "Close" }

   // Winner announcement (capture username)
   { "Pattern": "(?i)winner.*@(\\w+)", "Action": "Winner" }
   ```

   _Note: The bot includes sophisticated anti-loop protection to prevent it from triggering itself._

### Variables & Observability

The bot exposes extremely detailed metrics to **Streamer.bot Global Variables**. You can use these in C# Actions or OBS
Text Sources (syntax `%VariableName%`).

**Core Metrics (Requires `ExposeVariables: true` in profile):**

- `GiveawayBot_Main_IsActive` (True/False)
- `GiveawayBot_Main_EntryCount` (Unique users)
- `GiveawayBot_Main_TicketCount` (Total tickets incl. luck)
- `GiveawayBot_Main_WinnerName`

**System Metrics (Always Available):**

- `GiveawayBot_Metrics_Entries_Total` (Lifetime entries)
- `GiveawayBot_Metrics_Entries_Rejected` (Blocked bots)
- `GiveawayBot_Metrics_Winners_Total` (Lifetime winners)
- `GiveawayBot_Metrics_ApiErrors` (Wheel API fail count)
- `GiveawayBot_Metrics_SystemErrors` (Internal exceptions)
- `GiveawayBot_Metrics_LoopDetected` (Anti-loop triggers prevented)

**Enhanced Diagnostics (Phase 9 - Cache & Anti-Loop):**

- `GiveawayBot_Metrics_CacheSize` (Message ID cache size)
- `GiveawayBot_Metrics_CleanupCount` (Cache cleanup operations)
- `GiveawayBot_Metrics_LoopByMsgId` (Loops detected via message ID)
- `GiveawayBot_Metrics_LoopByToken` (Loops detected via invisible token)
- `GiveawayBot_Metrics_LoopByBotFlag` (Loops detected via bot flag)
- `GiveawayBot_Metrics_ConfigReloads` (Configuration reload count)
- `GiveawayBot_Metrics_FileIOErrors` (File I/O error count)

**Performance Metrics (Phase 10 - Stopwatch Tracking):**

- `GiveawayBot_Metrics_EntriesProcessed` (Entry processing count)
- `GiveawayBot_Metrics_EntryProcessingTotalMs` (Total entry processing time)
- `GiveawayBot_Metrics_EntryProcessingAvgMs` (Average entry processing time)
- `GiveawayBot_Metrics_WinnerDrawAttempts` (Winner draw attempt count)
- `GiveawayBot_Metrics_WinnerDrawSuccesses` (Winner draw success count)
- `GiveawayBot_Metrics_WheelApiCalls` (Wheel API call count)
- `GiveawayBot_Metrics_WheelApiTotalMs` (Total Wheel API latency)
- `GiveawayBot_Metrics_WheelApiAvgMs` (Average Wheel API latency)

### Performance & Limits

- **Entries**: The system is tested to handle 10,000+ entries in memory.
- **Persistence**: Using `Mirror` mode ensures data survives Streamer.bot crashes by syncing to disk every 30s.

---

## Integrations (Wheel & OBS)

### 🎡 Wheel of Names

To enable the "Spin" effect:

1. Get an API Key from [Wheel of Names](https://wheelofnames.com/api-doc).
2. Save it in Streamer.bot's Global Variables Window named as `WheelOfNamesApiKey`.
3. Set `EnableWheel` to `'true'` in the same section as the API Key.
4. Set `WheelSettings.ShareMode` to `'copyable'` or `'private'`, in the same section as the API Key.

> [!IMPORTANT]
> **Security - API Key Encryption**
>
> 1. Paste your **plain text** API key into Streamer.bot variable `WheelOfNamesApiKey`
> 2. Bot **automatically encrypts** it with AES-256-CBC on next execution
> 3. Variable updates to `AES:...` (encrypted blob)
> 4. **Legacy keys**: Old `OBF:` keys are auto-converted to `AES:` (seamless upgrade)
> 5. **Machine-specific**: Encrypted key only works on this PC/user (re-enter if moving bot)
>
> **Security Notes:**
>
> - Keys are never logged (sanitized in all outputs)
> - AES encryption uses machine + username for key derivation (you don't need a separate password to encrypt/decrypt)
> - If encryption fails, error is logged but key remains in plaintext (you'll be notified)

### 🎥 OBS Source Control

1. Create a **Browser Source** in OBS. (Or use a source that you already have)
2. Note the **Scene Name** and **Source Name** exactly (Case Sensitive!).
3. Update Config in either json or Streamer.Bot Variables:

   ```json
   "EnableObs": true,
   "ObsScene": "MyScene",
   "ObsSource": "MyBrowserSource"
   ```

4. **Result**: When you type `!draw`, the bot gets a unique Wheel URL and instantly pushes it to that Browser Source.

---

## Troubleshooting

### Decision Tree: Problem Diagnosis

Use this flowchart to systematically diagnose issues:

```text
START: Having an issue?
│
├─→ [Bot not responding at all?]
│   │
│   ├─→ YES → Run !giveaway system test
│   │         │
│   │         ├─→ No response? → Check Streamer.bot Action triggers
│   │         │                  - Verify "Giveaway Core" action exists
│   │         │                  - Check triggers are enabled
│   │         │                  - Restart Streamer.bot
│   │         │
│   │         └─→ Gets response → Check test results:
│   │                             │
│   │                             ├─→ "File Write: FAILED"
│   │                             │   → Run Streamer.bot as Admin OR
│   │                             │     move folder out of Program Files
│   │                             │
│   │                             ├─→ "Config: Invalid"
│   │                             │   → Run !giveaway config check
│   │                             │     Fix JSON syntax errors
│   │                             │
│   │                             └─→ All tests passed
│   │                                 → Check specific command triggers
│   │
│   └─→ NO → Continue...
│
├─→ [Entries not being accepted?]
│   │
│   ├─→ Giveaway open? (Run !giveaway status)
│   │   │
│   │   ├─→ NO → Run !start first
│   │   │
│   │   └─→ YES → Check logs (.../logs/General/YYYY-MM-DD.log)
│   │             │
│   │             ├─→ "RATE LIMIT" → Normal spam protection
│   │             ├─→ "Low entropy" → Username validation working
│   │             ├─→ "Account too new" → MinAccountAgeDays check
│   │             └─→ "Duplicate" → User already entered
│   │
│   └─→ Continue...
│
├─→ [Wheel of Names not working?]
│   │
│   ├─→ API key set?
│   │   │
│   │   ├─→ NO → Get key from wheelofnames.com/api
│   │   │         Set WheelOfNamesApiKey variable
│   │   │
│   │   └─→ YES → Check logs for API errors
│   │             │
│   │             ├─→ "401 Unauthorized" → Invalid API key
│   │             ├─→ "429 Too Many Requests" → Rate limited
│   │             └─→ "Connection failed" → Internet/firewall issue
│   │
│   └─→ Wheel creates but OBS not updating?
│       → Verify OBS scene/source names (case-sensitive!)
│         Check OBS is on correct scene
│
├─→ [Variables not showing in OBS?]
│   │
│   ├─→ ExposeVariables = true in config?
│   │   │
│   │   ├─→ NO → Set to true, reload config
│   │   │
│   │   └─→ YES → Check OBS syntax: %VariableName% not {VariableName}
│   │             Refresh OBS scene (switch away and back)
│   │
│   └─→ Continue...
│
├─→ [Config changes not taking effect?]
│   │
│   ├─→ Check RunMode
│   │   │
│   │   ├─→ "FileSystem" → Edit JSON file directly
│   │   │                  Reload: !giveaway config reload
│   │   │
│   │   ├─→ "GlobalVar" → Edit Streamer.bot variables
│   │   │                Changes apply immediately
│   │   │
│   │   └─→ "Mirror" → Edit either (syncs both ways)
│   │                  Verify no JSON syntax errors
│   │
│   └─→ Continue...
│
└─→ [Still having issues?]
    │
    ├─→ Check logs: .../Giveaway Helper/logs/General/YYYY-MM-DD.log
    │   Look for [ERROR] or [WARN] messages
    │
    ├─→ Run diagnostics: !giveaway system test verbose
    │
    ├─→ Review FAQ.md for common issues
    │
    └─→ Check ROADMAP.md - feature might be planned but not implemented
```

---

### Common Issues & Quick Fixes

### "System Test Failed"

Run `!giveaway system test`.

- **Write Access**: Ensure Streamer.bot isn't in a protected folder (like Program Files) or run as Admin.
- **Config Error**: Run `!giveaway config check` to see specific JSON errors.

### "Loop Detected" in Logs

The bot has built-in protection. If you name your bot "StreamerBot" and your command is also "!giveaway", it might see
its own message.

- **Fix**: The bot automatically appends an invisible token (`\u200B`) to its messages to ignore them. Ensure you aren't
  stripping invisible characters in other actions.

### Profiles not syncing?

Check your `RunMode`. If set to `FileSystem`, variables might not update instantly. Use `Mirror` mode for best
experience.
