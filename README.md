# Giveaway Bot

> **Giveaway System for Streamer.bot**
>
> 📖 **[READ THE WIKI DOCUMENTATION](https://github.com/Sythsaz/Giveaway-Bot/wiki)**
>
> **Active Development! There may be breaking changes until things get ironed out a bit better**
>
> [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
> [![Streamer.bot](https://img.shields.io/badge/Streamer.bot-v0.2.3%2B-blueviolet)](https://streamer.bot)
> [![GitHub release](https://img.shields.io/github/v/release/Sythsaz/Giveaway-Bot.svg)](https://github.com/Sythsaz/Giveaway-Bot/releases)
>
> [![Markdown Lint](https://github.com/Sythsaz/Giveaway-Bot/actions/workflows/markdown-lint.yml/badge.svg)](https://github.com/Sythsaz/Giveaway-Bot/actions/workflows/markdown-lint.yml)
> [![C# 7.3 .NET Tests](https://github.com/Sythsaz/Giveaway-Bot/actions/workflows/tests.yml/badge.svg)](https://github.com/Sythsaz/Giveaway-Bot/actions/workflows/tests.yml)
>
> ![C# Version](https://img.shields.io/badge/C%23-7.3-blue)
> ![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-purple)

![Giveaway Bot Banner](.github/assets/banner.png)

## 📖 Documentation

All documentation has moved to the **[GitHub Wiki](https://github.com/Sythsaz/Giveaway-Bot/wiki)**.

- **[User Guide](https://github.com/Sythsaz/Giveaway-Bot/wiki/User-Guide)**: Installation, commands, and configuration.
- **[Advanced Guide](https://github.com/Sythsaz/Giveaway-Bot/wiki/Advanced-Configuration)**: Custom triggers, OBS
  integration, and power-user features.
- **[FAQ](https://github.com/Sythsaz/Giveaway-Bot/wiki/FAQ)**: Troubleshooting and common questions.
- **[Developer Guide](https://github.com/Sythsaz/Giveaway-Bot/wiki/Developer-Guide)**: Architecture, contributing, and building.

## ✨ Key Features

- **Multi-Profile Support**: Run "Daily", "Weekly", and "Sub-Only" giveaways simultaneously.
- **Remote Control & Automation**: Control giveaways programmatically via Streamer.bot variables (Stream Deck ready).
- **Timed Giveaways**: Set a duration (e.g., "10m") and the bot automatically closes the giveaway.
- **Configurable Messages**: Customize every chat response to match your stream's personality.
- **Enterprise Security**: AES-256-CBC (DPAPI) **Auto-Encryption** for API Keys, **GDPR Data Deletion**,
  & anti-loop protection.
- **Smart Validation**: Blocks bots using entropy checks and account age verification.
- **Rich Feedback**: Windows **Toast Notifications**, **Localization** support, and highly visible chat alerts.
- **Automated Config**: Auto-Import global variables (API keys) from JSON on startup.
- **Observability**: Real-time **OBS variables** and automated wheel spins.
- **Wheel of Names Integration**: Seamlessly sync entries to the wheel and trigger spins.
- **Discord Integration**: Automatically announce winners to a Discord channel (Native or Webhook).
- **Bidirectional Sync (Mirror Mode)**: Update config settings directly from Streamer.bot Global Variables.
- **Separate Game Name Dumps**: Export just the "Game Name" (e.g., Player.1234) for easy import into other tools.
- **Auto-Update**: Built-in command `!giveaway update` to check for and download the latest version.

## 🚀 Quick Start

1. **Download** the latest `GiveawayBot.cs` from [Releases](https://github.com/Sythsaz/Giveaway-Bot/releases).
2. **Import** into Streamer.bot:
   - Create a new Action named "Giveaway Bot".
   - Add a "Code > Execute C# Code" sub-action.
   - Paste the contents of `GiveawayBot.cs`.
   - Click "Compile" (Ensure you have added the references first).
3. **Configure**:
   - The bot will generate a config file at `.../Streamer.bot/data/Giveaway Bot/config/giveaway_config.json`.
   - Edit this file or use the [Example Configs](examples/).
4. **Run**:
   - Type `!giveaway system test` in chat to verify installation.
   - See the **[Deployment Guide](https://github.com/Sythsaz/Giveaway-Bot/wiki/Deployment-Guide)** for details.

## ⚙️ Compatibility

**Runtime Environment**: C# 7.3 / .NET Framework 4.8 (Streamer.bot's current runtime)

## 📦 Release Assets

Each GitHub release is expected to include these assets:

- `GiveawayBot.cs`
- `CHANGELOG.md`

## 🤝 Contributing

Contributions are welcome! Please read our [Contributing Guide](CONTRIBUTING.md) and
[Code of Conduct](CODE_OF_CONDUCT.md).

## 🔒 Security

We take security seriously. See our [Security Policy](SECURITY.md) for details.

---

**Maintained by [Sythsaz](https://github.com/Sythsaz)**
**[Website Homepage](https://sythsaz.dpdns.org)
