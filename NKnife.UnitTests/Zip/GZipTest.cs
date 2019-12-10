using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NKnife.UnitTests.Zip
{
    public class GZipTest
    {
        [Fact]
        public void DirTest()
        {
            var basePath = "d:\\zzZ\\";

            FileAndDirectoryGenerator.Run(basePath, 5,5,5);
        }
    }
}
