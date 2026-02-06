// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable IDE0071 // Simplify interpolation
#pragma warning disable IDE0056 // Indexing can be simplified (C# 8.0 ^ operator)
#pragma warning disable IDE0054 // Use compound assignment
#pragma warning disable IDE0074 // Use compound assignment
#pragma warning disable CA1854 // Prefer TryGetValue
using System;
using System.IO;
using System.Threading.Tasks;
using StreamerBot;

namespace StreamerBot.Tests
{
    public class TestRunner
    {
        public static void Main()
        {
            try { File.WriteAllText("TEST_STARTED.txt", "Execution reached TestRunner.Main at " + DateTime.Now.ToString()); } catch { }

            Console.WriteLine("=================================");
            Console.WriteLine("   GiveawayBot Final Verification ");
            Console.WriteLine("=================================");

            try { File.AppendAllText("test_debug.log", $"[Main] Started at {DateTime.Now}\n"); } catch { }

            // Ensure console output is flushed
            Console.Out.Flush();

            try
            {
                RunTestsAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[FATAL ERROR] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("\n[DONE] Finished.");
            Console.Out.Flush();
        }

        private static async Task RunTestsAsync()
        {
            // Cleanup storage to ensure clean test environment
            string storageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot");
            if (Directory.Exists(storageDir))
            {
                // Try to delete, but ignore if locked
                try { Directory.Delete(storageDir, true); } catch { }
            }

            await ConfigSyncTests.Run();
            await ProfileTests.Run();
            await ProfilePersistenceTests.Run();
            await ProfileConfigTests.Run();
            await ProfileSecurityTests.Run();
            await ProfileEdgeCaseTests.Run();
            await ProfileLogicTests.Run();
            await IntegrationTests.Run();
            await SeparateGameNameDumpsTests.Run();
            await ProfileStrictnessTests.Run();
            await CoreTests.Run();

            var cph10 = new MockCPH();
            var m10 = new GiveawayManager();
            m10.Initialize(new CPHAdapter(cph10, cph10.Args));

        }
    }

    // Signal that all tests have finished
    public class TestExecutionSignal { }
}
