# Florals Giveaway Bot

> **Giveaway system for Streamer.bot**
>
> 📖 **Documentation**: [User Guide](USER_GUIDE.md) | [Advanced Guide](ADVANCED.md) | [FAQ](FAQ.md)
>
> 📥 **Installation**: [How to Install (Step-by-Step)](USER_GUIDE.md#installation--verification)
> _Import `GiveawayBot.cs` into Streamer.bot C# Action_

## ✨ Key Features

- **Multi-Profile Support**: Run "Daily", "Weekly", and "Sub-Only" giveaways simultaneously.
- **Enterprise-Grade Security**: AES-256-CBC encryption for API keys & anti-loop protection.
- **Smart Validation**: Blocks bots using entropy checks and account age verification.
- **Rich Feedback**: Windows **Toast Notifications** and highly visible chat alerts.
- **Observability**: Real-time **OBS variables** and automated wheel spins.

## 🚀 Core Commands

| Command                  | Permission | Description                                |
| :----------------------- | :--------- | :----------------------------------------- |
| `!enter`                 | Everyone   | Enter the "Main" giveaway                  |
| `!giveaway`              | Mod+       | Base command (Alias: `!ga`)                |
| `!start`                 | Mod+       | Open giveaway for entries                  |
| `!end`                   | Mod+       | Close giveaway                             |
| `!draw`                  | Mod+       | Pick a winner                              |
| `!giveaway system test`  | Mod+       | **Run this first!** Full system diagnostic |
| `!giveaway profile list` | Mod+       | Show all active giveaway profiles          |

## ⚙️ Essential Settings

Copy to `config/giveaway_config.json`:

```json
{
  "RunMode": "Mirror", // Best for stability (Syncs File <-> Vars)
  "StatePersistenceMode": "Both", // Backup active entries to Disk & Memory
  "MaxEntriesPerMinute": 60, // Global spam protection
  "SubLuckMultiplier": 2, // Subs get 2x tickets (0 to disable)
  "EnableWheel": false, // Set true to spin Wheel of Names on draw
  "WheelApiKeyVar": "WheelOfNamesApiKey", // Variable holding your API key
  "ToastNotifications": {
    // Windows Desktop Alerts
    "WinnerSelected": true,
    "GiveawayOpened": true,
    "GiveawayClosed": true
  }
}
```

## 📊 OBS Variables

If `ExposeVariables: true` in your profile config:

- `%GiveawayBot_Main_IsActive%`
- `%GiveawayBot_Main_EntryCount%`
- `%GiveawayBot_Main_WinnerName%`

## 🔍 Troubleshooting Fast Track

| Problem            | Solution                                                   |
| :----------------- | :--------------------------------------------------------- |
| **First Run?**     | Run `!giveaway system test` to verify install              |
| **No entries?**    | Check `AllowedExternalBots` if using Nightbot triggers     |
| **Config errors?** | Run `!giveaway config check` to find typos                 |
| **Wheel fail?**    | Set `WheelOfNamesApiKey` variable (Text is auto-encrypted) |

## 📁 File Locations

- Config: `.../Streamer.bot/data/Giveaway Helper/config/giveaway_config.json`
- Logs: `.../Streamer.bot/data/Giveaway Helper/logs/General/`
- Dumps: `.../Streamer.bot/data/Giveaway Helper/dumps/Main/`

## 🛠 Development Setup

This project is built for **Streamer.bot v0.2.3+** and targets **.NET Framework 4.8**.

### Dependencies

The project references Streamer.bot DLLs which must be present on your machine.

1. **Locate Streamer.bot**: Ensure you have Streamer.bot installed.
2. **Configure Path**: The project defaults to looking for Streamer.bot at `C:\Streamer.bot`.
   - If your install is elsewhere (e.g., `D:\Streamer Bot`), create a file named `StreamerBot.csproj.user` in the project root:

     ```xml
     <Project>
       <PropertyGroup>
         <StreamerBotPath>D:\Streamer Bot</StreamerBotPath>
       </PropertyGroup>
     </Project>
     ```

   - This file is ignored by git, so your local path won't affect others.

### Building

Open `StreamerBot.csproj` in your IDE (VS Code, Visual Studio, or Rider) and build.

```bash
dotnet build
```

---

**Version**: 1.0.1 | **C# Compatibility**: 7.3 | **Streamer.bot**: v0.2.3+
