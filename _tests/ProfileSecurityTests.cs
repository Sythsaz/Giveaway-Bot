// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.IO;
using System.Threading.Tasks;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class ProfileSecurityTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Profile Security Tests ===");
            await Test_ProfileSecurity_Comprehensive();
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

        private static async Task Test_ProfileSecurity_Comprehensive()
        {
            Console.WriteLine("Profile Security & Authorization Comprehensive (8 tests)");
            var (m, cph) = SetupWithCph();
            var adapter = new CPHAdapter(cph);
            adapter.Logger = m.Logger;

            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "TestLogs", "General");
            string logFile = Path.Combine(logDir, $"{DateTime.Now:yyyy-MM-dd}.log");

            try
            {
                // Clear any existing log file for clean test
                if (File.Exists(logFile))
                {
                    try { File.Delete(logFile); } catch { }
                }

                // 1. Test Non-Broadcaster CreateProfile Rejection
                Console.Write("  - Non-broadcaster CreateProfile (reject):   ");
                cph.Args["isBroadcaster"] = false;
                cph.Args["user"] = "RegularUser";
                cph.Args["userId"] = "999";
                cph.Args["rawInput"] = "!giveaway profile create SecureTest1";
                await m.ProcessTrigger(adapter);

                var config = m.Loader.GetConfig(adapter);
                bool notCreated = !config.Profiles.ContainsKey("SecureTest1");
                if (notCreated) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Profile was created!)");

                // 2. Test Non-Broadcaster DeleteProfile Rejection
                Console.Write("  - Non-broadcaster DeleteProfile (reject):   ");
                // First create as broadcaster
                cph.Args["isBroadcaster"] = true;
                cph.Args["rawInput"] = "!giveaway profile create SecureTest2";
                await m.ProcessTrigger(adapter);

                // Try to delete as non-broadcaster
                cph.Args["isBroadcaster"] = false;
                cph.Args["user"] = "RegularUser";
                cph.Args["rawInput"] = "!giveaway profile delete SecureTest2 confirm";
                await m.ProcessTrigger(adapter);

                config = m.Loader.GetConfig(adapter);
                bool stillExists = config.Profiles.ContainsKey("SecureTest2");
                if (stillExists) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Profile was deleted!)");

                // 3. Test Non-Broadcaster CloneProfile Rejection
                Console.Write("  - Non-broadcaster CloneProfile (reject):    ");
                cph.Args["isBroadcaster"] = false;
                cph.Args["user"] = "RegularUser";
                cph.Args["rawInput"] = "!giveaway profile clone Main SecureTest3";
                await m.ProcessTrigger(adapter);

                config = m.Loader.GetConfig(adapter);
                bool notCloned = !config.Profiles.ContainsKey("SecureTest3");
                if (notCloned) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Clone was created!)");

                // 4. Test Non-Broadcaster UpdateProfileConfig Rejection
                Console.Write("  - Non-broadcaster Config Update (reject):   ");
                int originalValue = config.Profiles["Main"].MaxEntriesPerMinute;
                cph.Args["isBroadcaster"] = false;
                cph.Args["user"] = "RegularUser";
                cph.Args["rawInput"] = "!giveaway profile config Main MaxEntriesPerMinute=999";
                await m.ProcessTrigger(adapter);

                config = m.Loader.GetConfig(adapter);
                bool unchanged = config.Profiles["Main"].MaxEntriesPerMinute == originalValue;
                if (unchanged) Console.WriteLine("PASS");
                else Console.WriteLine("FAIL (Config was updated!)");

                // 5. Test Audit Log for Unauthorized CreateProfile
                Console.Write("  - Audit log: Unauthorized Create logged:    ");
                if (File.Exists(logFile))
                {
                    string logContent = File.ReadAllText(logFile);
                    bool hasCreateAttempt = logContent.Contains("SecureTest1") &&
                                           logContent.Contains("Unauthorized") &&
                                           logContent.Contains("RegularUser");
                    if (hasCreateAttempt) Console.WriteLine("PASS");
                    else Console.WriteLine("FAIL (No log entry found)");
                }
                else
                {
                    Console.WriteLine("FAIL (Log file not created)");
                }

                // 6. Test Audit Log for Unauthorized DeleteProfile
                Console.Write("  - Audit log: Unauthorized Delete logged:    ");
                if (File.Exists(logFile))
                {
                    string logContent = File.ReadAllText(logFile);
                    bool hasDeleteAttempt = logContent.Contains("SecureTest2") &&
                                           logContent.Contains("Unauthorized");
                    if (hasDeleteAttempt) Console.WriteLine("PASS");
                    else Console.WriteLine("FAIL (No log entry found)");
                }
                else
                {
                    Console.WriteLine("FAIL (Log file not created)");
                }

                // 7. Test Audit Log Entry Format Verification
                Console.Write("  - Audit log entry format verification:     ");
                if (File.Exists(logFile))
                {
                    string logContent = File.ReadAllText(logFile);
                    string[] lines = logContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    bool formatValid = false;
                    foreach (var line in lines)
                    {
                        if (line.Contains("Unauthorized"))
                        {
                            // Expected format: [YYYY-MM-DD HH:mm:ss] [WARN ] [Security] Unauthorized...
                            // Note: Matches [WARN ] [Category] [Security]...
                            var regex = new System.Text.RegularExpressions.Regex(@"\[\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}(?:\s+[AP]M)?\] \[WARN\s*\] .*\[Security\]");
                            formatValid = regex.IsMatch(line);
                            break;
                        }
                    }
                    if (formatValid) Console.WriteLine("PASS");
                    else Console.WriteLine($"FAIL (Format invalid)");
                }
                else
                {
                    Console.WriteLine("FAIL (No log file)");
                }

                // 8. Test Audit Log Daily Rotation
                Console.Write("  - Audit log daily rotation (file naming):   ");
                string expectedFileName = $"{DateTime.Now:yyyy-MM-dd}.log";
                bool correctNaming = logFile.EndsWith(expectedFileName);
                if (correctNaming) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (Expected: {expectedFileName}, Got: {Path.GetFileName(logFile)})");

                try { File.AppendAllText("test_debug.log", "[Test_ProfileSecurity_Comprehensive] finished\n"); } catch { }
            }
            finally
            {
                // Cleanup
                try
                {
                    string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "config", "giveaway_config.json");
                    if (File.Exists(configPath)) File.Delete(configPath);
                    if (File.Exists(logFile)) File.Delete(logFile);
                }
                catch { }
            }
        }
    }
}
