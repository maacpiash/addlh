/*
    addlh
    Copyright (C) 2020  Mohammad Abdul Ahad Chowdhury

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using static System.Console;

namespace AddLicenseHeader
{
    public partial class Program
    {
        public static (string, string) CheckArgs(string[] args)
        {
            string[] keys = new string[] { "-h", "-l", "-d", "-s" };
            string[] values = keys.Select(k => ParseValue(args, k)).ToArray();

            string licenseHeader = values[0] is null ? values[1] : values[0]; // "-h"/"-l"/null
            string sourceDir = values[2] is null ? values[3] : values[2]; // "-d"/"-s"/null
            return (licenseHeader, sourceDir);
        }

        public static string ParseValue(string[] args, string key)
        {
            int idx = System.Array.IndexOf(args, key);
            return (idx >= 0) && (args.Length > idx + 1) && (!args[idx + 1].StartsWith("-"))
                ? args[idx + 1]
                : null;
        }

        static string GetVersion()
        {
            return Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion
                .ToString();
        }

        public static void PrintHelp()
        {
            WriteLine("Usage: addlh [options]\n\nOptions:");
            WriteLine("-h|-l [header-file-path] (default: \"./LICENSE{.md}\")");
            WriteLine("-s|-d [source-dir-path] (default: \"./src\")");
            WriteLine("\nExamples:");
            WriteLine("addlh -h ../MIT.txt -s ./src/app");
            WriteLine("addlh -d ./tests -l ./GPL-2.txt");
        }

        public static List<string> ConvertToComments(string[] license, string[] comm, string ind)
        {
            var lines = new List<string>();
            if (license.Length == 0) return lines;
            if ((license.FirstOrDefault().StartsWith(comm[0].Trim()) &&
                    license.LastOrDefault().EndsWith(comm[2].Trim())) // multi-line comment
                || license.All(s => s.StartsWith(ind.Trim()))) // each line commented individually
            {
                lines = new List<string>(license);
            }
            else
            {
                lines = new List<string>();
                lines.Add(comm[0]);
                lines.AddRange(license.Select(s => comm[1] + s));
                lines.Add(comm[2]);
            }

            return lines;
        }

        public static void ShowLicenseText()
        {
            WriteLine("\naddlh Copyright (C) 2020  Mohammad Abdul Ahad Chowdhury");
            WriteLine("This program comes with ABSOLUTELY NO WARRANTY; This is free software,");
            WriteLine("and you are welcome to redistribute it under certain conditions.");
        }

        public static IEnumerable<string> OnlySrcFiles(string[] paths)
        {
            var extensions = new List<string>
            {
                ".cs", ".ts", ".js", ".tsx", ".jsx", ".py", ".c", ".java", ".cpp", ".h", ".cc"
            }; // only the languages that I know — in the order of my skill level!

            return paths
                .Select(path => path.ToLower())
                .Where(s => extensions.Contains(Path.GetExtension(s).ToLower()));
        }
    }
}
