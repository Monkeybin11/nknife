using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NKnife.Util
{
    public class UtilByte
    {
        /// <summary>
        ///     将int转换为大端模式的字节数组，即高位在前（与<seealso cref="ToIntByBigEndian"/>相对应）。BigEndian是指低地址存放最高有效字节，高位在前（MSB）；而LittleEndian则是低地址存放最低有效字节，高位在后（LSB）。
        /// </summary>
        public static byte[] ToBigEndianByteArray(int value)
        {
            var src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }

        /// <summary>
        ///     将大端模式（高位在前）的byte数组转为int(与<seealso cref="ToBigEndianByteArray"/>相对应)。BigEndian是指低地址存放最高有效字节，高位在前（MSB）；而LittleEndian则是低地址存放最低有效字节，高位在后（LSB）。
        /// </summary>
        public static int ToIntByBigEndian(byte[] array)
        {
            if (array.Length != 4)
                return -1;
            return ((array[0] & 0xff) << 24) | ((array[1] & 0xff) << 16) | ((array[2] & 0xff) << 8) | ((array[3] & 0xff) << 0);
        }

        /// <summary>
        /// 16进制格式string转byte[]。
        /// </summary>
        /// <param name="hexString">一个描述16进制数据的字符串，每个16进制数据可能是全写，也可能是简写，如0xFF,或者FF。</param>
        /// <param name="separator">每个字节之间的间隔符，默认是空格。可以没有空格符。</param>
        /// <exception cref="ArgumentException">输入16进制数据字符串长度不符合要求</exception>
        /// <exception cref="FormatException">输入16进制数据字符串不符合16进制格式</exception>
        public static byte[] ConvertToBytes(string hexString, string separator = " ")
        {
            if (string.IsNullOrEmpty(separator) && hexString.Length % 2 != 0)
                throw new ArgumentException("输入16进制数据字符串长度不符合要求", nameof(hexString));
            bool hasHexFlag = hexString.Contains("0x");//是否有“0x”标志
            string[] hexArray;
            if (!string.IsNullOrEmpty(separator))
            {
                hexArray = hexString.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
                if (hasHexFlag)
                {
                    for (int i = 0; i < hexArray.Length; i++)
                    {
                        hexArray[i] = hexArray[i].Substring(2);
                    }
                }
            }
            else
            {
                int jump = hasHexFlag ? 4 : 2;
                var list = new List<string>();
                for (int i = 0; i < hexString.Length; i = i + jump)
                {
                    if (hexString.Length >= i + jump)
                    {
                        var sub = hexString.Substring(i, jump);
                        if (hasHexFlag)
                            sub = sub.Substring(2);
                        list.Add(sub);
                    }
                }

                hexArray = list.ToArray();
            }

            byte[] bs = new byte[hexArray.Length];

            for (int i = 0; i < hexArray.Length; i++)
            {
                var b = byte.Parse(hexArray[i], NumberStyles.HexNumber);
                bs[i] = b;
            }

            return bs;
        }

        private static readonly Regex _ByteCharRegex = new Regex("^[A-Fa-f0-9]+$");

        private static bool IsHexDigit(string str)
        {
            return _ByteCharRegex.IsMatch(str);
        }

        /// <summary>
        /// 比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        public static bool Compare(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length)
                return false;
            return b1.Where((t, i) => t.Equals(b2[i])).Any();
        }

        /// <summary>
        /// 用memcmp比较字节数组
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        public static int MemoryCompare(byte[] b1, byte[] b2)
        {
            IntPtr retval = memcmp(b1, b2, new IntPtr(b1.Length));
            return retval.ToInt32();
        }

        /// <summary>
        /// memcmp API
        /// </summary>
        /// <param name="b1">字节数组1</param>
        /// <param name="b2">字节数组2</param>
        /// <param name="count"></param>
        /// <returns>如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。</returns>
        [DllImport("msvcrt.dll")]
        private static extern IntPtr memcmp(byte[] b1, byte[] b2, IntPtr count);
    }
}
