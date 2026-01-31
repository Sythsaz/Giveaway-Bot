# Commit Summary

Here is a summary of the changes for your GitHub commit.

## 📦 Features

### Auto-Update Notification System (v1.4.0)

- **Implemented `UpdateService`**: Checks `RELEASE_NOTES.md` on GitHub for newer versions.
- **New Command `!giveaway update`**:
  - Checks for updates.
  - Downloads the latest `GiveawayBot.cs` to a local `Giveaway Helper/updates/` folder.
  - Notifies via **Toast Notification** with the exact file path.
- **Startup Check**: Automatically checks for updates when the bot initializes and broadcasts a notification if one is found.
- **Privacy & Security**:
  - Does NOT auto-replace the running script (requires user action).
  - Uses Toast notifications to keep file paths private.

## 📝 Documentation

- **File**: `GiveawayBot.cs`
  - **Version Bump**: Updated `Version` constant to `1.4.0`.
  - Added `UpdateService` class (nested in `CPHInline` for `CPHAdapter` access).
  - Added `_updateService` field to `GiveawayManager`.
  - Added `CheckForUpdatesStartup` method called in `Initialize`.
  - Added `HandleUpdateCommand` method in `ProcessTrigger`.
  - **Fixes**:
    - Fixed `CPHAdapter.ShowToastNotification` arguments to prevent crash.
    - Improved nullability handling in `UpdateService`.
  - Executed a comprehensive code-to-docs audit.

### CHANGELOG.md

- Added `[1.4.0]` entry:
  - **Added**: Auto-Update Notification System details.
  - **Fixed**: Toast notification crash fix and code quality improvements.
- Fixed markdown line-length lint errors.
