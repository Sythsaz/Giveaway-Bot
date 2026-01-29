# Changelog

All notable changes to the Florals Giveaway Bot will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Community files: CONTRIBUTING.md, CHANGELOG.md, CODE_OF_CONDUCT.md, SECURITY.md
- Pull Request template for standardized contributions
- GitHub Actions workflows for markdown linting and automated releases
- Comprehensive documentation reorganization into `/docs` folder
- Architecture documentation with system diagrams
- Developer guide for contributors
- Example configurations covering multiple use cases
- Visual assets: repository logo, banner, and feature mockups
- GitHub repository topics for improved discoverability

### Changed

- Reorganized documentation from root to `/docs` folder
- Enhanced README with badges, visual assets, and architecture overview

## [1.0.1] - 2026-01-29

### Added (v1.0.1)

- Version constant (`GiveawayBot.Version`) for easy version tracking
- Bug report issue template with detailed diagnostic sections
- Enhanced documentation with new commands and configuration details

### Changed (v1.0.1)

- Updated USER_GUIDE.md with additional command examples
- Improved configuration help text and examples
- Enhanced README with clearer installation steps

### Fixed

- Minor documentation inconsistencies

## [1.0.0] - 2026-01-29

### Added (v1.0.0)

- **Multi-Profile Support**: Run multiple giveaways simultaneously (e.g., Daily, Weekly, Sub-Only)
- **Enterprise Security**: DPAPI encryption for API keys with automatic encryption on first run
- **Smart Validation**: Bot detection via entropy checks, account age verification, username pattern matching
- **Toast Notifications**: Windows desktop alerts for giveaway events (winner selected, opened, closed)
- **Observability**: Real-time Streamer.bot global variables for OBS integration
- **Wheel of Names Integration**: Automated wheel spins with full API v2 support
- **Multi-Platform Broadcasting**: Support for Twitch, YouTube, and Kick
- **Flexible Persistence**: Multiple run modes (FileSystem, GlobalVar, ReadOnlyVar, Mirror)
- **State Management**: Configurable persistence (File, GlobalVar, Both) with automatic sync
- **Advanced Triggers**: Support for commands, Stream Deck buttons, hotkeys, and custom triggers
- **External Bot Support**: Parse entries from Nightbot, Moobot, and other chat bots
- **Profile Management**: Create, configure, import, export, start, end profiles via commands
- **Comprehensive Logging**: File-based logging with retention policies and size caps
- **Sub Luck Multiplier**: Configurable bonus tickets for subscribers
- **Rate Limiting**: Per-profile entry rate limiting to prevent spam
- **Dump Functionality**: Export entries and winners to JSON files
- **System Diagnostics**: Comprehensive `!giveaway system test` command
- **Configuration Validation**: `!giveaway config check` with detailed error messages

### Core Commands

- `!enter` - Enter the active giveaway
- `!giveaway` (alias: `!ga`) - Base command for all giveaway operations
- `!start` / `!end` - Open/close giveaways
- `!draw` - Pick a winner
- `!giveaway system test` - Run full system diagnostic
- `!giveaway profile list` - Show all profiles
- `!giveaway profile config <name>` - View profile configuration
- `!giveaway profile start/end <name>` - Control profile state
- `!giveaway profile import/export <name>` - Backup/restore profiles
- `!giveaway config check` - Validate configuration

### Configuration Options

- **RunMode**: FileSystem, GlobalVar, ReadOnlyVar, Mirror
- **StatePersistenceMode**: File, GlobalVar, Both
- **LogLevel**: TRACE, VERBOSE, DEBUG, INFO, WARN, ERROR, FATAL
- **MaxEntriesPerMinute**: Rate limiting per profile
- **SubLuckMultiplier**: Bonus tickets for subscribers
- **EnableWheel**: Wheel of Names integration toggle
- **ToastNotifications**: Configurable desktop alerts
- **ExposeVariables**: Sync profile state to Streamer.bot globals
- **ExternalListeners**: Parse messages from allowed bots
- **UsernamePattern**: Regex validation for entries
- **MinAccountAgeDays**: Minimum account age requirement
- **EnableEntropyCheck**: Detect low-quality/bot names

### Security Features

- DPAPI encryption for API keys (Windows Data Protection API)
- Automatic encryption of plain-text API keys on first run
- Anti-loop protection to prevent infinite recursion
- Bot detection via Shannon entropy analysis
- Account age verification
- Rate limiting and spam protection

### Documentation

- Comprehensive USER_GUIDE.md with installation and usage
- ADVANCED.md covering complex configurations
- FAQ.md with troubleshooting and common questions
- README.md with quick start guide
- Inline JSON configuration help text

### Technical Details

- **Target Framework**: .NET Framework 4.8
- **C# Compatibility**: C# 7.3 (Streamer.bot environment)
- **Streamer.bot Version**: v0.2.3+
- **License**: MIT
- **Platform Support**: Windows 10/11

---

## Version History Links

- [Unreleased](https://github.com/Sythsaz/Giveaway-Bot/compare/v1.0.1...HEAD)
- [1.0.1](https://github.com/Sythsaz/Giveaway-Bot/compare/v1.0.0...v1.0.1)
- [1.0.0](https://github.com/Sythsaz/Giveaway-Bot/releases/tag/v1.0.0)
