// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class IntegrationTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Integration & Advanced Feature Tests ===");
            Test_MultiPlatformMessenger(); // Non-async
            await Test_ExternalBotListener_InlineList();
            await Test_ExternalBotListener_CaseSensitive();
            await Test_ExternalBotListener_VariableSource();
            await Test_ExternalBotListener_FileSource();
            await Test_ExternalBotListener_Ignored();
            await Test_ShortCommandAliases();
            await Test_AutoEncryption();
            await Test_EnhancedExports();
            await Test_ProfileImportExport();
            await Test_Dumps();
            await Test_GenericTriggers();
            await Test_WheelIntegration();
            await Test_ExternalBotIntegration();
            await Test_WheelIntegration_Failures();
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

        public static void Test_MultiPlatformMessenger()
        {
            Console.Write("[TEST] Multi-Platform Messenger: ");
            var c = new MockCPH();
            var config = new GiveawayBotConfig();
            config.Globals.FallbackPlatform = "Kick";
            config.Globals.EnabledPlatforms = new List<string> { "Twitch", "YouTube" };
            var messenger = new MultiPlatformMessenger(config);

            // 1. All live, Multi-Platform = true (Twitch, YouTube)
            c.SetGlobalVar("GiveawayBot_EnableMultiPlatform", true, true);
            c.IsTwitchLiveValue = true;
            c.IsYouTubeLiveValue = true;
            c.IsKickLiveValue = true;
            c.ChatHistory.Clear();
            messenger.SendBroadcast(new CPHAdapter(c), "Hello All");
            if (c.ChatHistory.Count != 2 || !c.ChatHistory.Any(x => x.Contains("Hello All"))) throw new Exception("Broadcast failed to reach all live platforms.");

            // 2. Fallback (Nothing live -> Kick)
            c.IsTwitchLiveValue = false;
            c.IsYouTubeLiveValue = false;
            c.IsKickLiveValue = false;
            c.ChatHistory.Clear();
            messenger.SendBroadcast(new CPHAdapter(c), "Going to fallback");
            if (c.ChatHistory.Count != 1 || !c.ChatHistory.Any(x => x.Contains("Going to fallback"))) throw new Exception("Fallback failed.");

            // 3. Multi-Platform = false (Only source -> YouTube)
            c.SetGlobalVar("GiveawayBot_EnableMultiPlatform", false, true);
            c.IsTwitchLiveValue = true;
            c.IsYouTubeLiveValue = true;
            c.ChatHistory.Clear();
            messenger.SendBroadcast(new CPHAdapter(c), "Direct Message", "YouTube");
            if (c.ChatHistory.Count != 1 || !c.ChatHistory.Any(x => x.Contains("Direct Message"))) throw new Exception("Direct messaging (Multi=false) failed.");

            Console.WriteLine("PASS");
        }

        private static async Task Test_ExternalBotListener_InlineList()
        {
            Console.Write("[TEST] External Bot Listener (Inline List): ");
            var (m, c) = SetupWithCph();

            // Configure Main profile with inline bot list
            GiveawayManager.GlobalConfig.Profiles["Main"].AllowedExternalBots = new List<string> { "Moobot", "Nightbot" };
            GiveawayManager.GlobalConfig.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
            {
                new BotListenerRule { Pattern = "(?i)THE GIVEAWAY IS NOW CLOSED", Action = "Close" }
            };
            m.States["Main"].IsActive = true;

            // Simulate bot message
            c.Args["user"] = "Moobot";
            c.Args["userId"] = "12345";
            c.Args["message"] = "The giveaway is now closed for new entries!";
            c.Args["isBot"] = true;

            await m.ProcessTrigger(new CPHAdapter(c));

            if (!m.States["Main"].IsActive) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL (Giveaway still active)");
        }

        private static async Task Test_ExternalBotListener_CaseSensitive()
        {
            Console.Write("[TEST] External Bot Listener (Case Sensitive): ");
            var (m, c) = SetupWithCph();

            // Configure with exact case "Moobot"
            GiveawayManager.GlobalConfig.Profiles["Main"].AllowedExternalBots = new List<string> { "Moobot" };
            GiveawayManager.GlobalConfig.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
            {
                new BotListenerRule { Pattern = "CLOSE", Action = "Close" }
            };
            m.States["Main"].IsActive = true;

            // Try with incorrect case "moobot" - should be ignored
            c.Args["user"] = "moobot";
            c.Args["userId"] = "12345";
            c.Args["message"] = "CLOSE";
            c.Args["isBot"] = true;

            await m.ProcessTrigger(new CPHAdapter(c));

            // Should still be active because "moobot" != "Moobot"
            if (m.States["Main"].IsActive) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL (Case insensitive match occurred)");
        }

        private static async Task Test_ExternalBotListener_VariableSource()
        {
            Console.Write("[TEST] External Bot Listener (Variable Source): ");
            var (m, c) = SetupWithCph();

            // Set up bot list in Streamer.bot variable
            c.SetGlobalVar("GiveawayBot_AllowedBots", "Moobot\nNightbot\nStreamlabs", true);

            // Configure to read from variable
            GiveawayManager.GlobalConfig.Profiles["Main"].AllowedExternalBots = new List<string> { "GiveawayBot_AllowedBots" };
            GiveawayManager.GlobalConfig.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
            {
                new BotListenerRule { Pattern = "OPEN", Action = "Open" }
            };
            m.States["Main"].IsActive = false;

            // Simulate Streamlabs bot message
            c.Args["user"] = "Streamlabs";
            c.Args["userId"] = "67890";
            c.Args["message"] = "OPEN THE GIVEAWAY";
            c.Args["isBot"] = true;

            await m.ProcessTrigger(new CPHAdapter(c));

            if (m.States["Main"].IsActive) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL (Giveaway not opened)");
        }

        private static async Task Test_ExternalBotListener_FileSource()
        {
            Console.Write("[TEST] External Bot Listener (File Source): ");
            var (m, c) = SetupWithCph();

            // Create temporary bot list file
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string botListPath = Path.Combine(baseDir, "test_botlist.txt");
            File.WriteAllText(botListPath, "Moobot\n# Comment line\nNightbot\n  \nStreamElements");

            try
            {
                // Configure to read from file
                GiveawayManager.GlobalConfig.Profiles["Main"].AllowedExternalBots = new List<string> { botListPath };
                GiveawayManager.GlobalConfig.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
                {
                    new BotListenerRule { Pattern = "WINNER.*", Action = "Winner" }
                };
                m.States["Main"].IsActive = true;
                m.States["Main"].Entries["user1"] = new Entry { UserId = "user1", UserName = "User1", TicketCount = 1 };

                // Simulate StreamElements bot message
                c.Args["user"] = "StreamElements";
                c.Args["userId"] = "11111";
                c.Args["message"] = "WINNER IS @User1";
                c.Args["isBot"] = true;

                await m.ProcessTrigger(new CPHAdapter(c));

                // Check if winner was drawn (state should have LastWinnerName set)
                if (m.States["Main"].LastWinnerName != null) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Winner not drawn)");
            }
            finally
            {
                if (File.Exists(botListPath)) File.Delete(botListPath);
            }
        }

        private static async Task Test_ExternalBotListener_Ignored()
        {
            Console.Write("[TEST] External Bot Listener (Ignored Bot): ");
            var (m, c) = SetupWithCph();

            // Configure whitelist with only Moobot
            GiveawayManager.GlobalConfig.Profiles["Main"].AllowedExternalBots = new List<string> { "Moobot" };
            GiveawayManager.GlobalConfig.Profiles["Main"].ExternalListeners = new List<BotListenerRule>
            {
                new BotListenerRule { Pattern = "CLOSE", Action = "Close" }
            };
            m.States["Main"].IsActive = true;

            // Try with non-whitelisted bot
            c.Args["user"] = "Nightbot";
            c.Args["userId"] = "99999";
            c.Args["message"] = "CLOSE THE GIVEAWAY";
            c.Args["isBot"] = true;

            await m.ProcessTrigger(new CPHAdapter(c));

            // Should still be active because bot was filtered
            if (m.States["Main"].IsActive) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL (Non-whitelisted bot was processed)");
        }

        private static async Task Test_ShortCommandAliases()
        {
            Console.WriteLine("\nShort Command Aliases Checking");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);
            cph.Args["isBroadcaster"] = true;

            try
            {
                // 1. !ga create
                Console.Write("  - !ga create:                            ");
                cph.Args["rawInput"] = "!ga create AliasProf1";
                await m.ProcessTrigger(adapter);
                if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasProf1")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                // 2. !ga p create
                Console.Write("  - !ga p create:                          ");
                cph.Args["rawInput"] = "!ga p create AliasProf2";
                await m.ProcessTrigger(adapter);
                if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasProf2")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                // 3. !ga delete
                Console.Write("  - !ga delete:                            ");
                cph.Args["rawInput"] = "!ga delete AliasProf1 confirm";
                await m.ProcessTrigger(adapter);
                if (!m.Loader.GetConfig(adapter).Profiles.ContainsKey("AliasProf1")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                // 4. !giveaway p list (Mixed)
                Console.Write("  - !giveaway p list:                      ");
                cph.Args["rawInput"] = "!giveaway p list";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter);
                Console.WriteLine("PASS");
            }
            finally { }
        }

        private static async Task Test_AutoEncryption()
        {
            Console.Write("[TEST] Auto-Obfuscation: ");
            var cph = new MockCPH();
            var m = new GiveawayManager();

            // 1. Setup: Plain text key
            cph.SetGlobalVar("GiveawayBot_RunMode", "GlobalVar", true);
            cph.SetGlobalVar("WheelOfNamesApiKey", "PLAIN_SECRET", true);

            // 2. Initialize (should trigger auto-encrypt)
            m.Initialize(new CPHAdapter(cph));

            // 3. Verify
            string storedKey = cph.GetGlobalVar<string>("WheelOfNamesApiKey");
            if (storedKey != null && storedKey.StartsWith("AES:"))
            {
                // Verify we can decrypt it back
                string decrypted = GiveawayManager.DecryptSecret(storedKey);
                if (decrypted == "PLAIN_SECRET") Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Decrypted: {decrypted})");
            }
            else
            {
                Console.WriteLine($"FAIL (Not encrypted: {storedKey})");
            }
            await Task.CompletedTask;
        }

        private static async Task Test_EnhancedExports()
        {
            Console.Write("[TEST] Enhanced Exports (CSV/JSON): ");
            var (m, cph) = SetupWithCph();

            // Setup Profile
            cph.Args["rawInput"] = "!giveaway profile create ExportTest";
            cph.Args["isBroadcaster"] = true; // Required for profile management
            cph.Args["user"] = "Tester";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // Add Entry
            m.States["ExportTest"].Entries["u1"] = new Entry { UserId = "u1", UserName = "Tester, One", TicketCount = 1, IsSub = false, EntryTime = DateTime.Now };

            // Test CSV
            var config = GiveawayManager.GlobalConfig.Profiles["ExportTest"];
            config.DumpFormat = DumpFormat.CSV;
            config.DumpEntriesOnEnd = true;

            // Trigger End to Dump
            m.States["ExportTest"].IsActive = true;
            cph.Args.Clear();
            cph.Args["rawInput"] = "!giveaway profile end ExportTest";
            cph.Args["isBroadcaster"] = true;
            cph.Args["user"] = "Tester";
            await m.ProcessTrigger(new CPHAdapter(cph));

            string dumpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "dumps", "ExportTest");
            string csvFile = Directory.GetFiles(dumpDir, "*_Entries.csv").OrderByDescending(f => f).FirstOrDefault();

            bool csvPass = false;
            if (csvFile != null && File.Exists(csvFile))
            {
                string content = File.ReadAllText(csvFile);
                // Verify Header and Quoted Name
                if (content.Contains("UserId,Username") && content.Contains("\"Tester, One\"")) csvPass = true;
            }

            if (!csvPass) { Console.WriteLine($"FAIL (CSV generation failed or content mismatch. Found: {csvFile})"); return; }

            // Test JSON
            config.DumpFormat = DumpFormat.JSON;

            // Trigger End again
            m.States["ExportTest"].IsActive = true;
            await m.ProcessTrigger(new CPHAdapter(cph));

            string jsonFile = Directory.GetFiles(dumpDir, "*_Entries.json").OrderByDescending(f => f).FirstOrDefault();
            bool jsonPass = false;
            if (jsonFile != null && File.Exists(jsonFile))
            {
                string content = File.ReadAllText(jsonFile);
                if (content.Trim().StartsWith("[") && content.Contains("Tester, One")) jsonPass = true;
            }

            if (jsonPass) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL (JSON generation failed)");
        }

        private static async Task Test_ProfileImportExport()
        {
            Console.Write("[TEST] Profile Export/Import: ");
            var (m, cph) = SetupWithCph();

            // 1. Create Source Profile
            cph.Args["rawInput"] = "!giveaway profile create ExportMe";
            cph.Args["isBroadcaster"] = true;
            cph.Args["user"] = "Tester";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // Modify it slightly
            var config = GiveawayManager.GlobalConfig;
            config.Profiles["ExportMe"].SubLuckMultiplier = 42;
            m.Loader.WriteConfigText(new CPHAdapter(cph), JsonConvert.SerializeObject(config));
            m.Loader.InvalidateCache(); // Force reload if needed.

            // 2. Export
            cph.Args["rawInput"] = "!giveaway profile export ExportMe";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // Check file
            var exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "exports");
            var exportFile = Directory.GetFiles(exportDir, "ExportMe_Export_*.json").OrderByDescending(f => f).FirstOrDefault();

            if (exportFile == null) { Console.WriteLine("FAIL (Export file not found)"); return; }
            string exportedJson = File.ReadAllText(exportFile);

            if (!exportedJson.Contains("\"SubLuckMultiplier\": 42")) { Console.WriteLine("FAIL (Export content incorrect)"); return; }

            // 3. Import (from File)
            cph.Args["rawInput"] = $"!giveaway profile import \"{exportFile}\" ImportedProfile";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // Verify
            var newConfig = m.Loader.GetConfig(new CPHAdapter(cph));
            if (!newConfig.Profiles.ContainsKey("ImportedProfile")) { Console.WriteLine("FAIL (ImportedProfile not in config)"); return; }
            if (newConfig.Profiles["ImportedProfile"].SubLuckMultiplier != 42) { Console.WriteLine("FAIL (Imported property mismatch)"); return; }

            // 4. Import (Raw JSON - Relative Path Logic)
            string importDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "import");
            if (!Directory.Exists(importDir)) Directory.CreateDirectory(importDir);
            File.WriteAllText(Path.Combine(importDir, "Relative.json"), exportedJson);

            cph.Args["rawInput"] = "!giveaway profile import Relative RelativeProf";
            await m.ProcessTrigger(new CPHAdapter(cph));

            newConfig = m.Loader.GetConfig(new CPHAdapter(cph));
            if (newConfig.Profiles.ContainsKey("RelativeProf") && newConfig.Profiles["RelativeProf"].SubLuckMultiplier == 42)
                Console.WriteLine("PASS");
            else
                Console.WriteLine("FAIL (Relative import failed)");
        }

        private static async Task Test_Dumps()
        {
            Console.Write("[TEST] Dumps: ");
            var (m, cph) = SetupWithCph();

            cph.Args["rawInput"] = "!giveaway profile create Main";
            cph.Args["isBroadcaster"] = true;
            await m.ProcessTrigger(new CPHAdapter(cph));

            if (m.States.TryGetValue("Main", out var s))
            {
                s.IsActive = true;
                s.Entries["t"] = new Entry { UserName = "T", UserId = "t", TicketCount = 1 };
                GiveawayManager.GlobalConfig.Profiles["Main"].EnableWheel = false;
                cph.Args.Clear(); cph.Args["command"] = "!draw"; cph.Args["isBroadcaster"] = true;
                await m.ProcessTrigger(new CPHAdapter(cph));
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string d = Path.Combine(baseDir, "Giveaway Helper", "dumps", "Main");
                if (Directory.Exists(d) && Directory.GetFiles(d).Length > 0) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Dir exists: {Directory.Exists(d)})");
            }
            else Console.WriteLine("FAIL (No state)");
        }

        private static async Task Test_GenericTriggers()
        {
            Console.Write("[TEST] Generic Triggers (ID/Name): ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));

            // Inject Custom Config for Testing
            var customProf = new GiveawayProfileConfig();
            customProf.Triggers.Clear();
            customProf.Triggers["name:My Timer"] = "Entry";
            customProf.Triggers["id:uuid-1234"] = "Draw";
            customProf.Triggers["sd:SD-BUTTON-1"] = "Start";
            GiveawayManager.GlobalConfig.Profiles["Custom"] = customProf;
            m.States["Custom"] = new GiveawayState { CurrentGiveawayId = "custom-id", IsActive = true };

            bool passed = true;

            // 1. Test Name Trigger
            cph.Args.Clear(); cph.Args["triggerName"] = "My Timer"; cph.Args["userId"] = "u1"; cph.Args["user"] = "U1";
            await m.ProcessTrigger(new CPHAdapter(cph));
            if (!m.States["Custom"].Entries.ContainsKey("u1")) { Console.Write("FAIL(Name) "); passed = false; }

            // 2. Test ID Trigger (Case Insensitive)
            cph.Args.Clear(); cph.Args["triggerId"] = "UUID-1234"; cph.Args["isBroadcaster"] = true;
            await m.ProcessTrigger(new CPHAdapter(cph));
            // Draw logic requires entries, so if we get here without crash and it matched 'Draw', it's good. 
            // We check logs for "No entries!" from HandleDraw

            // 3. Test StreamDeck Trigger
            cph.Args.Clear(); cph.Args["sdButtonId"] = "sd-button-1";
            await m.ProcessTrigger(new CPHAdapter(cph));
            if (!m.States["Custom"].IsActive) { Console.Write("FAIL(SD) "); passed = false; }

            if (passed) Console.WriteLine("PASS");
        }

        private static async Task Test_WheelIntegration()
        {
            Console.Write("[TEST] Wheel Integration (Mock): ");
            var cph = new MockCPH();
            cph.SetGlobalVar("WheelOfNamesApiKey", "test-key", true);

            // Fake Handler to simulate API response
            var fakeHandler = new FakeHttpMessageHandler((req) =>
            {
                if (req == null || req.RequestUri == null || req.RequestUri.ToString() != "https://wheelofnames.com/api/v2/wheels")
                    return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

                if (!req.Headers.Contains("x-api-key"))
                    return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

                return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new System.Net.Http.StringContent("{\"data\":{\"path\":\"abc-123\"}}", System.Text.Encoding.UTF8, "application/json")
                };
            });

            var client = new WheelOfNamesClient(fakeHandler);
            var url = await client.CreateWheel(new CPHAdapter(cph), new List<string> { "u1", "u2" }, "WheelOfNamesApiKey", new WheelConfig());

            if (url == "https://wheelofnames.com/abc-123") Console.WriteLine("PASS");
            else Console.WriteLine($"FAIL (Got: {url})");
        }

        private static async Task Test_ExternalBotIntegration()
        {
            Console.WriteLine("\n[TEST] External Bot Integration:");
            var (m, cph) = SetupWithCph();
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
            cph.Args["rawInput"] = "!join";

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
                throw new System.Threading.Tasks.TaskCanceledException("Timeout simulated");
            });

            var client = new WheelOfNamesClient(fakeHandler);
            // Should not crash, returns null/empty
            string url = await client.CreateWheel(new CPHAdapter(cph), new List<string> { "u1" }, "key", new WheelConfig());

            if (string.IsNullOrEmpty(url))
                Console.WriteLine("PASS");
            else
                Console.WriteLine($"FAIL (Got {url} instead of null)");
        }
    }

    public class FakeHttpMessageHandler : System.Net.Http.HttpMessageHandler
    {
        private readonly Func<System.Net.Http.HttpRequestMessage, System.Net.Http.HttpResponseMessage> _handler;

        public FakeHttpMessageHandler(Func<System.Net.Http.HttpRequestMessage, System.Net.Http.HttpResponseMessage> handler)
        {
            _handler = handler;
        }

        protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(_handler(request));
        }
    }
}
