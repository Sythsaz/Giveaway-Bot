// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class SeparateGameNameDumpsTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Separate Game Name Dumps Tests ===");
            await Test_SeparateGameNameDumps();
        }

        private static async Task Test_SeparateGameNameDumps()
        {
            Console.Write("[TEST] Separate GameName Dumps: ");
            var cph = new MockCPH();
            var m = new GiveawayManager();

            // Reset static state
            GiveawayManager.GlobalConfig = null;
            m.States.Clear();
            var adapter = new CPHAdapter(cph, cph.Args);
            adapter.Logger = cph.Logger;
            m.Initialize(adapter);

            cph.Args["isBroadcaster"] = true;

            try
            {
                // Create test profile
                cph.Args["rawInput"] = "!giveaway profile create GameNameTest";
                await m.ProcessTrigger(adapter);

                // Enable separate dumps
                var config = GiveawayManager.GlobalConfig.Profiles["GameNameTest"];
                config.DumpSeparateGameNames = true;
                config.DumpEntriesOnEnd = true;
                config.DumpWinnersOnDraw = true;
                config.DumpFormat = DumpFormat.TXT;
                config.EnableWheel = false;

                // Add entries: one with GameName, one without
                m.States["GameNameTest"].IsActive = true;
                m.States["GameNameTest"].Entries["user1"] = new Entry
                {
                    UserId = "user1",
                    UserName = "User1",
                    GameName = "GameUser.1234",
                    TicketCount = 1,
                    EntryTime = DateTime.Now
                };
                m.States["GameNameTest"].Entries["user2"] = new Entry
                {
                    UserId = "user2",
                    UserName = "User2",
                    GameName = null, // No game name
                    TicketCount = 1,
                    EntryTime = DateTime.Now
                };

                // End giveaway to trigger dump
                cph.Args.Clear();
                cph.Args["rawInput"] = "!giveaway profile end GameNameTest";
                cph.Args["isBroadcaster"] = true;
                await m.ProcessTrigger(adapter);

                // Check for separate GameNames file
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string dumpDir = Path.Combine(baseDir, "Giveaway Bot", "dumps", "GameNameTest");

                if (!Directory.Exists(dumpDir))
                {
                    Console.WriteLine("FAIL (Dump directory not found)");
                    return;
                }

                var gameNamesFiles = Directory.GetFiles(dumpDir, "*_Entries_GameNames.txt");
                if (gameNamesFiles.Length == 0)
                {
                    Console.WriteLine("FAIL (GameNames file not created)");
                    return;
                }

                // Verify content
                string content = File.ReadAllText(gameNamesFiles[0]);
                bool hasGameName = content.Contains("GameUser.1234");
                bool hasUserName = content.Contains("User2");

                if (hasGameName && hasUserName)
                {
                    Console.WriteLine("PASS");
                }
                else
                {
                    Console.WriteLine($"FAIL (Content missing. Has GameName: {hasGameName}, Has UserName: {hasUserName})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL ({ex.Message})");
            }
        }
    }
}
