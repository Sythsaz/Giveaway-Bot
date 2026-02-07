# Release Notes v1.5.0

## Security Section Guidance

When a release contains security-relevant updates, include a dedicated `### Security` section under that release heading with:

- impacted component/feature
- severity/impact summary
- mitigation guidance
- advisory link (if published)

If no security changes apply, the section may be omitted.

This release focuses on **Documentation Synchronization**, **Code Quality Refactors**, and **Infrastructure Hardening**.

## [1.5.0] - 2026-02-05

### Changed (v1.5.0)

- **Documentation**: Fully verified and synchronized Wiki/Examples with code.
  - **Critical Fix**: Corrected API Key variable name documentation to `Giveaway Global WheelApiKey`.
  - **New Examples**: Added `TimerDuration` to profile examples.
- **Code Quality**: Refactored `SyncProfileVariables` to use centralized constants, eliminating magic strings.
- **Infrastructure**: Enforced C# 7.3 compatibility via GitHub Actions and pre-commit hooks to match Streamer.bot runtime.

### Fixed (v1.5.0)

- **Timer Crash**: Fixed a crash caused by invalid/empty Timer Duration variables.
- **Config Help**: Corrected internal help text to point to the correct Global Variable name.

## [1.4.3] - 2026-02-03

### Added (v1.4.3)

- **GDPR**: `!giveaway data delete <user>` command for comprehensive data scrubbing (logs, metrics, active entries).
- **Performance**: Smart config synchronization with JSON caching to reduce CPU load.
- **Reliability**: Consolidated timer logic for better stability.

### Changed (v1.4.3)

- **Security**: Upgraded API key encryption to use portable, randomized salt instead of machine-bound key.
- **Documentation**: Comprehensive XML documentation audit for `GiveawayBot.cs`.
- **Code Quality**: Enhanced `ParseBoolVariant` to return `null` for invalid inputs.

### Fixed (v1.4.2)

- **Toast Notifications**: Resolved an issue where `!giveaway update` and other toast events failed silently on
  some Streamer.bot versions. Switched to `dynamic` dispatch to handle optional parameters correctly.

## [1.4.1] - 2026-01-31

### Fixed (v1.4.1)

- **CI/CD**: Addressed false positives in C# 7.3 compatibility check and fixed Markdown line length violations in
  documentation.

## [1.4.0] - 2026-01-31

### Added (v1.4.0)

- **Auto-Update System**: Bot now checks for updates on startup and via `!giveaway update`.
- **Toast Notifications**: Added configurable toast alerts for `EntryAccepted`, `WinnerSelected`, and more.
- **Privacy**: Update paths are now shown via ephemeral toasts instead of public chat.

## [1.3.3] - 2026-01-30

### Added (v1.3.3)

- **Remote Control**: `IsActive` can now be toggled via global variable (`GiveawayBot_<Profile>_IsActive`) to
  remotely Start/End giveaways.

## [1.3.2] - 2026-01-30

### Added (v1.3.2)

- **Dynamic Configuration**: `MaxEntriesPerMinute`, `RequireSubscriber`, and `SubLuckMultiplier` can now be updated
  on-the-fly via Streamer.bot global variables (`GiveawayBot_<Profile>_<Key>`).
- **Global Override Protection**: Configuration overrides (like `GiveawayBot_ExposeVariables`) are now explicitly
  protected from being pruned by the variable cleanup logic.

### Fixed (v1.3.2)

- **Variable Sync Bug**: Resolved an issue where variables were incorrectly exposed in `Mirror` mode even when
  `ExposeVariables` was disabled.
- **CI/CD**: Fixed C# 7.3 compatibility check pipeline to avoid false positives and remove dependencies on
  proprietary DLLs.

## [1.3.1] - 2026-01-30

### Changed (v1.3.1)

- **Performance**: Optimized `ConfigLoader.GetConfig` to reduce CPH calls by 50% during config checks in `Mirror` mode.

## [1.3.0] - 2026-01-30

### Added (v1.3.0)

- **Timed Giveaways**: `TimerDuration` configuration (e.g., "10m", "30s") allows giveaways to automatically close.
- **Configurable Messages**: `Messages` dictionary in profile config allows full customization of bot responses.
- **Granular Sync**: New two-way sync for individual message variables (`GiveawayBot_<Profile>_Msg_<Key>`).
- **Heartbeat Logging**: Added a 5-minute heartbeat log to `LifecycleTick`.
- **Localization Keys**: Publicly exposed `Loc.Keys`.
- **Performance**: Implemented "Smart Sync" to eliminate redundant global variable updates.

**Full Changelog**: <https://github.com/Sythsaz/Giveaway-Bot/compare/v1.2.0...v1.3.2>
