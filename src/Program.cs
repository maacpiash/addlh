﻿/*
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
using System.IO;
using static System.Console;

namespace AddLicenseHeader
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            WriteLine($"addlh (version {GetVersion()})\n");
            
            #region checking args
            if (System.Array.IndexOf(args, "--help") >= 0)
            {
                PrintHelp();
                ShowLicenseText();
                return;
            }
            var (hfExists, sdExists) = CheckArgs(args);

            string headerFile = hfExists ?? "./LICENSE";
            string srcDir = sdExists ?? "./src";

            if (!File.Exists(headerFile))
            {
                headerFile = "./LICENSE.md";
                if (!File.Exists(headerFile))
                {
                    WriteLine("ERROR: header-file not found.\n");
                    PrintHelp();
                    return;
                }
            }

            if (!Directory.Exists(srcDir))
            {
                WriteLine("ERROR: source-directory not found.\n");
                PrintHelp();
                return;
            }

            WriteLine($"license-header: {headerFile}");
            WriteLine($"source-directory: {srcDir}");
            #endregion

            var files = Directory.GetFiles(srcDir, "*.*", SearchOption.AllDirectories);
            var srcPaths = OnlySrcFiles(files);

            var licenseLines = File.ReadAllLines(headerFile);
            List<string> cLicenseLines, pLicenseLines, comments;

            var c_Comments = new string[] { "/* ", " * ", " */" };
            var pyComments = new string[] { "\"\"\"", "\t", "\"\"\"" };
            cLicenseLines = ConvertToComments(licenseLines, c_Comments, "//");
            pLicenseLines = ConvertToComments(licenseLines, pyComments, "#");

            List<string> lines;
            foreach (var path in srcPaths)
            {
                comments = Path.GetExtension(path).ToLower() == ".py"
                    ? pLicenseLines
                    : cLicenseLines;
                lines = new List<string>(comments);
                lines.AddRange(File.ReadAllLines(path));
                File.WriteAllLines(path, lines);
                WriteLine($"Added license header at the beginning of {path}");
            }
        }
    }
}
