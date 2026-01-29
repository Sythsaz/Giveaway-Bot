# Giveaway Bot

> **Giveaway System for Streamer.bot**
>
> [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
> [![Streamer.bot](https://img.shields.io/badge/Streamer.bot-v0.2.3%2B-blueviolet)](https://streamer.bot)
> [![GitHub release](https://img.shields.io/github/v/release/Sythsaz/Giveaway-Bot.svg)](https://github.com/Sythsaz/Giveaway-Bot/releases)
> [![Build Status](https://github.com/Sythsaz/Giveaway-Bot/actions/workflows/markdown-lint.yml/badge.svg)](https://github.com/Sythsaz/Giveaway-Bot/actions)

![Florals Giveaway Bot Banner](.github/assets/banner.png)

## 📖 Documentation

- **[User Guide](docs/USER_GUIDE.md)**: Installation, commands, and configuration.
- **[Advanced Guide](docs/ADVANCED.md)**: Custom triggers, OBS integration, and power-user features.
- **[FAQ](docs/FAQ.md)**: Troubleshooting and common questions.
- **[Developer Guide](docs/DEVELOPMENT.md)**: Architecture, contributing, and building.

## ✨ Key Features

- **Multi-Profile Support**: Run "Daily", "Weekly", and "Sub-Only" giveaways simultaneously.
- **Enterprise Security**: AES-256-CBC (DPAPI) encryption, **GDPR Data Deletion** (`!giveaway data delete`),
  & anti-loop protection.
- **Smart Validation**: Blocks bots using entropy checks and account age verification.
- **Rich Feedback**: Windows **Toast Notifications**, **Localization** support, and highly visible chat alerts.
- **Automated Config**: Auto-Import global variables (API keys) from JSON on startup.
- **Observability**: Real-time **OBS variables** and automated wheel spins.
- **Wheel of Names Integration**: Seamlessly sync entries to the wheel and trigger spins.

## 🚀 Quick Start

1. **Download** the latest `GiveawayBot.cs` from [Releases](https://github.com/Sythsaz/Giveaway-Bot/releases).
2. **Import** into Streamer.bot:
   - Create a new Action named "Giveaway Bot".
   - Add a "Code > Execute C# Code" sub-action.
   - Paste the contents of `GiveawayBot.cs`.
   - Click "Compile" (Ensure you have references added: `System.Net.Http.dll`, `System.Core.dll`).
3. **Configure**:
   - The bot will generate a config file at `.../Streamer.bot/data/Giveaway Helper/config/giveaway_config.json`.
   - Edit this file or use the [Example Configs](examples/).
4. **Run**:
   - Type `!giveaway system test` in chat to verify installation.

## ⚙️ Core Commands

| Command  | Permission | Description                            |
| :------- | :--------- | :------------------------------------- |
| `!enter` | Everyone   | Enter the active giveaway              |
| `!start` | Mod+       | Open giveaway for entries              |
| `!end`   | Mod+       | Close giveaway                         |
| `!draw`  | Mod+       | Pick a winner (spins wheel if enabled) |

See [User Guide](docs/USER_GUIDE.md) for the full command list.

## 🏗️ Architecture

The bot uses a singleton manager pattern to handle state, configuration, and Streamer.bot interactions.

```mermaid
graph LR
    User[User] -- !enter --> SB[Streamer.bot]
    SB -- Trigger --> Manager[GiveawayManager]
    Manager -- Read/Write --> State[State File]
    Manager -- Sync --> Wheel[Wheel of Names]
    Manager -- Broadcast --> Chat[Twitch/YT/Kick]
```

See [Architecture Docs](docs/ARCHITECTURE.md) for details.

## 🔧 Compatibility

**Runtime Environment**: C# 7.3 / .NET Framework 4.8 (Streamer.bot's current runtime)

This project's C# version is tied to Streamer.bot's embedded scripting environment. If Streamer.bot upgrades to a
newer .NET runtime, this project will adopt modern C# features accordingly. See [CONTRIBUTING.md](CONTRIBUTING.md)
for details on C# 7.3 limitations during development.

## 🤝 Contributing

Contributions are welcome! Please read our [Contributing Guide](CONTRIBUTING.md) and
[Code of Conduct](CODE_OF_CONDUCT.md).

## 🔒 Security

We take security seriously. See our [Security Policy](SECURITY.md) for details on supported versions and reporting vulnerabilities.

---

**Maintained by [Sythsaz](https://github.com/Sythsaz)**
