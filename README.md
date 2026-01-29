# Florals Giveaway Bot

> **Giveaway system for Streamer.bot**
>
> 📖 **Documentation**: [User Guide](USER_GUIDE.md) | [Advanced Guide](ADVANCED.md) | [FAQ](FAQ.md)

## 🚀 Core Commands

| Command  | Permission | Description               |
| -------- | ---------- | ------------------------- |
| `!enter` | Everyone   | Enter the active giveaway |
| `!start` | Mod+       | Open giveaway for entries |
| `!end`   | Mod+       | Close giveaway            |
| `!draw`  | Mod+       | Pick a winner             |

## ⚙️ Essential Settings

```json
{
  "RunMode": "Mirror", // Best for stability
  "StatePersistenceMode": "Both", // Backup everywhere
  "MaxEntriesPerMinute": 60, // Spam protection
  "SubLuckMultiplier": 2, // Subs get 2x tickets
  "EnableWheel": false, // Requires API key
  "ExposeVariables": true // OBS integration
}
```

## 📊 OBS Variables (if `ExposeVariables: true`)

- `%GiveawayBot_Main_IsActive%` - True/False
- `%GiveawayBot_Main_EntryCount%` - Number of entrants
- `%GiveawayBot_Main_WinnerName%` - Last winner

## 🔍 Troubleshooting Fast Track

| Problem             | Solution                                                  |
| ------------------- | --------------------------------------------------------- |
| No entries accepted | Check `!giveaway system test`                             |
| Bot not responding  | Verify triggers are set for `!enter`, `!start`, etc.      |
| Config errors       | Run `!giveaway config check`                              |
| Wheel not loading   | Verify `WheelOfNamesApiKey` is set (plain text first run) |

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

**Version**: 1.0.0 | **C# Compatibility**: 7.3 | **Streamer.bot**: v0.2.3+
