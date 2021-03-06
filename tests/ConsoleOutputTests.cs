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
using System;
using System.IO;
using System.Linq;
using Xunit;
using static AddLicenseHeader.Program;

namespace AddLicenseHeader.Tests
{
    public class ConsoleOutputTests
    {
        [Fact]
        public void CanMainForHelp() // covers `PrintHelp` and `ShowLicenseText`
        {
            var currentConsoleOut = Console.Out;
            var texts = new string[]
            {
                "addlh (version X.Y.Z)",
                "Usage: addlh [options]\n\nOptions:",
                "-h|-l [header-file-path] (default: \"./LICENSE{.md}\")",
                "-s|-d [source-dir-path] (default: \"./src\")",
                "Examples:",
                "addlh -h ../MIT.txt -s ./src/app",
                "addlh -d ./tests -l ./GPL-2.txt",
                "addlh Copyright (C) 2020  Mohammad Abdul Ahad Chowdhury",
                "This program comes with ABSOLUTELY NO WARRANTY; This is free software,",
                "and you are welcome to redistribute it under certain conditions."
            };

            string[] lines;

            using (var consoleOutput = new ConsoleOutput())
            {
                Main(new string[] { "--help" });
                var output = consoleOutput.GetOuput().Split(Environment.NewLine);
                lines = output.Select(x => x.Trim()).ToArray();
            }

            for (int i = 1; i < texts.Length; i++)
                Assert.Equal(texts[i], lines[i]);
        }

        [Fact]
        public void CanMainIfNoTxtFile()
        {
            var currentConsoleOut = Console.Out;
            string[] lines;

            using (var consoleOutput = new ConsoleOutput())
            {
                Main(new string[] {});
                var output = consoleOutput.GetOuput().Split(Environment.NewLine);
                lines = output.Select(x => x.Trim()).ToArray();
            }

            Assert.Equal("ERROR: header-file not found.", lines[1]);
        }
    }

    // The following code, and instructions on how to use it, were obtained from this page:
    // https://www.codeproject.com/articles/501610/getting-console-output-within-a-unit-test

    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
