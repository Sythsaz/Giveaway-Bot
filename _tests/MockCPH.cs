using System;
// Streamer.bot uses .NET Framework 4.8 / C# 7.3
#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable IDE0028 // Simplify collection initialization
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using StreamerBot;

namespace StreamerBot.Tests
{
    public class MockCPH : IGiveawayCPH
    {
        public Dictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> Globals { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, Dictionary<string, object>> UserVars { get; set; } = new Dictionary<string, Dictionary<string, object>>();
        public List<string> Logs { get; set; } = new List<string>();
        public List<string> ChatHistory { get; set; } = new List<string>();
        public string EventType { get; set; } = "CommandTriggered";
        public FileLogger Logger { get; set; } = new FileLogger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Giveaway Bot", "TestLogs"));

        public void LogInfo(string m) { Console.WriteLine($"[INFO] {m}"); Logs.Add(m); }
        public void LogWarn(string m) { Console.WriteLine($"[WARN] {m}"); Logs.Add(m); }
        public void LogDebug(string m) { Console.WriteLine($"[DEBUG] {m}"); Logs.Add(m); }
        public void LogError(string m) { Console.WriteLine($"[ERROR] {m}"); Logs.Add(m); }
        public void LogTrace(string m) { Console.WriteLine($"[TRACE] {m}"); Logs.Add(m); }
        public void ShowToastNotification(string title, string message)
        {
            Console.WriteLine($"[TOAST] {title}: {message}");
        }

        public void LogVerbose(string m) { Console.WriteLine($"[VERBOSE] {m}"); Logs.Add(m); }
        public void LogFatal(string m) { Console.WriteLine($"[FATAL] {m}"); Logs.Add(m); }

        public bool TryGetArg<T>(string n, out T v)
        {
            if (Args.TryGetValue(n, out var o))
            {
                try { v = (T)Convert.ChangeType(o, typeof(T)); return true; } catch { }
            }
            v = default(T); return false;
        }

        public T GetGlobalVar<T>(string n, bool p = true)
        {
            if (Globals.TryGetValue(n, out var o))
            {
                try { return (T)Convert.ChangeType(o, typeof(T)); } catch { }
            }
            return default(T);
        }

        public void SetGlobalVar(string n, object v, bool p = true) => Globals[n] = v;

        public T GetUserVar<T>(string u, string n, bool p = true)
        {
            if (UserVars.TryGetValue(u, out var dict))
            {
                if (dict.TryGetValue(n, out var o))
                {
                    try { return (T)Convert.ChangeType(o, typeof(T)); } catch { }
                }
            }
            return default(T);
        }

        public void SetUserVar(string u, string n, object v, bool p = true)
        {
            if (!UserVars.ContainsKey(u)) UserVars[u] = new Dictionary<string, object>();
            UserVars[u][n] = v;
        }

        public void SendMessage(string m, bool b = true) { Console.WriteLine($"[CHAT] {m}"); ChatHistory.Add(m); }
        public void SendYouTubeMessage(string m) { Console.WriteLine($"[YT] {m}"); ChatHistory.Add(m); }
        public void SendKickMessage(string m) { Console.WriteLine($"[KICK] {m}"); ChatHistory.Add(m); }
        public void TwitchReplyToMessage(string message, string replyId, bool useBot = true, bool fallback = true)
        {
            Console.WriteLine($"[TWITCH REPLY to {replyId}] {message}");
            ChatHistory.Add($"@reply:{replyId}:{message}");
        }


        public bool IsTwitchLive() => IsTwitchLiveValue;
        public bool IsYouTubeLive() => IsYouTubeLiveValue;
        public bool IsKickLive() => IsKickLiveValue;

        public bool IsTwitchLiveValue { get; set; } = false;
        public bool IsYouTubeLiveValue { get; set; } = false;
        public bool IsKickLiveValue { get; set; } = false;

        // Aliases for reflection calls in CPHAdapter
        public bool IsTwitchBroadcasterLive() => IsTwitchLiveValue;
        public bool IsYouTubeBroadcasterLive() => IsYouTubeLiveValue;
        public bool IsKickBroadcasterLive() => IsKickLiveValue;

        public void ObsSetBrowserSource(string s, string o, string u) { Console.WriteLine($"[OBS] {s}/{o} -> {u}"); }
        public object GetEventType() => EventType;

        public bool RunAction(string actionName, bool runImmediately = true)
        {
            Console.WriteLine($"[ACTION] Running: {actionName} (Immediate: {runImmediately})");
            return true;
        }

        public List<string> GetGlobalVarNames(bool persisted = true) => Globals.Keys.ToList();
        public void UnsetGlobalVar(string varName, bool persisted = true)
        {
            Globals.Remove(varName);
        }
    }
}
