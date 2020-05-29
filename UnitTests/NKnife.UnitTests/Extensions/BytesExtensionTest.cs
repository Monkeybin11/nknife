﻿using System;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    /// <summary>
    ///     BytesExtensionTest 的摘要说明
    /// </summary>
    public class BytesExtensionTest
    {
        [Fact]
        public void IndexOfTestMethod1()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x03};
            int actual = source.Find(target);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void IndexOfTestMethod2()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x04};
            int actual = source.Find(target);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void IndexOfTestMethod3()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {};
            int actual = source.Find(target);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void IndexOfTestMethod4()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target1 = new byte[] {0x00};
            int actual = source.Find(target1);
            Assert.Equal(0, actual);

            var target2 = new byte[] {0x09};
            actual = source.Find(target2);
            Assert.Equal(9, actual);
        }

        [Fact]
        public void IndexOfTestMethod5()
        {
            var source = new byte[] {0x02, 0x03};
            var target = new byte[] {0x02, 0x03};
            int actual = source.Find(target);
            Assert.Equal(0, actual);
            actual = source.Find(source);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void IndexOfTestMethod6()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x08, 0x09};
            int actual = source.Find(target);
            Assert.Equal(8, actual);
        }

        [Fact]
        public void IndexOfTestMethod7()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            int actual = source.Find(target);
            Assert.Equal(3, actual);
        }

        [Fact]
        public void IndexOfTestMethod8()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x09 };
            int actual = source.Find(target);
            Assert.Equal(-1, actual);
        }

        [Fact]
        public void IndexOfTestMethod10()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00, 0x01, 0x02, 0x03, 0x00, 0x01, 0x02, 0x03 };
            var target = new byte[] { 0x02, 0x03 };

            int actual = source.Find(target);
            Assert.Equal(2, actual);

            actual = source.Find(target, 2);
            Assert.Equal(2, actual);

            actual = source.Find(target, 3);
            Assert.Equal(6, actual);

            actual = source.Find(target, 6);
            Assert.Equal(6, actual);

            actual = source.Find(target, 7);
            Assert.Equal(10, actual);

            actual = source.Find(target, 10);
            Assert.Equal(10, actual);

            actual = source.Find(target, 11);
            Assert.Equal(-1, actual);
        }

    }
}