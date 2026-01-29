// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class Phase10Tests
    {
        public static async Task Run(GiveawayManager m, MockCPH cph)
        {
            Console.WriteLine("\n=== Phase 10: 100% Coverage & Perfection ===");
            await Test_CommandAliases_Comprehensive(m, cph);
            await Test_ExternalBotIntegration(m, cph);
            await Test_WheelIntegration_Failures();
            await Test_RunMode_Mirror_Conflict(m, cph);
            await Test_Concurrency_EntryVsDraw(m, cph);
        }

        private static async Task Test_CommandAliases_Comprehensive(GiveawayManager m, MockCPH cph)
        {
            Console.WriteLine("\n[TEST] Command Aliases:");
            var adapter = new CPHAdapter(cph);

            // 1. !ga alias (Create)
            Console.Write("  - '!ga' alias (Create):                  ");
            cph.Args["rawInput"] = "!ga profile create AliasTest";
            cph.Args["isBroadcaster"] = true;
            cph.Args["user"] = "Broadcaster";
            // Ensure clean state
            if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasTest"))
                await m.Loader.DeleteProfileAsync(adapter, "AliasTest");

            await m.ProcessTrigger(adapter);

            if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasTest"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL");

            // 2. !ga p alias (Delete)
            Console.Write("  - '!ga p' alias (Delete):                ");
            cph.Args["rawInput"] = "!ga p delete AliasTest confirm";
            await m.ProcessTrigger(adapter);

            if (!m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasTest"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL");

            // 3. !GIVEAWAY case insensitivity
            Console.Write("  - '!GIVEAWAY' case checking:             ");
            cph.Args["rawInput"] = "!GIVEAWAY profile create CaseTest";
            await m.ProcessTrigger(adapter);
            if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("CaseTest"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL");

            // Cleanup
            cph.Args["rawInput"] = "!ga p delete CaseTest confirm";
            await m.ProcessTrigger(adapter);
        }

        private static async Task Test_ExternalBotIntegration(GiveawayManager m, MockCPH cph)
        {
            Console.WriteLine("\n[TEST] External Bot Integration:");
            var adapter = new CPHAdapter(cph);

            // Setup: Allow "MockBot" in Main profile
            var config = m.Loader.GetConfig(adapter);
            if (!config.Profiles.ContainsKey("Main")) await m.Loader.CreateProfileAsync(adapter, "Main");

            config.Profiles["Main"].AllowedExternalBots = new List<string> { "MockBot" };
            config.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
            {
                new BotListenerRule { Pattern = "^!join$", Action = "Enter" }
            };
            m.Loader.WriteConfigText(adapter, JsonConvert.SerializeObject(config));
            m.Loader.InvalidateCache();
            m.Initialize(adapter); // Reload

            // 1. Simulate Bot Message (Blocked initially)
            Console.Write("  - External Bot (Blocked if not matches): ");
            cph.Args.Clear();
            cph.Args["isBot"] = true;
            cph.Args["user"] = "RandomBot";
            cph.Args["message"] = "!join";
            cph.Args["rawInput"] = "!join"; // ProcessTrigger looks at message for external listeners usually? 
                                            // Wait, ProcessTrigger uses rawInput for commands, but Loop logic checks Args["message"]
                                            // Let's ensure consistency

            await m.ProcessTrigger(adapter);
            // Should NOT have entry
            if (!m.States["Main"].Entries.ContainsKey("RandomBot"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL");

            // 2. Simulate Allowed Bot Message
            Console.Write("  - External Bot (Allowed & Triggered):    ");
            cph.Args.Clear();
            cph.Args["isBot"] = true;
            cph.Args["user"] = "MockBot"; // Matches Allowed
            cph.Args["userId"] = "mb1";
            cph.Args["message"] = "!join"; // Matches Pattern
                                           // Note: External bot logic in HandleBotMessage relies on LoopDetector flagging it

            m.States["Main"].IsActive = true;
            await m.ProcessTrigger(adapter);

            if (m.States["Main"].Entries.ContainsKey("mb1") || m.States["Main"].Entries.Values.Any(e => e.UserName == "MockBot"))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Entries: {m.States["Main"].Entries.Count})");
        }

        private static async Task Test_WheelIntegration_Failures()
        {
            Console.WriteLine("\n[TEST] Wheel Integration Failures:");
            Console.Write("  - API Timeout/Error Handling:            ");

            // Mock CPH
            var cph = new MockCPH();
            cph.SetGlobalVar("WheelOfNamesApiKey", "test-key", true);

            // Fake Handler: Timeout
            var fakeHandler = new FakeHttpMessageHandler((req) =>
            {
                throw new TaskCanceledException("Timeout simulated");
            });

            var client = new WheelOfNamesClient(fakeHandler);
            // Should not crash, returns null/empty
            string url = await client.CreateWheel(new CPHAdapter(cph), new List<string> { "u1" }, "key", new WheelConfig());

            if (string.IsNullOrEmpty(url))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Got {url} instead of null)");
        }

        private static async Task Test_RunMode_Mirror_Conflict(GiveawayManager m, MockCPH cph)
        {
            Console.WriteLine("\n[TEST] RunMode Mirror Config Conflict:");
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
            m.Loader.InvalidateCache(); // Force reload
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

        private static async Task Test_Concurrency_EntryVsDraw(GiveawayManager m, MockCPH cph)
        {
            Console.WriteLine("\n[TEST] Concurrency Entry vs Draw:");
            Console.Write("  - Parallel Execution:                    ");

            var adapter = new CPHAdapter(cph);

            // Setup keys
            if (!m.Loader.GetConfig(adapter).Profiles.ContainsKey("ConcurrencyProfile"))
                await m.Loader.CreateProfileAsync(adapter, "ConcurrencyProfile");

            // Reset State
            m.States["ConcurrencyProfile"].IsActive = true;
            m.States["ConcurrencyProfile"].Entries.Clear();

            // Prepare tasks
            var tasks = new List<Task>();
            int entryCount = 50;

            // Task to flood entries
            tasks.Add(Task.Run(async () =>
            {
                for (int i = 0; i < entryCount; i++)
                {
                    var localCph = new MockCPH();
                    localCph.Args["command"] = "!enter";
                    localCph.Args["userId"] = $"user_{i}";
                    localCph.Args["user"] = $"User_{i}";
                    // We must share the SAME manager instance 'm'
                    await m.ProcessTrigger(new CPHAdapter(localCph));
                }
            }));

            // Task to Draw midway
            tasks.Add(Task.Run(async () =>
            {
                await Task.Delay(20); // Small delay to let some entries in
                var localCph = new MockCPH();
                localCph.Args["command"] = "!draw";
                localCph.Args["isBroadcaster"] = true;
                localCph.Args["rawInput"] = "!draw";
                await m.ProcessTrigger(new CPHAdapter(localCph));
            }));

            try
            {
                await Task.WhenAll(tasks);

                // Verification
                int count = m.States["ConcurrencyProfile"].Entries.Count;
                if (count > 0)
                    Console.WriteLine($"PASS (Entries: {count})");
                else
                    Console.WriteLine("FAIL (Zero entries processed)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL (Exception: {ex.Message})");
            }
        }
    }
}
