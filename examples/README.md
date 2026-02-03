# Example Configurations

This directory contains pre-configured `giveaway_config.json` examples for various use cases.

## How to Use

1. **Choose an example** that fits your needs.
2. **Copy the content** to `.../Streamer.bot/data/Giveaway Bot/config/giveaway_config.json`.
3. **Modify** any specific values (like API keys, ObsScene names).
4. **Resave** the file. The bot will automatically reload (if RunMode is not set to ReadOnlyVar).

## Available Examples

### [01-minimal-starter.json](01-minimal-starter.json)

Start here if you're new! Contains just the "Main" profile with essential commands (`!enter`, `!draw`).
Good for testing.

### [02-multi-profile-setup.json](02-multi-profile-setup.json)

A complete setup exhibiting the bot's power:

- **Main**: Standard giveaway for everyone.
- **Weekly**: A separate long-running giveaway.
- **SubOnly**: Restricted to subscribers logic (via sub luck).

### [03-advanced-features.json](03-advanced-features.json)

For power users who need strict control:

- **Bot Protection**: Entropy check enabled.
- **Account Age**: Minimum 30 days.
- **Username Pattern**: Restricted alphameric names.
- **External Bots**: Parsing entries from Nightbot.

### [04-wheel-of-names.json](04-wheel-of-names.json)

Focuses on the visual experience:

- **Wheel Integrated**: Automatically sends entries to Wheel of Names.
- **OBS Control**: Switches scenes automatically.
- **Toast Notifications**: Detailed desktop alerts.

### [05-timed-and-custom-messages.json](05-timed-and-custom-messages.json)

Showcasing v1.3.0 features:

- **Timed Giveaway**: A "Flash Giveaway" profile that closes automatically after 5 minutes (`TimerDuration`).
- **Custom Messages**: Customized "HYPE" messages using the `Messages` dictionary.
- **Toast Notifications**: Enabled for high-priority events.
