# Roadmap

This document outlines the future development plans for the Giveaway Bot.

## 🚀 Planned Features

### Data Management & Privacy

- [x] **Advanced GDPR Cleanup**: Iterate known user IDs from active profiles to clean associated globals
      (currently relies on `!giveaway data delete` resolving IDs from active entries or command arguments).

## 💡 Potential Improvements

- [ ] **Unified Event Bus**: Refactor `Messenger.SendBroadcast` and `CPHAdapter` calls into a stricter
      event-driven architecture to decouple logic from Streamer.bot actions further.
- [x] **Cross-Profile Analytics**: Add a command to generate aggregate stats across all profiles
      (e.g., total entries across "Daily" and "Weekly" giveaways).

## 🔧 Maintenance

- [ ] **Unit Test Coverage**: Expand `TestRunner.cs` to cover edge cases for the new `Loc` system and `ImportGlobals` logic.
- [ ] **Documentation Localization**: Translate `USER_GUIDE.md` into other languages if community demand arises.

## 📋 Backlog & Technical Debt

- [x] **Standardize Timer Usage**: Consolidate `System.Threading.Timer` and `System.Timers.Timer` usage for
      consistency across the codebase.
- [x] **Refactor ObsController**: Resolve static vs instance method confusion in `ObsController` to clarify the
      API and usage.
- [x] **Configuration Schema Validation**: Implement stricter JSON schema validation during config load to prevent
      typos and invalid structures.
- [x] **Concurrency Stress Testing**: Create specialized unit tests targeting race conditions (e.g., parallel
      entries vs draw) to verify robustness.
- [x] **Security Review**: Evaluate the current `Environment`-based key derivation strategy for AES encryption to
      ensure it meets security requirements.

## 🔒 Technical Constraints

### C# 7.3 Compatibility (Streamer.bot Runtime)

This project currently targets **C# 7.3** and **.NET Framework 4.8** due to Streamer.bot's embedded scripting
runtime environment.

**If Streamer.bot upgrades to a newer .NET runtime**, this project will adopt modern C# features accordingly.
Until then, we cannot use:

- ❌ **C# 8.0+**: Nullable reference types, pattern matching enhancements, using declarations, `??=` operator, switch expressions
- ❌ **C# 9.0+**: Target-typed new expressions, records, init-only setters
- ❌ **C# 10.0+**: Global usings, file-scoped namespaces, record structs
- ❌ **C# 11.0+**: Required members, list patterns, raw string literals
- ❌ **C# 12.0+**: Primary constructors, collection expressions

**Note**: The `.editorconfig` file suppresses IDE suggestions for these features to reduce noise during
development. These suppressions should be reviewed and removed when Streamer.bot updates its runtime.

### Why This Matters

- **Copy-Paste Deployment**: The bot deploys as a single C# file directly into Streamer.bot's "Execute C# Code" action
- **No Control Over Runtime**: We cannot bundle or upgrade dependencies; we must match Streamer.bot's environment exactly
- **Future-Ready**: The architecture is designed to be upgrade-friendly when Streamer.bot evolves
