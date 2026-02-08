# Changelog

All notable changes to the Giveaway Bot will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.5.7] - 2026-02-08

### Changed

- **Release Workflow**: Updated `tools/auto-release.ps1` to create temporary `chore/release-vX.Y.Z` branches and open
  Pull Requests, replacing the previous `release/vX.Y.Z` branch strategy to better align with conventional commits.
- **Documentation**: Updated `CONTRIBUTING.md` to reflect the new PR-based release workflow.
- **Documentation**: Corrected the configuration directory path in `README.md` and `CONTRIBUTING.md` from
  `Giveaway Helper` to `Giveaway Bot`.
- **Release Assets**: Removed `README.md` from release artifacts in favor of linking to the
  [Wiki](https://github.com/Sythsaz/Giveaway-Bot/wiki) in the release notes.
- **Tests**: Fixed incorrect directory path in `IntegrationTests.cs` to match the actual `Giveaway Bot` folder.

## [1.5.6] - 2026-02-08

### Fixed

- **Linting**: Resolved `markdownlint` errors in `CHANGELOG.md` (duplicate headings, trailing newlines).
- **Config**: Updated `.markdownlint-cli2.jsonc` to strict JSON format and added to `.prettierignore`
  to prevent formatter conflicts.
- **Workflow**: Changed release workflow to utilize Pull Requests (`gh pr create`) and automate tagging on merge.

## [1.5.5] - 2026-02-08

### Added

- **Automation**: Added `tools/auto-release.ps1` for one-click releases.
- **CI**: Added `workflow_dispatch` trigger to `release.yml` for manual release execution.

## [1.5.4] - 2026-02-07

### Added

- **Refactoring**: Added `#region` directives throughout `GiveawayBot.cs` for improved code navigation
  and organization.
- **Code Quality**: Enforced strict adherence to coding rules, including thread safety, time handling,
  and magic string elimination.
- **Logging**: Standardized logging messages to use `[Class] [Method]` prefix for better traceability.
- **Refactoring**: Replaced remaining magic strings with `GiveawayConstants`.

### Security

- _No security changes in this release cycle._

### Infrastructure

- Implementing Version Unification (PR #43)

## [1.5.3] - 2026-02-06

### Added (v1.5.3)

- **Separate GameName Dumps**: New `DumpSeparateGameNames` configuration option creates additional
  `*_GameNames.txt` and `*_Winners_GameNames.txt` files containing only parsed game names/codes
  (e.g., `PlayerName.1234`) without Discord/Twitch usernames. This makes it easier to import winner
  lists into game APIs or reward distribution tools that require in-game identifiers.
  - Applies to both entry dumps and winner dumps
  - Falls back to username if no GameName is parsed
  - Controlled per-profile via `DumpSeparateGameNames` setting
- **Bidirectional Config Sync (Mirror Mode)**: Profile configuration variables can now be modified directly in
  Streamer.bot's global variables and automatically sync back to the config file. Previously, Mirror mode only
  supported Config → GlobalVar sync, requiring manual config file edits for changes.
  - All editable profile settings supported: dump options, entry requirements, luck multipliers, wheel/OBS settings,
    validation rules, timer duration
  - Changes detected during lifecycle ticks (every 30 seconds) or triggered actions
  - Comprehensive logging of detected changes at INFO level (`[Mirror] Detected external change: ...`)
  - Automatic config file save when changes detected

### Fixed (v1.5.3)

- **Test Suite**: Added `SeparateGameNameDumpsTests` to verify GameName dump file creation and
  content validation

### Infrastructure (v1.5.3)

- **Version Unification**: Introduced `VERSION` file as the single source of truth for the project version.
- **Automation**: Added `tools/update-version.ps1` to automate version bumps across code, project files, and docs.
- **Safety**: Added `tools/install-hooks.ps1` to install a git pre-commit hook that prevents version mismatches.
- **CI**: Added `version-consistency.yml` workflow to enforce version alignment in Pull Requests.

## [1.5.1] - 2026-02-06

### Added (v1.5.1)

- **Discord Integration**: Automatically announcer winners to a Discord channel.
- **Dual Mode Support**: Supports both **Streamer.bot Native Integration** (Channel ID) and standard **Webhooks**.
- **Configurable Messages**: Customize the announcement message with `{winner}` placeholder via `DiscordMessage` setting.

## [1.5.0] - 2026-02-05

### Changed (v1.5.0)

- **Code Quality**: Completely refactored `SyncProfileVariables` to use centralized `GiveawayConstants`
  for all 30+ profile variable keys.
  - Eliminated ~20 hardcoded "magic strings" (e.g., "Enable Wheel", "Dump Format").
  - Ensures strict consistency between the internal variable generation and the consumption logic.
- **Documentation**: Synchronized Wiki and Examples to match v1.5.0 code, specifically correcting
  API Key variable names and adding dynamic timer examples.
- **Config Instructions**: Fixed internal help text in `GiveawayBot.cs` to correctly reference `Giveaway Global WheelApiKey`.
- **Documentation**: Comprehensive XML documentation audit for `GiveawayBot.cs` covering all public methods
  and properties.
- **Code Quality**: Enhanced `ParseBoolVariant` to return `null` for invalid inputs, improving config parsing robustness.
- **DEVELOPMENT.md**: Enhanced with three-layer C# 7.3 enforcement documentation (build-time, IDE, pre-commit)
- **CONTRIBUTING.md**: Expanded C# 7.3 constraints section with pre-commit hook usage and common pitfalls
- **ROADMAP.md**: Added Technical Constraints section clarifying C# 7.3 is tied to Streamer.bot runtime
- **README.md**: Added Compatibility section explaining C# 7.3 runtime requirement

### Fixed (v1.5.0)

- **Timer Crash**: Resolved a critical issue where an empty or invalid "Timer Duration" global variable would
  cause the bot to crash during configuration updates. Invalid values now safely disable the timer and log a warning.
- **Metrics Bug**: Removed duplicate `IncUserMetric` call in `HandleEntry` that was double-counting user entry statistics
- **Config Compatibility**: Restored `GlobalRunMode` constant to `"Giveaway Global RunMode"` (removed space)
  to maintain backward compatibility with existing user configurations
- **Variable Management**: Fixed `IsManagedVariable` pattern to only match profile-specific variables
  (`"Giveaway {Profile} ..."`) instead of all variables starting with `"Giveaway "`
- **Wiki Generator**: Updated `GenerateWiki.cs` to resolve C# 5 compilation errors (removed `out var`),
  robust parsing for `Triggers.Add` (insensitive to whitespace),
  and enforced alphabetical sorting for stable documentation output.

### Infrastructure (v1.5.0)

- **Build Enforcement**: Set `LangVersion=7.3` in StreamerBot.csproj to prevent C# 8.0+ compilation
- **Nullable Types**: Disabled nullable reference types (`Nullable=disable`) for C# 7.3 compatibility
- **Implicit Usings**: Disabled global using directives (`ImplicitUsings=disable`) - C# 10.0 feature
- **EditorConfig**: Added IDE0074 suppression for null-coalescing assignment operator suggestions
- **Test Suite**: Removed all C# 8.0+ syntax (nullable annotations, pragmas) from test files

## [1.4.2] - 2026-01-31

### Fixed (v1.4.2)

- **Global Settings Sync**: `CheckForConfigUpdates` now correctly synchronizes Global variables
  (`RunMode`, `LogLevel`, `FallbackPlatform`, `EnableSecurityToasts`) when changed via Streamer.bot,
  ensuring bi-directional config management.
- **Test Suite**: Fixed `ProfileSecurityTests` to support the new AM/PM timestamp format in logs,
  resolving false failures.

## [1.4.0] - 2026-01-31

### Added (v1.4.0)

- **Auto-Update Notification System**: New `!giveaway update` command to check for updates from GitHub.
- **Update Service**: Automatically checks `RELEASE_NOTES.md` on startup and alerts via Toast/Chat if a new version is available.
- **Privacy-Focused**: Downloads updates to a local `updates/` folder and notifies via Toast to avoid exposing file paths
  in streaming chat.
- **Security Alerts**: Added `EnableSecurityToasts` (default: true) to trigger Windows notifications for
  unauthorized command attempts, spam detection (rate limits), and API key failures.

### Fixed (v1.4.0)

- **Toast Notifications**: Fixed `CPHAdapter.ShowToastNotification` signature to correctly invoke Streamer.bot method
  (was causing crashes).
- **Code Quality**: Addressed nullability warnings in `UpdateService` and refactored it for better encapsulation.

## [1.3.3] - 2026-01-30

### Added (v1.3.3)

- **Remote Control**: implemented `CheckForConfigUpdates` monitoring for `GiveawayBot_<Profile>_IsActive`.
  Setting this to `true` (if inactive) starts the giveaway; setting to `false` (if active) ends it.
- **System Override**: Updated `HandleEnd` to allow system-triggered endings (bypassing permissions checks).

## [1.3.2] - 2026-01-30

### Added (v1.3.2)

- **Dynamic Configuration**: `MaxEntriesPerMinute`, `RequireSubscriber`, and `SubLuckMultiplier` can now be updated
  on-the-fly via Streamer.bot global variables (`GiveawayBot_<Profile>_<Key>`).
- **Global Override Protection**: Configuration overrides (like `GiveawayBot_ExposeVariables`) are now explicitly
  protected from being pruned by the variable cleanup logic.

### Fixed (v1.3.2)

- **Log Suppression**: Refactored trace logging to respect the standard `LogLevel` setting (specifically "TRACE")
  instead of requiring a custom flag, eliminating "ghost update" spam by default while preserving debug capabilities.

- **Variable Sync Bug**: Resolved an issue where variables were incorrectly exposed in `Mirror` mode even when
  `ExposeVariables` was disabled, ensuring strict adherence to configuration.
- **Test Isolation**: Improved `ConfigSyncTests` to prevent cross-contamination between test runs by enforcing
  cleaner state resets.

## [1.3.1] - 2026-01-30

### Changed (v1.3.1)

- **Performance**: Optimized `ConfigLoader.GetConfig` to reduce CPH calls by 50% during config checks in `Mirror` mode.
  It now reuses the initial global variable fetch instead of re-reading it during the reload phase.

## [1.3.0] - 2026-01-30

### Added (v1.3.0)

- **Timed Giveaways**: `TimerDuration` configuration (e.g., "10m", "30s") allows giveaways to automatically close.
- **Configurable Messages**: `Messages` dictionary in profile config allows full customization of bot responses.
- **Granular Sync**: New two-way sync for individual message variables (`GiveawayBot_<Profile>_Msg_<Key>`) for
  easier editing in Streamer.bot.
- **Heartbeat Logging**: Added a 5-minute heartbeat log to `LifecycleTick` to confirm timer health without spam.
- **Message Placeholders**: Support for dynamic placeholders like `{0}` (Winner Name/Count) and `{1}` (Target Name)
  in custom messages.
- **Localization Keys**: Publicly exposed `Loc.Keys` to facilitate dynamic variable discovery.
- **Performance Optimization**: Implemented "Smart Sync" (`SetGlobalVarIfChanged`) to eliminate redundant global variable
  updates, drastically reducing log spam and IPC overhead.

## [1.2.0] - 2026-01-29

### Added (v1.2.0)

- **Cross-Profile Analytics**: `!giveaway stats global` command to view aggregate stats (entries, winners, unique users)
  across all profiles.
- **Advanced GDPR Cleanup**: Enhanced `!giveaway data delete` to scrub global metrics and user variables in addition
  to active entries.
- **Unified Event Bus**: Refactored internal architecture to use `GiveawayEventBus` for better decoupling and
  maintainability.

## [1.1.0] - 2026-01-29

### Added (v1.1.0)

- **GDPR Compliance**: New `!giveaway data delete <user>` command to scrub user data from memory and logs.
- **Auto-Import**: Config option `ImportGlobals` to automatically set Streamer.bot global variables (e.g., API keys).
- **Localization**: `CustomStrings` dictionary in `giveaway_config.json` allows overriding bot response messages.
- **Dispose Pattern**: Implemented `IDisposable` in `GiveawayManager` for robust resource cleanup.

### Changed (v1.1.0)

- **Optimization**: `CheckForConfigUpdates` now caches trigger JSON to reduce CPU usage during high load.
- **Security**: Unauthorized management commands are now silently ignored to prevent chat spam.
- **Logging**: Config sync errors are now logged as Warnings for better visibility.

## [1.0.1] - 2026-01-29

### Added (v1.0.1)

- Version constant (`GiveawayBot.Version`) for easy version tracking
- Bug report issue template with detailed diagnostic sections
- Enhanced documentation with new commands and configuration details

### Changed (v1.0.1)

- Updated USER_GUIDE.md with additional command examples
- Improved configuration help text and examples
- Enhanced README with clearer installation steps

### Fixed (v1.0.1)

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
