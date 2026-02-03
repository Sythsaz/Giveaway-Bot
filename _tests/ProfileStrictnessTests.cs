using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StreamerBot;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace StreamerBot.Tests
{
    public static class ProfileStrictnessTests
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== Profile Strictness Tests ===");
            await Test_RequireFollower_RejectsNonFollower();
            await Test_RequireFollower_AcceptsFollower();
            await Test_RequireSubscriber_RejectsNonSub();
            await Test_RequireSubscriber_AcceptsSub();
        }

        private static (GiveawayManager m, MockCPH cph, CPHAdapter adapter) Setup()
        {
            var cph = new MockCPH();
            var m = new GiveawayManager();
            var adapter = new CPHAdapter(cph, cph.Args);

            // Initializing will create default config
            m.Initialize(adapter);

            // Ensure Main profile exists and is active
            if (!m.States.TryGetValue("Main", out var state))
            {
                state = new GiveawayState();
                m.States["Main"] = state;
            }
            state.Entries.Clear();
            state.IsActive = true;

            // Setup default user args
            cph.Args["userId"] = "strictUser";
            cph.Args["user"] = "StrictUser";
            cph.Args["userType"] = "Twitch";
            cph.Args["command"] = "!enter";

            return (m, cph, adapter);
        }

        private static async Task Test_RequireFollower_RejectsNonFollower()
        {
            Console.Write("[TEST] RequireFollower (Reject): ");
            var (m, cph, adapter) = Setup();

            // Configure Strictness
            GiveawayManager.GlobalConfig.Profiles["Main"].RequireFollower = true;

            // Ensure NOT a follower
            cph.Followers.Remove("strictUser");

            await m.ProcessTrigger(adapter);

            if (m.States["Main"].Entries.Count != 0)
                throw new Exception("Entry count mismatch: Expected 0 (Rejected), Got " + m.States["Main"].Entries.Count);

            // Check Metric
            if (!cph.Globals.TryGetValue("Giveaway Global Metrics EntriesRejected", out var rejected) || Convert.ToInt32(rejected) != 1)
                throw new Exception("Metric mismatch: EntriesRejected expected 1");

            Console.WriteLine("PASS");
        }

        private static async Task Test_RequireFollower_AcceptsFollower()
        {
            Console.Write("[TEST] RequireFollower (Accept): ");
            var (m, cph, adapter) = Setup();

            // Configure Strictness
            GiveawayManager.GlobalConfig.Profiles["Main"].RequireFollower = true;

            // Set as follower
            cph.Followers.Add("strictUser");

            await m.ProcessTrigger(adapter);

            if (m.States["Main"].Entries.Count != 1)
                throw new Exception("Entry count mismatch: Expected 1 (Accepted), Got " + m.States["Main"].Entries.Count);

            Console.WriteLine("PASS");
        }

        private static async Task Test_RequireSubscriber_RejectsNonSub()
        {
            Console.Write("[TEST] RequireSubscriber (Reject): ");
            var (m, cph, adapter) = Setup();

            // Configure Strictness
            GiveawayManager.GlobalConfig.Profiles["Main"].RequireSubscriber = true;

            // Ensure NOT a sub
            cph.Subscribers.Remove("strictUser");

            await m.ProcessTrigger(adapter);

            if (m.States["Main"].Entries.Count != 0)
                throw new Exception("Entry count mismatch: Expected 0 (Rejected), Got " + m.States["Main"].Entries.Count);

            // Check Metric
            if (!cph.Globals.TryGetValue("Giveaway Global Metrics EntriesRejected", out var rejected) || Convert.ToInt32(rejected) != 1)
                throw new Exception("Metric mismatch: EntriesRejected expected 1");

            Console.WriteLine("PASS");
        }

        private static async Task Test_RequireSubscriber_AcceptsSub()
        {
            Console.Write("[TEST] RequireSubscriber (Accept): ");
            var (m, cph, adapter) = Setup();

            // Configure Strictness
            GiveawayManager.GlobalConfig.Profiles["Main"].RequireSubscriber = true;

            // Set as sub
            cph.Subscribers.Add("strictUser");

            await m.ProcessTrigger(adapter);

            if (m.States["Main"].Entries.Count != 1)
                throw new Exception("Entry count mismatch: Expected 1 (Accepted), Got " + m.States["Main"].Entries.Count);

            Console.WriteLine("PASS");
        }
    }
}
