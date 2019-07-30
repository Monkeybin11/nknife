using FluentAssertions;
using NKnife.Chinese;
using NKnife.Util;
using Xunit;

namespace NKnife.UnitTests.Util
{
    public class UtilRandomTest
    {
        /// <summary>
        /// 验证非负正整数是否为 2 的幂级
        /// </summary>
        [Fact]
        public void RandomStringTest()
        {
            int length = 999;
            var str = UtilRandom.GetRandomString(length);
            str.Length.Should().Be(length);
        }


    }
}
