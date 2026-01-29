// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class ProfileConfigTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Profile Config & Routing Tests ===");
            await Test_UpdateProfileConfig_Comprehensive();
            await Test_TriggerManagement_Comprehensive();
            await Test_CommandRouting_Parsing_Comprehensive();
            await Test_BatchOperations();
            await Test_ConfigReflection();
            await Test_CommandAliases_Comprehensive();
        }

        private static (GiveawayManager m, MockCPH cph) SetupWithCph()
        {
            var cph = new MockCPH();
            var m = new GiveawayManager();
            // Reset static state for isolation
            GiveawayManager.GlobalConfig = null;
            m.States.Clear();
            var adapter = new CPHAdapter(cph);
            adapter.Logger = cph.Logger;
            m.Initialize(adapter);
            return (m, cph);
        }
        
        private static async Task Test_BatchOperations()
        {
            Console.Write("\n[TEST] Batch Operations (*): ");
            var (m, cph) = SetupWithCph();
            cph.Args["isBroadcaster"] = true;
            cph.Args["user"] = "Tester";

            // 1. Create Batch1, Batch2
            cph.Args["rawInput"] = "!giveaway profile create Batch1";
            await m.ProcessTrigger(new CPHAdapter(cph));
            cph.Args["rawInput"] = "!giveaway profile create Batch2";
            await m.ProcessTrigger(new CPHAdapter(cph));

            // 2. Batch Config
            cph.Args["rawInput"] = "!giveaway profile config * SubLuckMultiplier=99";
            await m.ProcessTrigger(new CPHAdapter(cph));

            var config = GiveawayManager.GlobalConfig;
            if (config.Profiles["Batch1"].SubLuckMultiplier != 99 || config.Profiles["Batch2"].SubLuckMultiplier != 99)
            {
                Console.WriteLine("FAIL (Batch Config failed)"); return;
            }

            // 3. Batch Start
            cph.Args["rawInput"] = "!giveaway profile start *";
            await m.ProcessTrigger(new CPHAdapter(cph));

            if (!m.States["Batch1"].IsActive || !m.States["Batch2"].IsActive)
            {
                Console.WriteLine("FAIL (Batch Start failed)"); return;
            }

            // 4. Batch End
            cph.Args["rawInput"] = "!giveaway profile end all"; // Test 'all' alias
            await m.ProcessTrigger(new CPHAdapter(cph));

            if (m.States["Batch1"].IsActive || m.States["Batch2"].IsActive)
            {
                Console.WriteLine("FAIL (Batch End failed)"); return;
            }

            Console.WriteLine("PASS");
        }

        private static async Task Test_ConfigReflection()
        {
            Console.WriteLine("\n[TEST] Config Reflection (Phase 9+):");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create P9Reflect";
                await m.ProcessTrigger(adapter);

                // 3. New Config Key (Reflection Verification)
                Console.Write("  - Update generic new key (pass):         ");
                // 'DumpEntriesOnEntry' was NOT handled in old logic specific switch, now should be handled by Reflection
                var (s1, e1) = await m.Loader.UpdateProfileConfigAsync(adapter, "P9Reflect", "DumpEntriesOnEntry", "true");
                var cfg2 = m.Loader.GetConfig(adapter);
                if (cfg2.Profiles["P9Reflect"].DumpEntriesOnEntry == true) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL: {e1}");

                Console.Write("  - Update complex SubLuck (pass):         ");
                var (s2, e2) = await m.Loader.UpdateProfileConfigAsync(adapter, "P9Reflect", "SubLuckMultiplier", "99");
                if (s2 && m.Loader.GetConfig(adapter).Profiles["P9Reflect"].SubLuckMultiplier == 99) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL: {e2}");
                
                // Cleanup
                cph.Args["rawInput"] = "!giveaway profile delete P9Reflect confirm";
                await m.ProcessTrigger(adapter);
            }
            finally { }
        }
        
        private static GiveawayProfileConfig? GetProfile(GiveawayBotConfig config, string name)
        {
            if (config == null || config.Profiles == null) return null;
            foreach (var kv in config.Profiles)
            {
                if (string.Equals(kv.Key, name, StringComparison.OrdinalIgnoreCase)) return kv.Value;
            }
            return null;
        }

        private static async Task Test_UpdateProfileConfig_Comprehensive()
        {
            Console.WriteLine("UpdateProfileConfig Comprehensive");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                // 1. Setup profile (Ensure isBroadcaster is set for SUBCOMMANDS too)
                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create TestU";
                await m.ProcessTrigger(adapter);

                // 2. Test MaxEntriesPerMinute (Int, >0)
                Console.Write("  - MaxEntriesPerMinute (valid):           ");
                var (s1, e1) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "MaxEntriesPerMinute", "10");
                if (s1 && m.Loader.GetConfig(adapter).Profiles["TestU"].MaxEntriesPerMinute == 10) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: " + e1);

                Console.Write("  - MaxEntriesPerMinute (invalid):         ");
                var (s2, e2) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "MaxEntriesPerMinute", "-1");
                if (!s2) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Accepted -1)");

                // 3. Test EnableWheel (Bool)
                Console.Write("  - EnableWheel (true):                    ");
                var (s3, e3) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "EnableWheel", "true");
                if (s3 && m.Loader.GetConfig(adapter).Profiles["TestU"].EnableWheel) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                // 4. Test nested WheelSettings (Wheel.SpinTime)
                Console.Write("  - Nested Wheel.SpinTime (valid):         ");
                var (s4, e4) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "Wheel.SpinTime", "15");
                if (s4 && m.Loader.GetConfig(adapter).Profiles["TestU"].WheelSettings.SpinTime == 15) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                // 5. Test UsernamePattern (Regex validation)
                Console.Write("  - UsernamePattern (valid regex):         ");
                var (s5, e5) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "UsernamePattern", "^[A-Z]+$");
                if (s5 && m.Loader.GetConfig(adapter).Profiles["TestU"].UsernamePattern == "^[A-Z]+$") Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");

                Console.Write("  - UsernamePattern (invalid regex):       ");
                var (s6, e6) = await m.Loader.UpdateProfileConfigAsync(adapter, "TestU", "UsernamePattern", "[");
                if (!s6) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Accepted invalid regex)");

                // 6. Test unauthorized or unknown profile
                Console.Write("  - Unknown profile (fail):                ");
                var (s7, e7) = await m.Loader.UpdateProfileConfigAsync(adapter, "NonExistent", "MaxEntriesPerMinute", "5");
                if (!s7 && e7.Contains("not found")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL");
            }
            finally { }
        }

        private static async Task Test_TriggerManagement_Comprehensive()
        {
            Console.WriteLine("Trigger Management Comprehensive");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                // 1. Setup profile
                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create TestT";
                await m.ProcessTrigger(adapter);

                // 2. Add Trigger (valid)
                Console.Write("  - Add Trigger (valid):                   ");
                var (s8, e8) = await m.Loader.AddProfileTriggerAsync(adapter, "TestT", "command:!win", "Winner");
                var config = m.Loader.GetConfig(adapter);
                if (s8 && config.Profiles.TryGetValue("TestT", out var profile) && profile.Triggers.TryGetValue("command:!win", out var action) && action == "Winner") Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: " + (e8 ?? "Profile missing or key missing"));

                // 3. Add Trigger (invalid action)
                Console.Write("  - Add Trigger (invalid action):          ");
                var (s9, e9) = await m.Loader.AddProfileTriggerAsync(adapter, "TestT", "reward:abc", "InvalidAction");
                if (!s9) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Accepted invalid action)");

                // 4. Remove Trigger
                Console.Write("  - Remove Trigger:                        ");
                var (s10, e10) = await m.Loader.RemoveProfileTriggerAsync(adapter, "TestT", "command:!win");
                config = m.Loader.GetConfig(adapter);
                if (s10 && config.Profiles.TryGetValue("TestT", out profile) && !profile.Triggers.ContainsKey("command:!win")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: " + (e10 ?? "Profile missing or key still present"));

                // 5. Remove Trigger (not found)
                Console.Write("  - Remove Trigger (missing):              ");
                var (s11, e11) = await m.Loader.RemoveProfileTriggerAsync(adapter, "TestT", "command:nonexistent");
                if (!s11 && e11 != null && e11.Contains("not found")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: " + (e11 ?? "No error returned"));
            }
            finally { }
        }

        private static async Task Test_CommandRouting_Parsing_Comprehensive()
        {
            Console.WriteLine("Command Routing & Parsing Comprehensive");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);

            try
            {
                cph.Args["isBroadcaster"] = true;
                cph.Args["isBot"] = false;
                cph.Args["user"] = "Broadcaster";
                cph.Args["userId"] = "1";

                // 1. Test List command (initial - should have Main)
                Console.Write("  - List command (initial):                ");
                cph.Args["rawInput"] = "!giveaway profile list";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter);
                if (cph.Logs.Any(l => l.Contains("Main"))) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Main profile not listed)");

                // 2. Test Create (with leading/trailing spaces)
                Console.Write("  - Create (spaces in args):               ");
                cph.Args["rawInput"] = "  !giveaway   profile   create   SpacedProfile  ";
                await m.ProcessTrigger(adapter);
                if (m.Loader.GetConfig(adapter).Profiles.ContainsKey("SpacedProfile")) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Trimming failed or Unauthorized)");

                // 3. Test Config routing
                Console.Write("  - Config routing (EnableWheel):          ");
                cph.Args["rawInput"] = "!giveaway profile config SpacedProfile EnableWheel true";
                // Verification: Config updated
                await m.ProcessTrigger(adapter);
                var configAfterConfig = m.Loader.GetConfig(adapter);
                var spacedProf = GetProfile(configAfterConfig, "SpacedProfile");
                if (spacedProf != null && spacedProf.EnableWheel) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: SpacedProfile not found or EnableWheel false");

                // Trigger management
                Console.Write("  - Trigger add (message check):           ");
                cph.Args["rawInput"] = "!giveaway profile trigger SpacedProfile add command:!test MyAction";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter); // TRIGGER ADD
                if (cph.Logs.Any(l => l.Contains("Added trigger"))) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: Trigger add message missing");

                Console.Write("  - Trigger add (verification):            ");
                // Verification: Trigger exists
                var configAfterAdd = m.Loader.GetConfig(adapter);
                var spacedProfAfterAdd = GetProfile(configAfterAdd, "SpacedProfile");
                if (spacedProfAfterAdd != null)
                {
                    bool triggerFound = false;
                    foreach (var tk in spacedProfAfterAdd.Triggers.Keys)
                    {
                        if (tk.Equals("command:!test", StringComparison.OrdinalIgnoreCase)) { triggerFound = true; break; }
                    }
                    if (triggerFound) Console.WriteLine("PASS");
                    else Console.WriteLine("FAIL: Trigger 'command:!test' not found");
                }
                else Console.WriteLine("FAIL: SpacedProfile missing after trigger add");

                Console.Write("  - Trigger removal (message check):       ");
                cph.Args["rawInput"] = "!giveaway profile trigger SpacedProfile remove command:!test";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter); // TRIGGER REMOVE
                if (cph.Logs.Any(l => l.Contains("Removed trigger"))) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL: Trigger remove message missing");

                Console.Write("  - Trigger removal (verification):        ");
                // Verification: Trigger removed
                var configAfterRemove = m.Loader.GetConfig(adapter);
                var spacedProfAfterRemove = GetProfile(configAfterRemove, "SpacedProfile");
                if (spacedProfAfterRemove != null)
                {
                    bool triggerFound = false;
                    foreach (var tk in spacedProfAfterRemove.Triggers.Keys)
                    {
                        if (tk.Equals("command:!test", StringComparison.OrdinalIgnoreCase)) { triggerFound = true; break; }
                    }
                    if (!triggerFound) Console.WriteLine("PASS");
                    else Console.WriteLine("FAIL: Trigger 'command:!test' still exists");
                }
                else Console.WriteLine("FAIL: SpacedProfile missing after trigger remove");

                // 5. Test Case Insensitivity in Subcommands
                Console.Write("  - Subcommand Case (LIST):                ");
                cph.Args["rawInput"] = "!giveaway profile LIST";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter);
                if (cph.Logs.Any(l => l.Contains("Profiles:"))) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Subcommand case sensitive)");

                // 6. Test Unknown Subcommand
                Console.Write("  - Unknown subcommand:                    ");
                cph.Args["rawInput"] = "!giveaway profile jump";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter);
                if (cph.Logs.Any(l => l.Contains("Usage: !giveaway profile"))) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (No usage guide)");
            }
            finally { }
        }

        private static async Task Test_CommandAliases_Comprehensive()
        {
            Console.WriteLine("\n[TEST] Command Aliases:");
            var (m, cph) = SetupWithCph();
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
    }
}
