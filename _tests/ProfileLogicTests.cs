// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class ProfileLogicTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Profile Logic & Concurrency Tests ===");
            await Test_ConfigAndRouting();
            await Test_RateLimit();
            await Test_Concurrency();
            await Test_Concurrency_EntryVsDraw();
        }

        private static async Task Test_ConfigAndRouting()
        {
            Console.Write("[TEST] Config & Routing (End-to-End): ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            cph.Args["rawInput"] = "!giveaway config gen";
            m.Initialize(new CPHAdapter(cph));
            await m.ProcessTrigger(new CPHAdapter(cph));
            if (m.States.TryGetValue("Main", out var s))
            {
                s.IsActive = true;
                cph.Args.Clear(); cph.Args["command"] = "!enter"; cph.Args["userId"] = "u1"; cph.Args["user"] = "U1";
                await m.ProcessTrigger(new CPHAdapter(cph));
                if (s.Entries.TryGetValue("u1", out var e1) && e1.TicketCount == 2) Console.WriteLine("PASS"); else Console.WriteLine("FAIL (No entry or incorrect tickets, default is 2)");
            }
            else Console.WriteLine("FAIL (No state)");
        }

        private static async Task Test_RateLimit()
        {
            Console.Write("[TEST] Global Rate Limit: ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));
            
            // Setup Main state and config
            if (GiveawayManager.GlobalConfig == null) GiveawayManager.GlobalConfig = new GiveawayBotConfig();
            if (!GiveawayManager.GlobalConfig.Profiles.ContainsKey("Main")) GiveawayManager.GlobalConfig.Profiles["Main"] = new GiveawayProfileConfig();
            
            m.States["Main"] = new GiveawayState { IsActive = true };
            
            if (m.States.TryGetValue("Main", out var s))
            {
                s.IsActive = true;
                GiveawayManager.GlobalConfig.Profiles["Main"].MaxEntriesPerMinute = 2;
                cph.Args.Clear(); cph.Args["command"] = "!enter"; cph.Args["userId"] = "u1"; cph.Args["user"] = "U1"; await m.ProcessTrigger(new CPHAdapter(cph));
                cph.Args.Clear(); cph.Args["command"] = "!enter"; cph.Args["userId"] = "u2"; cph.Args["user"] = "U2"; await m.ProcessTrigger(new CPHAdapter(cph));
                cph.Args.Clear(); cph.Args["command"] = "!enter"; cph.Args["userId"] = "u3"; cph.Args["user"] = "U3"; await m.ProcessTrigger(new CPHAdapter(cph)); // Should be blocked
                if (s.Entries.Count == 2) Console.WriteLine("PASS"); else Console.WriteLine($"FAIL (Count={s.Entries.Count})");
            }
            else Console.WriteLine("FAIL (No state)");
        }

        private static async Task Test_Concurrency()
        {
            Console.Write("[TEST] Concurrency: ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));
            
            // Setup Main and Weekly
             if (GiveawayManager.GlobalConfig == null) GiveawayManager.GlobalConfig = new GiveawayBotConfig();
             GiveawayManager.GlobalConfig.Profiles["Main"] = new GiveawayProfileConfig();
             GiveawayManager.GlobalConfig.Profiles["Weekly"] = new GiveawayProfileConfig();
             GiveawayManager.GlobalConfig.Profiles["Weekly"].Triggers["command:!weekly"] = "Entry";

             m.States["Main"] = new GiveawayState { IsActive = true };
             m.States["Weekly"] = new GiveawayState { IsActive = true };

            if (m.States.TryGetValue("Main", out var s1) && m.States.TryGetValue("Weekly", out var s2))
            {
                s1.IsActive = true; s2.IsActive = true;
                cph.Args.Clear(); cph.Args["command"] = "!enter"; cph.Args["userId"] = "u1"; cph.Args["user"] = "U1"; await m.ProcessTrigger(new CPHAdapter(cph));
                cph.Args.Clear(); cph.Args["command"] = "!weekly"; cph.Args["userId"] = "u2"; cph.Args["user"] = "U2"; await m.ProcessTrigger(new CPHAdapter(cph));
                bool u1In1 = s1.Entries.ContainsKey("u1");
                bool u2In2 = s2.Entries.ContainsKey("u2");
                bool leak = s1.Entries.ContainsKey("u2");
                if (u1In1 && u2In2 && !leak) Console.WriteLine("PASS");
                else Console.WriteLine($"FAIL (u1InMain={u1In1}, u2InWeekly={u2In2}, u2InMain={leak})");
            }
            else Console.WriteLine("FAIL (States missing)");
        }

        private static async Task Test_Concurrency_EntryVsDraw()
        {
            Console.WriteLine("\n[TEST] Concurrency Entry vs Draw:");
            Console.Write("  - Parallel Execution:                    ");
            var cph = new MockCPH();
            var m = new GiveawayManager();
            m.Initialize(new CPHAdapter(cph));

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
