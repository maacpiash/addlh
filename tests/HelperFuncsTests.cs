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
using System.Linq;
using Xunit;
using static AddLicenseHeader.Program;

namespace AddLicenseHeader.Tests
{
    public class HelperFuncsTests
    {
        [Theory]
        [InlineData("-h/h/-d/d", "h", "d")]
        [InlineData("-h/-d/d", null, "d")]
        [InlineData("-d/d", null, "d")]
        [InlineData("-h/h/-d", "h", null)]
        [InlineData("-h/h", "h", null)]
        public void CanCheckArgs(string argValues, string licPath, string dirPath)
        {
            string[] args = argValues.Split('/');
            Assert.Equal((licPath, dirPath), CheckArgs(args));

        }

        [Theory]
        [InlineData("cs-ha-rp", "/* - * - */", "//", "/* - * cs- * ha- * rp- */")]
        [InlineData("/*-cs-ha-rp-*/", "/* - * - */", "//", "/*-cs-ha-rp-*/")]
        [InlineData("// cs-// ha-// rp", "/* - * - */", "//", "// cs-// ha-// rp")]
        [InlineData("py-th-on", "\"\"\"-\t-\"\"\"", "#", "\"\"\"-\tpy-\tth-\ton-\"\"\"")]
        [InlineData("\"\"\"-py-th-on-\"\"\"", "\"\"\"-\t-\"\"\"", "#", "\"\"\"-py-th-on-\"\"\"")]
        [InlineData("# py-# th-# on", "\"\"\"-\t-\"\"\"", "#", "# py-# th-# on")]
        public void CanConvertToComments(string license, string comm, string ind, string result)
        {
            string[] oldLines = license.Split('-');
            string[] commenters = comm.Split('-');
            string[] newLines = result.Split('-');
            Assert.Equal(newLines, ConvertToComments(oldLines, commenters, ind));
        }

        [Fact]
        public void CanConvertToComments_WhenLicenseFileIsEmpty()
        {
            var nothing = new string[] {};
            Assert.Equal(nothing, ConvertToComments(nothing, nothing, ""));
        }

        [Fact]
        public void CanSelectOnlySrcFiles()
        {
            var paths = new string[]
            {
                "a.cs", "b.ts", "c.js", "d.tsx", "e.jsx", "f.py", "g.c", "h.java",
                "i.cpp", "j.h", "k.cc", "l.txt", "m.mp4", "o.ogg", "p.png", "q.rst"
            };
            var files = OnlySrcFiles(paths);
            Assert.Equal(paths.Take(11), files);
        }
    }
}
