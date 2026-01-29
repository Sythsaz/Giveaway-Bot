# Roadmap

This document outlines the future development plans for the Giveaway Bot.

## 🚀 Planned Features

### Data Management & Privacy

- [ ] **GDPR Data Deletion Command**: Add a command (e.g., `!giveaway data delete <user>`) to automate the removal of
      user data from local dump files and active state to simplify GDPR compliance.

## 💡 Potential Improvements

- [ ] **Global Variable Auto-Import**: Fully automate the restoration of Streamer.bot global variables from
      `giveaway_config.json` on startup (currently partially manual for new installs).
- [ ] **Localization Support**: Allow configuration of response messages for different languages.

## 🔧 Technical Debt & Maintenance

- [ ] **Resolve Unauthorized Command Handling**: Address the TODO in `ProcessTrigger` regarding unauthorized management
      attempts. Decide whether to warn the user, silently ignore, or send a specific broadcast message.
      (Ref: `GiveawayManager.ProcessTrigger` ~line 1916)
- [ ] **Fix Incomplete Dispose Pattern**: Ensure `_dumpTimer` and any other disposable resources are correctly disposed
      in `GiveawayManager.Dispose` to prevent potential memory leaks during bot re-initialization.
- [ ] **Optimize Config Sync Performance**: Investigate the performance impact of `CheckForConfigUpdates` deserializing
      JSON on every trigger. Consider implementing a hash check or rate limiting to reduce overhead.
- [ ] **Enhance Config Sync Error Logging**: Elevate JSON parsing errors in `CheckForConfigUpdates` from `LogTrace` to
      `LogWarn` to ensure users are alerted to invalid configuration JSON in global variables.
