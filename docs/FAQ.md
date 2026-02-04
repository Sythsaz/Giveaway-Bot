# Frequently Asked Questions (FAQ)

> **Version**: 1.4.3
>
> **[← Back to USER_GUIDE](USER_GUIDE.md) | [Advanced Topics →](ADVANCED.md)**

---

## 📑 Table of Contents

1. [Common Errors](#common-errors)
2. [Bot Detection & Security](#bot-detection--security)
3. [Wheel of Names Connection](#wheel-of-names-connection)
4. [Configuration & State](#configuration--state)
5. [Commands & Operations](#commands--operations)

---

## Common Errors

### "Ticket count inconsistent"

**Error:** `[Code: InvalidState] Ticket count inconsistent`
**Cause:** The number of tickets in the internal tracker doesn't match the sum of entries.
**Fix:** Run `!giveaway p config <profile> check`. It will recalculate tickets.

### "Loop Detected" / "Anti-Loop Triggered"

**Error:** `[AntiLoop] Ignoring trigger: Bot Token Detected`
**Cause:** The bot heard itself or another bot responding to it.
**Fix:**

- Ensure other bots (Nightbot, etc.) aren't repeating the exact command.
- Check `AllowedExternalBots` in `giveaway_config.json`.

### "Config not found (Check RunMode)!"

**Cause:** Bot cannot find a valid config source.
**Fix:**

- If RunMode is `FileSystem`, ensure `giveaway_config.json` is in `data/Giveaway Helper/config/`.
- If RunMode is `GlobalVar`, ensure `GiveawayBot_Config` variable is populated.

---

## Bot Detection & Security

### How does the bot detect fake accounts?

1. **Entropy Check:** Analyzes username randomness. e.g. `asdkljfkj123` has high entropy. `JohnDoe` has low.
2. **Account Age:** Checks how old the account is (requires Streamer.bot to have this data available).
3. **Regex:** Checks against `UsernamePattern` (e.g. banning "hoss00312\_...").

### Why is my legit account blocked?

Check logs for `[Entry] Rejected`.

- **Reason: Entropy**: Disable `EnableEntropyCheck` in config.
- **Reason: Account Age**: Lower `MinAccountAgeDays`.

---

## Wheel of Names Connection

### "Wheel API Error: 403 Forbidden"

- **Cause:** Invalid API Key.
- **Fix:** Update `WheelOfNamesApiKey` global variable. Wait for bot to encrypt it (check logs).

### "Wheel spin failed to start"

- **Cause:** Browser source not open or not connected.
- **Fix:** Ensure the overlay is active in OBS.

---

## Configuration & State

### Which RunMode should I use?

- **`FileSystem`**: Config in JSON only. Fast, but Streamer.bot vars won't update.
- **`GlobalVar`**: Config in Streamer.bot variables only. Good for UI editing.
- **`Mirror`** ⭐: Best of both - syncs file ↔ variables bidirectionally.

---

## Commands & Operations

### Q: Why isn't `!enter` working?

**A:** Check these in order:

1. Is giveaway open? (Run `!start` first)
2. Trigger set? (Streamer.bot must have a Command trigger for `!enter`)
3. Run `!giveaway system test` to diagnose
4. Check logs in `...logs/General/YYYY-MM-DD.log`

### Q: Can I use custom entry commands (not `!enter`)?

**A:** Yes! Edit triggers in profile config:

```json
"Triggers": {
  "command:!ticket": "Enter",
  "command:!join": "Enter"
}
```

### Q: How do I run multiple giveaways at once?

**A:**

1. Create profiles: `!giveaway profile create Weekly`
2. **Batch Start**: `!giveaway profile start *` (Starts ALL profiles)
3. **Targeted**: `!giveaway profile start Main,Weekly`
4. Each profile has independent state/settings

### Q: How do I enable desktop notifications?

**A:** Enable `ToastNotifications` in your profile config. You can get alerts for Winner Selected, Giveaway
Open/Close, and more.
_(Requires Windows 10/11 and Streamer.bot notifications enabled)_

### Q: What does "Sub Luck" actually do?

**A:** Subscribers get **extra tickets** in the drawing pool.

- `SubLuckMultiplier: 2` → Subs get **3 total tickets** (1 base + 2 bonus)
- `SubLuckMultiplier: 0` → Everyone gets 1 ticket (fair mode)

### Q: Can I test regex patterns before deploying?

**A:** Yes! Use: `!giveaway regex test <pattern> <text>`

**Example:**

```bash
!giveaway regex test (?i)giveaway.*open The GIVEAWAY is NOW OPEN!
→ ✅ MATCH: "GIVEAWAY is NOW OPEN"
```

---

## Configuration

### Q: Where do I edit settings?

**A:** Three ways:

1. **JSON File** (recommended): Edit `giveaway_config.json` directly
2. **In-Chat**: `!giveaway profile config Main MaxEntriesPerMinute=100`
3. **Streamer.bot Variables**: Edit `GiveawayBot_Profile_Main_MaxEntriesPerMinute`

### Q: What's the difference between `DumpEntriesOnEnd` and `DumpEntriesOnEntry`?

**A:**

- **`DumpEntriesOnEnd: true`**: Saves **one file** when you run `!end` (final snapshot)
- **`DumpEntriesOnEntry: true`**: Saves entries **as they come in** (real-time, batched every N seconds)

### Q: How do I disable logging?

**A:** Set `LogToStreamerBot: false` in `Globals`. File logs cannot be fully disabled but can be set to info level
(minimal overhead).

### Q: What happens if I set `MaxEntriesPerMinute` too low?

**A:** Legitimate users may get blocked during high-traffic periods. Recommended: 60+ for small streams, 200+ for large.

### Q: How do I make the giveaway close automatically?

**A:** Add `"TimerDuration": "10m"` (or "30s", "1h") to your profile config. The bot will automatically run `!end`
when time runs out.

### Q: Can I change what the bot says?

**A:** Yes! You can override any message (like "Winner Selected") by adding a `"Messages"` block to your config, or
by simply setting a global variable like `GiveawayBot_Main_Msg_WinnerSelected`. See the [User Guide](USER_GUIDE.md)
for details.

---

## Integration (Wheel & OBS)

### Q: Do I need to pay for Wheel of Names API?

**A:** Wheel of Names has free. Check their [page](https://wheelofnames.com/api-doc).

### Q: My Wheel isn't showing in OBS. Why?

**A:**

1. **OBS Source Name**: Must match EXACTLY (case-sensitive)
2. **Scene Active**: OBS must be on the scene you configured
3. **Test**: Run `!draw` and check Streamer.bot logs for "OBS Updated" message
4. **Fallback**: Manually verify the Wheel URL in logs and paste to browser

### Q: Can I customize the Wheel appearance?

**A:** Yes, via Wheel of Names API settings. Customize colors, fonts, spin speed:

```json
"WheelSettings": {
  "ShareMode": "copyable",
  "Theme": "dark",
  "Colors": ["#FF5733", "#33FF57", "#3357FF"]
}
```

See [Wheel API Docs](https://wheelofnames.com/api-doc) for full options.

### Q: What if the Wheel API is down?

**A:** The bot falls back to **local RNG** (random number generator). Winner is still selected and logged, but no
visual spin.

---

## Security & Privacy

### Q: Is my API key secure?

**A:** Yes:

- Encrypted with **AES-256-CBC** on first use
- Derives encryption key from a **randomized salt** stored in your config
- **Portable**: Works if you move your entire bot folder to a new PC
- **Secure**: Won't work if someone steals just the config file (they need the salt + your specific setup context)
- Never logged (all outputs sanitized with `[REDACTED]`)

### Q: What data does the bot store?

**A:**

- **Entry Data**: Username, UserID, timestamp, sub status
- **Logs**: Debug/error messages (sanitized)
- **Config**: Your settings (no passwords)
- **No sensitive data** like chat messages or personal info

### Q: Is the bot GDPR compliant?

**A:** The bot stores minimal data locally. For GDPR compliance:

1. **Automated Deletion**: Run `!giveaway data delete <username>` to scrub a user's active entries, history,
   and metrics from memory and logs.
2. **Retention Policy**: Set `LogRetentionDays` to comply with your region (default: 90 days).
3. **Manual**: You can also manually delete files from the `dumps` folder as well as from the Streamer Bot Variables.
4. No data is sent to external services (except Wheel API if enabled).

### Q: Can other mods see my API keys?

**A:** No. API keys stored in Streamer.bot variables are **only visible to the broadcaster** (you) until they are converted
to the encrypted format. `AES:...` strings are unreadable.

---

## Performance & Limits

### Q: How many entries can the bot handle?

**A:** Tested successfully with **10,000+ entries**. Performance depends on:

- Your PC (RAM/CPU)
- `StatePersistenceMode` (`File` is slower on HDD)
- Entry validation rules (regex is CPU-intensive)

### Q: The bot is slowing down Streamer.bot. Why?

**A:**

1. **Disable verbose logging**: Set `LogLevel` to `INFO` (not `TRACE`)
2. **Reduce disk writes**: Set `StateSyncIntervalSeconds: 60` (from default 30)
3. **Disable real-time dumps**: Set `DumpEntriesOnEntry: false`

### Q: What's the rate limit for Wheel API calls?

**A:** Wheel of Names API has rate limits. The bot includes **automatic retry with exponential
backoff** if you hit limits.

---

## Troubleshooting

### Q: "Config file not found" error

**A:** Run `!giveaway config gen` to create a default config. Then customize as needed.

### Q: "Loop Detected" in logs - what does this mean?

**A:** The bot detected **its own message** and ignored it (anti-loop protection). This is normal and prevents
infinite command loops.

**False positives:** If you see this for legitimate user messages, ensure your Streamer.bot isn't stripping invisible
characters (`\u200B`).

### Q: Variables not updating in OBS

**A:**

1. **Enable exposure**: Set `ExposeVariables: true` in profile config
2. **Check syntax**: OBS text sources use `%VariableName%` NOT `{VariableName}`
3. **Refresh**: Sometimes OBS needs a scene switch to update

### Q: Bot responds but entries aren't saved

**A:** Check `StatePersistenceMode`:

- If `GlobalVar` and Streamer.bot crashes → data lost
- Solution: Use `Both` or `Mirror` mode

### Q: Can't delete a profile

**A:** Must confirm deletion with full command:

```bash
!giveaway profile delete Weekly confirm
```

(Safety measure to prevent accidental deletion)

### Q: Toast notifications not showing

**A:** Windows 10/11 only. Check:

1. **Windows Settings → Notifications → Streamer.bot** must be enabled
2. **Focus Assist** might block (check system tray icon)

---

## Error Messages Index

| Error Message                      | Meaning                                  | Solution                                                 |
| ---------------------------------- | ---------------------------------------- | -------------------------------------------------------- |
| `Missing 'userId' in trigger args` | Streamer.bot didn't pass user data       | Check trigger configuration                              |
| `RATE LIMIT: rejected`             | Too many entries too fast                | Normal - spam protection working                         |
| `Entry rejected: Low entropy`      | Username appears to be keyboard smashing | User needs a real username                               |
| `Account too new`                  | Account age < `MinAccountAgeDays`        | Intentional - bot prevention                             |
| `Giveaway not active`              | User tried `!enter` when closed          | Normal - tell them to wait                               |
| `Encryption check failed`          | API key encryption error                 | Re-enter API key in plain text                           |
| `Config deserialization failed`    | JSON syntax error                        | Run `!giveaway config check` for details                 |
| `File system access denied`        | Permissions issue                        | Run Streamer.bot as admin OR move to unrestricted folder |
| `Wheel API returned 401`           | Invalid API key                          | Verify key at wheelofnames.com                           |
| `Wheel API returned 429`           | Rate limit exceeded                      | Wait 60s, reduce draw frequency                          |
| `Failed to flush entries to dump`  | Disk I/O error                           | Check available disk space                               |
| `OBS connection failed`            | OBS not running or scene/source mismatch | Verify OBS is open + scene/source names                  |

---

## Still Have Questions?

1. **Check Logs**: `.../Giveaway Helper/logs/General/YYYY-MM-DD.log` often has answers
2. **Run Diagnostics**: `!giveaway system test` for detailed health check
3. **Config Validator**: `!giveaway config check` for JSON errors
4. **GitHub Issues**: Report bugs or request features _(if GitHub repo available)_

---

**[← Back to USER_GUIDE](USER_GUIDE.md) | [Advanced Topics →](ADVANCED.md) | [Quick Reference →](README.md)**
