using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NKnife.Util
{
    public class UtilByte
    {
        /// <summary>
        ///     将int转为低字节在后，高字节在前的byte数组
        ///     b[0] = 11111111(0xff) & 01100001
        ///     b[1] = 11111111(0xff) & 00000000
        ///     b[2] = 11111111(0xff) & 00000000
        ///     b[3] = 11111111(0xff) & 00000000
        /// </summary>
        public static byte[] IntToByteArray2(int value)
        {
            var src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }

        /// <summary>
        ///     将高字节在前，低字节在后的byte数组转为int(与IntToByteArray2想对应)
        /// </summary>
        public static int ByteArrayToInt2(byte[] array)
        {
            if (array.Length != 4)
                return -1;
            return ((array[0] & 0xff) << 24) | ((array[1] & 0xff) << 16) | ((array[2] & 0xff) << 8) | ((array[3] & 0xff) << 0);
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
