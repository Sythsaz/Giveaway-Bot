# Roadmap

This document outlines the future development plans for the Giveaway Bot.

## 🚀 Planned Features

### Data Management & Privacy

- [ ] **Advanced GDPR Cleanup**: Iterate known user IDs from active profiles to clean associated globals (currently relies on `!giveaway data delete` resolving IDs from active entries or command arguments).

## 💡 Potential Improvements

- [ ] **Unified Event Bus**: Refactor `Messenger.SendBroadcast` and `CPHAdapter` calls into a stricter event-driven architecture to decouple logic from Streamer.bot actions further.
- [ ] **Cross-Profile Analytics**: Add a command to generate aggregate stats across all profiles (e.g., total entries across "Daily" and "Weekly" giveaways).

## 🔧 Maintenance

- [ ] **Unit Test Coverage**: Expand `TestRunner.cs` to cover edge cases for the new `Loc` system and `ImportGlobals` logic.
- [ ] **Documentation Localization**: Translate `USER_GUIDE.md` into other languages if community demand arises.
