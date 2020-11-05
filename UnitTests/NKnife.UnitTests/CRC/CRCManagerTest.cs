using System;
using System.Linq;
using NKnife.CRC;
using NKnife.CRC.Enum;
using Xunit;

namespace NKnife.UnitTests.CRC
{
    /// <summary>
    ///     This is a test class for CRCManagerTest and is intended
    ///     to contain all CRCManagerTest Unit Tests
    /// </summary>
    public class CRCManagerTest
    {
        private readonly CRCFactory _factory = new CRCFactory();

        [Fact]
        public void CRC16_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16);
            var source = "1234567890";
            var expectedCheckSum = "C57A";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;

            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16);
            var source = "1234567890";
            var expectedCheckSum = "4F74";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;

            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC32_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC32);
            var source = "1234567890";
            var expectedCheckSum = "261DAEE5";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC32_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC32);
            var source = "1234567890";
            var expectedCheckSum = "DC936EB1";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0x0000_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0x0000);
            var source = "1234567890";
            var expectedCheckSum = "D321";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0x0000_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0x0000);
            var source = "1234567890";
            var expectedCheckSum = "48E6";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0x1D0F_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0x1D0F);
            var source = "1234567890";
            var expectedCheckSum = "57D8";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0x1D0F_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0x1D0F);
            var source = "1234567890";
            var expectedCheckSum = "B928";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0xFFFF_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0xFFFF);
            var source = "1234567890";
            var expectedCheckSum = "3218";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16CCITT_0xFFFF_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16CCITT_0xFFFF);
            var source = "1234567890";
            var expectedCheckSum = "59EA";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16Kermit_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16Kermit);
            var source = "1234567890";
            var expectedCheckSum = "6B28";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16Kermit_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16Kermit);
            var source = "1234567890";
            var expectedCheckSum = "6163";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC8_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC8);
            var source = "1234567890";
            var expectedCheckSum = "38";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16Modbus_ASCII_Test()
        {
            _factory.DataFormat = OriginalDataFormat.ASCII;
            var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
            var source = "1234567890";
            var expectedCheckSum = "C20A";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC16Modbus_HEX_Test()
        {
            _factory.DataFormat = OriginalDataFormat.HEX;
            var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
            var source = "CC0900097600040000";
            var expectedCheckSum = "0C47";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            var actual = provider.GetCRC(source);
            Assert.Equal(expectedCheckSum, actual.CrcHexadecimal);
            Assert.Equal(expectedCheckSumValue, actual.CrcDecimal);
            Assert.Equal(expectedFullData, actual.FullDataHexadecimal);
            Assert.True(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [Fact]
        public void CRC_SourceStringEmpty_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.HEX;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
                var source = "";
                var actual = provider.GetCRC(source);
            });
        }

        [Fact]
        public void CRC_SourceArrayEmpty1_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.ASCII;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
                var source = new byte[0];
                provider.GetCRC(source);
            });
        }

        [Fact]
        public void CRC_SourceArrayEmpty2_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.ASCII;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
                byte[] source = null;
                provider.GetCRC(source);
            });
        }

        [Fact]
        public void BytesToHexString1_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.ASCII;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);
                var source = new byte[0];
                var actual = provider.BytesToHexString(source);
            });
        }

        [Fact]
        public void BytesToHexString2_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.ASCII;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);

                byte[] source = null;
                var actual = provider.BytesToHexString(source);
            });
        }

        [Fact]
        public void HexStringToBytes_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _factory.DataFormat = OriginalDataFormat.ASCII;
                var provider = _factory.CreateProvider(CRCProvider.CRC16Modbus);

                var actual = provider.HexStringToBytes("");
            });
        }
    }
}