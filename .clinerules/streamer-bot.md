# Streamer.bot C# Coding Rules - Part 1: Core Foundations

> These rules are **MANDATORY**. Follow them strictly to ensure stability and compatibility.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 1. Core Philosophy: Meticulousness

The Streamer.bot environment is unique. It runs compiled C# code at runtime within a long-lived process. Errors here can crash the bot or silently fail, ruining a live stream.

- **Explicit over Implicit**: Do not rely on default values. Explicitly define all configurations.
- **Zero Assumptions**:
  - Never assume an `args` key exists. CHECK IT.
  - Never assume a file exists. CHECK IT.
  - Never assume a network call will succeed. TRY-CATCH IT.
- **Strong Typing**:
  - The use of `dynamic` is **STRICTLY PROHIBITED**.
  - **Reason**: It requires `Microsoft.CSharp.dll` which may not be present or compatible in all SB versions (especially .NET Core vs Framework variances), and it hides compilation errors until runtime.
  - **Alternative**: Use safe Reflection or `JObject` (Newtonsoft.Json) with explicit casting.

### 1.1. Identity & Versioning

Every action must be identifiable in logs. Anonyous code is hard to debug.

- **Action Name**: Define a constant `ActionName`.
- **Version**: Define a constant `Version`.
- **Log Prefix**: ALL logs must use these.

```csharp
public class CPHInline
{
    private const string ActionName = "{Prorgam Name} Bot";
    private const string Version = "1.0.2";

    // Usage: CPH.LogInfo($"[{ActionName} v{Version}] Starting...");
}
```

## 2. Architecture & Structure

Streamer.bot compiles each "Action" code block as a single unit.

### 2.1. Compilation Unit

- **Single-File Compatibility**: all code _can_ be in one file, but for larger projects, use regions.
- **Namespace**: `MultiStreamingBot.[UniqueFeatureName]`
  - _Prevents namespace collisions when multiple C# actions are loaded._
- **Class**: `public class CPHInline`
  - _Must be named exactly this._
- **Method**: `public bool Execute()`
  - _Must be the entry point._

### 2.2. Service Pattern

Do not write 500 lines of code inside `Execute()`. Use the Service Pattern.

- **Separation of Concerns**: Create strictly typed classes for logic (e.g., `PointRedemptionService`, `FileLogger`).
- **Dependency Injection**: Pass `CPH` into these services.
  - **Interface**: Define `interface IStartable` or `interface IService` if useful, but always pass `ICPHInline` (if available) or wrap `CPH` in a custom `IStreamerBot` interface to allow unit testing in Visual Studio.
- **Initialization**: Use an `Init()` method.
  - **Avoid Static Constructors**: They run ONCE per compilation (app lifetime usually). If you need fresh state per action run, use instance methods or explicit `Init`.

```csharp
public class CPHInline
{
    public bool Execute()
    {
        // 1. Setup
        var logger = new Logger(CPH);
        var service = new GiveawayService(CPH, logger);

        // 2. Execution
        return service.Run(args);
    }
}
```

## 3. Safety & Stability (The "Global Net")

Every action **MUST** be crash-proof.

### 3.1. Global Try-Catch

The `Execute` method must be wrapped in a catch-all block. Uncaught exceptions here stops the entire sub-action chain.

```csharp
public bool Execute()
{
    try
    {
        // Your code here
        return true;
    }
    catch (Exception ex)
    {
        // ALWAYS log the stack trace. "Something went wrong" is useless.
        CPH.LogError($"[CRITICAL] Action Failed: {ex.Message}\n{ex.StackTrace}");
        return false;
    }
}
```

### 3.2. Guarded Execution

Any method that touches **I/O (Files)**, **Network (HTTP)**, or **Parsing** must handle its own exceptions or propagate them strictly to a handler that does.

- **Null Checks**: `obj?.Property` is your friend.
- **Validation**: `if (string.IsNullOrWhiteSpace(input)) return;`

## 4. Argument Handling (`args`)

The `args` dictionary is your input. It is untyped and dangerous.

### 4.1. The Golden Rule of Extraction

**NEVER** do this: `int x = (int)args["x"];` -> CRASH if missing or not int.
**ALWAYS** use a helper or `TryGetValue`.

### 4.2. Recommended Helper

Copy this helper into your `CPHInline` or a shared `Helpers` region.

```csharp
// Boilerplate for safe argument extraction
private T GetArg<T>(Dictionary<string, object> args, string key, T defaultValue)
{
    if (!args.TryGetValue(key, out object valObj) || valObj == null)
        return defaultValue;

    try
    {
        // Handle common numeric coercions (long -> int, double -> int)
        if (typeof(T) == typeof(int) && valObj is long l) return (T)(object)(int)l;
        if (typeof(T) == typeof(int) && valObj is double d) return (T)(object)(int)d;

        return (T)Convert.ChangeType(valObj, typeof(T));
    }
    catch
    {
        CPH.LogWarn($"[Arg] Failed to convert '{key}' ({valObj.GetType().Name}) to {typeof(T).Name}. Using default.");
        return defaultValue;
    }
}
```

### 4.3. Magic Strings & Constants

String typos are the #1 cause of "Silent Failure" (where the bot runs but does nothing).

- **Prohibition**: Do not use raw strings for dictionary keys, file paths, or specific values.
- **Solution**: Define `private const` or `public const` fields.

```csharp
private static class Args
{
    public const string UserName = "userName";
    public const string TargetId = "targetId";
}

// Good
var user = GetArg<string>(args, Args.UserName, "Guest");
```

## 5. Logging Standards

Logs are your only window into the bot's brain.

### 5.1. Format

`[Component/Class] [Method] Message (ExtraContext)`

- Example: `[GiveawayService] [PickWinner] Found 50 entrants. (Filter: SubsOnly)`

### 5.2. Levels

- **CPH.LogInfo**: Normal flow events ("Job started", "Job finished").
- **CPH.LogWarn**: Recoverable issues ("Config missing, using default", "API timeout, retrying").
- **CPH.LogError**: Action-terminating failures ("Disk full", "401 Unauthorized").
- **CPH.LogDebug**: Granular data (enabled via a flag). **DO NOT** spam LogInfo with debug data.

## 6. References

You **MUST** list required DLL references at the top of your main file for clarity.

```csharp
// css_ref System.Net.Http.dll
// css_ref Newtonsoft.Json.dll
// css_ref System.Core.dll
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
```

### 6.1. Framework Restrictions

- **Target**: Streamer.bot runs on .NET Framework 4.8 (usually).
- **Warning**: Do NOT try to use .NET Core / .NET 5+ APIs (e.g., `System.Text.Json`, `Span<T>`) unless you are 100% sure the user's environment handles it.
- **Safe Bet**: Stick to `Newtonsoft.Json` (usually included) and standard pre-2019 C# features.

### 6.2. External Dependencies (DLLs)

Streamer.bot does NOT support Nuget. You cannot use `project.json` or `.csproj`.

- **Manual Install**: You must download `.dll` files manually (compatible with .NET Framework 4.8).
- **Placement**: Place them in a `dlls` folder relative to the bot, or a known absolute path.
- **Reference**: Use the `// css_ref` directive at the **very top** of your code.

```csharp
// css_ref C:\Streamer.bot\dlls\MyLibrary.dll
using MyLibrary;
```

# Streamer.bot C# Coding Rules - Part 2: The Mechanics

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 7. I/O & Persistence

File operations are the most common source of bot crashes.

### 7.1. Atomic Writes ("Write-Replace")

**NEVER** write directly to a critical file. If the bot crashes mid-write, the file is corrupted forever.

1. Write to `filename.tmp`.
2. Move `filename.tmp` to `filename.json` with `overwrite: true`.

```csharp
public void SaveData(string path, string data)
{
    string tempPath = path + ".tmp";
    File.WriteAllText(tempPath, data, Encoding.UTF8); // Always UTF8
    File.Move(tempPath, path); // Atomic on NTFS
}
```

### 7.2. JSON Serialization

- **No Dynamic**: Use defined classes.
- **Settings**: Always use `ReferenceLoopHandling.Ignore` if your objects might have cycles.
- **Paths**: Use `Path.Combine`. Do NOT concatenate strings with `\\`.
- **Relative Paths**: Do NOT assume the Current Working Directory is the bot folder. Always use absolute paths or paths derived from a known configuration.

### 7.3. Folder Management

- **Auto-Create**: Always check `Directory.Exists()` before writing. If false, `Directory.CreateDirectory()`.
- **Sanitization**: If using user input for filenames, use `Path.GetInvalidFileNameChars()` to sanitize.

## 8. Network & HTTP

External APIs will fail. Your code must not.

### 8.1. The HttpClient Singleton

**CRITICAL**: `HttpClient` is designed to be instantiated ONCE per application life.

- **BAD**: `using (var client = new HttpClient()) { ... }` inside a loop -> Socket Exhaustion -> Bot Crash.
- **GOOD**: `private static readonly HttpClient _client = new HttpClient();`

### 8.2. Resilience (Polly-lite)

You cannot import Polly easily. Write a simple retry wrapper.

- **Timeout**: Set `_client.Timeout = TimeSpan.FromSeconds(10);`. Default is 100s (too long).
- **User-Agent**: Many APIs (GitHub, Twitch) block requests without a User-Agent. Set it globally.

```csharp
private static readonly HttpClient _http;
static CPHInline()
{
    _http = new HttpClient();
    _http.Timeout = TimeSpan.FromSeconds(10);
    _http.DefaultRequestHeaders.Add("User-Agent", "StreamerBot/MyAction 1.0");
}
```

## 9. Concurrency & Thread Safety

Actions run in parallel. Shared state is dangerous.

### 9.1. Locking

If multiple actions modify a shared `List<T>` or write to the same file, you **MUST** lock.

- **Lock Object**: `private static readonly object _lock = new object();`
- **Usage**: `lock(_lock) { ... }` for memory operations.
- **Files**: For files, standard `lock` works for logic, but `FileShare.ReadWrite` is needed if opening streams.

### 9.2. Static State Lifecycle

**CRITICAL**: `static` variables persist across ALL executions of the action until code is recompiled or the bot restarts.

- **Bleed**: Data from Action Run A will appear in Action Run B if not cleared.
- **Reset**: Implement a `Reset()` method or strictly re-initialize state in your `Execute()` method.

```csharp
public bool Execute()
{
    // RESET static state if it's meant to be per-run
    _processedUsers.Clear();

    // ... execution ...
    return true;
}
```

## 10. Performance & Loops

### 10.1. Loop Safety

**NEVER** write a `while(true)` or `while(!done)` loop without a delay. It will pin a CPU core to 100% and freeze the bot.

- **Mandatory**: `CPH.Wait(10);` inside any waiting loop.

### 10.2. String Performance

- **Concatenation**: Do NOT use `s += "text"` inside loops. It creates thousands of garbage strings.
- **Requirement**: Use `StringBuilder` for any loop-based text generation (e.g., building a chat message from a list of users).

```csharp
StringBuilder sb = new StringBuilder();
foreach(var user in users)
{
    sb.Append(user).Append(", ");
}
return sb.ToString();
```

## 11. Safe Reflection

Since `dynamic` is banned, you may need Reflection. Reflection is slow.

- **Caching**: Do not call `GetMethod` in `Execute()`. Call it once in a static constructor or lazy property and cache the `MethodInfo`.
- **Safety**: `MethodInfo` might be null if the target dll updates. Check for null before Invoke.

```csharp
private static MethodInfo _cachedMethod;
private static void InvokeSafe(object target)
{
    if (_cachedMethod == null)
    {
        _cachedMethod = target.GetType().GetMethod("SpecialFunc");
    }
    _cachedMethod?.Invoke(target, null);
}
```

## 12. Resource Cleanup

- **IDisposable**: If you create a `process`, `stream`, or `bitmap`, wrap it in `using`.
- **Events**: If you subscribe to `CPH.ObsEvents`, you usually don't need to unsubscribe as the CPH instance is ephemeral, BUT if you hook into static C# events, you **MUST** unsubscribe to avoid memory leaks.

# Streamer.bot C# Coding Rules - Part 3: The Ecosystem

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 13. Security

Streamer.bot often runs with high privileges. Do not be the weak link.

### 13.1. Secrets Management

- **NEVER** hardcode API keys, passwords, or OAuth tokens in C# files.
- **Retrieval**: Use `args` populated from the Action's user interface or `CPH.GetGlobalVar` (if encrypted/hidden).

### 13.2. Log Sanitation

- **Redaction**: Before logging an object, ensure it doesn't contain tokens.
- **Masking**: `LogInfo($"User token: {token.Substring(0, 4)}***");`

### 13.3. Input Sanitization

- **Path Traversal**: If a user can specify a filename, sanitize it strictly.
  ```csharp
  string safeName = Path.GetFileName(userInput); // Removes directory separators
  ```
- **Injection**: If you were to run SQL or Shell commands (avoid if possible), use parameterized queries only.

## 14. Code Style & Organization

Clean code reduces bugs.

### 14.1. Standard Naming

- **Classes/Methods**: `PascalCase` (`Run`, `CalculatePoints`).
- **Variables/Parameters**: `camelCase` (`userId`, `pointsToGive`).
- **Private Fields**: `_camelCase` (`_logger`, `_httpClient`).
- **Constants**: `PascalCase` (`DefaultTimeout`, `MaxRetries`).

### 14.2. Regions

Because we often use single-file compilation, use Regions to navigate.

```csharp
#region References
// css_ref ...
#endregion

#region Services
public class MyService { ... }
#endregion
```

## 15. Testing & Mocking

We strive for testable code. The biggest hurdle is the `CPH` object.

### 15.1. The Interface Wrapper

Do not pass `CPH` directly. Wrap it.

```csharp
public interface IStreamerBot
{
    void LogInfo(string message);
    void LogError(string message);
    object GetGlobalVar<T>(string varName, bool persisted = true);
    // Add methods as used
}

public class StreamerBotAdapter : IStreamerBot
{
    private readonly IInlineInvokeProxy _cph;
    public StreamerBotAdapter(IInlineInvokeProxy cph) { _cph = cph; }
    public void LogInfo(string msg) => _cph.LogInfo(msg);
    public void LogError(string msg) => _cph.LogError(msg);
    public object GetGlobalVar<T>(string name, bool p) => _cph.GetGlobalVar<T>(name, p);
}
```

### 15.2. Mock Implementation

Use this in your separate Test Project to simulate the bot.

```csharp
public class MockCPH : IStreamerBot
{
    public List<string> Logs = new List<string>();

    public void LogInfo(string msg) => Logs.Add($"[INFO] {msg}");
    public void LogError(string msg) => Logs.Add($"[ERROR] {msg}");
    public object GetGlobalVar<T>(string name, bool p) => default(T);
}
```

## 16. Action Chaining & Queues

### 16.1. Synchronous vs Asynchronous

- **RunAction**: Runs the action immediately and waits for it to finish (or runs in parallel depending on the strict overload).
- **RunAction (Run Action by Name)**: Usually synchronous.
- **Race Conditions**: If Action A starts Action B and C, do not assume B finishes before C unless you strictly wait.

### 16.2. Queue Management

- **Blocking**: Do not block the main CPH thread with `Thread.Sleep(5000)`.
- **Queues**: If an operation is long, consider adding it to a dedicated "Long Running" action queue in Streamer.bot (Blocking Queue) rather than doing it inline.

## 17. Prohibited APIs

Some .NET APIs are dangerous in the Streamer.bot context.
| API | Status | Reason | Replacement |
| :--- | :--- | :--- | :--- |
| `MessageBox.Show` | **BANNED** | Blocks the thread, requires GUI interaction (hangs in headless). | `CPH.LogInfo` |
| `Console.ReadLine` | **BANNED** | Blocks forever. There is no Console stdin. | None (Use `args`) |
| `Environment.Exit` | **BANNED** | Kills the entire Streamer.bot application. | `return false` |
| `dynamic` | **BANNED** | Runtime binding risks, missing DLLs. | Reflection / JObject |
| `Thread.Sleep` | **CAUTION** | Blocks the worker thread. | `Task.Delay` (if async) or `CPH.Wait` |
| `Process.Start` | **CAUTION** | Can spawn orphaned windows. | Ensure `CreateNoWindow = true` |

## 18. Recipes

### 18.1. Safe HTTP GET

```csharp
public string Get(string url)
{
    try
    {
        var response = _http.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
    }
    catch (Exception ex)
    {
        CPH.LogError($"[Http] Failed GET {url}: {ex.Message}");
        return null;
    }
}
```

## 19. Final Checklist

Before finishing a task:

1. Did I wrap `Execute` in try-catch?
2. Did I remove all `dynamic` types?
3. Did I use `GetArg` for all inputs?
4. Are my file writes atomic?
5. Is my `HttpClient` static?
6. Did I use `StringBuilder` for loops?
7. Did I reset my static state?

# Streamer.bot C# Coding Rules - Part 4: Reference & Mechanics

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 20. Common CPH API Reference

This list covers the most essential methods. _Note: CPH methods are case-sensitive in some contexts, always use PascalCase._

### 20.1. Messaging & Chat

| Method                                               | Description                                                         |
| :--------------------------------------------------- | :------------------------------------------------------------------ |
| `CPH.SendMessage(string msg)`                        | Sends a message to the broadcast account's primary chat.            |
| `CPH.SendTwitchMessage(string msg)`                  | Explicitly sends to Twitch.                                         |
| `CPH.SendYouTubeMessage(string msg)`                 | Explicitly sends to YouTube.                                        |
| `CPH.TwitchReplyToMessage(string msg, string msgId)` | Replies to a specific chat message (Twitch).                        |
| `CPH.SendTwitchMessage(string msg)`                  | **PREFERRED** for Twitch. Aliases `SendMessage` but clearer intent. |
| `CPH.SendYouTubeMessage(string msg)`                 | **PREFERRED** for YouTube.                                          |

> [!TIP]
> Always use platform-specific methods (e.g., `SendTwitchMessage`) over generic `SendMessage` when you need specific features like replies or channel targeting.

### 20.2. Variables & State

| Method                                                                     | Description                                         |
| :------------------------------------------------------------------------- | :-------------------------------------------------- |
| `CPH.SetGlobalVar(string name, object val, bool persist)`                  | Sets a global variable. `persist=true` saves to DB. |
| `CPH.GetGlobalVar<T>(string name, bool persist)`                           | Gets a global variable.                             |
| `CPH.UnsetGlobalVar(string name, bool persist)`                            | Removes a global variable.                          |
| `CPH.SetTwitchUserVar(string user, string name, object val, bool persist)` | Sets a var tied to a specific Twitch user.          |
| `CPH.GetTwitchUserVar<T>(string user, string name, bool persist)`          | Gets a user-specific var.                           |

### 20.3. Actions & Flow

| Method                                                    | Description                                                                |
| :-------------------------------------------------------- | :------------------------------------------------------------------------- |
| `CPH.RunAction(string actionName, bool runImmediately)`   | Runs another action by name.                                               |
| `CPH.RunActionById(string actionId, bool runImmediately)` | Runs another action by ID (safer if names change).                         |
| `CPH.EnableAction(string name)`                           | Enables an action.                                                         |
| `CPH.DisableAction(string name)`                          | Disables an action.                                                        |
| `CPH.Wait(int ms)`                                        | Pauses execution safely (non-blocking to the app, blocking to the thread). |

### 20.4. OBS & Streaming

| Method                                                                      | Description                                                                           |
| :-------------------------------------------------------------------------- | :------------------------------------------------------------------------------------ |
| `CPH.ObsIsConnected(int connectionIdx)`                                     | Checks if OBS is connected.                                                           |
| `CPH.ObsSetSourceVisibility(string scene, string source, bool visible)`     | Toggles source visibility.                                                            |
| `CPH.ObsSetScene(string sceneName)`                                         | Switches the active scene.                                                            |
| `CPH.ObsGetSceneItemProperties(string scene, string item)`                  | **Critical** for getting positions/transforms.                                        |
| `CPH.ObsSendRaw(string requestType, string jsonPayload, int connectionIdx)` | **Advanced**: Send raw WebSocket requests for missing features (Filters, Transforms). |

> [!WARNING]
> `ObsSendRaw` is powerful but dangerous. Always validate your JSON payload.

## 21. Data Standards

Consistency prevents "works on my machine" bugs.

### 21.1. Time & Dates

- **Storage**: ALWAYS store dates in **UTC** (`DateTime.UtcNow`).
- **Format**: ISO 8601 String (`yyyy-MM-ddTHH:mm:ssZ`) is the safest for JSON/Files.
- **Display**: Convert to Local Time ONLY when displaying to the user.

```csharp
// BAD
string badDate = DateTime.Now.ToString(); // Depends on system locale (MM/dd vs dd/MM)

// GOOD
string goodDate = DateTime.UtcNow.ToString("o"); // 2023-10-05T14:30:00.0000000Z
```

### 21.2. Paths & Separators

- **Windows Only**: Streamer.bot is Windows-only, but `Path.Combine` is still required.
- **Normalization**: User inputs might use `/` or `\`. Normalize before comparing.

```csharp
string cleanPath = Path.GetFullPath(inputPath).TrimEnd(Path.DirectorySeparatorChar);
```

## 22. Global Variable Persistence Scopes

Understanding `persisted` is critical for data integrity.

| Scope        | `persisted = false`                             | `persisted = true`                                   |
| :----------- | :---------------------------------------------- | :--------------------------------------------------- |
| **Lifetime** | Until Streamer.bot closes/restarts.             | **Forever** (Saved to `database.db`).                |
| **Speed**    | Extremely fast (In-memory).                     | Slower (Disk I/O).                                   |
| **Usage**    | Session caches, temporary flags, loop counters. | Configs, User Points, Leaderboards, Long-term stats. |
| **Risk**     | Lost on crash/restart.                          | Persists through crashes.                            |

> [!TIP]
> Use `persisted=false` for high-frequency updates (e.g., download progress), and `persisted=true` only for the final result.

### 22.1. The Unset Trap

`CPH.UnsetGlobalVar` MUST match the `persisted` flag of the `Set`.

- If you `Set(..., persist: true)`, you **MUST** `Unset(..., persist: true)`.
- calling `Unset(..., persist: false)` will NOT remove the database entry, and it will reappear on restart.

## 23. Debugging Techniques

You cannot attach a debugger. Use these techniques.

### 23.1. The "Args Dumper"

When you don't know what data an event provides, drop this at the top of your `Execute`.

```csharp
// Dump all args to the log
foreach (var kvp in args)
{
    CPH.LogInfo($"[DEBUG] Key: {kvp.Key} | Type: {kvp.Value?.GetType().Name} | Val: {kvp.Value}");
}
```

### 23.2. Visual Studio "Mock" Debugging

1. Create a Console Project in VS.
2. Interface your logic (see Rule 15.1 in Part 3).
3. Create a `MockCPH` that prints to Console.
4. Run your code in VS with the Mock.
5. **Only paste the Logic Class back into Streamer.bot.**

## 24. Performance Limits

- **Execution Time**: If an action runs for >60 seconds, check your loops.
- **Variables**: Retrieving 1000 global vars one-by-one is slow. Use a single JSON object stored in one variable if they are related.
- **Timers**: Do not use `System.Timers.Timer` inside C#. Use Streamer.bot's native "Timers" tab or `CPH.Wait()` loops if necessary (with exit conditions).

# Streamer.bot C# Coding Rules - Part 5: Advanced Patterns

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 25. Cross-Action Communication

Passing data between C# actions is often necessary but unsafe if done loosely.

### 25.1. The Argument Hand-off

To pass data to another action, inject it into the `args` dictionary _before_ calling `RunAction`.

- **Method**: `CPH.SetArgument(string key, object val)`
- **Scope**: These arguments are ephemeral and exist only for the lifetime of the called action chain.

```csharp
// Action A (Caller)
CPH.SetArgument("targetUser", "Ashton");
CPH.SetArgument("pointsToGive", 100);
CPH.RunAction("GivePointsAction", true); // runImmediately=true to wait for it (usually)
```

### 25.2. Returning Data

Actions cannot "return" values directly. Use a shared Global Variable or a wrapper service if you need a return value, OR use a shared mutable object if running synchronously in the same memory space (risky).
**Recommendation**: Use `CPH.SetGlobalVar("LastAction_Result", value, false)` for simple returns.

## 26. The Shared Library Pattern

Instead of duplicating code, create a "Library Action" that contains compiled classes.

1. Create an Action named `[Lib] CommonServices`.
2. Add a C# Code sub-action.
3. Define your classes `public class MySharedService { ... }`.
4. **Compile** this action once on startup (Tick "Run on Startup" or run manually).
5. **Issue**: Types defined in one action are NOT automatically visible in another unless they are in the _same_ AppDomain assembly context, which SB handles loosely.
   - **Better Approach**: Use a real DLL for complex shared logic (See Part 1).
   - **Script Approach**: Copy-paste the class into a redundant region or use `// css_include` if supported (rarely stable in SB).
   - **Wait**: Actually, `CPH` methods are shared, but your _custom classes_ are isolated per compilation unit. **Do NOT assume you can instantiate `ClassFromActionA` inside `ActionB`.** You must use **Interfaces** or **JSON Serialization** to pass complex tokens.

## 27. Complex State Management

Storing complex objects (like a `PlayerProfile` class) in Global Variables.

### 27.1. Serialization Strategy

Streamer.bot's `SetGlobalVar` handles primitives well. For Objects, it relies on internal serialization which might differ from your code.
**Rule**: Serialize to JSON String before saving.

```csharp
// Save
string json = JsonConvert.SerializeObject(myProfile);
CPH.SetGlobalVar("Profile_Ashton", json, true);

// Load
string json = CPH.GetGlobalVar<string>("Profile_Ashton", true);
var profile = JsonConvert.DeserializeObject<PlayerProfile>(json);
```

### 27.2. The "Updates" List

If you are tracking a list of items (e.g., `ViewerQueue`), do not read/modify/write in a long loop without locking, as other threads might modify it.

- **Pattern**:
  1. Lock
  2. Get JSON
  3. Deserialize
  4. Modify
  5. Serialize
  6. Set Global Var
  7. Unlock

# Streamer.bot C# Coding Rules - Part 6: Troubleshooting & Diagnostics

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 28. Symptom Decision Tree

When the bot acts up, use this tree to identify the root cause.

### 28.1. "The Bot is Frozen / Unresponsive"

- **Cause**: Infinite Loop or Blocking Call on the Main Thread.
- **Check**:
  - Did you use `while(true)` without `CPH.Wait()`?
  - Did you call `Thread.Sleep()`?
  - Did you call a synchronous Network Method that timed out?

### 28.2. "The Action Runs but Nothing Happens (Silent Failure)"

- **Cause**: Uncaught Exception or Logic Gap.
- **Check**:
  - Do you have the Global Try/Catch?
  - Check the "Log" tab in SB.
  - Verify `args` names match exactly (Case Sensitive).

### 28.3. "Old Code is Running" (The Ghost Action)

- **Cause**: SB caches compiled assemblies. Sometimes recompiling doesn't replace the old one in memory if handles are held.
- **Fix**: Restart Streamer.bot.

## 29. Common Compilation Errors

| Error                                                       | Meaning                     | Fix                                                 |
| :---------------------------------------------------------- | :-------------------------- | :-------------------------------------------------- |
| `CS0246` The type or namespace name 'X' could not be found  | Missing Reference or Using. | Check `// css_ref` paths. Check `using` statements. |
| `CS0103` The name 'X' does not exist in the current context | Typo or Variable Scope.     | Check variable spelling and definition.             |
| `CS1002` ; expected                                         | Syntax Error.               | Check end of lines.                                 |
| `CS1513` } expected                                         | Mismatched Braces.          | Check your Region or Class closing braces.          |

## 30. Debugging "Headless"

Since you have no debugger:

1. **The "Breadcrumb" Method**: `CPH.LogInfo("Step 1"); ... CPH.LogInfo("Step 2");`
2. **The "Dumper"**:
   ```csharp
   CPH.LogInfo(JsonConvert.SerializeObject(myComplexObject));
   ```
3. **The "Isolate"**: Copy the code to a new Blank Action and test just that function.

# Streamer.bot C# Coding Rules - Part 7: Testing & Mocking

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 31. The Visual Studio Workflow

Writing C# in the small SB text window is painful. Use Visual Studio (Community).

1. Create a **Console App (.NET Framework 4.7.2 or 4.8)**.
2. Add reference to `System.Net.Http` and `Newtonsoft.Json`.
3. Create an Interface `IStreamerBot` (See Part 3).
4. Implement `MockCPH` (See below).
5. Write your Logic Class using `IStreamerBot`.
6. **Deploy**: Copy ONLY the Logic Class and the `CPHInline` adapter to Streamer.bot.

## 32. The Mock Implementation

Copy this into your VS Project to simulate the bot.

```csharp
public class MockCPH : IStreamerBot
{
    private Dictionary<string, object> _globals = new Dictionary<string, object>();

    public void LogInfo(string msg) => Console.WriteLine($"[INFO] {msg}");
    public void LogWarn(string msg) => Console.WriteLine($"[WARN] {msg}");
    public void LogError(string msg) => Console.WriteLine($"[ERROR] {msg}");

    public void SetGlobalVar(string name, object val, bool persist) => _globals[name] = val;
    public object GetGlobalVar<T>(string name, bool persist)
    {
        if (_globals.ContainsKey(name)) return (T)_globals[name];
        return default(T);
    }

    // Add other methods as needed
    public void SendTwitchMessage(string msg) => Console.WriteLine($"[TWITCH] {msg}");
}
```

## 33. The Adapter Pattern

In your VS project, you code against `IStreamerBot`. In Streamer.bot, you wrap `CPH`:

```csharp
// Inside Streamer.bot
public class SbAdapter : IStreamerBot
{
    private IInlineInvokeProxy _cph;
    public SbAdapter(IInlineInvokeProxy cph) { _cph = cph; }
    public void LogInfo(string m) => _cph.LogInfo(m);
    // ... forward all calls
}
```

## 34. Unit Testing

If you use this pattern, you can write NUnit/xUnit tests against your Logic Class using the `MockCPH`. This is the ONLY way to verify complex logic (like points calculations, queues, games) without spamming your live chat.

# Streamer.bot C# Coding Rules - Part 8: Deployment & Sharing

> [!IMPORTANT]
> These rules are **MANDATORY**.
> This is Part 1 of 10. You must follow all `rules_*.md`.

## 35. Export Preparation

Before you share your action (Export -> Copy Import Code), you MUST sanitize it.

### 35.1. Strip Secrets

- **Check**: Look for any hardcoded API Keys, OAuth tokens, or Webhook URLs.
- **Rule**: Replace them with placeholders `YOUR_KEY_HERE` or, better yet, switch to using `args`/Global Variables so the user enters them in the UI.

### 35.2. File Path Abstraction

- **Problem**: `C:\Users\Ashton\Documents\ ` won't exist on another user's PC.
- **Fix**: Use relative paths or ask the user to configure a path variable.

## 36. Documentation

An Import Code is opaque. You must provide a `README.txt` or a description field.

- **Required**:
  1. **Dependencies**: List any DLLs required (See Part 1).
  2. **Variables**: List all Global Variables the user needs to set manually.
  3. **Commands**: List any Chat Commands triggers that map to the actions.

## 37. Versioning

- **Internal**: Keep `const string Version = "1.0.1";` in your C# code.
- **External**: When posting to the repository/Discord, include the version in the filename or post title.

## 38. The "One-Click" Ideal

Try to make your action self-initializing.

- **Init Action**: Create a specific "Setup" action that creates necessary Global Variables with default values if they are missing.
- **Instruction**: "Run the [Setup] action once after importing."

# Streamer.bot C# Code Templates

> This is Part 1 of 10. You must follow all `rules_*.md`.

> [!NOTE]
> Use these templates to start your actions. They follow all the Rules.

## 1. Basic Action (The Wrapper)

Use this for simple, one-file scripts.

```csharp
using System;
using System.Collections.Generic;

// css_ref System.Core.dll

public class CPHInline
{
    private const string ActionName = "MyFeature";

    public bool Execute()
    {
        try
        {
            CPH.LogInfo($"[{ActionName}] Starting...");

            // Your Code Here

            return true;
        }
        catch (Exception ex)
        {
            CPH.LogError($"[{ActionName}] Failed: {ex.Message}");
            return false;
        }
    }
}
```

## 2. Service-Based Action (Best Practice)

Use this for complex logic.

```csharp
using System;
using System.Collections.Generic;

// css_ref System.Net.Http.dll
// css_ref Newtonsoft.Json.dll

public class CPHInline
{
    public bool Execute()
    {
        try
        {
            var service = new MyService(CPH);
            service.Run(args);
            return true;
        }
        catch (Exception ex)
        {
            CPH.LogError($"[MyService] Critical Error: {ex.Message}");
            return false;
        }
    }
}

public class MyService
{
    private readonly IInlineInvokeProxy _cph;

    public MyService(IInlineInvokeProxy cph)
    {
        _cph = cph;
    }

    public void Run(Dictionary<string, object> args)
    {
        _cph.LogInfo("Service Running...");
        // Logic...
    }
}
```

## 3. Background Job (Long Running)

For checking APIs or timers.

```csharp
public class CPHInline
{
    private volatile bool _keepRunning = true;

    public bool Execute()
    {
        try
        {
            while (_keepRunning)
            {
                // Do Work
                CPH.LogInfo("Checking...");

                // WAIT IS MANDATORY
                CPH.Wait(5000);

                // Optional: Check a global var to kill the loop externally
                // if (CPH.GetGlobalVar<bool>("StopJob")) break;
            }
            return true;
        }
        catch (Exception ex)
        {
           CPH.LogError($"Job Crashing: {ex.Message}");
           return false;
        }
    }
}
```

# Streamer.bot CPH API Cheatsheet

> This is Part 1 of 10. You must follow all `rules_*.md`.

> [!TIP]
> Keep this open while coding.

## Variables

| Method                                                        | Returns | Notes                                               |
| :------------------------------------------------------------ | :------ | :-------------------------------------------------- |
| `GetGlobalVar<T>(string name, bool persist)`                  | `T`     | Returns default(T) if missing. Case-sensitive name. |
| `SetGlobalVar(string name, object val, bool persist)`         | `void`  | `persist=true` writes to DB (slow).                 |
| `UnsetGlobalVar(string name, bool persist)`                   | `void`  | **MUST** match persistence of Set.                  |
| `GetTwitchUserVar<T>(string user, string name, bool persist)` | `T`     | `user` is login name.                               |

## Messaging

| Method                                        | Notes                               |
| :-------------------------------------------- | :---------------------------------- |
| `SendMessage(string msg)`                     | Sends to broadcast target. Generic. |
| `SendTwitchMessage(string msg)`               | **Preferred** for Twitch.           |
| `TwitchReplyToMessage(string msg, string id)` | Requires MsgId from args.           |
| `SendYouTubeMessage(string msg)`              | **Preferred** for YouTube.          |

## Actions

| Method                                        | Notes                                                |
| :-------------------------------------------- | :--------------------------------------------------- |
| `RunAction(string name, bool runImmediately)` | `runImmediately=true` blocks until finish (usually). |
| `SetArgument(string key, object val)`         | Sets arg for _next_ queued action or sub-action.     |

## OBS

| Method                                                              | Notes                                     |
| :------------------------------------------------------------------ | :---------------------------------------- |
| `ObsIsConnected(int idx)`                                           | 0 is usually default connection.          |
| `ObsSetScene(string name)`                                          | Switches scene.                           |
| `ObsSetSourceVisibility(string scene, string source, bool visible)` | Basic toggle.                             |
| `ObsSendRaw(string request, string payload, int idx)`               | **Advanced**. Use for Filters/Transforms. |

## Utils

| Method                     | Notes                                     |
| :------------------------- | :---------------------------------------- |
| `Wait(int ms)`             | **Mandatory** in loops. Pauses execution. |
| `LogInfo(string msg)`      | Visible in Bot Log.                       |
| `LogError(string msg)`     | Highlights Red in Log. Use for Try/Catch. |
| `EscapeString(string str)` | URL Encoding.                             |
