# Advanced Topics & Technical Guide

> **Version**: 1.3.2
>
> **[← Back to USER_GUIDE](USER_GUIDE.md) | [FAQ →](FAQ.md)**

---

## 📑 Table of Contents

1. [Security & Privacy](#security--privacy)
2. [Performance & Scaling](#performance--scaling)
3. [File Structure Reference](#file-structure-reference)
4. [Variable Reference](#variable-reference)
5. [Migration Guides](#migration-guides)
6. [Custom Integration Examples](#custom-integration-examples)
7. [Backup & Recovery](#backup--recovery)

---

## Security & Privacy

### Encryption Details

The bot uses **AES-256-CBC** encryption for API keys with the following implementation:

**Auto-Encryption (New in 1.3.2):**
If you set the global variable `GiveawayBot_Globals_WheelApiKey` to a plain text key (e.g. `abc-123`), the bot will
immediately detect it, validate it with the Wheel of Names API, and if valid, **encrypt it automatically**
(replacing the value with `ENC:...`).

**Key Derivation:**

```text
MachineID = Environment.MachineName
UserID = Environment.UserName
Salt = SHA256(MachineID + UserID)
EncryptionKey = First 32 bytes of Salt
IV = First 16 bytes of Salt
```

**Why machine-specific?**

- Prevents key theft (encrypted blob won't work on attacker's PC)
- No password storage needed
- Automatic, transparent to user

**Limitations:**

- **Moving PCs**: Must re-enter API key in plain text
- **User account changes**: Must re-enter if Windows username changes
- **Reinstalling Windows**: May invalidate keys

### Data Retention Policy

**What's Stored:**

| Data Type     | Location             | Retention                       | Purpose              |
| :------------ | :------------------- | :------------------------------ | :------------------- |
| Entry Records | `dumps/Main/*.txt`   | Manual deletion                 | Winner verification  |
| Logs          | `logs/General/*.log` | `LogRetentionDays` (default 90) | Debugging            |
| State Files   | `state/*.json`       | Until profile deleted           | Active giveaway data |
| Config        | `config/*.json`      | Permanent                       | Bot settings         |

**GDPR Compliance:**

- No PII beyond username/UserID (public Twitch data)
- Local storage only (except Wheel API if enabled)
- Users can request data deletion (manual file removal)

### API Key Security Best Practices

1. **Never share encrypted blobs** (`AES:...`) - they contain your key
2. **Limit API key scope** - use Wheel of Names' read-only keys if possible
3. **Rotate keys** - change periodically (update variable + re-encrypt)
4. **Monitor logs** - watch for `[Security]` warnings

---

## Performance & Scaling

### Recommended Settings by Stream Size

#### Small Streams (<100 viewers)

```json
{
  "MaxEntriesPerMinute": 60,
  "StateSyncIntervalSeconds": 30,
  "LogLevel": "INFO",
  "DumpEntriesOnEntry": false,
  "ExposeVariables": true
}
```

**Why:** Balanced performance, minimal overhead

#### Medium Streams (100-1,000 viewers)

```json
{
  "MaxEntriesPerMinute": 200,
  "StateSyncIntervalSeconds": 60,
  "LogLevel": "WARN",
  "DumpEntriesOnEntry": true,
  "DumpEntriesOnEntryThrottle": 30,
  "ExposeVariables": true
}
```

**Why:** Higher rate limits, less frequent disk writes

#### Large Streams (1,000+ viewers)

```json
{
  "MaxEntriesPerMinute": 500,
  "StateSyncIntervalSeconds": 120,
  "LogLevel": "ERROR",
  "DumpEntriesOnEntry": false,
  "ExposeVariables": false,
  "EnableEntropyCheck": true,
  "MinAccountAgeDays": 30
}
```

**Why:** Aggressive rate limiting, bot protection, minimal I/O

### Performance Bottlenecks

**Disk I/O (Slow HDD):**

- Symptom: Streamer.bot freezes when entries spike
- Solution 1: Set `StatePersistenceMode: GlobalVar` (risk: data loss on crash)
- Solution 2: Move Streamer.bot folder to SSD
- Solution 3: Increase `StateSyncIntervalSeconds` to 120+

**CPU (Regex Validation):**

- Symptom: High CPU when `UsernamePattern` or `ExternalListeners` enabled
- Solution: Simplify regex patterns, avoid backtracking (e.g., `.*` is slow)
- Test patterns with `!giveaway regex test` for performance

**Memory (Large Entry Counts):**

- Symptom: Streamer.bot memory usage grows unbounded
- Solution: Close/archive old giveaways regularly
- Estimate: ~100 bytes per entry → 10,000 entries ≈ 1 MB (negligible)

### Smart Sync Optimization (New in v1.3.0)

The bot now uses an intelligent "Diff & Sync" system (`SetGlobalVarIfChanged`). It only sends updates to Streamer.bot
when variables _actually change_.

- **Impact**: Reduces IPC log spam by 99% during idle periods.
- **Benefit**: You can leave `ExposeVariables: true` enabled even on lower-end systems with minimal performance penalty.

### Optimization Checklist

- [ ] Use `Mirror` RunMode (best for stability + performance)
- [ ] Set LogLevel to `INFO` or higher (avoid `TRACE`/`DEBUG` in production)
- [ ] Set LogLevel to `INFO` or higher. NOTE: `TRACE` level will output high-frequency variable sync logs (spammy).
- [ ] Disable `DumpEntriesOnEntry` for streams >500 viewers
- [ ] Increase `StateSyncIntervalSeconds` if on slow HDD
- [ ] Enable `EnableEntropyCheck` if seeing bot/fake accounts
- [ ] Use Wheel API **Animate** mode (lower overhead than interactive)

---

## Remote Control & Automation

As of v1.3.3, you can control the bot **programmatically** by setting specific Global Variables in Streamer.bot.
This allows you to start/end giveaways from your own Actions, Stream Deck, or other integration logic without
simulating chat commands.

### How to use Integration Control

To trigger an action, use the **Core -> Global Variables -> Set Global Variable** sub-action in Streamer.bot.

| Goal               | Variable Name                         | Set Value to... | Effect                                                  |
| :----------------- | :------------------------------------ | :-------------- | :------------------------------------------------------ |
| **Start Giveaway** | `GiveawayBot_<Profile>_IsActive`      | `True`          | Opens the giveaway (if closed). Triggers `HandleStart`. |
| **End Giveaway**   | `GiveawayBot_<Profile>_IsActive`      | `False`         | Closes the giveaway (if open). Triggers `HandleEnd`.    |
| **Change Timer**   | `GiveawayBot_<Profile>_TimerDuration` | `"5m"`          | Updates auto-close timer and announces new time.        |

> [!IMPORTANT]
> **System Override**: Variable-based triggers bypass the usual "Moderator" permission check,
> allowing automated systems to control the bot.

---

## File Structure Reference

```text
C:\Users\<You>\Streamer.bot\data\Giveaway Helper\
│
├── config\
│   └── giveaway_config.json         # Main configuration
│       ├── Globals (Bot-wide settings)
│       └── Profiles
│           ├── Main
│           ├── Weekly
│           └── ...
│
├── dumps\                            # Entry/Winner exports
│   ├── Main\
│   │   ├── 20260128_Entries.txt              # Final snapshot (!end)
│   │   ├── 20260128_Entries_Incremental.txt # Real-time (if enabled)
│   │   └── 20260128_Winners.txt               # Winner log
│   └── Weekly\
│       └── ...
│
├── logs\                             # Debug & error logs
│   └── General\
│       ├── 2026-01-28.log            # Daily rotation
│       ├── 2026-01-27.log
│       └── ...                       # Auto-pruned after LogRetentionDays
│
└── state\                            # Active giveaway data
    ├── Main.json                     # Real-time entry list
    └── Weekly.json
```

**File Formats:**

**`giveaway_config.json`**: Standard JSON, editable in any text editor
**`state/Main.json`**: JSON with UTF-8 encoding, contains active entries
**`dumps/*.txt`**: Plain text, one entry per line:

```text
[14:23:45] UserName (123456) - Tickets: 3
```

**`logs/*.log`**: Plaintext with ISO 8601 timestamps:

```text
[2026-01-28 14:23:45] [INFO] [GiveawayManager] Giveaway started
```

---

## Variable Reference

### Profile-Specific Variables

(Exposed when `ExposeVariables: true` in profile)

| Variable Name                               | Type     | Update Frequency           | Example               |
| ------------------------------------------- | -------- | -------------------------- | --------------------- |
| `GiveawayBot_<Profile>_IsActive`            | Boolean  | Immediate (on start/end)   | `true`                |
| `GiveawayBot_<Profile>_EntryCount`          | Integer  | Per entry (+1)             | `47`                  |
| `GiveawayBot_<Profile>_TicketCount`         | Integer  | Per entry (incl. sub luck) | `68`                  |
| `GiveawayBot_<Profile>_WinnerName`          | String   | On draw                    | `CoolViewer123`       |
| `GiveawayBot_<Profile>_WinnerUserId`        | String   | On draw                    | `987654321`           |
| `GiveawayBot_<Profile>_LastEntry`           | String   | Per entry                  | `NewUser456`          |
| `GiveawayBot_<Profile>_DrawTime`            | DateTime | On draw                    | `2026-01-28 14:30:00` |
| `GiveawayBot_<Profile>_WinnerCount`         | Integer  | Per Draw                   | `3`                   |
| `GiveawayBot_<Profile>_CumulativeEntries`   | Integer  | Per entry                  | `150`                 |
| `GiveawayBot_<Profile>_SubEntryCount`       | Integer  | Per entry                  | `12`                  |
| `GiveawayBot_<Profile>_TimerDuration`       | String   | On config change           | `10m`                 |
| `GiveawayBot_<Profile>_Msg_<Key>`           | String   | On config change           | `Winner is {0}!`      |
| `GiveawayBot_<Profile>_MaxEntriesPerMinute` | Integer  | On config change           | `100`                 |
| `GiveawayBot_<Profile>_RequireSubscriber`   | Boolean  | On config change           | `true`                |
| `GiveawayBot_<Profile>_SubLuckMultiplier`   | Decimal  | On config change           | `1.5`                 |

### Global Metrics

(Always available)

| Variable Name                                  | Type    | Description                                                  |
| ---------------------------------------------- | ------- | ------------------------------------------------------------ |
| `GiveawayBot_Metrics_Entries_Total`            | Integer | Lifetime entries accepted.                                   |
| `GiveawayBot_Metrics_Entries_Rejected`         | Integer | Spam/bots/regex-mismatches blocked.                          |
| `GiveawayBot_Metrics_Winners_Total`            | Integer | Total winners drawn.                                         |
| `GiveawayBot_Metrics_ApiErrors`                | Integer | Wheel API failures.                                          |
| `GiveawayBot_Metrics_SystemErrors`             | Integer | Internal exceptions rooted in bot logic.                     |
| `GiveawayBot_Metrics_LoopDetected`             | Integer | Anti-loop protection triggers fired.                         |
| `GiveawayBot_Metrics_WheelApiCalls`            | Integer | Total calls to Wheel of Names API.                           |
| `GiveawayBot_Metrics_WheelApiTotalMs`          | Integer | Total time spent waiting for Wheel API (latency).            |
| `GiveawayBot_Metrics_Validation_RegexTimeouts` | Integer | Count of Regex operations aborted (ReDoS protection).        |
| `GiveawayBot_Globals_WheelApiKeyStatus`        | String  | "Configured (Direct)", "Configured (Indirect)", or "Missing" |

### Configuration Variables

(Editable in Streamer.bot UI)

| Variable Name                     | Default   | Description                                            |
| --------------------------------- | --------- | ------------------------------------------------------ |
| `GiveawayBot_RunMode`             | `Mirror`  | Config sync mode                                       |
| `GiveawayBot_LogLevel`            | `INFO`    | Minimum log severity                                   |
| `GiveawayBot_LogMaxFileSizeMB`    | `10`      | Max size of a single log file                          |
| `GiveawayBot_LogSizeCapMB`        | `100`     | Total log directory size cap                           |
| `GiveawayBot_Globals_WheelApiKey` | _(empty)_ | Wheel API key (Auto-Encrypts if entered as plain text) |

**OBS Usage Example:**

```text
Text Source: "Entries: %GiveawayBot_Main_EntryCount% | Winner: %GiveawayBot_Main_WinnerName%"
```

---

## Migration Guides

### From Nightbot/Moobot Giveaways

**Old System:**

- Nightbot stores list of users
- Manual `!winner` command picks random from list

**Migration Steps:**

1. **Export old entries** (if possible) from Nightbot dashboard
2. **Manually add** to `state/Main.json` (or start fresh)
3. **Configure external listener:**

   ```json
   "AllowedExternalBots": ["Nightbot"],
   "ExternalListeners": [
     { "Pattern": "(?i)giveaway.*open", "Action": "Open" },
     { "Pattern": "(?i)giveaway.*close", "Action": "Close" }
   ]
   ```

4. **Test**: Have Nightbot say "GIVEAWAY OPEN" → Bot should auto-start

**Benefits:**

- Wheel integration
- Sub luck
- Anti-bot protection
- OBS variables

### From StreamElements

**Old System:**

- StreamElements Giveaway module
- Web dashboard management

**Migration:**

1. **No direct import** - SE doesn't export entry data
2. **Fresh start** recommended
3. **Replicate settings** manually:
   - SE "Subscriber Luck" → `SubLuckMultiplier`
   - SE "Account Age" → `MinAccountAgeDays`

### From Excel Sheets / Manual Tracking

**Old System:**

- Copy/paste usernames to Excel
- Use =RAND() to pick winner

**Migration:**

1. If you have old entry list, convert to JSON:

   ```json
   [
     {
       "UserId": "123456",
       "UserName": "OldWinner",
       "EntryTime": "2026-01-15T10:00:00",
       "TicketCount": 1
     }
   ]
   ```

2. Paste into `state/Main.json` under `"Entries": {}`
3. Run `!giveaway system test` to validate

---

## Custom Integration Examples

### Discord Webhook on Winner

Edit `HandleDrawWinner` method in `GiveawayBot.cs` to add:

```csharp
// After winner selection
var webhookUrl = "https://discord.com/api/webhooks/YOUR_WEBHOOK";
var payload = new {
    content = $"🎉 Giveaway Winner: **{winnerEntry.UserName}**!"
};
var json = JsonConvert.SerializeObject(payload);
using (var client = new System.Net.WebClient())
{
    client.Headers.Add("Content-Type", "application/json");
    client.UploadString(webhookUrl, json);
}
```

**Note:** Requires `System.Net` reference (already included in Streamer.bot).

### OBS Advanced - Custom Overlay

Create HTML file in OBS Browser Source:

```html
<!DOCTYPE html>
<html>
  <head>
    <style>
      body {
        font-family: Arial;
        background: transparent;
      }
      #count {
        font-size: 48px;
        color: #ffd700;
      }
    </style>
  </head>
  <body>
    <div id="count">Entries: 0</div>
    <script>
      // Poll Streamer.bot variable (requires OBS WebSocket)
      setInterval(() => {
        // Use Streamer.bot API to fetch variable
        fetch(
          "http://localhost:7474/GetGlobalVar?name=GiveawayBot_Main_EntryCount",
        )
          .then((r) => r.json())
          .then((data) => {
            document.getElementById("count").textContent =
              `Entries: ${data.value}`;
          });
      }, 1000);
    </script>
  </body>
</html>
```

**Requirements:**

- Streamer.bot HTTP Server enabled
- CORS configured
- `ExposeVariables: true`

---

## Backup & Recovery

### Manual Backup (Recommended Monthly)

**What to backup:**

1. **Config**: `config/giveaway_config.json`
2. **Active State**: `state/*.json` (if mid-giveaway)
3. **Historical Data** (optional): `dumps/` folder

**Backup command (PowerShell):**

```powershell
$source = "$env:APPDATA\Streamer.bot\data\Giveaway Helper"
$dest = "C:\Backups\GiveawayBot_$(Get-Date -Format 'yyyyMMdd').zip"
Compress-Archive -Path $source -DestinationPath $dest
```

### Disaster Recovery Scenarios

#### Lost Config File

1. Run `!giveaway config gen` to create default
2. Restore from backup if available
3. Reconfigure settings via `!giveaway profile config` commands

#### Corrupted State File

1. Check logs: `logs/General/YYYY-MM-DD.log` for errors
2. If `state/Main.json` is corrupt, delete it
3. Bot will create fresh state (loses active entries)
4. Restore from backup if critical

#### Streamer.bot Wipe

1. **Config**: Restore `config/` folder
2. **API Keys**: Re-enter `WheelOfNamesApiKey` (plain text)
3. **Global Variables**: Manually recreate in Streamer.bot UI
4. Run `!giveaway system test` to verify

### Automated Backup (Advanced)

Create Windows Task Scheduler job:

**Trigger:** Daily at 3 AM
**Action:** PowerShell script:

```powershell
$source = "$env:APPDATA\Streamer.bot\data\Giveaway Helper\config"
$dest = "C:\Backups\GiveawayConfig_$(Get-Date -Format 'yyyyMMdd').json"
Copy-Item "$source\giveaway_config.json" -Destination $dest

# Prune old backups (>30 days)
Get-ChildItem "C:\Backups\GiveawayConfig_*.json" |
  Where-Object { $_.LastWriteTime -lt (Get-Date).AddDays(-30) } |
  Remove-Item
```

---

**[← Back to USER_GUIDE](USER_GUIDE.md) | [FAQ →](FAQ.md) | [Quick Reference →](README.md)**
