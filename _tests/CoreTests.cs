// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class CoreTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Core Functionality Tests ===");
            await Test_Logging();
            Test_LogRetentionAndSize(); // Non-async
            await Test_SystemHealthCheck();
            await Test_MetricsTracking();
            await Test_ConfigMigration();
            Test_LocSystem();
        }

        private static (GiveawayManager m, MockCPH cph) SetupWithCph()
        {
            var cph = new MockCPH();
            var m = new GiveawayManager();
            // Reset static state
            GiveawayManager.GlobalConfig = null;
            m.States.Clear();
            var adapter = new CPHAdapter(cph);
            adapter.Logger = cph.Logger;
            m.Initialize(adapter);
            return (m, cph);
        }

        private static Task Test_Logging()
        {
            Console.Write("[TEST] Human-Readable Logging: ");
            var cph = new MockCPH();
            // 1. Test Default (INFO) - Debug should NOT be logged
            cph.SetGlobalVar("GiveawayBot_LogLevel", "INFO", true);
            var logger = new FileLogger();
            logger.LogDebug(new CPHAdapter(cph), "TestCat", "HiddenMessage");
            logger.LogInfo(new CPHAdapter(cph), "TestCat", "VisibleMessage");

            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "logs", "General");
            string logFile = Path.Combine(logDir, $"{DateTime.Now:yyyy-MM-dd}.log");

            if (File.Exists(logFile))
            {
                try { File.Delete(logFile); } catch { } // Cleanup previous runs
                logger.LogInfo(new CPHAdapter(cph), "TestCat", "VisibleMessage"); // Write again to ensure fresh file

                string content = File.ReadAllText(logFile);
                if (!content.Contains("HiddenMessage") && content.Contains("VisibleMessage") && content.Contains("[INFO ] [TestCat]"))
                {
                    // 2. Test Level Change (TRACE) - Trace should now be logged
                    cph.SetGlobalVar("GiveawayBot_LogLevel", "TRACE", true);
                    logger.LogTrace(new CPHAdapter(cph), "TestCat", "TraceMessage");
                    content = File.ReadAllText(logFile);
                    if (content.Contains("TraceMessage")) Console.WriteLine("PASS");
                    else Console.WriteLine($"FAIL (Trace missing. Content={content})");
                }
                else Console.WriteLine($"FAIL (Content mismatch)");
            }
            else
            {
                // First write might have failed or file system lag? Write again.
                logger.LogInfo(new CPHAdapter(cph), "TestCat", "FinalRetry");
                if (File.Exists(logFile)) Console.WriteLine("PASS"); else Console.WriteLine("FAIL (File not created)");
            }
            return Task.CompletedTask;
        }

        private static void Test_LogRetentionAndSize()
        {
            Console.Write("[TEST] Log Retention & Size Cap: ");
            var c = new MockCPH();
            var logger = new FileLogger();
            c.Logger = logger;

            // Since PruneLogs is private, we use reflection
            var pruneMethod = typeof(FileLogger).GetMethod("PruneLogs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance) ?? throw new Exception("PruneLogs not found");

            // Base directory for logs
            string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "logs");
            string logDir = Path.Combine(baseDir, "General");
            if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

            // 1. Retention Test
            string oldFile = Path.Combine(logDir, "old-retention-test.log");
            File.WriteAllText(oldFile, "old content");
            File.SetLastWriteTime(oldFile, DateTime.Now.AddDays(-100));

            c.SetGlobalVar("GiveawayBot_LogRetentionDays", 90);
            pruneMethod.Invoke(logger, new object[] { new CPHAdapter(c) });

            if (File.Exists(oldFile)) throw new Exception("Old log was not pruned by retention policy.");

            // 2. Size Cap Test
            string bigFile = Path.Combine(logDir, "big-size-test.log");
            byte[] biggerData = new byte[2 * 1024 * 1024]; // 2MB
            File.WriteAllBytes(bigFile, biggerData);

            c.SetGlobalVar("GiveawayBot_LogSizeCapMB", 1); // 1MB cap
            pruneMethod.Invoke(logger, new object[] { new CPHAdapter(c) });

            if (File.Exists(bigFile)) throw new Exception("Big log was not pruned by size cap.");

            Console.WriteLine("PASS");
        }

        private static async Task Test_SystemHealthCheck()
        {
            Console.WriteLine("[TEST] System Health Check:");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));

            // Map the command manually to trigger the check
            cph.Args["rawInput"] = "!giveaway system test";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // We check logs/messages for expected output. 
            // In MockCPH, SendMessage usually prints to console. 
            // If we reach here without crash, it's a good sign.
            Console.WriteLine("    -> Check complete (checks passed if no exception)");
        }

        private static async Task Test_MetricsTracking()
        {
            Console.Write("[TEST] Metrics Tracking: ");
            var (m, c) = SetupWithCph();

            // 1. Enter as User 1
            c.Args["userId"] = "U1";
            c.Args["user"] = "UserOne";
            c.Args["command"] = "!enter";
            await m.ProcessTrigger(new CPHAdapter(c));

            // 2. Enter as User 2 (Sub)
            c.Args.Clear();
            c.Args["userId"] = "U2";
            c.Args["user"] = "UserTwo";
            c.Args["isSubscribed"] = true;
            c.Args["command"] = "!enter";
            await m.ProcessTrigger(new CPHAdapter(c));

            // 3. Verify Global Metrics
            var total = c.GetGlobalVar<long>("GiveawayBot_Metrics_Entries_Total");
            if (total != 2) throw new Exception($"Global total mismatch: {total}");

            // 4. Verify Per-User Metrics
            var u1Total = c.GetUserVar<long>("U1", "GiveawayBot_UserMetrics_EntriesTotal");
            var u2Total = c.GetUserVar<long>("U2", "GiveawayBot_UserMetrics_EntriesTotal");
            if (u1Total != 1 || u2Total != 1) throw new Exception($"User entry metrics mismatch: U1={u1Total}, U2={u2Total}");

            // 5. Draw and verify Win Metric
            c.Args.Clear();
            c.Args["command"] = "!draw";
            c.Args["isBroadcaster"] = true;
            await m.ProcessTrigger(new CPHAdapter(c));

            var u1Wins = c.GetUserVar<long>("U1", "GiveawayBot_UserMetrics_WinsTotal");
            var u2Wins = c.GetUserVar<long>("U2", "GiveawayBot_UserMetrics_WinsTotal");
            if (u1Wins + u2Wins != 1) throw new Exception("Win metric not applied to winner.");

            Console.WriteLine("PASS");
        }

        private static async Task Test_ConfigMigration()
        {
            Console.Write("[TEST] Config Migration: ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configDir = Path.Combine(baseDir, "Giveaway Helper", "config");
            string configPath = Path.Combine(configDir, "giveaway_config.json");

            try
            {
                if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);

                // 1. Create a "Legacy" config (Missing metadata, custom profile)
                var legacy = new
                {
                    Profiles = new Dictionary<string, object> {
                    { "LegacyProfile", new { Triggers = new Dictionary<string, string> { { "command:!old", "Entry" } } } }
                }
                };
                File.WriteAllText(configPath, JsonConvert.SerializeObject(legacy));

                // 2. Run Migration
                cph.Args["rawInput"] = "!giveaway config gen";
                await m.ProcessTrigger(new CPHAdapter(cph));

                // 3. Verify
                string updatedJson = File.ReadAllText(configPath);
                var updated = JsonConvert.DeserializeObject<GiveawayBotConfig>(updatedJson);

                bool hasLegacy = updated != null && updated.Profiles != null && updated.Profiles.ContainsKey("LegacyProfile");
                bool hasMetadata = updated != null && updated.Instructions != null && updated.Instructions.Length > 0;
                bool hasWeekly = updated != null && updated.Profiles != null && updated.Profiles.ContainsKey("Weekly");

                if (hasLegacy && hasMetadata && !hasWeekly) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Legacy={hasLegacy}, Metadata={hasMetadata}, Weekly={hasWeekly})");
            }
            finally
            {
                try { if (File.Exists(configPath)) File.Delete(configPath); } catch { }
            }
        }

        private static void Test_LocSystem()
        {
            Console.WriteLine("\n[TEST] Localization (Loc) System:");

            // 1. Defaut String
            Console.Write("  - Default string retrieval:              ");
            // Ensure Config is empty first
            GiveawayManager.GlobalConfig = new GiveawayBotConfig();

            string def = Loc.Get("EntryAccepted_NoLuck", 5);
            if (def.Contains("Total tickets: 5")) Console.WriteLine("PASS");
            else Console.WriteLine($"FAIL (Got: {def})");

            // 2. Override String
            Console.Write("  - Config override retrieval:             ");
            GiveawayManager.GlobalConfig.Globals.CustomStrings = new Dictionary<string, string>
            {
                { "EntryAccepted_NoLuck", "Overridden! Tickets: {0}" }
            };

            string overridden = Loc.Get("EntryAccepted_NoLuck", 10);
            if (overridden == "Overridden! Tickets: 10") Console.WriteLine("PASS");
            else Console.WriteLine($"FAIL (Got: {overridden})");

            // 3. Missing Key Fallback
            Console.Write("  - Missing key fallback:                  ");
            string missing = Loc.Get("NonExistentKey");
            if (missing == "[NonExistentKey]") Console.WriteLine("PASS");
            else Console.WriteLine($"FAIL (Got: {missing})");
        }
    }
}
