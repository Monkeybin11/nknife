using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests.XML
{
    public class XmlHelperTest
    {
        [Fact]
        public void PathSeparatorCharTest()
        {
            var a = Path.AltDirectorySeparatorChar;
            var b = Path.DirectorySeparatorChar;
            var c = Path.PathSeparator;
            var d = Path.VolumeSeparatorChar;
            a.Should().Be('/');
            b.Should().Be('\\');
            c.Should().Be(';');
            d.Should().Be(':');
        }
    }
}
