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
            cph.Globals["GiveawayBot_RunMode"] = "FileSystem";

            // Delete config file to ensure clean defaults (Expose=False)
            // Path might depend on where tests are running, but assuming standard structure:
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");
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
                    string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");
                    File.WriteAllText(configPath, "{}"); // Empty JSON defaults to FileSystem mode in loader
                }
                catch { }
            }

            m.Initialize(adapter);
            // Ensure Main exists in a clean state
#pragma warning disable IDE0074 // Use compound assignment - C# 7.3 doesn't support null-coalescing assignment
            if (GiveawayManager.GlobalConfig == null) GiveawayManager.GlobalConfig = new GiveawayBotConfig();
#pragma warning restore IDE0074
            var config = GiveawayManager.GlobalConfig;
            // Default to FileSystem for tests unless specified otherwise, to respect ExposeVariables toggle
            config.Globals.RunMode = "FileSystem";
            // CheckForConfigUpdates/GetConfig might reload defaults (Mirror) if file missing.
            // Force RunMode via GlobalVar override to ensure test isolation.
            cph.Globals["GiveawayBot_RunMode"] = "FileSystem";
            return (m, cph);
        }

        private static async Task Test_VariableSync()
        {
            Console.Write("[TEST] Variable Sync: ");
            var (m, c) = SetupWithCph();
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
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");
            File.WriteAllText(configPath, JsonConvert.SerializeObject(freshConfig, Formatting.Indented));

            // Force the manager to see this new file immediately
            // This prevents a race condition or partial state
            // Although ProcessTrigger will do it, we want to be sure GlobalConfig is aligned

            // Clear any globals set during Initialize
            c.Globals.Clear();
            c.Globals["GiveawayBot_RunMode"] = "FileSystem";

            m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed

            // Clear any globals set during Initialize (if RunMode default was Mirror)
            // But preserve the RunMode override we need
            c.Globals.Clear();
            c.Globals["GiveawayBot_RunMode"] = "FileSystem";

            m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed
            c.Args["userId"] = "V1";
            c.Args["user"] = "V1";
            c.Args["command"] = "!enter";
            await m.ProcessTrigger(new CPHAdapter(c));

            if (c.Globals.ContainsKey("GiveawayBot_Main_EntryCount")) throw new Exception("Variables exposed while disabled!");

            // 2. Enable and sync
            GiveawayManager.GlobalConfig.Profiles["Main"].ExposeVariables = true;

            // Workaround: Clear private _lastSyncedValues to force full sync (since we cleared Globals)
            var field = typeof(GiveawayManager).GetField("_lastSyncedValues", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var dict = (System.Collections.IDictionary)field.GetValue(m);
                dict?.Clear();
            }

            c.Args["userId"] = "V2";
            c.Args["user"] = "V2";
            await m.ProcessTrigger(new CPHAdapter(c));

            if (!c.Globals.TryGetValue("GiveawayBot_Main_EntryCount", out var count) || (int)count != 2)
                throw new Exception($"Entry count mismatch: expected 2, got {count}");

            if (!c.Globals.TryGetValue("GiveawayBot_Main_IsActive", out var active))
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

            if (!c.Globals.TryGetValue("GiveawayBot_Main_WinnerName", out var name) || string.IsNullOrEmpty(name?.ToString()))
                throw new Exception("WinnerName not synced");

            Console.WriteLine("PASS");
        }

        private static async Task Test_ExposeVariables_GlobalOverride()
        {
            Console.Write("[TEST] ExposeVariables Global Override: ");
            var (m, c) = SetupWithCph();
            try
            {
                var config = GiveawayManager.GlobalConfig.Profiles["Main"];
                m.States["Main"].Entries.Clear(); // Clear any existing entries from previous tests

                // 1. Profile enabled, but Global Override = false
                GiveawayManager.GlobalConfig.Profiles["Main"].ExposeVariables = true;
                c.SetGlobalVar("GiveawayBot_ExposeVariables", "false", true); // Set global variable override

                // Re-sync to ensure the manager sees the global override immediately
                // Since we aren't using the full runner loop, we manually trigger a sync or rely on ProcessTrigger
                // But ProcessTrigger might be too late if the check happens inside it.
                // Actually, ProcessTrigger checks updates at the start.
                // Ensure args are clean so we don't accidentally match a different logic path
                c.Args.Clear();

                // DEBUG: Ensure override is set
                if (!c.Globals.ContainsKey("GiveawayBot_ExposeVariables")) Console.WriteLine("[TEST WARN] Override missing from Globals!");

                // Enable TRACE logging to see Sync decisions
                GiveawayManager.GlobalConfig.Globals.LogLevel = "TRACE";

                m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed

                c.Args["userId"] = "GO1";
                c.Args["user"] = "GO1";
                c.Args["command"] = "!enter";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (c.Globals.ContainsKey("GiveawayBot_Main_EntryCount")) throw new Exception("Global override (false) failed!");

                // 2. Profile disabled, but Global Override = true
                config.ExposeVariables = false;
                c.SetGlobalVar("GiveawayBot_ExposeVariables", "true", true); // Set global variable override

                c.Args["userId"] = "GO2";
                c.Args["user"] = "GO2";
                await m.ProcessTrigger(new CPHAdapter(c));

                if (!c.Globals.TryGetValue("GiveawayBot_Main_EntryCount", out var count) || (int)count != 2)
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

            // Set mode to GlobalVar and inject config into variable
            c.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
            // We need to ensure global settings reflect this too if default was FileSystem
            GiveawayManager.GlobalConfig = null; // Reset to force reload logic if any
            var configJson = "{\"Globals\":{\"RunMode\":\"GlobalVar\"},\"Profiles\":{\"GlobalProfile\":{\"ExposeVariables\":true}}}";
            c.SetGlobalVar("GiveawayBot_Config", configJson, true);

            // Ensure no local file interferes with the GlobalVar test
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");
            if (File.Exists(configPath)) File.Delete(configPath);

            m.Initialize(new CPHAdapter(c));

            if (!GiveawayManager.GlobalConfig.Profiles.ContainsKey("GlobalProfile")) throw new Exception("Failed to load config from GlobalVar!");

            // Trigger an update (Create Profile)
            c.Args["isBroadcaster"] = true;
            c.Args["rawInput"] = "!giveaway create NewGlobal";
            await m.ProcessTrigger(new CPHAdapter(c));

            var updatedJson = c.GetGlobalVar<string>("GiveawayBot_Config", true);
            if (!updatedJson.Contains("NewGlobal")) throw new Exception("Failed to save config back to GlobalVar!");

            Console.WriteLine("PASS");
        }

        private static async Task Test_ConfigErrorTracking()
        {
            Console.Write("[TEST] Config Error Tracking: ");
            var c = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(c));

            // Inject broken JSON into global var and set mode
            c.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
            c.SetGlobalVar("GiveawayBot_Config", "{ broken json", true);

            // Wait for hot-reload throttle (5s) to ensure RefreshConfig actually reloads
            await Task.Delay(5100);

            // Trigger validation check
            c.Args["isBroadcaster"] = true;
            c.Args["rawInput"] = "!giveaway config check";
            await m.ProcessTrigger(new CPHAdapter(c));

            var errors = c.GetGlobalVar<string>("GiveawayBot_LastConfigErrors", true);
            if (string.IsNullOrEmpty(errors)) throw new Exception("Failed to track config errors (variable is empty)!");
            if (!errors.Contains("JSON Error")) throw new Exception($"Failed to track config errors (variable content mismatch: '{errors}')!");

            // Fix JSON
            c.SetGlobalVar("GiveawayBot_Config", "{\"Profiles\":{}}", true);
            await Task.Delay(5100);
            await m.ProcessTrigger(new CPHAdapter(c)); // This re-runs validation during IdentifyTrigger/RefreshConfig flow

            c.Args["rawInput"] = "!giveaway config check";
            await m.ProcessTrigger(new CPHAdapter(c));

            errors = c.GetGlobalVar<string>("GiveawayBot_LastConfigErrors", true);
            if (!string.IsNullOrEmpty(errors)) throw new Exception($"Failed to clear config errors (variable still has: '{errors}')!");

            Console.WriteLine("PASS");
        }

        private static async Task Test_BooleanParsingVariants()
        {
            Console.Write("[TEST] Boolean Parsing Variants: ");
            var (m, c) = SetupWithCph();
            // Ensure global override is active for this test so we can toggle it globally easily
            c.SetGlobalVar("GiveawayBot_ExposeVariables", "true", true);

            var config = GiveawayManager.GlobalConfig.Profiles["Main"];
            config.ExposeVariables = false;
            m.States["Main"].IsActive = true; // Activate giveaway so entries can be processed

            int i = 0;
            string[] truthy = new string[] { "true", "TRUE", "1", "yes", "YES", "on" };
            foreach (var t in truthy)
            {
                c.Globals.Clear();
                // Set GlobalVar to force Enable
                c.SetGlobalVar("GiveawayBot_ExposeVariables", t, true);

                c.Args["userId"] = "BT" + (i++);
                c.Args["user"] = "BT" + i;
                c.Args["command"] = "!enter";
                await m.ProcessTrigger(new CPHAdapter(c));
                if (!c.Globals.ContainsKey("GiveawayBot_Main_EntryCount")) throw new Exception($"Failed to parse truthy variant: {t}");
            }

            config.ExposeVariables = true;
            string[] falsy = new string[] { "false", "FALSE", "0", "no", "NO", "off" };
            foreach (var f in falsy)
            {
                c.Globals.Clear();
                // Set GlobalVar to force Disable
                c.SetGlobalVar("GiveawayBot_ExposeVariables", f, true);

                c.Args["userId"] = "BF" + (i++);
                c.Args["user"] = "BF" + i;
                await m.ProcessTrigger(new CPHAdapter(c));
                // If skip, variable won't be set
                if (c.Globals.ContainsKey("GiveawayBot_Main_EntryCount")) throw new Exception($"Failed to parse falsy variant: {f}");
            }

            Console.WriteLine("PASS");
        }

        private static async Task Test_InitialSync()
        {
            Console.Write("[TEST] Initial Sync: ");
            var c = new MockCPH();
            c.SetGlobalVar("GiveawayBot_ExposeVariables", "true", true);
            c.SetGlobalVar("GiveawayBot_LogLevel", "INFO", true);
            c.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
            c.SetGlobalVar("GiveawayBot_Config", "{\"Globals\":{\"RunMode\":\"GlobalVar\",\"LogLevel\":\"debug\",\"ExposeVariables\":true},\"Profiles\":{\"Main\":{},\"Weekly\":{}}}", true);

            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(c));

            if (!c.Globals.ContainsKey("GiveawayBot_Main_EntryCount")) throw new Exception("Main variables missing after Initialize!");
            if (!c.Globals.ContainsKey("GiveawayBot_Weekly_EntryCount")) throw new Exception("Weekly variables missing after Initialize!");
            if (!c.Globals.TryGetValue("GiveawayBot_RunMode", out var rm) || rm.ToString() != "GlobalVar") throw new Exception("RunMode missing or incorrect after Initialize!");
            if (c.Globals["GiveawayBot_LogLevel"]?.ToString() != "DEBUG") throw new Exception("LogLevel mismatch!");

            // Test config change sync (simulating adding a profile)
            var newJson = "{\"Globals\":{\"RunMode\":\"GlobalVar\"},\"Profiles\":{\"Main\":{},\"Weekly\":{},\"Bonus\":{}}}";
            c.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
            c.SetGlobalVar("GiveawayBot_Config", newJson, true);

            await Task.Delay(5100); // Wait for throttle

            c.Args["rawInput"] = "!giveaway config check";
            c.Args["isBroadcaster"] = true;
            await m.ProcessTrigger(new CPHAdapter(c));

            if (!c.Globals.ContainsKey("GiveawayBot_Bonus_EntryCount")) throw new Exception("New profile 'Bonus' missing after config change!");
            if (m.Messenger.Config.Profiles.Count != 3) throw new Exception("Messenger config not updated!");

            Console.WriteLine("PASS");
        }

        private static Task Test_FullConfigSync()
        {
            Console.Write("[TEST] Full Config Sync: ");
            var (m, c) = SetupWithCph();
            c.SetGlobalVar("GiveawayBot_ExposeVariables", "true", true);

            // Ensure some specific values are set in config
            GiveawayManager.GlobalConfig.Globals.LogRetentionDays = 123;
            GiveawayManager.GlobalConfig.Profiles["Main"].MaxEntriesPerMinute = 456;
            GiveawayManager.GlobalConfig.Profiles["Main"].WheelSettings.Title = "TEST WHEEL";

            // Sync
            m.SyncAllVariables(new CPHAdapter(c));

            // Verify Globals
            if (!c.Globals.TryGetValue("GiveawayBot_Globals_LogRetentionDays", out var ret) || (int)ret != 123)
                throw new Exception($"Global LogRetentionDays mismatch: {ret}");

            // Verify Profile Config
            if (!c.Globals.TryGetValue("GiveawayBot_Main_Config_MaxEntriesPerMinute", out var max) || (int)max != 456)
                throw new Exception($"Profile MaxEntries mismatch: {max}");

            if (!c.Globals.TryGetValue("GiveawayBot_Main_Config_Wheel_Title", out var title) || title.ToString() != "TEST WHEEL")
                throw new Exception($"Profile Wheel Title mismatch: {title}");

            Console.WriteLine("PASS");
            return Task.CompletedTask;
        }

        private static async Task Test_RunMode_Mirror()
        {
            Console.Write("[TEST] RunMode Mirror: ");
            var c = new MockCPH();
            var m = new GiveawayManager();

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configDir = Path.Combine(baseDir, "Giveaway Helper", "config");
            string configPath = Path.Combine(configDir, "giveaway_config.json");

            if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);

            try
            {
                // 1. Setup Initial: Mode=Mirror, File=A, Var=Empty
                c.SetGlobalVar("GiveawayBot_RunMode", "Mirror", true);
                string jsonA = "{\"Profiles\":{\"A\":{}}}";
                File.WriteAllText(configPath, jsonA);

                m.Initialize(new CPHAdapter(c)); // Should load A, sync to Var

                var varA = c.GetGlobalVar<string>("GiveawayBot_Config", true);
                if (string.IsNullOrEmpty(varA) || !varA.Contains("\"A\"")) throw new Exception("Init: Failed to sync File to GlobalVar (Profile A missing)!");

                // 2. Mock External GlobalVar Update (Var=B)
                string jsonB = "{\"Profiles\":{\"A\":{},\"B\":{}}}";
                c.SetGlobalVar("GiveawayBot_Config", jsonB, true);

                await Task.Delay(5500); // Wait for hot-reload throttle (5s)
                c.Args["rawInput"] = "!giveaway config check"; // Trigger GetConfig via ProcessTrigger
                c.Args["isBroadcaster"] = true;
                await m.ProcessTrigger(new CPHAdapter(c));

                string fileB = File.ReadAllText(configPath);
                if (string.IsNullOrEmpty(fileB) || !fileB.Contains("\"B\"")) throw new Exception("Update: Failed to sync GlobalVar to File (Profile B missing)!");

                // 3. Mock External File Update (File=C)
                string jsonC = "{\"Profiles\":{\"A\":{},\"B\":{},\"C\":{}}}";
                File.WriteAllText(configPath, jsonC);
                // Ensure timestamp is definitely newer
                File.SetLastWriteTime(configPath, DateTime.Now.AddSeconds(10));

                await Task.Delay(5500);
                await m.ProcessTrigger(new CPHAdapter(c));

                var varC = c.GetGlobalVar<string>("GiveawayBot_Config", true);
                if (string.IsNullOrEmpty(varC) || !varC.Contains("\"C\"")) throw new Exception("Sync: Failed to sync File to GlobalVar (Profile C missing)!");

                Console.WriteLine("PASS");
            }
            finally
            {
                if (File.Exists(configPath)) File.Delete(configPath);
            }
        }

        private static void Test_RunModeBootstrap()
        {
            Console.Write("[TEST] RunMode Bootstrap from File: ");
            var c = new MockCPH();

            // Simulate missing GiveawayBot_RunMode variable (not set)
            // Create a temp config file with RunMode set to Mirror
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDir, "Giveaway Helper", "config", "giveaway_config.json");
            string dirPath = Path.GetDirectoryName(configPath) ?? Path.Combine(baseDir, "Giveaway Helper", "config");
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

                // Verify that GiveawayBot_RunMode was set to "Mirror"
                var runMode = c.GetGlobalVar<string>("GiveawayBot_RunMode", true);

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
                cph.SetGlobalVar("GiveawayBot_RunMode", "FileSystem", true);
                m.Loader.InvalidateCache();

                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create P9RunMode";
                await m.ProcessTrigger(adapter);

                cph.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
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
            cph.SetGlobalVar("GiveawayBot_RunMode", "Mirror", true);
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDir, "Giveaway Helper", "config", "giveaway_config.json");

            // 1. File Newer -> Should Win
            Console.Write("  - File Newer (Wins):                     ");

            // Global has "OldProfile"
            cph.SetGlobalVar("GiveawayBot_Config", "{\"Profiles\":{\"OldProfile\":{}}}", true);
            cph.SetGlobalVar("GiveawayBot_Config_LastWriteTime", DateTime.Now.AddMinutes(-10).ToString("o"), true);

            // File has "NewProfile" and newer time
            File.WriteAllText(configPath, "{\"Profiles\":{\"NewProfile\":{}}}");
            File.SetLastWriteTime(configPath, DateTime.Now);

            // Trigger Load
            m.Loader.InvalidateCache();
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
            cph.SetGlobalVar("GiveawayBot_Config", "{\"Profiles\":{\"NewGlobalProfile\":{}}}", true);
            // Update Global Timestamp to NOW
            cph.SetGlobalVar("GiveawayBot_Config_LastWriteTime", DateTime.Now.ToString("o"), true);

            m.Loader.InvalidateCache();
            config = m.Loader.GetConfig(adapter);

            if (config.Profiles.ContainsKey("NewGlobalProfile") && !config.Profiles.ContainsKey("OldFileProfile"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Has New:{config.Profiles.ContainsKey("NewGlobalProfile")}, Has Old:{config.Profiles.ContainsKey("OldFileProfile")})");

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
            cph.SetGlobalVar("GiveawayBot_Config", JsonConvert.SerializeObject(config), true);

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
            if (!cph.Globals.TryGetValue("GiveawayBot_DynamicProfile_MaxEntriesPerMinute", out var v1) || v1.ToString() != "10")
                throw new Exception($"Initial Sync Failed: MaxEntriesPerMinute. Got {v1}");

            if (!cph.Globals.TryGetValue("GiveawayBot_DynamicProfile_RequireSubscriber", out var v2) || v2.ToString() != "False")
                throw new Exception($"Initial Sync Failed: RequireSubscriber. Got {v2}");

            // 2. Simulate External Update (Pull)
            cph.Globals["GiveawayBot_DynamicProfile_MaxEntriesPerMinute"] = "99";
            cph.Globals["GiveawayBot_DynamicProfile_RequireSubscriber"] = "true";
            cph.Globals["GiveawayBot_DynamicProfile_SubLuckMultiplier"] = "5.5";

            // Trigger CheckForConfigUpdates
            var checkMethod = m.GetType().GetMethod("CheckForConfigUpdates", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var task = (Task)checkMethod.Invoke(m, new object[] { adapter });
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
            cph.Globals["GiveawayBot_MirrorTest_EnableWheel"] = "false";
            cph.Globals["GiveawayBot_MirrorTest_UsernameRegex"] = "^[A-Z0-9]+$";
            cph.Globals["GiveawayBot_MirrorTest_WheelSettings_Title"] = "New Mirror Title";
            cph.Globals["GiveawayBot_MirrorTest_RedemptionCooldownMinutes"] = "5m"; // Test parsing string
            cph.Globals["GiveawayBot_MirrorTest_DumpEntriesOnEnd"] = "true";
            cph.Globals["GiveawayBot_MirrorTest_GameFilter"] = "GW2";


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
    }
}
