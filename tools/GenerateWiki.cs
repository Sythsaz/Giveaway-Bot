using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace WikiGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string sourceFile = @"..\GiveawayBot.cs";
                string wikiDir = @"..\..\Giveaway-Bot.wiki";

                if (args.Length >= 1) sourceFile = args[0];
                if (args.Length >= 2) wikiDir = args[1];

                if (!File.Exists(sourceFile))
                {
                    Console.WriteLine(string.Format("Error: Source file not found at {0}", Path.GetFullPath(sourceFile)));
                    Environment.Exit(1);
                }

                if (!Directory.Exists(wikiDir))
                {
                    Directory.CreateDirectory(wikiDir);
                }

                string code = File.ReadAllText(sourceFile);
                var items = ParseDocumentation(code);

                // 1. Generate API Reference (Original)
                GenerateApiReference(items, Path.Combine(wikiDir, "API-Reference.md"));

                // 2. Generate Configuration Reference
                GenerateConfigReference(items, code, Path.Combine(wikiDir, "Configuration-Reference.md"));

                // 3. Generate Commands Reference
                GenerateCommandsReference(items, code, Path.Combine(wikiDir, "Commands.md"));

                Console.WriteLine("Wiki generation complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal Error: " + ex.ToString());
                Environment.Exit(1);
            }
        }

        // --- Core Parsing ---

        static List<DocItem> ParseDocumentation(string code)
        {
            var items = new List<DocItem>();
            string[] lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            string currentTypeName = null;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                // 1. Parse Comments
                if (line.StartsWith("///"))
                {
                    var docBlock = new StringBuilder();
                    while (i < lines.Length && lines[i].Trim().StartsWith("///"))
                    {
                        docBlock.AppendLine(lines[i].Trim().Substring(3).Trim());
                        i++;
                    }

                    // Get the signature line
                    if (i < lines.Length)
                    {
                        string signature = lines[i].Trim();
                        // Skip attributes
                        while (signature.StartsWith("[") && i < lines.Length - 1)
                        {
                            i++;
                            signature = lines[i].Trim();
                        }

                        var docItem = ParseDocBlock(docBlock.ToString(), signature);
                        if (docItem != null)
                        {
                             // Track current class/interface for later members
                             if (docItem.Type == "Class" || docItem.Type == "Interface")
                             {
                                 currentTypeName = docItem.Name;
                             }
                             else
                             {
                                 docItem.ContainingType = currentTypeName;
                             }
                             items.Add(docItem);
                        }
                    }
                }
                // 2. Parse Constants (even without comments, for Commands)
                else if (line.Contains("const string"))
                {
                     var match = Regex.Match(line, @"public const string (\w+)\s*=\s*""(.+)""");
                     if (match.Success)
                     {
                         items.Add(new DocItem {
                             Type = "Constant",
                             Name = match.Groups[1].Value,
                             DefaultValue = match.Groups[2].Value,
                             Signature = line
                         });
                     }
                }
                // 3. Parse Command Triggers (e.g., Triggers.Add("command:!enter", ...))
                else if (line.Contains("Triggers.Add") && line.Contains("command:"))
                {
                     // Broaden matching for Triggers.Add to allow flexible whitespace/args
                     // e.g. Triggers.Add("command:!enter", GiveawayConstants.Action_Enter)
                     // or   Triggers.Add( "command:!enter" , GiveawayConstants.Action_Enter, ... )
                     var match = Regex.Match(line, @"Triggers\.Add\s*\(\s*""command:(.+?)""\s*,\s*GiveawayConstants\.Action_(.+?)\b");
                     if (match.Success)
                     {
                         items.Add(new DocItem {
                             Type = "CommandTrigger",
                             Name = match.Groups[2].Value, // Action (e.g. Enter)
                             DefaultValue = match.Groups[1].Value, // Command (e.g. !enter)
                             Signature = line
                         });
                     }
                }
            }
            return items;
        }

        static DocItem ParseDocBlock(string xml, string signature)
        {
            var summaryMatch = Regex.Match(xml, @"<summary>(.*?)</summary>", RegexOptions.Singleline);

            // Clean signature
            if (signature.EndsWith("{")) signature = signature.Substring(0, signature.Length - 1).Trim();
            if (signature.EndsWith(";")) signature = signature.Substring(0, signature.Length - 1).Trim();

            var item = new DocItem
            {
                Signature = signature,
                Summary = CleanText(summaryMatch.Groups[1].Value),
                OriginalXml = xml
            };

            // Parse Defaults for Properties
            // Pattern: public Type Name { get; set; } = DefaultValue;
            if (signature.Contains(" get; set;"))
            {
                item.Type = "Property";
                var parts = signature.Split(new[] { " = " }, StringSplitOptions.None);
                if (parts.Length > 1) item.DefaultValue = parts[1].Trim(';', ' ');

                // Extract Name/Type
                // "public int LogRetentionDays { get; set; }"
                var defPart = parts[0].Replace("{ get; set; }", "").Trim();
                var defTokens = defPart.Split(' ');
                if (defTokens.Length >= 3)
                {
                    item.Name = defTokens[defTokens.Length - 1]; // Last token is name
                    item.DataType = defTokens[defTokens.Length - 2]; // Second to last is type
                }
            }
            else if (signature.Contains("class "))
            {
                item.Type = "Class";
                var parts = signature.Split(' ');
                int idx = Array.IndexOf(parts, "class");
                if (idx != -1 && idx < parts.Length - 1) item.Name = parts[idx + 1];
            }
            else if (signature.Contains("interface "))
            {
                item.Type = "Interface";
                 var parts = signature.Split(' ');
                int idx = Array.IndexOf(parts, "interface");
                if (idx != -1 && idx < parts.Length - 1) item.Name = parts[idx + 1];
            }
            else
            {
                item.Type = "Method";
                // Method parsing logic
                item.Name = GetMethodName(signature); // <-- reusing helper
                 // Parameters
                  var paramMatches = Regex.Matches(xml, @"<param name=""(.*?)"">(.*?)</param>", RegexOptions.Singleline);
                  foreach (Match p in paramMatches)
                  {
                      item.Params.Add(new ParamInfo { Name = p.Groups[1].Value, Description = CleanText(p.Groups[2].Value) });
                  }
                  var returnMatch = Regex.Match(xml, @"<returns>(.*?)</returns>", RegexOptions.Singleline);
                  item.Returns = CleanText(returnMatch.Groups[1].Value);
            }

            var remarksMatch = Regex.Match(xml, @"<remarks>(.*?)</remarks>", RegexOptions.Singleline);
            item.Remarks = CleanText(remarksMatch.Groups[1].Value);

            return item;
        }

        static string CleanText(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return Regex.Replace(input, @"\s+", " ").Trim();
        }

        // --- Generators ---

        static void GenerateApiReference(List<DocItem> items, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# API Reference");
            sb.AppendLine("> Auto-generated from source code comments.");
            sb.AppendLine();
            sb.AppendLine("## Table of Contents");

            // Grouping Logic
            var classes = new List<ClassDoc>();
            ClassDoc currentClass = null;

            foreach (var item in items)
            {
                if (item.Type == "Class" || item.Type == "Interface")
                {
                    currentClass = new ClassDoc { Base = item };
                    classes.Add(currentClass);
                }
                else if (item.Type == "Method" && currentClass != null)
                {
                     // Primitive filtering: Skip logic-less constants/properties in API ref if we want, but user might want them.
                     // For now, let's keep it to Methods as per original request, or include Properties?
                     // Original request was methods. Let's stick to methods for API ref to keep it clean,
                     // OR include Properties if they have Docs.
                     // The previous version only had methods. Let's keep methods.
                     currentClass.Methods.Add(item);
                }
            }

            foreach (var cls in classes) sb.AppendLine(string.Format("- [{0}](#{1})", cls.Base.Name, GetAnchor(cls.Base.Name)));
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();

            foreach (var cls in classes)
            {
                sb.AppendLine(string.Format("## {0}", cls.Base.Name));
                sb.AppendLine(string.Format("> {0}", cls.Base.Summary));
                if (!string.IsNullOrEmpty(cls.Base.Remarks)) { sb.AppendLine(); sb.AppendLine(cls.Base.Remarks); }

                if (cls.Methods.Count > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine("### Methods");
                    sb.AppendLine();
                    foreach (var method in cls.Methods)
                    {
                        var shortName = GetMethodName(method.Signature);
                        sb.AppendLine(string.Format("#### `{0}`", shortName));
                        sb.AppendLine("```csharp");
                        sb.AppendLine(method.Signature);
                        sb.AppendLine("```");
                        sb.AppendLine(method.Summary);

                        if (method.Params.Count > 0)
                        {
                            sb.AppendLine();
                            sb.AppendLine("| Parameter | Description |");
                            sb.AppendLine("| :--- | :--- |");
                            foreach (var p in method.Params) sb.AppendLine(string.Format("| **{0}** | {1} |", p.Name, p.Description));
                        }
                        if (!string.IsNullOrEmpty(method.Returns)) { sb.AppendLine(); sb.AppendLine(string.Format("**Returns**: {0}", method.Returns)); }
                        sb.AppendLine();
                        sb.AppendLine("---");
                    }
                }
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString());
            Console.WriteLine("Generated API-Reference.md");
        }

        static void GenerateConfigReference(List<DocItem> items, string code, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Configuration Reference");
            sb.AppendLine("> Detailed reference for `giveaway_config.json` settings.");
            sb.AppendLine();

            string[] configClasses = new[] { "GiveawayBotConfig", "GiveawayProfileConfig" };

            // Find properties belonging to these classes.
            // Since our parser is linear, we need to associate properties with the last seen class.

            var classProps = items
                .Where(i => i.Type == "Property" && configClasses.Contains(i.ContainingType))
                .GroupBy(i => i.ContainingType)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var className in configClasses)
            {
                List<DocItem> props;
                if (!classProps.TryGetValue(className, out props)) continue;

                sb.AppendLine(string.Format("## {0}", className));
                sb.AppendLine("| Setting | Type | Default | Description |");
                sb.AppendLine("| :--- | :--- | :--- | :--- |");

                foreach (var prop in props)
                {
                    // Clean default value
                    string def = prop.DefaultValue ?? "null";
                    if (def.Contains("new ")) def = "(Complex Object)";

                    string summary = prop.Summary != null ? prop.Summary.Replace("|", "\\|").Replace("\n", " ") : "";
                    sb.AppendLine(string.Format("| **{0}** | `{1}` | `{2}` | {3} |",
                        prop.Name,
                        prop.DataType,
                        def,
                        summary));
                }
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString());
            Console.WriteLine("Generated Configuration-Reference.md");
        }

        static void GenerateCommandsReference(List<DocItem> items, string code, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Commands Reference");
            sb.AppendLine("> List of available chat commands and their trigger patterns.");
            sb.AppendLine();
            sb.AppendLine("## Core Commands");
            sb.AppendLine("| Action | Command Syntax |");
            sb.AppendLine("| :--- | :--- |");

            // Look for constants starting with Cmd_ or CmdPattern_
            // We assume CmdPattern_GiveawayPrefix is available
            string prefix = "!giveaway ";
            var prefixItem = items.FirstOrDefault(i => i.Name == "CmdPattern_GiveawayPrefix");
            if (prefixItem != null) prefix = prefixItem.DefaultValue;


            foreach (var item in items.Where(i => i.Type == "Constant"))
            {
                if (item.Name.StartsWith("Cmd_"))
                {
                    string cmdValues = item.DefaultValue;
                    string actionName = item.Name.Replace("Cmd_", "");
                    sb.AppendLine(string.Format("| **{0}** | `{1}{2}` |", actionName, prefix, cmdValues));
                }
            }

            foreach (var item in items.Where(i => i.Type == "CommandTrigger").OrderBy(i => i.Name))
            {
                // Group by action if needed, or just list them
                // item.Name = Action (e.g. Enter)
                // item.DefaultValue = Command (e.g. !enter)
                sb.AppendLine(string.Format("| **{0}** | `{1}` |", item.Name, item.DefaultValue));
            }

            sb.AppendLine();
            sb.AppendLine("## Aliases");
            sb.AppendLine("Most commands also work with `!ga` prefix.");

            File.WriteAllText(path, sb.ToString());
            Console.WriteLine("Generated Commands.md");
        }


        // --- Helpers ---

        static string GetAnchor(string name)
        {
            return name.ToLower().Replace(" ", "-").Replace(".", "").Replace("`", "");
        }

        static string GetMethodName(string signature)
        {
             var parts = signature.Split(new[] { '(', ' ' }, StringSplitOptions.RemoveEmptyEntries);
             for (int k = 0; k < parts.Length; k++)
             {
                 if (parts[k].Contains("(")) return parts[k].Split('(')[0];
             }
             return signature;
        }
    }

    class ClassDoc
    {
        public DocItem Base { get; set; }
        public List<DocItem> Methods { get; set; }

        public ClassDoc()
        {
            Methods = new List<DocItem>();
        }
    }

    class DocItem
    {
        public string Type { get; set; }
        public string Name { get; set; } // Class Name, Method Name, Property Name
        public string DataType { get; set; } // int, bool, string (for properties)
        public string DefaultValue { get; set; } // For properties/constants
        public string Signature { get; set; }
        public string Summary { get; set; }
        public string Returns { get; set; }
        public string Remarks { get; set; }
        public string OriginalXml { get; set; }
        public List<ParamInfo> Params { get; set; }
        public string ContainingType { get; set; } // <- new

        public DocItem()
        {
            Params = new List<ParamInfo>();
        }
    }

    class ParamInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
