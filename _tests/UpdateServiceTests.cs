// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable IDE0071 // Simplify interpolation
#pragma warning disable IDE0056 // Indexing can be simplified (C# 8.0 ^ operator)
#pragma warning disable IDE0054 // Use compound assignment
#pragma warning disable IDE0074 // Use compound assignment
#pragma warning disable CA1854 // Prefer TryGetValue
using System;
using System.Threading.Tasks;
using StreamerBot;

namespace StreamerBot.Tests
{
    public static class UpdateServiceTests
    {
        public static async Task Run()
        {
            Console.WriteLine("Running UpdateServiceTests...");

            await TestExtractChecksum();
            await TestValidateChecksum_Valid();
            await TestValidateChecksum_Invalid();
            await TestValidateChecksum_NoChecksum();

            Console.WriteLine("UpdateServiceTests passed.");
        }

        private static Task TestExtractChecksum()
        {
            // Case 1: Valid checksum in body
            string body = "Release notes...\nSHA256: AABBCC0011223344556677889900AABBCC0011223344556677889900AABBCC00\nMore text";
            string checksum = UpdateService.ExtractChecksum(body);
            if (checksum != "AABBCC0011223344556677889900AABBCC0011223344556677889900AABBCC00")
                throw new Exception($"TestExtractChecksum failed: Expected hash, got {checksum}");

            // Case 2: Case insensitive
            string body2 = "sha256: aabbcc0011223344556677889900aabbcc0011223344556677889900aabbcc00";
            string checksum2 = UpdateService.ExtractChecksum(body2);
            if (checksum2 == null || !checksum2.Equals("aabbcc0011223344556677889900aabbcc0011223344556677889900aabbcc00", StringComparison.OrdinalIgnoreCase))
                throw new Exception("TestExtractChecksum failed: Case insensitive match failed");

            // Case 3: No checksum
            if (UpdateService.ExtractChecksum("No hash here") != null)
                throw new Exception("TestExtractChecksum failed: Found non-existent hash");

            return Task.CompletedTask;
        }

        private static Task TestValidateChecksum_Valid()
        {
            string content = "Hello World";
            // SHA256("Hello World") = a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e
            string hash = "a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e";

            if (!UpdateService.ValidateChecksum(content, hash))
                throw new Exception("TestValidateChecksum_Valid failed: Valid content rejected");

            return Task.CompletedTask;
        }

        private static Task TestValidateChecksum_Invalid()
        {
            string content = "Hello World Modified";
            string hash = "a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e";

            if (UpdateService.ValidateChecksum(content, hash))
                throw new Exception("TestValidateChecksum_Invalid failed: Invalid content accepted");

            return Task.CompletedTask;
        }

        private static Task TestValidateChecksum_NoChecksum()
        {
            // Should return true (allow update with warning)
            if (!UpdateService.ValidateChecksum("Content", null))
                throw new Exception("TestValidateChecksum_NoChecksum failed: Null checksum should pass (warn only)");

            if (!UpdateService.ValidateChecksum("Content", ""))
                throw new Exception("TestValidateChecksum_NoChecksum failed: Empty checksum should pass (warn only)");

            return Task.CompletedTask;
        }
    }
}
