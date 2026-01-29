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
