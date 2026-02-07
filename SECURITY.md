# Security Policy

## Supported Versions

We accept security vulnerability reports for the versions listed below. If you are using an older version, please upgrade
to the latest supported version before reporting issues.

| Version | Supported          |
| ------- | ------------------ |
| 1.x.x   | :white_check_mark: |
| < 1.0.0 | :x:                |

## Reporting a Vulnerability

If you discover a security vulnerability in the Giveaway Bot, please **do not open a public issue**. Instead,
please report it via:

- **Discord**: DM `Sythsaz` - Discord Server[https://discord.gg/A7rXNaFTUX]

We will acknowledge your report within 72 hours and provide an estimated timeline for investigation and resolution.

## Security Features

This bot handles sensitive information (API keys) and interacts with public chat environments. It includes several
built-in security mechanisms:

### 1. API Key Encryption (AES-256-CBC)

The bot uses **AES-256-CBC** with a portable, randomized salt to encrypt sensitive configuration values at rest.

- **Automatic Encryption**: On first run, any plain-text API keys in `giveaway_config.json` (specifically
  `WheelOfNamesApiKey`) are automatically encrypted.
- **Portable Scope**: Keys are encrypted using a salt stored in your config file (`EncryptionSalt`). This means:
  - You **CAN** move your bot folder to a new PC (it will still work).
  - An attacker **CANNOT** decrypt your keys with just the config file (they need the salt AND the context of the
    running application).
- **Protection**: This prevents API keys from being stolen if the configuration file is accidentally shared without the
  salt or committed to version control.

### 2. Anti-Loop Protection

To prevent bot recursion and infinite loops:

- The bot inserts a zero-width space configuration token into its own output messages.
- It detects this token in incoming messages and ignores them, preventing it from triggering itself.

### 3. Bot Detection & Validation

To ensure fair giveaways, the bot implements several validation layers:

- **Entropy Checks**: Uses Shannon entropy analysis to detect random "keyboard smash" usernames typically used by
  mass-entry bots.
- **Account Age Verification**: Configurable `MinAccountAgeDays` prevents brand new accounts from entering.
- **Username Patterns**: Regex-based validation (`UsernamePattern`) to enforce community naming standards.

## Best Practices

- **Never commit `giveaway_config.json`** to public repositories if it contains unencrypted keys.
- **Rotate your API keys** immediately if you suspect they have been compromised.
- **Use "Mirror" RunMode** cautiously, as it syncs data between file system and global variables.

## Incident Response

In the event of a confirmed vulnerability:

1. We will issue a patch release immediately.
2. We will publish a security advisory detailing the impact and mitigation.
3. We will credit the reporter (with permission).
