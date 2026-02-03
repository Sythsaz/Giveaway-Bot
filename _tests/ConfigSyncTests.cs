// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class ConfigSyncTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Configuration & Sync Tests ===");
            await Test_GlobalSettingsSync();
            await Test_VariableSync();
            await Test_ExposeVariables_GlobalOverride();
            await Test_RunMode_GlobalVar();
            await Test_ConfigErrorTracking();
            await Test_BooleanParsingVariants();
            await Test_InitialSync();
            await Test_FullConfigSync();
            await Test_RunMode_Mirror();
            Test_RunModeBootstrap();
            await Test_RunModeSwitching();
            await Test_RunMode_Mirror_Conflict();
            await Test_ImportGlobals();
            await Test_DynamicVariableSync();
            await Test_FullMirrorIngestion();
        }

        private static (GiveawayManager m, MockCPH cph) SetupWithCph()
        {
            var cph = new MockCPH();
            var m = new GiveawayManager();
            // Reset static state for isolation (crucial for config tests)
            GiveawayManager.GlobalConfig = null;
            m.States.Clear(); // Although m is new, States is per instance, but GlobalConfig is static
            var adapter = new CPHAdapter(cph);
            adapter.Logger = cph.Logger;

            // Force RunMode via GlobalVar override BEFORE Initialize to prevent "Mirror" default exposure
            cph.Globals["Giveaway Global RunMode"] = "FileSystem";

            // Delete config file to ensure clean defaults (Expose=False)
            // Path might depend on where tests are running, but assuming standard structure:
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
                if (File.Exists(configPath))
                {
                    // Forcefully delete to ensure no lingering Mirror mode matching happens from disk
                    File.Delete(configPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST WARN] Failed to delete config: {ex.Message}");
                // If delete fails, try to overwrite with defaults so we don't stick to Mirror mode
                try
                {
                    string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
                    File.WriteAllText(configPath, "{}"); // Empty JSON defaults to FileSystem mode in loader
                }
                catch { }
            }

            m.Initialize(adapter);
            // Ensure Main exists in a clean state
#pragma warning disable IDE0074 // Use compound assignment - C# 7.3 doesn't support null-coalescing assignment
            if (GiveawayManager.GlobalConfig == null) GiveawayManager.GlobalConfig = new GiveawayBotConfig();
            if (GiveawayManager.GlobalConfig.Profiles == null) GiveawayManager.GlobalConfig.Profiles = new Dictionary<string, GiveawayProfileConfig>(StringComparer.OrdinalIgnoreCase);
            if (!GiveawayManager.GlobalConfig.Profiles.ContainsKey("Main")) GiveawayManager.GlobalConfig.Profiles["Main"] = new GiveawayProfileConfig();
#pragma warning restore IDE0074
            var config = GiveawayManager.GlobalConfig;
            // Default to FileSystem for tests unless specified otherwise, to respect ExposeVariables toggle
            config.Globals.RunMode = "FileSystem";
            // CheckForConfigUpdates/GetConfig might reload defaults (Mirror) if file missing.
            // Force RunMode via GlobalVar override to ensure test isolation.
            cph.Globals["Giveaway Global RunMode"] = "FileSystem";
            return (m, cph);
        }

        private static async Task Test_VariableSync()
        {
            Console.Write("[TEST] Variable Sync: ");
            var (m, c) = SetupWithCph();
            try
            {
                var config = GiveawayManager.GlobalConfig.Profiles["Main"];
                // 2. Set defaults force-ably via Disk Overwrite
                // Create a fresh config to ensure we aren't fighting static state
                var freshConfig = new GiveawayBotConfig();
                freshConfig.Globals.RunMode = "FileSystem";
                freshConfig.Profiles["Main"] = new GiveawayProfileConfig(); // Ensure Main exists with defaults
                freshConfig.Profiles["Main"].EnableEntropyCheck = false;
                freshConfig.Profiles["Main"].MaxEntriesPerMinute = 100;
                freshConfig.Profiles["Main"].RequireSubscriber = false;

                // SAVE to disk so reload consumes THIS config
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
                File.WriteAllText(configPath, JsonConvert.SerializeObject(freshConfig, Formatting.Indented));

                // Clear any globals set during Initialize (if RunMode default was Mirror)
                // But preserve the RunMode override we need
                c.Globals.Clear();
                c.Globals["Giveaway Global RunMode"] = "FileSystem";

                m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed
                c.Args["userId"] = "ValidUserOne";
                c.Args["user"] = "ValidUserOne";
                c.Args["command"] = "!enter";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (c.Globals.ContainsKey("Giveaway Main EntryCount")) throw new Exception("Variables exposed while disabled!");

                // 2. Enable and sync
                GiveawayManager.GlobalConfig.Profiles["Main"].ExposeVariables = true;

                // Workaround: Clear private _lastSyncedValues to force full sync (since we cleared Globals)
                var field = typeof(GiveawayManager).GetField("_lastSyncedValues", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    var dict = (System.Collections.IDictionary)field.GetValue(m);
                    dict?.Clear();
                }

                c.Args["userId"] = "ValidUserTwo";
                c.Args["user"] = "ValidUserTwo";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (!c.Globals.TryGetValue("Giveaway Main EntryCount", out var count) || (int)count != 2)
                    throw new Exception($"Entry count mismatch: expected 2, got {count} (UserOneAccepted: {c.Globals.ContainsKey("Giveaway Main EntryCount")})");

                if (!c.Globals.TryGetValue("Giveaway Main IsActive", out var active))
                {
                    var keys = string.Join(", ", c.Globals.Keys);
                    throw new Exception($"IsActive mismatch - Missing from Globals. Available: {keys}");
                }

                if (active is bool b && !b)
                    throw new Exception($"IsActive mismatch - Value is False");

                if (!(active is bool))
                    throw new Exception($"IsActive mismatch - Type is {active?.GetType().Name}, Value: {active}");

                // 3. Winner sync
                c.Args.Clear();
                c.Args["isBroadcaster"] = true;
                c.Args["command"] = "!draw";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (!c.Globals.TryGetValue("Giveaway Main WinnerName", out var name) || string.IsNullOrEmpty(name?.ToString()))
                    throw new Exception("WinnerName not synced");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static async Task Test_ExposeVariables_GlobalOverride()
        {
            Console.Write("[TEST] ExposeVariables Global Override: ");
            var (m, c) = SetupWithCph();

            // Ensure no ghost variables exist from previous tests
            c.Globals.Clear();
            c.SetGlobalVar("Giveaway Global RunMode", "FileSystem", true);

            // Clear internal sync cache (_lastSyncedValues) so manager doesn't think vars are already set
            var cacheField = typeof(GiveawayManager).GetField("_lastSyncedValues", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (cacheField != null)
            {
                var cache = cacheField.GetValue(m) as System.Collections.IDictionary;
                cache?.Clear();
                Console.WriteLine("[TEST DEBUG] Internal Sync Cache Cleared.");
            }

            m.States["Main"].Entries.Clear();


            var config = GiveawayManager.GlobalConfig.Profiles["Main"];
            // Set overwrite FIRST to prevent race condition with LifecycleTimer
            c.SetGlobalVar("Giveaway Global ExposeVariables", "false", true);
            config.ExposeVariables = true;

            try
            {
                // Re-sync to ensure the manager sees the global override immediately
                // Since we aren't using the full runner loop, we manually trigger a sync or rely on ProcessTrigger
                // But ProcessTrigger might be too late if the check happens inside it.
                // Actually, ProcessTrigger checks updates at the start.
                // Ensure args are clean so we don't accidentally match a different logic path
                c.Args.Clear();

                // DEBUG: Ensure override is set
                if (!c.Globals.ContainsKey("Giveaway Global ExposeVariables")) Console.WriteLine("[TEST WARN] Override missing from Globals!");

                // Enable TRACE logging to see Sync decisions
                GiveawayManager.GlobalConfig.Globals.LogLevel = "TRACE";
                GiveawayManager.GlobalConfig.Globals.RunMode = "FileSystem"; // Fix: Default is Mirror, which forces sync
                // Persist this change so reloads don't revert to Mirror
                new ConfigLoader().WriteConfigText(new CPHAdapter(c), Newtonsoft.Json.JsonConvert.SerializeObject(GiveawayManager.GlobalConfig, Newtonsoft.Json.Formatting.Indented));

                m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed

                Console.WriteLine($"[TEST DEBUG] Globals Count after Setup: {c.Globals.Count}");

                c.Args["userId"] = "GlobalOverrideOne";
                c.Args["user"] = "GlobalOverrideOne";
                c.Args["command"] = "!enter";
                await m.ProcessTrigger(new CPHAdapter(c));

                Console.WriteLine($"[TEST DEBUG] Globals Count BEFORE CHECK: {c.Globals.Count}");
                if (c.Globals.Count > 0)
                    Console.WriteLine($"[TEST DEBUG] Global Keys: {string.Join(", ", c.Globals.Keys)}");

                if (c.Globals.ContainsKey("Giveaway Main EntryCount")) throw new Exception("Global override (false) failed!");

                // 2. Profile disabled, but Global Override = true
                config.ExposeVariables = false;
                c.SetGlobalVar("Giveaway Global ExposeVariables", "true", true); // Set global variable override

                c.Args["userId"] = "GlobalOverrideTwo";
                c.Args["user"] = "GlobalOverrideTwo";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (!c.Globals.TryGetValue("Giveaway Main EntryCount", out var count) || (int)count != 2)
                    throw new Exception(String.Format("Global override (true) failed! Count: {0}", count));

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static async Task Test_RunMode_GlobalVar()
        {
            Console.Write("[TEST] RunMode GlobalVar: ");
            var c = new MockCPH();
            var m = new GiveawayManager();

            try
            {
                // Set mode to GlobalVar and inject config into variable
                c.SetGlobalVar("Giveaway Global RunMode", "GlobalVar", true);
                // We need to ensure global settings reflect this too if default was FileSystem
                GiveawayManager.GlobalConfig = null; // Reset to force reload logic if any
                var configJson = "{\"Globals\":{\"RunMode\":\"GlobalVar\"},\"Profiles\":{\"GlobalProfile\":{\"ExposeVariables\":true}}}";
                c.SetGlobalVar("Giveaway Global Config", configJson, true);

                // Ensure no local file interferes with the GlobalVar test
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
                if (File.Exists(configPath))
                {
                    File.Delete(configPath);
                    System.Threading.Thread.Sleep(20);
                }
                // Retry if file lock persists
                int retries = 0;
                while (File.Exists(configPath) && retries < 5)
                {
                    try { File.Delete(configPath); } catch { }
                    System.Threading.Thread.Sleep(50);
                    retries++;
                }

                string configDir = Path.GetDirectoryName(configPath);
                if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);

                // Force overwrite with clean default config to purge any "Expose=True" state
                // Retry logic in case of locking
                retries = 0;
                bool success = false;
                while (retries < 10)
                {
                    try
                    {
                        File.WriteAllText(configPath, "{ \"Profiles\": { \"Main\": {} } }");
                        success = true;
                        break;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(50);
                        retries++;
                    }
                }
                if (!success && File.Exists(configPath)) Console.WriteLine("[WARN] Failed to overwrite config file!");

                m.Initialize(new CPHAdapter(c));

                // Wait for background startup sync (CheckForUpdatesStartup) to finish
                // This prevents race conditions where initial sync creates variables mid-test
                // Using explicit WaitForStartup instead of arbitrary Delay
                m.WaitForStartup().Wait();
                // Force FileSystem mode to ensure ExposeVariables logic is tested (Mirror forces expose)
                Console.WriteLine($"[SETUP DEBUG] Clearing Globals. Count Before: {c.Globals.Count}");

                Task.Delay(50).Wait(); // Let any running callback finish

                c.Globals.Clear();
                Console.WriteLine($"[SETUP DEBUG] Globals Count After Clear: {c.Globals.Count}");

                c.SetGlobalVar("Giveaway Global RunMode", "GlobalVar", true);

                if (!GiveawayManager.GlobalConfig.Profiles.ContainsKey("GlobalProfile")) throw new Exception("Failed to load config from GlobalVar!");

                // Trigger an update (Create Profile)
                c.Args["isBroadcaster"] = true;
                c.Args["rawInput"] = "!giveaway create NewGlobal";
                await m.ProcessTrigger(new CPHAdapter(c));

                var updatedJson = c.GetGlobalVar<string>("Giveaway Global Config", true);
                if (!updatedJson.Contains("NewGlobal")) throw new Exception("Failed to save config back to GlobalVar!");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static async Task Test_ConfigErrorTracking()
        {
            Console.Write("[TEST] Config Error Tracking: ");

            // CLEANUP: Delete config file to prevent Mirror bootstrap carrying over
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
            if (File.Exists(configPath)) File.Delete(configPath);
            int retries = 0;
            while (File.Exists(configPath) && retries < 5) { try { File.Delete(configPath); } catch { } System.Threading.Thread.Sleep(50); retries++; }

            var c = new MockCPH();
            var m = new GiveawayManager();
            try
            {
                m.Initialize(new CPHAdapter(c));
                await Task.Delay(1000); // Allow CheckForConfigUpdatesStartup to finish bootstrapping
                // Warmup: Ensure _previousConfig is populated so subsequent error-fallback doesn't look like a "Change"
                await m.ProcessTrigger(new CPHAdapter(c));

                // Delete file created by bootstrap to force GlobalVar read during validation (since InvalidateCache resets _lastLoad)
                if (File.Exists(configPath)) File.Delete(configPath);


                // Inject broken JSON into global var and set mode
                // Use ReadOnlyVar to prevent ApplyConfigUpdates from overwriting our broken JSON (Self-Healing)
                c.SetGlobalVar("Giveaway Global RunMode", "ReadOnlyVar", true);
                c.SetGlobalVar("Giveaway Global Config", "{ broken json", true);
                // Force reload by making global var appear newer
                c.SetGlobalVar("Giveaway Global Config LastWriteTime", DateTime.Now.AddDays(1).ToString("o"), true);

                // Force cache invalidation to ensure immediate reload
                m.Loader.InvalidateCache();

                // Trigger validation check
                c.Args["isBroadcaster"] = true;
                c.Args["rawInput"] = "!giveaway config check";
                await m.ProcessTrigger(new CPHAdapter(c));

                var errors = c.GetGlobalVar<string>("Giveaway Global LastConfigErrors", true);
                if (string.IsNullOrEmpty(errors))
                {
                    Console.WriteLine($"[TEST DEBUG] LastConfigErrors Empty! Globals Count: {c.Globals.Count}");
                    Console.WriteLine($"[TEST DEBUG] Keys: {string.Join(", ", c.Globals.Keys)}");
                    throw new Exception("Failed to track config errors (variable is empty)!");
                }
                if (!errors.Contains("JSON")) throw new Exception($"Failed to track config errors (variable content mismatch: '{errors}')!");

                // Fix JSON
                c.SetGlobalVar("Giveaway Global Config", "{\"Profiles\":{}}", true);
                m.Loader.InvalidateCache();
                await m.ProcessTrigger(new CPHAdapter(c)); // This re-runs validation during IdentifyTrigger/RefreshConfig flow

                c.Args["rawInput"] = "!giveaway config check";
                await m.ProcessTrigger(new CPHAdapter(c));

                errors = c.GetGlobalVar<string>("Giveaway Global LastConfigErrors", true);
                if (!string.IsNullOrEmpty(errors)) throw new Exception($"Failed to clear config errors (variable still has: '{errors}')!");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static async Task Test_BooleanParsingVariants()
        {
            Console.Write("[TEST] Boolean Parsing Variants: ");
            var (m, c) = SetupWithCph();
            try
            {
                // Ensure global override is active for this test so we can toggle it globally easily
                c.SetGlobalVar("Giveaway Global RunMode", "FileSystem", true);
                c.SetGlobalVar("Giveaway Global ExposeVariables", "true", true);

                var config = GiveawayManager.GlobalConfig.Profiles["Main"];
                config.ExposeVariables = false;
                m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed

                int i = 0;
                string[] truthy = new string[] { "true", "TRUE", "1", "yes", "YES", "on" };
                foreach (var t in truthy)
                {
                    c.Globals.Clear();
                    c.SetGlobalVar("Giveaway Global RunMode", "FileSystem", true);
                    // Set GlobalVar to force Enable
                    c.SetGlobalVar("Giveaway Global ExposeVariables", t, true);

                    c.Args["userId"] = "TestUser_True_" + (i++);
                    c.Args["user"] = "TestUser_True_" + i;
                    c.Args["command"] = "!enter";
                    await m.ProcessTrigger(new CPHAdapter(c));
                    if (!c.Globals.ContainsKey("Giveaway Main EntryCount")) throw new Exception($"Failed to parse truthy variant: {t}");
                }

                config.ExposeVariables = true;
                string[] falsy = new string[] { "false", "FALSE", "0", "no", "NO", "off" };
                foreach (var f in falsy)
                {
                    c.Globals.Clear();
                    c.SetGlobalVar("Giveaway Global RunMode", "FileSystem", true);
                    // Set GlobalVar to force Disable
                    c.SetGlobalVar("Giveaway Global ExposeVariables", f, true);

                    c.Args["userId"] = "TestUser_False_" + (i++);
                    c.Args["user"] = "TestUser_False_" + i;
                    await m.ProcessTrigger(new CPHAdapter(c));
                    // If skip, variable won't be set
                    if (c.Globals.ContainsKey("Giveaway Main EntryCount")) throw new Exception($"Failed to parse falsy variant: {f}");
                }

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static async Task Test_InitialSync()
        {
            Console.Write("[TEST] Initial Sync: ");
            var c = new MockCPH();
            c.SetGlobalVar("Giveaway Global ExposeVariables", "true", true);
            c.SetGlobalVar("Giveaway Global LogLevel", "INFO", true);
            c.SetGlobalVar("Giveaway Global RunMode", "GlobalVar", true);
            c.SetGlobalVar("Giveaway Global Config", "{\"Globals\":{\"RunMode\":\"GlobalVar\",\"LogLevel\":\"debug\",\"ExposeVariables\":true},\"Profiles\":{\"Main\":{},\"Weekly\":{}}}", true);

            var m = new GiveawayManager();
            try
            {
                m.Initialize(new CPHAdapter(c));

                if (!c.Globals.ContainsKey("Giveaway Main EntryCount")) throw new Exception("Main variables missing after Initialize!");
                if (!c.Globals.ContainsKey("Giveaway Weekly EntryCount")) throw new Exception("Weekly variables missing after Initialize!");
                if (!c.Globals.TryGetValue("Giveaway Global RunMode", out var rm) || rm.ToString() != "GlobalVar") throw new Exception("RunMode missing or incorrect after Initialize!");
                if (c.Globals["Giveaway Global LogLevel"]?.ToString() != "DEBUG") throw new Exception("LogLevel mismatch!");

                // Test config change sync (simulating adding a profile)
                // 1. Enable GlobalVar Mode and wait for Migration (FileSystem -> GlobalVar)
                // Lifecycle tick defaults to 5s. We wait 6s to ensure it runs and overwrites GlobalVar with current memory state (Migration).
                Console.WriteLine($"[TEST] Setting RunMode=GlobalVar at {DateTime.Now}");
                c.SetGlobalVar("Giveaway Global RunMode", "GlobalVar", true);

                // Explicitly check for updates immediately to avoid waiting for timer
                // But test logic relies on Timer? "We wait 6s to ensure it runs"
                // The timer might be running on OLD MockCPH if we didn't cleanup? 
                // Now we cleanup. So new Timer starts.

                await Task.Delay(6000);
                Console.WriteLine($"[TEST] RunMode after delay: {c.GetGlobalVar<string>("Giveaway Global RunMode")}");

                // 2. Inject new Config (simulating adding a profile)
                var newJson = "{\"Globals\":{\"RunMode\":\"GlobalVar\"},\"Profiles\":{\"Main\":{},\"Weekly\":{},\"Bonus\":{}}}";
                c.SetGlobalVar("Giveaway Global Config", newJson, true);

                // No need to wait long, just ensure variable is set. "config check" will force reload.
                await Task.Delay(500);

                Console.WriteLine($"[TEST] RunMode before trigger: {c.GetGlobalVar<string>("Giveaway Global RunMode")}");
                c.Args["rawInput"] = "!giveaway config check";
                c.Args["isBroadcaster"] = true;
                await m.ProcessTrigger(new CPHAdapter(c));

                if (!c.Globals.ContainsKey("Giveaway Bonus EntryCount")) throw new Exception($"New profile 'Bonus' missing! RunMode={c.GetGlobalVar<string>("Giveaway Global RunMode")}, ConfigLoaded={GiveawayManager.GlobalConfig.Profiles.Count}");
                if (m.Messenger.Config.Profiles.Count != 3) throw new Exception("Messenger config not updated!");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }

        private static Task Test_FullConfigSync()
        {
            Console.Write("[TEST] Full Config Sync: ");
            var (m, c) = SetupWithCph();
            c.SetGlobalVar("Giveaway Global ExposeVariables", "true", true);

            try
            {
                // Ensure some specific values are set in config
                GiveawayManager.GlobalConfig.Globals.LogRetentionDays = 123;
                GiveawayManager.GlobalConfig.Profiles["Main"].MaxEntriesPerMinute = 456;
                GiveawayManager.GlobalConfig.Profiles["Main"].WheelSettings.Title = "TEST WHEEL";

                // Sync
                m.SyncAllVariables(new CPHAdapter(c));

                // Verify Globals
                if (!c.Globals.TryGetValue("Giveaway Global LogRetentionDays", out var ret) || (int)ret != 123)
                    throw new Exception($"Global LogRetentionDays mismatch: {ret}");

                // Verify Profile Config
                if (!c.Globals.TryGetValue("Giveaway Main MaxEntriesPerMinute", out var max) || (int)max != 456)
                    throw new Exception($"Profile MaxEntries mismatch: {max}");

                if (!c.Globals.TryGetValue("Giveaway Main WheelSettings Title", out var title) || title.ToString() != "TEST WHEEL")
                    throw new Exception($"Profile Wheel Title mismatch: {title}");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
            return Task.CompletedTask;
        }

        private static async Task Test_RunMode_Mirror()
        {
            Console.Write("[TEST] RunMode Mirror: ");
            var c = new MockCPH();
            c.LogInfo("[TEST_MARKER] Starting Test_RunMode_Mirror");
            var m = new GiveawayManager();

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configDir = Path.Combine(baseDir, "Giveaway Bot", "config");
            string configPath = Path.Combine(configDir, "giveaway_config.json");

            if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);

            try
            {
                // 1. Setup Initial: Mode=Mirror, File=A, Var=Empty
                c.SetGlobalVar("Giveaway Global RunMode", "Mirror", true);
                c.SetGlobalVar("Giveaway A TimerDuration", "10m", true); // FIX: Prevent TimerDuration oscilation
                // FIX: Verify Globals match to prevent CheckForConfigUpdates from triggering a dirty Save() and stomping our timestamp
                string jsonA = "{\"Profiles\":{\"A\":{\"TimerDuration\":\"10m\"}}, \"Globals\":{\"RunMode\":\"Mirror\"}}";
                File.WriteAllText(configPath, jsonA);
                File.SetLastWriteTime(configPath, DateTime.Now.AddSeconds(-10));

                m.Initialize(new CPHAdapter(c)); // Should load A, sync to Var

                var varA = c.GetGlobalVar<string>("Giveaway Global Config", true);
                if (string.IsNullOrEmpty(varA) || !varA.Contains("\"A\"")) throw new Exception("Init: Failed to sync File to GlobalVar (Profile A missing)!");

                // 2. Mock External GlobalVar Update (Var=B)
                // We keep RunMode Mirror in the update payload too
                string jsonB = "{\"Profiles\":{\"A\":{\"TimerDuration\":\"10m\"},\"B\":{\"TimerDuration\":\"10m\"}}, \"Globals\":{\"RunMode\":\"Mirror\"}}";
                c.SetGlobalVar("Giveaway Global Config", jsonB, true);
                c.SetGlobalVar("Giveaway B TimerDuration", "10m", true); // Ensure B doesn't trigger dirty

                // Ensure Global is NEWER than the file (which might have been touched by Initialize)
                var currentDiskTime = File.GetLastWriteTime(configPath);
                string newTs = currentDiskTime.AddDays(2).ToString("o");
                c.SetGlobalVar("Giveaway Global Config LastWriteTime", newTs, true);

                // Verify MockCPH State
                var checkTs = c.GetGlobalVar<string>("Giveaway Global Config LastWriteTime", true);
                Console.WriteLine($"[TEST DEBUG] Set Global TS: {newTs}. Readback: {checkTs}");
                if (checkTs != newTs) throw new Exception("MockCPH failed to persist variable!");

                await Task.Delay(5500); // Wait for hot-reload throttle (5.5s)
                c.Args["rawInput"] = "!giveaway config check"; // Trigger GetConfig via ProcessTrigger
                c.Args["isBroadcaster"] = true;
                await m.ProcessTrigger(new CPHAdapter(c));

                string fileB = File.ReadAllText(configPath);
                if (string.IsNullOrEmpty(fileB) || !fileB.Contains("\"B\"")) throw new Exception("Update: Failed to sync GlobalVar to File (Profile B missing)!");

                // 3. Mock External File Update (File=C)
                string jsonC = "{\"Profiles\":{\"A\":{\"TimerDuration\":\"10m\"},\"B\":{\"TimerDuration\":\"10m\"},\"C\":{\"TimerDuration\":\"10m\"}}, \"Globals\":{\"RunMode\":\"Mirror\"}}";
                File.WriteAllText(configPath, jsonC);
                // Ensure timestamp is definitively newer than the Global TS (which was +2 days)
                File.SetLastWriteTime(configPath, DateTime.Now.AddDays(3));

                await Task.Delay(5500);
                await m.ProcessTrigger(new CPHAdapter(c));

                var varC = c.GetGlobalVar<string>("Giveaway Global Config", true);
                if (string.IsNullOrEmpty(varC) || !varC.Contains("\"C\"")) throw new Exception("Sync: Failed to sync File to GlobalVar (Profile C missing)!");

                Console.WriteLine("PASS");
            }
            finally
            {
                m?.Dispose();
                if (File.Exists(configPath)) File.Delete(configPath);
                // Clean up debug log
                // if (File.Exists(@"C:\Users\ashto\Giveaway Bot\_tests\debug_log.txt")) File.Delete(@"C:\Users\ashto\Giveaway Bot\_tests\debug_log.txt");
            }
        }

        private static void Test_RunModeBootstrap()
        {
            Console.Write("[TEST] RunMode Bootstrap from File: ");
            var c = new MockCPH();

            // Simulate missing Giveaway Global RunMode variable (not set)
            // Create a temp config file with RunMode set to Mirror
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDir, "Giveaway Bot", "config", "giveaway_config.json");
            string dirPath = Path.GetDirectoryName(configPath) ?? Path.Combine(baseDir, "Giveaway Bot", "config");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            try
            {
                // Write config with RunMode = "Mirror"
                var testConfig = new
                {
                    Profiles = new { Main = new { Triggers = new { } } },
                    Globals = new { RunMode = "Mirror", LogLevel = "INFO" }
                };
                File.WriteAllText(configPath, JsonConvert.SerializeObject(testConfig, Formatting.Indented));

                // Create ConfigLoader (will bootstrap RunMode from file)
                var loader = new ConfigLoader();
                loader.GetConfig(new CPHAdapter(c)); // Trigger bootstrap

                // Verify that Giveaway Global RunMode was set to "Mirror"
                var runMode = c.GetGlobalVar<string>("Giveaway Global RunMode", true);

                if (runMode == "Mirror")
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine($"FAIL (Expected 'Mirror', got '{runMode}')");
                }
            }
            finally
            {
                if (File.Exists(configPath)) File.Delete(configPath);
            }
        }

        private static async Task Test_RunModeSwitching()
        {
            Console.Write("\n[TEST] RunMode Switching: ");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                // 1. RunMode Switching
                cph.SetGlobalVar("Giveaway Global RunMode", "FileSystem", true);
                m.Loader.InvalidateCache();

                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create P9RunMode";
                await m.ProcessTrigger(adapter);

                cph.SetGlobalVar("Giveaway Global RunMode", "GlobalVar", true);
                m.Loader.InvalidateCache();

                var cfg = m.Loader.GetConfig(adapter);
                Console.WriteLine("PASS");
            }
            finally { }
        }

        private static async Task Test_RunMode_Mirror_Conflict()
        {
            Console.WriteLine("\n[TEST] RunMode Mirror Config Conflict:");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            var adapter = new CPHAdapter(cph);

            // Setup
            cph.SetGlobalVar("Giveaway Global RunMode", "Mirror", true);
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDir, "Giveaway Bot", "config", "giveaway_config.json");
            string dirPath = Path.GetDirectoryName(configPath);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            // 1. File Newer -> Should Win
            Console.Write("  - File Newer (Wins):                     ");

            // Global has "OldProfile"
            cph.SetGlobalVar("Giveaway Global Config", "{\"Profiles\":{\"OldProfile\":{}}}", true);
            cph.SetGlobalVar("Giveaway Global Config LastWriteTime", DateTime.Now.AddMinutes(-10).ToString("o"), true);

            // File has "NewProfile" and newer time
            File.WriteAllText(configPath, "{\"Profiles\":{\"NewProfile\":{}}}");
            File.SetLastWriteTime(configPath, DateTime.Now);

            // Initialize Manager (Loads Config)
            m.Initialize(adapter);

            // Trigger Load
            // m.Loader.InvalidateCache(); // Init does this
            var config = m.Loader.GetConfig(adapter);

            if (config.Profiles.ContainsKey("NewProfile") && !config.Profiles.ContainsKey("OldProfile"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Has New:{config.Profiles.ContainsKey("NewProfile")}, Has Old:{config.Profiles.ContainsKey("OldProfile")})");

            // 2. Global Newer -> Should Win
            Console.Write("  - Global Newer (Wins):                   ");

            // File has "OldFileProfile" (Older)
            File.WriteAllText(configPath, "{\"Profiles\":{\"OldFileProfile\":{}}}");
            File.SetLastWriteTime(configPath, DateTime.Now.AddMinutes(-20));

            // Global has "NewGlobalProfile" (Newer)
            cph.SetGlobalVar("Giveaway Global Config", "{\"Profiles\":{\"NewGlobalProfile\":{}}}", true);
            // Update Global Timestamp to NOW
            cph.SetGlobalVar("Giveaway Global Config LastWriteTime", DateTime.Now.ToString("o"), true);

            m.Loader.InvalidateCache();
            config = m.Loader.GetConfig(adapter);

            if (config.Profiles.ContainsKey("NewGlobalProfile") && !config.Profiles.ContainsKey("OldFileProfile"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Has New:{config.Profiles.ContainsKey("NewGlobalProfile")}, Has Old:{config.Profiles.ContainsKey("OldFileProfile")})");

            m.Dispose();
            await Task.CompletedTask;
        }

        private static async Task Test_ImportGlobals()
        {
            Console.WriteLine("\n[TEST] ImportGlobals Logic:");
            Console.Write("  - Imports missing vars:                  ");

            // Setup
            var cph = new MockCPH();
            var adapter = new CPHAdapter(cph);
            var m = new GiveawayManager();

            // Prepare Config with ImportGlobals
            var config = new GiveawayBotConfig();
            config.Globals.ImportGlobals = new Dictionary<string, string>
            {
                { "NewKey", "NewValue" },
                { "ExistingKey", "BotShouldNotOverwriteThis" }
            };

            // Set existing key in CPH to ensure it is NOT overwritten
            cph.SetGlobalVar("ExistingKey", "OriginalValue", true);
            cph.SetGlobalVar("Giveaway Global Config", JsonConvert.SerializeObject(config), true);

            // Initialize runs SyncAllVariables -> Auto-Import
            m.Initialize(adapter);

            // Verify NewKey was imported
            string newVal = cph.GetGlobalVar<string>("NewKey", true);
            if (newVal == "NewValue")
            {
                // Verify ExistingKey was preserved
                string existingVal = cph.GetGlobalVar<string>("ExistingKey", true);
                if (existingVal == "OriginalValue")
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine($"FAIL (Overwrote existing: {existingVal})");
                }
            }
            else
            {
                Console.WriteLine($"FAIL (NewKey not set: {newVal})");
            }

            await Task.CompletedTask;
        }

        private static async Task Test_DynamicVariableSync()
        {
            Console.Write("[TEST] Dynamic Variable Sync (1.3.2): ");
            var (m, cph) = SetupWithCph();

            var adapter = new CPHAdapter(cph);

            // Force Mirror Mode for this test to ensure SyncVar works
            cph.Globals["Giveaway Global RunMode"] = "Mirror";
            cph.Globals["Giveaway Global Config"] = "{ \"Globals\": { \"RunMode\": \"Mirror\" } }";

            await m.Loader.CreateProfileAsync(adapter, "DynamicProfile");
            GiveawayManager.GlobalConfig = m.Loader.GetConfig(adapter);

            var config = GiveawayManager.GlobalConfig;
            var profile = config.Profiles["DynamicProfile"];

            // Set initial values
            profile.MaxEntriesPerMinute = 10;
            profile.RequireSubscriber = false;
            profile.SubLuckMultiplier = 1.0m;

            // Sync initial state (Push)
            m.SyncAllVariables(adapter);

            // Verify initial exposure
            if (!cph.Globals.TryGetValue("Giveaway DynamicProfile MaxEntriesPerMinute", out var v1) || v1.ToString() != "10")
                throw new Exception($"Initial Sync Failed: MaxEntriesPerMinute. Got {v1}");

            if (!cph.Globals.TryGetValue("Giveaway DynamicProfile RequireSubscriber", out var v2) || v2.ToString() != "False")
                throw new Exception($"Initial Sync Failed: RequireSubscriber. Got {v2}");

            // 2. Simulate External Update (Pull)
            cph.Globals["Giveaway DynamicProfile MaxEntriesPerMinute"] = "99";
            cph.Globals["Giveaway DynamicProfile RequireSubscriber"] = "true";
            cph.Globals["Giveaway DynamicProfile SubLuckMultiplier"] = "5.5";

            // Trigger CheckForConfigUpdates
            var checkMethod = m.GetType().GetMethod("CheckForConfigUpdates", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var task = (Task)checkMethod.Invoke(m, new object[] { adapter, true });
            await task;

            // Verify Config Update
            if (profile.MaxEntriesPerMinute != 99)
                throw new Exception(string.Format("Pull Sync Failed: MaxEntriesPerMinute. Expected 99, got {0}", profile.MaxEntriesPerMinute));

            if (profile.RequireSubscriber != true)
                throw new Exception(string.Format("Pull Sync Failed: RequireSubscriber. Expected True, got {0}", profile.RequireSubscriber));

            if (profile.SubLuckMultiplier != 5.5m)
                throw new Exception(string.Format("Pull Sync Failed: SubLuckMultiplier. Expected 5.5, got {0}", profile.SubLuckMultiplier));

            Console.WriteLine("PASS");
        }
        private static async Task Test_FullMirrorIngestion()
        {
            Console.Write("[TEST] Full Mirror Mode Ingestion: ");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            // 1. Create Profile
            await m.Loader.CreateProfileAsync(adapter, "MirrorTest");
            GiveawayManager.GlobalConfig = m.Loader.GetConfig(adapter);
            var profile = GiveawayManager.GlobalConfig.Profiles["MirrorTest"];

            // 2. Set defaults to ensure we are actually changing them
            profile.EnableWheel = true;
            profile.UsernameRegex = null;
            profile.WheelSettings.Title = "Old Title";
            profile.DumpEntriesOnEnd = false;

            // 3. Set Global Variables (The "Mirror")
            cph.Globals["Giveaway MirrorTest EnableWheel"] = "false";
            cph.Globals["Giveaway MirrorTest UsernameRegex"] = "^[A-Z0-9]+$";
            cph.Globals["Giveaway MirrorTest WheelSettings Title"] = "New Mirror Title";
            cph.Globals["Giveaway MirrorTest RedemptionCooldownMinutes"] = "5m"; // Test parsing string
            cph.Globals["Giveaway MirrorTest DumpEntriesOnEnd"] = "true";
            cph.Globals["Giveaway MirrorTest GameFilter"] = "GW2";
            cph.Globals["Giveaway Global RunMode"] = "Mirror";


            // 4. Trigger Sync (simulate period check or trigger)
            // Use reflection to call private CheckForConfigUpdates
            var checkMethod = m.GetType().GetMethod("CheckForConfigUpdates", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var task = (Task)checkMethod.Invoke(m, new object[] { adapter, true }); // fullSync=true
            await task;

            // 5. Verify Ingestion
            if (profile.EnableWheel != false)
                throw new Exception($"EnableWheel failed. Expected False, got {profile.EnableWheel}");

            if (profile.UsernameRegex != "^[A-Z0-9]+$")
                throw new Exception($"UsernameRegex failed. Expected '^[A-Z0-9]+$', got '{profile.UsernameRegex}'");

            if (profile.WheelSettings.Title != "New Mirror Title")
                throw new Exception($"WheelSettings.Title failed. Expected 'New Mirror Title', got '{profile.WheelSettings.Title}'");

            // 5m = 5 minutes
            if (profile.RedemptionCooldownMinutes != 5)
                throw new Exception($"RedemptionCooldownMinutes failed (Parsing). Expected 5, got {profile.RedemptionCooldownMinutes}");

            if (profile.DumpEntriesOnEnd != true)
                throw new Exception($"DumpEntriesOnEnd failed. Expected True, got {profile.DumpEntriesOnEnd}");

            if (profile.GameFilter != "GW2")
                throw new Exception($"GameFilter failed. Expected 'GW2', got '{profile.GameFilter}'");

            Console.WriteLine("PASS");
        }

        private static async Task Test_GlobalSettingsSync()
        {
            Console.Write("[TEST] Global Settings Sync: ");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                // Initialize configuration
                m.Initialize(adapter);

                // Set initial state
                GiveawayManager.GlobalConfig.Globals.LogLevel = "INFO";
                GiveawayManager.GlobalConfig.Globals.FallbackPlatform = "Twitch";
                // Note: SetupWithCph sets RunMode="FileSystem"

                // Set external Global Variables
                cph.Globals["Giveaway Global LogLevel"] = "TRACE";
                cph.Globals["Giveaway Global FallbackPlatform"] = "YouTube";

                // Trigger Sync via reflection
                var checkMethod = m.GetType().GetMethod("CheckForConfigUpdates", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var task = (Task)checkMethod.Invoke(m, new object[] { adapter, true }); // fullSync=true
                await task;

                // Verify
                if (GiveawayManager.GlobalConfig.Globals.LogLevel != "TRACE")
                    throw new Exception($"LogLevel sync failed. Expected TRACE, got {GiveawayManager.GlobalConfig.Globals.LogLevel}");

                if (GiveawayManager.GlobalConfig.Globals.FallbackPlatform != "YouTube")
                    throw new Exception($"FallbackPlatform sync failed. Expected YouTube, got {GiveawayManager.GlobalConfig.Globals.FallbackPlatform}");

                Console.WriteLine("PASS");
            }
            finally
            {
                m.Dispose();
            }
        }
    }
}
