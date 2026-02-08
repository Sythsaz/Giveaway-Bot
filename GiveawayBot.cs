// Suppress "modernization" suggestions to maintain compatibility with Streamer.bot's internal compiler
// Streamer.bot uses .NET Framework 4.8 / C# 7.3
// CI Verification Trigger environment)
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0301 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified
#pragma warning disable IDE0290 // Use primary constructor
#pragma warning disable IDE0055 // Fix formatting
#pragma warning disable IDE0034 // Simplify default expression
#pragma warning disable IDE0001 // Simplify name
#pragma warning disable IDE0074 // Use compound assignment
#pragma warning disable IDE0063 // 'using' statement can be simplified
#pragma warning disable IDE1006 // Naming styles
#pragma warning disable CA1825 // Avoid zero-length array allocations
#pragma warning disable RCS1077 // Optimize LINQ
#pragma warning disable RCS1036 // Optimize LINQ
#pragma warning disable RCS1079 // Throwing exceptions in finally
#pragma warning disable CS8603 // Possible null reference return
#pragma warning disable CS8601 // Possible null reference assignment
#pragma warning disable CS8618 // Non-nullable property initialization
#pragma warning disable CS8610 // Nullability of reference types in type of parameter doesn't match overridden member
#pragma warning disable CS8604 // Possible null reference argument
#pragma warning disable CS8602 // Dereference of a possibly null reference
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type
#pragma warning disable CS8605 // Unboxing a possibly null value
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0019 // Use pattern matching
#pragma warning disable IDE0078 // Use pattern matching
#pragma warning disable IDE0083 // Use pattern matching

// Streamer.bot specific / C# 7.3 Limits
#pragma warning disable CA1850 // Prefer static 'HashData' over 'ComputeHash' (.NET 5+)
#pragma warning disable CA2249 // Consider using 'string.Contains' with char (.NET Core 2.1+)
#pragma warning disable IDE0066 // Convert switch statement to expression (C# 8.0)
#pragma warning disable CA1854 // Prefer 'TryGetValue' (Keep simple)
#pragma warning disable CA1836 // Prefer 'IsEmpty' over 'Count' (Compatibility)
#pragma warning disable IDE0059 // Unnecessary assignment (Regex discard)
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0028 // Duplicate suppression (just in case)

// css_ref System.Net.Http.dll
// css_ref Newtonsoft.Json.dll
// css_ref System.Core.dll
// css_ref System.Xml.Linq.dll
// css_ref Microsoft.CSharp.dll
#region Imports & Assembly Attributes
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Reflection;
using System.Security.Cryptography;
using System.Globalization;
#endregion




// UNCONDITIONAL SUPPRESSION for Streamer.bot (Legacy C# Environment) - Required for valid compilation
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0301 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified
#pragma warning disable IDE0290 // Use primary constructor
#pragma warning disable IDE0055 // Fix formatting
#pragma warning disable IDE0034 // Simplify default expression
#pragma warning disable IDE0001 // Simplify name
#pragma warning disable IDE1006 // Naming styles
#pragma warning disable CA1825 // Avoid zero-length array allocations
#pragma warning disable RCS1077 // Optimize LINQ
#pragma warning disable RCS1036 // Optimize LINQ
#pragma warning disable RCS1079 // Throwing exceptions in finally
#pragma warning disable CS8603 // Possible null reference return
#pragma warning disable CS8601 // Possible null reference assignment
#pragma warning disable CS8618 // Non-nullable property initialization
#pragma warning disable CS8604 // Possible null reference argument
#pragma warning disable CS8602 // Dereference of a possibly null reference
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type
#pragma warning disable CS8605 // Unboxing a possibly null value
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0019 // Use pattern matching
#pragma warning disable IDE0078 // Use pattern matching
#pragma warning disable IDE0083 // Use pattern matching


#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
namespace StreamerBot
{
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0301 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified
#pragma warning disable IDE0290 // Use primary constructor
#pragma warning disable IDE0055 // Fix formatting
#pragma warning disable IDE0034 // Simplify default expression
#pragma warning disable IDE0001 // Simplify name
#pragma warning disable IDE1006 // Naming styles
#pragma warning disable CA1825 // Avoid zero-length array allocations
#pragma warning disable RCS1077 // Optimize LINQ
#pragma warning disable RCS1036 // Optimize LINQ
#pragma warning disable RCS1079 // Throwing exceptions in finally
#pragma warning disable CS8603 // Possible null reference return
#pragma warning disable CS8601 // Possible null reference assignment
#pragma warning disable CS8618 // Non-nullable property initialization
#pragma warning disable CS8604 // Possible null reference argument
#pragma warning disable CS8602 // Dereference of a possibly null reference
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type
#pragma warning disable CS8605 // Unboxing a possibly null value
#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable IDE0019 // Use pattern matching
#pragma warning disable IDE0078 // Use pattern matching
#pragma warning disable IDE0083 // Use pattern matching
#endif

#region Interfaces
    /// <summary>
    /// Interface wrapper for Streamer.bot's `CPH` object.
    /// Allows us to mock the CPH interaction for unit testing outside of Streamer.bot.
    /// This abstraction is crucial for maintaining testability and decoupling the bot logic from the runtime environment.
    /// </summary>
    public interface IGiveawayCPH
    {
        // Logging Methods
        /// <summary>Logs an informational message to the Streamer.bot log.</summary>
        void LogInfo(string message);
        /// <summary>Logs a warning message to the Streamer.bot log.</summary>
        void LogWarn(string message);
        /// <summary>Logs a debug message to the Streamer.bot log (requires verbose logging).</summary>
        void LogDebug(string message);
        /// <summary>Logs an error message to the Streamer.bot log.</summary>
        void LogError(string message);
        /// <summary>Logs a trace message for deep debugging.</summary>
        void LogTrace(string message);
        /// <summary>Logs a verbose message.</summary>
        void LogVerbose(string message);
        /// <summary>Logs a fatal error message, typically indicating a crash or critical failure.</summary>
        void LogFatal(string message);

        // Argument Handling
        /// <summary>
        /// Attempts to retrieving an argument from the action's argument dictionary.
        /// </summary>
        /// <typeparam name="T">The expected type of the argument.</typeparam>
        /// <param name="argName">The key name of the argument.</param>
        /// <param name="value">The output value if found.</param>
        /// <returns>True if found and successfully converted, otherwise false.</returns>
        bool TryGetArg<T>(string argName, out T value);

        // Variable Management
        /// <summary>Retrieves a global variable from Streamer.bot's persistent store.</summary>
        T GetGlobalVar<T>(string varName, bool persisted = true);
        /// <summary>Sets a global variable in Streamer.bot's persistent store.</summary>
        void SetGlobalVar(string varName, object value, bool persisted = true);
        /// <summary>Retrieves a user-specific variable.</summary>
        T GetUserVar<T>(string userId, string varName, bool persisted = true);
        /// <summary>Sets a user-specific variable.</summary>
        void SetUserVar(string userId, string varName, object value, bool persisted = true);
        /// <summary>Gets a list of all global variable names (used for cleanup).</summary>
        List<string> GetGlobalVarNames(bool persisted = true);
        /// <summary>Removes a global variable from the store.</summary>
        void UnsetGlobalVar(string varName, bool persisted = true);

        // Platform Actions
        /// <summary>Sends a chat message to the active platform (Twitch/YouTube/Kick).</summary>
        void SendMessage(string message, bool bot = true);
        /// <summary>Sends a chat message specifically to YouTube.</summary>
        void SendYouTubeMessage(string message);
        /// <summary>Sends a chat message specifically to Kick.</summary>
        void SendKickMessage(string message);
        /// <summary>Replies to a specific message on Twitch.</summary>
        void TwitchReplyToMessage(string message, string replyId, bool useBot = true, bool fallback = true);

        // State Checks
        /// <summary>Checks if the Twitch broadcaster is live.</summary>
        bool IsTwitchLive();
        /// <summary>Checks if the YouTube broadcaster is live.</summary>
        bool IsYouTubeLive();
        /// <summary>Checks if the Kick broadcaster is live.</summary>
        bool IsKickLive();

        // User Status
        /// <summary>Checks if a user is following the channel on Twitch.</summary>
        bool TwitchIsUserFollower(string userId);
        /// <summary>Checks if a user is subscribed to the channel on Twitch.</summary>
        bool TwitchIsUserSubscriber(string userId);

        // OBS Integration
        /// <summary>Updates a browser source URL in OBS via Streamer.bot.</summary>
        void ObsSetBrowserSource(string scene, string source, string url);

        // UI & Core
        /// <summary>Shows a Windows toast notification via Streamer.bot.</summary>
        void ShowToastNotification(string title, string message);
        /// <summary>Executes another Streamer.bot action by name.</summary>
        bool RunAction(string actionName, bool runImmediately = true);
        /// <summary>Gets the event trigger type.</summary>
        object GetEventType();

        // Internal
        /// <summary>Access to the internal file logger instance.</summary>
        FileLogger Logger { get; set; }
    }
#endregion

/*
 * Streamer.bot Giveaway Bot
 *
 * Target Environment: Streamer.bot Execute C# Code Action
 * Compatibility: .NET Framework 4.8 / C# 7.3
 *
 * Description:
 * A comprehensive giveaway management system for Streamer.bot.
 * Features include multi-profile support, anti-loop protection,
 * Bidirectional Config Sync (Mirror Mode), Separate Game Name Dumps,
 * Wheel of Names integration, OBS control, and robust logging/persistence.
 *
 * Rules & constraints:
 * - Must reside in a single file for CPHInline copy/paste deployment.
 * - Must NOT use C# 8.0+ features (e.g. file-scoped namespaces, records).
 * - Must handle CPH interaction via reflection or direct calls safely.
 */

/*--------------------------------------------*/
#region GiveawayBotHostBase
#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
    // Base class validation to ensure editor compatibility without external references
    public class GiveawayBotHostBase
    {
        public dynamic CPH { get; set; }
        // Mock args for editor support
        public Dictionary<string, object> args { get; set; } = new Dictionary<string, object>();
    }
    public class GiveawayBot : GiveawayBotHostBase
#else
public class CPHInline
#endif
#endregion
    /*--------------------------------------------*/
#region GiveawayBot Main Class
    {
        // Singleton instance to maintain state across multiple executions of the Execute() method
        private static GiveawayManager _manager;
        // Lock object to ensure thread-safe initialization of the singleton
        private static readonly object _initLock = new object();

        // Identity Constants
        private const string ActionName = "Giveaway Bot";
        public const string Version = GiveawayManager.Version;


        /// <summary>
        /// Entry point for the Streamer.bot Action.
        /// This method is called every time the action is triggered.
        /// It ensures the manager is initialized and then processes the current trigger.
        /// </summary>
        public bool Execute()
        {
            var adapter = new CPHAdapter(CPH, args);

            // Restore logger from singleton if it exists
            if (_manager != null)
            {
                adapter.Logger = _manager.Logger;
                // Force reset the thread-local logging flag in case a Previous execution on this thread hung
                FileLogger.ResetLoggingFlag();
            }

            // Safe RunMode check to avoid re-entrancy during the very first LogTrace
            string runMode = "Unknown";
            try { runMode = ConfigLoader.GetRunMode(adapter); } catch { }

            adapter.LogTrace($"[GiveawayBot] [Execute] >>> Entry point triggered. RunMode: {runMode}");
            try
            {
                EnsureInitialized(adapter);
                // Pass the CPH adapter to ensure the Logger and state persist correctly
                return _manager.ProcessTrigger(adapter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                adapter.LogFatal($"[GiveawayBot] [Execute] Unhandled exception in root: {ex.Message}");
                // Fallback direct bot log if adapter fails
                try { CPH.LogError($"[GiveawayBot] [Execute] FATAL CRASH: {ex.Message}"); } catch { }
                return false;
            }
            finally
            {
                adapter.LogTrace("[GiveawayBot] [Execute] <<< Exit point reached.");
            }
        }

        private static void EnsureInitialized(CPHAdapter adapter)
        {
            if (_manager != null) return;

            lock (_initLock)
            {
                if (_manager != null) return;

                adapter.LogDebug("[GiveawayBot] [EnsureInitialized] Initializing Giveaway Manager singleton...");

                _manager = new GiveawayManager
                {
                    Logger = new FileLogger()
                };
                adapter.Logger = _manager.Logger;

                adapter.LogVerbose("[GiveawayBot] [EnsureInitialized] FileLogger initialized and attached to CPH adapter.");

                _manager.Initialize(adapter);
                adapter.LogInfo("[GiveawayBot] [EnsureInitialized] Giveaway System initialized successfully.");
            }
        }
    }
#endregion
#region Localization
/// <summary>
/// Static Localization Helper.
/// Manages default strings and user overrides from config.
/// </summary>
public static class Loc
{
    private static readonly Dictionary<string, string> _defaults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Entries
            { "EntryAccepted", "Entry accepted! Total tickets: {0} ({1})" }, // {0}=Tickets, {1}=Luck
            { "EntryAccepted_NoLuck", "Entry accepted! Total tickets: {0}" },
            { "EntryRejected_MaxEntries", "You have reached the maximum number of entries." },
            { "EntryRejected_TooNew", "Your account is too new to join this giveaway." },
            { "EntryRejected_LowEntropy", "Entry rejected by anti-bot protection." },
            { "EntryRejected_Blacklisted", "You are not allowed to join this giveaway." },
            { "EntryRejected_NotFollower", "Sorry, you must be a follower to join this giveaway!" },
            { "EntryRejected_NotSubscriber", "Sorry, you must be a subscriber to join this giveaway!" },
            { "EntryRejected_GiveawayClosed", "The giveaway is currently closed." },

            // Toasts & UI
            { "ToastTitle", "Giveaway Bot" },
            { "ToastTitle_Security", "Giveaway Bot - Security" },
            { "ToastMsg_NewEntry", "New Entry: {0}" },
            { "ToastMsg_Rejected_Pattern", "Entry Rejected: {0} (Username Pattern)" },
            { "EntryRejected_GW2Pattern", "Sorry @{0}, please include your Guild Wars 2 account name (Format: Name.1234)" },
            { "ToastMsg_Rejected_Age", "Entry Rejected: {0} (Account Too New)" },
            { "ToastMsg_SpamDetected", "⚠ SPAM DETECTED: {0} (Rate Limit)" },
            { "ToastMsg_Opened", "Giveaway '{0}' is OPEN!" },
            { "ToastMsg_Closed", "Giveaway '{0}' is CLOSED!" },
            { "ToastMsg_Winner", "Winner: {0}!" },

            // Draw
            { "WinnerSelected", "🎉 Congratulations @{0}! You have won the giveaway!" },
            { "WinnerDrawn_Log", "Winner drawn: {0} (Tickets: {1})" },

            // State
            { "GiveawayOpened", "🎟 The giveaway for '{0}' is now OPEN! Type !enter to join." },
            { "GiveawayOpened_NoProfile", "🎟 The giveaway is now OPEN! Type !enter to join." },
            { "GiveawayClosed", "🚫 The giveaway is now CLOSED! No more entries accepted." },
            { "GiveawayFull", "🚫 The giveaway is FULL! No more entries accepted." },
            { "TimerUpdated", "⏳ Time limit updated! Ends in {0}." },

            // Update Service
            { "Update_CheckFailed", "❌ Failed to fetch release info." },
            { "Update_Available", "⬇ Update Available: {0}\nDownloading..." },
            { "Update_Downloaded", "✅ Saved to: updates/{0}\nImport into Streamer.bot to apply." },
            { "Update_FailedDownload", "❌ Failed to download file." },
            { "Update_UpToDate", "✅ You are on the latest version ({0})." },

            // Errors
            { "Error_Loop", "Loop detected. Setup error." },
            { "Error_System", "A system error occurred." },
            { "Error_NoPermission", "You do not have permission to use this command." }
        };

    public static IEnumerable<string> Keys => _defaults.Keys;

    public static string Get(string key, params object[] args)
    {
        return Get(key, null, args);
    }

    public static string Get(string key, string profileName, params object[] args)
    {
        string template = null;

        // Try Profile Overrides
        if (!string.IsNullOrEmpty(profileName) && GiveawayManager.GlobalConfig != null)
        {
            if (GiveawayManager.GlobalConfig.Profiles.TryGetValue(profileName, out var profile) && profile.Messages != null)
            {
                if (profile.Messages.TryGetValue(key, out var profileStr))
                {
                    template = profileStr;
                }
            }
        }

        // Try Global Custom Strings
        if (template == null && GiveawayManager.GlobalConfig?.Globals?.CustomStrings != null)
        {
            if (GiveawayManager.GlobalConfig.Globals.CustomStrings.TryGetValue(key, out var overrideStr))
            {
                template = overrideStr;
            }
        }

        // Try Default Dictionary
        if (template == null && _defaults.TryGetValue(key, out var defStr))
        {
            template = defStr;
        }

        // Fallback
        if (template == null) return $"[{key}]";

        // Randomize (Pick variant if | or , exists)
        template = GiveawayManager.PickRandomMessage(template);

        // Format
        if (args != null && args.Length > 0)
        {
            try { return string.Format(template, args); }
            catch { return template; } // Fail safe
        }
        return template;
    }
}
#endregion



#region GiveawayManager Core
    /// <summary>
    /// Core logic manager for the Giveaway Bot.
    /// Handles configuration loading, state management, trigger routing, and command execution.
    /// </summary>
    public class GiveawayManager : IDisposable
    {
        public const string Version = "1.5.6"; // Semantic Versioning (canonical: VERSION file)

        // ==================== Instance Fields ====================

        private bool _isDisposed = false;
        private ConfigLoader _configLoader;
        public ConfigLoader Loader => _configLoader;


        // The current active configuration, reloaded periodically
        private static GiveawayBotConfig _globalConfig;
        /// <summary>
        /// Gets or sets the globally shared configuration object.
        /// This is static to allow access from helper classes (like CPHAdapter logs),
        /// but effectively owned by the singleton GiveawayManager.
        /// </summary>
        public static GiveawayBotConfig GlobalConfig
        {
            get => _globalConfig;
            set => _globalConfig = value;
        }
        /// <summary>Accessor for use in static contexts (like logging).</summary>
        public static GiveawayBotConfig StaticConfig => _globalConfig;

        /// <summary>
        /// Thread-safe dictionary of active giveaway states, keyed by profile name (e.g., "Main", "Weekly").
        /// </summary>
        public ConcurrentDictionary<string, GiveawayState> States { get; private set; }

        // Timer for incremental entry dumping (batch processing)
        private System.Threading.Timer _dumpTimer = null;
        private int _tickCount = 0; // Optimization for polling frequency
        private CPHAdapter _currentAdapter; // Store for timer callback access

        // Cache for Trigger JSON strings to prevent redundant deserialization
        private Dictionary<string, string> _triggersJsonCache = new Dictionary<string, string>();

        // General cache for last synced variable values (Metrics + Config) to prevent log spam
        // Key: Global Variable Name, Value: Last synced value
        private Dictionary<string, object> _lastSyncedValues = new Dictionary<string, object>();

        // Operation tracking to prevent race conditions in Mirror mode
        // Tracks current state-changing operations to prevent false remote detection
        // Format: "START:ProfileName" or "END:ProfileName"
        private string _currentOperation = null;
        private readonly object _opLock = new object();

        /// <summary>
        /// Helper to set a global variable only if the value has changed.
        /// Dramatically reduces log spam by avoiding redundant SetGlobalVar calls.
        /// </summary>
        /// <param name="adapter">CPH Adapter</param>
        /// <param name="varName">Variable Name</param>
        /// <param name="value">New Value</param>
        /// <param name="persisted">Persist to disk (default true)</param>
        private void SetGlobalVarIfChanged(CPHAdapter adapter, string varName, object value, bool persisted = true)
        {
            if (!_lastSyncedValues.TryGetValue(varName, out object last) || !object.Equals(last, value))
            {
                _lastSyncedValues[varName] = value;
                adapter.SetGlobalVar(varName, value, persisted);
            }
            // Always touch to prevent pruning, even if value didn't change interpretation
            adapter.TouchGlobalVar(varName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveawayManager"/> class.
        /// Sets up the state container.
        /// </summary>
        public GiveawayManager()
        {
            States = new ConcurrentDictionary<string, GiveawayState>();
        }

        /// <summary>
        /// Sanitizes a string by replacing the application base directory with [BaseDir].
        /// Prevents leaking absolute paths in chat messages.
        /// </summary>
        /// <param name="input">The raw path string.</param>
        /// <returns>The sanitized string with [BaseDir] placeholder.</returns>
        public static string SanitizePath(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Security/Privacy: Sanitize Path
            // We want to prevent accidentally showing the user's full hard drive path in chat or logs (e.g., C:\Users\Ashton\...).
            // This method replaces the base directory with a safe placeholder "[BaseDir]".
            if (string.IsNullOrEmpty(baseDir)) return input;

            // Normalize slashes for comparison if needed, but simple replacement usually works
            return input.Replace(baseDir, "[BaseDir]\\").Replace(baseDir.TrimEnd('\\'), "[BaseDir]");
        }

        /// <summary>
        /// Selects a random message option if the input contains delimiters (| or ,).
        /// Prioritizes Pipe (|) splitting. duplicate commas are treated as text if pipes exist.
        /// </summary>
        /// <param name="rawMsg">The input message string (potentially containing delimiters).</param>
        /// <returns>A single selected message string.</returns>
        public static string PickRandomMessage(string rawMsg)
        {
             if (string.IsNullOrWhiteSpace(rawMsg)) return rawMsg;

             // ---------------------------------------------------------
             // RANDOM MESSAGE SELECTION LOGIC
             // ---------------------------------------------------------
             // Priority 1: Pipe Delimiter (|)
             // Used for advanced messages that might contain commas.
             // Example: "Hello, World|Hi, There" -> "Hello, World" OR "Hi, There"
             if (rawMsg.Contains("|"))
             {
                 var options = rawMsg.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                 return options.Length > 0 ? options[new Random().Next(options.Length)].Trim() : rawMsg;
             }

             // Priority 2: Comma Delimiter (,)
             // Used for simple lists.
             // Example: "Hi,Hello,Hey" -> "Hi" OR "Hello" OR "Hey"
             // We only fallback to this if NO pipes are present to avoid breaking sentences.
             var commaOptions = rawMsg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
             if (commaOptions.Length > 1)
             {
                 return commaOptions[new Random().Next(commaOptions.Length)].Trim();
             }

             // No delimiters found? Return the message as-is.
             return rawMsg;
        }

        // Timer for lifecycle events (timed giveaways)
        private System.Threading.Timer _lifecycleTimer = null;

        /// <summary>
        /// Parses a duration string (e.g., "10m", "1h", "30s") into total seconds.
        /// Defaults to minutes if no unit is specified (e.g., "5" -> 300s).
        /// Returns null if parsing fails.
        /// </summary>
        /// <param name="durationStr">The duration string to parse.</param>
        public static int? ParseDuration(string durationStr)
        {
            if (string.IsNullOrWhiteSpace(durationStr)) return null;
            durationStr = durationStr.Trim();
            if (durationStr.StartsWith("Enter duration", StringComparison.OrdinalIgnoreCase)) return null;
            durationStr = durationStr.ToLowerInvariant();

            int totalSeconds = 0;
            bool matched = false;

            // Regex Explanation:
            // (\d+)  -> Captures one or more digits (the number value).
            // ([wdhms]) -> Captures a single character from the set w,d,h,m,s (the unit).
            // Example: "1h30m" will match twice:
            // Match 1: Group 1="1", Group 2="h"
            // Match 2: Group 1="30", Group 2="m"
            var matches = Regex.Matches(durationStr, @"(\d+)([wdhms])");

            foreach (Match m in matches)
            {
                if (int.TryParse(m.Groups[1].Value, out int val) && val > 0)
                {
                    matched = true;
                    string unit = m.Groups[2].Value;
                    int multiplier = 1;
                    switch (unit)
                    {
                        case "s": multiplier = 1; break;
                        case "m": multiplier = 60; break;
                        case "h": multiplier = 3600; break;
                        case "d": multiplier = 86400; break;
                        case "w": multiplier = 604800; break;
                    }
                    totalSeconds += val * multiplier;
                }
            }

            if (matched) return totalSeconds;

            // Fallback for simple numbers (assumed MINUTES for user convenience)
            if (int.TryParse(durationStr, out int valMinutes) && valMinutes > 0)
                return valMinutes * 60; // Convert minutes to seconds

            return null;
        }

        /// <summary>
        /// Safely parses a duration string without throwing exceptions.
        /// Wraps ParseDuration logic in a try-catch for robustness.
        /// </summary>
        public static bool TryParseDurationSafe(string durationStr, out int seconds, CPHAdapter adapter = null)
        {
            seconds = 0;
            try
            {
                int? result = ParseDuration(durationStr);
                if (result.HasValue)
                {
                    seconds = result.Value;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                adapter?.LogWarn($"[Timer] Error parsing duration '{durationStr}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Resolves a target string (ProfileName, *, all, or comma-list) into a list of profile names.
        /// </summary>
        private List<string> ParseProfileTargets(CPHAdapter adapter, string input)
        {
            var results = new List<string>();
            if (GlobalConfig?.Profiles == null) return results;

            if (input.Equals("*") || input.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                return GlobalConfig.Profiles.Keys.ToList();
            }

            var tokens = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in tokens)
            {
                var trimmed = t.Trim();
                // Case-insensitive lookup
                var match = GlobalConfig.Profiles.Keys.FirstOrDefault(k => k.Equals(trimmed, StringComparison.OrdinalIgnoreCase));
                if (match != null) results.Add(match);
                else adapter.LogTrace($"[GiveawayManager] [ParseProfileTargets] Target '{trimmed}' found no matching profile.");
            }
            return results.Distinct().ToList();
        }

        /// <summary>
        /// Disposes managed resources including timer and lock.
        /// Call this method on bot shutdown to prevent resource leaks.
        /// </summary>
        public void Dispose()
        {
            _isDisposed = true;
            _dumpTimer?.Dispose();
            _lifecycleTimer?.Dispose();
            _lock?.Dispose();
            GC.SuppressFinalize(this);
        }

        public WheelOfNamesClient WheelClient { get; set; }
        public ObsController Obs { get; set; }
        public MultiPlatformMessenger Messenger { get; set; }
        public IEventBus Bus { get; set; }
        public MetricsService Metrics { get; set; }
        private MetricsContainer _cachedMetrics;

        // Tracks individual profile sync times for throttling
        private readonly ConcurrentDictionary<string, DateTime> _lastSyncTimes = new ConcurrentDictionary<string, DateTime>();

        // Tracks failed API keys to prevent infinite validation loops/toasts
        private readonly ConcurrentDictionary<string, bool> _failedEncryptionKeys = new ConcurrentDictionary<string, bool>();

        // Semaphore to prevent race conditions during entry operations
        // Used in HandleEntry and HandleDraw to ensure atomic state updates
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        // Anti-Loop Protection: Tracks processed message IDs to prevent duplicate processing
        private readonly ConcurrentDictionary<string, DateTime> _processedMsgIds = new ConcurrentDictionary<string, DateTime>();
        //private System.Threading.Timer _msgIdCleanupTimer; // Removed in refactor
        private DateTime _lastMsgCleanup = DateTime.MinValue;
        private DateTime _lastHeartbeat = DateTime.MinValue; // Heartbeat tracker

        // Anti-Loop Protection: Invisible token appended to bot messages for self-detection
        // Uses zero-width space (U+200B) followed by identifier for human-invisible marking
        public const string ANTI_LOOP_TOKEN = "\u200B\u200B";

        public FileLogger Logger { get; set; }


        /// <summary>
        /// Periodic tick handler to check for timed giveaway expirations and periodic tasks.
        /// </summary>
        /// <param name="state">The CPHAdapter instance.</param>
        /// <remarks>
        /// Tasks performed:
        /// 1. Polls for configuration updates (throttled).
        /// 2. Checks active giveaways for Auto-Close times.
        /// 3. Performs Message ID cleanup (throttled).
        /// 4. Logs heartbeat every 5 minutes if active.
        /// </remarks>
        private void LifecycleTick(object state)
        {
            var adapter = state as CPHAdapter;
            if (adapter == null || States == null) return;

            try
            {
                // Poll for dynamic variable updates (Remote Control)
                // Checks "Fast" vars every second, "Heavy" vars every 5 seconds
                // This dramatically reduces log spam while keeping Start/Stop responsive
                _tickCount++;
                bool fullSync = (_tickCount % 5 == 0);

                Task.Run(async () => {
                    try { await CheckForConfigUpdates(adapter, fullSync); }
                    catch { /* Ignore polling errors */ }
                });

                var now = DateTime.UtcNow;
                foreach (var kvp in States)
                {
                    var profileName = kvp.Key;
                    var s = kvp.Value;

                    // Check for Auto-Close
                    if (s.IsActive && s.AutoCloseTime.HasValue && now >= s.AutoCloseTime.Value)
                    {
                        // Ensure config exists
                        if (GlobalConfig != null && GlobalConfig.Profiles.TryGetValue(profileName, out var profile))
                        {
                            // Trigger Close
                            adapter.LogInfo($"[GiveawayManager] [LifecycleTick] Auto-closing giveaway '{profileName}' (Time expired).");
                            // Execute end synchronously on the timer thread is risky if it does heavy I/O,
                            // but HandleEnd is mostly state updates + async dumps.
                            // We need to run it in a way that doesn't block the timer too long.
                            // However, HandleEnd returns Task. We should fire and forget carefully.
                            Task.Run(async () => await HandleEnd(adapter, profile, s, profileName, "Timer"));
                        }
                    }
                }

                // Periodic Message ID Cleanup (Throttled inside LifecycleTick)
                if ((now - _lastMsgCleanup).TotalMilliseconds >= (GlobalConfig?.Globals?.MessageIdCleanupIntervalMs ?? 60000))
                {
                    _lastMsgCleanup = now;
                    CleanupExpiredMessageIds(adapter);
                }

                // Heartbeat Logging (Every 5 minutes)
                if ((now - _lastHeartbeat).TotalMinutes >= 5)
                {
                    _lastHeartbeat = now;
                    int activeCount = States.Values.Count(s => s.IsActive);
                    if (activeCount > 0)
                        adapter.LogVerbose($"[GiveawayManager] [LifecycleTick] Timer active. Active Profiles: {activeCount}");
                }
            }
            catch (Exception ex)
            {
                adapter.LogVerbose($"[GiveawayManager] [LifecycleTick] Tick error: {ex.Message}");
            }
        }



        /// <summary>
        /// Sets up the manager dependencies and loads initial state.
        /// </summary>
        /// <param name="adapter">The CPH adapter for Streamer.bot interaction.</param>
        /// <remarks>
        /// Performs the following initialization steps:
        /// 1. Ensures the Logger is properly attached.
        /// 2. Initializes the Configuration Loader and loads global settings.
        /// 3. Migrates legacy security tokens (API Keys) to the new encryption format.
        /// 4. Instantiates subsystems: EventBus, WheelClient, ObsController, Messenger, MetricsService.
        /// 5. Starts the lifecycle timer for periodic tasks.
        /// 6. Rehydrates persistence state for all known profiles.
        /// 7. Syncs global variables to Streamer.bot.
        /// 8. Starts the incremental dump timer for data safety.
        /// </remarks>
        public void Initialize(CPHAdapter adapter)
        {
            // Logger is already attached to Cph via EnsureInitialized, but we ensure it's in our singleton too
            if (Logger == null)
            {
                if (adapter.Logger != null) Logger = adapter.Logger;
                else Logger = new FileLogger();
            }
            if (adapter.Logger == null) adapter.Logger = Logger;

            adapter.LogTrace("[GiveawayManager] [Initialize] Starting Initialize...");



            _configLoader = new ConfigLoader();
            _configLoader.GenerateDefaultConfig(adapter); // Ensure config file exists for user to edit
            GlobalConfig = _configLoader.GetConfig(adapter);
            if (GlobalConfig == null)
            {
                adapter.LogFatal("[GiveawayManager] [Initialize] GlobalConfig is null! Bot initialization aborted.");
                return;
            }


            adapter.LogDebug($"[GiveawayManager] [Initialize] Primary configuration loaded. Profiles found: {string.Join(", ", GlobalConfig.Profiles.Keys.ToList() ?? new List<string>())}");

            MigrateSecurity(adapter);

            Bus = new GiveawayEventBus();
            WheelClient = new WheelOfNamesClient();
            WheelClient.Metrics = _cachedMetrics; // Enable API latency tracking
            Obs = new ObsController(GlobalConfig);
            Obs.Register(Bus);
            Messenger = new MultiPlatformMessenger(GlobalConfig);
            Messenger.Register(Bus); // Subscribe to events
            Metrics = new MetricsService();
            _cachedMetrics = Metrics.LoadMetrics(adapter);

            // Start lifecycle timer (1s interval)
#if !GIVEAWAY_TESTS
#pragma warning disable CS8622
            _lifecycleTimer = new System.Threading.Timer(LifecycleTick, adapter, 1000, 1000);
#pragma warning restore CS8622
#else
            adapter.LogInfo("[GiveawayManager] [Initialize] Test Environment detected: Lifecycle Timer disabled.");
#endif

            adapter.LogVerbose("[GiveawayManager] [Initialize] Internal services instantiated (Loader, Messenger, WheelClient, Metrics).");

            // Load persisted state for all known profiles to ensure we don't lose data on bot restart
            if (GlobalConfig != null)
            {
                foreach (var profileKey in GlobalConfig.Profiles.Keys)
                {
                    adapter.LogTrace($"[GiveawayManager] [Initialize] Rehydrating state for profile: {profileKey}");
                    var state = PersistenceService.LoadState(adapter, profileKey, GlobalConfig.Globals) ?? new GiveawayState();
                    // Ensure every profile has a unique ID for tracking purposes
                    if (state.CurrentGiveawayId == null)
                    {
                        state.CurrentGiveawayId = Guid.NewGuid().ToString();
                        adapter.LogDebug($"[GiveawayManager] [Initialize] Generated new Giveaway ID for {profileKey}: {state.CurrentGiveawayId}");
                        PersistenceService.SaveState(adapter, profileKey, state, GlobalConfig.Globals, true);
                    }
                    States[profileKey] = state;
                    _lastSyncTimes[profileKey] = DateTime.UtcNow;
                }
            }

            adapter.LogTrace("[GiveawayManager] [Initialize] Syncing variables...");
            adapter.SetGlobalVar(GiveawayConstants.GlobalLogPruneProbability, GlobalConfig.Globals.LogPruneProbability, true);
            adapter.SetGlobalVar(GiveawayConstants.GlobalLogMaxFileSize, GlobalConfig.Globals.LogMaxFileSizeMB, true);
            SyncAllVariables(adapter); // Sync both global and profile variables

            // Start periodic cleanup timer for message ID cache (Threading.Timer)
            // Use adapter as state object
            /*
            // Timer consolidated into LifecycleTick
             _msgIdCleanupTimer = new System.Threading.Timer(
                state => CleanupExpiredMessageIds((CPHAdapter)state),
                adapter,
                TimeSpan.FromMilliseconds(GlobalConfig.Globals.MessageIdCleanupIntervalMs),
                TimeSpan.FromMilliseconds(GlobalConfig.Globals.MessageIdCleanupIntervalMs));
            */

            adapter.LogInfo("[GiveawayManager] [Initialize] Initialization complete. All states rehydrated.");

            adapter.LogDebug($"[GiveawayManager] [Initialize] System Check - Storage Base: {AppDomain.CurrentDomain.BaseDirectory}");

            // Auto-Encrypt/Upgrade API Key if needed
            AutoEncryptApiKey(adapter);

            // Initialize incremental dump timer (checks every 5 seconds)
            _currentAdapter = adapter; // Store for timer callbacks
#if !GIVEAWAY_TESTS
#pragma warning disable CS8622 // Nullability warning (C# 7.3 doesn't have nullable reference types)
            _dumpTimer = new System.Threading.Timer(
                ProcessPendingDumpsCallback,
                null,
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(5)
            );
#pragma warning restore CS8622
            adapter.LogDebug("[GiveawayManager] [Initialize] Incremental dump timer started (5s interval)");
#endif

#if !GIVEAWAY_TESTS
            // Fire and forget update check on startup
            _startupTask = CheckForUpdatesStartup(adapter);
#endif
        }

        private Task _startupTask = null;
        public Task WaitForStartup() => _startupTask ?? Task.CompletedTask;

        private async Task CheckForUpdatesStartup(CPHAdapter adapter)
        {
            // Fire and forget update check on startup
            await UpdateService.CheckForUpdatesAsync(adapter, Version, notifyIfUpToDate: false);
        }

        /// <summary>
        /// Migrates legacy security settings to the current standard.
        /// Generates a portable EncryptionSalt if one does not exist.
        /// Re-encrypts existing API keys using the new salt if necessary.
        /// </summary>
        /// <param name="adapter">CPH Adapter for logging and variable access.</param>
        // TODO: Remove in v2.0 (Legacy Migration)
        private void MigrateSecurity(CPHAdapter adapter)
        {
            if (GlobalConfig?.Globals == null) return;
            var g = GlobalConfig.Globals;

            if (string.IsNullOrEmpty(g.EncryptionSalt))
            {
                adapter.LogInfo("[GiveawayManager] [MigrateSecurity] No Encryption Salt found. Generating new portable salt...");
                g.EncryptionSalt = Guid.NewGuid().ToString("N");

                // Save immediately so valid salt is persisted
                try
                {
                    string json = JsonConvert.SerializeObject(GlobalConfig, Formatting.Indented);
                    _configLoader?.WriteConfigText(adapter, json);
                }
                catch (Exception ex)
                {
                    adapter.LogError($"[GiveawayManager] [MigrateSecurity] Failed to save config during migration: {ex.Message}");
                }

                // Migrate stored API Key from Legacy MachineKey to New Salt
                string currentKey = adapter.GetGlobalVar<string>(g.WheelApiKeyVar, true);
                if (!string.IsNullOrEmpty(currentKey) && currentKey.StartsWith("AES:"))
                {
                    // DecryptSecret will fail with new Salt, fallback to MachineKey -> Success
                    string plain = DecryptSecret(currentKey);
                    if (!string.IsNullOrEmpty(plain))
                    {
                        // EncryptSecret uses the NEW Salt we just set
                        string newCipher = EncryptSecret(plain);
                        if (!string.IsNullOrEmpty(newCipher) && newCipher != currentKey)
                        {
                            adapter.SetGlobalVar(g.WheelApiKeyVar, newCipher, true);
                            adapter.LogInfo("[GiveawayManager] [MigrateSecurity] Existing API Key migrated to portable encryption.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Automatically encrypts or upgrades API keys to AES-256-CBC encryption.
        /// Supports backward-compatible auto-conversion from legacy OBF (Base64) format.
        /// Warns users if legacy DPAPI keys are detected (non-portable).
        /// </summary>
        // TODO: Remove in v2.0 (Legacy Migration)
        private void AutoEncryptApiKey(CPHAdapter adapter)
        {
            try
            {
                string currentKey = adapter.GetGlobalVar<string>(GlobalConfig.Globals.WheelApiKeyVar, true);
                if (string.IsNullOrEmpty(currentKey)) return;

                // Auto-convert OBF → AES (backward compatible upgrade)
                if (currentKey.StartsWith("OBF:"))
                {
                    string plainText = DecryptSecret(currentKey); // Decode Base64
                    if (!string.IsNullOrEmpty(plainText))
                    {
                        string encrypted = EncryptSecret(plainText);
                        if (!string.IsNullOrEmpty(encrypted))
                        {
                            adapter.SetGlobalVar(GlobalConfig.Globals.WheelApiKeyVar, encrypted, true);
                            adapter.LogInfo("[GiveawayManager] [AutoEncryptApiKey] ✅ Auto-converted legacy OBF key to AES-256.");
                            adapter.ShowToastNotification("Giveaway Bot", "API Key upgraded to AES encryption");
                            return;
                        }
                    }
                    adapter.LogWarn("[GiveawayManager] [AutoEncryptApiKey] ⚠ Failed to auto-convert OBF key. Please re-enter plain text API key.");
                    return;
                }

                // Warn about unsupported ENC (DPAPI)
                if (currentKey.StartsWith("ENC:"))
                {
                    adapter.LogWarn("[GiveawayManager] [AutoEncryptApiKey] ⚠ Legacy DPAPI (ENC:) key detected. Cannot decrypt. Please re-enter plain text API key.");
                    adapter.ShowToastNotification("Giveaway Bot", "API Key needs re-entry (unsupported format)");
                    return;
                }

                // Encrypt plain text keys
                if (!currentKey.StartsWith("AES:"))
                {
                    string encrypted = EncryptSecret(currentKey);
                    if (!string.IsNullOrEmpty(encrypted))
                    {
                        adapter.SetGlobalVar(GlobalConfig.Globals.WheelApiKeyVar, encrypted, true);
                        adapter.LogInfo("[Security] 🔒 Encrypted API key with AES-256.");
                    }
                    else
                    {
                        adapter.LogError("[Security] ❌ Failed to encrypt API key. Leaving in plaintext (SECURITY RISK!).");
                    }
                }
            }
            catch (Exception ex)
            {
                // CRITICAL: Sanitize any potential key material from logs
                adapter.LogWarn($"[Security] Encryption check failed: {SanitizeForLog(ex.Message)}");
            }
        }

        /// <summary>
        /// Encrypts a secret using AES-256-CBC.
        /// Uses EncryptionSalt from GlobalConfig if available (portable), else falls back to MachineName (legacy).
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <returns>The encrypted string prefixed with "AES:", or null if input is empty.</returns>
        public static string EncryptSecret(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return null;

            string seed = GlobalConfig?.Globals?.EncryptionSalt;
            if (string.IsNullOrEmpty(seed))
                seed = Environment.MachineName + "GiveawayBot_v2" + Environment.UserName;

            return EncryptWithSeed(plainText, seed);
        }

        /// <summary>
        /// Encrypts a string using AES-256-CBC with a specific seed.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="seed">The seed string to derive the key/IV from.</param>
        /// <returns>The encrypted string in base64 format prefixed with "AES:", or null on failure.</returns>
        private static string EncryptWithSeed(string plainText, string seed)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    // AES Configuration:
                    // KeySize 256: Strongest standard key size.
                    // CBC (Cipher Block Chaining): Each block of data is XORed with the previous one. secure for messages > 1 block.
                    // PKCS7: Adds padding bytes if the data isn't a perfect multiple of the block size.
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Key Derivation:
                    // We take the "Seed" (password) and hash it with SHA256 to get a perfect 32-byte (256-bit) key.
                    // This ensures any length password becomes a valid AES key.
                    using (var sha = SHA256.Create())
                    {
                        aes.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(seed));
                    }

                    // IV (Initialization Vector):
                    // A random starting block. This ensures that encrypting the same text twice results in different output.
                    aes.GenerateIV();

                    using (var encryptor = aes.CreateEncryptor())
                    using (var ms = new MemoryStream())
                    {
                        // Structure of the encrypted data: [IV (16 bytes)] + [Encrypted Content]
                        // We write the IV first so we can read it back during decryption.
                        ms.Write(aes.IV, 0, aes.IV.Length); // Prepend IV

                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }

                        // Convert to Base64 so it can be stored as a string in text files/settings.
                        return "AES:" + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Decrypts AES-256-CBC encrypted secret.
        /// Tries configured EncryptionSalt first, then falls back to legacy MachineName.
        /// Auto-converts legacy OBF: format (Base64) to plaintext.
        /// </summary>
        // TODO: Remove in v2.0 (Legacy Migration)
        public static string DecryptSecret(string secret)
        {
            if (string.IsNullOrEmpty(secret)) return null;

            // Auto-conversion: Legacy OBF: (Base64)
            if (secret.StartsWith("OBF:"))
            {
                try { return Encoding.UTF8.GetString(Convert.FromBase64String(secret.Substring(4))); }
                catch { return null; }
            }

            if (secret.StartsWith("ENC:")) return null; // Legacy DPAPI
            if (!secret.StartsWith("AES:")) return secret; // Not encrypted

            // Try Configured Salt (Portable)
            if (!string.IsNullOrEmpty(GlobalConfig?.Globals?.EncryptionSalt))
            {
                string result = TryDecrypt(secret, GlobalConfig.Globals.EncryptionSalt);
                if (result != null) return result;
            }

            // Try Legacy Machine Key (Fallback)
            return TryDecrypt(secret, Environment.MachineName + "GiveawayBot_v2" + Environment.UserName);
        }

        /// <summary>
        /// Attempts to decrypt the provided secret using the given seed.
        /// Returns null if decryption fails.
        /// </summary>
        private static string TryDecrypt(string secret, string seed)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(secret.Substring(4));
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var sha = SHA256.Create())
                    {
                        aes.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(seed));
                    }

                    // Extract IV
                    byte[] iv = new byte[16];
                    Array.Copy(cipherBytes, 0, iv, 0, 16);
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor())
                    using (var ms = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Sanitizes strings for logging - removes potential key/sensitive material.
        /// Redacts patterns that look like API keys, tokens, or encrypted blobs.
        /// </summary>
        private static string SanitizeForLog(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            // Remove base64-like patterns and encrypted blobs
            input = Regex.Replace(input, @"(AES|OBF|ENC):[A-Za-z0-9+/=]+", "$1:[REDACTED]");
            input = Regex.Replace(input, @"(api[_-]?key|token|secret|password)[:\s=]+\S+", "$1:[REDACTED]", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"[A-Za-z0-9+/=]{40,}", "[REDACTED]");
            return input;
        }
        /// <summary>
        /// Periodically cleans up expired message IDs from the anti-loop cache.
        /// Removes entries older than MESSAGE_ID_CACHE_TTL_MINUTES to prevent memory leak.
        /// Runs every MESSAGE_ID_CLEANUP_INTERVAL_MS milliseconds via timer.
        /// </summary>
        private void CleanupExpiredMessageIds(CPHAdapter adapter)
        {
            // Calculate threshold using configured TTL constant
            var threshold = DateTime.UtcNow.AddMinutes(-GlobalConfig.Globals.MessageIdCacheTtlMinutes);
            var expiredKeys = new List<string>();

            foreach (var kvp in _processedMsgIds)
            {
                if (kvp.Value < threshold)
                    expiredKeys.Add(kvp.Key);
            }

            foreach (var key in expiredKeys)
            {
                _processedMsgIds.TryRemove(key, out _);
            }

            // Update metrics
            if (_cachedMetrics != null)
            {
                _cachedMetrics.MessageIdCacheSize = _processedMsgIds.Count;
                _cachedMetrics.MessageIdCleanupCount++;
            }

            if (expiredKeys.Count > 0)
                adapter.LogTrace($"[AntiLoop] Cleaned up {expiredKeys.Count} expired message IDs. Cache size: {_processedMsgIds.Count}");

            // Sync metrics to global variables after cleanup
            SyncEnhancedMetrics(adapter);
        }

        /// <summary>
        /// Synchronizes enhanced metrics to Streamer.bot global variables.
        /// Exposes diagnostic counters for real-time monitoring in UI.
        /// Optimized to only update variables that have changed to reduce log spam.
        /// </summary>
        private void SyncEnhancedMetrics(CPHAdapter adapter)
        {
            if (_cachedMetrics == null) return;

            void SetMetric<T>(string suffix, T value)
            {
                SetGlobalVarIfChanged(adapter, "Giveaway Global Metrics " + suffix, value);
            }

            SetMetric("Cache Size", _cachedMetrics.MessageIdCacheSize);
            SetMetric("Cleanup Count", _cachedMetrics.MessageIdCleanupCount);
            SetMetric("Loop Detected", _cachedMetrics.LoopDetectedCount);
            SetMetric("Loop By MsgId", _cachedMetrics.LoopDetectedByMsgId);
            SetMetric("Loop By Token", _cachedMetrics.LoopDetectedByToken);
            SetMetric("Loop By BotFlag", _cachedMetrics.LoopDetectedByBotFlag);
            SetMetric("Config Reloads", _cachedMetrics.ConfigReloadCount);
            SetMetric("File IO Errors", _cachedMetrics.FileIOErrors);

            SetMetric("Entries Processed", _cachedMetrics.EntriesProcessedCount);
            SetMetric("Entry Processing Total Ms", _cachedMetrics.TotalEntryProcessingMs);
            // Computed average (avoid division by zero)
            long avgEntryMs = _cachedMetrics.EntriesProcessedCount > 0
                ? _cachedMetrics.TotalEntryProcessingMs / _cachedMetrics.EntriesProcessedCount
                : 0;
            SetMetric(GiveawayConstants.Metric_EntryProcessingAvgMs, avgEntryMs);

            SetMetric(GiveawayConstants.Metric_WinnerDrawAttempts, _cachedMetrics.WinnerDrawAttempts);
            SetMetric(GiveawayConstants.Metric_WinnerDrawSuccesses, _cachedMetrics.WinnerDrawSuccesses);

            SetMetric(GiveawayConstants.Metric_WheelApiCalls, _cachedMetrics.WheelApiCalls);
            SetMetric(GiveawayConstants.Metric_WheelApiTotalMs, _cachedMetrics.WheelApiTotalMs);
            long avgWheelMs = _cachedMetrics.WheelApiCalls > 0
                ? _cachedMetrics.WheelApiTotalMs / _cachedMetrics.WheelApiCalls
                : 0;
            SetMetric(GiveawayConstants.Metric_WheelApiAvgMs, avgWheelMs);

            SetMetric(GiveawayConstants.Metric_ApiErrors, _cachedMetrics.ApiErrors);
            SetMetric(GiveawayConstants.Metric_WheelApiErrors, _cachedMetrics.WheelApiErrors);
            SetMetric(GiveawayConstants.Metric_WheelApiInvalidKeys, _cachedMetrics.WheelApiInvalidKeys);
            SetMetric(GiveawayConstants.Metric_WheelApiTimeouts, _cachedMetrics.WheelApiTimeouts);
            SetMetric(GiveawayConstants.Metric_WheelApiNetworkErrors, _cachedMetrics.WheelApiNetworkErrors);
        }

        /// <summary>
        /// Increments a global metric counter.
        /// </summary>
        /// <param name="adapter">CPH adapter instance.</param>
        /// <param name="n">Metric name suffix (e.g., "Giveaway_Started").</param>
        /// <param name="d">Amount to increment (default: 1).</param>
        private void IncGlobalMetric(CPHAdapter adapter, string n, long d = 1)
        {
            var key = $"Giveaway Global Metrics {n}";
            var v = adapter.GetGlobalVar<long>(key, true);
            var newVal = v + d;
            adapter.SetGlobalVar(key, newVal, true);

            if (_cachedMetrics != null)
            {
                _cachedMetrics.GlobalMetrics[n] = newVal;
                _cachedMetrics.LastUpdated = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Public wrapper for incrementing global metrics (thread-safe).
        /// Allows external classes (e.g., WheelOfNamesClient, ObsController) to track metrics.
        /// Uses Streamer.bot's global variable locking mechanism for thread safety.
        /// </summary>
        /// <param name="adapter">CPH adapter instance (required)</param>
        /// <param name="metricName">Metric name without prefix (e.g., "WheelAPI_Errors")</param>
        /// <param name="delta">Amount to increment (default: 1)</param>
        public static void TrackMetric(CPHAdapter adapter, string metricName, long delta = 1)
        {
            if (adapter == null)
            {
                // Cannot log without adapter, silently return
                return;
            }

            if (string.IsNullOrEmpty(metricName))
            {
                adapter.LogWarn("[Metrics] TrackMetric called with empty metric name.");
                return;
            }

            try
            {
                var key = $"GiveawayBot Metrics {metricName}";
                var currentValue = adapter.GetGlobalVar<long>(key, true);
                var newValue = currentValue + delta;
                adapter.SetGlobalVar(key, newValue, true);

                adapter.LogTrace($"[Metrics] {metricName} incremented: {currentValue} → {newValue} (+{delta})");
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Metrics] Failed to track metric '{metricName}': {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if a message/event should be ignored due to anti-loop protections.
        /// Returns true if the message should be IGNORED (loop detected).
        /// Implements three-layer protection: message ID dedup, invisible token, bot source flag.
        /// </summary>
        /// <param name="adapter">CPH adapter for argument extraction and logging</param>
        /// <param name="reason">Output parameter explaining why loop was detected (if any)</param>
        /// <returns>True if loop detected (ignore message), False if safe to process</returns>
        private bool IsLoopDetected(CPHAdapter adapter, out string reason)
        {
            reason = null;

            // Message ID Deduplication
            // Prevents processing the same Twitch/YouTube message twice
            var msgId = adapter.TryGetArg<string>("msgId", out var id) ? id : null;
            if (!string.IsNullOrEmpty(msgId))
            {
                if (_processedMsgIds.ContainsKey(msgId))
                {
                    reason = "Message ID already processed";
                    if (_cachedMetrics != null)
                    {
                        _cachedMetrics.LoopDetectedCount++;
                        _cachedMetrics.LoopDetectedByMsgId++;
                    }
                    return true; // LOOP DETECTED
                }
                // Add to cache with 5-minute expiry (timer-based cleanup prevents memory leak)
                _processedMsgIds[msgId] = DateTime.UtcNow;
            }

            // Invisible Token Detection
            // Bot appends ANTI_LOOP_TOKEN to all outgoing messages
            // If we see it in an incoming message, it's our own message echoed back
            var message = adapter.TryGetArg<string>("message", out var msg) ? msg : "";
            if (message.Contains(ANTI_LOOP_TOKEN))
            {
                reason = "Bot token detected in message";
                if (_cachedMetrics != null)
                {
                    _cachedMetrics.LoopDetectedCount++;
                    _cachedMetrics.LoopDetectedByToken++;
                }
                return true; // LOOP DETECTED
            }

            // Bot Source Flag (with External Override)
            // Streamer.bot may provide an 'isBot' flag for bot-originated messages
            var isBot = adapter.TryGetArg<bool>("isBot", out var bot) && bot;
            if (isBot)
            {
                // CRITICAL EXCEPTION: Check if this bot is explicitly allowed in ANY profile
                // We must check all profiles because loop detection happens before routing
                adapter.TryGetArg<string>("userId", out _); // Extract but don't assign
                var userName = adapter.TryGetArg<string>("user", out var uname) ? uname : null;

                // Fast check: Is this user in any AllowedExternalBots list?
                bool isAllowed = false;
                if (GlobalConfig != null)
                {
                    foreach (var profile in GlobalConfig.Profiles.Values)
                    {
                        var resolvedBots = ResolveBotList(adapter, profile.AllowedExternalBots);
                        if (resolvedBots != null && resolvedBots.Contains(userName))
                        {
                            isAllowed = true;
                            // Inject a special flag so ProcessTrigger knows to handle this as a bot command
                            adapter.Args["_isAllowedBot"] = true;
                            break;
                        }
                    }
                }

                if (!isAllowed)
                {
                    reason = "Message source flagged as Bot";
                    if (_cachedMetrics != null)
                    {
                        _cachedMetrics.LoopDetectedCount++;
                        _cachedMetrics.LoopDetectedByBotFlag++;
                    }
                    return true; // LOOP DETECTED
                }
                // If allowed, fall through and return false (safe to process)
            }

            return false; // No loop detected - safe to process
        }

        /// <summary>
        /// Checks if the message is from an allowed external bot and triggers actions if it matches a pattern.
        /// Returns TRUE if an action was taken (handled), FALSE otherwise.
        /// </summary>
        private async Task<bool> HandleBotMessage(CPHAdapter adapter)
        {
            // Verify this is marked as an allowed bot (checked in IsLoopDetected)
            if (!adapter.Args.ContainsKey("_isAllowedBot")) return false;

            var userName = adapter.TryGetArg<string>("user", out var u) ? u : "";
            var message = adapter.TryGetArg<string>("message", out var msg) ? msg : "";
            var userId = adapter.TryGetArg<string>("userId", out var uid) ? uid : "";
            var userPlatform = adapter.TryGetArg<string>("userType", out var utype) ? utype : "Twitch";

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(message)) return false;

            // Iterate all profiles to find matching rules for this bot
            bool handled = false;
            foreach (var kvp in GlobalConfig.Profiles)
            {
                var profileName = kvp.Key;
                var config = kvp.Value;

                // Skip if this profile doesn't allow this bot
                var resolvedBots = ResolveBotList(adapter, config.AllowedExternalBots);
                if (resolvedBots == null || !resolvedBots.Contains(userName))
                    continue;

                // Check rules
                if (config.ExternalListeners == null) continue;

                foreach (var rule in config.ExternalListeners)
                {
                    try
                    {
                        if (Regex.IsMatch(message, rule.Pattern, RegexOptions.IgnoreCase))
                        {
                            adapter.LogInfo($"[ExternalBot] Message from '{userName}' matched rule '{rule.Pattern}' -> Action: {rule.Action}");

                            // Execute the mapped action on this profile
                            // Actions: "Enter", "Winner", "Open", "Close"
                            await ExecuteExternalAction(adapter, profileName, rule.Action, userName, userId, message, userPlatform);
                            handled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        adapter.LogError($"[ExternalBot] Rule processing failed for '{rule.Pattern}': {ex.Message}");
                    }
                }
            }

            return handled;
        }

        /// <summary>
        /// Resolves the AllowedExternalBots list from multiple possible sources.
        /// Priority: File Path -> Streamer.bot Variable -> Inline List
        /// </summary>
        /// <param name="adapter">CPH adapter for logging and variable access</param>
        /// <param name="allowedBots">Source list from config (may be file path, variable name, or inline list)</param>
        /// <returns>Resolved list of bot names (case-sensitive)</returns>
        // TODO: Remove in v2.0 (Legacy Migration)
        private static List<string> ResolveBotList(CPHAdapter adapter, List<string> allowedBots)
        {
            if (allowedBots == null || allowedBots.Count == 0) return new List<string>();

            // Single-item list might be a file path or variable name
            if (allowedBots.Count == 1)
            {
                var source = allowedBots[0];

                // Check if it's a file path
                if (File.Exists(source))
                {
                    try
                    {
                        adapter.LogDebug($"[ExternalBot] Loading bot list from file: {source}");
                        var lines = File.ReadAllLines(source);
                        var result = new List<string>();
                        foreach (var line in lines)
                        {
                            var trimmed = line.Trim();
                            if (!string.IsNullOrEmpty(trimmed) && !trimmed.StartsWith("#"))
                            {
                                result.Add(trimmed);
                            }
                        }
                        adapter.LogInfo($"[ExternalBot] Loaded {result.Count} bot(s) from file: {source}");
                        return result;
                    }
                    catch (Exception ex)
                    {
                        adapter.LogError($"[ExternalBot] Failed to read bot list file '{source}': {ex.Message}");
                        return new List<string>();
                    }
                }

                // Check if it's a Streamer.bot variable (starts with "GiveawayBot_" or contains no path separators)
                if (source.StartsWith("GiveawayBot_") || !source.Contains("\\") && !source.Contains("/"))
                {
                    try
                    {
                        var varValue = adapter.GetGlobalVar<string>(source, true);
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            adapter.LogDebug($"[ExternalBot] Loading bot list from variable: {source}");
                            var result = new List<string>();

                            // Support both newline and comma separation
                            var delimiters = new string[] { "\r\n", "\n", "," };
                            var parts = varValue.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var part in parts)
                            {
                                var trimmed = part.Trim();
                                if (!string.IsNullOrEmpty(trimmed))
                                {
                                    result.Add(trimmed);
                                }
                            }
                            adapter.LogInfo($"[ExternalBot] Loaded {result.Count} bot(s) from variable: {source}");
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        adapter.LogError($"[ExternalBot] Failed to read bot list variable '{source}': {ex.Message}");
                    }
                }
            }

            // Fallback: treat as inline list
            adapter.LogTrace($"[ExternalBot] Using inline bot list ({allowedBots.Count} bot(s))");
            return allowedBots.ToList();
        }

        /// <summary>
        /// Executes an external action (like a toast or OBS change) based on a configured event name.
        /// </summary>
        /// <param name="adapter">CPH adapter.</param>
        /// <param name="profileName">Current profile name.</param>
        /// <param name="action">Action name ("Open", "Close", "Winner").</param>
        /// <param name="userName">User triggering the action.</param>
        /// <param name="userId">User ID triggering the action.</param>
        /// <param name="message">Original message content.</param>
        /// <param name="platform">Platform of the trigger.</param>
        private async Task ExecuteExternalAction(CPHAdapter adapter, string profileName, string action, string userName, string userId, string message, string platform)
        {
            if (!States.TryGetValue(profileName, out var state)) return;
            var config = GlobalConfig.Profiles[profileName];

            switch (action.ToLowerInvariant())
            {
                case "open":
                    if (!state.IsActive)
                    {
                        await HandleStart(adapter, config, state, profileName, platform);
                    }
                    break;
                case "close":
                    if (state.IsActive)
                    {
                        await HandleEnd(adapter, config, state, profileName, platform);
                    }
                    break;
                case "winner":
                    await HandleDraw(adapter, config, state, profileName, platform);
                    break;
                case "enter":
                    if (state.IsActive && !string.IsNullOrEmpty(userId))
                    {
                        // Treat as a valid entry attempt with explicit user info
                        await HandleEntry(adapter, config, state, profileName, userId, userName);
                    }
                    else
                    {
                         adapter.LogWarn($"[ExternalBot] 'Enter' action ignored: Giveaway inactive or missing UserID ({userId}).");
                    }
                    break;
                default:
                    adapter.LogWarn($"[ExternalBot] Unknown action '{action}' mapped in ExternalListeners.");
                    break;
            }
        }



        /// <summary>
        /// Validates username OR entry input against configured regex pattern.
        /// Returns true if INVALID (should be rejected).
        /// Supports dual-check: If userName matches, it's valid. If not, checks gameNameInput.
        /// </summary>
        /// <param name="userName">The username to check.</param>
        /// <param name="gameNameInput">Optional game name input to check if username fails.</param>
        /// <param name="config">The profile configuration containing the regex pattern.</param>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="validName">Output parameter for the valid name found (if any).</param>
        /// <returns>True if INVALID (reject), False if Valid (allow)</returns>
        private bool IsEntryNameInvalid(string userName, string gameNameInput, GiveawayProfileConfig config, CPHAdapter adapter, out string validName)
        {
            validName = userName; // Default to userName
            if (string.IsNullOrEmpty(config.UsernameRegex))
            {
                // Validation disabled, but still capture explicit input if provided
                if (!string.IsNullOrEmpty(gameNameInput)) validName = gameNameInput;
                return false;
            }

            try
            {
                // Recompile regex only if pattern changed
                if (_usernameRegex == null || _lastUsernamePattern != config.UsernameRegex)
                {
                    _usernameRegex = new Regex(config.UsernameRegex, RegexOptions.None, TimeSpan.FromMilliseconds(GlobalConfig.Globals.RegexTimeoutMs));
                    _lastUsernamePattern = config.UsernameRegex;
                    adapter.LogTrace($"[Validation] Compiled new regex pattern: '{config.UsernameRegex}'");
                }

                // CHECK 1: Twitch Username
                if (_usernameRegex.IsMatch(userName))
                {
                    // Valid Twitch Name
                    return false;
                }

                // CHECK 2: Input Argument (Game Name)
                if (!string.IsNullOrEmpty(gameNameInput))
                {
                    if (_usernameRegex.IsMatch(gameNameInput))
                    {
                        validName = gameNameInput; // Use the game name
                        return false;
                    }
                }

                // Both failed
                adapter.LogTrace($"[Validation] Entry rejected: '{userName}' (and input '{gameNameInput}') did not match pattern '{config.UsernameRegex}'");
                return true;
            }
            catch (RegexMatchTimeoutException)
            {
                adapter.LogError($"[Validation] Regex timeout: '{config.UsernameRegex}'");
                IncGlobalMetric(adapter, "Validation_RegexTimeouts");
                return false; // Fail-open
            }
            catch (ArgumentException ex)
            {
                adapter.LogError($"[Validation] Invalid regex: '{config.UsernameRegex}' - {ex.Message}");
                return false; // Fail-open
            }
        }

        // Cached regex for username pattern validation (recompiled only when pattern changes)
        private static Regex _usernameRegex = null;
        private static string _lastUsernamePattern = null;



        /// <summary>
        /// Increments a user-specific metric counter, storing it in Streamer.bot's user variables.
        /// Also updates the in-memory cached metrics for real-time display.
        /// </summary>
        /// <param name="adapter">CPH adapter instance.</param>
        /// <param name="userId">The user's ID (e.g., Twitch User ID).</param>
        /// <param name="userName">The user's display name.</param>
        /// <param name="metricKey">The metric key to increment.</param>
        /// <param name="gameName">Optional game name to update.</param>
        private void IncUserMetric(CPHAdapter adapter, string userId, string userName, string metricKey, string gameName = null)
        {
            if (_cachedMetrics != null)
            {
                if (!_cachedMetrics.UserMetrics.TryGetValue(userId, out var uMetric))
                {
                    uMetric = new UserMetricSet { UserName = userName, GameName = gameName };
                    _cachedMetrics.UserMetrics[userId] = uMetric;
                }

                // Update Name/GameName if provided, just in case they changed
                if (!string.IsNullOrEmpty(gameName)) uMetric.GameName = gameName;

                if (!uMetric.Metrics.ContainsKey(metricKey)) uMetric.Metrics[metricKey] = 0;
                uMetric.Metrics[metricKey]++;
            }
        }

        // Sticking with static compiled Regex for .NET Framework 4.8 compatibility.
        // GeneratedRegex is .NET 7+ only, which would break the Streamer.bot internal compiler.
#pragma warning disable SYSLIB1045
        private static readonly Regex _msgIdRegex = new Regex(@"\b[a-zA-Z0-9]{8,12}\b", RegexOptions.Compiled);
        private static readonly Regex _createRegex = new Regex(@"!(?:giveaway|ga)\s+create\s+([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex _deleteRegex = new Regex(@"!(?:giveaway|ga)\s+delete\s+([a-zA-Z0-9_\-]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex _profileCmdRegex = new Regex(@"!(?:giveaway|ga)\s+(?:profile|p)\s+(\w+)(?:\s+(.+))?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
#pragma warning restore SYSLIB1045

        /// <summary>
        /// Synchronizes Streamer.bot variables for ALL profiles and global settings.
        /// Useful after initialization or configuration changes.
        /// Strategy:
        /// 1. Reset 'touched' variables tracker.
        /// 2. Sync Global Settings (RunMode, API Keys, etc.).
        /// 3. Sync Profile Variables (Triggers, Configs, Live Stats).
        /// 4. Prune any variables that were NOT touched (Orphan cleanup).
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        public void SyncAllVariables(CPHAdapter adapter)
        {
            if (GlobalConfig == null) return;
            adapter.LogTrace("[Sync] Starting full variable synchronization and pruning...");

            adapter.ResetTouchedVars();

            SyncGlobalVars(adapter);
            SyncEnhancedMetrics(adapter); // Expose diagnostic metrics
            foreach (var profile in GlobalConfig.Profiles)
            {
                if (States.TryGetValue(profile.Key, out var state))
                {
                    SyncProfileVariables(adapter, profile.Key, profile.Value, state, GlobalConfig.Globals);
                }
                else
                {
                    // If state not loaded for some reason, try now
                    var s = PersistenceService.LoadState(adapter, profile.Key, GlobalConfig.Globals) ?? new GiveawayState();
                    States[profile.Key] = s;
                    SyncProfileVariables(adapter, profile.Key, profile.Value, s, GlobalConfig.Globals);
                }
            }

            // After all known variables are touched, prune the orphans
            PruneUnusedVariables(adapter);
        }

        /// <summary>
        /// Identifies and removes Streamer.bot global variables starting with 'GiveawayBot_'
        /// that were not updated (touched) during the most recent synchronization.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        private static void PruneUnusedVariables(CPHAdapter adapter)
        {
            try
            {
                adapter.LogTrace("[Prune] Auditing global variables for orphans...");
                var allVars = adapter.GetGlobalVarNames(true);
                var managedVars = allVars.Where(CPHAdapter.IsManagedVariable).ToList();
                var touched = adapter.GetTouchedVars().ToHashSet(StringComparer.OrdinalIgnoreCase);
                var toPrune = managedVars.Where(v => !touched.Contains(v)).ToList();

                if (toPrune.Count > 0)
                {
                    adapter.LogInfo(string.Format("[Prune] Audited {0} managed variables. Removing {1} orphans: {2}", managedVars.Count, toPrune.Count, string.Join(", ", toPrune)));
                    foreach (var v in toPrune)
                    {
                        adapter.UnsetGlobalVar(v, true);
                        // Also unset non-persisted version just in case of mirror mismatch
                        adapter.UnsetGlobalVar(v, false);
                    }
                }
                else
                {
                    adapter.LogTrace($"[Prune] No orphans found. All {managedVars.Count} managed variables are currently active.");
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Prune] Variable cleanup failed: {ex.Message}");
            }
        }
        /// <summary>
        /// Exposes global management variables to Streamer.bot for visibility.
        /// Aggregates metrics from all profiles for a comprehensive sync.
        /// Optimized to avoid redundant log spam.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        public void SyncGlobalVars(CPHAdapter adapter)
        {
            if (GlobalConfig == null) return;

            // Only log trace if we haven't synced this batch recently or on first run
            // (Approximated by checking one key var presence in cache)
            if (!_lastSyncedValues.ContainsKey("Giveaway Global Run Mode"))
            {
                adapter.Logger?.LogTrace(adapter, "System", "Syncing Global Configuration Variables...");
            }

            // Touch input overrides so they aren't pruned
            adapter.TouchGlobalVar("Giveaway Global Run Mode");
            adapter.TouchGlobalVar("Giveaway Global Expose Variables");
            // Triggers variable pattern logic handled in CheckForConfigUPdates, but global touch here?

            var g = GlobalConfig.Globals;

            // Sync RunMode & LogLevel (Always set to ensure normalization)
            SetGlobalVarIfChanged(adapter, "Giveaway Global Run Mode", g.RunMode ?? "FileSystem", true);
            string level = string.IsNullOrEmpty(g.LogLevel) ? "INFO" : g.LogLevel.ToUpperInvariant();
            SetGlobalVarIfChanged(adapter, GiveawayConstants.GlobalLogLevel, level, true);
            string currentLevelVar = level;

            // Expose all GlobalSettings fields
            SetGlobalVarIfChanged(adapter, "Giveaway Global Log To Streamer Bot", g.LogToStreamerBot, true);

            SetGlobalVarIfChanged(adapter, "Giveaway Global Wheel Api Key Var", g.WheelApiKeyVar, true);

            // Direct API Key Status Check & Initialization
            // Ensure the direct variable exists so users can see it in valid variable lists
            string keyVal = adapter.GetGlobalVar<string>("Giveaway Global Wheel Api Key");
            if (keyVal == null)
            {
                // Default as Help Text
                adapter.SetGlobalVar("Giveaway Global Wheel Api Key", "Enter Wheel of Names API Key", true);
                keyVal = "Enter Wheel of Names API Key";
            }
            string keyStatus = (string.IsNullOrEmpty(keyVal) || keyVal.StartsWith("Enter ")) ? "Missing" : "Configured";
            SetGlobalVarIfChanged(adapter, "Giveaway Global Wheel Api Key Status", keyStatus, true);

            SetGlobalVarIfChanged(adapter, "Giveaway Global Log Retention Days", g.LogRetentionDays, true);
            SetGlobalVarIfChanged(adapter, "Giveaway Global Log Size Cap MB", g.LogSizeCapMB, true);
            SetGlobalVarIfChanged(adapter, "Giveaway Global Fallback Platform", g.FallbackPlatform ?? "", true);
            SetGlobalVarIfChanged(adapter, "Giveaway Global State Persistence Mode", g.StatePersistenceMode ?? "Both", true);
            SetGlobalVarIfChanged(adapter, "Giveaway Global State Sync Interval Seconds", g.StateSyncIntervalSeconds, true);
            SetGlobalVarIfChanged(adapter, "Giveaway Global Security Toasts", g.EnableSecurityToasts, true);

            if (g.EnabledPlatforms != null)
            {
                SetGlobalVarIfChanged(adapter, GiveawayConstants.GlobalEnabledPlatforms, string.Join(",", g.EnabledPlatforms), true);
            }

            // Sync Root Config Metadata
            if (GlobalConfig.Instructions != null)
                SetGlobalVarIfChanged(adapter, GiveawayConstants.GlobalInstructions, string.Join("\n", GlobalConfig.Instructions), true);
            if (GlobalConfig.TriggerPrefixHelp != null)
                SetGlobalVarIfChanged(adapter, GiveawayConstants.GlobalTriggerPrefixHelp, string.Join("\n", GlobalConfig.TriggerPrefixHelp), true);

            // Auto-Import Globals from Config (e.g., API Keys)
            if (g.ImportGlobals != null)
            {
                foreach (var kvp in g.ImportGlobals)
                {
                    // Only set if missing/empty to prevent overwriting runtime changes
                    var existing = adapter.GetGlobalVar<string>(kvp.Key);
                    if (string.IsNullOrEmpty(existing))
                    {
                        adapter.SetGlobalVar(kvp.Key, kvp.Value, true);
                        adapter.LogInfo($"[Sync] Imported Global Variable from Config: {kvp.Key}");
                    }
                }
            }

            // adapter.Logger?.LogTrace(adapter, "System", $"Global Sync: Mode={ConfigLoader.GetRunMode(adapter)}, LogLevel={currentLevelVar}...");

            // Aggregate Metrics from Memory
            long totalEntries = 0;
            long totalActiveWinners = 0;

            foreach (var state in States.Values)
            {
                totalEntries += state.CumulativeEntries;
                totalActiveWinners += state.WinnerCount;
            }

            // Sync Metrics (Update existing or bootstrap)
            UpdateMetric(adapter, GiveawayConstants.Metric_EntriesTotal, totalEntries);
            UpdateMetric(adapter, GiveawayConstants.Metric_WinnersTotal, totalActiveWinners);

            // Explicitly touch/bootstrap the primary management metrics to ensure they don't get pruned
            UpdateMetric(adapter, GiveawayConstants.Metric_EntriesRejected, 0);
            UpdateMetric(adapter, GiveawayConstants.Metric_ApiErrors, 0);
            UpdateMetric(adapter, GiveawayConstants.Metric_SystemErrors, 0);

            // Extra safety for configuration status and json vars
            adapter.TouchGlobalVar(GiveawayConstants.GlobalConfigStatus);
            adapter.TouchGlobalVar(GiveawayConstants.GlobalLastConfigErrors);
            adapter.TouchGlobalVar(GiveawayConstants.GlobalConfig);
            adapter.TouchGlobalVar(GiveawayConstants.GlobalConfigLastWrite);
            adapter.TouchGlobalVar(GiveawayConstants.GlobalBackupCount);
        }

        /// <summary>
        /// Updates a snapshot metric (e.g., latency, processing time) without incrementing.
        /// Logs a performance warning if latency exceeds thresholds.
        /// </summary>
        /// <param name="adapter">CPH adapter instance.</param>
        /// <param name="name">Metric name suffix (e.g., "EntryProcessingAvgMs").</param>
        /// <param name="value">The new value for the metric.</param>
        private void UpdateMetric(CPHAdapter adapter, string name, long value)
        {
            string key = GiveawayConstants.GlobalMetricsPrefix + name;
            // Use locally cached check instead of GetGlobalVar for better performance and reduced spam
            SetGlobalVarIfChanged(adapter, key, value, true);

            // Performance Warning (Latency)
            if (name == "Entry Processing Avg Ms" && value > 2000)
            {
                adapter.LogWarn($"[Performance] High Latency: Entry Processing took {value}ms (>2000ms threshold).");
            }
        }

        /// <summary>
        /// Persists the current in-memory metrics to storage (file/GlobalVar).
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        public void SaveDirtyMetrics(CPHAdapter adapter)
        {
            if (Metrics != null && _cachedMetrics != null)
            {
                Metrics.SaveMetrics(adapter, _cachedMetrics);
            }
        }

    /// <summary>
    /// Synchronizes profile-specific variables to Streamer.bot global variables.
    /// Handles 'ExposeVariables' logic to selectively show/hide runtime data.
    /// Optimized to avoid redundant log spam.
    /// </summary>
    /// <param name="adapter">The CPH adapter context.</param>
    /// <param name="profileName">The name of the profile to sync.</param>
    /// <param name="config">The profile configuration.</param>
    /// <param name="state">The current runtime state of the profile.</param>
    /// <param name="globals">The global settings.</param>
    private void SyncProfileVariables(CPHAdapter adapter, string profileName, GiveawayProfileConfig config, GiveawayState state, GlobalSettings globals)
    {
        // Always sync the full state JSON for persistence/visibility, regardless of ExposeVariables
        // This is potentially large, so we check diff first
        // HIDDEN from UI (persisted=false) - internal state JSON
        SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileStateBlobSuffix}", JsonConvert.SerializeObject(state), false);

        // Check for global override in variables
        bool? overrideVal = ParseBoolVariant(adapter.GetGlobalVar<string>(GiveawayConstants.GlobalExposeVariables, true));
        string currentRunMode = ConfigLoader.GetRunMode(adapter); // Use authoritative mode (includes overrides)
        bool isMirror = globals.RunMode == "Mirror";
        adapter.LogDebug($"[DEBUG] Sync Check: Mirror={isMirror}, Override={overrideVal}, ConfExpose={config.ExposeVariables}");

        if (isMirror || (overrideVal ?? globals.ExposeVariables ?? config.ExposeVariables))
        {
            adapter.LogDebug($"[DEBUG] Entering Sync Block for {profileName}");

            // Track if config needs to be saved (Mirror mode only)
            bool configDirty = false;

            // --- Helper for Variables with Help Text Defaults ---
            void SyncVar(string keySuffix, object value, object defaultValue, string helpText)
            {
                string fullKey = $"{GiveawayConstants.ProfileVarBase} {profileName} {keySuffix}";

                // If value matches default, we prefer to show the Help Text to document the variable
                bool isDefault = false;
                if (value == null && defaultValue == null) isDefault = true;
                else if (value != null && value.Equals(defaultValue)) isDefault = true;

                if (isDefault && !string.IsNullOrEmpty(helpText))
                {
                    // Enforce Help Text if value is default.
                    // But if checking RunMode Mirror, we overwrite.
                    SetGlobalVarIfChanged(adapter, fullKey, helpText, true);
                }
                else
                {
                    SetGlobalVarIfChanged(adapter, fullKey, value ?? "", true);
                }
            }

            // --- Helper for Bidirectional Sync (Mirror Mode) ---
            // Reads a variable back and returns the updated value if changed externally
            T CheckVarChange<T>(string keySuffix, T currentValue, T defaultValue, Func<string, T> parser = null)
            {
                if (!isMirror) return currentValue; // Only in Mirror mode

                string fullKey = $"{GiveawayConstants.ProfileVarBase} {profileName} {keySuffix}";
                string rawValue = adapter.GetGlobalVar<string>(fullKey, true);

                if (string.IsNullOrEmpty(rawValue)) return currentValue; // No value set

                try
                {
                    T parsedValue;
                    if (parser != null)
                    {
                        parsedValue = parser(rawValue);
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        bool? boolVal = ParseBoolVariant(rawValue);
                        if (!boolVal.HasValue) return currentValue;
                        parsedValue = (T)(object)boolVal.Value;
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        if (!int.TryParse(rawValue, out int intVal)) return currentValue;
                        parsedValue = (T)(object)intVal;
                    }
                    else if (typeof(T) == typeof(decimal))
                    {
                        if (!decimal.TryParse(rawValue, out decimal decVal)) return currentValue;
                        parsedValue = (T)(object)decVal;
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        parsedValue = (T)(object)rawValue;
                    }
                    else
                    {
                        return currentValue; // Unsupported type
                    }

                    // Check if value changed
                    if (!EqualityComparer<T>.Default.Equals(currentValue, parsedValue))
                    {
                        adapter.LogInfo($"[Mirror] Detected external change: {fullKey} = '{parsedValue}' (was '{currentValue}')");
                        configDirty = true;
                        return parsedValue;
                    }

                    return currentValue;
                }
                catch (Exception ex)
                {
                    adapter.LogWarn($"[Mirror] Failed to parse variable {fullKey}: {ex.Message}");
                    return currentValue;
                }
            }

            // Runtime State (Read-Only Status)
            SyncVar(GiveawayConstants.ProfileIsActiveSuffix, state.IsActive, null, null); // Always show bool
            SyncVar("EntryCount", state.Entries.Count, null, null);
            var totalTickets = state.Entries.Values.Sum(e => e.TicketCount);
            SyncVar("TicketCount", totalTickets, null, null);

            // HIDDEN State identifiers
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileGiveawayIdSuffix}", state.CurrentGiveawayId ?? "", false);
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileWinnerNameSuffix}", state.LastWinnerName ?? "", true);
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileWinnerUserIdSuffix}", state.LastWinnerUserId ?? "", true);
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileWinnerCountSuffix}", state.WinnerCount, true);
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileCumulativeEntriesSuffix}", state.CumulativeEntries, true);
            int subCount = state.Entries.Values.Count(e => e.IsSub);
            SetGlobalVarIfChanged(adapter, $"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileSubEntryCountSuffix}", subCount, true);

            // Dynamic Config Exposure (Editable)
            // Use Help Text for defaults

            SyncVar(GiveawayConstants.ProfileTimerDurationSuffix, config.TimerDuration, null, "Enter duration (e.g. 10m, 1h)");
            SyncVar(GiveawayConstants.ProfileMaxEntriesSuffix, config.MaxEntriesPerMinute, 0, "Enter max entries per minute (0=Unlimited)");
            SyncVar(GiveawayConstants.ProfileRequireFollowerSuffix, config.RequireFollower, null, "Require follower? (True/False)");
            SyncVar(GiveawayConstants.ProfileRequireSubscriberSuffix, config.RequireSubscriber, null, "Require subscriber? (True/False)");
            SyncVar(GiveawayConstants.ProfileSubLuckMultiplierSuffix, config.SubLuckMultiplier, 1.0m, "Enter multiplier >= 1.0 (e.g. 1.5)");

            // Wheel & OBS Settings
            SyncVar(GiveawayConstants.ProfileEnableWheelSuffix, config.EnableWheel, false, "Enable Wheel? (True/False)");
            SyncVar(GiveawayConstants.ProfileEnableObsSuffix, config.EnableObs, false, "Enable OBS? (True/False)");
            SyncVar(GiveawayConstants.ProfileObsSceneSuffix, config.ObsScene, null, "Enter OBS Scene Name");
            SyncVar(GiveawayConstants.ProfileObsSourceSuffix, config.ObsSource, null, "Enter OBS Source Name");

            // Wheel of Names Configuration
            if (config.WheelSettings == null) config.WheelSettings = new WheelConfig();
            SyncVar(GiveawayConstants.ProfileWheelTitleSuffix, config.WheelSettings.Title, null, "Enter Wheel Title");
            SyncVar(GiveawayConstants.ProfileWheelDescriptionSuffix, config.WheelSettings.Description, null, "Enter Wheel Description");
            SyncVar(GiveawayConstants.ProfileWheelSpinTimeSuffix, config.WheelSettings.SpinTime, 10000, "Enter spin time in ms (e.g. 6000)");
            SyncVar(GiveawayConstants.ProfileWheelAutoRemoveWinnerSuffix, config.WheelSettings.AutoRemoveWinner, false, "Remove winner after spin? (True/False)");
            SyncVar(GiveawayConstants.ProfileWheelShareModeSuffix, config.WheelSettings.ShareMode, null, "Enter share mode (e.g. 'link')");
            SyncVar(GiveawayConstants.ProfileWheelWinnerMessageSuffix, config.WheelSettings.WinnerMessage, null, "Enter winner message");

            // Dump/Export Settings
            SyncVar(GiveawayConstants.ProfileDumpFormatSuffix, config.DumpFormat.ToString(), "JSON", "JSON|CSV|XML");
            SyncVar(GiveawayConstants.ProfileDumpOnEndSuffix, config.DumpEntriesOnEnd, false, "Dump on end? (True/False)");
            SyncVar(GiveawayConstants.ProfileDumpOnEntrySuffix, config.DumpEntriesOnEntry, false, "Dump on new entry? (True/False)");
            SyncVar(GiveawayConstants.ProfileDumpThrottleSuffix, config.DumpEntriesOnEntryThrottleSeconds, 5, "Enter throttle in seconds (Default: 5)");
            SyncVar(GiveawayConstants.ProfileDumpWinnersSuffix, config.DumpWinnersOnDraw, false, "Dump when winner drawn? (True/False)");
            SyncVar(GiveawayConstants.ProfileDumpSeparateGameNamesSuffix, config.DumpSeparateGameNames, false, "Dump separate game names? (True/False)");

            // Entry Validation
            SyncVar(GiveawayConstants.ProfileUsernameRegexSuffix, config.UsernameRegex, null, "Enter Regex for username validation");
            SyncVar(GiveawayConstants.ProfileMinAccountAgeSuffix, config.MinAccountAgeDays, 0, "Enter min account age in days (0=Disabled)");
            SyncVar(GiveawayConstants.ProfileEnableEntropySuffix, config.EnableEntropyCheck, true, "Check for random keys? (True/False)");
            SyncVar(GiveawayConstants.ProfileWinChanceSuffix, config.WinChance, 1.0, "Enter win chance (0.0 to 1.0)");
            SyncVar(GiveawayConstants.ProfileGameFilterSuffix, config.GameFilter, null, "Enter game name to filter (e.g. 'GW2')");
            SyncVar(GiveawayConstants.ProfileRedemptionCooldownSuffix, config.RedemptionCooldownMinutes, 0, "Enter cooldown in minutes (0=Disabled)");

            // Log trace only if we are actually syncing something relevant
            if (!_lastSyncedValues.ContainsKey($"{GiveawayConstants.ProfileVarBase} {profileName} {GiveawayConstants.ProfileIsActiveSuffix}"))
            {
                adapter.Logger?.LogTrace(adapter, profileName, $"Syncing {profileName} config variables...");
            }

            // --- Bidirectional Sync: Apply Variable Changes to Config (Mirror Mode Only) ---
            if (isMirror)
            {
                // Read back config variables and detect external changes
                config.DumpSeparateGameNames = CheckVarChange(GiveawayConstants.ProfileDumpSeparateGameNamesSuffix, config.DumpSeparateGameNames, false);
                config.DumpEntriesOnEnd = CheckVarChange(GiveawayConstants.ProfileDumpOnEndSuffix, config.DumpEntriesOnEnd, false);
                config.DumpEntriesOnEntry = CheckVarChange(GiveawayConstants.ProfileDumpOnEntrySuffix, config.DumpEntriesOnEntry, false);
                config.DumpWinnersOnDraw = CheckVarChange(GiveawayConstants.ProfileDumpWinnersSuffix, config.DumpWinnersOnDraw, false);
                config.DumpEntriesOnEntryThrottleSeconds = CheckVarChange(GiveawayConstants.ProfileDumpThrottleSuffix, config.DumpEntriesOnEntryThrottleSeconds, 5);
                config.RequireFollower = CheckVarChange(GiveawayConstants.ProfileRequireFollowerSuffix, config.RequireFollower, false);
                config.RequireSubscriber = CheckVarChange(GiveawayConstants.ProfileRequireSubscriberSuffix, config.RequireSubscriber, false);
                config.SubLuckMultiplier = CheckVarChange(GiveawayConstants.ProfileSubLuckMultiplierSuffix, config.SubLuckMultiplier, 1.0m);
                config.EnableWheel = CheckVarChange(GiveawayConstants.ProfileEnableWheelSuffix, config.EnableWheel, false);
                config.EnableObs = CheckVarChange(GiveawayConstants.ProfileEnableObsSuffix, config.EnableObs, false);
                config.EnableEntropyCheck = CheckVarChange(GiveawayConstants.ProfileEnableEntropySuffix, config.EnableEntropyCheck, true);
                config.WinChance = CheckVarChange(GiveawayConstants.ProfileWinChanceSuffix, config.WinChance, 1.0);
                config.MaxEntriesPerMinute = CheckVarChange(GiveawayConstants.ProfileMaxEntriesSuffix, config.MaxEntriesPerMinute, 0);
                config.MinAccountAgeDays = CheckVarChange(GiveawayConstants.ProfileMinAccountAgeSuffix, config.MinAccountAgeDays, 0);
                config.RedemptionCooldownMinutes = CheckVarChange(GiveawayConstants.ProfileRedemptionCooldownSuffix, config.RedemptionCooldownMinutes, 0);

                // String fields
                config.TimerDuration = CheckVarChange(GiveawayConstants.ProfileTimerDurationSuffix, config.TimerDuration, (string)null);
                config.ObsScene = CheckVarChange(GiveawayConstants.ProfileObsSceneSuffix, config.ObsScene, (string)null);
                config.ObsSource = CheckVarChange(GiveawayConstants.ProfileObsSourceSuffix, config.ObsSource, (string)null);
                config.UsernameRegex = CheckVarChange(GiveawayConstants.ProfileUsernameRegexSuffix, config.UsernameRegex, (string)null);
                config.GameFilter = CheckVarChange(GiveawayConstants.ProfileGameFilterSuffix, config.GameFilter, (string)null);

                // Wheel settings
                if (config.WheelSettings != null)
                {
                    config.WheelSettings.Title = CheckVarChange(GiveawayConstants.ProfileWheelTitleSuffix, config.WheelSettings.Title, (string)null);
                    config.WheelSettings.Description = CheckVarChange(GiveawayConstants.ProfileWheelDescriptionSuffix, config.WheelSettings.Description, (string)null);
                    config.WheelSettings.SpinTime = CheckVarChange(GiveawayConstants.ProfileWheelSpinTimeSuffix, config.WheelSettings.SpinTime, 10000);
                    config.WheelSettings.AutoRemoveWinner = CheckVarChange(GiveawayConstants.ProfileWheelAutoRemoveWinnerSuffix, config.WheelSettings.AutoRemoveWinner, false);
                    config.WheelSettings.ShareMode = CheckVarChange(GiveawayConstants.ProfileWheelShareModeSuffix, config.WheelSettings.ShareMode, (string)null);
                    config.WheelSettings.WinnerMessage = CheckVarChange(GiveawayConstants.ProfileWheelWinnerMessageSuffix, config.WheelSettings.WinnerMessage, (string)null);
                }

                // If config changed, save it
                if (configDirty)
                {
                    adapter.LogInfo($"[Mirror] Config changes detected for '{profileName}'. Saving to disk and GlobalVar...");
                    // Get the full config - we already have it in GlobalConfig
                    var fullConfig = GlobalConfig;
                    // Ensure the updated config object is assigned
                    fullConfig.Profiles[profileName] = config;
                    string json = JsonConvert.SerializeObject(fullConfig, Formatting.Indented);
                    var configLoader = new ConfigLoader();
                    configLoader.WriteConfigTextAsync(adapter, json).Wait(); // Sync call acceptable here
                }
            }
        }

        adapter.Logger?.LogTrace(adapter, profileName, $"Sync Complete: {profileName} (Entries: {state.Entries.Count}, Active: {state.IsActive}, Sub Luck: {config.SubLuckMultiplier})");
    }

        /// <summary>
        /// Robustly parses a boolean value from various string representations (true, on, 1, yes, etc.).
        /// Returns null if the input cannot be parsed as a boolean.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>True, False, or null if parsing fails.</returns>
        private static bool? ParseBoolVariant(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            input = input.Trim().ToLowerInvariant();
            if (input == "true" || input == "1" || input == "yes" || input == "on") return true;
            if (input == "false" || input == "0" || input == "no" || input == "off") return false;
            return null;
        }
        /// <summary>
        /// Parses a time duration string (e.g., "10m", "1h", "300s") into integer minutes.
        /// Wraps the existing ParseDuration helper to ensure consistency.
        /// </summary>
        /// <param name="input">The time duration string to parse.</param>
        /// <returns>The duration in minutes, or 0 if parsing fails.</returns>
        private static int ParseDurationMinutes(string input)
        {
            var seconds = ParseDuration(input);
            if (seconds.HasValue)
            {
                return (int)(seconds.Value / 60);
            }
            return 0;
        }

        /// <summary>
        /// Applies game-specific username pattern and entropy settings based on GameFilter config.
        /// Currently supports "GW2" (Guild Wars 2) to enforce account name format (Name.1234).
        /// </summary>
        /// <param name="config">Profile configuration to modify.</param>
        private static void ApplyGameFilter(GiveawayProfileConfig config)
        {
            if (string.IsNullOrEmpty(config.GameFilter)) return;

            string game = config.GameFilter.Trim();
            if (game.Equals("GW2", StringComparison.OrdinalIgnoreCase) ||
                game.Equals("Guild Wars 2", StringComparison.OrdinalIgnoreCase))
            {
                // GW2 Account Name Format: Name using alpha characters, dot, 4 digits.
                config.UsernameRegex = @"^[a-zA-Z]+\.\d{4}$";
                config.EnableEntropyCheck = false; // Structured names have low entropy
            }
            // Add more game filters here as needed
        }


        /// <summary>
        /// Determines if the triggering user is the broadcaster/owner.
        /// Checks against Streamer.bot's `broadcastUserId` argument or Role Level 4 (Broadcaster).
        /// </summary>
        /// <param name="adapter">The CPH Adapter.</param>
        /// <returns>True if the user is the broadcaster, otherwise false.</returns>
        private static bool IsBroadcaster(CPHAdapter adapter)
        {
            if (adapter.TryGetArg<bool>("isBroadcaster", out var isB) && isB) return true;

            // Fallback to ID comparison which is definitive in Streamer.bot 1.x
            adapter.TryGetArg<string>("userId", out var uid);
            adapter.TryGetArg<string>("broadcastUserId", out var buid);

            // Also check role just in case (optional, IDs are better)
            adapter.TryGetArg<int>("role", out var uRole);

            return (!string.IsNullOrEmpty(uid) && uid == buid) || uRole == 4;
        }


        /// <summary>
        /// Handles profile management commands (!giveaway profile ...).
        /// Supports create, delete, clone, listing, and configuration updates.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="rawInput">The full command string.</param>
        /// <param name="platform">The source platform.</param>
        /// <returns>True if the command was handled.</returns>
        public async Task<bool> HandleProfileCommand(CPHAdapter adapter, string rawInput, string platform)
        {
            await Task.Yield();
            // Check RunMode - block in ReadOnlyVar
            string runMode = ConfigLoader.GetRunMode(adapter);
            if (runMode.Equals("ReadOnlyVar", StringComparison.OrdinalIgnoreCase))
            {
                Messenger?.SendBroadcast(adapter, "⚠ Profile management disabled in ReadOnlyVar mode", platform);
                return true;
            }

            var match = _profileCmdRegex.Match(rawInput);
            if (!match.Success)
            {
                Messenger?.SendBroadcast(adapter, "Usage: !ga p <create|delete|clone|list|config|trigger> <args>", platform);
                return true;
            }

            string subcommand = match.Groups[1].Value.ToLower();
            string args = match.Groups.Count > 2 ? match.Groups[2].Value.Trim() : "";

            adapter.LogDebug($"[ProfileCmd] Sub: '{subcommand}', Args: '{args}'");

            switch (subcommand)
            {
                case "start":
                case "open":
                    {
                        // COMMAND: START
                        // Usage: !giveaway profile start <ProfileName|*|all>
                        // Starts a specific profile (or all profiles)
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile start <ProfileName|*|all>", platform);
                            return true;
                        }

                        // Resolve targets (supports comma-separated list or wildcards)
                        var targets = ParseProfileTargets(adapter, parts[0]);
                        if (targets.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ No matching profiles found for '{parts[0]}'", platform);
                            return true;
                        }

                        foreach (var name in targets)
                        {
                            if (GlobalConfig.Profiles.TryGetValue(name, out var pConfig) && States.TryGetValue(name, out var pState))
                            {
                                await HandleStart(adapter, pConfig, pState, name, platform);
                            }
                        }
                        return true;
                    }

                case "end":
                case "close":
                    {
                        // COMMAND: END
                        // Usage: !giveaway profile end <ProfileName|*|all>
                        // Ends/Closes a specific profile (or all profiles)
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile end <ProfileName|*|all>", platform);
                            return true;
                        }

                        var targets = ParseProfileTargets(adapter, parts[0]);
                        if (targets.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ No matching profiles found for '{parts[0]}'", platform);
                            return true;
                        }

                        foreach (var name in targets)
                        {
                            if (GlobalConfig.Profiles.TryGetValue(name, out var pConfig) && States.TryGetValue(name, out var pState))
                            {
                                await HandleEnd(adapter, pConfig, pState, name, platform);
                            }
                        }
                        return true;
                    }

                case "create":
                    {
                        // COMMAND: CREATE
                        // Usage: !giveaway profile create <ProfileName>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile create <ProfileName>", platform);
                            return true;
                        }

                        string profileName = parts[0];
                        // ConfigLoader handles default creation and validation
                        var (created, createError) = await _configLoader.CreateProfileAsync(adapter, profileName);
                        if (created)
                        {
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Profile '{profileName}' created! Use '!giveaway profile config {profileName} ...' to configure.", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Create failed: {createError}", platform);
                        }
                        break;
                    }

                case "update":
                    // COMMAND: UPDATE
                    // Checks for bot updates via GitHub Releases
                    if (!IsBroadcaster(adapter))
                    {
                         // Security: Only broadcaster can trigger updates
                        Messenger?.SendBroadcast(adapter, "⛔ Only the broadcaster can update the bot.", platform);
                        return true;
                    }
                    Messenger?.SendBroadcast(adapter, "🔄 Checking GitHub for updates...", platform);
                    // Use internal UpdateService
                    await UpdateService.CheckForUpdatesAsync(adapter, Version, true);
                    return true;

                case "reset":
                    {
                        // COMMAND: RESET
                        // Usage: !giveaway profile reset <ProfileName> confirm
                        // Resets a profile to default settings (destroys custom config)
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Safety: Require 'confirm' keyword to prevent accidents
                        if (parts.Length < 2 || !parts[1].Equals("confirm", StringComparison.OrdinalIgnoreCase))
                        {
                            string profileName = parts.Length > 0 ? parts[0] : "<name>";
                            Messenger?.SendBroadcast(adapter, $"⚠ To reset '{profileName}', add 'confirm': !giveaway profile reset {profileName} confirm", platform);
                            return true;
                        }

                        var (reset, resetError) = await _configLoader.ResetProfileAsync(adapter, parts[0]);
                        if (reset)
                        {
                            // Reload config to reflect reset
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Profile '{parts[0]}' reset to default settings.", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Reset failed: {resetError}", platform);
                        }
                        break;
                    }

                case "delete":
                    {
                        // COMMAND: DELETE
                        // Usage: !giveaway profile delete <ProfileName> confirm
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Safety: Require 'confirm' keyword
                        if (parts.Length < 2 || !parts[1].Equals("confirm", StringComparison.OrdinalIgnoreCase))
                        {
                            string profileName = parts.Length > 0 ? parts[0] : "<name>";
                            Messenger?.SendBroadcast(adapter, $"⚠ To delete '{profileName}', add 'confirm': !giveaway profile delete {profileName} confirm", platform);
                            return true;
                        }

                        // Per Guidelines: Delete should create a backup before removal
                        var (deleted, deleteError, backupPath) = await _configLoader.DeleteProfileAsync(adapter, parts[0]);
                        if (deleted)
                        {
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Profile '{parts[0]}' deleted. Backup: {backupPath}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Delete failed: {deleteError}", platform);
                        }
                        break;
                    }

                case "clone":
                    {
                        // COMMAND: CLONE
                        // Usage: !giveaway profile clone <SourceProfile> <NewProfileName>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile clone <SourceProfile> <NewProfileName>", platform);
                            return true;
                        }

                        var (cloned, cloneError) = await _configLoader.CloneProfileAsync(adapter, parts[0], parts[1]);
                        if (cloned)
                        {
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Cloned '{parts[0]}' → '{parts[1]}'", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Clone failed: {cloneError}", platform);
                        }
                        break;
                    }

                case "list":
                    {
                        // COMMAND: LIST
                        // Displays all available profiles
                        var currentConfig = GlobalConfig;
                        if (currentConfig?.Profiles == null || currentConfig.Profiles.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, "No profiles configured", platform);
                            return true;
                        }

                        string profileList = string.Join(", ", currentConfig.Profiles.Keys);
                        Messenger?.SendBroadcast(adapter, $"Profiles ({currentConfig.Profiles.Count}): {profileList}", platform);
                        break;
                    }
                case "config":
                    {
                        // COMMAND: CONFIG
                        // Usage: !giveaway profile config <ProfileName|*|all> <Key>=<Value> or <Key> <Value>
                        // Updates a specific setting for one or more profiles
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile config <Target> <Key>=<Value> or <Key> <Value>", platform);
                            return true;
                        }

                        string target = parts[0];
                        string key, val;

                        // Parse Key-Value pair (supports "Key=Value" and "Key Value")
                        if (parts.Length == 2 && parts[1].Contains("="))
                        {
                            var kv = parts[1].Split(new[] { '=' }, 2);
                            key = kv[0];
                            val = kv[1];
                        }
                        else if (parts.Length >= 3)
                        {
                            key = parts[1];
                            val = parts[2];
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile config <Target> <Key>=<Value>", platform);
                            return true;
                        }

                        // Resolve targets
                        var profiles = ParseProfileTargets(adapter, target);
                        if (profiles.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ No profiles matched '{target}'", platform);
                            return true;
                        }

                        int successCount = 0;
                        string lastError = "";

                        // Apply updates individually to each matched profile
                        foreach (var profileName in profiles)
                        {
                            var (updated, updateError) = await _configLoader.UpdateProfileConfigAsync(adapter, profileName, key, val);
                            if (updated) successCount++;
                            else lastError = updateError;
                        }

                        if (successCount > 0)
                        {
                            // Refresh Config
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Updated {successCount} profile(s). Set {key}={val}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Config update failed: {lastError}", platform);
                        }
                        break;
                    }

                case "export":
                case "exp":
                    {
                        // COMMAND: EXPORT
                        // Usage: !giveaway profile export <ProfileName>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile export <ProfileName>", platform);
                            return true;
                        }

                        var (success, msg, path) = await _configLoader.ExportProfileAsync(adapter, parts[0]);
                        if (success)
                        {
                           // Sanitize path to prevent leaking user directory structure in chat
                            Messenger?.SendBroadcast(adapter, $"✅ Exported '{parts[0]}' to: {SanitizePath(path)}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Export failed: {SanitizePath(msg)}", platform);
                        }
                        break;
                    }

                case "import":
                case "imp":
                    {
                        // COMMAND: IMPORT
                        // Usage: !giveaway profile import <FileOrJson> [NewProfileName]
                        // Supports importing from a file (relative to Import folder) or raw JSON

                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile import <FileOrJson> [NewProfileName]", platform);
                            return true;
                        }

                        string source = parts[0];
                        string targetName = parts.Length > 1 ? parts[1] : null;

                        var (success, msg) = await _configLoader.ImportProfileAsync(adapter, source, targetName);
                        if (success)
                        {
                             // Refresh Config after import
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Import successful: {SanitizePath(msg)}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Import failed: {SanitizePath(msg)}", platform);
                        }
                        break;
                    }

                case "trigger":
                    {
                        // COMMAND: TRIGGER
                        // Manage event triggers dynamically
                        // Usage: !giveaway profile trigger <ProfileName> add <type:value> <Action>
                        // Usage: !giveaway profile trigger <ProfileName> remove <type:value>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 3)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile trigger <ProfileName> add <trigger> <action> | remove <trigger>", platform);
                            return true;
                        }

                        string profileName = parts[0];
                        string operation = parts[1].ToLower();
                        string triggerSpec = parts[2];

                        string errorMessage;

                        if (operation == "add")
                        {
                            if (parts.Length < 4)
                            {
                                Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile trigger <ProfileName> add <trigger> <action>", platform);
                                return true;
                            }

                            string action = parts[3];
                            var debugCfg = _configLoader.GetConfig(adapter);
                            adapter.LogDebug($"[ProfileCmd] Trigger Add: Profile='{profileName}', Spec='{triggerSpec}', Action='{action}'. Available Profiles: {string.Join(", ", debugCfg.Profiles.Keys)}");

                            var (triggerAdded, addError) = await _configLoader.AddProfileTriggerAsync(adapter, profileName, triggerSpec, action);
                            errorMessage = addError;
                            if (triggerAdded)
                            {
                                adapter.LogDebug($"[ProfileCmd] Trigger Add SUCCESS for '{profileName}'");
                                GlobalConfig = _configLoader.GetConfig(adapter);
                                if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                                Messenger?.SendBroadcast(adapter, $"✅ Added trigger to {profileName}: {triggerSpec} → {action}", platform);
                            }
                            else
                            {
                                adapter.LogDebug($"[ProfileCmd] Trigger Add FAILED for '{profileName}': {errorMessage}");
                                Messenger?.SendBroadcast(adapter, $"⚠ Add trigger failed: {errorMessage}", platform);
                            }
                        }
                        else if (operation == "remove")
                        {
                            var (triggerRemoved, removeError) = await _configLoader.RemoveProfileTriggerAsync(adapter, profileName, triggerSpec);
                            errorMessage = removeError;
                            if (triggerRemoved)
                            {
                                GlobalConfig = _configLoader.GetConfig(adapter);
                                if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                                Messenger?.SendBroadcast(adapter, $"✅ Removed trigger from {profileName}: {triggerSpec}", platform);
                            }
                            else
                            {
                                Messenger?.SendBroadcast(adapter, $"⚠ Remove trigger failed: {errorMessage}", platform);
                            }
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"Unknown operation '{operation}'. Use 'add' or 'remove'", platform);
                        }
                        break;
                    }

                default:
                    Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile <create|delete|clone|list|config|trigger> <args>", platform);
                    break;
            }

            return true;
        }


        /// <summary>
        /// Retrieves the WheelOfNames API Key, prioritizing the direct global variable.
        /// Returns null if no valid key is found (strict mode).
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <returns>The API Key string or null.</returns>
        private static string GetWheelApiKey(CPHAdapter adapter)
        {
            // Try Direct Variable (New Standard)
            string directKey = adapter.GetGlobalVar<string>("Giveaway Global WheelApiKey");
            if (!string.IsNullOrEmpty(directKey)) return directKey;

            // No Fallback
            return null;
        }



        /// <summary>
        /// Periodic check for configuration updates from Streamer.bot Global Variables.
        /// This allows users to update Triggers via JSON blobs which then sync back to disk.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="fullSync">If true, performs expensive checks (Triggers, API Key validation). False for lightweight polling.</param>
        /// <remarks>
        /// Logic Flow:
        /// 1. Auto-Encrypts API Keys if found in plaintext.
        /// 2. Updates Global Variable Status indicators (Missing/Configured).
        /// 3. Syncs global settings (RunMode, LogLevel).
        /// 4. Iterates all profiles to sync Triggers/Messages (if fullSync).
        /// 5. Checks for dynamic Timer Duration updates and adjusts active timers in real-time.
        /// 6. (Mirror Mode) Syncs profile settings from global variables to memory.
        /// </remarks>
        // Checks global variables for changes and updates internal config if needed.
        // Core component of Bidirectional Config Sync (Mirror Mode).
        private async Task CheckForConfigUpdates(CPHAdapter adapter, bool fullSync = true)
        {
            if (_isDisposed) return;
            if (GlobalConfig?.Profiles == null) return;
    bool dirty = false;

            // Dynamic Global Settings Check (API Key Status)
            if (fullSync && GlobalConfig.Globals != null)
            {
                 // Check status: Direct > Indirect
                 string directKey = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalWheelApiKey);

                // TODO: Future Roadmap - Support additional encryption methods.
                // Auto-Encryption Logic: If key is plain text (not empty, not ENC:), validate and encrypt it.
                if (!string.IsNullOrEmpty(directKey) && !directKey.StartsWith("ENC:"))
                {
                    // FIX: Ignore default placeholder to prevent spamming validation errors
                    if (directKey.Equals("Enter Wheel of Names API Key", StringComparison.OrdinalIgnoreCase))
                    {
                        // Do nothing - user hasn't configured it yet
                    }
                    else if (_failedEncryptionKeys.ContainsKey(directKey))
                    {
                        // Do nothing - we already tried and failed to validate this specific key
                    }
                    else
                    {
                            // Validate first to ensure we don't encrypt garbage.
                            // We use the WheelOfNamesClient to perform a lightweight "GetWheels" call to verify the key.
                            bool? isValid = await new WheelOfNamesClient().ValidateApiKey(adapter, directKey);
                            if (isValid == true)
                            {
                                // Validation passed: Encrypt the key using AES-256-CBC with the bot's seed.
                                // The prefix "AES:" tells the bot this is an encrypted secret that needs decryption before use.
                                string encrypted = GiveawayManager.EncryptSecret(directKey);
                                if (!string.IsNullOrEmpty(encrypted))
                                {
                                    // Update the global variable immediately so the user doesn't see the plaintext key anymore.
                                    adapter.SetGlobalVar(GiveawayConstants.GlobalWheelApiKey, encrypted, true);
                                    adapter.LogInfo("🔒 API Key validated and encrypted successfully.");
                                    directKey = encrypted; // Update local var for status check
                                }
                            }
                        else if (isValid == false)
                        {
                            // Validation failed - mark this key as failed so we don't spam the API or user with toasts
                            _failedEncryptionKeys.TryAdd(directKey, true);
                            adapter.LogDebug($"[Config] Marked invalid API key as failed to prevent re-validation loop: {directKey.Substring(0, Math.Min(5, directKey.Length))}...");
                        }
                    }
                }

                 string indirectKey = null;

                 if (string.IsNullOrEmpty(directKey))
                 {
                     // Fallback check
                     string keyName = GlobalConfig.Globals.WheelApiKeyVar ?? "Wheel Of Names Api Key";
                     indirectKey = adapter.GetGlobalVar<string>(keyName);

                     // Check for common error: Key in Name Field
                     if (Guid.TryParse(keyName, out _) || keyName.Length > 50)
                     {
                         if (string.IsNullOrEmpty(indirectKey)) indirectKey = "ERROR: Key in Name Field!";
                     }
                 }

                 string expectedStatus = "Missing";
                 if (!string.IsNullOrEmpty(directKey)) expectedStatus = "Configured (Direct)";
                 else if (!string.IsNullOrEmpty(indirectKey)) expectedStatus = "Configured (Indirect)";
                 if (indirectKey == "ERROR: Key in Name Field!") expectedStatus = "ERROR: Key in Name Field!";

                 string currentStatus = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalWheelApiKeyStatus);

                 if (!string.Equals(currentStatus, expectedStatus, StringComparison.Ordinal))
                 {
                     adapter.SetGlobalVar(GiveawayConstants.GlobalWheelApiKeyStatus, expectedStatus, true);
                 }


                 // RunMode
                 string runModeVal = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalRunMode, true);
                 if (!string.IsNullOrEmpty(runModeVal) && runModeVal != GlobalConfig.Globals.RunMode)
                 {
                     GlobalConfig.Globals.RunMode = runModeVal;
                     dirty = true;
                     adapter.LogInfo($"[Config] Run Mode updated to '{runModeVal}' via Global Variable.");
                 }

                 // LogLevel
                 string logLevelVal = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalLogLevel, true);
                 if (!string.IsNullOrEmpty(logLevelVal))
                 {
                     string normalized = logLevelVal.ToUpperInvariant();
                     if (normalized != GlobalConfig.Globals.LogLevel)
                     {
                         GlobalConfig.Globals.LogLevel = normalized;
                         dirty = true;
                         adapter.LogInfo($"[Config] Log Level updated to '{normalized}' via Global Variable.");
                     }
                 }

                 // FallbackPlatform
                 string fallbackVal = adapter.GetGlobalVar<string>("Giveaway Global Fallback Platform", true);
                 if (!string.IsNullOrEmpty(fallbackVal) && fallbackVal != GlobalConfig.Globals.FallbackPlatform)
                 {
                     GlobalConfig.Globals.FallbackPlatform = fallbackVal;
                     dirty = true;
                     adapter.LogInfo($"[Config] Fallback Platform updated to '{fallbackVal}' via Global Variable.");
                 }

                 // SecurityToasts
                 string secToastVal = adapter.GetGlobalVar<string>("Giveaway Security Toasts", true);
                 if (!string.IsNullOrEmpty(secToastVal))
                 {
                     bool? secToastBool = ParseBoolVariant(secToastVal);
                     if (secToastBool.HasValue && secToastBool.Value != GlobalConfig.Globals.EnableSecurityToasts)
                     {
                         GlobalConfig.Globals.EnableSecurityToasts = secToastBool.Value;
                         dirty = true;
                         adapter.LogInfo($"[Config] Security Toasts updated to '{secToastBool.Value}' via Global Variable.");
                     }
                 }
            }


            foreach (var kvp in GlobalConfig.Profiles)
            {
                var name = kvp.Key;
                var profile = kvp.Value;
                try
                {
                    if (fullSync)
                    {
                        // Check triggers variable for 2-way sync
                        string varName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileTriggersSuffix}";
                        string triggersJson = adapter.GetGlobalVar<string>(varName, true);
                        if (!string.IsNullOrEmpty(triggersJson))
                        {
                            // Optimization: Skip deserialization if JSON hasn't changed
                            if (_triggersJsonCache.TryGetValue(name, out var cachedJson) && cachedJson == triggersJson)
                            {
                                continue;
                            }

                            try
                            {
                                var incoming = JsonConvert.DeserializeObject<Dictionary<string, string>>(triggersJson);
                                if (incoming != null)
                                {
                                    // Cache the new JSON since it parsed successfully
                                    _triggersJsonCache[name] = triggersJson;

                                    // Compare with current
                                    var current = profile.Triggers ?? new Dictionary<string, string>();
                                    bool equal = incoming.Count == current.Count;
                                    if (equal)
                                    {
                                        foreach (var triggerKvp in incoming)
                                        {
                                            if (!current.TryGetValue(triggerKvp.Key, out var existingVal) || existingVal != triggerKvp.Value)
                                            {
                                                equal = false;
                                                break;
                                        }
                                    }
                                }

                                if (!equal)
                                {
                                    adapter.LogInfo("[Sync] Triggers for profile '" + name + "' updated via Global Variable.");
                                    // Replace triggers manually to preserve case-insensitive instance
                                    profile.Triggers.Clear();
                                    foreach (var kt in incoming) profile.Triggers[kt.Key] = kt.Value;
                                    dirty = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string err = $"[Sync] Invalid JSON in variable '{varName}': {ex.Message}";
                            adapter.LogWarn(err);
                            adapter.SetGlobalVar("Giveaway Bot Last Config Errors", err, true);
                        }
                    }

                    // Check Messages variable for 2-way sync
                    string msgVarName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileMessagesSuffix}";
                    string msgJson = adapter.GetGlobalVar<string>(msgVarName, true);
                    if (!string.IsNullOrEmpty(msgJson))
                    {
                        // Optimization: Check cache
                        string cacheKey = name + "_msgs";
                        if (!_triggersJsonCache.TryGetValue(cacheKey, out var cachedMsgJson) || cachedMsgJson != msgJson)
                        {
                            try
                            {
                                var incomingMsgs = JsonConvert.DeserializeObject<Dictionary<string, string>>(msgJson);
                                if (incomingMsgs != null)
                                {
                                    _triggersJsonCache[cacheKey] = msgJson;
                                    var currentMsgs = profile.Messages ?? new Dictionary<string, string>();

                                    // Simple count check or key check is insufficient, need content check
                                    // But strictly speaking, if JSON changed, we should probably just update.
                                    // The cache check handles the "no change" case efficiently.

                                    profile.Messages = incomingMsgs;
                                    dirty = true;
                                    adapter.LogInfo($"[Config] Detected external update to Messages for profile '{name}'.");
                                }
                            }
                            catch (Exception ex)
                            {
                                adapter.LogWarn($"[Config] Failed to parse Messages JSON for '{name}': {ex.Message}");
                            }
                        }
                    }
                    } // End fullSync block

                // Dynamic Timer Duration Check
                // Only allow dynamic variable syncing in Mirror or GlobalVar mode
                string currentMode = GlobalConfig.Globals.RunMode;
                bool allowSync = (currentMode == "Mirror" || currentMode == "GlobalVar");

                if (allowSync)
                {
                    string timerVarName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileTimerDurationSuffix}";
                    string timerVal = adapter.GetGlobalVar<string>(timerVarName, true);

                    // Normalize nulls
                    if (string.IsNullOrWhiteSpace(timerVal)) timerVal = null;
                    else timerVal = timerVal.Trim();

                    // Check for change
                    if (timerVal != profile.TimerDuration)
                    {
                        string oldVal = profile.TimerDuration;
                        profile.TimerDuration = timerVal;
                        dirty = true;
                        adapter.LogInfo($"[Config] Timer Duration for '{name}' updated from '{oldVal}' to '{timerVal}' via Global Variable.");

                        // Dynamic Runtime Adjustment
                        if (States.TryGetValue(name, out var state) && state.IsActive && state.StartTime.HasValue)
                        {
                        if (TryParseDurationSafe(timerVal, out int newDurationSec, adapter) && newDurationSec > 0)
                        {
                            // Timer Duration Logic:
                            // If the duration string (e.g., "10m") changes at runtime, we assume the user wants to
                            // Reset/Extend the timer relative to NOW.
                            // We accept the new duration as "Time Remaining" rather than "Total Time".
                            var newEndTime = DateTime.Now.AddSeconds(newDurationSec);
                            state.AutoCloseTime = newEndTime;

                            var remaining = newEndTime - DateTime.Now;
                            string timeStr = $"{(int)remaining.TotalMinutes}m {(int)remaining.Seconds}s";

                            string msg = Loc.Get("Timer Updated", name, timeStr);
                            // Broadcast update to chat
                            Messenger?.SendBroadcast(adapter, msg, GlobalConfig.Globals.FallbackPlatform);
                            adapter.LogInfo($"[Timer] Updated runtime auto-close to {newEndTime} (Ends in {timeStr})");
                        }
                        else
                        {
                            // Timer removed/invalid - Switch to manual
                            state.AutoCloseTime = null;
                            if (!string.IsNullOrWhiteSpace(timerVal))
                                adapter.LogWarn($"[Timer] Invalid duration '{timerVal}' - disabling runtime timer.");
                            else
                                adapter.LogInfo($"[Timer] Runtime timer disabled (Manual close only).");
                        }

                        }
                    }

                    if (fullSync)
                    {
                    // --- Dynamic Variable Updates ---

                    // MaxEntriesPerMinute (Validation: >= 0)
                    string maxEntriesVarName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileMaxEntriesSuffix}";
                    string maxEntriesVal = adapter.GetGlobalVar<string>(maxEntriesVarName, true);
                    if (int.TryParse(maxEntriesVal, out int newMaxEntries) && newMaxEntries >= 0)
                    {
                        if (newMaxEntries != profile.MaxEntriesPerMinute)
                        {
                            adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Max Entries Per Minute for '{name}' updated: {profile.MaxEntriesPerMinute} -> {newMaxEntries}");
                            profile.MaxEntriesPerMinute = newMaxEntries;
                            dirty = true;
                        }
                    }


                    //  Require Follower (Validation: bool)
                    syncBool("Require Follower", v => { if(profile.RequireFollower != v) { profile.RequireFollower = v; dirty = true; adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Require Follower for '{name}' updated to {v}"); } });

                    //  Require Subscriber (Validation: bool)
                    syncBool("Require Subscriber", v => { if(profile.RequireSubscriber != v) { profile.RequireSubscriber = v; dirty = true; adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Require Subscriber for '{name}' updated to {v}"); } });

                    // Sub Luck Multiplier (Validation: >= 1.0)
                    string subLuckVarName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileSubLuckMultiplierSuffix}";
                    string subLuckVal = adapter.GetGlobalVar<string>(subLuckVarName, true);
                    if (decimal.TryParse(subLuckVal, out decimal newSubLuck) && newSubLuck >= 1.0m)
                    {
                        if (newSubLuck != profile.SubLuckMultiplier)
                        {
                            adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Sub Luck Multiplier for '{name}' updated: {profile.SubLuckMultiplier} -> {newSubLuck}");
                            profile.SubLuckMultiplier = newSubLuck;
                            dirty = true;
                        }
                    }

                    // --- Variable System Overhaul (Mirror Mode) ---
                    // Helper Actions for clean sync code (C# 7.3)
                    // Transforms "Key_Name" to "Giveaway {name} Key Name"
                    void syncStr(string vSuffix, Action<string> setter, string ignoreHelpText = null)
                    {
                        // Replace underscores with spaces for the lookup key
                        string cleanSuffix = vSuffix.Replace('_', ' ');
                        string val = adapter.GetGlobalVar<string>($"{GiveawayConstants.ProfileVarBase} {name} {cleanSuffix}", true);
                        if (val != null)
                        {
                            // If the value matches the generic Help Text, do NOT import it as configuration
                            if (ignoreHelpText != null && val.Trim().Equals(ignoreHelpText, StringComparison.OrdinalIgnoreCase)) return;
                            setter(val);
                        }
                    }
                    void syncBool(string vSuffix, Action<bool> setter)
                    {
                        string cleanSuffix = vSuffix.Replace('_', ' ');
                        bool? val = ParseBoolVariant(adapter.GetGlobalVar<string>($"{GiveawayConstants.ProfileVarBase} {name} {cleanSuffix}", true));
                        if (val.HasValue) setter(val.Value);
                    }
                    void syncInt(string vSuffix, Action<int> setter)
                    {
                        string cleanSuffix = vSuffix.Replace('_', ' ');
                         if (int.TryParse(adapter.GetGlobalVar<string>($"{GiveawayConstants.ProfileVarBase} {name} {cleanSuffix}", true), out int val) && val >= 0) setter(val);
                    }

                    // Wheel Configuration
                    syncBool("Enable Wheel", v => { if(profile.EnableWheel != v) { profile.EnableWheel = v; dirty = true; adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Enable Wheel updated for '{name}'."); } });

                    if (profile.WheelSettings == null) profile.WheelSettings = new WheelConfig();
                    syncStr("Wheel Settings Title", v => { if(profile.WheelSettings.Title != v) { profile.WheelSettings.Title = v; dirty = true; } }, "Enter Wheel Title");
                    syncStr("Wheel Settings Description", v => { if(profile.WheelSettings.Description != v) { profile.WheelSettings.Description = v; dirty = true; } }, "Enter Wheel Description");
                    syncStr("Wheel Settings Winner Message", v => { if(profile.WheelSettings.WinnerMessage != v) { profile.WheelSettings.WinnerMessage = v; dirty = true; } }, "Enter winner message");
                    syncInt("Wheel Settings Spin Time", v => { if(v > 0 && profile.WheelSettings.SpinTime != v) { profile.WheelSettings.SpinTime = v; dirty = true; } });
                    syncBool("Wheel Settings Auto Remove Winner", v => { if(profile.WheelSettings.AutoRemoveWinner != v) { profile.WheelSettings.AutoRemoveWinner = v; dirty = true; } });
                    syncStr("Wheel Settings Share Mode", v => { if(profile.WheelSettings.ShareMode != v) { profile.WheelSettings.ShareMode = v; dirty = true; } }, "Enter share mode (e.g. 'link')");

                    // OBS Configuration
                    syncBool("Enable OBS", v => { if(profile.EnableObs != v) { profile.EnableObs = v; dirty = true; adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Enable OBS updated for '{name}'."); } });
                    syncStr("Obs Scene", v => { if(profile.ObsScene != v) { profile.ObsScene = v; dirty = true; } }, "Enter OBS Scene Name");
                    syncStr("Obs Source", v => { if(profile.ObsSource != v) { profile.ObsSource = v; dirty = true; } }, "Enter OBS Source Name");

                    // Validation & Filters
                    syncStr("Username Regex", v => { if(profile.UsernameRegex != v) { profile.UsernameRegex = v; dirty = true; } }, "Enter Regex for username validation");
                    syncStr("Game Filter", v => { if(profile.GameFilter != v) { profile.GameFilter = v; dirty = true; } }, "Enter game name to filter (e.g. 'GW2')");
                    syncInt("Min Account Age Days", v => { if(profile.MinAccountAgeDays != v) { profile.MinAccountAgeDays = v; dirty = true; } });

                    // Enhanced Redemption Cooldown Parsing
                    syncStr("Redemption Cooldown Minutes", v => {
                        int mins = ParseDurationMinutes(v);
                        if (profile.RedemptionCooldownMinutes != mins) {
                            profile.RedemptionCooldownMinutes = mins;
                            dirty = true;
                        }
                    }, "Enter cooldown in minutes (0=Disabled)");
                    syncBool("Enable Entropy Check", v => { if(profile.EnableEntropyCheck != v) { profile.EnableEntropyCheck = v; dirty = true; } });

                    // WinChance
                    string winChanceVar = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileWinChanceSuffix}";
                    string winChanceVal = adapter.GetGlobalVar<string>(winChanceVar, true);
                    if (double.TryParse(winChanceVal, out double newChance) && newChance >= 0 && newChance <= 1.0)
                    {
                        if (Math.Abs(newChance - profile.WinChance) > 0.0001) { profile.WinChance = newChance; dirty = true; }
                    }

                    // Dump Config
                    string dumpFmtVar = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileDumpFormatSuffix}";
                    string dumpFmtVal = adapter.GetGlobalVar<string>(dumpFmtVar, true);
                    if (!string.IsNullOrEmpty(dumpFmtVal) && Enum.TryParse(dumpFmtVal, true, out DumpFormat newFmt))
                    {
                        if (profile.DumpFormat != newFmt) { profile.DumpFormat = newFmt; dirty = true; }
                    }
                    syncBool("Dump Entries On End", v => { if(profile.DumpEntriesOnEnd != v) { profile.DumpEntriesOnEnd = v; dirty = true; } });
                    syncBool("Dump Entries On Entry", v => { if(profile.DumpEntriesOnEntry != v) { profile.DumpEntriesOnEntry = v; dirty = true; } });
                    syncBool("Dump Winners On Draw", v => { if(profile.DumpWinnersOnDraw != v) { profile.DumpWinnersOnDraw = v; dirty = true; } });
                    syncInt("Dump Entries On Entry Throttle Seconds", v => { if(profile.DumpEntriesOnEntryThrottleSeconds != v) { profile.DumpEntriesOnEntryThrottleSeconds = v; dirty = true; } });

                    }
                }
                }
                catch (Exception ex)
                {
                    adapter.LogTrace($"[Sync] Error checking updates for {name}: {ex.Message}");
                }

                if (fullSync)
                {
                // Check Individual Message Variables (Granular 2-Way Sync)
                // Checks Giveaway {name} Msg {key}
                foreach (var key in Loc.Keys)
                {
                    string varName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileMsgPrefix}{key}";
                    string varValue = adapter.GetGlobalVar<string>(varName, true);

                    // If variable exists
                    if (varValue != null)
                    {
                        if (profile.Messages == null) profile.Messages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                        // Update if different from current config
                        if (!profile.Messages.TryGetValue(key, out var currentVal) || currentVal != varValue)
                        {
                            profile.Messages[key] = varValue;
                            dirty = true;
                            adapter.LogInfo($"[GiveawayManager] [CheckForConfigUpdates] Detected external update to Message '{key}' for profile '{name}'.");
                        }
                    }
                }

                    } // End fullSync block

                // Dynamic IsActive Check (Remote Start/Stop)
                if (States.TryGetValue(name, out var profileState))
                {
                    // RACE CONDITION PREVENTION:
                    // When we start/end a giveaway locally, we update the state first.
                    // However, the global variable sync might lag slightly or run concurrently.
                    // We interpret the 'active' variable as a remote command.
                    // To avoid the bot seeing its own status update as a "New Command", we skip this check
                    // if we are currently performing an operation on this profile.
                    if (_currentOperation != null &&
                        (_currentOperation == $"START:{name}" || _currentOperation == $"END:{name}"))
                    {
                        adapter.LogTrace($"[Sync] Skipping remote detection for '{name}' - operation in progress: {_currentOperation}");
                        continue;
                    }

                    string activeVarName = $"{GiveawayConstants.ProfileVarBase} {name} {GiveawayConstants.ProfileIsActiveSuffix}";
                    string activeValStr = adapter.GetGlobalVar<string>(activeVarName, true);
                    if (!string.IsNullOrEmpty(activeValStr))
                    {
                        bool? activeVal = ParseBoolVariant(activeValStr);
                        if (activeVal.HasValue)
                        {
                            if (activeVal.Value && !profileState.IsActive)
                            {
                                adapter.LogInfo($"[System] Remote START detected for profile '{name}' via global variable.");
                                await HandleStart(adapter, profile, profileState, name, "System");
                            }
                            else if (!activeVal.Value && profileState.IsActive)
                            {
                                adapter.LogInfo($"[System] Remote END detected for profile '{name}' via global variable.");
                                await HandleEnd(adapter, profile, profileState, name, "System", true); // Bypass auth
                            }
                        }
                    }
                }
            }

            if (dirty)
            {
                adapter.LogInfo("[Sync] Configuration changes detected from Global Variables. Saving to disk...");
                var newJson = JsonConvert.SerializeObject(GlobalConfig, Formatting.Indented);
                await _configLoader.WriteConfigTextAsync(adapter, newJson);
                _configLoader.InvalidateCache();
                // ProcessTrigger will pick up the new config in the very next block via GetConfig()
            }
            else if (fullSync)
            {
                // Verify polling is working without spamming unless tracing
                adapter.LogTrace($"[Sync] Config check complete. No changes detected across {GlobalConfig.Profiles.Count} profiles.");
            }
        }

        /// <summary>
        /// Processes a trigger event (Chat, Command, etc.) from Streamer.bot.
        /// This is the main entry point for all giveaway interactions.
        /// </summary>
        /// <param name="adapter">CPH adapter containing the event arguments.</param>
        /// <returns>True if processed successfully, False otherwise.</returns>
        /// <remarks>
        /// Execution Flow:
        /// 1. Anti-Loop/Bot Protection: Checks for self-triggering or loops.
        /// 2. Config Sync: refreshing configuration from global variables if needed.
        /// 3. Trigger Identification: Determines which Profile and Action (Start, Enter, etc.) matches the event.
        /// 4. Global Command Handling: Processes system-wide commands (!giveaway create, delete, etc.) if no profile matched.
        /// 5. Profile Action Dispatch: Delegates to specific handlers (HandleEntry, HandleStart, etc.) based on the identified action.
        /// </remarks>
        public async Task<bool> ProcessTrigger(CPHAdapter adapter)
        {
            adapter.LogTrace("[ProcessTrigger] >>> Method entered.");

            // Dump detailed args if Trace is enabled (for deep debugging of triggers)
            if (GlobalConfig?.Globals?.LogLevel == "TRACE")
            {
                try
                {
                    var debugArgs = new List<string>();
                    foreach(var key in adapter.Args.Keys)
                    {
                        // Mask sensitive keys
                        if (key.ToLower().Contains("token") || key.ToLower().Contains("key"))
                            debugArgs.Add($"{key}: [REDACTED]");
                        else
                            debugArgs.Add($"{key}: {adapter.Args[key]}");
                    }
                    adapter.LogTrace($"[Trigger] Context ({debugArgs.Count}): {string.Join(", ", debugArgs)}");
                }
                catch (Exception ex)
                {
                    adapter.LogTrace($"[Trigger] Context Dump Failed: {ex.Message}");
                }
            }
            string profileName = null, action = null, platform, sourceDetails;
            try
            {
                // Safety checks for initialization
                if (_configLoader == null)
                {
                    adapter.LogWarn("[ProcessTrigger] Terminating: ConfigLoader is null.");
                    return true;
                }

                // =========================================================================================
                // ANTI-LOOP PROTECTION & BOT DETECTION
                // Consolidates checks for self-loops, external bot allows, and message IDs.
                // =========================================================================================

                // Implements 3-layer protection:
                // 1. Message ID Deduplication (Prevent processing same msg twice)
                // 2. Bot Token/User Check (Prevent responding to known bots unless allowed)
                // 3. Self-Check (Prevent bot from triggering itself via broadcast)
                if (IsLoopDetected(adapter, out var loopReason))
                {
                     // Loop Detected: Log trace and exit to prevent spam or infinite recursion
                    adapter.LogTrace($"[AntiLoop] Ignoring trigger: {loopReason}");
                    return true; // Exit early
                }

                // Check if this is an allowed external bot message that needs processing
                if (await HandleBotMessage(adapter))
                {
                    return true; // Handled by bot listener logic
                }


                // =========================================================================================
                // CONFIGURATION SYNC
                // Ensure we have the latest rules before processing.
                // =========================================================================================
                // Check if the Config File has changed on disk (e.g. edited manually or by another instance)
                var newConfig = _configLoader.GetConfig(adapter);
                if (newConfig != null)
                {
                    if (newConfig != GlobalConfig)
                    {
                        adapter.LogDebug("[ProcessTrigger] Configuration change detected via GetConfig.");
                        GlobalConfig = newConfig;
                        IncGlobalMetric(adapter, "Config_Reloads");
                        if (Messenger != null) Messenger.Config = GlobalConfig;
                        SyncAllVariables(adapter); // Sync whenever config changes
                    }
                }

                // Check for updates from Global Variables (2-Way Sync)
                // Must happen AFTER GetConfig to ensure we apply partial updates to the LATEST config
                // and don't overwrite the global master JSON with stale data.
                await CheckForConfigUpdates(adapter);
                // =========================================================================================
                // TRIGGER IDENTIFICATION
                // Analyze the arguments to determine which profile and action map to this event.
                // Precedence: Command Name > Argument Pattern > Regex Match
                // =========================================================================================
                (profileName, action, platform, sourceDetails) = TriggerInspector.IdentifyTrigger(adapter, GlobalConfig?.Profiles);

                adapter.LogTrace($"[ProcessTrigger] IdentifyTrigger Result: Profile={profileName ?? "null"}, Action={action ?? "null"}, Platform={platform ?? "null"}");

                // Auto-generate config if a system command is used and config is missing
                // This handles the "First Run" experience
                if (GlobalConfig == null && sourceDetails.Contains("!giveaway"))
                {
                    bool isManagementCmd = CheckMgmt(sourceDetails);
                    if (isManagementCmd)
                    {
                        if (IsBroadcaster(adapter))
                        {
                            _configLoader.GenerateDefaultConfig(adapter);
                            GlobalConfig = _configLoader.GetConfig(adapter);

                            SyncAllVariables(adapter);

                            // Re-identify now that we have profiles
                            (profileName, action, platform, sourceDetails) = TriggerInspector.IdentifyTrigger(adapter, GlobalConfig.Profiles);
                        }
                    }
                }

                if (GlobalConfig == null) return true;

                // Handle global utility commands (like generating default config) if no profile matched
                if (profileName == null)
                {
                    // GLOBAL COMMANDS HANDLER
                    adapter.TryGetArg<string>("rawInput", out var rawInput);
                    adapter.LogTrace($"[Trigger] Global Command Check: '{rawInput}'");
                    if (rawInput != null)
                    {
                        // Profile management commands (!giveaway profile ...)
                        if (CheckProfileCmd(rawInput) || CheckProfileCmd(sourceDetails))
                        {
                            // Security: Enforce Broadcaster permissions
                            if (!IsBroadcaster(adapter))
                            {
                                adapter.TryGetArg<string>("user", out var user);
                                adapter.LogWarn($"[Security] Unauthorized profile management attempt by {user}: {rawInput}");
                                if (GlobalConfig.Globals.EnableSecurityToasts)
                                {
                                    adapter.ShowToastNotification("Giveaway Bot - Security", $"⛔ Unauthorized profile access: {user}");
                                }
                                return true;
                            }

                            return await HandleProfileCommand(adapter, rawInput, platform);
                        }
                        // Data Deletion Commands (!giveaway data ...)
                        if (CheckDataCmd(rawInput) || CheckDataCmd(sourceDetails))
                        {
                             return await HandleDataDeletion(adapter, rawInput, platform);
                        }

                        // Permissions Check for Global Admin Commands
                        bool isManagementCmd = CheckMgmt(rawInput);

                        if (isManagementCmd)
                        {
                            if (!IsBroadcaster(adapter))
                            {
                                adapter.TryGetArg<string>("user", out var user);
                                adapter.LogWarn($"[Security] Unauthorized management attempt by {user}: {rawInput}");

                                if (GlobalConfig.Globals.EnableSecurityToasts)
                                {
                                    adapter.ShowToastNotification("Giveaway Bot - Security", $"⛔ Unauthorized admin attempt: {user}");
                                }
                                return true;
                            }
                        }

                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_ConfigGen) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_ConfigGen))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_ConfigGen}");
                            _configLoader.GenerateDefaultConfig(adapter);
                            GlobalConfig = _configLoader.GetConfig(adapter); // Refresh local cache immediately

                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, "Config generated!", platform ?? "Twitch");
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_ConfigCheck) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_ConfigCheck))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_ConfigCheck}");
                            _configLoader.InvalidateCache(); // Force fresh reload from disk/global
                            var report = _configLoader.ValidateConfig(adapter);
                            adapter.LogInfo($"[Config] Logic Check: {report}");

                            // Refresh the local cache so SyncAllVariables uses the NEW values
                            GlobalConfig = _configLoader.GetConfig(adapter);

                            SyncAllVariables(adapter);

                            Messenger?.SendBroadcast(adapter, "Report: " + report, platform ?? "Twitch");
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_SystemTest) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_SystemTest))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_SystemTest}");
                            await PerformSystemCheck(adapter);
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_RegexTest) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_RegexTest))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_RegexTest}");
                            await HandleRegexTest(adapter, rawInput, platform);
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_Create) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_Create))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_Create}");
                            var match = _createRegex.Match(rawInput);
                            if (!match.Success) { Messenger?.SendBroadcast(adapter, "Usage: !giveaway create <name>", platform ?? "Twitch"); return true; }

                            var (created, createError) = await _configLoader.CreateProfileAsync(adapter, match.Groups[1].Value);
                            if (created)
                            {
                                GlobalConfig = _configLoader.GetConfig(adapter); // Refresh local cache immediately
                                if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                                SyncAllVariables(adapter);
                                Messenger?.SendBroadcast(adapter, $"Profile '{match.Groups[1].Value}' created! Run '!giveaway config check' to verify.", platform ?? "Twitch");
                            }
                            else
                            {
                                Messenger?.SendBroadcast(adapter, $"⚠ Create failed: {createError}", platform ?? "Twitch");
                            }
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_Delete) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_Delete))
                        {
                            adapter.LogTrace($"[Trigger] Matched: {GiveawayConstants.Cmd_Delete}");
                            var match = _deleteRegex.Match(rawInput);
                            if (!match.Success) { Messenger?.SendBroadcast(adapter, "Usage: !giveaway delete <name> confirm", platform ?? "Twitch"); return true; }

                            // Safety check: require "confirm" in message
                            if (!rawInput.Contains("confirm") && !sourceDetails.Contains("confirm"))
                            {
                                Messenger?.SendBroadcast(adapter, $"⚠ To delete '{match.Groups[1].Value}', add 'confirm': !giveaway delete {match.Groups[1].Value} confirm", platform ?? "Twitch");
                                return true;
                            }

                            var (deleted, deleteError, backupPath) = await _configLoader.DeleteProfileAsync(adapter, match.Groups[1].Value);
                            if (deleted)
                            {
                                GlobalConfig = _configLoader.GetConfig(adapter);
                                if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                                SyncAllVariables(adapter);
                                Messenger?.SendBroadcast(adapter, $"✅ Profile '{match.Groups[1].Value}' deleted. Backup: {backupPath}", platform ?? "Twitch");
                            }
                            else
                            {
                                Messenger?.SendBroadcast(adapter, $"⚠ Delete failed: {deleteError}", platform);
                            }
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_Stats) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_Stats))
                        {
                            return await HandleStatsCommand(adapter, rawInput, platform);
                        }
                        if (CheckCmd(rawInput, GiveawayConstants.Cmd_Update) || CheckCmd(sourceDetails, GiveawayConstants.Cmd_Update))
                        {
                            if (!IsBroadcaster(adapter))
                            {
                                Messenger?.SendBroadcast(adapter, "⛔ Only the broadcaster can update the bot.", platform);
                                return true;
                            }
                            Messenger?.SendBroadcast(adapter, "🔄 Checking GitHub for updates...", platform);
                            await UpdateService.CheckForUpdatesAsync(adapter, Version, true);
                            return true;
                        }
                    }
                    return true;
                }

                // Ensure configuration exists for the identified profile
                if (!GlobalConfig.Profiles.TryGetValue(profileName, out var profileConfig)) return true;

                // Ensure state is loaded for the identified profile
                if (!States.TryGetValue(profileName, out var profileState))
                {
                    profileState = PersistenceService.LoadState(adapter, profileName, GlobalConfig.Globals) ?? new GiveawayState { CurrentGiveawayId = Guid.NewGuid().ToString() };
                    States[profileName] = profileState;
                }

                if (action == null) return true;

                // Execute the requested action
                // Execute the requested action
                if (action.Equals(GiveawayConstants.Action_Enter, StringComparison.OrdinalIgnoreCase))
                {
                     return await HandleEntry(adapter, profileConfig, profileState, profileName);
                }
                else if (action.Equals(GiveawayConstants.Action_Winner, StringComparison.OrdinalIgnoreCase))
                {
                     return await HandleDraw(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                }
                else if (action.Equals(GiveawayConstants.Action_Open, StringComparison.OrdinalIgnoreCase))
                {
                     return await HandleStart(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                }
                else if (action.Equals(GiveawayConstants.Action_Close, StringComparison.OrdinalIgnoreCase))
                {
                     return await HandleEnd(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                }
                else
                {
                    adapter.LogWarn($"[{profileName}] Unknown action: {action}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"Action execution error ({profileName}:{action}): {ex.Message}");
                return false;
            }
            finally
            {
                SaveDirtyMetrics(adapter);
            }
        }


        private static bool CheckProfileCmd(string s) => s != null && (s.Contains(GiveawayConstants.CmdPattern_GiveawayPrefix + GiveawayConstants.CmdPrefix_Profile) || s.Contains(GiveawayConstants.CmdPattern_GaPrefix + GiveawayConstants.CmdPrefix_Profile) || s.Contains(GiveawayConstants.CmdPattern_GiveawayPrefix + GiveawayConstants.CmdPrefix_ProfileShort) || s.Contains(GiveawayConstants.CmdPattern_GaPrefix + GiveawayConstants.CmdPrefix_ProfileShort));
        private static bool CheckMgmt(string s) => s != null && (CheckCmd(s, GiveawayConstants.CmdPrefix_Config) || CheckCmd(s, GiveawayConstants.CmdPrefix_System) || CheckCmd(s, GiveawayConstants.Cmd_Create) || CheckCmd(s, GiveawayConstants.Cmd_Delete));
        private static bool CheckCmd(string s, string sub) => s != null && (s.Contains(GiveawayConstants.CmdPattern_GiveawayPrefix + sub) || s.Contains(GiveawayConstants.CmdPattern_GaPrefix + sub));

        /// <summary>
        /// Handles a user entering the giveaway.
        /// Performs validation (spam, filters, cooldowns) and updates the state.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="config">Profile configuration.</param>
        /// <param name="state">Current giveaway state.</param>
        /// <param name="profileName">Name of the profile.</param>
        /// <param name="explicitUserId">Optional: User ID injected from external source (bypasses CPH args).</param>
        /// <param name="explicitUserName">Optional: User Name injected from external source.</param>
        /// <returns>True if the entry was processed (accepted or rejected with reason).</returns>

        private async Task<bool> HandleEntry(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string explicitUserId = null, string explicitUserName = null)
        {
            var stopwatch = Stopwatch.StartNew(); // Track entry processing time
            adapter.LogTrace($"[HandleEntry] >>> Starting entry for profile {profileName}");

            // Resolve UserID: Explicit -> CPH Arg
            string userId = explicitUserId;
            if (string.IsNullOrEmpty(userId))
            {
                if (!adapter.TryGetArg<string>("userId", out userId))
                {
                    adapter.LogWarn($"[HandleEntry] Abandoned: Missing 'userId' in trigger args for {profileName}");
                    return true;
                }
            }

            // Resolve UserName: Explicit -> CPH Arg
            string userName = explicitUserName;
            if (string.IsNullOrEmpty(userName))
            {
                if (!adapter.TryGetArg<string>("user", out userName))
                {
                    adapter.LogWarn($"[HandleEntry] Abandoned: Missing 'user' (name) in trigger args for {profileName}");
                    return true;
                }
            }

            // Resolve Platform: CPH Arg -> Default "Twitch"
            // We need this early for platform-specific validation (e.g. Followers)
            string platform = "Twitch";
            if (adapter.TryGetArg<string>("platform", out var detectedPlatform) && !string.IsNullOrEmpty(detectedPlatform))
                platform = detectedPlatform;


            // Username/GameName Pattern Check
            // Extract arguments as potential Game Name
            string gameNameInput = null;
            if (adapter.TryGetArg<string>("rawInput", out var rawInput) && !string.IsNullOrEmpty(rawInput))
            {
                // Simple heuristic: Take everything after first space if it starts with !
                var parts = rawInput.Split(new[] { ' ' }, 2);
                if (parts.Length > 1) gameNameInput = parts[1].Trim();
                else if (!rawInput.StartsWith("!")) gameNameInput = rawInput.Trim(); // Just text
            }

            string validName = userName;
            string finalEntryName = userName; // Default

            // Perform Regex Validation (if configured)
            if (IsEntryNameInvalid(userName, gameNameInput, config, adapter, out validName))
            {
                IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);

                if (config.ToastNotifications.TryGetValue("Entry Rejected", out var notify) && notify)
                         adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Rejected_Pattern", userName));

                Messenger?.SendReply(adapter, Loc.Get("EntryRejected_GW2Pattern", userName), platform, userName);
                return true; // Reject and return
            }
            finalEntryName = validName;

            // Separate validation logic to helper method to reduce complexity
            if (!ValidateEntryRequest(adapter, config, state, profileName, userId, userName, platform))
            {
                return true;
            }

            // Lock critical section to ensure thread-safe entry addition
            await _lock.WaitAsync();
            try
            {
                // Validate giveaway is open and user hasn't already entered
                if (!state.IsActive)
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Giveaway not active (User: {userName})");
                    IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                    return true;
                }
                if (state.Entries.ContainsKey(userId))
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Duplicate (User: {userName})");
                    IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                    return true;
                }

                adapter.TryGetArg<bool>("isSubscribed", out var isSub);
                // Ticket Calculation Logic:
                // Base tickets = 1.
                // If user is Subscriber AND SubLuckMultiplier > 1.0, apply multiplier.
                // We use Ceiling to ensure even a partial multiplier gives at least an extra ticket if > 1.0.
                // Example: Multiplier 1.5 -> 1.5 -> Ceiling(1.5) = 2 tickets.
                int tickets = 1;
                if (isSub && config.SubLuckMultiplier > 1.0m)
                {
                    tickets = (int)Math.Ceiling(tickets * config.SubLuckMultiplier);
                }

                adapter.LogDebug($"[{profileName}] Ticket Calculation for {userName} (Sub={isSub}): Base=1 * Multiplier={(isSub ? config.SubLuckMultiplier : 1.0m)} = {tickets}");

                // Create entry with ticket calculation based on sub status
                state.Entries[userId] = new Entry
                {
                    UserId = userId,
                    UserName = userName,
                    GameName = finalEntryName != userName ? finalEntryName : null,
                    IsSub = isSub,
                    EntryTime = DateTime.Now,
                    TicketCount = tickets
                };
                state.CumulativeEntries++;

                // Enqueue for incremental dump if enabled
                if (config.DumpEntriesOnEntry)
                {
                    state.PendingDumps.Enqueue(state.Entries[userId]);
                    adapter.LogTrace($"[{profileName}] Queued entry for incremental dump: {userName}");
                }


                // Persist state (Throttled for entries)
                // Writing to disk on every single entry can be slow and cause IO contention.
                // We typically sync only if the interval (e.g., 5 seconds) has passed.
                // Critical state changes (Start/End/Winner) always happen immediately.
                bool needsSync = true;
                if (_lastSyncTimes.TryGetValue(profileName, out var lastSync))
                {
                    if ((DateTime.Now - lastSync).TotalSeconds < GlobalConfig.Globals.StateSyncIntervalSeconds)
                    {
                        needsSync = false;
                    }
                }

                if (needsSync)
                {
                    PersistenceService.SaveState(adapter, profileName, state, GlobalConfig.Globals);
                    _lastSyncTimes[profileName] = DateTime.Now;
                }

                SyncProfileVariables(adapter, profileName, config, state, GlobalConfig.Globals);
                IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesTotal);
                adapter.LogInfo($"[{profileName}] Accepted: {userName} (Tickets: {tickets})");

                IncUserMetric(adapter, userId, userName, GiveawayConstants.Metric_EntriesTotalUser, state.Entries[userId].GameName);

                // Platform is already resolved at start of method

                // Extract message ID for threaded replies
                string msgId = null;
                adapter.TryGetArg<string>("msgId", out msgId);

                if (Bus != null)
                {
                    Bus.Publish(new EntryAcceptedEvent(adapter, profileName, state, state.Entries[userId], platform, msgId));
                }
                else
                {
                    string acceptedMsg = Loc.Get("EntryAccepted", profileName, tickets, config.SubLuckMultiplier);
                    if (isSub && config.SubLuckMultiplier == 0) acceptedMsg = Loc.Get("EntryAccepted_NoLuck", profileName, tickets);

                    // Send entry confirmation message to chat
                    Messenger?.SendBroadcast(adapter, acceptedMsg, platform);

                    if (config.ToastNotifications != null && config.ToastNotifications.TryGetValue("EntryAccepted", out var notify) && notify)
                         adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_NewEntry", userName));
                }

                return true;
            }
            catch (Exception ex)
            {
                adapter.Logger?.LogError(adapter, profileName, "HandleEntry Failed", ex);
                IncGlobalMetric(adapter, GiveawayConstants.Metric_SystemErrors);
                return true;
            }
            finally
            {
                _lock.Release();

                // Track entry processing time
                stopwatch.Stop();
                if (_cachedMetrics != null)
                {
                    _cachedMetrics.TotalEntryProcessingMs += stopwatch.ElapsedMilliseconds;
                    _cachedMetrics.EntriesProcessedCount++;
                }
            }
        }

        /// <summary>
        /// Performs all pre-lock validation checks for an entry request.
        /// Includes Game Filters, Cooldowns, Rate Limits, Strictness Checks (Follower/Sub), Regex, Entropy, and Account Age.
        /// </summary>
        /// <param name="adapter">The CPHAdapter instance for interacting with Streamer.bot.</param>
        /// <param name="config">The configuration for the specific giveaway profile.</param>
        /// <param name="state">The current state of the giveaway for the profile.</param>
        /// <param name="profileName">The name of the giveaway profile.</param>
        /// <param name="userId">The ID of the user attempting to enter.</param>
        /// <param name="userName">The name of the user attempting to enter.</param>
        /// <param name="platform">The platform from which the entry request originated (e.g., "Twitch").</param>
        /// <returns>True if the entry request is valid and should proceed to locking/state checks. False if rejected.</returns>
        private bool ValidateEntryRequest(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string userId, string userName, string platform)
        {
            // VALIDATION PIPELINE
            // The order of these checks matters for performance and logic.
            // 1. Game Filter (Fastest, modifies config)
            // 2. Cooldown (Fast, state lookup)
            // 3. Strictness (Platform checks)
            // 4. Rate Limit (Math)
            // 5. Regex (Expensive text processing)
            // 6. Entropy (Math on text)
            // 7. Account Age (API/Arg lookup)

            // GAME FILTER: Apply game filter FIRST as it can modify config.UsernamePattern and config.EnableEntropyCheck.
            ApplyGameFilter(config);

            // Check redemption cooldown (if enabled)
            if (config.RedemptionCooldownMinutes > 0)
            {
                if (state.RedemptionCooldowns.TryGetValue(userId, out var lastRedemption))
                {
                    var elapsed = (DateTime.Now - lastRedemption).TotalMinutes;
                    if (elapsed < config.RedemptionCooldownMinutes)
                    {
                        var remaining = (int)Math.Ceiling(config.RedemptionCooldownMinutes - elapsed);
                        adapter.LogTrace($"[{profileName}] Redemption cooldown active for {userName} ({remaining}min remaining)");
                        IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                        IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejectedCooldown);
                        return false;
                    }
                }
                // Update cooldown timestamp after passing the check
                state.RedemptionCooldowns[userId] = DateTime.Now;
            }

            // Strictness Check: Follower (Twitch Only)
            if (config.RequireFollower)
            {
                if (platform.Equals("Twitch", StringComparison.OrdinalIgnoreCase))
                {
                    if (!adapter.TwitchIsUserFollower(userId))
                    {
                        adapter.LogTrace($"[HandleEntry] Rejected {userName} (Not a follower).");
                        IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                        return false;
                    }
                }
                else
                {
                    adapter.LogVerbose($"[{profileName}] Skipped Require Follower check for {userName} (Platform: {platform})");
                }
            }

            // Strictness Check: Subscriber (Twitch Only)
            if (config.RequireSubscriber)
            {
                if (platform.Equals("Twitch", StringComparison.OrdinalIgnoreCase))
                {
                    if (!adapter.TwitchIsUserSubscriber(userId))
                    {
                        adapter.LogTrace($"[HandleEntry] Rejected {userName} (Not a subscriber).");
                        IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                        return false;
                    }
                }
                else
                {
                    adapter.LogVerbose($"[{profileName}] Skipped Require Subscriber check for {userName} (Platform: {platform})");
                }
            }

            // Global rate limit check
            if (state.IsSpamming(config.MaxEntriesPerMinute, GlobalConfig.Globals.SpamWindowSeconds))
            {
                adapter.LogTrace($"[HandleEntry] RATE LIMIT: {userName} rejected for {profileName} (Limit: {config.MaxEntriesPerMinute}/min).");
                IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRateLimited);

                if (GlobalConfig.Globals.EnableSecurityToasts)
                {
                    adapter.ShowToastNotification(Loc.Get("ToastTitle_Security"), Loc.Get("ToastMsg_SpamDetected", userName));
                }
                return false;
            }

            // Username/GameName Pattern Check
            // Extract arguments as potential Game Name
            string gameNameInput = null;
            if (adapter.TryGetArg<string>("rawInput", out var rawInput) && !string.IsNullOrEmpty(rawInput))
            {
                // Remove !command if present (simple heuristic, though rawInput usually contains just args in some SB versions, check !enter)
                // Actually CPH 'rawInput' is usually the full message. 'input0' is first arg.
                // Let's rely on 'rawInput' and strip the command if it starts with it.
                // Or safely, just check 'input0', 'input1' etc?
                // Best: Use 'rawInput' but trim command.
                // However, TriggerInspector has already run.

                // Let's assume rawInput might contain the command.
                // But simplified: Just take everything after the first space if it starts with !
                var parts = rawInput.Split(new[] { ' ' }, 2);
                if (parts.Length > 1) gameNameInput = parts[1].Trim();
                else if (!rawInput.StartsWith("!")) gameNameInput = rawInput.Trim(); // Just text
            }

            string validName = userName;
            if (IsEntryNameInvalid(userName, gameNameInput, config, adapter, out validName))
            {
                IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);

                // Notify User
                if (config.ToastNotifications.TryGetValue("Entry Rejected", out var notify) && notify)
                         adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Rejected_Pattern", userName));

                Messenger?.SendReply(adapter, Loc.Get("EntryRejected_GW2Pattern", userName), platform, userName); // Reply is safe?
                // Note: SendReply might cause loop if not careful, but it's a rejection.

                return false;
            }
            // Capture the validated name (Game Name OR User Name) for storage
            string finalEntryName = validName;

            // Entropy Check
            if (config.EnableEntropyCheck)
            {
                if (!EntryValidator.HasSufficientEntropy(userName, _configLoader.GetConfig(adapter).Globals.MinUsernameEntropy))
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Low entropy/suspicious name (User: {userName})");
                    IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                    if (config.ToastNotifications.TryGetValue("Entry Rejected", out var notify) && notify)
                        adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Rejected_Pattern", userName));
                    return false;
                }
            }

            // Account Age Check
            if (config.MinAccountAgeDays > 0)
            {
                if (adapter.TryGetArg<DateTime>("createdAt", out var createdAt))
                {
                    var accountAgeDays = (DateTime.Now - createdAt).TotalDays;
                    if (accountAgeDays < config.MinAccountAgeDays)
                    {
                        adapter.LogTrace($"[{profileName}] Entry rejected: Account too new ({accountAgeDays:F1} days < {config.MinAccountAgeDays} required) (User: {userName})");
                        IncGlobalMetric(adapter, GiveawayConstants.Metric_EntriesRejected);
                        if (config.ToastNotifications.TryGetValue("Entry Rejected", out var notify) && notify)
                            adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Rejected_Age", userName));
                        return false;
                    }
                    adapter.LogTrace($"[{profileName}] Account age check passed: {userName} ({accountAgeDays:F1} days old)");
                }
                else
                {
                    adapter.LogTrace($"[{profileName}] Account age validation skipped: 'createdAt' argument not available from Streamer.bot (User: {userName})");
                }
            }

            return true;
        }

        /// <summary>
        /// Handles drawing a winner for the giveaway.
        /// Supports determining winners via local RNG or the WheelOfNames API.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="config">Profile configuration.</param>
        /// <param name="state">Current state.</param>
        /// <param name="profileName">Profile name.</param>
        /// <param name="platform">Platform context.</param>
        /// <returns>True if the command was processed.</returns>
        private async Task<bool> HandleDraw(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string platform)
        {
            // Track winner draw metrics
            if (_cachedMetrics != null)
                _cachedMetrics.WinnerDrawAttempts++;

            adapter.LogInfo($"[HandleDraw] >>> Drawing winner for {profileName} (Platform: {platform})...");
            // Permission check: Only mods/broadcasters can draw
            adapter.TryGetArg<bool>("isModerator", out var isMod);
            adapter.TryGetArg<bool>("isBroadcaster", out var isBroadcaster);
            if (!isBroadcaster && !isMod)
            {
                adapter.LogVerbose("[HandleDraw] Cancelled: User is neither Broadcaster nor Moderator.");
                return true;
            }

            await _lock.WaitAsync();
            try
            {
                if (state.Entries.Count == 0)
                {
                    adapter.LogWarn($"[HandleDraw] ABORT: No entries for profile {profileName}.");
                    Messenger?.SendBroadcast(adapter, $"[{profileName}] No entries!", platform);
                    return true;
                }

                // Ticket Pool Flattening:
                // To support weighted probability (TicketCount > 1), we create a flat list where
                // each user appears N times, where N is their TicketCount.
                // Then a uniform random selection from this list gives the correct probability.
                adapter.LogVerbose($"[{profileName}] Generating ticket pool from {state.Entries.Count} entrants...");
                var pool = state.Entries.Values.SelectMany(e =>
                {
                    adapter.LogTrace($"[{profileName}] Pool Add: {e.UserName} ({e.TicketCount} tickets)");
                    return Enumerable.Repeat(e.UserName, e.TicketCount);
                }).ToList();
                var uniqueCount = state.Entries.Count;
                adapter.LogInfo($"[{profileName}] Drawing winner. Unique Entrants: {uniqueCount}, Total Tickets in Pool: {pool.Count}");

                // Integration: Wheel of Names
                if (config.EnableWheel && WheelClient != null)
                {
                    adapter.LogDebug($"[{profileName}] Method: Wheel Of Names API");
                    string apiKey = GetWheelApiKey(adapter);
                    string url = await WheelClient.CreateWheel(adapter, pool, apiKey, config.WheelSettings);
                    if (url != null)
                    {
                        if (Bus != null)
                        {
                            Bus.Publish(new WheelReadyEvent(adapter, profileName, state, url, platform));
                        }
                        else
                        {
                            // Fallback/Legacy direct call if EventBus was null (unlikely)
                            if (config.EnableObs && Obs != null) Obs.SetBrowserSource(adapter, config.ObsScene, config.ObsSource, url);
                            Messenger?.SendBroadcast(adapter, $"Wheel Ready! {url}", platform);
                        }

                        if (config.DumpWinnersOnDraw) await DumpWinnersAsync(adapter, profileName, pool, config);
                        return true;
                    }
                    adapter.LogWarn($"[{profileName}] Wheel API failed or returned null. Falling back to local RNG.");
                    if (_cachedMetrics != null) _cachedMetrics.ApiErrors++;
                }

                // Fallback: Local Random Draw
                // TODO: V2.0 - Replace with crypto-strength RNG
                adapter.LogDebug($"[{profileName}] Method: Local RNG");
                var rng = new Random();
                int winIndex = rng.Next(pool.Count);
                if (GlobalConfig?.Globals?.LogLevel == "TRACE")
                {
                    adapter.LogTrace($"[{profileName}] RNG Debug: Selected Index {winIndex} / {pool.Count}");
                }
                var winnerName = pool[winIndex];

                // Find the winner's entry to get their UserId for metrics
                var winnerEntry = state.Entries.Values.FirstOrDefault(e => e.UserName == winnerName);
                if (winnerEntry != null && winnerEntry.UserId != null)
                {
                    IncUserMetric(adapter, winnerEntry.UserId, winnerEntry.UserName ?? "Unknown", "WinsTotal", winnerEntry.GameName);
                    state.LastWinnerName = winnerEntry.UserName;
                    state.LastWinnerUserId = winnerEntry.UserId;
                    state.WinnerCount++;
                    IncGlobalMetric(adapter, GiveawayConstants.Metric_WinnersTotal);

                    // Track successful draw
                    if (_cachedMetrics != null)
                        _cachedMetrics.WinnerDrawSuccesses++;

                    adapter.LogDebug($"[{profileName}] Winner: {winnerName} ({winnerEntry.UserId}) - WinsTotal incremented.");

                    if (Bus != null)
                    {
                        Bus.Publish(new WinnerSelectedEvent(adapter, profileName, state, winnerEntry, platform));
                    }
                    else
                    {
                         // Fallback Broadcast if EventBus is missing
                         string rawTmpl = GiveawayManager.PickRandomMessage(config.WheelSettings?.WinnerMessage);
                         string msg = rawTmpl?.Replace("{name}", winnerEntry.UserName) ?? Loc.Get("ToastMsg_Winner", winnerEntry.UserName);
                         Messenger?.SendBroadcast(adapter, msg, platform);
                    }
                }



                // Winner message handled by EventBus subscribers
                if (config.DumpWinnersOnDraw) await DumpWinnersAsync(adapter, profileName, pool, config, state);
                PersistenceService.SaveState(adapter, profileName, state, GlobalConfig.Globals, true);
                SyncProfileVariables(adapter, profileName, config, state, GlobalConfig.Globals);
                return true;
            }
            catch (Exception ex)
            {
                adapter.LogError($"[{profileName}] HandleDraw Failed: {ex.Message}");
                return true;
            }
            finally { _lock.Release(); }
        }

        /// <summary>
        /// Opens the giveaway for new entries.
        /// Resets state, starts timers (if configured), and broadcasts the open message.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="config">Profile Config.</param>
        /// <param name="state">Profile State.</param>
        /// <param name="profileName">Profile Name.</param>
        /// <param name="platform">Platform context.</param>
        /// <returns>True if processed.</returns>

        private async Task<bool> HandleStart(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string platform)
        {
            // Track this operation to prevent false remote END detection during startup
            lock (_opLock) { _currentOperation = $"START:{profileName}"; }

            await _lock.WaitAsync();
            try
            {
              state.IsActive = true;
            state.Entries.Clear();
            state.WinnerCount = 0;
            state.LastWinnerName = null;
            state.LastWinnerUserId = null;
            state.LastWinnerName = null;
            state.LastWinnerUserId = null;
            state.AutoCloseTime = null; // Reset auto-close
            state.StartTime = DateTime.UtcNow; // Track start time for dynamic calcs

            // Handle Timed Giveaway
            var duration = ParseDuration(config.TimerDuration);
            if (duration.HasValue)
            {
                state.AutoCloseTime = DateTime.UtcNow.AddSeconds(duration.Value);
                adapter.LogInfo($"[{profileName}] Timed giveaway started. Will auto-close in {duration.Value}s.");
            }


            IncGlobalMetric(adapter, GiveawayConstants.Metric_GiveawayStarted);

            PersistenceService.SaveState(adapter, profileName, state, GlobalConfig.Globals);
            SyncProfileVariables(adapter, profileName, config, state, GlobalConfig.Globals);

            string msgKey = "GiveawayOpened";
            if (profileName != "Main" && !GlobalConfig.Profiles.ContainsKey("Main")) msgKey = "GiveawayOpened_NoProfile"; // Legacy fallback

            string openMsg = Loc.Get(msgKey, profileName, profileName);
            Messenger?.SendBroadcast(adapter, openMsg, platform);

            if (Bus != null) Bus.Publish(new GiveawayStartedEvent(adapter, profileName, state, platform));
                else
                {
                    if (config.ToastNotifications != null && config.ToastNotifications.TryGetValue("GiveawayOpened", out var notify) && notify)
                        adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Opened", profileName));
                }

                // Clear operation tracking after successful start
                lock (_opLock) { _currentOperation = null; }

                return true;
            }
            catch (Exception ex)
            {
                adapter.LogError($"[{profileName}] HandleStart Failed: {ex.Message}");
                lock (_opLock) { _currentOperation = null; } // Clear on error too
                return true;
            }
            finally { _lock.Release(); }
        }


        /// <summary>
        /// Closes the giveaway and optionally dumps the final entry list.
        /// Stops timers, resets cooldowns, and broadcasts the closed message.
        /// </summary>
        /// <remarks>
        /// This method is idempotent; calling it on an already closed giveaway is safe.
        /// </remarks>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="config">Profile Config.</param>
        /// <param name="state">Profile State.</param>
        /// <param name="profileName">Profile Name.</param>
        /// <param name="platform">Platform.</param>
        /// <param name="bypassAuth">If true, skips broadcaster/mod checks (used for auto-close).</param>

        private async Task<bool> HandleEnd(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string platform, bool bypassAuth = false)
        {
            // Track this operation to prevent false remote START detection during shutdown
            lock (_opLock) { _currentOperation = $"END:{profileName}"; }

            if (!bypassAuth)
            {
                adapter.TryGetArg<bool>("isModerator", out var isMod);
                adapter.TryGetArg<bool>("isBroadcaster", out var isBroadcaster);
                if (!isBroadcaster && !isMod) return true;
            }

            await _lock.WaitAsync();
            try
            {
            if (!state.IsActive)
            {
                // Already closed
                return true;
            }

            state.IsActive = false;
            state.AutoCloseTime = null; // Clear timer
            state.RedemptionCooldowns.Clear(); // Reset cooldowns for next giveaway


            IncGlobalMetric(adapter, GiveawayConstants.Metric_GiveawayEnded);

            PersistenceService.SaveState(adapter, profileName, state, GlobalConfig.Globals);
            SyncProfileVariables(adapter, profileName, config, state, GlobalConfig.Globals);

            if (config.DumpEntriesOnEnd)
            {
                await DumpEntriesAsync(adapter, profileName, state, config);
            }

            string closeMsg = Loc.Get("GiveawayClosed", profileName);
            Messenger?.SendBroadcast(adapter, closeMsg, platform);

            if (Bus != null) Bus.Publish(new GiveawayEndedEvent(adapter, profileName, state, platform));
                else
                {
                    if (config.ToastNotifications != null && config.ToastNotifications.TryGetValue("GiveawayClosed", out var notify) && notify)
                        adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("ToastMsg_Closed", profileName));
                }

                // Clear operation tracking after successful end
                lock (_opLock) { _currentOperation = null; }

                return true;
            }
            catch (Exception ex)
            {
                adapter.LogError($"[{profileName}] HandleEnd Failed: {ex.Message}");
                lock (_opLock) { _currentOperation = null; } // Clear on error too
                return true;
            }
            finally { _lock.Release(); }
        }

        /// <summary>
        /// Asynchronously dumps the list of winners to a timestamped file.
        /// Supports TXT, CSV, and JSON formats based on profile config.
        /// </summary>
        /// <param name="adapter">CPH Adapter used for logging errors.</param>
        /// <param name="profileName">Name of the profile (used for directory structure).</param>
        /// <param name="pool">List of winner usernames.</param>
        /// <param name="config">Profile configuration determining the output format.</param>
        /// <param name="state">Giveaway state to lookup extra entry details (like GameName).</param>
        private static async Task DumpWinnersAsync(CPHAdapter adapter, string profileName, List<string> pool, GiveawayProfileConfig config, GiveawayState state = null)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "dumps", profileName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                // Determine format
                DumpFormat fmt = config?.DumpFormat ?? DumpFormat.TXT;
                string ext = fmt == DumpFormat.JSON ? "json" : (fmt == DumpFormat.CSV ? "csv" : "txt");

                var path = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd_HHmm}_Winners.{ext}");

                using (var writer = new System.IO.StreamWriter(path, false))
                {
                    if (fmt == DumpFormat.JSON)
                    {
                        var json = JsonConvert.SerializeObject(pool.Distinct(), Formatting.Indented);
                        await writer.WriteAsync(json).ConfigureAwait(false);
                    }
                    else if (fmt == DumpFormat.CSV)
                    {
                        await writer.WriteLineAsync("Username,GameName").ConfigureAwait(false);
                        foreach (var winnerName in pool.Distinct())
                        {
                            var entry = state?.Entries.Values.FirstOrDefault(e => e.UserName == winnerName);
                            var gameName = entry?.GameName ?? "";

                            // Escape commas if needed (simple implementation)
                            var escapedName = winnerName.Contains(",") ? $"\"{winnerName}\"" : winnerName;
                            var escapedGame = gameName.Contains(",") ? $"\"{gameName}\"" : gameName;

                            await writer.WriteLineAsync($"{escapedName},{escapedGame}").ConfigureAwait(false);
                        }
                    }
                    else // TXT
                    {
                        foreach (var winnerName in pool.Distinct())
                        {
                            var entry = state?.Entries.Values.FirstOrDefault(e => e.UserName == winnerName);
                            var gameNameDisplay = !string.IsNullOrEmpty(entry?.GameName) ? $" [{entry.GameName}]" : "";
                            await writer.WriteLineAsync($"{winnerName}{gameNameDisplay}").ConfigureAwait(false);
                        }
                    }
                }

                // Generate separate GameNames file if enabled
                if (config?.DumpSeparateGameNames == true)
                {
                    var gameNamesPath = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd_HHmm}_Winners_GameNames.txt");
                    using (var writer = new System.IO.StreamWriter(gameNamesPath, false))
                    {
                        foreach (var winnerName in pool.Distinct())
                        {
                            var entry = state?.Entries.Values.FirstOrDefault(e => e.UserName == winnerName);
                            var displayName = !string.IsNullOrEmpty(entry?.GameName) ? entry.GameName : winnerName;
                            await writer.WriteLineAsync(displayName).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Non-critical dump operation - log error but don't crash bot
                try
                {
                    adapter?.LogError($"[{profileName}] Failed to dump winners: {ex.Message}");
                }
                catch { /* Silent fallback if logging fails */ }
            }
        }

        /// <summary>
        /// Asynchronously dumps full entry details to a timestamped file.
        /// This is typically called when a giveaway ends.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="profileName">Name of the profile.</param>
        /// <param name="state">Current state containing the entries to dump.</param>
        /// <param name="config">Configuration determining dump format.</param>
        private static async Task DumpEntriesAsync(CPHAdapter adapter, string profileName, GiveawayState state, GiveawayProfileConfig config)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "dumps", profileName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                // Determine format
                DumpFormat fmt = config?.DumpFormat ?? DumpFormat.TXT;
                string ext = fmt == DumpFormat.JSON ? "json" : (fmt == DumpFormat.CSV ? "csv" : "txt");

                var path = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd_HHmm}_Entries.{ext}");

                using (var writer = new System.IO.StreamWriter(path, false))
                {
                    if (fmt == DumpFormat.JSON)
                    {
                        var json = JsonConvert.SerializeObject(state.Entries.Values, Formatting.Indented);
                        await writer.WriteAsync(json).ConfigureAwait(false);
                    }
                    else if (fmt == DumpFormat.CSV)
                    {
                        await writer.WriteLineAsync("UserId,Username,GameName,IsSub,TicketCount,EntryTime").ConfigureAwait(false);
                        foreach (var entry in state.Entries.Values)
                        {
                            var safeUser = entry.UserName.Replace("\"", "\"\"");
                            if (safeUser.Contains(",")) safeUser = $"\"{safeUser}\"";

                            var safeGame = (entry.GameName ?? "").Replace("\"", "\"\"");
                            if (safeGame.Contains(",")) safeGame = $"\"{safeGame}\"";

                            var line = $"{entry.UserId},{safeUser},{safeGame},{entry.IsSub},{entry.TicketCount},{entry.EntryTime:O}";
                            await writer.WriteLineAsync(line).ConfigureAwait(false);
                        }
                    }
                    else // TXT
                    {
                        foreach (var entry in state.Entries.Values)
                        {
                            var gameNameDisplay = !string.IsNullOrEmpty(entry.GameName) ? $" [{entry.GameName}]" : "";
                            var line = $"{entry.UserName} ({entry.UserId}){gameNameDisplay} - Tickets: {entry.TicketCount}";
                            await writer.WriteLineAsync(line).ConfigureAwait(false);
                        }
                    }
                }

                // Generate separate GameNames file if enabled
                if (config?.DumpSeparateGameNames == true)
                {
                    var gameNamesPath = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd_HHmm}_Entries_GameNames.txt");
                    using (var writer = new System.IO.StreamWriter(gameNamesPath, false))
                    {
                        foreach (var entry in state.Entries.Values)
                        {
                            var displayName = !string.IsNullOrEmpty(entry.GameName) ? entry.GameName : entry.UserName;
                            await writer.WriteLineAsync(displayName).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Non-critical dump operation
                adapter?.LogError($"[{profileName}] Failed to dump entries: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs a comprehensive health check of the bot's environment (Files, Config, Persistence, API).
        /// Logs results to the Streamer.bot log and broadcasts them to chat.
        /// </summary>
        public async Task PerformSystemCheck(CPHAdapter adapter)
        {
            adapter.LogInfo("[SystemCheck] >>> Starting full architectural verification...");
            var results = new List<string>
            {
                "[SYSTEM TEST IN PROGRESS...]",
                // Environment & Baseline
                "--- Baseline ---",
                $"• Platform: {Environment.OSVersion}",
                $"• Runtime: {AppDomain.CurrentDomain.FriendlyName}",
                // [WARNING] Do not output this path to chat to prevent information leakage.
                $"• Base Directory: {AppDomain.CurrentDomain.BaseDirectory}",

                // Logging System Check
                "--- Logging Infrastructure ---"
            };
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "logs");
                results.Add($"• Log Directory: {logDir}");
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

                string testFile = Path.Combine(logDir, $"check_{Guid.NewGuid():N}.tmp");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                results.Add("• Disk Write Access: PASSED ✅");
            }
            catch (Exception ex)
            {
                results.Add($"• Disk Write Access: FAILED ❌ ({ex.Message})");
                adapter.LogError($"[SystemCheck] FS Check failed: {ex.Message}");
            }

            // Configuration & Migration Check
            results.Add("--- Configuration ---");
            var mode = ConfigLoader.GetRunMode(adapter);
            results.Add($"• RunMode: {mode}");
            results.Add($"• Profile Count: {GlobalConfig?.Profiles.Count ?? 0}");
            var configReport = _configLoader?.ValidateConfig(adapter) ?? "Error: ConfigLoader not initialized";
            results.Add($"• Profile Config Validator: {(configReport.Contains("VALID") ? "PASS ✅" : configReport)}");

            // Persistence & Migration Integrity
            results.Add("--- Persistence ---");
            results.Add($"• State Mode: {GlobalConfig?.Globals.StatePersistenceMode}");
            results.Add($"• Sync Interval: {GlobalConfig?.Globals.StateSyncIntervalSeconds}s");
            try
            {
                var testKey = GiveawayConstants.GlobalHealthTest;
                var testVal = Guid.NewGuid().ToString();
                adapter.SetGlobalVar(testKey, testVal, true);
                var readBack = adapter.GetGlobalVar<string>(testKey, true);
                results.Add($"• Persistence (GlobalVar): {(readBack == testVal ? "PASS ✅" : "FAIL ❌ (Data mismatch)")}");
            }
            catch (Exception ex) { results.Add($"• Persistence (GlobalVar): FAIL ❌ ({ex.Message})"); }

            // File System Check
            try
            {
                string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot");
                string logDir = Path.Combine(baseDir, "logs", "General");
                string testFile = Path.Combine(logDir, "write_test.tmp");
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                results.Add("• File System: PASS ✅ (Write access confirmed)");
            }
            catch (Exception ex) { results.Add($"• File System: FAIL ❌ ({ex.Message})"); }

            // API Check (Only if configured)
            var cfg = GlobalConfig;
            if (cfg?.Globals != null && !string.IsNullOrEmpty(cfg.Globals.WheelApiKeyVar))
            {
                var key = adapter.GetGlobalVar<string>(cfg.Globals.WheelApiKeyVar, true);
                if (string.IsNullOrEmpty(key)) results.Add("• Wheel API: WARN ⚠️ (API Key variable is empty)");
                else results.Add("• Wheel API: READY 🟢 (Key found)");
            }

            // Multi-Platform Connectivity Check
            if (cfg?.Globals != null)
            {
                var pList = cfg.Globals.EnabledPlatforms ?? new List<string> { "Twitch", "YouTube", "Kick" };
                List<string> live = new List<string>();
                if (pList.Contains("Twitch") && adapter.IsTwitchLive()) live.Add("Twitch");
                if (pList.Contains("YouTube") && adapter.IsYouTubeLive()) live.Add("YouTube");
                if (pList.Contains("Kick") && adapter.IsKickLive()) live.Add("Kick");
                results.Add($"• Platform Status: {(live.Count > 0 ? "LIVE 🟢 (" + string.Join(", ", live) + ")" : "OFFLINE ⚪ (Fallback: " + cfg.Globals.FallbackPlatform + ")")}");
            }

            // Send results via multi-platform bridge
            foreach (var msg in results)
            {
                // Log to file for persistence
                adapter.LogInfo($"[Report] {msg}");

                Messenger?.SendBroadcast(adapter, msg, "Twitch");
                await Task.Delay(500);
            }
        }

        /// <summary>
        /// Tests a regex pattern against sample text, showing match results and captured groups.
        /// Includes timeout protection to prevent ReDoS (Regular Expression Denial of Service).
        /// </summary>
        /// <param name="adapter">CPH adapter for messaging</param>
        /// <param name="rawInput">Full command string</param>
        /// <param name="platform">Platform to send response to</param>
        private async Task HandleRegexTest(CPHAdapter adapter, string rawInput, string platform)
        {
            try
            {
                // Parse command: !giveaway regex test <pattern> <text>
                // Extract everything after "regex test"
                int startIdx = rawInput.IndexOf("regex test");
                if (startIdx == -1)
                {
                    Messenger?.SendBroadcast(adapter, "Usage: !giveaway regex test <pattern> <text>", platform);
                    return;
                }

                string remainder = rawInput.Substring(startIdx + "regex test".Length).Trim();
                if (string.IsNullOrEmpty(remainder))
                {
                    Messenger?.SendBroadcast(adapter, "Usage: !giveaway regex test <pattern> <text>", platform);
                    return;
                }

                // Split into pattern and test text
                // Find first space after pattern (simple parsing - pattern cannot contain spaces unless escaped)
                var parts = remainder.Split(new[] { ' ' }, 2);
                if (parts.Length < 2)
                {
                    Messenger?.SendBroadcast(adapter, "Usage: !giveaway regex test <pattern> <text>", platform);
                    return;
                }

                string pattern = parts[0];
                string testText = parts[1];

                adapter.LogDebug($"[RegexTest] Pattern: '{pattern}' | Text: '{testText}'");

                // Test the regex with timeout protection (prevent ReDoS)
                try
                {
                    var regex = new Regex(pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
                    var match = regex.Match(testText);

                    if (match.Success)
                    {
                        // Build capture groups string
                        string groups = "";
                        if (match.Groups.Count > 1)
                        {
                            var captured = new List<string>();
                            for (int i = 1; i < match.Groups.Count; i++)
                            {
                                if (match.Groups[i].Success)
                                {
                                    captured.Add(match.Groups[i].Value);
                                }
                            }
                            if (captured.Count > 0)
                            {
                                groups = " | Captured: " + string.Join(", ", captured.ToArray());
                            }
                        }

                        Messenger?.SendBroadcast(adapter, $"✅ MATCH: \"{match.Value}\"{groups}", platform);
                        adapter.LogInfo($"[RegexTest] Match successful: '{match.Value}'{groups}");
                    }
                    else
                    {
                        Messenger?.SendBroadcast(adapter, $"❌ NO MATCH for pattern: {pattern}", platform);
                        adapter.LogInfo($"[RegexTest] No match for pattern '{pattern}' against '{testText}'");
                    }
                }
                catch (ArgumentException ex)
                {
                    Messenger?.SendBroadcast(adapter, $"❌ INVALID PATTERN: {ex.Message}", platform);
                    adapter.LogWarn($"[RegexTest] Invalid regex pattern '{pattern}': {ex.Message}");
                }
                catch (RegexMatchTimeoutException)
                {
                    Messenger?.SendBroadcast(adapter, "⏱ TIMEOUT: Pattern too complex (>100ms)", platform);
                    adapter.LogWarn($"[RegexTest] Regex timeout for pattern '{pattern}'");
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[RegexTest] Unexpected error: {ex.Message}");
                Messenger?.SendBroadcast(adapter, "❌ Test failed. Check logs for details.", platform);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Timer callback to check all profiles for pending entry dumps.
        /// Processes batches based on configured throttle seconds.
        /// </summary>
        private void ProcessPendingDumpsCallback(object state)
        {
            try
            {
                foreach (var kvp in States)
                {
                    var profileName = kvp.Key;
                    var profileState = kvp.Value;

                    if (!GlobalConfig.Profiles.TryGetValue(profileName, out var config)) continue;
                    if (!config.DumpEntriesOnEntry) continue;

                    var elapsed = (DateTime.Now - profileState.LastDumpTime).TotalSeconds;
                    if (elapsed < config.DumpEntriesOnEntryThrottleSeconds) continue;

                    if (profileState.PendingDumps.Count > 0)
                    {
                        // Fire-and-forget async (don't block timer)
                        Task.Run(async () => await FlushPendingDumpsAsync(_currentAdapter, profileName, profileState));
                    }
                }
            }
            catch (Exception ex)
            {
                _currentAdapter?.LogError($"[DumpTimer] Error processing pending dumps: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously flushes pending entry dumps to disk in batched writes.
        /// Implements error recovery by re-queuing failed entries.
        /// </summary>
        /// <param name="adapter">CPH adapter for logging</param>
        /// <param name="profileName">Profile name for file organization</param>
        /// <param name="state">Giveaway state containing pending dumps</param>
        private static async Task FlushPendingDumpsAsync(CPHAdapter adapter, string profileName, GiveawayState state)
        {
            var batch = new List<Entry>();
            try
            {
                // Dequeue all pending entries
                while (state.PendingDumps.TryDequeue(out var entry))
                {
                    batch.Add(entry);
                }

                if (batch.Count == 0) return;

                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "dumps", profileName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                var path = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd}_Entries_Incremental.txt");

                // C# 7.3 compatible async file write
                using (var writer = new StreamWriter(path, append: true))
                {
                    foreach (var entry in batch)
                    {
                        var line = $"[{entry.EntryTime:hh:mm:ss tt}] {entry.UserName} ({entry.UserId}) - Tickets: {entry.TicketCount}";
                        await writer.WriteLineAsync(line).ConfigureAwait(false);
                    }
                }

                state.LastDumpTime = DateTime.Now;
                adapter?.LogDebug($"[{profileName}] Flushed {batch.Count} entries to incremental dump");
            }
            catch (Exception ex)
            {
                adapter?.LogError($"[{profileName}] Failed to flush {batch.Count} entries to incremental dump: {ex.Message}");

                // Re-queue failed entries for retry
                foreach (var entry in batch)
                {
                    state.PendingDumps.Enqueue(entry);
                }
            }
        }

        /// <summary>
        /// Handles GDPR data deletion requests.
        /// Removes a user from all active profiles, metrics, and global variables.
        /// Also attempts to sanitize historical log files.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="rawInput">Command string.</param>
        /// <param name="platform">Platform context.</param>
        /// <remarks>
        /// This is a destructive operation required for compliance.
        /// It scrubs:
        /// 1. Active Entries in memory.
        /// 2. Persisted State (JSON).
        /// 3. Global User Variables (Giveaway_User_*).
        /// 4. Global Metrics.
        /// 5. Text Dump Files (Best Effort).
        /// </remarks>
        private async Task<bool> HandleDataDeletion(CPHAdapter adapter, string rawInput, string platform)
        {
    var match = Regex.Match(rawInput, @"(?:!giveaway|!ga)\s+(?:data|d)\s+delete\s+(.+)", RegexOptions.IgnoreCase);
    if (!match.Success)
    {
        Messenger?.SendBroadcast(adapter, "Usage: !giveaway data delete <username>", platform);
        return true;
    }

    string targetUser = match.Groups[1].Value.Trim();
    adapter.LogInfo($"[GDPR] Starting data deletion for user: {targetUser}");
    Messenger?.SendBroadcast(adapter, $"Processing data deletion for '{targetUser}'... (This may take a moment)", platform);

    await _lock.WaitAsync();
    try
    {
        int entriesRemoved = 0;
        int profilesAffected = 0;

        // Clean Active States (Memory + JSON persistence)
        if (GlobalConfig?.Profiles != null)
        {
            foreach (var profileKey in GlobalConfig.Profiles.Keys.ToList())
            {
                // Ensure state is loaded
                if (!States.ContainsKey(profileKey))
                {
                    var s = PersistenceService.LoadState(adapter, profileKey, GlobalConfig.Globals);
                    if (s != null) States[profileKey] = s;
                }

                if (States.TryGetValue(profileKey, out var state))
                {
                    // Find by UserName (Case Insensitive) OR BaseUserId
                    // Note: Entries are keyed by UserId.
                    var entry = state.Entries.Values.FirstOrDefault(e => e.UserName.Equals(targetUser, StringComparison.OrdinalIgnoreCase) || e.UserId == targetUser);

                    if (entry != null)
                    {
                        state.Entries.Remove(entry.UserId);
                        state.CumulativeEntries--; // Decrement header
                        entriesRemoved++;
                        profilesAffected++;
                        adapter.LogInfo($"[GDPR] Removed entry from profile '{profileKey}' (Tickets: {entry.TicketCount})");

                        // Persist changes immediately
                        PersistenceService.SaveState(adapter, profileKey, state, GlobalConfig.Globals, true);
                    }
                }
            }
        }

        // Clean User Variables (Global Vars) & Metrics
        // Iterate metrics to resolve ID from Name if possible
        if (_cachedMetrics != null && _cachedMetrics.UserMetrics != null)
        {
            var userMetricKeys = _cachedMetrics.UserMetrics
                .Where(kvp => kvp.Value.UserName.Equals(targetUser, StringComparison.OrdinalIgnoreCase))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var uid in userMetricKeys)
            {
                _cachedMetrics.UserMetrics.Remove(uid);
                adapter.UnsetGlobalVar($"{GiveawayConstants.UserVarPrefix}{uid}");
                adapter.LogInfo($"[GDPR] Cleaned global metrics/vars for UserID: {uid}");
                entriesRemoved++; // Count metric removal as a "record" removed
            }
        }

        // Clean Historical Logs (Dumps)
        // Directory: Giveaway Bot/data/dumps (and others?)
        string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot");
        if (Directory.Exists(baseDir))
        {
            var files = Directory.GetFiles(baseDir, "*.txt", SearchOption.AllDirectories); // .txt logs
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);
                var newLines = lines.Where(l => !l.Contains(targetUser)).ToArray(); // Simple substring match - aggressive but safer for GDPR
                if (lines.Length != newLines.Length)
                {
                    File.WriteAllLines(file, newLines);
                    adapter.LogInfo($"[GDPR] Sanitized file: {Path.GetFileName(file)} (Removed {lines.Length - newLines.Length} lines)");
                }
            }
        }

        // Clean JSON State Files (for profiles not in config anymore?)
        // We already handled active profiles.

        Messenger?.SendBroadcast(adapter, $"✅ Data deletion complete for '{targetUser}'. Removed from {profilesAffected} active profiles and cleaned global records.", platform);
    }
    catch (Exception ex)
    {
        adapter.LogError($"[GDPR] Error deleting data for {targetUser}: {ex.Message}");
        Messenger?.SendBroadcast(adapter, $"⚠ Error during deletion: {ex.Message}", platform);
    }
    finally
    {
        _lock.Release();
    }
    return true;
}

        /// <summary>
        /// Handles the '!giveaway stats' command.
        /// Displays global aggregated stats or stats for a specific profile.
        /// </summary>
        /// <param name="adapter">CPH Adapter.</param>
        /// <param name="rawInput">Command string.</param>
        /// <param name="platform">Platform context.</param>
        private async Task<bool> HandleStatsCommand(CPHAdapter adapter, string rawInput, string platform)
        {
    await Task.CompletedTask;
    // Usage: !giveaway stats [global|profileName]
    // Default to global if no arg

    // Simple parsing
    bool isGlobal = rawInput.ToLower().Contains("global") || !GlobalConfig.Profiles.Keys.Any(k => rawInput.Contains(k));

    if (isGlobal)
    {
        long totalEntries = 0;
        long totalWinners = 0;
        long uniqueUsers = 0;

        // Sum from States
        foreach(var state in States.Values)
        {
            totalEntries += state.CumulativeEntries;
            totalWinners += state.WinnerCount;
        }

        // Get unique users from Metrics if available, otherwise just count all current entries
        if (_cachedMetrics != null)
        {
            uniqueUsers = _cachedMetrics.UserMetrics.Count;
            // Also use global counters if available
            if (_cachedMetrics.GlobalMetrics.TryGetValue("Entries_Total", out var gEntries)) totalEntries = Math.Max(totalEntries, gEntries);
            if (_cachedMetrics.GlobalMetrics.TryGetValue("Winners_Total", out var gWinners)) totalWinners = Math.Max(totalWinners, gWinners);
        }
        else
        {
             // Fallback estimation
             uniqueUsers = States.Values.SelectMany(s => s.Entries.Keys).Distinct().Count();
        }

        Messenger?.SendBroadcast(adapter, $"📊 [Global Stats] Entries: {totalEntries} | Winners: {totalWinners} | Unique Users: {uniqueUsers}", platform);
    }
    else
    {
        // Profile specific stats
        // Find profile name in string
        var profile = GlobalConfig.Profiles.Keys.FirstOrDefault(k => rawInput.Contains(k));
        if (profile != null && States.TryGetValue(profile, out var state))
        {
             Messenger?.SendBroadcast(adapter, $"📊 [{profile}] active: {state.Entries.Count} | Total: {state.CumulativeEntries} | Winners: {state.WinnerCount} | Last Winner: {state.LastWinnerName ?? "None"}", platform);
        }
        else
        {
             Messenger?.SendBroadcast(adapter, "Profile not found or no stats available.", platform);
        }
    }

    return true;
}

/// <summary>
/// Checks if the input string corresponds to a data command (e.g., for GDPR actions).
/// </summary>
/// <param name="s">The input string to check.</param>
/// <returns>True if the string contains a data command, false otherwise.</returns>
private static bool CheckDataCmd(string s) => s != null && (s.Contains(GiveawayConstants.CmdPattern_GiveawayPrefix + GiveawayConstants.CmdPrefix_Data) || s.Contains(GiveawayConstants.CmdPattern_GaPrefix + GiveawayConstants.CmdPrefix_Data) || s.Contains(GiveawayConstants.CmdPattern_GiveawayPrefix + GiveawayConstants.CmdPrefix_DataShort) || s.Contains(GiveawayConstants.CmdPattern_GaPrefix + GiveawayConstants.CmdPrefix_DataShort));


    }

    /// <summary>
    /// Data transfer object holding the results of a trigger identification.
    /// Indicates which profile and action should be executed.
    /// </summary>
    public class TriggerResult
    {
        public string Profile { get; set; }
        public string Action { get; set; }
        public string Platform { get; set; } = "Twitch";
        public string Details { get; set; } = "";

        /// <summary>
        /// Deconstructs the result into its component parts.
        /// </summary>
        public void Deconstruct(out string profile, out string action, out string platform, out string details)
        {
            profile = Profile;
            action = Action;
            platform = Platform;
            details = Details;
        }
    }

#endregion

#region TriggerInspector
    /// <summary>
    /// Routes incoming Streamer.bot triggers (Commands, StreamDeck, Raw Input) to specific giveaway profiles.
    /// Returns the profile name and the abstract action (Entry, Draw, etc.) to perform.
    /// </summary>
    public class TriggerInspector
    {
        public TriggerInspector() { }

        private static readonly char[] SplitColon = new char[] { ':' };

        /// <summary>
        /// Analyzes an incoming trigger to determine if it matches any configured profile triggers.
        /// </summary>
        /// <param name="adapter">CPH adapter instance.</param>
        /// <param name="profiles">The dictionary of configured profiles.</param>
        /// <returns>A TriggerResult indicating the matched profile and action, or empty if no match.</returns>
        /// <remarks>
        /// Matching Precedence:
        /// 1. Command Matches (Exact Arg or Input0)
        /// 2. StreamDeck Button ID
        /// 3. Trigger ID (Streamer.bot Action ID)
        /// 4. Trigger Name
        /// 5. Regex/Pattern Match on Command/Message
        /// </remarks>
        public static TriggerResult IdentifyTrigger(CPHAdapter adapter, Dictionary<string, GiveawayProfileConfig> profiles)
        {
            adapter.TryGetArg<string>("userType", out var userType);
            string platform = userType ?? "Twitch"; // Default to Twitch if unknown
            if (platform.Equals("youtube", StringComparison.OrdinalIgnoreCase)) platform = "YouTube";
            if (platform.Equals("kick", StringComparison.OrdinalIgnoreCase)) platform = "Kick";
            if (platform.Equals("twitch", StringComparison.OrdinalIgnoreCase)) platform = "Twitch";
            adapter.LogTrace($"[Trigger] Platform detected: {platform} (Raw: {userType})");

            // ---------------------------------------------------------
            // 1. DATA EXTRACTION
            // Streamer.bot provides many arguments. We try to grab them all.
            // 'command' = The command name if triggered by a command (e.g., "!giveaway")
            // 'message' = The raw chat message
            // 'rawInput' = The message usually, or the input after the command
            // 'input0' = The first word after the command (e.g., in "!giveaway start", input0 is "start")
            // ---------------------------------------------------------
            adapter.TryGetArg<string>("command", out var cmd);
            adapter.TryGetArg<string>("message", out var msgArg);
            adapter.TryGetArg<string>("sdButtonId", out var sdId);
            adapter.TryGetArg<string>("triggerId", out var tId);
            adapter.TryGetArg<string>("triggerName", out var tName);
            adapter.TryGetArg<string>("rawInput", out var rawInput);
            adapter.TryGetArg<string>("input0", out var input0); // Capture split input arg 0

            adapter.LogVerbose($"[Trigger] Inspecting: cmd={cmd}, msg={msgArg}, sd={sdId}, tid={tId}, name={tName}, raw={rawInput}, in0={input0}");
            string detail = $"cmd={cmd}, msg={msgArg}, sd={sdId}, tid={tId}, name={tName}, raw={rawInput}";

            if (profiles != null)
            {
                foreach (var kvp in profiles)
                {
                    if (kvp.Value.Triggers == null) continue;
                    foreach (var t in kvp.Value.Triggers)
                    {
                        var parts = t.Key.Split(SplitColon, 2);
                        if (parts.Length < 2) continue;
                        var type = parts[0].ToLower();
                        var val = parts[1];

                        adapter.LogTrace($"Checking Profile={kvp.Key}: Trigger={t.Key} (Type={type}, Val={val}) vs Input(cmd={cmd}, sd={sdId}, tid={tId}, name={tName})");

                        // =================================================================================
                        // TRIGGER MATCHING PRECEDENCE
                        // 1. Exact Command Match (Fastest) - Matches !giveaway command arguments
                        // 2. StreamDeck Button ID - Matches hardware inputs
                        // 3. Named Trigger ID - Matches Streamer.bot Action Triggers
                        // 4. Regex Pattern - Analyzes raw chat messages (Slowest)
                        // =================================================================================

                        if (type == "command")
                        {
                            // Check strict 'command' arg
                            if (cmd != null && cmd.Equals(val, StringComparison.OrdinalIgnoreCase))
                            {
                                adapter.LogDebug($"[Trigger] Match found (Command Arg): {kvp.Key} -> {t.Value} ({cmd})");
                                return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail };
                            }

                            // Check 'input0' (often populated for commands with args)
                            if (input0 != null && input0.Equals(val, StringComparison.OrdinalIgnoreCase))
                            {
                                adapter.LogDebug($"[Trigger] Match found (Input0): {kvp.Key} -> {t.Value} ({input0})");
                                return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail };
                            }

                            // Fallback: Check rawInput or message for "StartsWith" (e.g. Chat Message trigger)
                            // Sometimes a trigger isn't a "Command" in Streamer.bot but just a "Execute C#" code action
                            // listening to "Chat Message" events.
                            // In this case, we manually check if the message starts with the command.
                            string fallback = rawInput ?? msgArg;
                            if (fallback != null)
                            {
                                var trimmed = fallback.Trim();
                                if (trimmed.Equals(val, StringComparison.OrdinalIgnoreCase) ||
                                    trimmed.StartsWith(val + " ", StringComparison.OrdinalIgnoreCase))
                                {
                                    adapter.LogDebug($"[Trigger] Match found (Raw/Msg): {kvp.Key} -> {t.Value} ('{trimmed}')");
                                    return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail };
                                }
                            }
                        }

                        if (type == "sd" && sdId != null && sdId.Equals(val, StringComparison.OrdinalIgnoreCase)) { adapter.LogDebug($"[Trigger] Match found: {kvp.Key} -> {t.Value} (SD Button: {sdId})"); return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail }; }
                        if (type == "id" && tId != null && tId.Equals(val, StringComparison.OrdinalIgnoreCase)) { adapter.LogDebug($"[Trigger] Match found: {kvp.Key} -> {t.Value} (Trigger ID: {tId})"); return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail }; }
                        if (type == "name" && tName != null && tName.Equals(val, StringComparison.OrdinalIgnoreCase)) { adapter.LogDebug($"[Trigger] Match found: {kvp.Key} -> {t.Value} (Trigger Name: {tName})"); return new TriggerResult { Profile = kvp.Key, Action = t.Value, Platform = platform, Details = detail }; }
                    }
                }
            }
            return new TriggerResult { Profile = null, Action = null, Platform = platform, Details = detail };
        }
    }

#endregion

#region CPHAdapter
    /// <summary>
    /// Real implementation of IGiveawayCPH that forwards calls to the actual Streamer.bot CPH instance.
    /// Uses explicit method resolution to avoid AmbiguousMatchException.
    /// </summary>
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified
    public class CPHAdapter : IGiveawayCPH
    {
        private readonly object _cph;
        private readonly Type _t;
        public FileLogger Logger { get; set; }

        // Developer Controls
        /// <summary>
        /// Controls whether GetGlobalVar calls are logged to TRACE.
        /// Useful to disable for reducing log spam while keeping other reflection logs.
        /// </summary>
        private const bool LogReflectionGetGlobalVar = false;
        /// <summary>
        /// Tracks which global variables have been accessed/modified to optimize updates.
        /// </summary>
        private readonly HashSet<string> _touchedGlobalVars = new HashSet<string>(StringComparer.OrdinalIgnoreCase);



        /// <summary>
        /// Initializes a new instance of the <see cref="CPHAdapter"/> class.
        /// </summary>
        /// <param name="cph">The raw Streamer.bot CPH object.</param>
        /// <param name="args">The arguments dictionary from the action context.</param>
        public CPHAdapter(object cph, Dictionary<string, object> args)
        {
            _cph = cph;
            _t = _cph.GetType();
            _args = args ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Displays a toast notification in Streamer.bot.
        /// </summary>
        /// <param name="title">Title of the notification.</param>
        /// <param name="message">Body message of the notification.</param>
        public void ShowToastNotification(string title, string message)
        {
#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
             Console.WriteLine($"[TOAST] {title}: {message}");
#else
             try
             {
                 // Use InvokeMember with OptionalParamBinding to handle optional parameters (e.g. icon path)
                 // This avoids using 'dynamic' which requires Microsoft.CSharp.dll reference (CS0656)
                 _t.InvokeMember("ShowToastNotification",
                     System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.OptionalParamBinding,
                     null, _cph, new object[] { title, message });
             }
             catch (Exception ex)
             {
                 try { _t.GetMethod("LogDebug", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[CPH] Toast failed: " + ex.Message }); } catch { }
             }
#endif
        }

        private Dictionary<string, object> _args;

        /// <summary>
        /// Gets or sets the arguments dictionary for the current action.
        /// Mirrors the CPH.Args dictionary for mockability.
        /// </summary>
        public Dictionary<string, object> Args
        {
            get
            {
                // In a real environment, CPH.Args isn't directly exposed as a property on CPH usually,
                // but accessed via CPH.TryGetArg or similar. However, tests mock CPHAdapter.
                // For this adapter, we mirror the internal dictionary or re-fetch.
                return _args;
            }
            set { _args = value; }
        }


        /// <summary>
        /// Clears the set of "touched" or updated variables for the current cycle.
        /// </summary>
        public void ResetTouchedVars() { _touchedGlobalVars.Clear(); }

        /// <summary>
        /// Returns a list of global variables that were updated during the current cycle.
        /// Used for pruning orphans.
        /// </summary>
        public IEnumerable<string> GetTouchedVars() { return _touchedGlobalVars; }
        /// <summary>
        /// Determines if a variable name indicates it is managed by this bot.
        /// Supports both new 'Giveaway ' and legacy 'GiveawayBot_' prefixes for backward compatibility and cleanup.
        /// </summary>
        public static bool IsManagedVariable(string name)
        {
            return name != null && (name.StartsWith(GiveawayConstants.GlobalVarPrefix, StringComparison.OrdinalIgnoreCase) ||
                                    // Identify all profile-related variables (Giveaway {Profile} ...)
                                    name.StartsWith(GiveawayConstants.ProfileVarBase + " {", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Marks a global variable as "touched" (active) to prevent pruning.
        /// </summary>
        public void TouchGlobalVar(string name) { if (IsManagedVariable(name)) _touchedGlobalVars.Add(name); }

        /// <summary>
        /// Helper to find methods safely via reflection, handling overloads.
        /// </summary>
        /// <param name="name">Method name.</param>
        /// <param name="paramCount">Number of parameters.</param>
        /// <returns>The MethodInfo if found, otherwise null.</returns>
        private MethodInfo GetMethod(string name, int paramCount)
        {
            var methods = _t.GetMethods().Where(m => m.Name == name && m.GetParameters().Length == paramCount).ToList();
            if (methods.Count == 0) return null;
            if (methods.Count == 1) return methods[0];
            // If multiple, prioritize one with string as first param
            return methods.FirstOrDefault(m => m.GetParameters()[0].ParameterType == typeof(string)) ?? methods[0];
        }

        private MethodInfo GetGenericMethod(string name, int paramCount) =>
            _t.GetMethods().FirstOrDefault(m => m.Name == name && m.IsGenericMethod && m.GetParameters().Length == paramCount);

        /// <summary>
        /// Dumps all available methods on the underlying CPH object to the log for debugging purposes.
        /// </summary>
        public void DumpMethods()
        {
            try
            {
                var names = string.Join(", ", _t.GetMethods().Select(m => $"{m.Name}({m.GetParameters().Length})").Distinct().OrderBy(x => x));
                LogInfo($"[Diagnostic] Available CPH Methods: {names}");
            }
            catch { }
        }

        private readonly Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();

        /// <summary>Logs an informational message to the Streamer.bot log.</summary>
        public void LogInfo(string m) { Logger?.LogInfo(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogInfo", new object[] { m }, 1); }
        /// <summary>Logs a warning message to the Streamer.bot log.</summary>
        public void LogWarn(string m) { Logger?.LogWarn(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogWarn", new object[] { m }, 1); }
        /// <summary>Logs a debug message to the Streamer.bot log.</summary>
        public void LogDebug(string m) { Logger?.LogDebug(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
        /// <summary>Logs an error message to the Streamer.bot log.</summary>
        public void LogError(string m) { Logger?.LogError(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogError", new object[] { m }, 1); }
        /// <summary>Logs a verbose message (mapped to Debug in SB).</summary>
        public void LogVerbose(string m) { Logger?.LogVerbose(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
        /// <summary>Logs a trace message (mapped to Debug in SB).</summary>
        public void LogTrace(string m) { Logger?.LogTrace(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
        /// <summary>Logs a fatal error message (mapped to Error in SB with prefix).</summary>
        public void LogFatal(string m) { Logger?.LogFatal(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogError", new object[] { "[FATAL] " + m }, 1); }

#pragma warning disable IDE0028, IDE0300 // Local containment

        /// <summary>
        /// Invokes a method on the underlying CPH object via reflection safely.
        /// Handles method resolution and error catching.
        /// </summary>
        /// <param name="name">Name of the method to invoke.</param>
        /// <param name="args">Arguments to pass.</param>
        /// <param name="paramCount">Number of parameters expected (for overload resolution).</param>
        /// <returns>The return value of the method, or null on failure.</returns>
        private object InvokeSafe(string name, object[] args, int paramCount)
        {
#pragma warning disable IDE0028, IDE0300 // Local containment
            try
            {
                string cacheKey = $"{name}_{paramCount}";
                if (!_methodCache.TryGetValue(cacheKey, out var m))
                {
                    m = GetMethod(name, paramCount);
                    if (m != null) _methodCache[cacheKey] = m;
                }

                if (m == null) return null;
                // Use safe string conversion for args to avoid null issues in string.Join
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable CA1846 // Prefer AsSpan over Substring
                string[] argStrings;
                if (args != null)
                {
                    argStrings = new string[args.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        string val = args[i]?.ToString() ?? "null";
#pragma warning disable
                        if (val.Length > 200) val = val.Substring(0, 200) + "...[TRUNCATED]";
#pragma warning restore
                        argStrings[i] = val;
                    }
                }
                else
                {
                    argStrings = Array.Empty<string>();
                }
                Logger?.LogTrace(this, "Reflect", string.Format("Invoke: {0}({1})", name, string.Join(", ", argStrings)));
                return m.Invoke(_cph, args);
            }
            catch (Exception ex)
            {
                // Fallback to internal bot logging if reflection fails
                try { _t.GetMethod("LogError", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[ReflectError] " + name + ": " + ex.Message }); } catch { }
                return null;
            }
        }

        /// <summary>
        /// Attempts to get an argument value from the action context.
        /// </summary>
        /// <typeparam name="T">The expected type of the argument.</typeparam>
        /// <param name="n">The name of the argument.</param>
        /// <param name="v">When this method returns, contains the value of the argument, or default(T) if not found.</param>
        /// <returns>True if the argument was found and successfully converted; otherwise, false.</returns>
        public bool TryGetArg<T>(string n, out T v)
        {
            v = default(T);
            try
            {
                var m = GetGenericMethod("TryGetArg", 2)?.MakeGenericMethod(typeof(T));
                if (m == null) return false;
                object[] args = new object[] { n, default(T) };
                object res = m.Invoke(_cph, args);
                bool r = res != null && (bool)res;
                if (r && args[1] != null)
                {
                    try { v = (T)Convert.ChangeType(args[1], typeof(T)); } catch { v = (T)args[1]; }
                }
                return r;
            }
            catch (Exception ex)
            {
                try { _t.GetMethod("LogError", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[ReflectError] TryGetArg<" + typeof(T).Name + ">: " + ex.Message }); } catch (Exception) { }
                return false;
            }
        }

        /// <summary>
        /// Retrieves the value of a global variable from Streamer.bot.
        /// </summary>
        /// <typeparam name="T">The type of the variable value.</typeparam>
        /// <param name="n">The name of the variable.</param>
        /// <param name="p">Whether to persist the variable (default: true).</param>
        /// <returns>The confirmed value of the variable, or default(T) if not found/error.</returns>
        public T GetGlobalVar<T>(string n, bool p = true)
        {
            try
            {
                var m = GetGenericMethod("GetGlobalVar", 2)?.MakeGenericMethod(typeof(T));
                if (m == null) return default(T);
                var res = m.Invoke(_cph, new object[] { n, p });
                var val = res == null ? default(T) : (T)Convert.ChangeType(res, typeof(T));

                // Explicitly check LogLevel to ensure suppression even if Logger doesn't filter correctly
                bool isTrace = string.Equals(GiveawayManager.StaticConfig?.Globals?.LogLevel, "TRACE", StringComparison.OrdinalIgnoreCase);
#pragma warning disable CS0162 // Unreachable code detected
                if (isTrace && LogReflectionGetGlobalVar) Logger?.LogTrace(this, "Reflect", $"GetGlobalVar<{typeof(T).Name}>('{n}', {p}) => {val}");
#pragma warning restore CS0162 // Unreachable code detected

                return val;
            }
            catch (Exception ex)
            {
                // Fallback to direct invoke if convert fails (for objects) or other reflection issues
                try
                {
                    var m = GetGenericMethod("GetGlobalVar", 2)?.MakeGenericMethod(typeof(T));
                    var res = m?.Invoke(_cph, new object[] { n, p });
                    var val = res == null ? default(T) : (T)res;
#pragma warning disable CS0162 // Unreachable code detected
                    if (LogReflectionGetGlobalVar) Logger?.LogTrace(this, "Reflect", $"GetGlobalVar<{typeof(T).Name}>('{n}', {p}) => {val} (Fallback)");
#pragma warning restore CS0162 // Unreachable code detected
                    Logger?.LogError(this, "Reflect", $"GetGlobalVar Exception: {ex.Message}");
                    return val;
                }
                catch (Exception nestedEx)
                {
                    try { _t.GetMethod("LogError", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[ReflectError] GetGlobalVar: " + nestedEx.Message }); } catch { }
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Sets a global variable value.
        /// </summary>
        /// <param name="n">The name of the variable.</param>
        /// <param name="v">The value to set.</param>
        /// <param name="p">Whether to persist the variable (default: true).</param>
        /// <remarks>
        /// Automatically marks the variable as "touched" to prevent it from being pruned during cleanup.
        /// </remarks>
        public void SetGlobalVar(string n, object v, bool p = true)
        {
            InvokeSafe("SetGlobalVar", new object[] { n, v, p }, 3);
            TouchGlobalVar(n);
        }

        /// <summary>
        /// Retrieves a list of all current global variable names.
        /// Used for identifying orphaned variables that should be deleted.
        /// </summary>
        /// <param name="p">Whether to check persisted variables (default: true).</param>
        /// <returns>A list of variable names.</returns>
        public List<string> GetGlobalVarNames(bool p = true)
        {
            var names = new List<string>();
            try
            {
                var m = GetMethod("GetGlobalVarValues", 1);
                if (m == null) return names;

                var rawList = m.Invoke(_cph, new object[] { p });
                var list = rawList as System.Collections.IEnumerable;
                if (list == null) return names;

                foreach (var item in list)
                {
                    if (item == null) continue;
                    var prop = item.GetType().GetProperty("VariableName");
                    var name = prop?.GetValue(item) as string;
                    if (!string.IsNullOrEmpty(name)) names.Add(name);
                }
            }
            catch (Exception ex)
            {
                LogError($"[Reflect] GetGlobalVarNames failed: {ex.Message}");
            }
            return names;
        }

        /// <summary>
        /// Removes a global variable.
        /// </summary>
        /// <param name="n">The name of the variable to unset.</param>
        /// <param name="p">Whether it is a persisted variable (default: true).</param>
        public void UnsetGlobalVar(string n, bool p = true) { InvokeSafe("UnsetGlobalVar", new object[] { n, p }, 2); }

        /// <summary>
        /// Retrieves the value of a user-specific variable.
        /// </summary>
        /// <typeparam name="T">The type of the variable value.</typeparam>
        /// <param name="u">The user ID (or login).</param>
        /// <param name="n">The variable name.</param>
        /// <param name="p">Whether to persist (default: true).</param>
        /// <returns>The variable value, or default(T).</returns>
        public T GetUserVar<T>(string u, string n, bool p = true)
        {
            try
            {
                var m = GetGenericMethod("GetUserVar", 3)?.MakeGenericMethod(typeof(T));
                if (m == null) return default(T);
                var res = m.Invoke(_cph, new object[] { u, n, p });
                return res == null ? default(T) : (T)Convert.ChangeType(res, typeof(T));
            }
            catch (Exception ex)
            {
                try { _t.GetMethod("LogError", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[ReflectError] GetUserVar: " + ex.Message }); } catch { }
                return default(T);
            }
        }

        /// <summary>
        /// Sets a user-specific variable value.
        /// </summary>
        /// <param name="u">The user ID (or login).</param>
        /// <param name="n">The variable name.</param>
        /// <param name="v">The value to set.</param>
        /// <param name="p">Whether to persist (default: true).</param>
        public void SetUserVar(string u, string n, object v, bool p = true)
        {
            InvokeSafe("SetUserVar", new object[] { u, n, v, p }, 4);
        }

        /// <summary>
        /// Sends a chat message to the default platform (usually Twitch).
        /// Automatically appends the anti-loop token (zero-width space) to prevent determining self-triggering.
        /// </summary>
        /// <param name="m">The message to send.</param>
        /// <param name="b">Whether to use the bot account (default: true).</param>
        public void SendMessage(string m, bool b = true)
        {
            // Do not randomize here! It breaks system messages that contain commas.
            // Randomization is now handled by Loc.Get() and specific callers.
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            if (GetMethod("SendMessage", 3) != null) { InvokeSafe("SendMessage", new object[] { msg, b, true }, 3); return; }
            if (GetMethod("SendMessage", 2) != null) { InvokeSafe("SendMessage", new object[] { msg, b }, 2); return; }
            InvokeSafe("SendMessage", new object[] { msg }, 1);
        }

        /// <summary>
        /// Sends a chat message to YouTube.
        /// Automatically appends the anti-loop token.
        /// </summary>
        /// <param name="m">The message to send.</param>
        public void SendYouTubeMessage(string m)
        {
            // Do not randomize here.
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            InvokeSafe("SendYouTubeMessage", new object[] { msg }, 1);
        }

        /// <summary>
        /// Sends a chat message to Kick.
        /// Automatically appends the anti-loop token.
        /// </summary>
        /// <param name="m">The message to send.</param>
        public void SendKickMessage(string m)
        {
            // Do not randomize here.
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            InvokeSafe("SendKickMessage", new object[] { msg }, 1);
        }

        /// <summary>
        /// Sends a message to a Discord channel via Streamer.bot integration.
        /// </summary>
        /// <param name="channelId">The Discord Channel ID.</param>
        /// <param name="message">The message to send.</param>
        public void DiscordSendMessage(string channelId, string message)
        {
            // Native SB method: void DiscordSendMessage(string channelId, string message, bool tts = false);
            InvokeSafe("DiscordSendMessage", new object[] { channelId, message, false }, 3);
        }

        /// <summary>
        /// Sends a threaded reply to a specific Twitch chat message.
        /// </summary>
        /// <param name="message">Message content to send.</param>
        /// <param name="replyId">Message ID to reply to.</param>
        /// <param name="useBot">Use bot account (true) or broadcaster account (false).</param>
        /// <param name="fallback">Fallback to broadcast account if bot is not available.</param>
        public void TwitchReplyToMessage(string message, string replyId, bool useBot = true, bool fallback = true)
        {
            // Do not randomize here.
            InvokeSafe("TwitchReplyToMessage", new object[] { message, replyId, useBot, fallback }, 4);
        }


        /// <summary>
        /// Checks if the connected Twitch broadcaster account is currently live.
        /// </summary>
        public bool IsTwitchLive()
        {
            // Only check broadcaster status - bot streaming status is not relevant for giveaways
            object res = InvokeSafe("IsTwitchBroadcasterLive", Array.Empty<object>(), 0);
            bool isLive = res != null && (bool)res;
            if (res == null) LogTrace("[Platform] IsTwitchBroadcasterLive method not found or returned null");
            return isLive;
        }

        /// <summary>
        /// Checks if the connected YouTube broadcaster account is currently live.
        /// </summary>
        public bool IsYouTubeLive()
        {
            // Only check broadcaster status - bot streaming status is not relevant for giveaways
            object res = InvokeSafe("IsYouTubeBroadcasterLive", Array.Empty<object>(), 0);
            bool isLive = res != null && (bool)res;
            if (res == null) LogTrace("[Platform] IsYouTubeBroadcasterLive method not found or returned null");
            return isLive;
        }

        /// <summary>
        /// Checks if the connected Kick broadcaster account is currently live.
        /// </summary>
        public bool IsKickLive()
        {
            // Only check broadcaster status - bot streaming status is not relevant for giveaways
            object res = InvokeSafe("IsKickBroadcasterLive", Array.Empty<object>(), 0);
            bool isLive = res != null && (bool)res;
            if (res == null) LogTrace("[Platform] IsKickBroadcasterLive method not found or returned null");
            return isLive;
        }

        /// <summary>
        /// Checks if a user is following the channel on Twitch.
        /// </summary>
        /// <param name="userId">The user ID to check.</param>
        /// <returns>True if following, false otherwise.</returns>
        public bool TwitchIsUserFollower(string userId) => InvokeSafe("TwitchIsUserFollower", new object[] { userId, true }, 2) as bool? ?? false;

        /// <summary>
        /// Checks if a user is subscribed to the channel on Twitch.
        /// </summary>
        /// <param name="userId">The user ID to check.</param>
        /// <returns>True if subscribed, false otherwise.</returns>
        public bool TwitchIsUserSubscriber(string userId) => InvokeSafe("TwitchIsUserSubscriber", new object[] { userId, true }, 2) as bool? ?? false;

        /// <summary>
        /// Sets the URL of an OBS Browser Source.
        /// </summary>
        /// <param name="s">The scene name.</param>
        /// <param name="o">The source name.</param>
        /// <param name="u">The URL to set.</param>
        public void ObsSetBrowserSource(string s, string o, string u)
        {
            if (GetMethod("ObsSetBrowserSource", 4) != null) { InvokeSafe("ObsSetBrowserSource", new object[] { s, o, u, 0 }, 4); return; }
            InvokeSafe("ObsSetBrowserSource", new object[] { s, o, u }, 3);
        }

        /// <summary>
        /// Retrieves the event type that triggered the current action.
        /// </summary>
        /// <returns>The event type object (platform specific).</returns>
        public object GetEventType() { return InvokeSafe("GetEventType", Array.Empty<object>(), 0); }

        /// <summary>
        /// Runs a specified Streamer.bot action.
        /// </summary>
        public bool RunAction(string actionName, bool runImmediately = true)
        {
            object res = null;
            if (GetMethod("RunAction", 2) != null)
            {
                 res = InvokeSafe("RunAction", new object[] { actionName, runImmediately }, 2);
            }
            else
            {
                 // Fallback to 1-param if strict version differs (unlikely but safe)
                 res = InvokeSafe("RunAction", new object[] { actionName }, 1);
            }
            return res != null && (bool)res;
        }

#pragma warning restore IDE0090 // 'new' expression can be simplified
#pragma warning restore IDE0300 // Use collection expression
#pragma warning restore IDE0028 // Simplify collection initialization
    }

    /// <summary>
    /// Handles loading and saving the JSON configuration file.
    /// Supports hot-reloading if the file changes on disk.
    /// </summary>
#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified


#endregion

#region ConfigLoader
    public class ConfigLoader
    {
        public ConfigLoader() { }
        private GiveawayBotConfig _cached = new GiveawayBotConfig();
        private DateTime _lastLoad = DateTime.MinValue;
        private readonly string _dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config");
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");

        private string _lastError = "";

        // Profile name validation: alphanumeric + underscore, 1-32 chars (C# 7.3 compatible)
        private static readonly Regex _profileNameRegex = new Regex(@"^[A-Za-z0-9][A-Za-z0-9_]{0,31}$", RegexOptions.Compiled);
        private static readonly string[] _reservedNames = new string[] { "Globals", "_instructions", "_trigger_prefixes_help", "_help", "defaults" };


        /// <summary>
        /// Determines the current active RunMode (FileSystem, GlobalVar, ReadOnlyVar, Mirror).
        /// defaults to "FileSystem" if not set.
        /// </summary>
        public static string GetRunMode(CPHAdapter adapter)
        {
            var mode = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalRunMode, true);
            if (string.IsNullOrEmpty(mode)) return "FileSystem";
            // RUN MODES:
            // "FileSystem": Standard mode. Configs are read from/written to JSON files on disk. safe and persistent.
            // "GlobalVar": Memory-only mode. Configs live in Streamer.bot variables. Fast, but lost if Streamer.bot crashes without persistence.
            // "Mirror": Hybrid mode. Reads from Global Vars (for remote updates) but backs up to Disk. Best for remote control setups.
            // "ReadOnlyVar": Strict mode. Reads from Global Vars but NEVER writes back. Useful for slave bots.
            if (mode.Equals("FileSystem", StringComparison.OrdinalIgnoreCase)) return "FileSystem";
            if (mode.Equals("GlobalVar", StringComparison.OrdinalIgnoreCase)) return "GlobalVar";
            if (mode.Equals("ReadOnlyVar", StringComparison.OrdinalIgnoreCase)) return "ReadOnlyVar";
            if (mode.Equals("Mirror", StringComparison.OrdinalIgnoreCase)) return "Mirror";
            return "FileSystem";
        }

        /// <summary>
        /// Reads the raw configuration JSON string from the configured storage source.
        /// </summary>
        /// <param name="adapter">CPH adapter for accessing global variables.</param>
        /// <param name="forceDisk">If true, forces reading from disk regardless of run mode.</param>
        /// <returns>The raw JSON string, or null if not found.</returns>
        private string ReadConfigText(CPHAdapter adapter, bool forceDisk = false)
        {
            if (!forceDisk)
            {
                string mode = GetRunMode(adapter);
                // Fallback Strategy:
                // If we are in a mode that supports Global Variables, we try to read from memory first.
                // This allows the configuration to be updated remotely (e.g. via Deck/CPH) without touching files.
                if (mode == "GlobalVar" || mode == "ReadOnlyVar" || mode == "Mirror")
                {
                    var json = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalConfig, true);
                    if (string.IsNullOrEmpty(json) && mode != "Mirror")
                        adapter.LogWarn($"[Config] RunMode is {mode} but '{GiveawayConstants.GlobalConfig}' global variable is empty!");

                    // If valid JSON was found in variables, return it immediately.
                    // In Mirror mode, if it's empty, we proceed to read from disk below (Self-Healing).
                    if (!string.IsNullOrEmpty(json)) return json;
                }
            }
            return File.Exists(_path) ? File.ReadAllText(_path) : null;
        }

        /// <summary>
        /// Forces the configuration to be reloaded from storage on the next request.
        /// </summary>
        public void InvalidateCache() => _lastLoad = DateTime.MinValue;

        /// <summary>
        /// Synchronously writes configuration JSON to storage.
        /// Maintained for backward compatibility and initial setup.
        /// </summary>
        public void WriteConfigText(CPHAdapter adapter, string json)
        {
            try
            {
                // Synchronous wrapper or implementation for legacy calls
                string mode = GetRunMode(adapter);
                if (mode == "ReadOnlyVar") return;

                if (mode == "GlobalVar" || mode == "Mirror")
                {
                    // Prevent "Giveaway GLobal Config" from cluttering the persistent variables list in ANY mode.
                    // This variable is still available in memory for GlobalVar mode, but won't be saved to DB.
                    bool persist = false;
                    adapter.SetGlobalVar(GiveawayConstants.GlobalConfig, json, persist);

                    // Always persist the timestamp so we know when to reload
                    adapter.SetGlobalVar(GiveawayConstants.GlobalConfigLastWrite, DateTime.UtcNow.ToString("o"), true);

                    if (mode == "GlobalVar") return;
                }

                if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

                // Atomic Write
                string tempPath = _path + ".tmp";
                File.WriteAllText(tempPath, json);
                if (File.Exists(_path)) File.Delete(_path);
                File.Move(tempPath, _path);
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Config] Failed to write config (Sync): {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously writes configuration JSON to storage (Disk and/or GlobalVar).
        /// </summary>
        public async Task WriteConfigTextAsync(CPHAdapter adapter, string json)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                string mode = GetRunMode(adapter);
                adapter.LogTrace($"[Config] WriteConfigTextAsync called. Mode: {mode}, Json Length: {json?.Length ?? 0}");

                if (mode == "ReadOnlyVar")
                {
                    adapter.LogWarn("[Config] Write attempted while in ReadOnlyVar mode. Operation skipped.");
                    return;
                }

                // adapter.LogInfo($"[Config] DEBUG WRITE JSON (WriteAsync): {json}");

                if (mode == "GlobalVar" || mode == "Mirror")
                {
                    adapter.LogDebug($"[Config] Saving configuration to '{GiveawayConstants.GlobalConfig}' global variable (Mode: {mode}).");
                    // Do not persist the JSON blob to DB to avoid hitting limits/clutter.
                    adapter.SetGlobalVar(GiveawayConstants.GlobalConfig, json, false);
                    adapter.SetGlobalVar(GiveawayConstants.GlobalConfigLastWrite, DateTime.UtcNow.ToString("o"), true);
                    if (mode == "GlobalVar") return;
                }

                // FileSystem or Mirror mode
                if (!Directory.Exists(_dir))
                {
                    adapter.LogDebug($"[Config] Creating config directory: {_dir}");
                    Directory.CreateDirectory(_dir);
                }

                using (var writer = new StreamWriter(_path, false))
                {
                    await writer.WriteAsync(json);
                }
                adapter.LogDebug($"[Config] Configuration successfully saved to disk: {_path}");
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Config] Failed to write config to file: {ex.Message}");
            }
            finally
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds > 500)
                {
                    adapter.LogWarn($"[Performance] Config Write took {sw.ElapsedMilliseconds}ms (>500ms). Check disk I/O latency.");
                }
            }
        }

        private string _lastLoadedJson = null;

        /// <summary>
        /// Ensures that the Profiles dictionary in the config object uses case-insensitive keys.
        /// Also fixes up the Triggers dictionary within each profile.
        /// </summary>
        /// <param name="c">The configuration object to fix.</param>
        private static void EnsureCaseInsensitive(GiveawayBotConfig c)
        {
            if (c == null) return;
            // Bot Config Profiles
            // Why Custom Comparer?
            // C# Dictionaries are Case-Sensitive by default ("Main" != "main").
            // User input commands are often messy. We force the dictionary to use 'OrdinalIgnoreCase'.
            // This ensures looking up "main", "MAIN", or "Main" always finds the same profile.
            if (c.Profiles != null && !(c.Profiles.Comparer is StringComparer scp && scp == StringComparer.OrdinalIgnoreCase))
            {
                var newProfiles = new Dictionary<string, GiveawayProfileConfig>(StringComparer.OrdinalIgnoreCase);
                foreach (var kv in c.Profiles) newProfiles[kv.Key] = kv.Value;
                // Use reflection to set it back if setter is private/missing, or just assume we have a setter now
                try {
                    c.GetType().GetProperty("Profiles")?.SetValue(c, newProfiles);
                } catch {
                    // Fallback: Clear and refill if we can't set
                    c.Profiles.Clear();
                    foreach(var kv in newProfiles) c.Profiles[kv.Key] = kv.Value;
                }
            }

            // Profile Triggers
            if (c.Profiles != null)
            {
                foreach (var p in c.Profiles.Values)
                {
                    if (p.Triggers != null && !(p.Triggers.Comparer is StringComparer sct && sct == StringComparer.OrdinalIgnoreCase))
                    {
                        var newTriggers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var kv in p.Triggers) newTriggers[kv.Key] = kv.Value;
                        try {
                            p.GetType().GetProperty("Triggers")?.SetValue(p, newTriggers);
                        } catch {
                            p.Triggers.Clear();
                            foreach(var kv in newTriggers) p.Triggers[kv.Key] = kv.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates configuration values and clamps them to safe ranges.
        /// Also removes profiles with invalid names to prevent system errors.
        /// </summary>
        private static void ValidateConfig(CPHAdapter adapter, GiveawayBotConfig c)
        {
            if (c == null) return;

            // Profile Names & Reserved Keywords
            if (c.Profiles != null)
            {
                var invalidKeys = new List<string>();
                foreach (var profileKey in c.Profiles.Keys)
                {
                    if (!_profileNameRegex.IsMatch(profileKey))
                    {
                        adapter.LogError($"[Config] Invalid Profile Name '{profileKey}'. Must be alphanumeric/underscore (max 32 chars). Ignoring.");
                        invalidKeys.Add(profileKey);
                    }
                    else if (_reservedNames.Contains(profileKey, StringComparer.OrdinalIgnoreCase))
                    {
                        adapter.LogError($"[Config] Reserved Profile Name '{profileKey}'. This name is system-locked. Ignoring.");
                        invalidKeys.Add(profileKey);
                    }
                }
                foreach (var key in invalidKeys) c.Profiles.Remove(key);
            }

            // Clamp Values (Globals)
            if (c.Globals != null)
            {
               if (c.Globals.LogPruneProbability < 0) c.Globals.LogPruneProbability = 0;
               if (c.Globals.LogPruneProbability > 100) c.Globals.LogPruneProbability = 100;

               if (c.Globals.LogRetentionDays < 1) c.Globals.LogRetentionDays = 1;
               if (c.Globals.MessageIdCacheTtlMinutes < 1) c.Globals.MessageIdCacheTtlMinutes = 1;
            }

            // Clamp Values (Profiles)
            if (c.Profiles != null)
            {
                foreach (var profile in c.Profiles.Values)
                {
                     // Throttle logic
                     if (profile.DumpEntriesOnEntryThrottleSeconds < 5)
                     {
                         adapter.LogWarn($"[Config] DumpEntriesOnEntryThrottle too low ({profile.DumpEntriesOnEntryThrottleSeconds}s), clamped to 5s");
                         profile.DumpEntriesOnEntryThrottleSeconds = 5;
                     }
                     if (profile.DumpEntriesOnEntryThrottleSeconds > 300)
                     {
                         adapter.LogWarn($"[Config] DumpEntriesOnEntryThrottle too high ({profile.DumpEntriesOnEntryThrottleSeconds}s), clamped to 300s");
                         profile.DumpEntriesOnEntryThrottleSeconds = 300;
                     }

                     if (profile.WinChance < 0) profile.WinChance = 0;
                     if (profile.SubLuckMultiplier < 1) profile.SubLuckMultiplier = 1;
                }
            }
        }

        /// <summary>
        /// Retrieves the current configuration object, managing caching and auto-reloading.
        /// Performs synchronization between disk and global variables based on RunMode.
        /// </summary>
        public GiveawayBotConfig GetConfig(CPHAdapter adapter)
        {
            try
            {
                string mode = GetRunMode(adapter);
                bool forceFileReload = false;
                bool forceGlobalReload = false;
                string preloadedGlobalJson = null; // Optimization: Store fetched variable to avoid double-fetch

                string lastLoadStr = _lastLoad == DateTime.MinValue ? "Never" : _lastLoad.ToString();
                adapter.LogTrace($"[Config] GetConfig check. Mode: {mode}, LastLoad: {lastLoadStr}");

                // Check for Disk Changes (Priority for FileSystem/Mirror)
                if ((mode == "FileSystem" || mode == "Mirror") && File.Exists(_path))
                {
                    var lastWrite = File.GetLastWriteTimeUtc(_path);
                    if (lastWrite > _lastLoad)
                    {
                        // Check if Global is even NEWER in Mirror mode
                        bool globalIsNewer = false;
                        if (mode == "Mirror")
                        {
                            var globalTimeStr = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalConfigLastWrite, true);
                            if (DateTime.TryParse(globalTimeStr, out var globalTime))
                            {
                                if (globalTime > lastWrite)
                                {
                                    globalIsNewer = true;
                                    adapter.LogTrace($"[Config] Mirror: GlobalVar timestamp ({globalTime}) is newer than Disk ({lastWrite}). skipping Disk reload.");
                                }
                            }
                        }

                        if (!globalIsNewer)
                        {
                            adapter.LogTrace($"[Config] Mirror/FS: Disk file is newer ({lastWrite} > {_lastLoad}). Reloading...");
                            forceFileReload = true;
                        }
                    }
                }

                // Check for GlobalVar Changes (Mirror Mode Only)
                if (mode == "Mirror" && !forceFileReload)
                {
                    // Optimization: Check timestamp FIRST to avoid fetching huge JSON string if not needed
                    var globalTimeStr = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalConfigLastWrite, true);
                    bool globalTimestampChanged = false;

                    if (DateTime.TryParse(globalTimeStr, out var globalTime))
                    {
                        var lastWrite = File.Exists(_path) ? File.GetLastWriteTimeUtc(_path) : DateTime.MinValue;
                        adapter.LogInfo($"[Config] Mirror Path 2: Global={globalTime:o}, LastLoad={_lastLoad:o}, Disk={lastWrite:o}");

                        if (globalTime > _lastLoad)
                        {
                            globalTimestampChanged = true;
                            // Also check if it's newer than disk (Emergency conflict resolution)
                             if (File.Exists(_path))
                             {
                                 adapter.LogInfo($"[Config] Mirror: GlobalVar timestamp ({globalTime}) > Disk ({lastWrite}). Forcing Global Reload.");
                                     forceGlobalReload = true;
                                 }
                             }
                        }

                    // Trust the timestamp if present
                    if (globalTimestampChanged || string.IsNullOrEmpty(globalTimeStr))
                    {
                         preloadedGlobalJson = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalConfig, true);
                         if (!string.IsNullOrEmpty(preloadedGlobalJson) && preloadedGlobalJson != _lastLoadedJson)
                         {
                             adapter.LogTrace("[Config] Mirror: Global Variable content changed. Syncing.");
                             forceGlobalReload = true;
                         }
                    }
                }

                // Perform Reload if needed
                // Check configured interval (default 5 seconds)
                int reloadInterval = GiveawayManager.GlobalConfig?.Globals.ConfigReloadIntervalSeconds ?? 5;

                // Only use time-based reload for GlobalVar mode (polling).
                // filesystem/Mirror modes have explicit change detection above (Timestamp/FileWatcher logic).
                bool timeExpired = (DateTime.UtcNow - _lastLoad).TotalSeconds > reloadInterval;
                bool shouldReload = forceFileReload || forceGlobalReload;

                if (mode != "FileSystem" && mode != "Mirror" && timeExpired)
                {
                    shouldReload = true;
                }

                if (shouldReload)
                {
                    string json = null;
                    if (forceFileReload)
                    {
                        // Disk reload overrides global in memory
                        json = ReadConfigText(adapter, true);

                        // If we are in Mirror mode and the file is newer, immediately sync the GlobalVar
                        if (mode == "Mirror" && !string.IsNullOrEmpty(json) && json != _lastLoadedJson)
                        {
                            adapter.LogInfo("[Config] Mirror: Disk override detected. Updating Global Variable to match.");
                            adapter.SetGlobalVar(GiveawayConstants.GlobalConfig, json, true);
                            try
                            {
                                adapter.SetGlobalVar(GiveawayConstants.GlobalConfigLastWrite, File.GetLastWriteTimeUtc(_path).ToString("o"), true);
                            }
                            catch {}
                        }
                    }
                    else if (forceGlobalReload)
                    {
                        // Use preloaded JSON if available (Smart Fetch)
                        json = preloadedGlobalJson ?? ReadConfigText(adapter, false);

                        // If GlobalVar changed, sync it to disk immediately in Mirror mode
                        if (mode == "Mirror" && !string.IsNullOrEmpty(json))
                        {
                            adapter.LogInfo("[Config] Mirror: Global Variable override detected. Updating local file to match.");
                            adapter.LogInfo($"[Config] DEBUG WRITE JSON (GetConfig): {json}");
                            try
                            {
                                File.WriteAllText(_path, json);
                                // Update last load to match file time so we don't reload immediately
                                _lastLoad = DateTime.UtcNow;
                                adapter.LogDebug($"[Config] DEBUG WRITE COMPLETE. Exists: {File.Exists(_path)}");
                            }
                            catch (Exception ex) { adapter.LogError($"[Config] Mirror: Local file sync failed: {ex.Message}"); }
                        }
                    }
                    else
                    {
                        // Routine check (GlobalVar Mode polling)
                         json = ReadConfigText(adapter, false);
                    }

                    if (!string.IsNullOrEmpty(json))
                    {
                        // Check if content actually changed to avoid unnecessary deserialization
                        if (json != _lastLoadedJson)
                        {
                            var c = JsonConvert.DeserializeObject<GiveawayBotConfig>(json, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
                            if (c != null)
                            {
                                // Bootstrap RunMode: Initialize Streamer.bot variable from file if variable is missing
                                if (c.Globals != null && !string.IsNullOrEmpty(c.Globals.RunMode))
                                {
                                    var currentMode = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalRunMode, true);
                                    if (string.IsNullOrEmpty(currentMode))
                                    {
                                        adapter.LogInfo($"[Config] Bootstrapping RunMode from file: '{c.Globals.RunMode}'");
                                        adapter.SetGlobalVar(GiveawayConstants.GlobalRunMode, c.Globals.RunMode, true);
                                    }
                                    else if (currentMode != c.Globals.RunMode)
                                    {
                                        // Effective mode (Global) overrides File
                                        c.Globals.RunMode = currentMode;
                                    }
                                    // After bootstrap, Mirror mode's bidirectional sync handles all future changes
                                }

                                if (forceFileReload || mode == "FileSystem") adapter.LogDebug($"[Config] Sync: Loaded from disk: {_path}");
                                else if (forceGlobalReload) adapter.LogDebug("[Config] Sync: Loaded from Global Variable.");

                                // Fix up dictionaries if they regressed to case-sensitive
                                EnsureCaseInsensitive(c);

                                // Validate and clamp values using strict schema rules
                                ValidateConfig(adapter, c);

                                _cached = c;
                                _lastLoadedJson = json;
                                adapter.LogDebug($"[ConfigLoader] [GetConfig] Deserialized OK. Profiles Found: {string.Join(", ", _cached.Profiles.Keys)}");
                                SetStatus(adapter, "Valid ✅");
                                _lastError = "";
                                adapter.SetGlobalVar(GiveawayConstants.GlobalLastConfigErrors, null, true); // Clear any previous error state
                                RunFuzzyCheck(adapter, _cached);
                            }
                        }
                    }
                    else if (mode == "Mirror" && string.IsNullOrEmpty(json) && File.Exists(_path))
                    {
                        // Emergency Restore: If GlobalVar is empty but file exists, sync it back
                        adapter.LogWarn("[ConfigLoader] [GetConfig] Mirror: Global Variable is empty! Restoring from local file.");
                        json = File.ReadAllText(_path);
                        adapter.SetGlobalVar(GiveawayConstants.GlobalConfig, json, true);
                        _lastLoadedJson = json;
                    }
                    _lastLoad = DateTime.UtcNow;
                }
            }
            catch (Exception ex)
            {
                SetStatus(adapter, $"❌ Error: {ex.Message}");
                if (_lastError != ex.Message)
                {
                    _lastError = ex.Message;
                    adapter.LogError($"[ConfigLoader] [GetConfig] Sync Failed: {ex.Message}");
                    adapter.SetGlobalVar(GiveawayConstants.GlobalLastConfigErrors, ex.Message, true);
                }
            }
            return _cached;
        }

        /// <summary>
        /// Resets a specific profile to its default settings.
        /// </summary>
        public async Task<(bool Success, string Error)> ResetProfileAsync(CPHAdapter adapter, string profileName)
        {
            try
            {
                var config = GetConfig(adapter);
                if (config == null || config.Profiles == null) return (false, "Configuration not loaded.");

                if (!config.Profiles.ContainsKey(profileName)) return (false, $"Profile '{profileName}' not found.");

                // Create default profile config
                var defaultProfile = new GiveawayProfileConfig();
                config.Profiles[profileName] = defaultProfile; // Replace with default

                // Save
                _cached = config;
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                await WriteConfigTextAsync(adapter, json);

                adapter.LogInfo($"[ConfigLoader] [ResetProfileAsync] Profile '{profileName}' has been reset to defaults.");
                return (true, null);
            }
            catch (Exception ex)
            {
                adapter.LogError($"[ConfigLoader] [ResetProfileAsync] Failed to reset profile '{profileName}': {ex.Message}");
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Exports a profile configuration to a JSON file.
        /// </summary>
        public async Task<(bool success, string message, string path)> ExportProfileAsync(CPHAdapter adapter, string profileName)
        {
            try
            {
                var config = GetConfig(adapter);
                if (!config.Profiles.TryGetValue(profileName, out var profile)) return (false, $"Profile '{profileName}' not found", "");

                string exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "exports");
                if (!Directory.Exists(exportDir)) Directory.CreateDirectory(exportDir);

                string fileName = $"{profileName}_Export_{DateTime.UtcNow:yyyyMMdd_HHmm}.json";
                string fullPath = Path.Combine(exportDir, fileName);

                string json = JsonConvert.SerializeObject(profile, Formatting.Indented);

                using (var writer = new StreamWriter(fullPath, false))
                {
                    await writer.WriteAsync(json);
                }

                return (true, "Success", fileName);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, "");
            }
        }

        /// <summary>
        /// Imports a profile from a file or JSON string.
        /// </summary>
        public async Task<(bool success, string message)> ImportProfileAsync(CPHAdapter adapter, string source, string targetName)
        {
            try
            {
                string jsonContent = null;
                string usedTargetName = targetName;

                // Resolve Source (File vs JSON)
                string importDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "import");
                if (!Directory.Exists(importDir)) Directory.CreateDirectory(importDir);

                // Check if source is a file path
                bool isFile = false;
                string filePath = null;

                // Absolute path check
                if (File.Exists(source))
                {
                    isFile = true;
                    filePath = source;
                }
                // Relative path check
                else if (File.Exists(Path.Combine(importDir, source)))
                {
                    isFile = true;
                    filePath = Path.Combine(importDir, source);
                }
                // Check if user added .json extension
                else if (File.Exists(Path.Combine(importDir, source + ".json")))
                {
                    isFile = true;
                    filePath = Path.Combine(importDir, source + ".json");
                }

                if (isFile)
                {
                    // Offload file reading to thread pool to prevent blocking
                    using (var reader = File.OpenText(filePath))
                    {
                        jsonContent = await reader.ReadToEndAsync();
                    }

                    if (string.IsNullOrEmpty(usedTargetName))
                    {
                        usedTargetName = Path.GetFileNameWithoutExtension(filePath);
                        // Clean up export timestamps if present (e.g. MyProf_Export_2023...)
                        if (usedTargetName.Contains("_Export_"))
                            usedTargetName = usedTargetName.Substring(0, usedTargetName.IndexOf("_Export_"));
                    }
                }
                else
                {
                    // Treat as raw JSON
                    jsonContent = source;
                    if (string.IsNullOrEmpty(usedTargetName)) return (false, "Target profile name is required for raw JSON import.");
                }

                if (string.IsNullOrWhiteSpace(jsonContent)) return (false, "Empty content");

                // Deserialize & Validate
                GiveawayProfileConfig importedProfile = null;
                try
                {
                    importedProfile = JsonConvert.DeserializeObject<GiveawayProfileConfig>(jsonContent);
                }
                catch (Exception ex)
                {
                    return (false, $"Invalid JSON: {ex.Message}");
                }

                if (importedProfile == null) return (false, "Deserialization yielded null");

                // Validate critical fields
                if (importedProfile.MaxEntriesPerMinute <= 0) importedProfile.MaxEntriesPerMinute = 60; // Auto-fix

                // Save to Global Config
                var config = GetConfig(adapter);

                // Check for overwrite
                if (config.Profiles.ContainsKey(usedTargetName))
                {
                     // For now, fail to prevent accident (Require 'force' in future or different name)
                     // Or maybe we accept overwrite if user provided the name explicitly?
                     // Let's protect "Main" specifically or just fail.
                     return (false, $"Profile '{usedTargetName}' already exists. Please choose a different name.");
                }

                // Sanitize name
                if (!Regex.IsMatch(usedTargetName, @"^[A-Za-z0-9_]+$"))
                    return (false, "Invalid profile name (Alphanumeric only).");

                config.Profiles[usedTargetName] = importedProfile;

                // Write back
                string mode = GetRunMode(adapter); // Ensure we use current mode
                string newJson = JsonConvert.SerializeObject(config, Formatting.Indented);

                // Use existing write method
                if (mode == "GlobalVar" || mode == "Mirror") adapter.SetGlobalVar("Giveaway Global Config", newJson, true);
                if (mode == "FileSystem" || mode == "Mirror")
                {
                    if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
                    File.WriteAllText(_path, newJson);
                }

                InvalidateCache();
                return (true, $"Profile '{usedTargetName}' imported successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Import Logic Error: {ex.Message}");
            }
        }


        /// <summary>
        /// Updates the configuration status global variable.
        /// </summary>
        private static void SetStatus(CPHAdapter adapter, string s) => adapter.SetGlobalVar(GiveawayConstants.GlobalConfigStatus, s, true);

        /// <summary>
        /// Validates the current configuration for semantic errors (e.g., negative values).
        /// Returns a formatted validation status string.
        /// </summary>
        public string ValidateConfig(CPHAdapter adapter)
        {
            // Force a disk check for validation if file exists
            bool hasDiskOverride = false;
            if (File.Exists(_path))
            {
                var lastWrite = File.GetLastWriteTimeUtc(_path);
                if (lastWrite > _lastLoad) hasDiskOverride = true;
            }

            var json = ReadConfigText(adapter, hasDiskOverride);
            if (string.IsNullOrEmpty(json)) return "Config not found (Check RunMode)!";

            try
            {
                var c = JsonConvert.DeserializeObject<GiveawayBotConfig>(json, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

                // Semantic validation
                if (c != null)
                {
                    foreach (var p in c.Profiles)
                    {
                        if (p.Value.MaxEntriesPerMinute <= 0) return HandleValidationError(adapter, $"❌ Profile '{p.Key}': MaxEntriesPerMinute must be > 0");
                        if (p.Value.SubLuckMultiplier < 0) return HandleValidationError(adapter, $"❌ Profile '{p.Key}': SubLuckMultiplier cannot be negative");
                    }
                }

                adapter.SetGlobalVar("Giveaway Global LastConfigErrors", null, true); // Clear errors
                SetStatus(adapter, "Valid ✅");
                if (c != null) RunFuzzyCheck(adapter, c);
                return "Configuration is VALID ✅";
            }
            catch (JsonException jex) { return HandleValidationError(adapter, $"❌ JSON Error: {jex.Message}"); }
            catch (Exception ex) { return HandleValidationError(adapter, $"❌ Error: {ex.Message}"); }
        }

        /// <summary>
        /// Updates the configuration status global variable with an error message.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="msg">The error message to display.</param>
        /// <returns>The error message returned as a string.</returns>
        private static string HandleValidationError(CPHAdapter adapter, string msg)
        {
            SetStatus(adapter, msg);
            adapter.SetGlobalVar("Giveaway Global LastConfigErrors", msg, true);
            return msg;
        }

        /// <summary>
        /// Generates a default configuration file if none exists, or migrates an existing one.
        /// Keeps user profiles intact during migration.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        public void GenerateDefaultConfig(CPHAdapter adapter)
        {
            try
            {
                string existingJson = ReadConfigText(adapter);
                var blueprint = new GiveawayBotConfig();
                var latestInstructions = blueprint.Instructions;
                var latestHelp = blueprint.TriggerPrefixHelp;

                if (!string.IsNullOrEmpty(existingJson))
                {
#pragma warning disable IDE0028, IDE0300 // Local containment
                    // Safe Migration: Read existing config (from file or var)
                    blueprint.Profiles.Clear();
                    JsonConvert.PopulateObject(existingJson, blueprint, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

                    blueprint.Instructions = latestInstructions;
                    blueprint.TriggerPrefixHelp = latestHelp;

                    if (blueprint.Globals.EnabledPlatforms != null)
                    {
#pragma warning disable IDE0300 // Use collection expression
                        var unique = new HashSet<string>();
                        if (blueprint.Globals.EnabledPlatforms != null)
                        {
                            foreach (var p in blueprint.Globals.EnabledPlatforms) unique.Add(p);
                        }
                        blueprint.Globals.EnabledPlatforms = new List<string>();
                        blueprint.Globals.EnabledPlatforms.AddRange(unique);
                    }
                    adapter.Logger?.LogInfo(adapter, "Config", $"Migrated existing config. User profiles preserved, help metadata updated.");
                }
                else
                {
                    var weeklyProfile = new GiveawayProfileConfig();
                    weeklyProfile.Triggers.Clear();
                    weeklyProfile.Triggers["command:!weekly"] = "Entry";
                    blueprint.Profiles["Weekly"] = weeklyProfile;
                }
                WriteConfigText(adapter, JsonConvert.SerializeObject(blueprint, Formatting.Indented));
            }
            catch (Exception ex)
            {
                adapter.Logger?.LogError(adapter, "Config", $"Failed to generate/update config: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Creates a new profile with default settings asynchronously.
        /// Persists changes to disk/global vars immediately.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="name">The name of the new profile.</param>
        /// <returns>A tuple containing Success (bool) and ErrorMessage (string).</returns>
        public async Task<(bool Success, string ErrorMessage)> CreateProfileAsync(CPHAdapter adapter, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return (false, "Profile name cannot be empty");

            // Validation: profile name format
            if (!_profileNameRegex.IsMatch(name)) return (false, "Profile name must start with alphanumeric, allow underscores thereafter, 1-32 chars");

            // Validation: check reserved names (case-insensitive)
            foreach (string reserved in _reservedNames)
            {
                if (reserved.Equals(name, StringComparison.OrdinalIgnoreCase)) return (false, $"'{name}' is a reserved name");
            }

            var config = GetConfig(adapter);

            // Validation: check duplicate (case-insensitive)
            foreach (string existing in config.Profiles.Keys)
            {
                if (existing.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return (false, $"Profile '{existing}' already exists");
                }
            }

            await BackupConfigAsync(adapter);
            config.Profiles[name] = new GiveawayProfileConfig();
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache(); // Force reload
            adapter.Logger?.LogInfo(adapter, "Config", $"Profile created: {name}");
            return (true, "");
        }

        /// <summary>
        /// Deletes a profile and its associated data asynchronously.
        /// Creates a comprehensive backup of the profile config and state before deletion.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="name">The name of the profile to ignore.</param>
        /// <returns>A tuple containing Success (bool), ErrorMessage (string), and BackupPath (string).</returns>
        public async Task<(bool Success, string ErrorMessage, string BackupPath)> DeleteProfileAsync(CPHAdapter adapter, string name)
        {
            string backupPath = null;

            if (string.IsNullOrWhiteSpace(name)) return (false, "Profile name cannot be empty", "");
            if (name.Equals("Main", StringComparison.OrdinalIgnoreCase)) return (false, "Cannot join the void. 'Main' profile is eternal.", "");

            var config = GetConfig(adapter);
            if (!config.Profiles.TryGetValue(name, out var profile)) return (false, $"Profile '{name}' not found", "");

            // Note: Consider making async in future refactor (Optimization).
            try
            {
                string backupDir = Path.Combine(_dir, "backups", $"deleted_{name}_{DateTime.UtcNow:yyyyMMdd_HHmm}");
                if (!Directory.Exists(backupDir)) Directory.CreateDirectory(backupDir);

                // var profile = config.Profiles[name]; // Already retrieved via TryGetValue

                // Backup profile configuration
                string profileJson = JsonConvert.SerializeObject(profile, Formatting.Indented);
                using (var writer = new StreamWriter(Path.Combine(backupDir, "profile_config.json"))) await writer.WriteAsync(profileJson);

                // Backup active state if exists
                string statePath = Path.Combine(_dir, "..", "data", $"state_{name}.json");
                if (File.Exists(statePath))
                {
                    using (var source = File.OpenRead(statePath))
                    using (var dest = File.Create(Path.Combine(backupDir, "state.json")))
                        await source.CopyToAsync(dest);
                }

                // Backup Streamer.bot variables (if accessible)
                var varsBackup = new Dictionary<string, object>();
                string[] varKeys = new string[] {
                    $"Giveaway {name} IsActive",
                    $"Giveaway {name} EntryCount",
                    $"Giveaway {name} TicketCount",
                    $"Giveaway {name} Id",
                    $"Giveaway {name} WinnerName",
                    $"Giveaway {name} WinnerUserId"
                };

                foreach (string key in varKeys)
                {
                    var value = adapter.GetGlobalVar<object>(key, true);
                    if (value != null) varsBackup[key] = value;
                }

                if (varsBackup.Count > 0)
                {
                    string varsJson = JsonConvert.SerializeObject(varsBackup, Formatting.Indented);
                    using (var writer = new StreamWriter(Path.Combine(backupDir, "variables.json"))) await writer.WriteAsync(varsJson);
                }

                backupPath = backupDir;
                adapter.LogInfo($"[ConfigLoader] [DeleteProfileAsync] Profile deletion backup created: {backupPath}");
            }
            catch (Exception ex)
            {
                adapter.LogWarn($"[ConfigLoader] [DeleteProfileAsync] Backup creation failed (proceeding anyway): {ex.Message}");
                backupPath = $"(backup failed: {ex.Message})";
            }

            // Perform deletion
            await BackupConfigAsync(adapter); // Also create standard config backup
            config.Profiles.Remove(name);
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache();

            //  Unset all Streamer.bot variables for this profile
            adapter.SetGlobalVar($"Giveaway {name} IsActive", (string)null, true);
            adapter.SetGlobalVar($"Giveaway {name} EntryCount", (string)null, true);
            adapter.SetGlobalVar($"Giveaway {name} TicketCount", (string)null, true);
            adapter.SetGlobalVar($"Giveaway {name} Id", (string)null, true);
            adapter.SetGlobalVar($"Giveaway {name} WinnerName", (string)null, true);
            adapter.SetGlobalVar($"Giveaway {name} WinnerUserId", (string)null, true);

            adapter.Logger?.LogInfo(adapter, "Config", $"Profile deleted: {name}");
            return (true, "", backupPath);
        }

        /// <summary>
        /// Clones an existing profile to a new profile asynchronously.
        /// Copies all settings and triggers but resets runtime state.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="sourceProfile">The name of the source profile to clone.</param>
        /// <param name="newProfileName">The name for the new profile.</param>
        /// <returns>A tuple containing Success (bool) and ErrorMessage (string).</returns>
        public async Task<(bool Success, string ErrorMessage)> CloneProfileAsync(CPHAdapter adapter, string sourceProfile, string newProfileName)
        {
            if (string.IsNullOrWhiteSpace(sourceProfile) || string.IsNullOrWhiteSpace(newProfileName)) return (false, "Profile names cannot be empty");

            // Validation: new profile name format
            if (!_profileNameRegex.IsMatch(newProfileName))
            {
                return (false, "New profile name must be alphanumeric + underscores, 1-32 chars");
            }

            // Validation: check reserved names
            foreach (string reserved in _reservedNames)
            {
                if (reserved.Equals(newProfileName, StringComparison.OrdinalIgnoreCase))
                {
                    return (false, $"'{newProfileName}' is a reserved name");
                }
            }

            var config = GetConfig(adapter);

            // Validation: source profile exists
            if (!config.Profiles.TryGetValue(sourceProfile, out var sourceConfig))
            {
                return (false, $"Source profile '{sourceProfile}' not found");
            }

            // Validation: new profile doesn't exist (case-insensitive manual scan)
            foreach (var kv in config.Profiles)
            {
                if (string.Equals(kv.Key, newProfileName, StringComparison.OrdinalIgnoreCase))
                {
                    return (false, "Profile '" + kv.Key + "' already exists");
                }
            }

            // DEEP COPY via JSON round-trip (C# 7.3 compatible)
            string json = JsonConvert.SerializeObject(sourceConfig);
            GiveawayProfileConfig clonedProfile = JsonConvert.DeserializeObject<GiveawayProfileConfig>(json);

            // Copy triggers manually to preserve case-insensitive dictionary instance
            var sourceObj = config.Profiles[sourceProfile];
            clonedProfile.Triggers.Clear();
            if (sourceObj.Triggers != null)
            {
                foreach (var kv in sourceObj.Triggers) clonedProfile.Triggers[kv.Key] = kv.Value;
            }

            // Disable variable exposure (user must enable manually)
            clonedProfile.ExposeVariables = false;

            // Add cloned profile
            await BackupConfigAsync(adapter);
            config.Profiles[newProfileName] = clonedProfile;
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache();

            adapter.Logger?.LogInfo(adapter, "Config", $"Profile cloned: {sourceProfile} → {newProfileName}");
            return (true, "");
        }



        /// <summary>
        /// Updates a specific configuration key for a profile asynchronously.
        /// Uses Reflection to support all properties of GiveawayProfileConfig dynamically.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="profileName">The name of the profile to update.</param>
        /// <param name="key">The configuration key (property name) to update.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>A tuple containing Success (bool) and ErrorMessage (string).</returns>
        public async Task<(bool Success, string ErrorMessage)> UpdateProfileConfigAsync(CPHAdapter adapter, string profileName, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(profileName) || string.IsNullOrWhiteSpace(key)) return (false, "Profile name and key required");

            var config = GetConfig(adapter);
            if (!config.Profiles.TryGetValue(profileName, out var profile)) return (false, $"Profile '{profileName}' not found");

            string subKey = null;
            if (key.Contains("."))
            {
                var parts = key.Split(new[] { '.' }, 2);
                key = parts[0];
                subKey = parts[1];
            }

            try
            {
                // Handle WheelSettings.* (Nested Object)
                if (key.Equals("WheelSettings", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(subKey))
                {
                    if (profile.WheelSettings == null) profile.WheelSettings = new WheelConfig();

                    var wheelProp = typeof(WheelConfig).GetProperty(subKey, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (wheelProp == null) return (false, $"Unknown WheelSettings key: {subKey}");

                    if (wheelProp.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(value, out bool b)) wheelProp.SetValue(profile.WheelSettings, b);
                        else return (false, "Invalid boolean value");
                    }
                    else if (wheelProp.PropertyType == typeof(int))
                    {
                        if (int.TryParse(value, out int i)) wheelProp.SetValue(profile.WheelSettings, i);
                        else return (false, "Invalid integer value");
                    }
                    else if (wheelProp.PropertyType == typeof(string))
                    {
                        if (wheelProp.Name.Equals("ShareMode", StringComparison.OrdinalIgnoreCase))
                        {
                            var validModes = new[] { "private", "gallery", "copyable", "spin-only" };
                            bool isValid = false;
                            foreach (string mode in validModes) { if (mode.Equals(value, StringComparison.OrdinalIgnoreCase)) { isValid = true; break; } }
                            if (isValid) value = value.ToLower();
                            else return (false, "WheelSettings.ShareMode must be one of: private, gallery, copyable, spin-only");
                        }
                        wheelProp.SetValue(profile.WheelSettings, value);
                    }
                    else return (false, $"Unsupported type for key: {subKey}");

                    // Persistence
                    await BackupConfigAsync(adapter);
                    await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
                    InvalidateCache();
                    adapter.Logger?.LogInfo(adapter, "Config", $"Updated {profileName}.WheelSettings.{subKey} = {value}");
                    return (true, $"Updated WheelSettings.{subKey} to '{value}'");
                }

                // Handle Top-Level Properties via Reflection
                var prop = typeof(GiveawayProfileConfig).GetProperty(key, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (prop == null) return (false, $"Unknown configuration key: {key}");

                // Validate and Set based on Type
                if (prop.PropertyType == typeof(bool))
                {
                    if (bool.TryParse(value, out bool b)) prop.SetValue(profile, b);
                    else return (false, "Invalid boolean value");
                }
                else if (prop.PropertyType == typeof(int))
                {
                    if (int.TryParse(value, out int i)) prop.SetValue(profile, i);
                    else return (false, "Invalid integer value");
                }
                else if (prop.PropertyType == typeof(double))
                {
                    if (double.TryParse(value, out double d)) prop.SetValue(profile, d);
                    else return (false, "Invalid number value");
                }
                else if (prop.PropertyType.IsEnum)
                {
                    try
                    {
                        var enumVal = Enum.Parse(prop.PropertyType, value, true);
                        prop.SetValue(profile, enumVal);
                    }
                    catch
                    {
                         return (false, $"Invalid value for enum {prop.PropertyType.Name}. Allowed: {string.Join(", ", Enum.GetNames(prop.PropertyType))}");
                    }
                }
                else if (prop.PropertyType == typeof(string))
                {
                    if (prop.Name.Equals("UsernamePattern", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrWhiteSpace(value) || value.Equals("null", StringComparison.OrdinalIgnoreCase))
                        {
                            prop.SetValue(profile, null);
                            await BackupConfigAsync(adapter);
                            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
                            InvalidateCache();
                            adapter.Logger?.LogInfo(adapter, "Config", $"Updated {profileName}.UsernamePattern = null");
                            return (true, "Cleared UsernamePattern");
                        }
                        try { _ = new System.Text.RegularExpressions.Regex(value); }
                        catch { return (false, "Invalid regex pattern"); }
                    }
                    prop.SetValue(profile, value);
                }
                else
                {
                     return (false, $"Complex property '{key}' cannot be updated via commands. Edit JSON.");
                }

                // Persistence
                await BackupConfigAsync(adapter);
                await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
                InvalidateCache();
                adapter.Logger?.LogInfo(adapter, "Config", $"Updated {profileName}.{prop.Name} = {value}");
                return (true, $"Updated {prop.Name} to '{value}'");
            }
            catch (Exception ex)
            {
                return (false, $"Update failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds or updates a trigger for a profile.
        /// Valid actions: Entry, Winner, Open, Close.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="triggerSpec">The trigger specification (e.g., "command:!join").</param>
        /// <param name="action">The action to execute (Enter, Winner, Open, Close).</param>
        /// <returns>A tuple containing Success (bool) and ErrorMessage (string).</returns>
        public async Task<(bool Success, string ErrorMessage)> AddProfileTriggerAsync(CPHAdapter adapter, string profileName, string triggerSpec, string action)
        {
            var config = GetConfig(adapter);

            if (config == null || config.Profiles == null) return (false, "Configuration or Profiles dictionary is missing");

            // Standardized case-insensitive lookup for profile
            GiveawayProfileConfig profile = null;
            string actualName = null;
            foreach (var kv in config.Profiles)
            {
                if (kv.Key.Equals(profileName, StringComparison.OrdinalIgnoreCase))
                {
                    profile = kv.Value;
                    actualName = kv.Key;
                    break;
                }
            }

            if (profile == null) return (false, "Profile '" + profileName + "' not found");

            profileName = actualName; // Normalize casing

            // Validation: action is valid
            string[] validActions = new string[] { "Entry", "Winner", "Open", "Close" };
            string normalizedAction = null;
            foreach (string va in validActions)
            {
                if (va.Equals(action, StringComparison.OrdinalIgnoreCase))
                {
                    normalizedAction = va;
                    break;
                }
            }

            if (normalizedAction == null) return (false, "Invalid action '" + action + "'. Must be one of: Enter, Winner, Open, Close");
            action = normalizedAction;

            // Triggers dictionary is read-only but initialized by default.
            // If it were somehow null, we can't assign it, so we rely on initialization.
            if (profile.Triggers == null) return (false, "Critical: Triggers dictionary is null");

            // Add or update trigger
            profile.Triggers[triggerSpec] = action;

            // Persist changes
            await BackupConfigAsync(adapter);
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache();

            adapter.Logger?.LogInfo(adapter, "Config", "Added trigger to " + profileName + ": " + triggerSpec + " -> " + action);
            return (true, "");
        }

        /// <summary>
        /// Removes a specific trigger from a profile.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="profileName">The name of the profile.</param>
        /// <param name="triggerSpec">The trigger specification to remove.</param>
        /// <returns>A tuple containing Success (bool) and ErrorMessage (string).</returns>
        public async Task<(bool Success, string ErrorMessage)> RemoveProfileTriggerAsync(CPHAdapter adapter, string profileName, string triggerSpec)
        {
            var config = GetConfig(adapter);

            // Manual case-insensitive lookup for profile
            GiveawayProfileConfig profile = null;
            string actualName = null;
            foreach (var kv in config.Profiles)
            {
                if (string.Equals(kv.Key, profileName, StringComparison.OrdinalIgnoreCase))
                {
                    profile = kv.Value;
                    actualName = kv.Key;
                    break;
                }
            }

            if (profile == null)
            {
                return (false, "Profile '" + profileName + "' not found");
            }

            profileName = actualName; // Normalize casing

            // Check if trigger exists
            if (profile.Triggers == null || !profile.Triggers.ContainsKey(triggerSpec))
            {
                return (false, $"Trigger '{triggerSpec}' not found in profile '{profileName}'");
            }

            // Create a new dictionary to hold the triggers after removal, preserving case-insensitivity
            var newTriggers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in profile.Triggers)
            {
                if (!kv.Key.Equals(triggerSpec, StringComparison.OrdinalIgnoreCase))
                {
                    newTriggers[kv.Key] = kv.Value;
                }
            }

            // Clear and repopulate to preserve case-insensitive dictionary instance
            profile.Triggers.Clear();
            foreach (var kv in newTriggers) profile.Triggers[kv.Key] = kv.Value;

            // Persist changes
            await BackupConfigAsync(adapter);
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache();

            adapter.Logger?.LogInfo(adapter, "Config", $"Removed trigger from {profileName}: {triggerSpec}");
            return (true, "");
        }


        /// <summary>
        /// Creates a ZIP backup of the current configuration file in the 'backups' directory.
        /// Retains a configurable number of rolling backups.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        private async Task BackupConfigAsync(CPHAdapter adapter)
        {
            string mode = GetRunMode(adapter);
            if (mode != "FileSystem" && mode != "Mirror") return;

            try
            {
                if (!File.Exists(_path)) return;

                string backupDir = Path.Combine(_dir, "backups");
                if (!Directory.Exists(backupDir)) Directory.CreateDirectory(backupDir);

                string zipPath = Path.Combine(backupDir, "config_history.zip");
                int maxBackups = adapter.GetGlobalVar<int>(GiveawayConstants.GlobalBackupCount, true);
                // Use default from global settings if not configured
                if (maxBackups <= 0) maxBackups = 10; // Default fallback if config load fails

                // Async zip creation: 4096 buffer, 'true' for useAsync
                using (var zipStream = new FileStream(zipPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, true))
                using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    string entryName = $"giveaway_config_{DateTime.UtcNow:yyyyMMdd_HHmmss}.json";
                    var entry = zip.CreateEntry(entryName);

                    using (var entryStream = entry.Open())
                    using (var fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        await fileStream.CopyToAsync(entryStream);
                    }

                    // Rolling retention
                    var history = zip.Entries.OrderByDescending(e => e.LastWriteTime).ToList();
                    int currentCount = history.Count;
                    if (currentCount > maxBackups)
                    {
                        for (int i = maxBackups; i < currentCount; i++)
                        {
                            history[i].Delete();
                        }
                    }
                }
                adapter.Logger?.LogInfo(adapter, "Config", "Backup created in config_history.zip");
            }
            catch (Exception ex)
            {
                adapter.Logger?.LogError(adapter, "Config", $"Backup failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Calculates the Levenshtein edit distance between two strings.
        /// Used for fuzzy matching configuration keys to suggest corrections.
        /// </summary>
        /// <param name="s">The first string.</param>
        /// <param name="t">The second string.</param>
        /// <returns>The edit distance (number of operations).</returns>
        public static int LevenshteinDistance(string s, string t)
        {
            // Handle edge cases: if one string is empty, the distance is the length of the other.
            if (string.IsNullOrEmpty(s)) return string.IsNullOrEmpty(t) ? 0 : t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;

            int n = s.Length, m = t.Length;

            // Create a matrix (2D array) to store distances between all prefixes of s and t.
            // d[i, j] holds the edit distance between the first i characters of s and the first j characters of t.
            int[,] d = new int[n + 1, m + 1];

            // Initialize the first row and column.
            // The distance between any string and an empty string is the length of that string (all deletions).
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            // Populate the matrix
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    // If characters match, cost is 0. If they differ, cost is 1 (Substitution).
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Calculate the minimum cost to get to this cell (i, j) from:
                    // 1. Deletion (d[i-1, j] + 1)
                    // 2. Insertion (d[i, j-1] + 1)
                    // 3. Substitution (d[i-1, j-1] + cost)
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            // The bottom-right cell contains the Levenshtein distance between the full strings.
            return d[n, m];
        }

        /// <summary>
        /// Performs a fuzzy check on the configuration object to detect potential typos in keys.
        /// Logs warnings and suggestions to Streamer.bot global variables.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="config">The configuration object to check.</param>
        private static void RunFuzzyCheck(CPHAdapter adapter, GiveawayBotConfig config)
        {
            CheckObj(adapter, config, "Root");
            CheckObj(adapter, config.Globals, "Globals");
            foreach (var p in config.Profiles)
            {
                CheckObj(adapter, p.Value, $"Profiles['{p.Key}']");
                CheckObj(adapter, p.Value.WheelSettings, $"Profiles['{p.Key}'].WheelSettings");
            }
        }

        /// <summary>
        /// Recursively checks an object's properties against its ExtensionData to find potential typos.
        /// </summary>
        /// <param name="adapter">CPH adapter for logging.</param>
        /// <param name="obj">The object to check.</param>
        /// <param name="path">Breadcrumb path for logging (e.g., "Profiles['Main']").</param>
        private static void CheckObj(CPHAdapter adapter, object obj, string path)
        {
            if (obj is null) return;
            var extProp = obj.GetType().GetProperty("ExtensionData");
            if (extProp == null) return;
            var extData = extProp.GetValue(obj) as Dictionary<string, object>;
            if (extData == null || extData.Count == 0) return;

            var validKeys = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.Name != "ExtensionData" && p.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                                .Select(p => p.Name).ToList();

            foreach (var key in extData.Keys)
            {
                string best = ""; int min = int.MaxValue;
                foreach (var vk in validKeys)
                {
                    int d = LevenshteinDistance(key.ToLower(), vk.ToLower());
                    if (d < min) { min = d; best = vk; }
                }
                if (min <= 2)
                {
                    adapter.LogWarn($"[ConfigLoader] [RunFuzzyCheck] {path}: Unrecognized key '{key}'. Did you mean '{best}'?");
                    adapter.SetGlobalVar("Giveaway Global LastConfigErrors", $"❌ {path}: Typo '{key}'? Hint: '{best}'", true);
                }
                else
                {
                    adapter.LogTrace($"[ConfigLoader] [RunFuzzyCheck] {path}: Ignored unknown key '{key}'.");
                }
            }
        }
    }

    /// <summary>
    /// Persists the active state (entries, active status) of giveaways.
    /// Uses Streamer.bot Global Variables for persistence to survive restarts.
    /// </summary>
    public class PersistenceService
    {
        /// <summary>
        /// Resolves the absolute file path for a profile's state JSON.
        /// </summary>
        /// <param name="p">The profile name.</param>
        /// <returns>The absolute file path to the state file.</returns>
        private static string GetStatePath(string p) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "state", $"{p}.json");

        /// <summary>
        /// Saves the giveaway state to disk and/or global variables based on persistence mode.
        /// Serializes the state object to JSON.
        /// </summary>
        /// <param name="adapter">CPH Adapter for logging and global var access.</param>
        /// <param name="p">Profile name.</param>
        /// <param name="s">State object to save.</param>
        /// <param name="globals">Global settings containing persistence mode.</param>
        /// <param name="critical">If true, logs TRACE level confirmation of save.</param>
        public static void SaveState(CPHAdapter adapter, string p, GiveawayState s, GlobalSettings globals, bool critical = false)
        {
            var json = JsonConvert.SerializeObject(s);
            string mode = globals.StatePersistenceMode;

            bool saveToFile = mode == "File" || mode == "Both";
            bool saveToVar = mode == "GlobalVar" || mode == "Both";

            if (saveToFile)
            {
                try
                {
                    string path = GetStatePath(p);
                    string dir = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    File.WriteAllText(path, json);
                    if (critical) adapter.LogTrace($"[Persistence] Critical state saved to file: {path}");
                }
                catch (Exception ex)
                {
                    adapter.LogError($"[Persistence] File save failed for {p}: {ex.Message}");
                    saveToVar = true; // Emergency fallback
                }
            }

            if (saveToVar)
            {
                adapter.SetGlobalVar($"{GiveawayConstants.GlobalStatePrefix}{p}", json, true);
                if (critical) adapter.LogTrace($"[Persistence] Critical state saved to GlobalVar for {p}");
            }
            else if (!saveToVar && mode != "Both")
            {
                // Clean up global var if we are in File mode and not migrating
                adapter.SetGlobalVar($"{GiveawayConstants.GlobalStatePrefix}{p}", (string)null, true);
            }
        }

        /// <summary>
        /// Loads the giveaway state from disk or global variables.
        /// Prioritizes Global Variables in 'Mirror' mode if available.
        /// </summary>
        /// <param name="adapter">CPH Adapter for logging and global var access.</param>
        /// <param name="p">Profile name.</param>
        /// <param name="globals">Global settings containing persistence mode.</param>
        /// <returns>The loaded GiveawayState or null if not found/error.</returns>
        public static GiveawayState LoadState(CPHAdapter adapter, string p, GlobalSettings globals)
        {
            string mode = globals.StatePersistenceMode;
            string json = null;
            adapter.LogTrace($"[Persistence] LoadState for {p} (Mode: {mode})");

            if (mode == "File" || mode == "Both")
            {
                string path = GetStatePath(p);
                if (File.Exists(path))
                {
                    json = File.ReadAllText(path);
                    adapter.LogTrace($"[Persistence] State loaded from file: {path} ({json.Length} chars)");
                }
                else
                {
                    adapter.LogTrace($"[Persistence] State file not found for {p}: {path}");
                }
            }

            if (string.IsNullOrEmpty(json) && (mode == "GlobalVar" || mode == "Both" || json == null))
            {
                // Try new standard key first
                json = adapter.GetGlobalVar<string>($"{GiveawayConstants.GlobalStatePrefix}{p}", true);

                // Fallback to legacy key if empty
                if (string.IsNullOrEmpty(json))
                {
                    json = adapter.GetGlobalVar<string>($"GiveawayBot_State_{p}", true);
                    if (!string.IsNullOrEmpty(json))
                    {
                        adapter.LogInfo($"[Persistence] Migrated legacy state for '{p}' to new format.");
                        // Optional: Write back to new location immediately?
                        // For now we just read it. SaveState will handle writing the new key next time.
                    }
                }

                if (!string.IsNullOrEmpty(json)) adapter.LogTrace($"[Persistence] State loaded from GlobalVar for {p} ({json.Length} chars)");
            }

            if (string.IsNullOrEmpty(json))
            {
                adapter.LogDebug($"[Persistence] No persistent state found for profile {p}.");
                return null;
            }

            try
            {
                 return JsonConvert.DeserializeObject<GiveawayState>(json);
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Persistence] Deserialization failed for {p}: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Robust local file logger for auditing and debugging.
    /// Writes line-delimited JSON for easy machine parsing.
    /// </summary>
    public class FileLogger
    {
        private readonly string _base;

        // ThreadStatic: This field is unique to each thread.
        // We use it to prevent infinite recursion if a logging method crashes and tries to log the crash.
        [ThreadStatic] private static bool _isLogging;

        // Lock Object:
        // In Streamer.bot, actions can run in parallel on different threads.
        // If two threads try to write to the same file at the exact same time, the file will be corrupt or throw an error.
        // We use 'lock(_lock)' to force them to wait their turn.
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes the file logger with a base path.
        /// </summary>
        /// <param name="basePath">Base directory for logs. Defaults to 'Giveaway Bot/logs'.</param>
        public FileLogger(string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
                _base = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "logs");
            else
                _base = basePath;
        }

        /// <summary>Logs an INFO message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogInfo(CPHAdapter adapter, string c, string m) => Log(adapter, "INFO", c, m);
        /// <summary>Logs a WARNING message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogWarn(CPHAdapter adapter, string c, string m) => Log(adapter, "WARN", c, m);
        /// <summary>Logs an ERROR message with optional exception details.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        /// <param name="e">Exception (optional).</param>
        public void LogError(CPHAdapter adapter, string c, string m, Exception e = null) => Log(adapter, "ERROR", c, $"{m} {e?.Message}");
        /// <summary>Logs a DEBUG message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogDebug(CPHAdapter adapter, string c, string m) => Log(adapter, "DEBUG", c, m);
        /// <summary>Logs a TRACE message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogTrace(CPHAdapter adapter, string c, string m) => Log(adapter, "TRACE", c, m);
        /// <summary>Logs a VERBOSE message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogVerbose(CPHAdapter adapter, string c, string m) => Log(adapter, "VERBOSE", c, m);
        /// <summary>Logs a FATAL message.</summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="c">Category.</param>
        /// <param name="m">Message.</param>
        public void LogFatal(CPHAdapter adapter, string c, string m) => Log(adapter, "FATAL", c, m);

        /// <summary>Resets the logging flag to allow new log entries.</summary>
        public static void ResetLoggingFlag() => _isLogging = false;

        /// <summary>
        /// Internal method to handle the actual logging logic including file rotation and pruning.
        /// </summary>
        private void Log(CPHAdapter adapter, string level, string category, string message)
        {
            if (_isLogging) return;
            _isLogging = true;
            try
            {
                // Auto-Prune check: runs roughly once every LOG_PRUNE_CHECK_PROBABILITY log calls to minimize I/O overhead
                int probability = adapter.GetGlobalVar<int>(GiveawayConstants.GlobalLogPruneProbability, true);
                if (probability <= 0) probability = 100;
                if (new Random().Next(probability) == 0) PruneLogs(adapter);

                // Check Global Log Level (Defaults to INFO if not set)
                string currentLevelConfig = adapter.GetGlobalVar<string>(GiveawayConstants.GlobalLogLevel, true);
                if (string.IsNullOrEmpty(currentLevelConfig)) currentLevelConfig = "INFO";

                if (!IsLogLevelEnabled(currentLevelConfig, level)) return;

                string d = Path.Combine(_base, "General");
                if (!Directory.Exists(d)) Directory.CreateDirectory(d);

                var now = DateTime.UtcNow;
                string todayLog = Path.Combine(d, $"{now:yyyy-MM-dd}.log");

                // Rotation Check
                CheckForRotation(adapter, todayLog);

                string line = $"[{now:yyyy-MM-dd hh:mm:ss tt}] [{level,-5}] [{category}] {message}" + Environment.NewLine;

                // CRITICAL: Thread Safety
                // We lock on the static _lock object to ensure only one thread writes to the file at a time.
                // Without this, two giveaways ending at the exact same millisecond could crash the bot.
                lock (_lock)
                {
                    File.AppendAllText(todayLog, line);
                }
            }
            catch (Exception ex)
            {
                try { adapter.LogWarn($"[GiveawayBot] File logging failed: {ex.Message}"); } catch { }
            }
            finally
            {
                _isLogging = false;
            }
        }

        /// <summary>
        /// Enforce log retention policies (age and size).
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        private void PruneLogs(CPHAdapter adapter)
        {
            try
            {
                if (!Directory.Exists(_base)) return;

                int retentionDays = adapter.GetGlobalVar<int>(GiveawayConstants.GlobalLogRetentionDays, true);
                if (retentionDays <= 0) retentionDays = 90; // Default

                long sizeCapMB = adapter.GetGlobalVar<long>(GiveawayConstants.GlobalLogSizeCap, true);
                if (sizeCapMB <= 0) sizeCapMB = 100; // Default

                var cutoff = DateTime.UtcNow.AddDays(-retentionDays);
                var files = Directory.GetFiles(_base, "*.log", SearchOption.AllDirectories)
                                     .Select(f => new FileInfo(f))
                                     .OrderBy(f => f.LastWriteTime)
                                     .ToList();

                // Retention-based pruning
                foreach (var f in files.Where(f => f.LastWriteTime < cutoff))
                {
                    f.Delete();
                }

                // Size-based pruning
                long totalSize = files.Sum(f => f.Length);
                long capBytes = sizeCapMB * 1024 * 1024;

                if (totalSize > capBytes)
                {
                    foreach (var f in files.Where(f => f.Exists))
                    {
                        totalSize -= f.Length;
                        f.Delete();
                        if (totalSize <= capBytes) break;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Checks if the current log file exceeds the size limit and rotates it if necessary.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="path">The path to the current log file.</param>
        private void CheckForRotation(CPHAdapter adapter, string path)
        {
            try
            {
                if (!File.Exists(path)) return;

                int maxMb = adapter.GetGlobalVar<int>(GiveawayConstants.GlobalLogMaxFileSize, true);
                if (maxMb <= 0) maxMb = 10; // Default 10MB

                var fi = new FileInfo(path);
                if (fi.Length > (maxMb * 1024 * 1024))
                {
                    // Rotate
                    string timestamp = DateTime.UtcNow.ToString("HH-mm-ss");
                    string newName = path.Replace(".log", $"_{timestamp}.log");

                    try
                    {
                        // Double check target doesn't exist (fast retry)
                        if (!File.Exists(newName))
                        {
                            File.Move(path, newName);
                             // Temporarily break out of logging context to avoid recursion if we were logging about rotation
                        }
                    }
                    catch (Exception ex)
                    {
                         // Signal safely - FileLogger re-entrancy guard will prevent infinite loop specific to file logging,
                         // but this ensures it hits the Streamer.bot main log tab.
                         adapter.LogWarn($"[FileLogger] Failed to rotate log file: {ex.Message}");
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Checks if the message level is enabled by the current configuration.
        /// </summary>
        /// <param name="configured">The configured log level.</param>
        /// <param name="current">The current message log level.</param>
        /// <returns>True if the message should be logged, false otherwise.</returns>
        private static bool IsLogLevelEnabled(string configured, string current)
        {
            var levels = new List<string> { "TRACE", "VERBOSE", "DEBUG", "INFO", "WARN", "ERROR", "FATAL" };
            int configIdx = levels.FindIndex(l => string.Equals(l, configured, StringComparison.OrdinalIgnoreCase));
            int currentIdx = levels.FindIndex(l => string.Equals(l, current, StringComparison.OrdinalIgnoreCase));

            // If config is invalid, default to INFO (index 3)
            if (configIdx == -1) configIdx = 3;
            return currentIdx >= configIdx;
        }
    }

#endregion

#region Validation, Constants & Data Models
    /// <summary>
    /// Utility class for validating giveaway entries and detecting suspicious/bot accounts.
    /// </summary>
    public static class EntryValidator
    {
        /// <summary>
        /// Validates a username against a regex pattern.
        /// </summary>
        /// <param name="username">The username to validate</param>
        /// <param name="pattern">Regex pattern (null/empty returns true)</param>
        /// <returns>True if valid or pattern is disabled, false otherwise</returns>
        public static bool ValidateUsername(string username, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return true; // Validation disabled
            if (string.IsNullOrEmpty(username)) return false; // Invalid username

            try
            {
                return Regex.IsMatch(username, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
            }
            catch (RegexMatchTimeoutException)
            {
                // Regex took too long - reject for safety
                return false;
            }
            catch
            {
                // Invalid regex pattern - allow entry but log warning elsewhere
                return true;
            }
        }

        /// <summary>
        /// Checks if a username has sufficient entropy to be considered "real".
        /// Uses Shannon entropy to detect keyboard smashing like "asdfgh", "zzzzzz", etc.
        /// </summary>
        /// <param name="text">The username to check</param>
        /// <param name="threshold">Entropy threshold (default 2.5)</param>
        /// <returns>True if entropy is sufficient (appears legitimate), false if suspicious</returns>
        public static bool HasSufficientEntropy(string text, double threshold = 2.5)
        {
            if (string.IsNullOrEmpty(text)) return false;

            // Normalize to lowercase for consistency
            text = text.ToLower();

            // Calculate character frequency distribution
            var freq = new Dictionary<char, int>();
            foreach (var c in text)
            {
                if (!freq.ContainsKey(c)) freq[c] = 0;
                freq[c]++;
            }

            // Shannon entropy calculation: H = -Σ(p(x) * log2(p(x)))
            double entropy = 0.0;
            foreach (var kv in freq)
            {
                double probability = (double)kv.Value / text.Length;
                entropy -= probability * Math.Log(probability, 2);
            }

            // Threshold: Real usernames typically have entropy > threshold
            // Examples using 2.5:
            //   "aaaaaaaa" = 0.0 (rejected)
            //   "asdfgh" = 2.58 (accepted)
            //   "FlowerPower123" = 3.67 (accepted)
            //   "zxcvbnmasdf" = 3.17 (accepted)

            return entropy > threshold;
        }
    }

    // ==================== Data Models ====================

#pragma warning disable IDE0028 // Simplify collection initialization
#pragma warning disable IDE0300 // Use collection expression
#pragma warning disable IDE0090 // 'new' expression can be simplified

    /// <summary>
    /// Configuration format for exporting/dumping data.
    /// </summary>
    public enum DumpFormat
    {
        TXT,
        CSV,
        JSON
    }

    public static class GiveawayConstants
    {
        // Metrics
        public const string Metric_EntriesRejected = "Entries Rejected";
        public const string Metric_EntriesRejectedCooldown = "Entries Rejected Cooldown";
        public const string Metric_EntriesRateLimited = "Entries Rate Limited";
        public const string Metric_EntriesTotal = "Entries Total";
        public const string Metric_EntriesTotalUser = "Entries Total"; // Keep consistent with Total
        public const string Metric_WinnersTotal = "Winners Total";
        public const string Metric_GiveawayStarted = "Giveaway Started";
        public const string Metric_GiveawayEnded = "Giveaway Ended";
        public const string Metric_SystemErrors = "System Errors";
        public const string GlobalStatePrefix = "Giveaway State ";
        public const string GlobalRunMode = "Giveaway Global Run Mode";
        public const string GlobalConfig = "Giveaway Global Config";
        public const string GlobalConfigLastWrite = "Giveaway Global Config Last Write Time";
        public const string GlobalMultiPlatform = "Giveaway Global Enable Multi Platform";
        public const string GlobalLogRetentionDays = "Giveaway Global Log Retention Days";
        public const string GlobalLogSizeCap = "Giveaway Global Log Size Cap MB";
        public const string GlobalLogMaxFileSize = "Giveaway Global Log Max File Size MB";
        public const string GlobalBackupCount = "Giveaway Global Backup Count";
        public const string GlobalLogPruneProbability = "Giveaway Global Log Prune Probability";
        public const string GlobalLogLevel = "Giveaway Global Log Level";
        public const string GlobalFallbackPlatform = "Giveaway Global Fallback Platform";
        public const string GlobalWheelApiKey = "Giveaway Global Wheel Api Key";
        public const string GlobalWheelApiKeyStatus = "Giveaway Global Wheel Api Key Status";
        public const string GlobalEnabledPlatforms = "Giveaway Global Enabled Platforms";
        public const string GlobalInstructions = "Giveaway Global Instructions";
        public const string GlobalTriggerPrefixHelp = "Giveaway Global Trigger Prefix Help";
        public const string GlobalConfigStatus = "Giveaway Global Config Status";

        public const string Metric_ApiErrors = "Api Errors";

        public const string Metric_TotalEntryProcessingMs = "Total Entry Processing Ms";
        public const string Metric_EntryProcessingAvgMs = "Entry Processing Avg Ms";
        public const string Metric_WinnerDrawAttempts = "Winner Draw Attempts";
        public const string Metric_WinnerDrawSuccesses = "Winner Draw Successes";

        public const string Metric_WheelApiCalls = "Wheel Api Calls";
        public const string Metric_WheelApiTotalMs = "Wheel Api Total Ms";
        public const string Metric_WheelApiAvgMs = "Wheel Api Avg Ms";
        public const string Metric_WheelApiErrors = "Wheel Api Errors";
        public const string Metric_WheelApiInvalidKeys = "Wheel Api Invalid Keys";
        public const string Metric_WheelApiTimeouts = "Wheel Api Timeouts";
        public const string Metric_WheelApiNetworkErrors = "Wheel Api Network Errors";

        // Actions
        public const string Action_Enter = "Enter";
        public const string Action_Winner = "Winner";
        public const string Action_Open = "Open";
        public const string Action_Close = "Close";

        // Management Commands
        public const string Cmd_ConfigGen = "config gen";
        public const string Cmd_ConfigCheck = "config check";
        public const string Cmd_SystemTest = "system test";
        public const string Cmd_RegexTest = "regex test";
        public const string Cmd_Create = "create";
        public const string Cmd_Delete = "delete";
        public const string Cmd_Stats = "stats";
        public const string Cmd_Update = "update";

        // Command Prefixes (for helper methods)
        public const string CmdPrefix_Config = "config";
        public const string CmdPrefix_System = "system";
        public const string CmdPrefix_Profile = "profile";
        public const string CmdPrefix_ProfileShort = "p";
        public const string CmdPrefix_Data = "data";
        public const string CmdPrefix_DataShort = "d";

        // Command Patterns
        public const string CmdPattern_GiveawayPrefix = "!giveaway ";
        public const string CmdPattern_GaPrefix = "!ga ";
        public const string GlobalLastConfigErrors = "Giveaway Global Last Config Errors";
        public const string GlobalHealthTest = "Giveaway Health Test";
        public const string UserVarPrefix = "Giveaway User Var";
        public const string GlobalVarPrefix = "Giveaway Var";

        // Profile Variable Patterns
        public const string GlobalExposeVariables = "Giveaway Global Expose Variables";
        public const string GlobalMetricsPrefix = "Giveaway Global Metrics ";

        public const string ProfileVarBase = "Giveaway";
        public const string ProfileSubLuckMultiplierSuffix = "Sub Luck Multiplier";
        public const string ProfileWinChanceSuffix = "Win Chance";
        public const string ProfileDumpFormatSuffix = "Dump Format";
        public const string ProfileIsActiveSuffix = "Is Active";
        public const string ProfileMsgPrefix = "Msg ";
        public const string ProfileStateBlobSuffix = "State Blob";
        public const string ProfileTriggersSuffix = "Triggers";
        public const string ProfileMessagesSuffix = "Messages";
        public const string ProfileTimerDurationSuffix = "Timer Duration";
        public const string ProfileMaxEntriesSuffix = "Max Entries Per Minute";

        // New Constants for SyncProfileVariables
        public const string ProfileRequireFollowerSuffix = "Require Follower";
        public const string ProfileRequireSubscriberSuffix = "Require Subscriber";
        public const string ProfileEnableWheelSuffix = "Enable Wheel";
        public const string ProfileEnableObsSuffix = "Enable OBS";
        public const string ProfileObsSceneSuffix = "Obs Scene";
        public const string ProfileObsSourceSuffix = "Obs Source";
        public const string ProfileWheelTitleSuffix = "Wheel Settings Title";
        public const string ProfileWheelDescriptionSuffix = "Wheel Settings Description";
        public const string ProfileWheelSpinTimeSuffix = "Wheel Settings Spin Time";
        public const string ProfileWheelAutoRemoveWinnerSuffix = "Wheel Settings Auto Remove Winner";
        public const string ProfileWheelShareModeSuffix = "Wheel Settings Share Mode";
        public const string ProfileWheelWinnerMessageSuffix = "Wheel Settings Winner Message";
        public const string ProfileDumpOnEndSuffix = "Dump Entries On End";
        public const string ProfileDumpOnEntrySuffix = "Dump Entries On Entry";
        public const string ProfileDumpThrottleSuffix = "Dump Entries On Entry Throttle Seconds";
        public const string ProfileDumpWinnersSuffix = "Dump Winners On Draw";
        public const string ProfileDumpSeparateGameNamesSuffix = "Dump Separate Game Names";
        public const string ProfileUsernameRegexSuffix = "Username Regex";
        public const string ProfileMinAccountAgeSuffix = "Min Account Age Days";
        public const string ProfileEnableEntropySuffix = "Enable Entropy Check";
        public const string ProfileGameFilterSuffix = "Game Filter";
        public const string ProfileRedemptionCooldownSuffix = "Redemption Cooldown Minutes";

        // Profile Internal State
        public const string ProfileGiveawayIdSuffix = "Giveaway Id";
        public const string ProfileWinnerNameSuffix = "Winner Name";
        public const string ProfileWinnerUserIdSuffix = "Winner User Id";
        public const string ProfileWinnerCountSuffix = "Winner Count";
        public const string ProfileCumulativeEntriesSuffix = "Cumulative Entries";
        public const string ProfileSubEntryCountSuffix = "Sub Entry Count";
    }

    /// <summary>
    /// Root configuration object for the Giveaway Bot.
    /// Matches the structure of the 'giveaway_config.json' file.
    /// contains global settings and a dictionary of profile-specific configurations.
    /// </summary>
    public class GiveawayBotConfig
    {
        /// <summary>
        /// Example instructions displayed at the top of the JSON config file.
        /// These are for user guidance and are not used by the bot logic.
        /// </summary>
        [JsonProperty("_instructions")]
        public string[] Instructions { get; set; } = new string[]
        {
            "WELCOME TO YOUR GIVEAWAY BOT!",
            "--------------------------------------------------------------------------------",
            "HOW TO CONFIGURE:",
            "1. PROFILES: Create different setups like 'Main', 'Monthly', or 'SubOnly'.",
            "2. TRIGGERS: Connect real events to actions. Format is 'Type:Name'.",
            "3. WHEEL (If Enabled): Set 'Giveaway Global WheelApiKey' in Streamer Bot's Global Persistent Variables.",
            "4. OBS (If Enabled): Set 'ObsScene' and 'ObsSource' in Profile.",
            "5. EXAMPLE: 'command:!join' means 'For the Command named !join'.",
            "6. TROUBLESHOOTING: Run chat command '!giveaway config check' if you get any errors.",
            "--------------------------------------------------------------------------------"
        };

        /// <summary>
        /// Help text for configuring triggers, displayed in the JSON file.
        /// </summary>
        [JsonProperty("_trigger_prefixes_help")]
        public string[] TriggerPrefixHelp { get; set; } = new string[]
        {
            "HOW TO WRITE TRIGGERS (Type:Value):",
            "• 'command:!join'     -> [Type] is 'command', [Value] is '!join'.",
            "• 'sd:MY-KEY-UID'     -> [Type] is 'sd' (Stream Deck), [Value] is your button ID.",
            "• 'name:Main Hotkey'  -> [Type] is 'name', [Value] is your Hotkey or Timer name.",
            "• 'id:12345-ABC'      -> [Type] is 'id', [Value] is a specific Event ID.",
            "--------------------------------------------------------------------------------",
            "MULTI-PLATFORM MESSAGING:",
            "1. SETTINGS: Set 'FallbackPlatform' in Globals for offline cases.",
            "2. BROADCAST: Set 'GiveawayBot_EnableMultiPlatform' (true/false) in SB.",
            "--------------------------------------------------------------------------------",
            "PERSISTENCE & PERFORMANCE:",
            "• 'RunMode'                  : FileSystem, GlobalVar, ReadOnlyVar, Mirror.",
            "• 'StatePersistenceMode'      : File, GlobalVar, Both.",
            "• 'StateSyncIntervalSeconds' : Sync frequency (Default: 30s).",
            "• 'LogLevel'                 : TRACE, DEBUG, INFO, WARN, ERROR, FATAL.",
            "--------------------------------------------------------------------------------",
            "RUN MODES:",
            "• FileSystem  : Uses local JSON file only (Fast, Local).",
            "• GlobalVar   : Uses SB Global Variable only (Memory/SB Internal).",
            "• ReadOnlyVar : Loads from SB Global Variable, forbids local/bot writes.",
            "• Mirror      : Bidirectional sync between File and Global Variable (Redundant).",
            "--------------------------------------------------------------------------------",
            "VARIABLE SYSTEM (Giveaway {Profile} {Var}):",
            "• [Live Stats]      : Is Active, Entry Count, Ticket Count, Id, Winner Name, Winner Count, Sub Entry Count."
        };

        /// <summary>
        /// Dictionary of giveaway profiles. Key is the profile name (e.g., "Main").
        /// </summary>
        public Dictionary<string, GiveawayProfileConfig> Profiles { get; set; } = new Dictionary<string, GiveawayProfileConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Global settings applicable to all profiles.
        /// </summary>
        public GlobalSettings Globals { get; set; } = new GlobalSettings();

        /// <summary>
        /// Initializes a new instance of the GiveawayBotConfig class with a default "Main" profile.
        /// </summary>
        public GiveawayBotConfig()
        {
            Profiles["Main"] = new GiveawayProfileConfig();
        }

        /// <summary>
        /// Extension data to capture any extra properties during JSON deserialization.
        /// Used for fuzzy matching to detect typos in the config file.
        /// </summary>
        [JsonExtensionData] public Dictionary<string, object> ExtensionData { get; set; }
    }
    /// <summary>
    /// Global application settings affecting all profiles (logging, API keys, platform sync).
    /// </summary>
    public class GlobalSettings
    {
        [JsonProperty("_warning")] public string Warning { get; set; } = "DO NOT share your API Keys with others!";

        [JsonProperty("_security_toasts_help")] public string SecurityToastsHelp { get; set; } = "Enable toast notifications for security events (Unauthorized Access, Spam, invalid keys). default: true.";

        /// <summary>
        /// Enable toast notifications for security events.
        /// </summary>
        public bool EnableSecurityToasts { get; set; } = true;

        /// <summary>
        /// Whether to output logs to Streamer.bot's internal log.
        /// </summary>
        public bool LogToStreamerBot { get; set; } = true;

        [JsonProperty("_expose_vars_help")] public string ExposeVarsHelp { get; set; } = "Expose all profile variables to Streamer.bot global variables (Giveaway {Profile} {Var}). Default: Defer to Profile.";

        /// <summary>
        /// Globally control variable exposure override. Null means defer to Profile setting.
        /// </summary>
        public bool? ExposeVariables { get; set; } = null;

        /// <summary>
        /// The name of the global variable storing the Wheel of Names API Key.
        /// </summary>
        public string WheelApiKeyVar { get; set; } = "Wheel Of Names Api Key";

        [JsonProperty("_encryption_salt_help")] public string EncryptionSaltHelp { get; set; } = "Randomly generated salt for portable encryption. DO NOT CHANGE MANUALLY.";

        /// <summary>
        /// Salt used for encrypting secrets. Auto-generated.
        /// </summary>
        public string EncryptionSalt { get; set; }

        [JsonProperty("_log_retention_help")] public string LogRetentionHelp { get; set; } = "How many days of historical logs to keep (default: 90).";

        /// <summary>
        /// Number of days to retain log files.
        /// </summary>
        public int LogRetentionDays { get; set; } = 90;

        [JsonProperty("_log_size_cap_help")] public string LogSizeCapHelp { get; set; } = "Total disk size limit for the logs directory in MB (default: 100). Oldest logs are pruned first if exceeded.";

        /// <summary>
        /// Maximum total size of the logs directory in MB.
        /// </summary>
        public int LogSizeCapMB { get; set; } = 100;

        [JsonProperty("_log_file_size_limit_help")] public string LogMaxFileSizeHelp { get; set; } = "Max size for a single log file in MB (default: 10). Rotates to new file if exceeded.";

        /// <summary>
        /// Maximum size of a single log file in MB before rotation.
        /// </summary>
        public int LogMaxFileSizeMB { get; set; } = 10;

        [JsonProperty("_run_mode_help")] public string RunModeHelp { get; set; } = "RunMode: FileSystem, GlobalVar, ReadOnlyVar.";

        /// <summary>
        /// Controls how configuration is stored and synchronized.
        /// Options: FileSystem, GlobalVar, ReadOnlyVar, Mirror.
        /// </summary>
        public string RunMode { get; set; } = "Mirror";

        [JsonProperty("_log_level_help")] public string LogLevelHelp { get; set; } = "LogLevel: TRACE, VERBOSE, DEBUG, INFO, WARN, ERROR, FATAL.";

        /// <summary>
        /// Minimum log level to record.
        /// </summary>
        public string LogLevel { get; set; } = "INFO";

        [JsonProperty("_fallback_platform_help")] public string FallbackPlatformHelp { get; set; } = "Fallback: Twitch, YouTube, Kick (if no platforms detected live).";

        /// <summary>
        /// Platform to send messages to if no active stream is detected.
        /// </summary>
        public string FallbackPlatform { get; set; } = "Twitch";

        [JsonProperty("_enabled_platforms_help")] public string EnabledPlatformsHelp { get; set; } = "The list of platforms to monitor for live status and broadcast to.";

        /// <summary>
        /// List of platforms to support (Twitch, YouTube, Kick).
        /// </summary>
        public List<string> EnabledPlatforms { get; set; } = new List<string> { "Twitch", "YouTube", "Kick" };

        [JsonProperty("_language_help")] public string LanguageHelp { get; set; } = "Language code for localization (e.g., en-US).";

        /// <summary>
        /// Language code for localization.
        /// </summary>
        public string Language { get; set; } = "en-US";

        [JsonProperty("_custom_strings_help")] public string CustomStringsHelp { get; set; } = "Override specific localized strings here. Key:Value pairs.";

        /// <summary>
        /// Custom string overrides for localization.
        /// </summary>
        public Dictionary<string, string> CustomStrings { get; set; } = new Dictionary<string, string>();

        [JsonProperty("_import_globals_help")] public string ImportGlobalsHelp { get; set; } = "Dictionary of Global Variables to import/set in Streamer.bot if missing (e.g. API Keys).";

        /// <summary>
        /// Global variables imported from Streamer.bot.
        /// </summary>
        public Dictionary<string, string> ImportGlobals { get; set; } = new Dictionary<string, string>();

        [JsonProperty("_persistence_mode_help")] public string PersistenceModeHelp { get; set; } = "StatePersistenceMode: Where to store active giveaway data (File, GlobalVar, Both).";

        /// <summary>
        /// Where to store active giveaway state (entries, etc.).
        /// Options: File, GlobalVar, Both.
        /// </summary>
        public string StatePersistenceMode { get; set; } = "Both";

        [JsonProperty("_sync_interval_help")] public string SyncIntervalHelp { get; set; } = "StateSyncIntervalSeconds: How often to flush entries to persistent storage (Default: 30).";

        /// <summary>
        /// Interval in seconds for flushing state to storage.
        /// </summary>
        public int StateSyncIntervalSeconds { get; set; } = 30;

        [JsonProperty("_advanced_settings_help")] public string AdvancedSettingsHelp { get; set; }
            = "Advanced: ConfigReloadInterval (s), CacheTTL (m), CleanupInterval (ms), Entropy (2.5), PruneProb (1/N), BackupCount.";

        /// <summary>Interval for checking config file updates.</summary>
        public int ConfigReloadIntervalSeconds { get; set; } = 5;
        /// <summary>Time to live for message ID cache.</summary>
        public int MessageIdCacheTtlMinutes { get; set; } = 5;
        /// <summary>Interval for cleaning up old message IDs.</summary>
        public int MessageIdCleanupIntervalMs { get; set; } = 60000;
        /// <summary>How long to cache API key validation results.</summary>
        public int ApiKeyValidationCacheMinutes { get; set; } = 30;
        /// <summary>Minimum entropy for username validation.</summary>
        public double MinUsernameEntropy { get; set; } = 2.5;
        /// <summary>Probability (1/N) of running log pruning on a log call.</summary>
        public int LogPruneProbability { get; set; } = 100;
        /// <summary>Number of config backups to keep.</summary>
        public int ConfigBackupCount { get; set; } = 10;

        /// <summary>Timeout for regex operations in ms.</summary>
        public int RegexTimeoutMs { get; set; } = 100;
        /// <summary>Timeout for HTTP requests in seconds.</summary>
        public int HttpClientTimeoutSeconds { get; set; } = 30;
        /// <summary>Window size for spam detection in seconds.</summary>
        public int SpamWindowSeconds { get; set; } = 60;

        [JsonExtensionData] public Dictionary<string, object> ExtensionData { get; set; }
    }
    /// <summary>
    /// Configuration for a specific giveaway profile (e.g., "Main", "SubOnly").
    /// Defines triggers, validation rules, rules, and integration settings.
    /// </summary>
    public class GiveawayProfileConfig
    {
        [JsonProperty("_actions_help")] public string ActionsHelp { get; set; } = "Available Actions: 'Enter' (Add user to giveaway), 'Winner' (Picks a winner), 'Open' (Enable entries), 'Close' (Disable entries)";

        /// <summary>
        /// Dictionary of event triggers mapped to actions.
        /// Key format: "type:value" (e.g., "command:!join").
        /// Value: Action (Enter, Winner, Open, Close).
        /// </summary>
        public Dictionary<string, string> Triggers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new profile with default command triggers.
        /// </summary>
        public GiveawayProfileConfig()
        {
            Triggers.Add("command:!enter", GiveawayConstants.Action_Enter);
            Triggers.Add("command:!draw", GiveawayConstants.Action_Winner);
            Triggers.Add("command:!start", GiveawayConstants.Action_Open);
            Triggers.Add("command:!end", GiveawayConstants.Action_Close);
            // Aliases for users who prefer the full prefix
            Triggers.Add("command:!ga start", GiveawayConstants.Action_Open);
            Triggers.Add("command:!ga end", GiveawayConstants.Action_Close);
            Triggers.Add("command:!ga draw", GiveawayConstants.Action_Winner);
            Triggers.Add("command:!ga enter", GiveawayConstants.Action_Enter);
            WheelSettings = new WheelConfig();
        }

        // Helper field for JSON documentation, ignored by logic but kept to suppress "unknown key" warnings
        [JsonProperty("_expose_variables_help")]
        public string ExposeVariablesHelp { get; set; } = "Set to true to expose these variables to Streamer.bot global vars.";

        [JsonProperty("_variable_exposure_help")] public string VariableExposureHelp { get; set; } = "RequireFollower/Subscriber check.";

        /// <summary>Require user to be a follower to enter.</summary>
        public bool RequireFollower { get; set; } = false;
        /// <summary>Require user to be a subscriber to enter.</summary>
        public bool RequireSubscriber { get; set; } = false;

        [JsonProperty("_entries_help")] public string EntriesHelp { get; set; } = "Max entries allowed per minute. Prevents bot spam.";

        /// <summary>Maximum entries accepted per minute across all users.</summary>
        public int MaxEntriesPerMinute { get; set; } = 45;

        /// <summary>Enable Wheel of Names integration.</summary>
        public bool EnableWheel { get; set; } = false;

        [JsonProperty("_discord_help")] public string DiscordHelp { get; set; } = "Configure Discord notifications. Use WebhookUrl OR ChannelId (Native). Native takes priority.";

        /// <summary>
        /// Optional: Discord Webhook URL for winner announcements.
        /// </summary>
        public string DiscordWebhookUrl { get; set; }

        /// <summary>
        /// Optional: Streamer.bot Discord Channel ID for native announcements.
        /// Note: Requires the bot to be connected to Discord in Streamer.bot settings.
        /// </summary>
        public string DiscordChannelId { get; set; }

        /// <summary>
        /// Optional: Custom message for Discord announcements.
        /// Supports placeholders like {winner}.
        /// </summary>
        public string DiscordMessage { get; set; } = "Congratulations {winner}!";
        /// <summary>Enable OBS Browser Source control.</summary>
        public bool EnableObs { get; set; } = false;
        /// <summary>OBS Scene name for the wheel source.</summary>
        public string ObsScene { get; set; } = "Giveaway";
        /// <summary>OBS Browser Source name to update with wheel URL.</summary>
        public string ObsSource { get; set; } = "WheelSource";

        /// <summary>Expose profile state to global variables.</summary>
        public bool ExposeVariables { get; set; } = false;

        [JsonProperty("_dump_format_help")] public string DumpFormatHelp { get; set; } = "Format for dump files: TXT, CSV, JSON.";

        /// <summary>Format for entry and winner dump files.</summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DumpFormat DumpFormat { get; set; } = DumpFormat.TXT;

        /// <summary>Settings for the Wheel of Names display.</summary>
        public WheelConfig WheelSettings { get; set; } = new WheelConfig();

        /// <summary>Dump all entries to a file when the giveaway ends.</summary>
        public bool DumpEntriesOnEnd { get; set; } = true;

        [JsonProperty("_dump_entries_on_entry_help")] public string DumpEntriesOnEntryHelp { get; set; } = "Write entries to txt file as accepted (real-time, throttled). DumpEntriesOnEntryThrottleSeconds controls batch frequency.";

        /// <summary>Write entries to file in real-time (batched).</summary>
        public bool DumpEntriesOnEntry { get; set; } = false;
        /// <summary>Throttle interval for real-time entry dumps.</summary>
        public int DumpEntriesOnEntryThrottleSeconds { get; set; } = 10;

        /// <summary>Dump winner details to file on draw.</summary>
        public bool DumpWinnersOnDraw { get; set; } = true;

        /// <summary>
        /// If true, generates a separate content-only file for winners/entries containing just the GameName (or UserName).
        /// Useful for copy-pasting into game clients.
        /// </summary>
        public bool DumpSeparateGameNames { get; set; } = false;

        [JsonProperty("_luck_help")] public string LuckHelp { get; set; } = "SubLuckMultiplier: Bonus tickets for subs (e.g. 2.0 = 2x tickets). WinChance: Probability (0.0-1.0) that ANY entry is accepted (Gatekeeper).";

        /// <summary>Ticket multiplier for subscribers.</summary>
        public decimal SubLuckMultiplier { get; set; } = 2.0m;

        // Entry Validation & Bot Detection Settings
        // Entry Validation & Bot Detection Settings
        [JsonProperty("_username_regex_help")] public string UsernameRegexHelp { get; set; } = "Regex pattern for username validation (null/empty = disabled). Example GW2: ^[A-Za-z0-9\\-\\_]{3,32}$";

        /// <summary>Regex pattern to validate usernames.</summary>
        public string UsernameRegex { get; set; } = null;

        [JsonProperty("_min_account_age_help")] public string MinAccountAgeHelp { get; set; } = "Minimum account age in days (0 = disabled). Rejects entries from accounts younger than specified days.";

        /// <summary>Minimum account age in days to allow entry.</summary>
        public int MinAccountAgeDays { get; set; } = 180;

        [JsonProperty("_entropy_check_help")] public string EntropyCheckHelp { get; set; } = "Enable entropy checking to detect keyboard smashing / low-quality names (e.g., 'asdfgh', 'zzzzzz').";

        /// <summary>Enable username entropy check for spam detection.</summary>
        public bool EnableEntropyCheck { get; set; } = true;

        [JsonProperty("_game_filter_help")] public string GameFilterHelp { get; set; } = "Game-specific validation. Options: 'GW2', 'Guild Wars 2' (auto-applies pattern + entropy settings). Leave null for custom UsernameRegex.";

        /// <summary>Preset game filter (e.g., 'GW2') to apply specific validation rules.</summary>
        public string GameFilter { get; set; } = null;

        [JsonProperty("_redemption_cooldown_help")] public string RedemptionCooldownHelp { get; set; } = "Per-user cooldown for redemptions in minutes (0 = disabled). Prevents users from redeeming same reward multiple times.";

        /// <summary>Cooldown in minutes for user redemptions.</summary>
        public int RedemptionCooldownMinutes { get; set; } = 0;


        // External Bot Listeners
        [JsonProperty("_allowed_external_bots_help")] public string AllowedExternalBotsHelp { get; set; } = "List of strict bot usernames (case-insensitive) that are allowed to trigger actions. E.g., ['Moobot', 'Nightbot'].";

        /// <summary>List of external bot usernames allowed to trigger events.</summary>
        public List<string> AllowedExternalBots { get; set; } = new List<string>();

        [JsonProperty("_external_listeners_help")] public string ExternalListenersHelp { get; set; } = "Rules for parsing messages from allowed external bots. Maps regular expressions to specific actions.";

        /// <summary>Rules for listening to external bot messages.</summary>
        public List<BotListenerRule> ExternalListeners { get; set; } = new List<BotListenerRule>();

        // Toast Notifications
        [JsonProperty("_toast_help")] public string ToastHelp { get; set; } = "Configurable toast notifications for specific events. Key = EventName, Value = Enabled (true/false).";

        /// <summary>Toast notification settings per event type.</summary>
        public Dictionary<string, bool> ToastNotifications { get; set; } = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
        {
            { "EntryAccepted", false },
            { "EntryRejected", true },
            { "WinnerSelected", true },
            { "GiveawayOpened", true },
            { "GiveawayClosed", true },
            { "UnauthorizedAccess", true }
        };

        /// <summary>Probability (0.0-1.0) that any entry is accepted.</summary>
        public double WinChance { get; set; } = 1.0;

        [JsonProperty("_messages_help")] public string MessagesHelp { get; set; } = "Override default messages. Key:Value pairs.";

        /// <summary>Custom message overrides.</summary>
        public Dictionary<string, string> Messages { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [JsonProperty("_timer_help")] public string TimerHelp { get; set; } = "Auto-close duration (e.g. '10m', '120s', or '5' for 5 minutes). Null = manual only.";

        /// <summary>Duration string for auto-closing giveaways.</summary>
        public string TimerDuration { get; set; }
    }
    /// <summary>
    /// Represents a rule for parsing messages from external bots.
    /// </summary>
    public class BotListenerRule
    {
        public string Action { get; set; } // e.g., "Enter", "Winner", "Open", "Close"
        public string Pattern { get; set; } // Regex pattern
    }

    /// <summary>
    /// Configuration settings for the Wheel of Names API v2 integration.
    /// </summary>
    public class WheelConfig
    {
        [JsonProperty("_wheel_config_help")] public string WheelConfigHelp { get; set; } = "Wheel of Names API v2 Configuration. ShareMode options: private, gallery, copyable, spin-only";
        /// <summary>Title displayed on the wheel.</summary>
        public string Title { get; set; } = "Giveaway Winner";
        /// <summary>Description displayed on the wheel page.</summary>
        public string Description { get; set; } = "Good Luck Everyone!";
        /// <summary>Duration of the spin animation in seconds.</summary>
        public int SpinTime { get; set; } = 10;
        /// <summary>Whether to remove the winner from the wheel after spinning.</summary>
        public bool AutoRemoveWinner { get; set; } = true;
        /// <summary>Share mode (private, gallery, copyable, spin-only).</summary>
        public string ShareMode { get; set; } = "private";

        [JsonProperty("_variables_help")] public string VariablesHelp { get; set; } = "Tips: Use '{name}' in the WinnerMessage to announce the specific winner!";
        /// <summary>Message to display when a winner is picked.</summary>
        public string WinnerMessage { get; set; } = "Winner is {name}!";

        /// <summary>Extension data to capture extra properties.</summary>
        [JsonExtensionData] public Dictionary<string, object> ExtensionData { get; set; }
    }

    /// <summary>
    /// Maintains the active runtime state of a giveaway (entries, active status, history).
    /// Serialized to disk/global vars for persistence.
    /// </summary>
    public class GiveawayState
    {
        /// <summary>Unique ID for the current giveaway session.</summary>
        public string CurrentGiveawayId { get; set; }
        /// <summary>Whether the giveaway is currently open for entries.</summary>
        public bool IsActive { get; set; }
        /// <summary>Dictionary of user entries, keyed by User ID.</summary>
        public Dictionary<string, Entry> Entries { get; } = new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
        /// <summary>Log of major actions/events for this session.</summary>
        public List<string> HistoryLog { get; set; } = new List<string>();

        public GiveawayState()
        {
            _globalSpam = new List<DateTime>();
        }

        /// <summary>Scheduled auto-close time (if timer is active).</summary>
        public DateTime? AutoCloseTime { get; set; }
        /// <summary>Time when the giveaway was started.</summary>
        public DateTime? StartTime { get; set; }

        /// <summary>Name of the last selected winner.</summary>
        public string LastWinnerName { get; set; }
        /// <summary>User ID of the last selected winner.</summary>
        public string LastWinnerUserId { get; set; }

        /// <summary>Total number of winners drawn in this session.</summary>
        public int WinnerCount { get; set; }
        /// <summary>Cumulative entry count across all sessions (stat).</summary>
        public long CumulativeEntries { get; set; }

        [JsonIgnore] private readonly List<DateTime> _globalSpam;

        /// <summary>
        /// Sliding window spam detection: counts entries in the last N seconds (default 60).
        /// </summary>
        /// <param name="limit">Max entries allowed in the window.</param>
        /// <param name="windowSeconds">Window size in seconds.</param>
        /// <returns>True if spam is detected (count &gt; limit).</returns>
        public bool IsSpamming(int limit, int windowSeconds = 60)
        {
            lock (_globalSpam)
            {
                var n = DateTime.UtcNow;
                _globalSpam.Add(n);
                _globalSpam.RemoveAll(t => (n - t).TotalSeconds > windowSeconds);
                return _globalSpam.Count > limit;
            }
        }

        // Per-user redemption cooldown tracking
        /// <summary>Tracks cooldown timestamps for user redemptions.</summary>
        public Dictionary<string, DateTime> RedemptionCooldowns { get; set; } = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

        // Incremental entry dumping (batched)
        /// <summary>Queue of entries waiting to be dumped to disk.</summary>
        [JsonIgnore] public ConcurrentQueue<Entry> PendingDumps { get; set; } = new ConcurrentQueue<Entry>();
        /// <summary>Last time entries were successfully dumped.</summary>
        [JsonIgnore] public DateTime LastDumpTime { get; set; } = DateTime.MinValue;


    }

    /// <summary>
    /// Represents a single user entry in the giveaway.
    /// </summary>
    public class Entry
    {
        /// <summary>Streamer.bot User ID.</summary>
        public string UserId { get; set; }
        /// <summary>Display Name.</summary>
        public string UserName { get; set; }
        /// <summary>Optional: In-Game Name or other input provided at entry time.</summary>
        public string GameName { get; set; }
        /// <summary>True if user is a subscriber.</summary>
        public bool IsSub { get; set; }
        /// <summary>Time of entry.</summary>
        public DateTime EntryTime { get; set; }
        /// <summary>Number of tickets awarded (including luck multipliers).</summary>
        public int TicketCount { get; set; }
    }

#endregion

#region Integrations (Wheel, Messenger, OBS)
    /// <summary>
    /// Client for the WheelOfNames.com API v2.
    /// Used to create custom winner-picking wheels dynamically.
    /// </summary>
    public class WheelOfNamesClient
    {
        private static readonly HttpClient _defaultClient = CreateHttpClient();
        private readonly HttpClient _h;

        /// <summary>
        /// Create configured HttpClient with timeout and headers.
        /// </summary>
        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            // Use infinite timeout on the client itself; we control request timeout via CancellationToken
            client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
            client.DefaultRequestHeaders.Add("User-Agent", "GiveawayBot-StreamerBot/1.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="WheelOfNamesClient"/> class.
        /// </summary>
        /// <param name="handler">Optional HTTP message handler for mocking/testing.</param>
        public WheelOfNamesClient(HttpMessageHandler handler = null)
        {
            _h = handler != null ? new HttpClient(handler) : _defaultClient;
        }

        /// <summary>
        /// Selects a random message variant from a pipe-delimited string.
        /// </summary>
        /// <param name="rawMsg">The raw message string containing variants separated by pipes.</param>
        /// <returns>A single selected message variant.</returns>
        private string GetRandomMessage(string rawMsg)
        {
             return GiveawayManager.PickRandomMessage(rawMsg);
        }

        /// <summary>
        /// Reference to metrics container for API latency tracking (set by GiveawayManager).
        /// </summary>
        public MetricsContainer Metrics { get; set; }

        // Response models to avoid 'dynamic' and dependency on Microsoft.CSharp
        /// <summary>Internal API response container.</summary>
        private class WheelApiResponse { public WheelApiData Data { get; set; } }
        /// <summary>Internal API data container.</summary>
        private class WheelApiData { public string Path { get; set; } }
        /// <summary>Internal API error container.</summary>
        private class WheelApiError { public string Error { get; set; } }

        // API key validation response models
        private class ApiKeyValidationResponse { public ApiKeyData Data { get; set; } }
        private class ApiKeyData
        {
            public string Uid { get; set; }
            public long LastActive { get; set; }
            public long Calls { get; set; }
            public long Created { get; set; }
        }

        // API key validation cache to prevent excessive validation requests
        // Cache entries expire after 30 minutes
        private static readonly ConcurrentDictionary<string, (bool IsValid, DateTime ValidatedAt)> _keyValidationCache
            = new ConcurrentDictionary<string, (bool, DateTime)>();

        /// <summary>
        /// Creates a new wheel on WheelOfNames.com via the API v2.
        /// </summary>
        /// <param name="adapter">The CPH adapter for logging.</param>
        /// <param name="entries">The list of names/entries to put on the wheel.</param>
        /// <param name="apiKeyVarName">The NAME of the Global Variable containing the API key.</param>
        /// <param name="config">The wheel configuration (title, description, settings).</param>
        /// <param name="validateKey">Whether to perform a pre-flight validation of the API key.</param>
        /// <returns>The URL path of the created wheel (e.g., "/abc-123"), or null on failure.</returns>
        public async Task<string> CreateWheel(CPHAdapter adapter, List<string> entries, string apiKeyVarName, WheelConfig config, bool validateKey = true)
        {
            string key = adapter.GetGlobalVar<string>(apiKeyVarName, true);
            if (string.IsNullOrEmpty(key))
            {
                adapter.Logger?.LogWarn(adapter, "WheelAPI", $"API Key variable '{apiKeyVarName}' is empty.");
                return null;
            }

            // Decrypt if necessary
            if (key.StartsWith("ENC:"))
            {
                string decrypted = GiveawayManager.DecryptSecret(key);
                if (string.IsNullOrEmpty(decrypted))
                {
                    adapter.Logger?.LogWarn(adapter, "WheelAPI", "Failed to decrypt API Key. Ensure the bot is running under the same Windows user that encrypted it.");
                    return null;
                }
                key = decrypted;
            }

            // Optional: Pre-flight API key validation (with 30-minute caching)
            if (validateKey)
            {
                // Check cache first to avoid excessive validation requests
                var hasCachedEntry = _keyValidationCache.TryGetValue(key, out var cacheEntry);

                if (hasCachedEntry)
                {
                   var cacheMinutes = GiveawayManager.GlobalConfig?.Globals.ApiKeyValidationCacheMinutes ?? 30;
                   var cacheAge = DateTime.UtcNow - cacheEntry.ValidatedAt;
                   if (cacheAge < TimeSpan.FromMinutes(cacheMinutes))
                    {
                        // Cache is still valid
                        if (!cacheEntry.IsValid)
                        {
                            adapter.LogWarn("[WheelAPI] Skipping wheel creation: API key previously validated as invalid (cached).");
                            return null;
                        }
                        adapter.LogTrace("[WheelAPI] Using cached API key validation (still valid).");
                    }
                    else
                    {
                        // Cache expired, remove entry and re-validate below
                        _keyValidationCache.TryRemove(key, out var removedResult);
                        hasCachedEntry = false;
                    }
                }

                // If not cached or cache expired, validate now
                if (!hasCachedEntry)
                {
                    // Pre-flight Check:
                    // Before trying to create a wheel (which is complex and heavy), we quickly check if the key is valid.
                    // This saves time and prevents sending a massive payload to an invalid endpoint.
                    var validationResult = await ValidateApiKey(adapter, key);
                    if (validationResult == false)
                    {
                        // Invalid key - cache result and abort wheel creation
                        // We cache "False" so we don't keep asking the API "Is this key valid?" when we know it isn't.
                        _keyValidationCache[key] = (false, DateTime.UtcNow);
                        adapter.LogWarn("[WheelAPI] Skipping wheel creation: API key validation failed.");
                        return null;
                    }
                    else if (validationResult == true)
                    {
                        // Valid key - cache result for future calls
                        _keyValidationCache[key] = (true, DateTime.UtcNow);
                    }
                    // If null (indeterminate - network error, API down), proceed anyway but don't cache
                    // This allows wheel creation to attempt even if validation service is temporarily unavailable
                }
            }

            try
            {
                // API Payload Construction (JSON):
                // The Wheel of Names API requires a specific structure.
                // We use "Anonymous Objects" (new { ... }) to match that structure without defining a whole Class for it.
                // Structure:
                // {
                //   "wheelConfig": { "entries": [...], "title": "..." },
                //   "shareMode": "..."
                // }
                var p = new
                {
                    wheelConfig = new
                    {
                        entries = entries.Select(x => new { text = x }).ToList(),
                        title = config.Title,
                        description = config.Description,
                        spinTime = config.SpinTime,
                        autoRemoveWinner = config.AutoRemoveWinner,
                        displayWinnerDialog = true,
                        winningMessage = GetRandomMessage(config.WinnerMessage)
                    },
                    shareMode = config.ShareMode
                };
                var jsonPayload = JsonConvert.SerializeObject(p);

                adapter.LogDebug($"[WheelAPI] Requesting wheel creation for {entries.Count} unique names.");
                adapter.LogTrace($"[WheelAPI] Payload: {jsonPayload}");

                // Track API latency
                var apiStopwatch = Stopwatch.StartNew();

                var r = new HttpRequestMessage(HttpMethod.Post, "https://wheelofnames.com/api/v2/wheels");
                r.Headers.Add("x-api-key", key);
                r.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Use configured timeout for this specific request
                int timeoutSeconds = GiveawayManager.GlobalConfig?.Globals.HttpClientTimeoutSeconds ?? 30;
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds)))
                {
                    try
                    {
                        var rs = await _h.SendAsync(r, cts.Token);
                        var content = await rs.Content.ReadAsStringAsync();

                        // Stop tracking and update metrics
                        apiStopwatch.Stop();

                        if (apiStopwatch.ElapsedMilliseconds > 3000)
                        {
                             adapter.LogWarn($"[Performance] Wheel API Request took {apiStopwatch.ElapsedMilliseconds}ms (>3000ms). Check internet connection.");
                        }

                        if (Metrics != null)
                        {
                            Metrics.WheelApiTotalMs += apiStopwatch.ElapsedMilliseconds;
                            Metrics.WheelApiCalls++;
                        }

                        adapter.LogTrace($"[WheelAPI] Response Status: {(int)rs.StatusCode} {rs.StatusCode}");
                        adapter.LogTrace($"[WheelAPI] Response Body: {content}");

                        if (!rs.IsSuccessStatusCode)
                        {
                            // Try to parse structured error response {"error": "message"}
                            string errorMsg = content;
                            try
                            {
                                var errorObj = JsonConvert.DeserializeObject<WheelApiError>(content);
                                if (errorObj != null && !string.IsNullOrEmpty(errorObj.Error))
                                {
                                    errorMsg = errorObj.Error;
                                }
                            }
                            catch { } // If parsing fails, use raw content

                            GiveawayManager.TrackMetric(adapter, "WheelAPI_Errors");
                            adapter.Logger?.LogError(adapter, "WheelAPI", $"Creation failed: {(int)rs.StatusCode} - {errorMsg}");
                            return null;
                        }

                        // Success - parse URL path
                        var result = JsonConvert.DeserializeObject<WheelApiResponse>(content);
                        GiveawayManager.TrackMetric(adapter, "WheelAPI_Success");
                        return result?.Data?.Path;
                    }
                    catch (TaskCanceledException)
                    {
                        GiveawayManager.TrackMetric(adapter, "WheelAPI_Timeouts");
                        adapter.Logger?.LogError(adapter, "WheelAPI", $"Creation timed out after {timeoutSeconds}s");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveawayManager.TrackMetric(adapter, "WheelAPI_NetworkErrors");
                        adapter.Logger?.LogError(adapter, "WheelAPI", $"Network error: {ex.Message}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                GiveawayManager.TrackMetric(adapter, "WheelAPI_Errors");
                adapter.Logger?.LogError(adapter, "WheelAPI", $"Unexpected error in CreateWheel: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Validates a Wheel of Names API key by making a GET request to /api/v2/api-keys.
        /// This is a lightweight pre-flight check to verify key validity before creating wheels.
        /// Retrieves API key usage statistics: uid, calls count, creation date, last active date.
        /// </summary>
        /// <param name="adapter">CPH adapter for logging</param>
        /// <param name="apiKey">API key to validate</param>
        /// <returns>
        /// True if the key is valid and active, False if invalid/revoked.
        /// Null if validation request fails (network issue, API down, etc.)
        /// </returns>
        public async Task<bool?> ValidateApiKey(CPHAdapter adapter, string apiKey)
        {
            if (adapter == null || string.IsNullOrEmpty(apiKey))
            {
                adapter?.LogWarn("[WheelAPI] ValidateApiKey called with null adapter or empty key.");
                return false;
            }

            // Decrypt if necessary
    if (apiKey.StartsWith("Enter "))
    {
        adapter.LogDebug("[WheelAPI] Placeholder API key detected. Skipping validation.");
        adapter.SetGlobalVar(GiveawayConstants.GlobalWheelApiKeyStatus, "Placeholder", true);
        return false;
    }

    if (apiKey.StartsWith("ENC:"))
            {
                string decrypted = GiveawayManager.DecryptSecret(apiKey);
                if (string.IsNullOrEmpty(decrypted))
                {
                    adapter.Logger?.LogWarn(adapter, "WheelAPI", "Failed to decrypt API Key in ValidateApiKey.");
                    return false;
                }
                apiKey = decrypted;
            }

            try
            {
                adapter.LogDebug("[WheelAPI] Validating API key...");

                var request = new HttpRequestMessage(HttpMethod.Get, "https://wheelofnames.com/api/v2/api-keys");
                request.Headers.Add("x-api-key", apiKey);

                var response = await _h.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Try to parse the response to get API key metadata
                    try
                    {
                        var data = JsonConvert.DeserializeObject<ApiKeyValidationResponse>(content);
                        if (data?.Data != null)
                        {
                            var createdDate = DateTimeOffset.FromUnixTimeSeconds(data.Data.Created).DateTime;

                            // C# 7.3 compatible: Use explicit if/else instead of ternary with nullable types
                            DateTime? lastActiveDate;
                            if (data.Data.LastActive > 0)
                            {
                                lastActiveDate = DateTimeOffset.FromUnixTimeSeconds(data.Data.LastActive).DateTime;
                            }
                            else
                            {
                                lastActiveDate = null;
                            }

                            var lastActiveStr = lastActiveDate != null ? lastActiveDate.Value.ToString("yyyy-MM-dd") : "Never";
                            adapter.LogInfo($"[WheelAPI] API key validated successfully. " +
                                          $"Calls: {data.Data.Calls}, Created: {createdDate:yyyy-MM-dd}, " +
                                          $"Last Active: {lastActiveStr}");
                            return true;
                        }
                    }
                    catch (Exception parseEx)
                    {
                        adapter.LogWarn($"[WheelAPI] API key validation succeeded but response parsing failed: {parseEx.Message}");
                        return true; // Key is valid even if we can't parse metadata
                    }

                    adapter.LogInfo("[WheelAPI] API key validated successfully.");
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // Try to parse error message
                    string errorMsg = "Invalid or revoked API key.";
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<WheelApiError>(content);
                        if (errorObj != null && !string.IsNullOrEmpty(errorObj.Error))
                        {
                            errorMsg = errorObj.Error;
                        }
                    }
                    catch { }

                    adapter.LogWarn($"[WheelAPI] API key validation failed: {errorMsg}");
                    if (Metrics != null) Metrics.WheelApiInvalidKeys++;

                    if (GiveawayManager.GlobalConfig?.Globals?.EnableSecurityToasts == true)
                    {
                        adapter.ShowToastNotification("Giveaway Bot - Security", $"❌ Invalid/Revoked Wheel API Key! Check logs.");
                    }
                    return false;
                }
                else
                {
                    adapter.LogError($"[WheelAPI] API key validation request failed with HTTP {(int)response.StatusCode}: {content}");
                    if (Metrics != null) Metrics.WheelApiErrors++;
                    return null; // Indeterminate - network/server issue
                }
            }
            catch (TaskCanceledException)
            {
                adapter.LogError("[WheelAPI] API key validation timed out (30 seconds).");
                if (Metrics != null) Metrics.WheelApiTimeouts++;
                return null;
            }
            catch (HttpRequestException httpEx)
            {
                adapter.LogError($"[WheelAPI] Network error during API key validation: {httpEx.Message}");
                if (Metrics != null) Metrics.WheelApiNetworkErrors++;
                return null;
            }
            catch (Exception ex)
            {
                adapter.LogError($"[WheelAPI] Unexpected error during API key validation: {ex.Message}");
                if (Metrics != null) Metrics.WheelApiErrors++;
                return null;
            }
        }
    }

    /// <summary>
    /// Orchestrates message delivery across multiple streaming platforms based on live status.
    /// </summary>
    public class MultiPlatformMessenger
    {
        public GiveawayBotConfig Config { get; set; }

        /// <summary>
        /// Initializes a new instance of the MultiPlatformMessenger class.
        /// </summary>
        /// <param name="config">The main giveaway configuration.</param>
        public MultiPlatformMessenger(GiveawayBotConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// Registers handlers for giveaway events to trigger notifications.
        /// </summary>
        /// <param name="bus">The event bus to subscribe to.</param>
        public void Register(IEventBus bus)
        {
            // PUB/SUB PATTERN (Publish/Subscribe):
            // The Messenger "Subscribes" to specific events (like WinnerSelected).
            // When the GiveawayManager "Publishes" that event, this code runs automatically.
            // This keeps the "Business Logic" (Manager) separate from the "UI/Notification" (Messenger).
            bus.Subscribe<WinnerSelectedEvent>(OnWinnerSelected);
            bus.Subscribe<WheelReadyEvent>(OnWheelReady);
            bus.Subscribe<GiveawayStartedEvent>(OnGiveawayStarted);
            bus.Subscribe<GiveawayEndedEvent>(OnGiveawayEnded);
            bus.Subscribe<EntryAcceptedEvent>(OnEntryAccepted);
        }

        /// <summary>
        /// Handles the WinnerSelected event to broadcast the winner announcement.
        /// </summary>
        /// <param name="evt">The winner selected event data.</param>
        private void OnWinnerSelected(WinnerSelectedEvent evt)
        {
             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config))
             {
                 // Handle Toast
                 if (config.ToastNotifications.TryGetValue("WinnerSelected", out var notify) && notify)
                 {
                     evt.Adapter.ShowToastNotification("Giveaway Winner", $"Winner: {evt.Winner.UserName}!");
                 }

                 // Handle Broadcast
                 // Uses the template from the profile's WheelSettings
                 string rawTmpl = GiveawayManager.PickRandomMessage(config.WheelSettings.WinnerMessage);
                 string msg = rawTmpl?.Replace("{name}", evt.Winner.UserName);
                 if (!string.IsNullOrEmpty(msg))
                 {
                     SendBroadcast(evt.Adapter, msg, evt.Source);
                 }

                 // --- Discord Integration ---
                 // Priority Hierarchy:
                 // 1. Native Discord Integration (Channel ID) - Preferred/Cleaner
                 // 2. Webhook URL - Fallback if native integration isn't set up
                 if (!string.IsNullOrEmpty(config.DiscordChannelId) || !string.IsNullOrEmpty(config.DiscordWebhookUrl))
                 {
                     string discordMsg = config.DiscordMessage?.Replace("{winner}", evt.Winner.UserName)
                                         ?? $"Congratulations {evt.Winner.UserName}!";

                     if (!string.IsNullOrEmpty(config.DiscordChannelId))
                     {
                         SendDiscordNative(evt.Adapter, config.DiscordChannelId, discordMsg);
                     }
                     else if (!string.IsNullOrEmpty(config.DiscordWebhookUrl))
                     {
                         SendDiscordWebhook(evt.Adapter, config.DiscordWebhookUrl, discordMsg);
                     }
                 }
             }
        }

        /// <summary>
        /// Handles the WheelReady event to broadcast the wheel link.
        /// </summary>
        /// <param name="evt">The wheel ready event data.</param>
        private void OnWheelReady(WheelReadyEvent evt)
        {
             // Broadcast the wheel link
             SendBroadcast(evt.Adapter, $"Wheel Ready! {evt.Url}", evt.Source);
        }

        /// <summary>
        /// Handles the GiveawayStarted event (notifications only, broadcast handled by logic).
        /// </summary>
        private void OnGiveawayStarted(GiveawayStartedEvent evt)
        {
             // Broadcast handled by HandleStart directly

             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config) && config.ToastNotifications.TryGetValue("GiveawayOpened", out var notify) && notify)
             {
                 evt.Adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{evt.ProfileName}' is OPEN!");
             }
        }

        /// <summary>
        /// Handles the GiveawayEnded event (notifications only, broadcast handled by logic).
        /// </summary>
        private void OnGiveawayEnded(GiveawayEndedEvent evt)
        {
             // Broadcast handled by HandleEnd directly

             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config) && config.ToastNotifications.TryGetValue("GiveawayClosed", out var notify) && notify)
             {
                 evt.Adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{evt.ProfileName}' is CLOSED!");
             }
        }

        /// <summary>
        /// Handles the EntryAccepted event to send confirmation replies and toast notifications.
        /// </summary>
        private void OnEntryAccepted(EntryAcceptedEvent evt)
        {
             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config))
             {
                 // Send entry confirmation message as a reply to the user
                 string acceptedMsg = Loc.Get("EntryAccepted", evt.ProfileName, evt.Entry.TicketCount, config.SubLuckMultiplier);
                 if (evt.Entry.IsSub && config.SubLuckMultiplier == 0)
                 {
                     acceptedMsg = Loc.Get("EntryAccepted_NoLuck", evt.ProfileName, evt.Entry.TicketCount);
                 }

                 // Reply directly to the user with platform-specific threading
                 SendReply(evt.Adapter, acceptedMsg, evt.Source, evt.Entry.UserName, evt.MessageId);

                 // Handle Toast notification
                 if (config.ToastNotifications.TryGetValue("EntryAccepted", out var notify) && notify)
                 {
                     evt.Adapter.ShowToastNotification("Giveaway Bot", $"New Entry: {evt.Entry.UserName}");
                 }
             }
        }

        /// <summary>
        /// Sends a message to one or more platforms based on their live status and configuration.
        /// </summary>
        /// <param name="adapter">CPH adapter context.</param>
        /// <param name="message">The message to broadcast.</param>
        /// <param name="sourcePlatform">The platform where the event originated (to prioritize reply).</param>
        public void SendBroadcast(CPHAdapter adapter, string message, string sourcePlatform = "Twitch")
        {
            bool enableMulti = adapter.GetGlobalVar<bool>(GiveawayConstants.GlobalMultiPlatform, true);
            var settings = Config.Globals;
            var targets = settings.EnabledPlatforms ?? new List<string> { "Twitch", "YouTube", "Kick" };

            adapter.LogTrace($"[Messenger] SendBroadcast: {message} (Source: {sourcePlatform}, Multi: {enableMulti})");

            List<string> livePlatforms = new List<string>();
            if (targets.Contains("Twitch") && adapter.IsTwitchLive()) livePlatforms.Add("Twitch");
            if (targets.Contains("YouTube") && adapter.IsYouTubeLive()) livePlatforms.Add("YouTube");
            if (targets.Contains("Kick") && adapter.IsKickLive()) livePlatforms.Add("Kick");

            adapter.LogVerbose($"[Messenger] Live Platforms: {string.Join(", ", livePlatforms)} (Multi: {enableMulti}, Source: {sourcePlatform})");

            // 1. Always prioritize responding to the source platform, even if not "Live"
            if (sourcePlatform != null && targets.Contains(sourcePlatform))
            {
                adapter.LogTrace($"[Messenger] Direct Reply targeted for {sourcePlatform}");
                SendMessageToPlatform(adapter, sourcePlatform, message);

                // If multi-platform IS NOT enabled, we stop here
                if (!enableMulti) return;
            }

            // 2. Multi-Platform Mirroring (only if enabled and stream is live)
            if (enableMulti && livePlatforms.Count > 0)
            {
                adapter.LogTrace($"[Messenger] Multi-platform broadcast: {enableMulti}, Platforms: {string.Join(", ", livePlatforms)}");
                foreach (var p in livePlatforms)
                {
                    if (p == sourcePlatform) continue; // Already sent above
                    SendMessageToPlatform(adapter, p, message);
                }
            }
            else if (livePlatforms.Count == 0 && (sourcePlatform == null || !targets.Contains(sourcePlatform)))
            {
                // 3. Fallback only if we have no source and no live platforms
                adapter.LogDebug($"[Messenger] No source/live platforms. Fallback: {settings.FallbackPlatform}");
                SendMessageToPlatform(adapter, settings.FallbackPlatform, message);
            }
        }

        /// <summary>
        /// Sends a message to a specific platform, appending the anti-loop token.
        /// </summary>
        /// <param name="adapter">CPH adapter context.</param>
        /// <param name="platform">Target platform (Twitch, YouTube, Kick).</param>
        /// <param name="msg">Message content.</param>
        private static void SendMessageToPlatform(CPHAdapter adapter, string platform, string msg)
        {
            // Append invisible anti-loop token to prevent bot from processing its own messages
            // Uses zero-width space (U+200B) which is invisible to users but detectable by our system
            string msgWithToken = msg + GiveawayManager.ANTI_LOOP_TOKEN;

            adapter.LogTrace($"[Messenger] Routing message to {platform} (with anti-loop token)");
            switch (platform.ToLower())
            {
                case "youtube": adapter.SendYouTubeMessage(msgWithToken); break;
                case "kick": adapter.SendKickMessage(msgWithToken); break;
                default: adapter.SendMessage(msgWithToken); break; // Default to Twitch
            }
        }

        /// <summary>
        /// Sends a message as a direct reply to a specific user using platform-specific reply features.
        /// For Twitch, uses threaded replies when msgId is available.
        /// </summary>
        /// <param name="adapter">CPH adapter context.</param>
        /// <param name="msg">Message content.</param>
        /// <param name="platform">Target platform (Twitch, YouTube, Kick).</param>
        /// <param name="userName">Username to reply to.</param>
        /// <param name="messageId">Message ID for threaded replies (Twitch msgId).</param>
        public void SendReply(CPHAdapter adapter, string msg, string platform, string userName, string messageId = null)
        {
            // Add anti-loop token
            string msgWithToken = msg + GiveawayManager.ANTI_LOOP_TOKEN;

            // Use platform-specific threaded replies when messageId is available
            if (!string.IsNullOrEmpty(messageId))
            {
                adapter.LogTrace($"[Messenger] Sending threaded reply to {userName} on {platform} (msgId: {messageId})");
                switch (platform.ToLower())
                {
                    case "twitch":
                        // Use Twitch's native threaded reply feature
                        adapter.TwitchReplyToMessage(msgWithToken, messageId, true, true);
                        return;
                    case "youtube":
                    case "kick":
                        // YouTube/Kick don't have native threaded replies in Streamer.bot API
                        // Fall through to @mention
                        break;
                }
            }

            // Fallback: Use @username mention for other platforms or when msgId unavailable
            adapter.LogTrace($"[Messenger] Sending @mention reply to {userName} on {platform}");
            string replyMsg = $"@{userName} {msg}";
            SendMessageToPlatform(adapter, platform, replyMsg);
        }

        /// <summary>
        /// Sends a message to a Discord channel via Streamer.bot native integration.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="channelId">The Discord channel ID.</param>
        /// <param name="message">The message content to send.</param>
        private void SendDiscordNative(CPHAdapter adapter, string channelId, string message)
        {
            adapter.LogDebug($"[Discord] Sending Native Message to Channel {channelId}: {message}");
            adapter.DiscordSendMessage(channelId, message);
        }

        /// <summary>
        /// Sends a message via Discord Webhook.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="webhookUrl">The Discord webhook URL.</param>
        /// <param name="message">The message content to send.</param>
        private void SendDiscordWebhook(CPHAdapter adapter, string webhookUrl, string message)
        {
            adapter.LogDebug($"[Discord] Sending Webhook to {webhookUrl}: {message}");
            try
            {
                using (var client = new HttpClient())
                {
                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(new { content = message });
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    // Blocking call to ensure delivery implies .Result
                    var response = client.PostAsync(webhookUrl, content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        adapter.LogWarn($"[Discord] Webhook failed: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Discord] Webhook exception: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Controls OBS sources (BrowserSource) for displaying the giveaway wheel.
    /// Wraps CPH OBS methods.
    /// </summary>
    public class ObsController
    {
        private readonly GiveawayBotConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObsController"/> class.
        /// </summary>
        public ObsController(GiveawayBotConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Registers a handler for relevant giveaway events (WheelReady).
        /// </summary>
        public void Register(IEventBus bus)
        {
            bus.Subscribe<WheelReadyEvent>(OnWheelReady);
        }

        /// <summary>
        /// Handles the WheelReady event to update the OBS browser source with the new wheel URL.
        /// </summary>
        private void OnWheelReady(WheelReadyEvent evt)
        {
            if (_config == null || !_config.Profiles.TryGetValue(evt.ProfileName, out var profile)) return;

            if (profile.EnableObs)
            {
                SetBrowserSource(evt.Adapter, profile.ObsScene, profile.ObsSource, evt.Url);
            }
        }

        /// <summary>
        /// Updates a Streamer.bot OBS Browser Source with a new URL (e.g., the Wheel of Names link).
        /// </summary>
        /// <param name="adapter">CPH Adapter for OBS calls.</param>
        /// <param name="scene">Name of the OBS scene.</param>
        /// <param name="source">Name of the Browser Source.</param>
        /// <param name="url">New URL to set.</param>
        public void SetBrowserSource(CPHAdapter adapter, string scene, string source, string url)
        {
            if (adapter == null) return;
            adapter.ObsSetBrowserSource(scene, source, url);
        }
    }

#endregion

#region Metrics Models
    /// <summary>
    /// Container for all bot metrics and statistics.
    /// Serialized to JSON for data persistence.
    /// </summary>
    public class MetricsContainer
    {
        public Dictionary<string, long> GlobalMetrics { get; set; } = new Dictionary<string, long>();
        public Dictionary<string, UserMetricSet> UserMetrics { get; set; } = new Dictionary<string, UserMetricSet>();
        public DateTime LastUpdated { get; set; }

        // Enhanced metrics for Phase 9 monitoring
        /// <summary>Current size of the message ID deduplication cache</summary>
        public int MessageIdCacheSize { get; set; }

        /// <summary>Count of message ID cleanup operations performed</summary>
        public int MessageIdCleanupCount { get; set; }

        /// <summary>Total count of loop detections across all layers</summary>
        public int LoopDetectedCount { get; set; }

        /// <summary>Count of loops detected via message ID deduplication</summary>
        public int LoopDetectedByMsgId { get; set; }

        /// <summary>Count of loops detected via invisible token check</summary>
        public int LoopDetectedByToken { get; set; }

        /// <summary>Count of loops detected via bot source flag</summary>
        public int LoopDetectedByBotFlag { get; set; }

        /// <summary>Count of configuration reloads</summary>
        public int ConfigReloadCount { get; set; }

        /// <summary>Count of file I/O errors</summary>
        public int FileIOErrors { get; set; }

        // Phase 10 additional metrics
        /// <summary>Total time spent processing entries (milliseconds)</summary>
        public long TotalEntryProcessingMs { get; set; }

        /// <summary>Count of entries processed (for average calculation)</summary>
        public int EntriesProcessedCount { get; set; }

        /// <summary>Count of winner draw attempts</summary>
        public int WinnerDrawAttempts { get; set; }

        /// <summary>Count of successful winner draws</summary>
        public int WinnerDrawSuccesses { get; set; }

        /// <summary>Total time for Wheel API calls (milliseconds)</summary>
        public long WheelApiTotalMs { get; set; }

        /// <summary>Count of Wheel API calls (for average latency calculation)</summary>
        public int WheelApiCalls { get; set; }

        // Error & Diagnostic Metrics
        public int ApiErrors { get; set; }
        public int WheelApiErrors { get; set; }
        public int WheelApiInvalidKeys { get; set; }
        public int WheelApiTimeouts { get; set; }
        public int WheelApiNetworkErrors { get; set; }
    }



#endregion

#region Event Bus
    /// <summary>
    /// Event Bus Interface for decoupled communication.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes a handler to a specific event type.
        /// </summary>
        /// <typeparam name="T">The event type to subscribe to.</typeparam>
        /// <param name="handler">The action to execute when the event is published.</param>
        void Subscribe<T>(Action<T> handler) where T : IGiveawayEvent;

        /// <summary>
        /// Unsubscribes a handler from a specific event type.
        /// </summary>
        /// <typeparam name="T">The event type to unsubscribe from.</typeparam>
        /// <param name="handler">The handler to remove.</param>
        void Unsubscribe<T>(Action<T> handler) where T : IGiveawayEvent;

        /// <summary>
        /// Publishes an event to all subscribed handlers.
        /// </summary>
        /// <typeparam name="T">The event type to publish.</typeparam>
        /// <param name="evt">The event data.</param>
        void Publish<T>(T evt) where T : IGiveawayEvent;
    }

    /// <summary>
    /// Base interface for all giveaway events.
    /// </summary>
    public interface IGiveawayEvent
    {
        /// <summary>The CPH adapter context.</summary>
        CPHAdapter Adapter { get; }
        /// <summary>The name of the giveaway profile.</summary>
        string ProfileName { get; }
        /// <summary>The current state of the giveaway.</summary>
        GiveawayState State { get; }
        /// <summary>The source platform or trigger.</summary>
        string Source { get; }
    }

    /// <summary>
    /// Thread-safe Event Bus implementation.
    /// </summary>
    public class GiveawayEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();
        private readonly object _lock = new object();

        /// <summary>
        /// Subscribes a handler to a specific event type.
        /// </summary>
        /// <typeparam name="T">The event type to subscribe to.</typeparam>
        /// <param name="handler">The action to execute when the event is published.</param>
        public void Subscribe<T>(Action<T> handler) where T : IGiveawayEvent
        {
            lock (_lock)
            {
                if (!_handlers.ContainsKey(typeof(T)))
                {
                    _handlers[typeof(T)] = new List<Delegate>();
                }
                _handlers[typeof(T)].Add(handler);
            }
        }

        /// <summary>
        /// Unsubscribes a handler from a specific event type.
        /// </summary>
        /// <typeparam name="T">The event type to unsubscribe from.</typeparam>
        /// <param name="handler">The handler to remove.</param>
        public void Unsubscribe<T>(Action<T> handler) where T : IGiveawayEvent
        {
            lock (_lock)
            {
                if (_handlers.ContainsKey(typeof(T)))
                {
                    _handlers[typeof(T)].Remove(handler);
                }
            }
        }

        /// <summary>
        /// Publishes an event to all subscribed handlers.
        /// </summary>
        /// <typeparam name="T">The event type to publish.</typeparam>
        /// <param name="evt">The event data.</param>
        public void Publish<T>(T evt) where T : IGiveawayEvent
        {
            List<Delegate> handlersToInvoke = null;
            lock (_lock)
            {
                if (_handlers.TryGetValue(typeof(T), out var list))
                {
                    handlersToInvoke = list.ToList();
                }
            }

            if (handlersToInvoke != null)
            {
                foreach (var handler in handlersToInvoke)
                {
                    try
                    {
                        ((Action<T>)handler)(evt);
                    }
                    catch (Exception ex)
                    {
                        // Log but don't crash the bus
                        evt.Adapter?.LogError($"[EventBus] Error handling event {typeof(T).Name}: {ex.Message}");
                    }
                }
            }
        }
    }

#endregion

#region Core Events
    public abstract class GiveawayEventBase : IGiveawayEvent
    {
        /// <summary>The CPH adapter context.</summary>
        public CPHAdapter Adapter { get; }
        /// <summary>The name of the giveaway profile.</summary>
        public string ProfileName { get; }
        /// <summary>The current state of the giveaway.</summary>
        public GiveawayState State { get; }
        /// <summary>The source platform or trigger.</summary>
        public string Source { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveawayEventBase"/> class.
        /// </summary>
        /// <param name="adapter">CPH adapter context.</param>
        /// <param name="profileName">Name of the giveaway profile.</param>
        /// <param name="state">Current giveaway state.</param>
        /// <param name="source">Source platform/trigger.</param>
        protected GiveawayEventBase(CPHAdapter adapter, string profileName, GiveawayState state, string source)
        {
            Adapter = adapter;
            ProfileName = profileName;
            State = state;
            Source = source;
        }
    }

    /// <summary>
    /// Event triggered when a giveaway is started/opened.
    /// </summary>
    public class GiveawayStartedEvent : GiveawayEventBase
    {
        /// <summary>Initializes a new instance of the <see cref="GiveawayStartedEvent"/> class.</summary>
        public GiveawayStartedEvent(CPHAdapter adapter, string profileName, GiveawayState state, string source) : base(adapter, profileName, state, source) { }
    }

    /// <summary>
    /// Event triggered when a giveaway is ended/closed.
    /// </summary>
    public class GiveawayEndedEvent : GiveawayEventBase
    {
        /// <summary>Initializes a new instance of the <see cref="GiveawayEndedEvent"/> class.</summary>
        public GiveawayEndedEvent(CPHAdapter adapter, string profileName, GiveawayState state, string source) : base(adapter, profileName, state, source) { }
    }

    /// <summary>
    /// Event triggered when a winner is selected.
    /// </summary>
    public class WinnerSelectedEvent : GiveawayEventBase
    {
        /// <summary>The winning entry.</summary>
        public Entry Winner { get; }

        /// <summary>Initializes a new instance of the <see cref="WinnerSelectedEvent"/> class.</summary>
        public WinnerSelectedEvent(CPHAdapter adapter, string profileName, GiveawayState state, Entry winner, string source) : base(adapter, profileName, state, source)
        {
            Winner = winner;
        }
    }

    /// <summary>
    /// Event triggered when a valid entry is accepted.
    /// </summary>
    public class EntryAcceptedEvent : GiveawayEventBase
    {
        /// <summary>The accepted entry details.</summary>
        public Entry Entry { get; }
        /// <summary>Twitch Message ID for threading (optional).</summary>
        public string MessageId { get; }

        /// <summary>Initializes a new instance of the <see cref="EntryAcceptedEvent"/> class.</summary>
        public EntryAcceptedEvent(CPHAdapter adapter, string profileName, GiveawayState state, Entry entry, string source, string messageId = null) : base(adapter, profileName, state, source)
        {
            Entry = entry;
            MessageId = messageId;
        }
    }

    /// <summary>
    /// Event triggered when a Wheel of Names link is generated and ready.
    /// </summary>
    public class WheelReadyEvent : GiveawayEventBase
    {
        /// <summary>The URL of the generated wheel.</summary>
        public string Url { get; }

        /// <summary>Initializes a new instance of the <see cref="WheelReadyEvent"/> class.</summary>
        public WheelReadyEvent(CPHAdapter adapter, string profileName, GiveawayState state, string url, string source) : base(adapter, profileName, state, source)
        {
            Url = url;
        }
    }



#endregion

#region Metrics Service
    /// <summary>
    /// Container for user-specific metrics.
    /// </summary>
    public class UserMetricSet
    {
        /// <summary>The username.</summary>
        public string UserName { get; set; }
        /// <summary>Optional: In-Game Name or other input provided at entry time.</summary>
        public string GameName { get; set; }
        /// <summary>Key-value pairs of metrics for this user.</summary>
        public Dictionary<string, long> Metrics { get; set; } = new Dictionary<string, long>();
    }

    public class MetricsService
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the MetricsService.
        /// Sets the path to the metrics storage file.
        /// </summary>
        public MetricsService()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "data", "metrics.json");
        }

        /// <summary>
        /// Saves metrics data to a JSON file.
        /// </summary>
        /// <param name="adapter">CPH adapter for logging.</param>
        /// <param name="metrics">The metrics container to save.</param>
        public void SaveMetrics(CPHAdapter adapter, MetricsContainer metrics)
        {
            try
            {
                string stateDir = Path.GetDirectoryName(_path);
                if (!string.IsNullOrEmpty(stateDir) && !Directory.Exists(stateDir)) Directory.CreateDirectory(stateDir);
                var json = JsonConvert.SerializeObject(metrics, Formatting.Indented);
                File.WriteAllText(_path, json);
                adapter.LogTrace($"[MetricsService] [SaveMetrics] Metrics successfully saved to disk: {_path}");
            }
            catch (Exception ex)
            {
                adapter.LogError($"[MetricsService] [SaveMetrics] Failed to save metrics: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads metrics data from the JSON file.
        /// </summary>
        /// <param name="adapter">CPH adapter for logging.</param>
        /// <returns>The loaded MetricsContainer, or a new empty one if load fails.</returns>
        public MetricsContainer LoadMetrics(CPHAdapter adapter)
        {
            try
            {
                if (!File.Exists(_path)) return new MetricsContainer();
                var json = File.ReadAllText(_path);
                var metrics = JsonConvert.DeserializeObject<MetricsContainer>(json);
                return metrics ?? new MetricsContainer();
            }
            catch (Exception ex)
            {
                adapter.LogError($"[MetricsService] [LoadMetrics] Failed to load metrics: {ex.Message}");
                return new MetricsContainer();
            }
        }
    }

#pragma warning restore IDE0090 // 'new' expression can be simplified
#pragma warning restore IDE0300 // Use collection expression
#pragma warning restore IDE0028 // Simplify collection initialization

#endregion

#region Update Service
    /// <summary>
    /// Service to check for updates from the official repository.
    /// Downloads updates to 'Giveaway Bot/updates/' as text files.
    /// </summary>
    public static class UpdateService
    {
        private const string RepoOwner = "Sythsaz";
        private const string RepoName = "Giveaway-Bot";
        private static readonly HttpClient _httpClient = new HttpClient();

        static UpdateService()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Sythsaz-StreamerBot-GiveawayBot");
        }

        /// <summary>
        /// Checks for updates and downloads them if available.
        /// </summary>
        /// <param name="adapter">The CPH adapter context.</param>
        /// <param name="currentVersion">The current version string of the bot.</param>
        /// <param name="notifyIfUpToDate">If true, sends a notification even if no update is found.</param>
        public static async Task CheckForUpdatesAsync(CPHAdapter adapter, string currentVersion, bool notifyIfUpToDate = false)
        {
            try
            {
                // 1. Get Latest Release Info
                var release = await GetLatestReleaseInfoAsync(adapter);
                if (release == null)
                {
                    if (notifyIfUpToDate) adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("Update_CheckFailed"));
                    return;
                }

                string remoteTag = release.TagName;
                string remoteVersion = remoteTag.TrimStart('v');

                // 2. Compare Versions
                if (IsNewer(remoteVersion, currentVersion))
                {
                    adapter.LogInfo($"[UpdateService] [CheckForUpdatesAsync] Update Available: {currentVersion} -> {remoteVersion}");
                    adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("Update_Available", remoteTag));

                    // 3. Download
                    string savedPath = await DownloadUpdateAsync(adapter, remoteTag);
                    if (!string.IsNullOrEmpty(savedPath))
                    {
                        string fileName = Path.GetFileName(savedPath);
                        adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("Update_Downloaded", fileName));
                        adapter.LogInfo($"[UpdateService] [CheckForUpdatesAsync] Update saved to: {savedPath}");

                        // Copy to clipboard instructions? No, just log.
                        adapter.LogInfo($"[UpdateService] [CheckForUpdatesAsync] INSTRUCTIONS: Open 'Giveaway Bot - Main', select 'Import', and pick '{savedPath}'.");
                    }
                    else
                    {
                        adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("Update_FailedDownload"));
                    }
                }
                else if (notifyIfUpToDate)
                {
                    adapter.LogInfo("[UpdateService] [CheckForUpdatesAsync] Bot is up to date.");
                    adapter.ShowToastNotification(Loc.Get("ToastTitle"), Loc.Get("Update_UpToDate", currentVersion));
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[UpdateService] [CheckForUpdatesAsync] Update Check Failed: {ex.Message}");
                if (notifyIfUpToDate) adapter.ShowToastNotification("Update Error", "❌ Unexpected error.");
            }
        }

        /// <summary>
        /// Retrieves the latest release information from GitHub via the API.
        /// </summary>
        /// <param name="adapter">The CPH adapter for logging.</param>
        /// <returns>A ReleaseInfo object containing tag name, name, and body; or null if failed.</returns>
        private static async Task<ReleaseInfo> GetLatestReleaseInfoAsync(CPHAdapter adapter)
        {
            try
            {
                // Use shared client with cancellation token for timeout
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    string url = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

                    var response = await _httpClient.GetAsync(url, cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        adapter.LogWarn($"[UpdateService] [GetLatestReleaseInfoAsync] API Error: {response.StatusCode}");
                        return null;
                    }

                    string json = await response.Content.ReadAsStringAsync();
                    // Basic JSON parsing using Newtonsoft
                    // Use JObject or strong type instead of dynamic to avoid CS0656 (missing Microsoft.CSharp ref)
                    var obj = JsonConvert.DeserializeObject<GitHubRelease>(json);

                    if (obj == null) return null;

                    return new ReleaseInfo
                    {
                        TagName = obj.TagName,
                        Name = obj.Name,
                        Body = obj.Body
                    };
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[UpdateService] [GetLatestReleaseInfoAsync] API Exception: {ex.Message}");
                return null;
            }
        }

        // Helper class for JSON deserialization
        private class GitHubRelease
        {
            [JsonProperty("tag_name")] public string TagName { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("body")] public string Body { get; set; }
        }

        /// <summary>
        /// Downloads the raw C# file for the specified tag from GitHub.
        /// </summary>
        /// <param name="adapter">The CPH adapter for logging.</param>
        /// <param name="tag">The git tag to download (e.g., "v1.0.0").</param>
        /// <returns>The full path to the downloaded file, or null if failed.</returns>
        private static async Task<string> DownloadUpdateAsync(CPHAdapter adapter, string tag)
        {
            try
            {
                // Use shared client (default timeout is fine for download)
                {
                    // Raw content URL
                    string url = $"https://raw.githubusercontent.com/{RepoOwner}/{RepoName}/{tag}/GiveawayBot.cs";

                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return null;

                    string content = await response.Content.ReadAsStringAsync();

                    // Validation
                    if (!content.Contains("class GiveawayBot"))
                    {
                        adapter.LogError("[UpdateService] [DownloadUpdateAsync] Downloaded content validation failed.");
                        return null;
                    }

                    // Save to 'updates' folder
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    // Try to duplicate the folder structure if we can, or just putting it in Giveaway Bot/updates
                    string updateFolder = Path.Combine(baseDir, "Giveaway Bot", "updates");
                    if (!Directory.Exists(updateFolder)) Directory.CreateDirectory(updateFolder);

                    string filename = $"GiveawayBot_{tag}.cs.txt"; // .txt to prevent accidental compilation or confusion
                    string fullPath = Path.Combine(updateFolder, filename);

                    File.WriteAllText(fullPath, content);
                    return fullPath;
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[UpdateService] [DownloadUpdateAsync] Download Exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Compares two version strings to determine if the remote version is newer.
        /// </summary>
        /// <param name="remote">The remote version string.</param>
        /// <param name="local">The local version string.</param>
        /// <returns>True if remote is newer, otherwise false.</returns>
        private static bool IsNewer(string remote, string local)
        {
            // First, try to parse as proper Semantic Versions (e.g. 1.0.0 vs 1.0.1)
            // This handles cases like 1.10 > 1.2 correctly (which string comparison gets wrong).
            if (Version.TryParse(remote, out Version vRemote) && Version.TryParse(local, out Version vLocal))
            {
                return vRemote > vLocal;
            }
            // Fallback: If non-numeric (e.g. "beta-1"), use simple alphabetical sort.
            return string.Compare(remote, local, StringComparison.OrdinalIgnoreCase) > 0;
        }

        private class ReleaseInfo
        {
            public string TagName { get; set; }
            public string Name { get; set; }
            public string Body { get; set; }
        }
    }
#endregion

#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
}
#endif
