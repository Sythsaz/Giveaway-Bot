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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// UNCONDITIONAL SUPPRESSION for Legacy C# Environment
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

    /// <summary>
    /// Interface wrapper for Streamer.bot's `CPH` object.
    /// Allows us to mock the CPH interaction for unit testing outside of Streamer.bot.
    /// </summary>
    public interface IGiveawayCPH
    {
        void LogInfo(string message); void LogWarn(string message); void LogDebug(string message); void LogError(string message); void LogTrace(string message); void LogVerbose(string message); void LogFatal(string message);
        bool TryGetArg<T>(string argName, out T value);
        T GetGlobalVar<T>(string varName, bool persisted = true);
        void SetGlobalVar(string varName, object value, bool persisted = true);
        T GetUserVar<T>(string userId, string varName, bool persisted = true);
        void SetUserVar(string userId, string varName, object value, bool persisted = true);
        List<string> GetGlobalVarNames(bool persisted = true);
        void UnsetGlobalVar(string varName, bool persisted = true);
        void SendMessage(string message, bool bot = true);
        void SendYouTubeMessage(string message);
        void SendKickMessage(string message);
        void TwitchReplyToMessage(string message, string replyId, bool useBot = true, bool fallback = true);
        bool IsTwitchLive();
        bool IsYouTubeLive();
        bool IsKickLive();
        void ObsSetBrowserSource(string scene, string source, string url);
        void ShowToastNotification(string title, string message);
        object GetEventType();
        FileLogger Logger { get; set; }
    }

/*
 * Streamer.bot Giveaway Bot
 * 
 * Target Environment: Streamer.bot Execute C# Code Action
 * Compatibility: .NET Framework 4.8 / C# 7.3
 * 
 * Description: 
 * A comprehensive giveaway management system for Streamer.bot.
 * Features include multi-profile support, anti-loop protection, 
 * Wheel of Names integration, OBS control, and robust logging/persistence.
 * 
 * Rules & constraints:
 * - Must reside in a single file for CPHInline copy/paste deployment.
 * - Must NOT use C# 8.0+ features (e.g. file-scoped namespaces, records).
 * - Must handle CPH interaction via reflection or direct calls safely.
 */

/*--------------------------------------------*/
#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
    // Base class validation to ensure editor compatibility without external references
    public class GiveawayBotHostBase { public dynamic CPH { get; set; } }
    public class GiveawayBot : GiveawayBotHostBase
#else
public class CPHInline
#endif
    /*--------------------------------------------*/
    {
        // Singleton instance to maintain state across multiple executions of the Execute() method
        private static GiveawayManager _manager;
        // Lock object to ensure thread-safe initialization of the singleton
        private static readonly object _initLock = new object();

        /// <summary>
        /// Entry point for the Streamer.bot Action.
        /// This method is called every time the action is triggered.
        /// It ensures the manager is initialized and then processes the current trigger.
        /// </summary>
        public bool Execute()
        {
            var adapter = new CPHAdapter(CPH);

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

            adapter.LogTrace($"[Execute] >>> Entry point triggered. RunMode: {runMode}");
            try
            {
                EnsureInitialized(adapter);
                // Pass the CPH adapter to ensure the Logger and state persist correctly
                return _manager.ProcessTrigger(adapter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                adapter.LogFatal($"[Execute] Unhandled exception in root: {ex.Message}");
                // Fallback direct bot log if adapter fails
                try { CPH.LogError($"[GiveawayBot] FATAL CRASH: {ex.Message}"); } catch { }
                return false;
            }
            finally
            {
                adapter.LogTrace("[Execute] <<< Exit point reached.");
            }
        }

        private static void EnsureInitialized(CPHAdapter adapter)
        {
            if (_manager != null) return;

            lock (_initLock)
            {
                if (_manager != null) return;

                adapter.LogDebug("[Execute] Initializing Giveaway Manager singleton...");

                _manager = new GiveawayManager
                {
                    Logger = new FileLogger()
                };
                adapter.Logger = _manager.Logger;

                adapter.LogVerbose("[Execute] FileLogger initialized and attached to CPH adapter.");

                _manager.Initialize(adapter);
                adapter.LogInfo("[Execute] Giveaway System initialized successfully.");
            }
        }
    }
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
            { "EntryRejected_GiveawayClosed", "The giveaway is currently closed." },

            // Draw
            { "WinnerSelected", "🎉 Congratulations @{0}! You have won the giveaway!" },
            { "WinnerDrawn_Log", "Winner drawn: {0} (Tickets: {1})" },
            
            // State
            { "GiveawayOpened", "🎟 The giveaway for '{0}' is now OPEN! Type !enter to join." },
            { "GiveawayOpened_NoProfile", "🎟 The giveaway is now OPEN! Type !enter to join." },
            { "GiveawayClosed", "🚫 The giveaway is now CLOSED! No more entries accepted." },
            { "GiveawayFull", "🚫 The giveaway is FULL! No more entries accepted." },
            { "TimerUpdated", "⏳ Time limit updated! Ends in {0}." },
            
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

        // 1. Try Profile Overrides
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

        // 2. Try Global Custom Strings
        if (template == null && GiveawayManager.GlobalConfig?.Globals?.CustomStrings != null)
        {
            if (GiveawayManager.GlobalConfig.Globals.CustomStrings.TryGetValue(key, out var overrideStr))
            {
                template = overrideStr;
            }
        }

        // 3. Try Default Dictionary
        if (template == null && _defaults.TryGetValue(key, out var defStr))
        {
            template = defStr;
        }

        // 4. Fallback
        if (template == null) return $"[{key}]";

        // 5. Format
        if (args != null && args.Length > 0)
        {
            try { return string.Format(template, args); }
            catch { return template; } // Fail safe
        }
        return template;
    }
}

    /// <summary>
    /// Core logic manager for the Giveaway Bot.
    /// Handles configuration loading, state management, trigger routing, and command execution.
    /// </summary>
    public class GiveawayManager : IDisposable
    {
        public const string Version = "1.4.2"; // Semantic Versioning       
        
        // ==================== Instance Fields ====================
        
        private ConfigLoader _configLoader;
        public ConfigLoader Loader => _configLoader;
        private UpdateService _updateService;

        // The current active configuration, reloaded periodically
        private static GiveawayBotConfig _globalConfig;
        public static GiveawayBotConfig GlobalConfig
        {
            get => _globalConfig;
            set => _globalConfig = value;
        }
        public static GiveawayBotConfig StaticConfig => _globalConfig;

        // In-memory cache of giveaway states, keyed by profile name (e.g., "Main", "Weekly")
        public ConcurrentDictionary<string, GiveawayState> States { get; private set; }

        // Timer for incremental entry dumping (batch processing)
        private System.Threading.Timer _dumpTimer;
        private int _tickCount = 0; // Optimization for polling frequency
        private CPHAdapter _currentAdapter; // Store for timer callback access
        
        // Cache for Trigger JSON strings to prevent redundant deserialization
        private Dictionary<string, string> _triggersJsonCache = new Dictionary<string, string>();
        
        // General cache for last synced variable values (Metrics + Config) to prevent log spam
        // Key: Global Variable Name, Value: Last synced value
        private Dictionary<string, object> _lastSyncedValues = new Dictionary<string, object>();

        /// <summary>
        /// Helper to set a global variable only if the value has changed.
        /// Dramatically reduces log spam by avoiding redundant SetGlobalVar calls.
        /// </summary>
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

        public GiveawayManager()
        {
            States = new ConcurrentDictionary<string, GiveawayState>();
        }

        /// <summary>
        /// Selects a random message option if the input contains delimiters (| or ,).
        /// Prioritizes Pipe (|) splitting. duplicate commas are treated as text if pipes exist.
        /// </summary>
        public static string PickRandomMessage(string rawMsg)
        {
             if (string.IsNullOrWhiteSpace(rawMsg)) return rawMsg;
             
             // Support pipe OR comma delimiter (Pipe preferred)
             // If pipes exist, we ONLY split on pipes (allowing commas in the messages)
             if (rawMsg.Contains("|"))
             {
                 var options = rawMsg.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                 return options.Length > 0 ? options[new Random().Next(options.Length)].Trim() : rawMsg;
             }
             
             // Fallback: Check for logic-based comma splitting (only if it looks like a list)
             // We want to be careful not to split normal sentences with commas.
             // Current logic: If it contains pipes, we handled it. If not, we check for multiple valid segments.
             // Actually, for consistency with WheelOfNamesClient logic which supported commas:
             // We will maintain the comma fallback but only if no pipes.
             var commaOptions = rawMsg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
             if (commaOptions.Length > 1)
             {
                 return commaOptions[new Random().Next(commaOptions.Length)].Trim();
             }

             return rawMsg;
        }

        // Timer for lifecycle events (timed giveaways)
        private System.Threading.Timer _lifecycleTimer;

        public static int? ParseDuration(string durationStr)
        {
            if (string.IsNullOrWhiteSpace(durationStr)) return null;
            durationStr = durationStr.Trim().ToLowerInvariant();

            int totalSeconds = 0;
            bool matched = false;

            // Regex to match "1h", "30m", "10s", "1d", "1w"
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
                else adapter.LogTrace($"[Batch] Target '{trimmed}' found no matching profile.");
            }
            return results.Distinct().ToList();
        }

        /// <summary>
        /// Disposes managed resources including timer and lock.
        /// Call this method on bot shutdown to prevent resource leaks.
        /// </summary>
        public void Dispose()
        {
            //_msgIdCleanupTimer?.Dispose(); // Removed in refactor
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
        /// Periodic tick to check for timed giveaway expirations.
        /// </summary>
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

                var now = DateTime.Now;
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
                            adapter.LogInfo($"[Lifecycle] Auto-closing giveaway '{profileName}' (Time expired).");
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
                        adapter.LogVerbose($"[Lifecycle] Timer active. Active Profiles: {activeCount}");
                }
            }
            catch (Exception ex)
            {
                adapter.LogVerbose($"[Lifecycle] Tick error: {ex.Message}");
            }
        }



        /// <summary>
        /// Sets up the manager dependencies and loads initial state.
        /// </summary>
        public void Initialize(CPHAdapter adapter)
        {
            // Logger is already attached to Cph via EnsureInitialized, but we ensure it's in our singleton too
            if (Logger == null)
            {
                if (adapter.Logger != null) Logger = adapter.Logger;
                else Logger = new FileLogger();
            }
            if (adapter.Logger == null) adapter.Logger = Logger;
        
    adapter.LogTrace("[GiveawayManager] Starting Initialize...");
    
    // Instantiate update service early
    _updateService = new UpdateService();

            _configLoader = new ConfigLoader();
            _configLoader.GenerateDefaultConfig(adapter); // Ensure config file exists for user to edit
            GlobalConfig = _configLoader.GetConfig(adapter);
            if (GlobalConfig == null)
            {
                adapter.LogFatal("[GiveawayManager] GlobalConfig is null! Bot initialization aborted.");
                return;
            }


            adapter.LogDebug($"[GiveawayManager] Primary configuration loaded. Profiles found: {string.Join(", ", GlobalConfig.Profiles.Keys.ToList() ?? new List<string>())}");

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
#pragma warning disable CS8622
            _lifecycleTimer = new System.Threading.Timer(LifecycleTick, adapter, 1000, 1000);
#pragma warning restore CS8622

            adapter.LogVerbose("[GiveawayManager] Internal services instantiated (Loader, Messenger, WheelClient, Metrics).");

            // Load persisted state for all known profiles to ensure we don't lose data on bot restart
            if (GlobalConfig != null)
            {
                foreach (var profileKey in GlobalConfig.Profiles.Keys)
                {
                    adapter.LogTrace($"[GiveawayManager] Rehydrating state for profile: {profileKey}");
                    var state = PersistenceService.LoadState(adapter, profileKey, GlobalConfig.Globals) ?? new GiveawayState();
                    // Ensure every profile has a unique ID for tracking purposes
                    if (state.CurrentGiveawayId == null)
                    {
                        state.CurrentGiveawayId = Guid.NewGuid().ToString();
                        adapter.LogDebug($"[GiveawayManager] Generated new Giveaway ID for {profileKey}: {state.CurrentGiveawayId}");
                        PersistenceService.SaveState(adapter, profileKey, state, GlobalConfig.Globals, true);
                    }
                    States[profileKey] = state;
                    _lastSyncTimes[profileKey] = DateTime.Now;
                }
            }

            adapter.LogTrace("[GiveawayManager] Syncing variables...");
            adapter.SetGlobalVar("GiveawayBot_LogPruneProbability", GlobalConfig.Globals.LogPruneProbability, true);
            adapter.SetGlobalVar("GiveawayBot_LogMaxFileSizeMB", GlobalConfig.Globals.LogMaxFileSizeMB, true);
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
            
            adapter.LogInfo("[GiveawayManager] Initialization complete. All states rehydrated.");

            adapter.LogDebug($"[GiveawayManager] System Check - Storage Base: {AppDomain.CurrentDomain.BaseDirectory}");
            
            // Auto-Encrypt/Upgrade API Key if needed
            AutoEncryptApiKey(adapter);
            
            // Initialize incremental dump timer (checks every 5 seconds)
            _currentAdapter = adapter; // Store for timer callbacks
#pragma warning disable CS8622 // Nullability warning (C# 7.3 doesn't have nullable reference types)
            _dumpTimer = new System.Threading.Timer(
                ProcessPendingDumpsCallback,
                null,
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(5)
            );
#pragma warning restore CS8622
            adapter.LogDebug("[GiveawayManager] Incremental dump timer started (5s interval)");

            // Fire and forget update check
            _ = CheckForUpdatesStartup(adapter);
        }

        private async Task CheckForUpdatesStartup(CPHAdapter adapter)
        {
            if (_updateService == null) return;
            var (available, version) = await _updateService.CheckForUpdatesAsync(adapter, Version);
            if (available)
            {
                string msg = $"📢 Helper Update v{version} available! Type '!giveaway update' to download.";
                adapter.LogInfo($"[UpdateService] {msg}");
                
                // Broadcast update availability
                Messenger?.SendBroadcast(adapter, msg, "Twitch");
            }
        }

        private async Task<bool> HandleUpdateCommand(CPHAdapter adapter, string platform)
        {
             if (_updateService == null) return true;
             
             adapter.LogInfo("[UpdateService] Checking for updates manually...");
             // Send immediate feedback
             Messenger?.SendBroadcast(adapter, "Checking for updates...", platform);
             
             var (available, version) = await _updateService.CheckForUpdatesAsync(adapter, Version);
             
             if (available)
             {
                 string path = await _updateService.DownloadUpdateAsync(adapter, version);
                 if (!string.IsNullOrEmpty(path))
                 {
                     adapter.ShowToastNotification("Giveaway Bot Update", $"Downloaded v{version} to: {path}");
                     Messenger?.SendBroadcast(adapter, $"✅ Update v{version} downloaded! Check your toast notifications for the file path.", platform);
                 }
                 else
                 {
                     Messenger?.SendBroadcast(adapter, "⚠ Download failed. Check logs.", platform);
                 }
             }
             else
             {
                 Messenger?.SendBroadcast(adapter, "✅ You are running the latest version.", platform);
             }
             return true;
        }

        private void MigrateSecurity(CPHAdapter adapter)
        {
            if (GlobalConfig?.Globals == null) return;
            var g = GlobalConfig.Globals;

            if (string.IsNullOrEmpty(g.EncryptionSalt))
            {
                adapter.LogInfo("[Security] No Encryption Salt found. Generating new portable salt...");
                g.EncryptionSalt = Guid.NewGuid().ToString("N");

                // Save immediately so valid salt is persisted
                try 
                {
                    string json = JsonConvert.SerializeObject(GlobalConfig, Formatting.Indented);
                    _configLoader?.WriteConfigText(adapter, json);
                }
                catch (Exception ex)
                {
                    adapter.LogError($"[Security] Failed to save config during migration: {ex.Message}");
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
                            adapter.LogInfo("[Security] Existing API Key migrated to portable encryption.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Automatically encrypts or upgrades API keys to AES-256-CBC encryption.
        /// Supports backward-compatible auto-conversion from legacy OBF (Base64) format.
        /// </summary>
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
                            adapter.LogInfo("[Security] ✅ Auto-converted legacy OBF key to AES-256.");
                            adapter.ShowToastNotification("Giveaway Bot", "API Key upgraded to AES encryption");
                            return;
                        }
                    }
                    adapter.LogWarn("[Security] ⚠ Failed to auto-convert OBF key. Please re-enter plain text API key.");
                    return;
                }
                
                // Warn about unsupported ENC (DPAPI)
                if (currentKey.StartsWith("ENC:"))
                {
                    adapter.LogWarn("[Security] ⚠ Legacy DPAPI (ENC:) key detected. Cannot decrypt. Please re-enter plain text API key.");
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
        /// Uses EncryptionSalt from GlobalConfig if available (portable), else MachineName (legacy).
        /// </summary>
        public static string EncryptSecret(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return null;
            
            string seed = GlobalConfig?.Globals?.EncryptionSalt;
            if (string.IsNullOrEmpty(seed))
                seed = Environment.MachineName + "GiveawayBot_v2" + Environment.UserName;
            
            return EncryptWithSeed(plainText, seed);
        }

        private static string EncryptWithSeed(string plainText, string seed)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    
                    using (var sha = SHA256.Create())
                    {
                        aes.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(seed));
                    }
                    
                    aes.GenerateIV();
                    
                    using (var encryptor = aes.CreateEncryptor())
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(aes.IV, 0, aes.IV.Length); // Prepend IV
                        
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }
                        
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
            
            // 1. Try Configured Salt (Portable)
            if (!string.IsNullOrEmpty(GlobalConfig?.Globals?.EncryptionSalt))
            {
                string result = TryDecrypt(secret, GlobalConfig.Globals.EncryptionSalt);
                if (result != null) return result;
            }
            
            // 2. Try Legacy Machine Key (Fallback)
            return TryDecrypt(secret, Environment.MachineName + "GiveawayBot_v2" + Environment.UserName);
        }

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
            var threshold = DateTime.Now.AddMinutes(-GlobalConfig.Globals.MessageIdCacheTtlMinutes);
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
                SetGlobalVarIfChanged(adapter, "GiveawayBot_Metrics_" + suffix, value);
            }
            
            SetMetric("CacheSize", _cachedMetrics.MessageIdCacheSize);
            SetMetric("CleanupCount", _cachedMetrics.MessageIdCleanupCount);
            SetMetric("LoopDetected", _cachedMetrics.LoopDetectedCount);
            SetMetric("LoopByMsgId", _cachedMetrics.LoopDetectedByMsgId);
            SetMetric("LoopByToken", _cachedMetrics.LoopDetectedByToken);
            SetMetric("LoopByBotFlag", _cachedMetrics.LoopDetectedByBotFlag);
            SetMetric("ConfigReloads", _cachedMetrics.ConfigReloadCount);
            SetMetric("FileIOErrors", _cachedMetrics.FileIOErrors);
            
            SetMetric("EntriesProcessed", _cachedMetrics.EntriesProcessedCount);
            SetMetric("EntryProcessingTotalMs", _cachedMetrics.TotalEntryProcessingMs);
            // Computed average (avoid division by zero)
            long avgEntryMs = _cachedMetrics.EntriesProcessedCount > 0 
                ? _cachedMetrics.TotalEntryProcessingMs / _cachedMetrics.EntriesProcessedCount 
                : 0;
            SetMetric("EntryProcessingAvgMs", avgEntryMs);
            
            SetMetric("WinnerDrawAttempts", _cachedMetrics.WinnerDrawAttempts);
            SetMetric("WinnerDrawSuccesses", _cachedMetrics.WinnerDrawSuccesses);
            
            SetMetric("WheelApiCalls", _cachedMetrics.WheelApiCalls);
            SetMetric("WheelApiTotalMs", _cachedMetrics.WheelApiTotalMs);
            long avgWheelMs = _cachedMetrics.WheelApiCalls > 0 
                ? _cachedMetrics.WheelApiTotalMs / _cachedMetrics.WheelApiCalls 
                : 0;
            SetMetric("WheelApiAvgMs", avgWheelMs);

            SetMetric("ApiErrors", _cachedMetrics.ApiErrors);
            SetMetric("WheelApiErrors", _cachedMetrics.WheelApiErrors);
            SetMetric("WheelApiInvalidKeys", _cachedMetrics.WheelApiInvalidKeys);
            SetMetric("WheelApiTimeouts", _cachedMetrics.WheelApiTimeouts);
            SetMetric("WheelApiNetworkErrors", _cachedMetrics.WheelApiNetworkErrors);
        }

        private void IncGlobalMetric(CPHAdapter adapter, string n, long d = 1)
        {
            var key = $"GiveawayBot_Metrics_{n}";
            var v = adapter.GetGlobalVar<long>(key, true);
            var newVal = v + d;
            adapter.SetGlobalVar(key, newVal, true);

            if (_cachedMetrics != null)
            {
                _cachedMetrics.GlobalMetrics[n] = newVal;
                _cachedMetrics.LastUpdated = DateTime.Now;
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
                var key = $"GiveawayBot_Metrics_{metricName}";
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
                _processedMsgIds[msgId] = DateTime.Now;
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
        /// <param name="platform">Platform of the trigger.</param>
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
        /// Validates username against configured regex pattern (if enabled).
        /// Returns true if username is INVALID (should be rejected).
        /// Includes 100ms timeout protection against ReDoS (Regular Expression Denial of Service) attacks.
        /// </summary>
        /// <param name="username">Username to validate</param>
        /// <param name="config">Profile configuration containing UsernameRegex</param>
        /// <param name="adapter">CPH adapter for logging</param>
        /// <returns>True if username is INVALID (reject entry), False if valid or validation disabled</returns>
        private bool IsUsernameRegexInvalid(string username, GiveawayProfileConfig config, CPHAdapter adapter)
        {
            if (string.IsNullOrEmpty(config.UsernameRegex))
                return false; // Validation disabled

            try
            {
                // Recompile regex only if pattern changed (performance optimization)
                if (_usernameRegex == null || _lastUsernamePattern != config.UsernameRegex)
                {
                    _usernameRegex = new Regex(config.UsernameRegex, RegexOptions.None, TimeSpan.FromMilliseconds(GlobalConfig.Globals.RegexTimeoutMs));
                    _lastUsernamePattern = config.UsernameRegex;
                    adapter.LogTrace($"[Validation] Compiled new username regex pattern: '{config.UsernameRegex}'");
                }

                if (!_usernameRegex.IsMatch(username))
                {
                    adapter.LogTrace($"[Validation] Username '{username}' rejected: Does not match pattern '{config.UsernameRegex}'");
                    return true; // INVALID - reject
                }

                return false; // Valid - allow
            }
            catch (RegexMatchTimeoutException)
            {
                adapter.LogError($"[Validation] Regex timeout (potential ReDoS): '{config.UsernameRegex}'");
                IncGlobalMetric(adapter, "Validation_RegexTimeouts");
                return false; // Fail-open on timeout (security > strictness)
            }
            catch (ArgumentException ex)
            {
                adapter.LogError($"[Validation] Invalid regex pattern: '{config.UsernameRegex}' - {ex.Message}");
                return false; // Fail-open on invalid regex
            }
        }

        // Cached regex for username pattern validation (recompiled only when pattern changes)
        private static Regex _usernameRegex = null;
        private static string _lastUsernamePattern = null;



        /// <summary>
        /// Increments a user-specific metric counter.
        /// </summary>
        /// <param name="adapter">CPH adapter.</param>
        /// <param name="u">The user's ID.</param>
        /// <param name="userName">The user's display name.</param>
        /// <param name="n">Metric key suffix.</param>
        /// <param name="d">Amount to increment (default 1).</param>
        private void IncUserMetric(CPHAdapter adapter, string u, string userName, string n, long d = 1)
        {
            var v = adapter.GetUserVar<long>(u, $"GiveawayBot_UserMetrics_{n}", true);
            var newVal = v + d;
            adapter.SetUserVar(u, $"GiveawayBot_UserMetrics_{n}", newVal, true);

            if (_cachedMetrics != null)
            {
                if (!_cachedMetrics.UserMetrics.TryGetValue(u, out var userSet))
                {
                    userSet = new UserMetricSet { UserName = userName };
                    _cachedMetrics.UserMetrics[u] = userSet;
                }
                userSet.UserName = userName; // Keep it fresh
                userSet.Metrics[n] = newVal;
                _cachedMetrics.LastUpdated = DateTime.Now;
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
        /// </summary>
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
        public void SyncGlobalVars(CPHAdapter adapter)
        {
            if (GlobalConfig == null) return;
            
            // Only log trace if we haven't synced this batch recently or on first run
            // (Approximated by checking one key var presence in cache)
            if (!_lastSyncedValues.ContainsKey("GiveawayBot_RunMode"))
            {
                adapter.Logger?.LogTrace(adapter, "System", "Syncing Global Configuration Variables...");
            }
            
            // Touch input overrides so they aren't pruned
            adapter.TouchGlobalVar("GiveawayBot_RunMode");
            adapter.TouchGlobalVar("GiveawayBot_ExposeVariables");
            adapter.TouchGlobalVar("GiveawayBot_Profile_Config_Triggers");

            var g = GlobalConfig.Globals;

            // Sync RunMode & LogLevel (Always set to ensure normalization)
            SetGlobalVarIfChanged(adapter, "GiveawayBot_RunMode", g.RunMode ?? "FileSystem", true);
            string level = string.IsNullOrEmpty(g.LogLevel) ? "INFO" : g.LogLevel.ToUpperInvariant();
            SetGlobalVarIfChanged(adapter, "GiveawayBot_LogLevel", level, true);
            string currentLevelVar = level;

            // Expose all GlobalSettings fields
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_LogToStreamerBot", g.LogToStreamerBot, true);
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_LogToStreamerBot", g.LogToStreamerBot, true);
            
            // Legacy Variable: Only sync if it is CUSTOM (i.e. not the default "WheelOfNamesApiKey")
            // This allows users to delete the confusion "GiveawayBot_Globals_WheelApiKeyVar" from SB if they are using the new system.
            if (!string.Equals(g.WheelApiKeyVar, "WheelOfNamesApiKey", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(g.WheelApiKeyVar))
            {
                SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_WheelApiKeyVar", g.WheelApiKeyVar, true);
            }
            
            // Direct API Key Status Check & Initialization
            // Ensure the direct variable exists so users can see it in valid variable lists
            string keyVal = adapter.GetGlobalVar<string>("GiveawayBot_Globals_WheelApiKey");
            if (keyVal == null) 
            {
                adapter.SetGlobalVar("GiveawayBot_Globals_WheelApiKey", "", true);
                keyVal = "";
            }
            string keyStatus = string.IsNullOrEmpty(keyVal) ? "Missing" : "Configured";
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_WheelApiKeyStatus", keyStatus, true);

            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_LogRetentionDays", g.LogRetentionDays, true);
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_LogSizeCapMB", g.LogSizeCapMB, true);
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_FallbackPlatform", g.FallbackPlatform ?? "", true);
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_StatePersistenceMode", g.StatePersistenceMode ?? "Both", true);
            SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_StateSyncIntervalSeconds", g.StateSyncIntervalSeconds, true);

            if (g.EnabledPlatforms != null)
            {
                SetGlobalVarIfChanged(adapter, "GiveawayBot_Globals_EnabledPlatforms", string.Join(",", g.EnabledPlatforms), true);
            }

            // Sync Root Config Metadata
            if (GlobalConfig.Instructions != null)
                SetGlobalVarIfChanged(adapter, "GiveawayBot_Instructions", string.Join("\n", GlobalConfig.Instructions), true);
            if (GlobalConfig.TriggerPrefixHelp != null)
                SetGlobalVarIfChanged(adapter, "GiveawayBot_TriggerPrefixHelp", string.Join("\n", GlobalConfig.TriggerPrefixHelp), true);

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
            UpdateMetric(adapter, "Entries_Total", totalEntries);
            UpdateMetric(adapter, "Winners_Total", totalActiveWinners);

            // 1. Explicitly touch/bootstrap the primary management metrics to ensure they don't get pruned
            // We use UpdateMetric with 0 value to leverage its internal touching logic, assuming it will perform the diff check if we update it
            // However, standard UpdateMetric logic was: get var, add delta. 
            // Actually UpdateMetric impl below: public void UpdateMetric(CPHAdapter adapter, string name, long value) -> Sets the value directly.
            // We should update UpdateMetric to use SetGlobalVarIfChanged too.
            UpdateMetric(adapter, "Entries_Rejected", 0);
            UpdateMetric(adapter, "ApiErrors", 0);
            UpdateMetric(adapter, "SystemErrors", 0);

            // 2. Extra safety for configuration status and json vars
            adapter.TouchGlobalVar("GiveawayBot_ConfigStatus");
            adapter.TouchGlobalVar("GiveawayBot_LastConfigErrors");
            adapter.TouchGlobalVar("GiveawayBot_Config");
            adapter.TouchGlobalVar("GiveawayBot_Config_LastWriteTime");
            adapter.TouchGlobalVar("GiveawayBot_BackupCount");
        }

        private void UpdateMetric(CPHAdapter adapter, string name, long value)
        {
            string key = "GiveawayBot_Metrics_" + name;
            // Use locally cached check instead of GetGlobalVar for better performance and reduced spam
            SetGlobalVarIfChanged(adapter, key, value, true);
            
            // Performance Warning (Latency)
            if (name == "EntryProcessingAvgMs" && value > 2000)
            {
                adapter.LogWarn($"[Performance] High Latency: Entry Processing took {value}ms (>2000ms threshold).");
            }
        }

        /// <summary>
        /// Persists the current in-memory metrics to storage (file/GlobalVar).
        /// </summary>
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
    private void SyncProfileVariables(CPHAdapter adapter, string profileName, GiveawayProfileConfig config, GiveawayState state, GlobalSettings globals)
    {
        // Always sync the full state JSON for persistence/visibility, regardless of ExposeVariables
        // This is potentially large, so we check diff first
        // HIDDEN from UI (persisted=false) - internal state JSON
        SetGlobalVarIfChanged(adapter, string.Format("GiveawayBot_State_{0}", profileName), JsonConvert.SerializeObject(state), false);

        string prefix = $"GiveawayBot_{profileName}_";

        // Check for global override in variables
        bool? overrideVal = ParseBoolVariant(adapter.GetGlobalVar<string>("GiveawayBot_ExposeVariables", true));
        string currentRunMode = ConfigLoader.GetRunMode(adapter); // Use authoritative mode (includes overrides)
        bool isMirror = string.Equals(currentRunMode, "Mirror", StringComparison.OrdinalIgnoreCase);

        if (isMirror || (overrideVal ?? globals.ExposeVariables ?? config.ExposeVariables))
        {
            // Runtime State
            SetGlobalVarIfChanged(adapter, prefix + "IsActive", state.IsActive, true);
            SetGlobalVarIfChanged(adapter, prefix + "EntryCount", state.Entries.Count, true);
            var totalTickets = state.Entries.Values.Sum(e => e.TicketCount);
            SetGlobalVarIfChanged(adapter, prefix + "TicketCount", totalTickets, true);
            // HIDDEN from UI (persisted=false) - internal implementation detail
            SetGlobalVarIfChanged(adapter, prefix + "GiveawayId", state.CurrentGiveawayId ?? "", false);
            SetGlobalVarIfChanged(adapter, prefix + "WinnerName", state.LastWinnerName ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "WinnerUserId", state.LastWinnerUserId ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "WinnerCount", state.WinnerCount, true);
            SetGlobalVarIfChanged(adapter, prefix + "CumulativeEntries", state.CumulativeEntries, true);
            // Calculate Sub Count - O(N) but generally fast enough
            int subCount = state.Entries.Values.Count(e => e.IsSub);
            SetGlobalVarIfChanged(adapter, prefix + "SubEntryCount", subCount, true);
            
            // Dynamic Config Exposure
            SetGlobalVarIfChanged(adapter, prefix + "TimerDuration", config.TimerDuration ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "MaxEntriesPerMinute", config.MaxEntriesPerMinute, true);
            SetGlobalVarIfChanged(adapter, prefix + "RequireSubscriber", config.RequireSubscriber, true);
            SetGlobalVarIfChanged(adapter, prefix + "SubLuckMultiplier", config.SubLuckMultiplier, true);
            
            // Wheel & OBS Settings
            SetGlobalVarIfChanged(adapter, prefix + "EnableWheel", config.EnableWheel, true);
            SetGlobalVarIfChanged(adapter, prefix + "EnableObs", config.EnableObs, true);
            SetGlobalVarIfChanged(adapter, prefix + "ObsScene", config.ObsScene ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "ObsSource", config.ObsSource ?? "", true);
            
            // Wheel of Names Configuration
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_Title", config.WheelSettings.Title ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_Description", config.WheelSettings.Description ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_SpinTime", config.WheelSettings.SpinTime, true);
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_AutoRemoveWinner", config.WheelSettings.AutoRemoveWinner, true);
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_ShareMode", config.WheelSettings.ShareMode ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "Wheel_WinnerMessage", config.WheelSettings.WinnerMessage ?? "", true);
            
            // Dump/Export Settings
            SetGlobalVarIfChanged(adapter, prefix + "DumpFormat", config.DumpFormat.ToString(), true);
            SetGlobalVarIfChanged(adapter, prefix + "DumpEntriesOnEnd", config.DumpEntriesOnEnd, true);
            SetGlobalVarIfChanged(adapter, prefix + "DumpEntriesOnEntry", config.DumpEntriesOnEntry, true);
            SetGlobalVarIfChanged(adapter, prefix + "DumpEntriesOnEntryThrottleSeconds", config.DumpEntriesOnEntryThrottleSeconds, true);
            SetGlobalVarIfChanged(adapter, prefix + "DumpWinnersOnDraw", config.DumpWinnersOnDraw, true);
            
            // Entry Validation & Bot Detection
            // Entry Validation & Bot Detection
            SetGlobalVarIfChanged(adapter, prefix + "UsernameRegex", config.UsernameRegex ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "MinAccountAgeDays", config.MinAccountAgeDays, true);
            SetGlobalVarIfChanged(adapter, prefix + "EnableEntropyCheck", config.EnableEntropyCheck, true);
            SetGlobalVarIfChanged(adapter, prefix + "WinChance", config.WinChance, true);
            SetGlobalVarIfChanged(adapter, prefix + "GameFilter", config.GameFilter ?? "", true);
            SetGlobalVarIfChanged(adapter, prefix + "RedemptionCooldownMinutes", config.RedemptionCooldownMinutes, true);


            // Log trace only if we are actually syncing something relevant (based on IsActive state presence)
            if (!_lastSyncedValues.ContainsKey(prefix + "IsActive"))
            {
                adapter.Logger?.LogTrace(adapter, profileName, $"Syncing {profileName} config variables...");
                }
            }

            adapter.Logger?.LogTrace(adapter, profileName, $"Sync Complete: {profileName} (Entries: {state.Entries.Count}, Active: {state.IsActive}, SubLuck: {config.SubLuckMultiplier})");
        }

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
        /// Supports: "GW2", "Guild Wars 2"
        /// </summary>
        /// <param name="config">Profile configuration to modify</param>
        private static void ApplyGameFilter(GiveawayProfileConfig config)
        {
            if (string.IsNullOrEmpty(config.GameFilter)) return;
            
            string game = config.GameFilter.Trim();
            if (game.Equals("GW2", StringComparison.OrdinalIgnoreCase) || 
                game.Equals("Guild Wars 2", StringComparison.OrdinalIgnoreCase))
            {
                config.UsernameRegex = @"^[a-zA-Z]+\.\d{4}$";
                config.EnableEntropyCheck = false;
            }
            // Add more game filters here as needed
        }


        /// <summary>
        /// Determines if the triggering user is the broadcaster/owner.
        /// Checks userId match or Role level 4 (Broadcaster).
        /// </summary>
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
                        // Usage: !giveaway profile start <ProfileName|*|all>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile start <ProfileName|*|all>", platform);
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
                                await HandleStart(adapter, pConfig, pState, name, platform);
                            }
                        }
                        return true;
                    }

                case "end":
                case "close":
                    {
                        // Usage: !giveaway profile end <ProfileName|*|all>
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
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 1)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile create <ProfileName>", platform);
                            return true;
                        }

                        string profileName = parts[0];
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

                case "delete":
                    {
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2 || !parts[1].Equals("confirm", StringComparison.OrdinalIgnoreCase))
                        {
                            string profileName = parts.Length > 0 ? parts[0] : "<name>";
                            Messenger?.SendBroadcast(adapter, $"⚠ To delete '{profileName}', add 'confirm': !giveaway profile delete {profileName} confirm", platform);
                            return true;
                        }

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
                        if (GlobalConfig?.Profiles == null || GlobalConfig.Profiles.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, "No profiles configured", platform);
                            return true;
                        }

                        string profileList = string.Join(", ", GlobalConfig.Profiles.Keys);
                        Messenger?.SendBroadcast(adapter, $"Profiles ({GlobalConfig.Profiles.Count}): {profileList}", platform);
                        break;
                    }
                case "config":
                    {
                        // Format: !giveaway profile config <ProfileName|*|all> <Key>=<Value> or <Key> <Value>
                        string[] parts = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 2)
                        {
                            Messenger?.SendBroadcast(adapter, "Usage: !giveaway profile config <Target> <Key>=<Value> or <Key> <Value>", platform);
                            return true;
                        }

                        string target = parts[0];
                        string key, val;

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

                        var profiles = ParseProfileTargets(adapter, target);
                        if (profiles.Count == 0)
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ No profiles matched '{target}'", platform);
                            return true;
                        }

                        int successCount = 0;
                        string lastError = "";

                        foreach (var profileName in profiles)
                        {
                            var (updated, updateError) = await _configLoader.UpdateProfileConfigAsync(adapter, profileName, key, val);
                            if (updated) successCount++;
                            else lastError = updateError;
                        }

                        if (successCount > 0)
                        {
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
                            Messenger?.SendBroadcast(adapter, $"✅ Exported '{parts[0]}' to: {path}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Export failed: {msg}", platform);
                        }
                        break;
                    }

                case "import":
                case "imp":
                    {
                        // Usage: !giveaway profile import <Source> [TargetName]
                        // Source can be a file path (absolute or relative to Import folder) or a raw JSON blob (if simple enough to carry in chat, unlikely but supported)
                        // If TargetName is omitted, it attempts to infer or uses Source filename.
                        
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
                            GlobalConfig = _configLoader.GetConfig(adapter);
                            if (GlobalConfig?.Globals != null) GlobalConfig.Globals.RunMode = ConfigLoader.GetRunMode(adapter);
                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, $"✅ Import successful: {msg}", platform);
                        }
                        else
                        {
                            Messenger?.SendBroadcast(adapter, $"⚠ Import failed: {msg}", platform);
                        }
                        break;
                    }

                case "trigger":
                    {
                        // Format: !giveaway profile trigger <ProfileName> add <type:value> <Action>
                        // Format: !giveaway profile trigger <ProfileName> remove <type:value>
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
        /// </summary>
        private static string GetWheelApiKey(CPHAdapter adapter)
        {
            // 1. Try Direct Variable (New Standard)
            string directKey = adapter.GetGlobalVar<string>("GiveawayBot_Globals_WheelApiKey");
            if (!string.IsNullOrEmpty(directKey)) return directKey;

            // 2. No Fallback (Strict Mode per User Request)
            return null;
        }
            


        /// <summary>
        /// Periodic check for configuration updates from Streamer.bot Global Variables.
        /// This allows users to update Triggers via JSON blobs which then sync back to disk.
        /// </summary>
        private async Task CheckForConfigUpdates(CPHAdapter adapter, bool fullSync = true)
        {
            if (GlobalConfig?.Profiles == null) return;

            // Dynamic Global Settings Check (API Key Status)
            if (fullSync && GlobalConfig.Globals != null)
            {
                 // Check status: Direct > Indirect
                 string directKey = adapter.GetGlobalVar<string>("GiveawayBot_Globals_WheelApiKey");
                 
                 // Auto-Encryption Logic: If key is plain text (not empty, not ENC:), validate and encrypt it.
                 if (!string.IsNullOrEmpty(directKey) && !directKey.StartsWith("ENC:"))
                 {
                     // Validate first to ensure we don't encrypt garbage
                     bool? isValid = await new WheelOfNamesClient().ValidateApiKey(adapter, directKey);
                     if (isValid == true)
                     {
                         string encrypted = GiveawayManager.EncryptSecret(directKey);
                         if (!string.IsNullOrEmpty(encrypted))
                         {
                             adapter.SetGlobalVar("GiveawayBot_Globals_WheelApiKey", encrypted, true);
                             adapter.LogInfo("🔒 API Key validated and encrypted successfully.");
                             directKey = encrypted; // Update local var for status check
                         }
                     }
                 }

                 string indirectKey = null;
                 
                 if (string.IsNullOrEmpty(directKey))
                 {
                     // Fallback check
                     string keyName = GlobalConfig.Globals.WheelApiKeyVar ?? "WheelOfNamesApiKey";
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

                 string currentStatus = adapter.GetGlobalVar<string>("GiveawayBot_Globals_WheelApiKeyStatus");
                 
                 if (!string.Equals(currentStatus, expectedStatus, StringComparison.Ordinal))
                 {
                     adapter.SetGlobalVar("GiveawayBot_Globals_WheelApiKeyStatus", expectedStatus, true);
                 }
            }

            bool dirty = false;
            foreach (var kvp in GlobalConfig.Profiles)
            {
                var name = kvp.Key;
                var profile = kvp.Value;
                try
                {
                    if (fullSync)
                    {
                        // Check triggers variable for 2-way sync
                    string varName = $"GiveawayBot_Profile_Config_Triggers_{name}";
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
                            adapter.SetGlobalVar("GiveawayBot_LastConfigErrors", err, true);
                        }
                    }
                
                    // Check Messages variable for 2-way sync
                    string msgVarName = $"GiveawayBot_Profile_Config_Messages_{name}";
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
                    string timerVarName = $"GiveawayBot_{name}_TimerDuration";
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
                        adapter.LogInfo($"[Config] TimerDuration for '{name}' updated from '{oldVal}' to '{timerVal}' via Global Variable.");

                        // Dynamic Runtime Adjustment
                        if (States.TryGetValue(name, out var state) && state.IsActive && state.StartTime.HasValue)
                        {
                            int? newDurationSec = ParseDuration(timerVal);
                            if (newDurationSec.HasValue)
                            {
                                // Treat new duration as "time remaining from now" (not total time from start)
                                var newEndTime = DateTime.Now.AddSeconds(newDurationSec.Value);
                                state.AutoCloseTime = newEndTime;
                                
                                var remaining = newEndTime - DateTime.Now;
                                string timeStr = $"{(int)remaining.TotalMinutes}m {(int)remaining.Seconds}s";

                                string msg = Loc.Get("TimerUpdated", name, timeStr);
                                // Broadcast update to chat
                                Messenger?.SendBroadcast(adapter, msg, GlobalConfig.Globals.FallbackPlatform);
                                adapter.LogInfo($"[Timer] Updated runtime auto-close to {newEndTime} (Ends in {timeStr})");
                            }
                            else
                            {
                                // Timer removed/invalid - Switch to manual
                                state.AutoCloseTime = null;
                                adapter.LogInfo($"[Timer] Runtime timer disabled (Manual close only).");
                            }
                        }
                    }

                    if (fullSync)
                    {
                    // --- Phase 2: Dynamic Variable Updates ---

                    // 1. MaxEntriesPerMinute (Validation: >= 0)
                    string maxEntriesVarName = $"GiveawayBot_{name}_MaxEntriesPerMinute";
                    string maxEntriesVal = adapter.GetGlobalVar<string>(maxEntriesVarName, true);
                    if (int.TryParse(maxEntriesVal, out int newMaxEntries) && newMaxEntries >= 0)
                    {
                        if (newMaxEntries != profile.MaxEntriesPerMinute)
                        {
                            adapter.LogInfo($"[Config] MaxEntriesPerMinute for '{name}' updated: {profile.MaxEntriesPerMinute} -> {newMaxEntries}");
                            profile.MaxEntriesPerMinute = newMaxEntries;
                            dirty = true;
                        }
                    }

                    // 2. RequireSubscriber (Validation: bool)
                    string reqSubVarName = $"GiveawayBot_{name}_RequireSubscriber";
                    string reqSubVal = adapter.GetGlobalVar<string>(reqSubVarName, true);
                    bool? newReqSub = ParseBoolVariant(reqSubVal);
                    if (newReqSub.HasValue && newReqSub.Value != profile.RequireSubscriber)
                    {
                        adapter.LogInfo($"[Config] RequireSubscriber for '{name}' updated: {profile.RequireSubscriber} -> {newReqSub.Value}");
                        profile.RequireSubscriber = newReqSub.Value;
                        dirty = true;
                    }

                    // 3. SubLuckMultiplier (Validation: >= 1.0)
                    string subLuckVarName = $"GiveawayBot_{name}_SubLuckMultiplier";
                    string subLuckVal = adapter.GetGlobalVar<string>(subLuckVarName, true);
                    if (decimal.TryParse(subLuckVal, out decimal newSubLuck) && newSubLuck >= 1.0m)
                    {
                        if (newSubLuck != profile.SubLuckMultiplier)
                        {
                            adapter.LogInfo($"[Config] SubLuckMultiplier for '{name}' updated: {profile.SubLuckMultiplier} -> {newSubLuck}");
                            profile.SubLuckMultiplier = newSubLuck;
                            dirty = true;
                        }
                    }

                    // --- Full Variable Ingestion (Mirror Mode) ---
                    // Helper Actions for clean sync code (C# 7.3)
                    void syncStr(string vSuffix, Action<string> setter)
                    {
                        string val = adapter.GetGlobalVar<string>($"GiveawayBot_{name}_{vSuffix}", true);
                        if (val != null) setter(val);
                    }
                    void syncBool(string vSuffix, Action<bool> setter)
                    {
                         bool? val = ParseBoolVariant(adapter.GetGlobalVar<string>($"GiveawayBot_{name}_{vSuffix}", true));
                         if (val.HasValue) setter(val.Value);
                    }
                    void syncInt(string vSuffix, Action<int> setter)
                    {
                        if (int.TryParse(adapter.GetGlobalVar<string>($"GiveawayBot_{name}_{vSuffix}", true), out int val) && val >= 0) setter(val);
                    }

                    // 4. Wheel Configuration
                    syncBool("EnableWheel", v => { if(profile.EnableWheel != v) { profile.EnableWheel = v; dirty = true; adapter.LogInfo($"[Config] EnableWheel updated for '{name}'."); } });
                    
                    if (profile.WheelSettings == null) profile.WheelSettings = new WheelConfig();
                    syncStr("WheelSettings_Title", v => { if(profile.WheelSettings.Title != v) { profile.WheelSettings.Title = v; dirty = true; } });
                    syncStr("WheelSettings_Description", v => { if(profile.WheelSettings.Description != v) { profile.WheelSettings.Description = v; dirty = true; } });
                    syncStr("WheelSettings_WinnerMessage", v => { if(profile.WheelSettings.WinnerMessage != v) { profile.WheelSettings.WinnerMessage = v; dirty = true; } });
                    syncInt("WheelSettings_SpinTime", v => { if(v > 0 && profile.WheelSettings.SpinTime != v) { profile.WheelSettings.SpinTime = v; dirty = true; } });
                    syncBool("WheelSettings_AutoRemoveWinner", v => { if(profile.WheelSettings.AutoRemoveWinner != v) { profile.WheelSettings.AutoRemoveWinner = v; dirty = true; } });
                    syncStr("WheelSettings_ShareMode", v => { if(profile.WheelSettings.ShareMode != v) { profile.WheelSettings.ShareMode = v; dirty = true; } });

                    // 5. OBS Configuration
                    syncBool("EnableObs", v => { if(profile.EnableObs != v) { profile.EnableObs = v; dirty = true; adapter.LogInfo($"[Config] EnableObs updated for '{name}'."); } });
                    syncStr("ObsScene", v => { if(profile.ObsScene != v) { profile.ObsScene = v; dirty = true; } });
                    syncStr("ObsSource", v => { if(profile.ObsSource != v) { profile.ObsSource = v; dirty = true; } });

                    // 6. Validation & Filters
                    syncStr("UsernameRegex", v => { if(profile.UsernameRegex != v) { profile.UsernameRegex = v; dirty = true; } });
                    syncStr("GameFilter", v => { if(profile.GameFilter != v) { profile.GameFilter = v; dirty = true; } });
                    syncInt("MinAccountAgeDays", v => { if(profile.MinAccountAgeDays != v) { profile.MinAccountAgeDays = v; dirty = true; } });
                    syncInt("MinAccountAgeDays", v => { if(profile.MinAccountAgeDays != v) { profile.MinAccountAgeDays = v; dirty = true; } });
                    // Enhanced RedemptionCooldown Parsing
                    syncStr("RedemptionCooldownMinutes", v => { 
                        int mins = ParseDurationMinutes(v);
                        if (profile.RedemptionCooldownMinutes != mins) { 
                            profile.RedemptionCooldownMinutes = mins; 
                            dirty = true; 
                        } 
                    });
                    syncBool("EnableEntropyCheck", v => { if(profile.EnableEntropyCheck != v) { profile.EnableEntropyCheck = v; dirty = true; } });
                    
                    // WinChance (Double handling)
                    string winChanceVar = $"GiveawayBot_{name}_WinChance";
                    string winChanceVal = adapter.GetGlobalVar<string>(winChanceVar, true);
                    if (double.TryParse(winChanceVal, out double newChance) && newChance >= 0 && newChance <= 1.0)
                    {
                        if (Math.Abs(newChance - profile.WinChance) > 0.0001) { profile.WinChance = newChance; dirty = true; }
                    }

                    // 7. Dump Config
                    string dumpFmtVar = $"GiveawayBot_{name}_DumpFormat";
                    string dumpFmtVal = adapter.GetGlobalVar<string>(dumpFmtVar, true);
                    if (!string.IsNullOrEmpty(dumpFmtVal) && Enum.TryParse(dumpFmtVal, true, out DumpFormat newFmt))
                    {
                        if (profile.DumpFormat != newFmt) { profile.DumpFormat = newFmt; dirty = true; }
                    }
                    syncBool("DumpEntriesOnEnd", v => { if(profile.DumpEntriesOnEnd != v) { profile.DumpEntriesOnEnd = v; dirty = true; } });
                    syncBool("DumpEntriesOnEntry", v => { if(profile.DumpEntriesOnEntry != v) { profile.DumpEntriesOnEntry = v; dirty = true; } });
                    syncBool("DumpWinnersOnDraw", v => { if(profile.DumpWinnersOnDraw != v) { profile.DumpWinnersOnDraw = v; dirty = true; } });
                    syncInt("DumpEntriesOnEntryThrottleSeconds", v => { if(profile.DumpEntriesOnEntryThrottleSeconds != v) { profile.DumpEntriesOnEntryThrottleSeconds = v; dirty = true; } });

                    }
                }
                catch (Exception ex)
                {
                    adapter.LogTrace($"[Sync] Error checking updates for {name}: {ex.Message}");
                }

                if (fullSync)
                {
                // Check Individual Message Variables (Granular 2-Way Sync)
                // Checks GiveawayBot_<Profile>_Msg_<Key>
                foreach (var key in Loc.Keys)
                {
                    string varName = $"GiveawayBot_{name}_Msg_{key}";
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
                            adapter.LogInfo($"[Config] Detected external update to Message '{key}' for profile '{name}'.");
                        }
                    }
                }

                    } // End fullSync block

                // Dynamic IsActive Check (Remote Start/Stop)
                if (States.TryGetValue(name, out var profileState))
                {
                    string activeVarName = $"GiveawayBot_{name}_IsActive";
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
                // Note: ProcessTrigger will pick up the new config in the very next block via GetConfig()
            }
            else if (fullSync)
            {
                // Verify polling is working without spamming unless tracing
                adapter.LogTrace($"[Sync] Config check complete. No changes detected across {GlobalConfig.Profiles.Count} profiles.");
            }
        }

        /// <summary>
        /// Processes a trigger event (Chat, Command, etc.).
        /// </summary>
        /// <param name="adapter">CPH adapter containing the event context.</param>
        /// <returns>True if processed successfully, False otherwise.</returns>
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

                // ANTI-LOOP PROTECTION (Consolidated)
                // Implements 3-layer protection: Message ID Dedup, Bot Token, and isBot flag
                if (IsLoopDetected(adapter, out var loopReason))
                {
                    adapter.LogTrace($"[AntiLoop] Ignoring trigger: {loopReason}");
                    return true; // Loop detected - exit early
                }

                // Check if this is an allowed external bot message that needs processing
                if (await HandleBotMessage(adapter))
                {
                    return true; // Handled by bot listener logic
                }



                // Check for updates from Global Variables (2-Way Sync)
                await CheckForConfigUpdates(adapter);

                // Refresh config if it has changed on disk
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
                // Analyze the arguments to determine which profile and action map to this event
                (profileName, action, platform, sourceDetails) = TriggerInspector.IdentifyTrigger(adapter, GlobalConfig?.Profiles);

                adapter.LogTrace($"[ProcessTrigger] IdentifyTrigger Result: Profile={profileName ?? "null"}, Action={action ?? "null"}, Platform={platform ?? "null"}");

                // Auto-generate config if a system command is used and config is missing
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
                    adapter.TryGetArg<string>("rawInput", out var rawInput);
                    adapter.LogTrace($"[Trigger] Global Command Check: '{rawInput}'");
                    if (rawInput != null)
                    {
                        // Profile management commands
                        if (CheckProfileCmd(rawInput) || CheckProfileCmd(sourceDetails))
                        {
                            if (!IsBroadcaster(adapter))
                            {
                                adapter.TryGetArg<string>("user", out var user);
                                adapter.LogWarn($"[Security] Unauthorized profile management attempt by {user}: {rawInput}");
                                return true;
                            }

                            return await HandleProfileCommand(adapter, rawInput, platform);
                        }
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
                                
                                // Silent denial for security - prevents information disclosure
                                // Optional toast notification if enabled in ANY profile
                                foreach (var profile in GlobalConfig.Profiles.Values)
                                {
                                    if (profile.ToastNotifications != null && 
                                        profile.ToastNotifications.TryGetValue("UnauthorizedAccess", out var notify) && notify)
                                    {
                                        adapter.ShowToastNotification("Giveaway Bot - Security", 
                                            $"Unauthorized command attempt by {user}");
                                        break; // Only show once
                                    }
                                }
                                return true;
                            }
                        }

                        if (CheckCmd(rawInput, "config gen") || CheckCmd(sourceDetails, "config gen"))
                        {
                            adapter.LogTrace("[Trigger] Matched: config gen");
                            _configLoader.GenerateDefaultConfig(adapter);
                            GlobalConfig = _configLoader.GetConfig(adapter); // Refresh local cache immediately

                            SyncAllVariables(adapter);
                            Messenger?.SendBroadcast(adapter, "Config generated!", platform ?? "Twitch");
                        }
                        if (CheckCmd(rawInput, "config check") || CheckCmd(sourceDetails, "config check"))
                        {
                            adapter.LogTrace("[Trigger] Matched: config check");
                            var report = _configLoader.ValidateConfig(adapter);
                            adapter.LogInfo($"[Config] Logic Check: {report}");

                            // Refresh the local cache so SyncAllVariables uses the NEW values
                            GlobalConfig = _configLoader.GetConfig(adapter);

                            SyncAllVariables(adapter);

                            Messenger?.SendBroadcast(adapter, "Report: " + report, platform ?? "Twitch");
                        }
                        if (CheckCmd(rawInput, "system test") || CheckCmd(sourceDetails, "system test"))
                        {
                            adapter.LogTrace("[Trigger] Matched: system test");
                            await PerformSystemCheck(adapter);
                        }
                        if (CheckCmd(rawInput, "regex test") || CheckCmd(sourceDetails, "regex test"))
                        {
                            adapter.LogTrace("[Trigger] Matched: regex test");
                            await HandleRegexTest(adapter, rawInput, platform);
                        }
                        if (CheckCmd(rawInput, "create") || CheckCmd(sourceDetails, "create"))
                        {
                            adapter.LogTrace("[Trigger] Matched: create");
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
                        if (CheckCmd(rawInput, "delete") || CheckCmd(sourceDetails, "delete"))
                        {
                            adapter.LogTrace("[Trigger] Matched: delete");
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
                                Messenger?.SendBroadcast(adapter, $"⚠ Delete failed: {deleteError}", platform ?? "Twitch");
                            }
                        }
                        if (CheckCmd(rawInput, "stats") || CheckCmd(sourceDetails, "stats"))
                        {
                            return await HandleStatsCommand(adapter, rawInput, platform ?? "Twitch");
                        }
                        if (CheckCmd(rawInput, "update") || CheckCmd(sourceDetails, "update"))
                        {
                            return await HandleUpdateCommand(adapter, platform ?? "Twitch");
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
                switch (action.ToLower())
                {
                    case "entry":
                    case "enter": return await HandleEntry(adapter, profileConfig, profileState, profileName);
                    case "draw":
                    case "winner": return await HandleDraw(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                    case "start":
                    case "open": return await HandleStart(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                    case "end":
                    case "close": return await HandleEnd(adapter, profileConfig, profileState, profileName, platform ?? "Twitch");
                    default:
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


        private static bool CheckProfileCmd(string s) => s != null && (s.Contains("!giveaway profile") || s.Contains("!ga profile") || s.Contains("!giveaway p") || s.Contains("!ga p"));
        private static bool CheckMgmt(string s) => s != null && (CheckCmd(s, "config") || CheckCmd(s, "system") || CheckCmd(s, "create") || CheckCmd(s, "delete"));
        private static bool CheckCmd(string s, string sub) => s != null && (s.Contains("!giveaway " + sub) || s.Contains("!ga " + sub));

        /// <summary>
        /// Handles a user entering the giveaway. Checks for spam, active status, and duplicates.
        /// Returns true to indicate the trigger was processed.
        /// </summary>
        /// <summary>
        /// Handles a user entering the giveaway. Checks for spam, active status, and duplicates.
        /// Returns true to indicate the trigger was processed.
        /// Allows explicit userId/userName injection for external calls (e.g. from generic bot events).
        /// </summary>
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

            // Apply game filter FIRST (modifies config.UsernamePattern and config.EnableEntropyCheck)
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
                        IncGlobalMetric(adapter, "Entries_Rejected");
                        IncGlobalMetric(adapter, "Entries_Rejected_Cooldown");
                        return true;
                    }
                }
                // Update cooldown timestamp after passing the check
                state.RedemptionCooldowns[userId] = DateTime.Now;
            }

            // Global rate limit check (prevents bot spam from overwhelming the system)
            if (state.IsSpamming(config.MaxEntriesPerMinute, GlobalConfig.Globals.SpamWindowSeconds))
            {
                adapter.LogTrace($"[HandleEntry] RATE LIMIT: {userName} rejected for {profileName} (Limit: {config.MaxEntriesPerMinute}/min).");
                IncGlobalMetric(adapter, "Entries_RateLimited");
                return true;
            }

            // Username Pattern Check
            // Validates the username against a streamer-defined regex pattern (if configured)
            if (IsUsernameRegexInvalid(userName, config, adapter))
            {
                // Rejection logged within IsUsernameRegexInvalid
                IncGlobalMetric(adapter, "Entries_Rejected");
                return true; 
            }


            // Entropy Check (Keyboard Smashing Detection)
            // Detects low-quality names like "asdfgh", "zzzzzz", etc.
            if (config.EnableEntropyCheck)
            {
                if (!EntryValidator.HasSufficientEntropy(userName, _configLoader.GetConfig(adapter).Globals.MinUsernameEntropy))
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Low entropy/suspicious name (User: {userName})");
                    IncGlobalMetric(adapter, "Entries_Rejected");
                    if (config.ToastNotifications.TryGetValue("EntryRejected", out var notify) && notify)
                        adapter.ShowToastNotification("Giveaway Bot", $"Entry Rejected: {userName} (Username Pattern)");
                    return true;
                }
            }

            // Account Age Check
            // Rejects entries from accounts that are too new (potential bots/alts)
            if (config.MinAccountAgeDays > 0)
            {
                // Try to get account creation date from Streamer.bot (if available)
                if (adapter.TryGetArg<DateTime>("createdAt", out var createdAt))
                {
                    var accountAgeDays = (DateTime.Now - createdAt).TotalDays;
                    if (accountAgeDays < config.MinAccountAgeDays)
                    {
                        adapter.LogTrace($"[{profileName}] Entry rejected: Account too new ({accountAgeDays:F1} days < {config.MinAccountAgeDays} required) (User: {userName})");
                        IncGlobalMetric(adapter, "Entries_Rejected");
                        if (config.ToastNotifications.TryGetValue("EntryRejected", out var notify) && notify)
                            adapter.ShowToastNotification("Giveaway Bot", $"Entry Rejected: {userName} (Account Too New)");
                        return true;
                    }
                    adapter.LogTrace($"[{profileName}] Account age check passed: {userName} ({accountAgeDays:F1} days old)");
                }
                else
                {
                    // Account creation date not available - log and allow entry
                    adapter.LogTrace($"[{profileName}] Account age validation skipped: 'createdAt' argument not available from Streamer.bot (User: {userName})");
                }
            }

            // Lock critical section to ensure thread-safe entry addition
            await _lock.WaitAsync();
            try
            {
                // Validate giveaway is open and user hasn't already entered
                if (!state.IsActive)
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Giveaway not active (User: {userName})");
                    IncGlobalMetric(adapter, "Entries_Rejected");
                    return true;
                }
                if (state.Entries.ContainsKey(userId))
                {
                    adapter.LogTrace($"[{profileName}] Entry rejected: Duplicate (User: {userName})");
                    IncGlobalMetric(adapter, "Entries_Rejected");
                    return true;
                }

                adapter.TryGetArg<bool>("isSubscribed", out var isSub);
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
                IncGlobalMetric(adapter, "Entries_Total");
                adapter.LogInfo($"[{profileName}] Accepted: {userName} (Tickets: {tickets})");

                IncUserMetric(adapter, userId, userName, "EntriesTotal");

                // Extract platform from trigger args (fallback: Twitch)
                string platform = "Twitch";
                if (adapter.TryGetArg<string>("platform", out var detectedPlatform) && !string.IsNullOrEmpty(detectedPlatform))
                {
                    platform = detectedPlatform;
                }
                
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
                         adapter.ShowToastNotification("Giveaway Bot", $"New Entry: {userName}");
                }

                return true;
            }
            catch (Exception ex)
            {
                adapter.Logger?.LogError(adapter, profileName, "HandleEntry Failed", ex);
                IncGlobalMetric(adapter, "SystemErrors");
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
        /// Handles drawing a winner. Supports local RNG or WheelOfNames API.
        /// Returns true to indicate the trigger was processed.
        /// </summary>
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

                // Flatten entries into a ticket pool (ticket count = duplication count)
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
                    IncUserMetric(adapter, winnerEntry.UserId, winnerEntry.UserName ?? "Unknown", "WinsTotal");
                    state.LastWinnerName = winnerEntry.UserName;
                    state.LastWinnerUserId = winnerEntry.UserId;
                    state.WinnerCount++;
                    IncGlobalMetric(adapter, "Winners_Total");
                    
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
                         string msg = config.WheelSettings?.WinnerMessage?.Replace("{name}", winnerEntry.UserName) ?? $"Winner: {winnerEntry.UserName}!";
                         Messenger?.SendBroadcast(adapter, msg, platform);
                    }
                }

                // Winner message handled by EventBus subscribers
                if (config.DumpWinnersOnDraw) await DumpWinnersAsync(adapter, profileName, pool, config);
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
        /// </summary>
        private async Task<bool> HandleStart(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string platform)
        {
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
            state.StartTime = DateTime.Now; // Track start time for dynamic calcs

            // Handle Timed Giveaway
            var duration = ParseDuration(config.TimerDuration);
            if (duration.HasValue)
            {
                state.AutoCloseTime = DateTime.Now.AddSeconds(duration.Value);
                adapter.LogInfo($"[{profileName}] Timed giveaway started. Will auto-close in {duration.Value}s.");
            }


            IncGlobalMetric(adapter, "Giveaway_Started");

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
                        adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{profileName}' is OPEN!");
                }

                return true;
            }
            finally { _lock.Release(); }
        }


        /// <summary>
        /// Closes the giveaway and optionally dumps the final entry list.
        /// Returns true to indicate the trigger was processed.
        /// </summary>
        private async Task<bool> HandleEnd(CPHAdapter adapter, GiveawayProfileConfig config, GiveawayState state, string profileName, string platform, bool bypassAuth = false)
        {
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
            

            IncGlobalMetric(adapter, "Giveaway_Ended");

            PersistenceService.SaveState(adapter, profileName, state, GlobalConfig.Globals);
            SyncProfileVariables(adapter, profileName, config, state, GlobalConfig.Globals);

            string closeMsg = Loc.Get("GiveawayClosed", profileName);
            Messenger?.SendBroadcast(adapter, closeMsg, platform);
            
            if (Bus != null) Bus.Publish(new GiveawayEndedEvent(adapter, profileName, state, platform));
                else
                {
                    if (config.ToastNotifications != null && config.ToastNotifications.TryGetValue("GiveawayClosed", out var notify) && notify)
                        adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{profileName}' is CLOSED!");
                }

                return true;
            }
            finally { _lock.Release(); }
        }

        /// <summary>
        /// Asynchronously dumps winner pool to a timestamped text file.
        /// Non-blocking I/O operation for better performance during draws.
        /// </summary>
        /// <param name="adapter">CPH adapter for safe logging</param>
        /// <param name="profileName">Profile name for directory organization</param>
        /// <param name="pool">List of winner usernames to dump</param>
        /// <param name="config">Profile configuration for dump format</param>
        private static async Task DumpWinnersAsync(CPHAdapter adapter, string profileName, List<string> pool, GiveawayProfileConfig config)
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
                        await writer.WriteLineAsync("Username").ConfigureAwait(false);
                        foreach (var winner in pool.Distinct())
                        {
                            // Escape commas if needed (simple implementation)
                            var escaped = winner.Contains(",") ? $"\"{winner}\"" : winner;
                            await writer.WriteLineAsync(escaped).ConfigureAwait(false);
                        }
                    }
                    else // TXT
                    {
                        foreach (var winner in pool.Distinct())
                        {
                            await writer.WriteLineAsync(winner).ConfigureAwait(false);
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
        /// Asynchronously dumps full entry details to a timestamped text file.
        /// Non-blocking I/O operation for better performance during giveaway close.
        /// </summary>
        /// <param name="profileName">Profile name for directory organization</param>
        /// <param name="state">Giveaway state containing entries to dump</param>
        /// <param name="config">Profile configuration for dump format</param>
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
                        await writer.WriteLineAsync("UserId,Username,IsSub,TicketCount,EntryTime").ConfigureAwait(false);
                        foreach (var entry in state.Entries.Values)
                        {
                            var safeUser = entry.UserName.Replace("\"", "\"\"");
                            if (safeUser.Contains(",")) safeUser = $"\"{safeUser}\"";
                            
                            var line = $"{entry.UserId},{safeUser},{entry.IsSub},{entry.TicketCount},{entry.EntryTime:O}";
                            await writer.WriteLineAsync(line).ConfigureAwait(false);
                        }
                    }
                    else // TXT
                    {
                        foreach (var entry in state.Entries.Values)
                        {
                            var line = $"{entry.UserName} ({entry.UserId}) - Tickets: {entry.TicketCount}";
                            await writer.WriteLineAsync(line).ConfigureAwait(false);
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
                var testKey = "GiveawayBot_HealthTest";
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
/// Removes user from active states, persisted logs, and clears user variables.
/// usage: !giveaway data delete <user>
/// </summary>
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

        // 1. Clean Active States (Memory + JSON persistence)
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

        // 2. Clean User Variables (Global Vars) & Metrics
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
                adapter.UnsetGlobalVar($"GiveawayBot_User_{uid}");
                adapter.LogInfo($"[GDPR] Cleaned global metrics/vars for UserID: {uid}");
                entriesRemoved++; // Count metric removal as a "record" removed
            }
        }

        // 3. Clean Historical Logs (Dumps)
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

        // 4. Clean JSON State Files (for profiles not in config anymore?)
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

private static bool CheckDataCmd(string s) => s != null && (s.Contains("!giveaway data") || s.Contains("!ga data") || s.Contains("!giveaway d") || s.Contains("!ga d"));


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

        public void Deconstruct(out string profile, out string action, out string platform, out string details)
        {
            profile = Profile;
            action = Action;
            platform = Platform;
            details = Details;
        }
    }

    /// <summary>
    /// Routes incoming Streamer.bot triggers (Commands, StreamDeck, Raw Input) to specific giveaway profiles.
    /// Returns the profile name and the abstract action (Entry, Draw, etc.) to perform.
    /// </summary>
    public class TriggerInspector
    {
        public TriggerInspector() { }
        
        private static readonly char[] SplitColon = new char[] { ':' };
        
        public static TriggerResult IdentifyTrigger(CPHAdapter adapter, Dictionary<string, GiveawayProfileConfig> profiles)
        {
            adapter.TryGetArg<string>("userType", out var userType);
            string platform = userType ?? "Twitch"; // Default to Twitch if unknown
            if (platform.Equals("youtube", StringComparison.OrdinalIgnoreCase)) platform = "YouTube";
            if (platform.Equals("kick", StringComparison.OrdinalIgnoreCase)) platform = "Kick";
            if (platform.Equals("twitch", StringComparison.OrdinalIgnoreCase)) platform = "Twitch";
            adapter.LogTrace($"[Trigger] Platform detected: {platform} (Raw: {userType})");

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
                            // Be careful: Ensure it starts with the command and is followed by space or end of string
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

        private readonly HashSet<string> _touchedGlobalVars = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public CPHAdapter(dynamic cph)
        {
            _cph = cph;
            _t = _cph.GetType();
            // If in strict mode (test/editor), verify arg access early
            try { _args = GetMethod("GetArgs", 0)?.Invoke(_cph, null) as Dictionary<string, object>; } catch { }
            if (_args == null) _args = new Dictionary<string, object>();
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
                 // Use dynamic dispatch to handle optional parameters (e.g. icon path) which vary by SB version
                 ((dynamic)_cph).ShowToastNotification(title, message);
             }
             catch (Exception ex)
             {
                 try { _t.GetMethod("LogDebug", new Type[] { typeof(string) })?.Invoke(_cph, new object[] { "[CPH] Toast failed: " + ex.Message }); } catch { }
             }
#endif
        }

        private Dictionary<string, object> _args;
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
        /// </summary>
        public static bool IsManagedVariable(string name) { return name != null && name.StartsWith("GiveawayBot_", StringComparison.OrdinalIgnoreCase); }

        /// <summary>
        /// Marks a global variable as "touched" (active) to prevent pruning.
        /// </summary>
        public void TouchGlobalVar(string name) { if (IsManagedVariable(name)) _touchedGlobalVars.Add(name); }

        // Helper to find methods safely even with overloads
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

        public void LogInfo(string m) { Logger?.LogInfo(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogInfo", new object[] { m }, 1); }
        public void LogWarn(string m) { Logger?.LogWarn(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogWarn", new object[] { m }, 1); }
        public void LogDebug(string m) { Logger?.LogDebug(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
        public void LogError(string m) { Logger?.LogError(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogError", new object[] { m }, 1); }
        public void LogVerbose(string m) { Logger?.LogVerbose(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
        public void LogTrace(string m) { Logger?.LogTrace(this, "CPH", m); if (GiveawayManager.StaticConfig?.Globals?.LogToStreamerBot ?? true) InvokeSafe("LogDebug", new object[] { m }, 1); }
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
                string[] argStrings;
                if (args != null)
                {
                    argStrings = new string[args.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        argStrings[i] = args[i]?.ToString() ?? "null";
                    }
                }
                else
                {
                    argStrings = new string[0];
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
        /// Retrieves the value of a global variable.
        /// </summary>
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
                if (isTrace) Logger?.LogTrace(this, "Reflect", $"GetGlobalVar<{typeof(T).Name}>('{n}', {p}) => {val}");
                
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
                    Logger?.LogTrace(this, "Reflect", $"GetGlobalVar<{typeof(T).Name}>('{n}', {p}) => {val} (Fallback)");
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
        public void SetGlobalVar(string n, object v, bool p = true)
        {
            InvokeSafe("SetGlobalVar", new object[] { n, v, p }, 3);
            TouchGlobalVar(n);
        }

        /// <summary>
        /// Retrieves a list of all current global variable names.
        /// </summary>
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
        public void UnsetGlobalVar(string n, bool p = true) { InvokeSafe("UnsetGlobalVar", new object[] { n, p }, 2); }

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

        public void SetUserVar(string u, string n, object v, bool p = true)
        {
            InvokeSafe("SetUserVar", new object[] { u, n, v, p }, 4);
        }

        /// <summary>
        /// Sends a chat message to the default platform (usually Twitch).
        /// Automatically appends the anti-loop token.
        /// </summary>
        public void SendMessage(string m, bool b = true)
        {
            m = GiveawayManager.PickRandomMessage(m);
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            if (GetMethod("SendMessage", 3) != null) { InvokeSafe("SendMessage", new object[] { msg, b, true }, 3); return; }
            if (GetMethod("SendMessage", 2) != null) { InvokeSafe("SendMessage", new object[] { msg, b }, 2); return; }
            InvokeSafe("SendMessage", new object[] { msg }, 1);
        }

        /// <summary>
        /// Sends a chat message to YouTube.
        /// Automatically appends the anti-loop token.
        /// </summary>
        public void SendYouTubeMessage(string m) 
        { 
            m = GiveawayManager.PickRandomMessage(m);
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            InvokeSafe("SendYouTubeMessage", new object[] { msg }, 1); 
        }
        
        /// <summary>
        /// Sends a chat message to Kick.
        /// Automatically appends the anti-loop token.
        /// </summary>
        public void SendKickMessage(string m) 
        {
            m = GiveawayManager.PickRandomMessage(m);
            string msg = m.EndsWith(GiveawayManager.ANTI_LOOP_TOKEN) ? m : m + GiveawayManager.ANTI_LOOP_TOKEN;
            InvokeSafe("SendKickMessage", new object[] { msg }, 1); 
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
            message = GiveawayManager.PickRandomMessage(message);
            InvokeSafe("TwitchReplyToMessage", new object[] { message, replyId, useBot, fallback }, 4);
        }


        /// <summary>
        /// Checks if the connected Twitch broadcaster account is currently live.
        /// </summary>
        public bool IsTwitchLive()
        {
            // Only check broadcaster status - bot streaming status is not relevant for giveaways
            object res = InvokeSafe("IsTwitchBroadcasterLive", new object[0], 0);
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
            object res = InvokeSafe("IsYouTubeBroadcasterLive", new object[0], 0);
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
            object res = InvokeSafe("IsKickBroadcasterLive", new object[0], 0);
            bool isLive = res != null && (bool)res;
            if (res == null) LogTrace("[Platform] IsKickBroadcasterLive method not found or returned null");
            return isLive;
        }

        /// <summary>
        /// Sets the URL of an OBS Browser Source.
        /// </summary>
        public void ObsSetBrowserSource(string s, string o, string u)
        {
            if (GetMethod("ObsSetBrowserSource", 4) != null) { InvokeSafe("ObsSetBrowserSource", new object[] { s, o, u, 0 }, 4); return; }
            InvokeSafe("ObsSetBrowserSource", new object[] { s, o, u }, 3);
        }

        public object GetEventType() { return InvokeSafe("GetEventType", new object[0], 0); }

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
        /// Retrieves the current configuration storage mode from Global Variables.
        /// Return values: "FileSystem", "GlobalVar", "ReadOnlyVar", "Mirror".
        /// </summary>
        public static string GetRunMode(CPHAdapter adapter)
        {
            var mode = adapter.GetGlobalVar<string>("GiveawayBot_RunMode", true);
            if (string.IsNullOrEmpty(mode)) return "FileSystem";
            // Normalization
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
                if (mode == "GlobalVar" || mode == "ReadOnlyVar" || mode == "Mirror")
                {
                    var json = adapter.GetGlobalVar<string>("GiveawayBot_Config", true);
                    if (string.IsNullOrEmpty(json) && mode != "Mirror")
                        adapter.LogWarn($"[Config] RunMode is {mode} but 'GiveawayBot_Config' global variable is empty!");

                    // In Mirror mode, if GlobalVar is empty, we MUST fall back to file
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
                    // Prevent "GiveawayBot_Config" from cluttering the persistent variables list in ANY mode (per user request).
                    // This variable is still available in memory for GlobalVar mode, but won't be saved to DB.
                    bool persist = false; 
                    adapter.SetGlobalVar("GiveawayBot_Config", json, persist);
                    
                    // Always persist the timestamp so we know when to reload
                    adapter.SetGlobalVar("GiveawayBot_Config_LastWriteTime", DateTime.Now.ToString("o"), true);
                    
                    if (mode == "GlobalVar") return;
                }

                if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
                File.WriteAllText(_path, json);
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
                
                if (mode == "GlobalVar" || mode == "Mirror")
                {
                    adapter.LogDebug($"[Config] Saving configuration to 'GiveawayBot_Config' global variable (Mode: {mode}).");
                    // Per user request: Do not persist the JSON blob to DB to avoid hitting limits/clutter.
                    adapter.SetGlobalVar("GiveawayBot_Config", json, false);
                    adapter.SetGlobalVar("GiveawayBot_Config_LastWriteTime", DateTime.Now.ToString("o"), true);
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
            // 1. Bot Config Profiles
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

            // 2. Profile Triggers
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

            // 1. Profile Names & Reserved Keywords
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

            // 2. Clamp Values (Globals)
            if (c.Globals != null) 
            {
               if (c.Globals.LogPruneProbability < 0) c.Globals.LogPruneProbability = 0;
               if (c.Globals.LogPruneProbability > 100) c.Globals.LogPruneProbability = 100;

               if (c.Globals.LogRetentionDays < 1) c.Globals.LogRetentionDays = 1;
               if (c.Globals.MessageIdCacheTtlMinutes < 1) c.Globals.MessageIdCacheTtlMinutes = 1;
            }

            // 3. Clamp Values (Profiles)
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

                // 1. Check for Disk Changes (Priority for FileSystem/Mirror)
                if ((mode == "FileSystem" || mode == "Mirror") && File.Exists(_path))
                {
                    var lastWrite = File.GetLastWriteTime(_path);
                    if (lastWrite > _lastLoad)
                    {
                        // Check if Global is even NEWER in Mirror mode
                        bool globalIsNewer = false;
                        if (mode == "Mirror")
                        {
                            var globalTimeStr = adapter.GetGlobalVar<string>("GiveawayBot_Config_LastWriteTime", true);
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

                // 2. Check for GlobalVar Changes (Mirror Mode Only)
                if (mode == "Mirror" && !forceFileReload)
                {
                    // Optimization: Check timestamp FIRST to avoid fetching huge JSON string if not needed
                    var globalTimeStr = adapter.GetGlobalVar<string>("GiveawayBot_Config_LastWriteTime", true);
                    bool globalTimestampChanged = false;
                    
                    if (DateTime.TryParse(globalTimeStr, out var globalTime))
                    {
                        if (globalTime > _lastLoad) 
                        {
                            globalTimestampChanged = true;
                            // Also check if it's newer than disk (Emergency conflict resolution)
                             if (File.Exists(_path))
                             {
                                 var lastWrite = File.GetLastWriteTime(_path);
                                 if (globalTime > lastWrite)
                                 {
                                     adapter.LogTrace($"[Config] Mirror: GlobalVar timestamp ({globalTime}) > Disk ({lastWrite}). Forcing Global Reload.");
                                     forceGlobalReload = true;
                                 }
                             }
                        }
                    }

                    // Only fetch the full JSON body if we suspect a change or we have no timestamp tracking
                    // (Or if we are just doing a sanity check and it's been a while? No, trust the timestamp if present)
                    if (globalTimestampChanged || string.IsNullOrEmpty(globalTimeStr))
                    {
                         preloadedGlobalJson = adapter.GetGlobalVar<string>("GiveawayBot_Config", true);
                         if (!string.IsNullOrEmpty(preloadedGlobalJson) && preloadedGlobalJson != _lastLoadedJson)
                         {
                             adapter.LogTrace("[Config] Mirror: Global Variable content changed. Syncing.");
                             forceGlobalReload = true;
                         }
                    }
                }

                // 3. Perform Reload if needed
                // Check configured interval (default 5 seconds)
                int reloadInterval = GiveawayManager.GlobalConfig?.Globals.ConfigReloadIntervalSeconds ?? 5;
                
                // FIX: Only use time-based reload for GlobalVar mode (polling). 
                // filesystem/Mirror modes have explicit change detection above (Timestamp/FileWatcher logic).
                bool timeExpired = (DateTime.Now - _lastLoad).TotalSeconds > reloadInterval;
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
                            adapter.SetGlobalVar("GiveawayBot_Config", json, true);
                            try
                            {
                                adapter.SetGlobalVar("GiveawayBot_Config_LastWriteTime", File.GetLastWriteTime(_path).ToString("o"), true);
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
                            try 
                            { 
                                File.WriteAllText(_path, json); 
                                // Update last load to match file time so we don't reload immediately
                                _lastLoad = DateTime.Now; 
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
                                    var currentMode = adapter.GetGlobalVar<string>("GiveawayBot_RunMode", true);
                                    if (string.IsNullOrEmpty(currentMode))
                                    {
                                        adapter.LogInfo($"[Config] Bootstrapping RunMode from file: '{c.Globals.RunMode}'");
                                        adapter.SetGlobalVar("GiveawayBot_RunMode", c.Globals.RunMode, true);
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
                                adapter.LogDebug($"[Config] Deserialized OK. Profiles Found: {string.Join(", ", _cached.Profiles.Keys)}");
                                SetStatus(adapter, "Valid ✅");
                                _lastError = "";
                                adapter.SetGlobalVar("GiveawayBot_LastConfigErrors", null, true); // Clear any previous error state
                                RunFuzzyCheck(adapter, _cached);
                            }
                        }
                    }
                    else if (mode == "Mirror" && string.IsNullOrEmpty(json) && File.Exists(_path))
                    {
                        // Emergency Restore: If GlobalVar is empty but file exists, sync it back
                        adapter.LogWarn("[Config] Mirror: Global Variable is empty! Restoring from local file.");
                        json = File.ReadAllText(_path);
                        adapter.SetGlobalVar("GiveawayBot_Config", json, true);
                        _lastLoadedJson = json;
                    }
                    _lastLoad = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                SetStatus(adapter, $"❌ Error: {ex.Message}");
                if (_lastError != ex.Message)
                {
                    _lastError = ex.Message;
                    adapter.LogError($"[Config] Sync Failed: {ex.Message}");
                }
            }
            return _cached;
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

                string fileName = $"{profileName}_Export_{DateTime.Now:yyyyMMdd_HHmm}.json";
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

                // 1. Resolve Source (File vs JSON)
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

                // 2. Deserialize & Validate
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
                
                // 3. Save to Global Config
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
                if (mode == "GlobalVar" || mode == "Mirror") adapter.SetGlobalVar("GiveawayBot_Config", newJson, true);
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


        private static void SetStatus(CPHAdapter adapter, string s) => adapter.SetGlobalVar("GiveawayBot_ConfigStatus", s, true);

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
                var lastWrite = File.GetLastWriteTime(_path);
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

                adapter.SetGlobalVar("GiveawayBot_LastConfigErrors", "", true); // Clear errors
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
        private static string HandleValidationError(CPHAdapter adapter, string msg)
        {
            SetStatus(adapter, msg);
            adapter.SetGlobalVar("GiveawayBot_LastConfigErrors", msg, true);
            return msg;
        }

        /// <summary>
        /// Generates a default configuration file if none exists, or migrates an existing one.
        /// Keeps user profiles intact during migration.
        /// </summary>
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
        public async Task<(bool Success, string ErrorMessage, string BackupPath)> DeleteProfileAsync(CPHAdapter adapter, string name)
        {
            string backupPath = null;
            
            if (string.IsNullOrWhiteSpace(name)) return (false, "Profile name cannot be empty", "");
            if (name.Equals("Main", StringComparison.OrdinalIgnoreCase)) return (false, "Cannot join the void. 'Main' profile is eternal.", "");

            var config = GetConfig(adapter);
            if (!config.Profiles.ContainsKey(name)) return (false, $"Profile '{name}' not found", "");

            // Perform backup before delete using synchronous operations inside async wrapper effectively (or standard file IO)
            // We kept file IO sync for profile specific backups for simplicity, but we can make them async if needed.
            // For now, focusing on the main BackupConfig and WriteConfigText being async.
            try
            {
                string backupDir = Path.Combine(_dir, "backups", $"deleted_{name}_{DateTime.Now:yyyyMMdd_HHmm}");
                if (!Directory.Exists(backupDir)) Directory.CreateDirectory(backupDir);

                var profile = config.Profiles[name];

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
                    $"GiveawayBot_{name}_IsActive",
                    $"GiveawayBot_{name}_EntryCount",
                    $"GiveawayBot_{name}_TicketCount",
                    $"GiveawayBot_{name}_Id",
                    $"GiveawayBot_{name}_WinnerName",
                    $"GiveawayBot_{name}_WinnerUserId"
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
                adapter.LogInfo($"[Config] Profile deletion backup created: {backupPath}");
            }
            catch (Exception ex)
            {
                adapter.LogWarn($"[Config] Backup creation failed (proceeding anyway): {ex.Message}");
                backupPath = $"(backup failed: {ex.Message})";
            }

            // Perform deletion
            await BackupConfigAsync(adapter); // Also create standard config backup
            config.Profiles.Remove(name);
            await WriteConfigTextAsync(adapter, JsonConvert.SerializeObject(config, Formatting.Indented));
            InvalidateCache();

            //  Unset all Streamer.bot variables for this profile
            adapter.SetGlobalVar($"GiveawayBot_{name}_IsActive", (string)null, true);
            adapter.SetGlobalVar($"GiveawayBot_{name}_EntryCount", (string)null, true);
            adapter.SetGlobalVar($"GiveawayBot_{name}_TicketCount", (string)null, true);
            adapter.SetGlobalVar($"GiveawayBot_{name}_Id", (string)null, true);
            adapter.SetGlobalVar($"GiveawayBot_{name}_WinnerName", (string)null, true);
            adapter.SetGlobalVar($"GiveawayBot_{name}_WinnerUserId", (string)null, true);

            adapter.Logger?.LogInfo(adapter, "Config", $"Profile deleted: {name}");
            return (true, "", backupPath);
        }

        /// <summary>
        /// Clones an existing profile to a new profile asynchronously.
        /// Copies all settings and triggers but resets runtime state.
        /// </summary>
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
        /// Handles type conversion and validation for keys like MaxEntriesPerMinute, EnableWheel, etc.
        /// </summary>
        /// <summary>
        /// Updates a specific configuration key for a profile asynchronously.
        /// Uses Reflection to support all properties of GiveawayProfileConfig dynamically.
        /// </summary>
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
                // 1. Handle WheelSettings.* (Nested Object)
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

                // 2. Handle Top-Level Properties via Reflection
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
                int maxBackups = adapter.GetGlobalVar<int>("GiveawayBot_BackupCount", true);
                // Use default from global settings if not configured
                if (maxBackups <= 0) maxBackups = 10; // Default fallback if config load fails

                // Async zip creation: 4096 buffer, 'true' for useAsync
                using (var zipStream = new FileStream(zipPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, true))
                using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    string entryName = $"giveaway_config_{DateTime.Now:yyyyMMdd_HHmmss}.json";
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
        public static int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return string.IsNullOrEmpty(t) ? 0 : t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;
            int n = s.Length, m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        /// <summary>
        /// Performs a fuzzy check on the configuration object to detect potential typos in keys.
        /// Logs warnings and suggestions to Streamer.bot global variables.
        /// </summary>
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
                    adapter.LogWarn($"[Config] {path}: Unrecognized key '{key}'. Did you mean '{best}'?");
                    adapter.SetGlobalVar("GiveawayBot_LastConfigErrors", $"❌ {path}: Typo '{key}'? Hint: '{best}'", true);
                }
                else
                {
                    adapter.LogTrace($"[Config] {path}: Ignored unknown key '{key}'.");
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
        private static string GetStatePath(string p) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "state", $"{p}.json");

        /// <summary>
        /// Saves the giveaway state to disk and/or global variables based on persistence mode.
        /// Serializes the state object to JSON.
        /// </summary>
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
                adapter.SetGlobalVar($"GiveawayBot_State_{p}", json, true);
                if (critical) adapter.LogTrace($"[Persistence] Critical state saved to GlobalVar for {p}");
            }
            else if (!saveToVar && mode != "Both")
            {
                // Clean up global var if we are in File mode and not migrating
                adapter.SetGlobalVar($"GiveawayBot_State_{p}", (string)null, true);
            }
        }

        /// <summary>
        /// Loads the giveaway state from disk or global variables.
        /// Prioritizes Global Variables in 'Mirror' mode if available.
        /// </summary>
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
                json = adapter.GetGlobalVar<string>($"GiveawayBot_State_{p}", true);
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
        [ThreadStatic] private static bool _isLogging;

        public FileLogger(string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
                _base = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "logs");
            else
                _base = basePath;
        }

        /// <summary>Logs an INFO message.</summary>
        public void LogInfo(CPHAdapter adapter, string c, string m) => Log(adapter, "INFO", c, m);
        /// <summary>Logs a WARNING message.</summary>
        public void LogWarn(CPHAdapter adapter, string c, string m) => Log(adapter, "WARN", c, m);
        /// <summary>Logs an ERROR message with optional exception details.</summary>
        public void LogError(CPHAdapter adapter, string c, string m, Exception e = null) => Log(adapter, "ERROR", c, $"{m} {e?.Message}");
        /// <summary>Logs a DEBUG message.</summary>
        public void LogDebug(CPHAdapter adapter, string c, string m) => Log(adapter, "DEBUG", c, m);
        /// <summary>Logs a TRACE message.</summary>
        public void LogTrace(CPHAdapter adapter, string c, string m) => Log(adapter, "TRACE", c, m);
        /// <summary>Logs a VERBOSE message.</summary>
        public void LogVerbose(CPHAdapter adapter, string c, string m) => Log(adapter, "VERBOSE", c, m);
        /// <summary>Logs a FATAL message.</summary>
        public void LogFatal(CPHAdapter adapter, string c, string m) => Log(adapter, "FATAL", c, m);

        public static void ResetLoggingFlag() => _isLogging = false;

        private void Log(CPHAdapter adapter, string level, string category, string message)
        {
            if (_isLogging) return;
            _isLogging = true;
            try
            {
                // Auto-Prune check: runs roughly once every LOG_PRUNE_CHECK_PROBABILITY log calls to minimize I/O overhead
                int probability = adapter.GetGlobalVar<int>("GiveawayBot_LogPruneProbability", true);
                if (probability <= 0) probability = 100;
                if (new Random().Next(probability) == 0) PruneLogs(adapter);

                // Check Global Log Level (Defaults to INFO if not set)
                string currentLevelConfig = adapter.GetGlobalVar<string>("GiveawayBot_LogLevel", true);
                if (string.IsNullOrEmpty(currentLevelConfig)) currentLevelConfig = "INFO";

                if (!IsLogLevelEnabled(currentLevelConfig, level)) return;

                string d = Path.Combine(_base, "General");
                if (!Directory.Exists(d)) Directory.CreateDirectory(d);
                
                var now = DateTime.Now;
                string todayLog = Path.Combine(d, $"{now:yyyy-MM-dd}.log");
                
                // Rotation Check
                CheckForRotation(adapter, todayLog);

                string line = $"[{now:yyyy-MM-dd hh:mm:ss tt}] [{level,-5}] [{category}] {message}" + Environment.NewLine;
                File.AppendAllText(todayLog, line);
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
        private void PruneLogs(CPHAdapter adapter)
        {
            try
            {
                if (!Directory.Exists(_base)) return;

                int retentionDays = adapter.GetGlobalVar<int>("GiveawayBot_LogRetentionDays", true);
                if (retentionDays <= 0) retentionDays = 90; // Default

                long sizeCapMB = adapter.GetGlobalVar<long>("GiveawayBot_LogSizeCapMB", true);
                if (sizeCapMB <= 0) sizeCapMB = 100; // Default

                var cutoff = DateTime.Now.AddDays(-retentionDays);
                var files = Directory.GetFiles(_base, "*.log", SearchOption.AllDirectories)
                                     .Select(f => new FileInfo(f))
                                     .OrderBy(f => f.LastWriteTime)
                                     .ToList();

                // 1. Retention-based pruning
                foreach (var f in files.Where(f => f.LastWriteTime < cutoff))
                {
                    f.Delete();
                }

                // 2. Size-based pruning
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
        private void CheckForRotation(CPHAdapter adapter, string path)
        {
            try
            {
                if (!File.Exists(path)) return;

                int maxMb = adapter.GetGlobalVar<int>("GiveawayBot_LogMaxFileSizeMB", true);
                if (maxMb <= 0) maxMb = 10; // Default 10MB

                var fi = new FileInfo(path);
                if (fi.Length > (maxMb * 1024 * 1024))
                {
                    // Rotate
                    string timestamp = DateTime.Now.ToString("HH-mm-ss");
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

    /// <summary>
    /// Root configuration object. Mirrors the structure of giveaway_config.json.
    /// Contains global settings and a collection of profiles.
    /// </summary>
    public class GiveawayBotConfig
    {
        [JsonProperty("_instructions")]
        public string[] Instructions { get; set; } = new string[]
        {
            "WELCOME TO YOUR GIVEAWAY BOT!",
            "--------------------------------------------------------------------------------",
            "HOW TO CONFIGURE:",
            "1. PROFILES: Create different setups like 'Main', 'Monthly', or 'SubOnly'.",
            "2. TRIGGERS: Connect real events to actions. Format is 'Type:Name'.",
            "3. WHEEL (If Enabled): Set 'GiveawayBot_Globals_WheelApiKey' in Globals.",
            "4. OBS (If Enabled): Set 'ObsScene' and 'ObsSource' in Profile.",
            "5. EXAMPLE: 'command:!join' means 'For the Command named !join'.",
            "6. TROUBLESHOOTING: Run chat command '!giveaway config check' if you get any errors.",
            "--------------------------------------------------------------------------------"
        };

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
            "VARIABLE SYSTEM (GiveawayBot_{Profile}_{Var}):",
            "• [Live Stats]      : IsActive, EntryCount, TicketCount, Id, WinnerName, WinnerCount, SubEntryCount."
        };


        public Dictionary<string, GiveawayProfileConfig> Profiles { get; set; } = new Dictionary<string, GiveawayProfileConfig>(StringComparer.OrdinalIgnoreCase);
        public GlobalSettings Globals { get; set; } = new GlobalSettings();
        public GiveawayBotConfig()
        {
            Profiles["Main"] = new GiveawayProfileConfig();
        }
        [JsonExtensionData] public Dictionary<string, object> ExtensionData { get; set; }
    }
    /// <summary>
    /// Global application settings affecting all profiles (logging, API keys, platform sync).
    /// </summary>
    public class GlobalSettings
    {
        [JsonProperty("_warning")] public string Warning { get; set; } = "DO NOT share your API Keys with others!";
        public bool LogToStreamerBot { get; set; } = true;
        
        [JsonProperty("_expose_vars_help")] public string ExposeVarsHelp { get; set; } = "Expose all profile variables to Streamer.bot global variables (GiveawayBot_{Profile}_{Var}). Default: Defer to Profile.";
        public bool? ExposeVariables { get; set; } = null;

        // Legacy: This holds the name of the variable, but new flow uses 'GiveawayBot_Globals_WheelApiKey' directly.
        public string WheelApiKeyVar { get; set; } = "WheelOfNamesApiKey";
        
        [JsonProperty("_encryption_salt_help")] public string EncryptionSaltHelp { get; set; } = "Randomly generated salt for portable encryption. DO NOT CHANGE MANUALLY.";
        public string EncryptionSalt { get; set; }

        [JsonProperty("_log_retention_help")] public string LogRetentionHelp { get; set; } = "How many days of historical logs to keep (default: 90).";
        public int LogRetentionDays { get; set; } = 90;

        [JsonProperty("_log_size_cap_help")] public string LogSizeCapHelp { get; set; } = "Total disk size limit for the logs directory in MB (default: 100). Oldest logs are pruned first if exceeded.";
        public int LogSizeCapMB { get; set; } = 100;

        [JsonProperty("_log_file_size_limit_help")] public string LogMaxFileSizeHelp { get; set; } = "Max PDF size for a single log file in MB (default: 10). Rotates to new file if exceeded.";
        public int LogMaxFileSizeMB { get; set; } = 10;

        [JsonProperty("_run_mode_help")] public string RunModeHelp { get; set; } = "RunMode: FileSystem, GlobalVar, ReadOnlyVar.";
        public string RunMode { get; set; } = "Mirror";

        [JsonProperty("_log_level_help")] public string LogLevelHelp { get; set; } = "LogLevel: TRACE, VERBOSE, DEBUG, INFO, WARN, ERROR, FATAL.";
        public string LogLevel { get; set; } = "INFO";

        [JsonProperty("_fallback_platform_help")] public string FallbackPlatformHelp { get; set; } = "Fallback: Twitch, YouTube, Kick (if no platforms detected live).";
        public string FallbackPlatform { get; set; } = "Twitch";

        [JsonProperty("_enabled_platforms_help")] public string EnabledPlatformsHelp { get; set; } = "The list of platforms to monitor for live status and broadcast to.";
        public List<string> EnabledPlatforms { get; set; } = new List<string> { "Twitch", "YouTube", "Kick" };

        [JsonProperty("_language_help")] public string LanguageHelp { get; set; } = "Language code for localization (e.g., en-US).";
        public string Language { get; set; } = "en-US";

        [JsonProperty("_custom_strings_help")] public string CustomStringsHelp { get; set; } = "Override specific localized strings here. Key:Value pairs.";
        public Dictionary<string, string> CustomStrings { get; set; } = new Dictionary<string, string>();

        [JsonProperty("_import_globals_help")] public string ImportGlobalsHelp { get; set; } = "Dictionary of Global Variables to import/set in Streamer.bot if missing (e.g. API Keys).";
        public Dictionary<string, string> ImportGlobals { get; set; } = new Dictionary<string, string>();

        [JsonProperty("_persistence_mode_help")] public string PersistenceModeHelp { get; set; } = "StatePersistenceMode: Where to store active giveaway data (File, GlobalVar, Both).";
        public string StatePersistenceMode { get; set; } = "Both";

        [JsonProperty("_sync_interval_help")] public string SyncIntervalHelp { get; set; } = "StateSyncIntervalSeconds: How often to flush entries to persistent storage (Default: 30).";
        public int StateSyncIntervalSeconds { get; set; } = 30;

        [JsonProperty("_advanced_settings_help")] public string AdvancedSettingsHelp { get; set; } 
            = "Advanced: ConfigReloadInterval (s), CacheTTL (m), CleanupInterval (ms), Entropy (2.5), PruneProb (1/N), BackupCount.";
        
        public int ConfigReloadIntervalSeconds { get; set; } = 5;
        public int MessageIdCacheTtlMinutes { get; set; } = 5;
        public int MessageIdCleanupIntervalMs { get; set; } = 60000;
        public int ApiKeyValidationCacheMinutes { get; set; } = 30;
        public double MinUsernameEntropy { get; set; } = 2.5;
        public int LogPruneProbability { get; set; } = 100;
        public int ConfigBackupCount { get; set; } = 10;
        
        public int RegexTimeoutMs { get; set; } = 100;
        public int HttpClientTimeoutSeconds { get; set; } = 30;
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

        public Dictionary<string, string> Triggers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public GiveawayProfileConfig()
        {
            Triggers.Add("command:!enter", "Enter");
            Triggers.Add("command:!draw", "Winner");
            Triggers.Add("command:!start", "Open");
            Triggers.Add("command:!end", "Close");
            WheelSettings = new WheelConfig();
        }

        // Helper field for JSON documentation, ignored by logic but kept to suppress "unknown key" warnings
        [JsonProperty("_expose_variables_help")]
        public string ExposeVariablesHelp { get; set; } = "Set to true to expose these variables to Streamer.bot global vars.";

        [JsonProperty("_entries_help")] public string EntriesHelp { get; set; } = "Max entries allowed per minute. Prevents bot spam.";
        public int MaxEntriesPerMinute { get; set; } = 45;
        public bool EnableWheel { get; set; } = false;
        public bool EnableObs { get; set; } = false;
        public string ObsScene { get; set; } = "Giveaway";
        public string ObsSource { get; set; } = "WheelSource";
        
        public bool ExposeVariables { get; set; } = false;

        [JsonProperty("_dump_format_help")] public string DumpFormatHelp { get; set; } = "Format for dump files: TXT, CSV, JSON.";
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DumpFormat DumpFormat { get; set; } = DumpFormat.TXT;
        
        public WheelConfig WheelSettings { get; set; } = new WheelConfig();
        public bool DumpEntriesOnEnd { get; set; } = true;
        
        [JsonProperty("_dump_entries_on_entry_help")] public string DumpEntriesOnEntryHelp { get; set; } = "Write entries to txt file as accepted (real-time, throttled). DumpEntriesOnEntryThrottleSeconds controls batch frequency.";
        public bool DumpEntriesOnEntry { get; set; } = false;
        public int DumpEntriesOnEntryThrottleSeconds { get; set; } = 10;
        
        public bool DumpWinnersOnDraw { get; set; } = true;

        [JsonProperty("_luck_help")] public string LuckHelp { get; set; } = "SubLuckMultiplier: Bonus tickets for subs (e.g. 2.0 = 2x tickets). WinChance: Probability (0.0-1.0) that ANY entry is accepted (Gatekeeper).";
        public decimal SubLuckMultiplier { get; set; } = 2.0m;

        // Entry Validation & Bot Detection Settings
        // Entry Validation & Bot Detection Settings
        [JsonProperty("_username_regex_help")] public string UsernameRegexHelp { get; set; } = "Regex pattern for username validation (null/empty = disabled). Example GW2: ^[A-Za-z0-9\\-\\_]{3,32}$";
        public string UsernameRegex { get; set; } = null;

        [JsonProperty("_min_account_age_help")] public string MinAccountAgeHelp { get; set; } = "Minimum account age in days (0 = disabled). Rejects entries from accounts younger than specified days.";
        public int MinAccountAgeDays { get; set; } = 180;

        [JsonProperty("_entropy_check_help")] public string EntropyCheckHelp { get; set; } = "Enable entropy checking to detect keyboard smashing / low-quality names (e.g., 'asdfgh', 'zzzzzz').";
        public bool EnableEntropyCheck { get; set; } = true;

        [JsonProperty("_game_filter_help")] public string GameFilterHelp { get; set; } = "Game-specific validation. Options: 'GW2', 'Guild Wars 2' (auto-applies pattern + entropy settings). Leave null for custom UsernameRegex.";
        public string GameFilter { get; set; } = null;

        [JsonProperty("_redemption_cooldown_help")] public string RedemptionCooldownHelp { get; set; } = "Per-user cooldown for redemptions in minutes (0 = disabled). Prevents users from redeeming same reward multiple times.";
        public int RedemptionCooldownMinutes { get; set; } = 0;


        // External Bot Listeners
        [JsonProperty("_allowed_external_bots_help")] public string AllowedExternalBotsHelp { get; set; } = "List of strict bot usernames (case-insensitive) that are allowed to trigger actions. E.g., ['Moobot', 'Nightbot'].";
        public List<string> AllowedExternalBots { get; set; } = new List<string>();

        [JsonProperty("_external_listeners_help")] public string ExternalListenersHelp { get; set; } = "Rules for parsing messages from allowed external bots. Maps regular expressions to specific actions.";
        public List<BotListenerRule> ExternalListeners { get; set; } = new List<BotListenerRule>();
        
        // Toast Notifications
        [JsonProperty("_toast_help")] public string ToastHelp { get; set; } = "Configurable toast notifications for specific events. Key = EventName, Value = Enabled (true/false).";
        public Dictionary<string, bool> ToastNotifications { get; set; } = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
        {
            { "EntryAccepted", false },
            { "EntryRejected", true },
            { "WinnerSelected", true },
            { "GiveawayOpened", true },
            { "GiveawayClosed", true },
            { "UnauthorizedAccess", true }
        };

        // Missing properties fixed
        public double WinChance { get; set; } = 1.0;
        public bool RequireSubscriber { get; set; } = false;

        [JsonProperty("_messages_help")] public string MessagesHelp { get; set; } = "Override default messages. Key:Value pairs.";
        public Dictionary<string, string> Messages { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [JsonProperty("_timer_help")] public string TimerHelp { get; set; } = "Auto-close duration (e.g. '10m', '120s', or '5' for 5 minutes). Null = manual only.";
        public string TimerDuration { get; set; }
    }
    /// <summary>
    /// Rule definition for parsing and acting upon messages from external bots.
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
        public string Title { get; set; } = "Giveaway Winner";
        public string Description { get; set; } = "Good Luck Everyone!";
        public int SpinTime { get; set; } = 10;
        public bool AutoRemoveWinner { get; set; } = true;
        public string ShareMode { get; set; } = "private";
        
        [JsonProperty("_variables_help")] public string VariablesHelp { get; set; } = "Tips: Use '{name}' in the WinnerMessage to announce the specific winner!";
        public string WinnerMessage { get; set; } = "Winner is {name}!";

        [JsonExtensionData] public Dictionary<string, object> ExtensionData { get; set; }
    }

    /// <summary>
    /// Maintains the active runtime state of a giveaway (entries, active status, history).
    /// Serialized to disk/global vars for persistence.
    /// </summary>
    public class GiveawayState
    {
        public string CurrentGiveawayId { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, Entry> Entries { get; } = new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
        public List<string> HistoryLog { get; set; } = new List<string>();

        public GiveawayState()
        {
            _globalSpam = new List<DateTime>();
        }

        public DateTime? AutoCloseTime { get; set; }
        public DateTime? StartTime { get; set; }

        public string LastWinnerName { get; set; }
        public string LastWinnerUserId { get; set; }

        public int WinnerCount { get; set; }
        public long CumulativeEntries { get; set; }

        [JsonIgnore] private readonly List<DateTime> _globalSpam;

        // Sliding window spam detection: counts entries in the last N seconds (default 60)
        public bool IsSpamming(int limit, int windowSeconds = 60)
        {
            lock (_globalSpam)
            {
                var n = DateTime.Now;
                _globalSpam.Add(n);
                _globalSpam.RemoveAll(t => (n - t).TotalSeconds > windowSeconds);
                return _globalSpam.Count > limit;
            }
        }
        
        // Per-user redemption cooldown tracking
        public Dictionary<string, DateTime> RedemptionCooldowns { get; set; } = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
        
        // Incremental entry dumping (batched)
        [JsonIgnore] public ConcurrentQueue<Entry> PendingDumps { get; set; } = new ConcurrentQueue<Entry>();
        [JsonIgnore] public DateTime LastDumpTime { get; set; } = DateTime.MinValue;


    }

    /// <summary>
    /// Represents a single user entry in the giveaway.
    /// </summary>
    public class Entry { public string UserId { get; set; } public string UserName { get; set; } public bool IsSub { get; set; } public DateTime EntryTime { get; set; } public int TicketCount { get; set; } }

    /// <summary>
    /// Client for the WheelOfNames.com API v2.
    /// Used to create custom winner-picking wheels dynamically.
    /// </summary>
    public class WheelOfNamesClient
    {
        private static readonly HttpClient _defaultClient = CreateHttpClient();
        private readonly HttpClient _h;

        // Create configured HttpClient with timeout and headers
        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            // Use infinite timeout on the client itself; we control request timeout via CancellationToken
            client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
            client.DefaultRequestHeaders.Add("User-Agent", "GiveawayBot-StreamerBot/1.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }



        public WheelOfNamesClient(HttpMessageHandler handler = null)
        {
            _h = handler != null ? new HttpClient(handler) : _defaultClient;
        }

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
        /// Creates a new wheel via the WheelOfNames API v2.
        /// Handles payload construction and error parsing.
        /// </summary>
        public async Task<string> CreateWheel(CPHAdapter adapter, List<string> e, string k, WheelConfig s, bool validateKey = true)
        {
            string key = adapter.GetGlobalVar<string>(k, true);
            if (string.IsNullOrEmpty(key))
            {
                adapter.Logger?.LogWarn(adapter, "WheelAPI", $"API Key variable '{k}' is empty.");
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
                   var cacheAge = DateTime.Now - cacheEntry.ValidatedAt;
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
                    var validationResult = await ValidateApiKey(adapter, key);
                    if (validationResult == false)
                    {
                        // Invalid key - cache result and abort wheel creation
                        _keyValidationCache[key] = (false, DateTime.Now);
                        adapter.LogWarn("[WheelAPI] Skipping wheel creation: API key validation failed.");
                        return null;
                    }
                    else if (validationResult == true)
                    {
                        // Valid key - cache result for future calls
                        _keyValidationCache[key] = (true, DateTime.Now);
                    }
                    // If null (indeterminate - network error, API down), proceed anyway but don't cache
                    // This allows wheel creation to attempt even if validation service is temporarily unavailable
                }
            }

            try
            {
                // Nest wheel properties under 'wheelConfig' per API v2 specification
                // API requires TWO top-level fields: wheelConfig (object) and shareMode (string)
                var p = new
                {
                    wheelConfig = new
                    {
                        entries = e.Select(x => new { text = x }).ToList(),
                        title = s.Title,
                        description = s.Description,
                        spinTime = s.SpinTime,
                        autoRemoveWinner = s.AutoRemoveWinner,
                        displayWinnerDialog = true,
                        winningMessage = GetRandomMessage(s.WinnerMessage)
                    },
                    shareMode = s.ShareMode
                };
                var jsonPayload = JsonConvert.SerializeObject(p);

                adapter.LogDebug($"[WheelAPI] Requesting wheel creation for {e.Count} unique names.");
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

        public MultiPlatformMessenger(GiveawayBotConfig config)
        {
            Config = config;
        }

        public void Register(IEventBus bus)
        {
            bus.Subscribe<WinnerSelectedEvent>(OnWinnerSelected);
            bus.Subscribe<WheelReadyEvent>(OnWheelReady);
            bus.Subscribe<GiveawayStartedEvent>(OnGiveawayStarted);
            bus.Subscribe<GiveawayEndedEvent>(OnGiveawayEnded);
            bus.Subscribe<EntryAcceptedEvent>(OnEntryAccepted);
        }

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
                 string msg = config.WheelSettings.WinnerMessage.Replace("{name}", evt.Winner.UserName);
                 if (!string.IsNullOrEmpty(msg))
                 {
                     SendBroadcast(evt.Adapter, msg, evt.Source);
                 }
             }
        }

        private void OnWheelReady(WheelReadyEvent evt)
        {
             // Broadcast the wheel link
             SendBroadcast(evt.Adapter, $"Wheel Ready! {evt.Url}", evt.Source);
        }

        private void OnGiveawayStarted(GiveawayStartedEvent evt)
        {
             // Broadcast handled by HandleStart directly
             
             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config) && config.ToastNotifications.TryGetValue("GiveawayOpened", out var notify) && notify)
             {
                 evt.Adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{evt.ProfileName}' is OPEN!");
             }
        }

        private void OnGiveawayEnded(GiveawayEndedEvent evt)
        {
             // Broadcast handled by HandleEnd directly

             if (Config.Profiles.TryGetValue(evt.ProfileName, out var config) && config.ToastNotifications.TryGetValue("GiveawayClosed", out var notify) && notify)
             {
                 evt.Adapter.ShowToastNotification("Giveaway Bot", $"Giveaway '{evt.ProfileName}' is CLOSED!");
             }
        }

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
        public void SendBroadcast(CPHAdapter adapter, string message, string sourcePlatform = "Twitch")
        {
            bool enableMulti = adapter.GetGlobalVar<bool>("GiveawayBot_EnableMultiPlatform", true);
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
        private void SendReply(CPHAdapter adapter, string msg, string platform, string userName, string messageId = null)
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
    }

    /// <summary>
    /// Controls OBS sources (BrowserSource) for displaying the giveaway wheel.
    /// Wraps CPH OBS methods.
    /// </summary>
    public class ObsController
    {
        private readonly GiveawayBotConfig _config;

        public ObsController(GiveawayBotConfig config) 
        {
            _config = config;
        }

        public void Register(IEventBus bus)
        {
            bus.Subscribe<WheelReadyEvent>(OnWheelReady);
        }

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



    /// <summary>
    /// Event Bus Interface for decoupled communication.
    /// </summary>
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler) where T : IGiveawayEvent;
        void Unsubscribe<T>(Action<T> handler) where T : IGiveawayEvent;
        void Publish<T>(T evt) where T : IGiveawayEvent;
    }

    /// <summary>
    /// Base interface for all giveaway events.
    /// </summary>
    public interface IGiveawayEvent
    {
        CPHAdapter Adapter { get; }
        string ProfileName { get; }
        GiveawayState State { get; }
        string Source { get; }
    }

    /// <summary>
    /// Thread-safe Event Bus implementation.
    /// </summary>
    public class GiveawayEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();
        private readonly object _lock = new object();

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

    // --- Core Events ---

    public abstract class GiveawayEventBase : IGiveawayEvent
    {
        public CPHAdapter Adapter { get; }
        public string ProfileName { get; }
        public GiveawayState State { get; }
        public string Source { get; }

        protected GiveawayEventBase(CPHAdapter adapter, string profileName, GiveawayState state, string source)
        {
            Adapter = adapter;
            ProfileName = profileName;
            State = state;
            Source = source;
        }
    }

    public class GiveawayStartedEvent : GiveawayEventBase
    {
        public GiveawayStartedEvent(CPHAdapter adapter, string profileName, GiveawayState state, string source) : base(adapter, profileName, state, source) { }
    }

    public class GiveawayEndedEvent : GiveawayEventBase
    {
        public GiveawayEndedEvent(CPHAdapter adapter, string profileName, GiveawayState state, string source) : base(adapter, profileName, state, source) { }
    }

    public class WinnerSelectedEvent : GiveawayEventBase
    {
        public Entry Winner { get; }
        public WinnerSelectedEvent(CPHAdapter adapter, string profileName, GiveawayState state, Entry winner, string source) : base(adapter, profileName, state, source) 
        {
            Winner = winner;
        }
    }

    public class EntryAcceptedEvent : GiveawayEventBase
    {
        public Entry Entry { get; }
        public string MessageId { get; }
        public EntryAcceptedEvent(CPHAdapter adapter, string profileName, GiveawayState state, Entry entry, string source, string messageId = null) : base(adapter, profileName, state, source)
        {
            Entry = entry;
            MessageId = messageId;
        }
    }

    public class WheelReadyEvent : GiveawayEventBase
    {
        public string Url { get; }
        public WheelReadyEvent(CPHAdapter adapter, string profileName, GiveawayState state, string url, string source) : base(adapter, profileName, state, source)
        {
            Url = url;
        }
    }



    public class UserMetricSet
    {
        public string UserName { get; set; }
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
        public void SaveMetrics(CPHAdapter adapter, MetricsContainer metrics)
        {
            try
            {
                string stateDir = Path.GetDirectoryName(_path);
                if (!string.IsNullOrEmpty(stateDir) && !Directory.Exists(stateDir)) Directory.CreateDirectory(stateDir);
                var json = JsonConvert.SerializeObject(metrics, Formatting.Indented);
                File.WriteAllText(_path, json);
                adapter.LogTrace($"[Metrics] Metrics successfully saved to disk: {_path}");
            }
            catch (Exception ex)
            {
                adapter.LogError($"[Metrics] Failed to save metrics: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads metrics data from the JSON file.
        /// </summary>
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
                adapter.LogError($"[Metrics] Failed to load metrics: {ex.Message}");
                return new MetricsContainer();
            }
        }
    }

#pragma warning restore IDE0090 // 'new' expression can be simplified
#pragma warning restore IDE0300 // Use collection expression
#pragma warning restore IDE0028 // Simplify collection initialization

    /// <summary>
    /// Service to check for updates from the official repository.
    /// </summary>
    public class UpdateService
    {
        // Point to the raw file of the main branch
        private const string ScriptUrl = "https://raw.githubusercontent.com/Sythsaz/Giveaway-Bot/main/GiveawayBot.cs";

        /// <summary>
        /// Checks GitHub for a newer version of the script.
        /// </summary>
        public async Task<(bool available, string version)> CheckForUpdatesAsync(CPHAdapter adapter, string currentVersion)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    // Add User-Agent to comply with GitHub API policies (even for raw content it's good practice)
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("StreamerBot-GiveawayHelper");

                    var content = await client.GetStringAsync(ScriptUrl);

                    // Regex to find: public const string Version = "1.3.2";
                    // Matches standard C# constant declaration
                    var match = Regex.Match(content, @"public\s+const\s+string\s+Version\s*=\s*""([\d\.]+)""");
                    if (match.Success)
                    {
                        string remoteVersion = match.Groups[1].Value;
                        if (IsNewer(currentVersion, remoteVersion))
                        {
                            return (true, remoteVersion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                adapter.LogWarn($"[UpdateService] Failed to check for updates: {ex.Message}");
            }
            return (false, "");
        }

        /// <summary>
        /// Downloads the latest script to a local updates folder.
        /// </summary>
        public async Task<string> DownloadUpdateAsync(CPHAdapter adapter, string version)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("StreamerBot-GiveawayHelper");

                    var content = await client.GetStringAsync(ScriptUrl);

                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    // Ensure we don't double up path separators or fail on different setups
                    string folder = Path.Combine(baseDir, "Giveaway Bot", "updates");

                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    string fileName = $"GiveawayBot_v{version}.cs.txt";
                    string fullPath = Path.Combine(folder, fileName);

                    File.WriteAllText(fullPath, content);
                    return fullPath;
                }
            }
            catch (Exception ex)
            {
                adapter.LogError($"[UpdateService] Failed to download update: {ex.Message}");
                return null;
            }
        }

        private bool IsNewer(string current, string remote)
        {
            if (System.Version.TryParse(current, out var vCur) && System.Version.TryParse(remote, out var vRem))
            {
                return vRem > vCur;
            }
            // Fallback string compare if parse fails (unlikely given regex)
            return string.Compare(remote, current, StringComparison.OrdinalIgnoreCase) > 0;
        }
    }

#if EXTERNAL_EDITOR || GIVEAWAY_TESTS
}
#endif

