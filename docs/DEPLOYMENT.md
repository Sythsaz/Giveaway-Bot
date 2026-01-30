# Deployment Guide

Complete step-by-step guide to deploy the Florals Giveaway Bot from zero to your first giveaway.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Quick Start 5 Minutes](#quick-start-5-minutes)
- [Detailed Setup](#detailed-setup)
- [Troubleshooting](#troubleshooting)
- [Next Steps](#next-steps)

---

## Prerequisites

### Required

- **Operating System**: Windows 10 or 11
- **Streamer.bot**: v0.2.3 or later ([Download Here](https://streamer.bot))
- **Streaming Platform**: Connected account (Twitch, YouTube, or Kick)

### Optional

- **OBS Studio**: For on-stream overlays
- **Wheel of Names Account**: For visual wheel spins
- **Stream Deck**: For button-based control

---

## Quick Start (5 Minutes)

Follow these 5 steps to run your first giveaway:

### Step 1: Download GiveawayBot.cs

1. Navigate to the [latest release](https://github.com/Sythsaz/Giveaway-Bot/releases)
2. Download `GiveawayBot.cs`
3. Save to a known location (e.g., Downloads folder)

### Step 2: Import into Streamer.bot

1. Open **Streamer.bot**
2. Go to the **Actions** tab
3. Right-click in the actions list → **Import**
4. Select the `GiveawayBot.cs` file you downloaded
5. Click **Open**

> **✅ Success**: You should see "Florals Giveaway Bot" in your actions list

### Step 3: Configure Basic Triggers

The bot needs to know when to respond to commands. Set up these 4 essential triggers:

#### Trigger 1: !enter

1. In Streamer.bot, go to the **Triggers** tab
1. Click **Add** → **Chat Command**
1. Set command to: `!enter`
1. Link to action: `Florals Giveaway Bot - Enter`
1. Check **Enabled**

#### Trigger 2: !start

1. Click **Add** → **Chat Command**
2. Set command to: `!start`
3. Link to action: `Florals Giveaway Bot - Open`
4. Check **Enabled**

#### Trigger 3: !draw

1. Click **Add** → **Chat Command**
2. Set command to: `!draw`
3. Link to action: `Florals Giveaway Bot - Winner`
4. Check **Enabled**

#### Trigger 4: !end

1. Click **Add** → **Chat Command**
2. Set command to: `!end`
3. Link to action: `Florals Giveaway Bot - Close`
4. Check **Enabled**

### Step 4: Run System Test

Verify everything is working:

1. In your streaming platform chat, type:

```bash
!giveaway system test
```

2. **Expected Response**:

```text
=========================================
FLORALS GIVEAWAY BOT - SYSTEM TEST
Version: 1.0.0
=========================================
✓ Config loaded successfully
✓ 1 profile found: Main
✓ File system access OK
✓ State files accessible
✓ Log files writable
✓ All checks passed!
=========================================
```

> **⚠️ If errors appear**: See [Troubleshooting](#troubleshooting) section below

### Step 5: Run Your First Giveaway

```bash
!start
```

**Bot Response**: "🎉 Giveaway 'Main' is now OPEN! Type !enter to join!"

```bash
!enter
```

**Bot Response**: "✅ {YourName}, you're entered! (1 ticket)"

```bash
!draw
```

**Bot Response**: "🎊 WINNER: {YourName}! Congratulations!"

```bash
!end
```

**Bot Response**: "❌ Giveaway 'Main' is now CLOSED."

🎉 **Congratulations!** You've successfully run your first giveaway!

---

## Detailed Setup

### Installing Streamer.bot (For Complete Beginners)

#### Download and Install

1. Visit [https://streamer.bot](https://streamer.bot)
2. Click **Download**
3. Run the installer
4. Follow the installation wizard

#### Connect Your Streaming Platform

**For Twitch**:

1. Open Streamer.bot
2. Go to **Platforms** → **Twitch**
3. Click **Sign In**
4. Authorize in your browser
5. ✅ Connection indicator should turn green

**For YouTube**:

1. Go to **Platforms** → **YouTube**
2. Click **Sign In**
3. Authorize with your Google account
4. ✅ Connection indicator should turn green

**For Kick**:

1. Go to **Platforms** → **Kick**
2. Enter your Kick username
3. Click **Connect**

### Creating the Action (Detailed)

#### Import Method

1. Open **Streamer.bot**
2. Click the **Actions** tab (left sidebar)
3. Right-click in the empty actions area
4. Select **Import** from context menu
5. Navigate to the `GiveawayBot.cs` file
6. Click **Open**
7. **Confirmation**: "Action imported successfully"

#### What Gets Imported

The import creates **one action** called "Florals Giveaway Bot" with multiple sub-actions:

- Enter (accept entries)
- Winner (draw winner)
- Open (start giveaway)
- Close (end giveaway)
- Giveaway (management commands)

### Setting Up Triggers (Detailed)

Triggers tell Streamer.bot when to run the giveaway bot. Here's how to set up each type:

#### Chat Command Triggers (Recommended)

**!enter Trigger**:

1. **Triggers** tab → **Add**
2. Select **Commands** → **Chat Message**
3. **Command**: `!enter`
4. **From**: `Anywhere` (allows viewers to enter)
5. **Action**: Select "Florals Giveaway Bot"
6. **Sub-Action**: Select "Enter"
7. Click **OK**

**!giveaway Trigger** (Management):

1. **Add** → **Commands** → **Chat Message**
2. **Command**: `!giveaway`
3. **From**: `Chat` (allows mod/broadcaster management)
4. **Action**: "Florals Giveaway Bot"
5. **Sub-Action**: "Giveaway"
6. Click **OK**

Repeat for `!start`, `!end`, `!draw` using the appropriate sub-actions.

#### Optional: Stream Deck Integration

If you have a Stream Deck:

1. **Triggers** tab → **Add**
2. Select **Stream Deck** → **Button Press**
3. **Button**: Press the physical button you want to use
4. **Action**: "Florals Giveaway Bot"
5. **Sub-Action**: Select desired action (e.g., "Winner")
6. **Label**: Name it (e.g., "Draw Winner")

#### Optional: Hotkey Triggers

For keyboard shortcuts:

1. **Triggers** tab → **Add**
2. Select **Hotkey**
3. **Key**: Press your desired key combo (e.g., F5)
4. **Action**: "Florals Giveaway Bot"
5. **Sub-Action**: Select desired action
6. Click **OK**

### Configuration

The bot uses a JSON configuration file: `giveaway_config.json`

#### Generate Default Config

```bash
!giveaway config gen
```

This creates a config file with sensible defaults and inline documentation.

#### Minimal Configuration

If you want to start simple, create `giveaway_config.json` manually:

```json
{
  "Profiles": {
    "Main": {
      "Triggers": {
        "command:!enter": "Enter",
        "command:!draw": "Winner",
        "command:!start": "Open",
        "command:!end": "Close"
      },
      "MaxEntriesPerMinute": 100,
      "SubLuckMultiplier": 1,
      "ExposeVariables": false
    }
  },
  "Globals": {
    "RunMode": "Mirror",
    "LogLevel": "INFO"
  }
}
```

**File Location**: Place in the same directory as Streamer.bot's executable.

#### Recommended Production Config

```json
{
  "Profiles": {
    "Main": {
      "Triggers": {
        "command:!enter": "Enter",
        "command:!join": "Enter",
        "command:!draw": "Winner",
        "command:!start": "Open",
        "command:!end": "Close"
      },
      "MaxEntriesPerMinute": 60,
      "SubLuckMultiplier": 2,
      "MinAccountAgeDays": 7,
      "EnableEntropyCheck": true,
      "ExposeVariables": true,
      "DumpEntriesOnEnd": true,
      "ToastNotifications": {
        "WinnerSelected": true,
        "GiveawayOpened": true,
        "GiveawayClosed": true
      }
    }
  },
  "Globals": {
    "RunMode": "Mirror",
    "StatePersistenceMode": "Both",
    "LogLevel": "INFO",
    "ImportGlobals": true
  }
}
```

**Key Differences**:

- **Anti-bot measures** enabled (account age, entropy check)
- **Multiple entry commands** (!enter and !join)
- **Sub luck** gives subscribers 2x tickets
- **Variable exposure** for OBS integration
- **Toast notifications** for important events

### OBS Integration

Display giveaway info on your stream using Streamer.bot global variables.

#### Setting Up Entry Counter

1. In OBS, add a **Text (GDI+)** source
2. Name it: "Giveaway Entry Count"
3. In the **Text** field, enter:

```text
Entries: %GiveawayBot_Main_EntryCount%
```

4. Style the text (font, color, size)
5. Position on your scene
6. ✅ The count will update automatically

#### Setting Up Giveaway Status

1. Add another **Text (GDI+)** source
2. Name it: "Giveaway Status"
3. In the **Text** field, enter:

```text
Giveaway: %GiveawayBot_Main_IsActive%
```

4. _(Optional)_ Use **Color Alteration** filter:
   - Green when "true"
   - Red when "false"

#### Dynamic Scene Visibility

Show/hide a scene element based on giveaway status:

1. Create a **Group** or **Scene** for giveaway elements
2. Right-click → **Filters**
3. Add **Show/Hide** filter
4. **Variable**: `GiveawayBot_Main_IsActive`
5. **Condition**: Show when `true`, Hide when `false`

> **⚠️ Important**: Set `ExposeVariables: true` in your profile config for OBS integration to work

### Wheel of Names Integration

Add visual wheel spins for winner selection.

#### Getting an API Key

1. Visit [https://wheelofnames.com](https://wheelofnames.com)
2. Create an account (free)
3. Go to **Settings** → **API**
4. Copy your **API Key**

#### Adding to Config

```json
{
  "Profiles": {
    "Main": {
      "EnableWheel": true,
      "WheelSettings": {
        "Title": "🎉 GIVEAWAY WINNER 🎉",
        "SpinTime": 10,
        "DisplayWinnerDuration": 5
      }
    }
  },
  "Globals": {
    "WheelOfNamesApiKey": "paste-your-api-key-here"
  }
}
```

#### Testing Wheel Integration

1. Start a giveaway: `!start`
2. Add some entries: `!enter`
3. Draw a winner: `!draw`
4. ✅ Browser should open with spinning wheel

> **🔐 Security Note**: On first launch, the bot will automatically encrypt your API key for security

---

## Troubleshooting

### "Action not found" Error

**Symptom**: Bot doesn't respond to any commands

**Solution**:

1. Check that "Florals Giveaway Bot" appears in Actions list
2. If missing, re-import `GiveawayBot.cs`
3. Verify triggers are linked to the correct action

### "Trigger not configured" Warning

**Symptom**: Bot responds with "No trigger configured for..."

**Solution**:

1. Run `!giveaway config check` to see trigger status
2. Verify `giveaway_config.json` has the `Triggers` section
3. Ensure trigger format is correct: `"command:!enter": "Enter"`

**Example Fix**:

```json
{
  "Profiles": {
    "Main": {
      "Triggers": {
        "command:!enter": "Enter"
      }
    }
  }
}
```

### Entries Not Saved After Restart

**Symptom**: Entry count resets to 0 when Streamer.bot restarts

**Solution**: Change `RunMode` to `Mirror` for persistent state

```json
{
  "Globals": {
    "RunMode": "Mirror",
    "StatePersistenceMode": "Both"
  }
}
```

**Explanation of Modes**:

- **FileSystem**: Uses local JSON file only (lost on failure)
- **GlobalVar**: Uses Streamer.bot memory only (lost on restart)
- **Mirror**: Syncs both file and memory (recommended)

### Chat Messages Not Detected

**Symptom**: Bot doesn't respond to chat commands

**Solution**:

1. Check platform connection (green indicator in Streamer.bot)
1. Verify Streamer.bot has permission to read chat
1. Test with a simple command like `!test` in another action
1. Check if bot is rate-limited by the platform

### "Access Denied" or File Errors

**Symptom**: Errors about file/folder permissions

**Solution**:

1. Run Streamer.bot as **Administrator** (right-click → Run as administrator)
1. Check `giveaway_config.json` file permissions
1. Ensure antivirus isn't blocking file writes

### Configuration Errors

**Symptom**: "Config validation failed" errors

**Solution**:

1. Run `!giveaway config check` for detailed error messages
2. Validate your JSON at [jsonlint.com](https://jsonlint.com)
3. Check for common mistakes:
   - Missing commas
   - Unmatched quotes
   - Incorrect property names (case-sensitive!)

### Wheel Doesn't Open

**Symptom**: `!draw` picks winner but wheel doesn't appear

**Solution**:

1. Verify `EnableWheel: true` in profile config
2. Check API key is set in `Globals.WheelOfNamesApiKey`
3. Test API connectivity: `!giveaway system test`
4. Ensure browser isn't blocking

pop-ups

---

## Next Steps

After completing your first giveaway, explore these features:

### Customize Your Setup

- **Configuration**: Edit `giveaway_config.json` to customize entry limits, sub luck, and messages
  - See [USER_GUIDE.md](USER_GUIDE.md) for all available options
- **Multiple Profiles**: Create separate giveaways for different purposes (Daily, Weekly, Sub-Only)
  - Learn more in [ADVANCED.md](ADVANCED.md#multi-profile-setup)

### Enhance Your Stream

- **OBS Integration**: Display entry counts and giveaway status on stream
  - Setup guide in [ADVANCED.md](ADVANCED.md#obs-integration)
- **Wheel of Names**: Enable visual wheel spins for winner selection
  - Configuration in [ADVANCED.md](ADVANCED.md#wheel-of-names-integration)

### Advanced Features

- **Anti-Bot Protection**: Configure account age, entropy checks, and rate limiting
  - Details in [ADVANCED.md](ADVANCED.md#anti-bot-measures)
- **Custom Triggers**: Set up StreamDeck buttons, hotkeys, or regex patterns
  - Examples in [ADVANCED.md](ADVANCED.md#custom-triggers)
- **Toast Notifications**: Get desktop alerts for giveaway events
  - Configuration in [USER_GUIDE.md](USER_GUIDE.md#toast-notifications)

### Resources

- **API Reference**: Technical documentation for developers ([API_REFERENCE.md](API_REFERENCE.md))
- **FAQ**: Common questions and troubleshooting ([FAQ.md](FAQ.md))
- **GitHub Issues**: Report bugs or request features at the [repository](https://github.com/Sythsaz/Giveaway-Bot/issues)

---

## Support

If you encounter issues not covered in this guide:

1. Check the [FAQ](FAQ.md) for common questions
2. Review the [User Guide](USER_GUIDE.md) for detailed explanations
3. Open an issue on [GitHub](https://github.com/Sythsaz/Giveaway-Bot/issues) with:
   - Your `!giveaway system test` output
   - Relevant log files (`logs/` folder)
   - Steps to reproduce the issue

Happy streaming! 🎉
