# Configuration Examples

This page provides copy-paste ready examples for `giveaway_config.json`.

> **How to use**: Copy the JSON content into your `.../Giveaway Helper/config/giveaway_config.json` file.

## 1. Minimal Starter

Best for first-time users. Just the basics.

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
      "MaxEntriesPerMinute": 100
    }
  },
  "Globals": {
    "RunMode": "Mirror",
    "LogLevel": "INFO"
  }
}
```

## 2. Multi-Profile Setup

Run a "Daily" giveaway and a "Weekly" long-running raffle simultaneously.

```json
{
  "Profiles": {
    "Main": {
      "Triggers": { "command:!enter": "Enter" },
      "MaxEntriesPerMinute": 100
    },
    "Weekly": {
      "Triggers": { "command:!raffle": "Enter" },
      "DumpEntriesOnEnd": true,
      "StatePersistenceMode": "Both"
    },
    "SubOnly": {
      "Triggers": { "command:!subluck": "Enter" },
      "RequireSubscriber": true,
      "SubLuckMultiplier": 0
    }
  }
}
```

## 3. Power User (Anti-Bot)

Strict security settings to block bots and spam.

```json
{
  "Profiles": {
    "Main": {
      "Triggers": { "command:!enter": "Enter" },
      "EnableEntropyCheck": true,
      "MinAccountAgeDays": 30,
      "UsernamePattern": "^[a-zA-Z0-9_]+$",
      "AllowedExternalBots": ["Nightbot"],
      "ExternalListeners": [
        { "Pattern": "(?i)giveaway.*open", "Action": "Open" }
      ]
    }
  }
}
```

## 4. Visuals (Wheel & OBS)

Automatically spin the wheel and update OBS scenes.

```json
{
  "Profiles": {
    "Main": {
      "EnableWheel": true,
      "WheelSettings": {
        "Title": "WINNER DRAW",
        "SpinTime": 15
      },
      "EnableObs": true,
      "ObsScene": "GiveawayScene",
      "ObsSource": "BrowserSource",
      "ToastNotifications": {
        "WinnerSelected": true
      }
    }
  },
  "Globals": {
    "WheelOfNamesApiKey": "PASTE_KEY_HERE"
  }
}
```

## 5. Timed & Custom Messages

"Flash Giveaway" mode with custom hype messages.

```json
{
  "Profiles": {
    "Flash": {
      "TimerDuration": "5m",
      "Messages": {
        "GiveawayOpened": "🚨 FLASH GIVEAWAY! 5 MINS ONLY! GO GO GO!",
        "WinnerSelected": "🏆 THE SPEEDSTER IS: {0}!"
      }
    }
  }
}
```

## 6. Full Reference

Every available option with default values. Useful for checking what's possible.

```json
{
  "Profiles": {
    "Reference": {
      "Triggers": {
        "command:!join": "Enter",
        "command:!pick": "Winner",
        "sd:Keypad-UID": "Open"
      },
      "MaxEntriesPerMinute": 45,
      "MinAccountAgeDays": 0,
      "RequireSubscriber": false,
      "SubLuckMultiplier": 2.0,
      "EnableEntropyCheck": true,
      "EnableWheel": false,
      "EnableObs": false,
      "ExposeVariables": false,
      "DumpEntriesOnEnd": true,
      "TimerDuration": "10m"
    }
  },
  "Globals": {
    "RunMode": "Mirror",
    "StatePersistenceMode": "Both",
    "LogLevel": "INFO",
    "EnabledPlatforms": ["Twitch", "YouTube", "Kick"],
    "LogRetentionDays": 90
  }
}
```
