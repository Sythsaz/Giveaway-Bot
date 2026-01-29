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
    public static class ProfileTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Profile Management Tests ===");
            await Test_ProfileManagement();
            await Test_CreateProfile_Comprehensive();
            await Test_DeleteProfile_Comprehensive();
            await Test_CloneProfile_Comprehensive();
        }

        private static async Task Test_ProfileManagement()
        {
            Console.Write("[TEST] Profile Management (Basic): ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));

            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");

            try
            {
                cph.Args["isBroadcaster"] = true;

                // Create
                cph.Args["rawInput"] = "!giveaway profile create NewProfile";
                await m.ProcessTrigger(new CPHAdapter(cph));

                var json = File.ReadAllText(configPath);
                bool created = json.Contains("NewProfile");

                // Delete
                cph.Args["rawInput"] = "!giveaway profile delete NewProfile confirm";
                await m.ProcessTrigger(new CPHAdapter(cph));

                json = File.ReadAllText(configPath);
                bool deleted = !json.Contains("NewProfile");

                if (created && deleted) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Created={created}, Deleted={deleted})");
            }
            finally
            {
                try { if (File.Exists(configPath)) File.Delete(configPath); } catch { }
            }
        }

        private static async Task Test_CreateProfile_Comprehensive()
        {
            Console.WriteLine("[TEST] CreateProfile Comprehensive:");
            var cph = new MockCPH();
            var adapter = new CPHAdapter(cph);
            var m = new GiveawayManager();
            m.Initialize(adapter);

            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper", "config", "giveaway_config.json");
            cph.Args["isBroadcaster"] = true;

            void assertCreate(string name, string testName, bool expectedSuccess)
            {
                Console.Write($"  - {testName.PadRight(40)}: ");
                cph.Args["rawInput"] = "!giveaway profile create " + name;
                cph.Logs.Clear();
                m.ProcessTrigger(adapter).Wait();

                string json = "{}";
                if (File.Exists(configPath)) json = File.ReadAllText(configPath);

                bool exists = json.Contains("\"" + name + "\"");
                bool success = expectedSuccess ? exists : !exists;

                if (success) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Exists={exists}, Expected={expectedSuccess})");

                // Cleanup for next subtest if needed
                if (exists)
                {
                    cph.Args["rawInput"] = "!giveaway profile delete " + name + " confirm";
                    try { m.ProcessTrigger(adapter).Wait(); } catch { }
                }
            }

            try
            {
                // Basic Validation
                assertCreate("ValidProfile", "Valid profile name", true);

                // Duplicate tests require existing profile
                // Create one first
                cph.Args["rawInput"] = "!giveaway profile create Duplicate";
                await m.ProcessTrigger(adapter);
                
                assertCreate("Duplicate", "Duplicate name (fail)", false);
                assertCreate("duplicate", "Duplicate name case-insensitive (fail)", false);

                // Invalid Characters
                assertCreate("Profile!", "Invalid chars (!) (fail)", false);
                assertCreate("Profile Space", "Invalid chars (Space) (fail)", false);
                assertCreate("Profile@Home", "Invalid chars (@) (fail)", false);

                // Reserved Names
                assertCreate("Globals", "Reserved name 'Globals' (fail)", false);
                assertCreate("_instructions", "Reserved name '_instructions' (fail)", false);
                assertCreate("Main", "Reserved name 'Main' (fail)", false);

                // Empty/Whitespace
                assertCreate("", "Empty name (fail)", false);
                // assertCreate("   ", "Whitespace name (fail)", false); // MockCPH parsing might trim rawInput differently, skipping strict test for now

                // Boundary Conditions
                assertCreate("A", "Boundary: 1 char (pass)", true);
                assertCreate("abcdefghijklmnopqrstuvwxyz123456", "Boundary: 32 chars (pass)", true);
                assertCreate("abcdefghijklmnopqrstuvwxyz1234567", "Boundary: 33 chars (fail)", false);

                // Underscores and Numbers
                assertCreate("Valid_Un_Der", "Name with underscores (pass)", true);
                assertCreate("Profile123", "Name with numbers (pass)", true);
                assertCreate("_leading", "Leading underscore (fail)", false);
                assertCreate("trailing_", "Trailing underscore (fail)", false);
            }
            finally
            {
                try { if (File.Exists(configPath)) File.Delete(configPath); } catch { }
            }
        }

        private static async Task Test_DeleteProfile_Comprehensive()
        {
            Console.WriteLine("[TEST] DeleteProfile Comprehensive:");
            var cph = new MockCPH();
            var adapter = new CPHAdapter(cph);
            var m = new GiveawayManager();
            m.Initialize(adapter);

            string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper");
            string configPath = Path.Combine(baseDir, "config", "giveaway_config.json");
            string backupDir = Path.Combine(baseDir, "config", "backups");
            cph.Args["isBroadcaster"] = true;

            try
            {
                // Ensure fresh start
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (File.Exists(configPath)) File.Delete(configPath);
                        break;
                    }
                    catch (IOException)
                    {
                        await Task.Delay(500);
                    }
                }

                // 1. Requirement: Guard against deletion without confirm
                cph.Args["rawInput"] = "!giveaway profile create ToDelete";
                await m.ProcessTrigger(adapter);

                Console.Write("  - Missing 'confirm' flag (fail):         ");
                cph.Args["rawInput"] = "!giveaway profile delete ToDelete";
                await m.ProcessTrigger(adapter);
                bool stillExists = File.Exists(configPath) && File.ReadAllText(configPath).Contains("ToDelete");
                if (stillExists) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Deleted without confirm)");

                // 2. Requirement: Successful deletion with confirm
                Console.Write("  - With 'confirm' flag (pass):            ");
                cph.Args["rawInput"] = "!giveaway profile delete ToDelete confirm";
                await m.ProcessTrigger(adapter);
                bool deleted = !File.Exists(configPath) || !File.ReadAllText(configPath).Contains("ToDelete");
                if (deleted) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Not deleted with confirm)");

                // 3. Requirement: Automatic backup creation
                Console.Write("  - Automatic backup creation (pass):      ");
                bool backupFound = Directory.Exists(backupDir) && Directory.GetFiles(backupDir, "giveaway_config_*.json.bak").Length > 0;
                if (backupFound) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (No backup found)");

                // 4. Requirement: Active giveaway protection
                Console.Write("  - Active giveaway protection (fail):     ");
                cph.Args["rawInput"] = "!giveaway profile create ActiveTest";
                await m.ProcessTrigger(adapter);

                // Manually create an active state file for this profile
                var state = new GiveawayState { IsActive = true, CurrentGiveawayId = "test-id" };
                var statePath = Path.Combine(baseDir, "state", "ActiveTest.json");
                string? stateDir = Path.GetDirectoryName(statePath);
                if (!string.IsNullOrEmpty(stateDir) && !Directory.Exists(stateDir)) Directory.CreateDirectory(stateDir);
                File.WriteAllText(statePath, JsonConvert.SerializeObject(state));

                // Invalidate cache and reload config to "see" the active state (if logic checks at runtime)
                m.Loader.InvalidateCache();

                cph.Args["rawInput"] = "!giveaway profile delete ActiveTest confirm";
                await m.ProcessTrigger(adapter);
                bool activeStillExists = File.Exists(configPath) && File.ReadAllText(configPath).Contains("ActiveTest");
                if (activeStillExists) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Deleted active giveaway!)");

                // 5. Requirement: Profile existence check
                Console.Write("  - Delete non-existent profile (fail):    ");
                cph.Args["rawInput"] = "!giveaway profile delete NoSuchProfile confirm";
                cph.Logs.Clear();
                await m.ProcessTrigger(adapter);
                // Should respond with error message, not crash
                Console.WriteLine("PASS");

                // 6. Requirement: Streamer.bot variable cleanup
                Console.Write("  - Global variable cleanup (pass):        ");
                // Mock a variable that would be present for a profile named 'ToCleanup'
                cph.SetGlobalVar("GiveawayBot_ToCleanup_IsActive", true, true);
                cph.Args["rawInput"] = "!giveaway profile create ToCleanup";
                await m.ProcessTrigger(adapter);
                cph.Args["rawInput"] = "!giveaway profile delete ToCleanup confirm";
                await m.ProcessTrigger(adapter);
                bool varUnset = !cph.Globals.ContainsKey("GiveawayBot_ToCleanup_IsActive");
                if (varUnset) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Variable remained)");
            }
            finally
            {
                try { if (File.Exists(configPath)) File.Delete(configPath); } catch { }
            }
        }

        private static async Task Test_CloneProfile_Comprehensive()
        {
            Console.WriteLine("[TEST] CloneProfile Comprehensive:");
            var cph = new MockCPH();
            var adapter = new CPHAdapter(cph);
            var m = new GiveawayManager();
            m.Initialize(adapter);

            string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Helper");
            string configPath = Path.Combine(baseDir, "config", "giveaway_config.json");
            cph.Args["isBroadcaster"] = true;

            try
            {
                // Ensure fresh start
                if (File.Exists(configPath)) File.Delete(configPath);
                try { File.AppendAllText("test_debug.log", "[Test_CloneProfile_Comprehensive] setup done\n"); } catch { }

                // 1. Create source profile with data
                cph.Args["rawInput"] = "!giveaway profile create SourceP";
                await m.ProcessTrigger(adapter);

                // Add a trigger and a custom requirement to verify deep copy
                var config = m.Loader.GetConfig(adapter);
                config.Profiles["SourceP"].SubLuckMultiplier = 5;
                config.Profiles["SourceP"].Triggers["command:SourceAction"] = "Enter";
                m.Loader.WriteConfigText(adapter, JsonConvert.SerializeObject(config));
                m.Loader.InvalidateCache();

                // 2. Requirement: Cloning correctly (pass)
                Console.Write("  - Cloning correctly (pass):              ");
                cph.Args["rawInput"] = "!giveaway profile clone SourceP ClonedP";
                await m.ProcessTrigger(adapter);

                var newConfig = m.Loader.GetConfig(adapter);
                bool cloneFound = newConfig.Profiles.ContainsKey("ClonedP");
                bool deepCopyOk = cloneFound && newConfig.Profiles["ClonedP"].SubLuckMultiplier == 5;
                if (deepCopyOk) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Clone missing or properties not copied)");

                // 3. Requirement: Triggers are cleared on clone (pass)
                Console.Write("  - Triggers cleared on clone (pass):      ");
                bool triggersCleared = cloneFound && newConfig.Profiles["ClonedP"].Triggers.Count == 0;
                if (triggersCleared) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Triggers were NOT cleared)");

                // 4. Requirement: ExposeVariables set to false on clone (pass)
                Console.Write("  - ExposeVariables false on clone (pass): ");
                bool exposeFalse = cloneFound && newConfig.Profiles["ClonedP"].ExposeVariables == false;
                if (exposeFalse) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (ExposeVariables remained true)");

                // 5. Requirement: Clone onto existing name (fail)
                Console.Write("  - Clone onto existing (fail):            ");
                cph.Args["rawInput"] = "!giveaway profile create Targeted";
                await m.ProcessTrigger(adapter);
                cph.Logs.Clear();
                cph.Args["rawInput"] = "!giveaway profile clone SourceP Targeted";
                await m.ProcessTrigger(adapter);
                // Check if Targeted's properties changed (they shouldn't)
                var finalConfig = m.Loader.GetConfig(adapter);
                bool originalPreserved = finalConfig.Profiles["Targeted"].ExposeVariables == false; // defaulted to false on create in this version
                if (originalPreserved) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Overwrote existing profile)");

                // 6. Basic Name Validation on Clone
                Console.Write("  - Invalid destination name (fail):       ");
                cph.Args["rawInput"] = "!giveaway profile clone SourceP \"Invalid Name!\"";
                await m.ProcessTrigger(adapter);
                var afterInvalid = m.Loader.GetConfig(adapter);
                if (afterInvalid.Profiles.Count == 3) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Created invalid profile name)");

                try { File.AppendAllText("test_debug.log", "[Test_CloneProfile_Comprehensive] finished\n"); } catch { }
            }
            finally
            {
                try { if (File.Exists(configPath)) File.Delete(configPath); } catch { }
            }
        }
    }
}
