using System;
using System.Linq;
using System.Text;
using NKnife.CRC.Enum;
using NKnife.CRC.Status;

namespace NKnife.CRC.Abstract
{
    public abstract class AbsCRCProvider
    {
        internal string[] _symbol = new string[] { " ", ",", "-", "|" };

        protected abstract uint[] CRCTable { get; }

        protected abstract uint Polynomial { get; set; }

        internal virtual OriginalDataFormat DataFormat { get; set; }

        public virtual CRCStatus GetCRC(string originalData)
        {
            if (string.IsNullOrEmpty(originalData))
            {
                throw new ArgumentNullException("originalData");
            }
            byte[] dataArray = null;

            switch (DataFormat)
            {
                case OriginalDataFormat.ASCII:
                    string filter = _symbol.Aggregate(originalData, (current, symbol) => current.Replace(symbol, ""));
                    dataArray = Encoding.ASCII.GetBytes(filter);
                    break;

                case OriginalDataFormat.HEX:
                    dataArray = HexStringToBytes(originalData);
                    break;
            }
            CRCStatus status = this.GetCRC(dataArray);
            return status;
        }

        public virtual CRCStatus GetCRC(byte[] originalArray)
        {
            if (originalArray == null || originalArray.Length <= 0)
            {
                throw new ArgumentNullException("originalArray");
            }

            return new CRCStatus();
        }

        protected void GetCRCStatus(ref CRCStatus status, uint crc, byte[] crcArray, byte[] originalArray)
        {
            //0xC57A
            //C5 is hi byte
            //7A is low byte

            status.CrcDecimal = crc;
            var crcHex = crc.ToString("X");

            if (crcHex.Length > 2 && crcHex.Length < 4)
            {
                status.CrcHexadecimal = crcHex.PadLeft(4, '0');
            }
            else if (crcHex.Length > 4 && crcHex.Length < 8)
            {
                status.CrcHexadecimal = crcHex.PadLeft(8, '0');
            }
            else
            {
                status.CrcHexadecimal = crcHex;
            }
            byte[] fullData = new byte[originalArray.Length + crcArray.Length];
            Array.Copy(originalArray, fullData, originalArray.Length);
            var reverseCrcArray = new byte[crcArray.Length];
            Array.Copy(crcArray, reverseCrcArray, crcArray.Length);

            Array.Reverse(reverseCrcArray);
            status.CrcArray = reverseCrcArray;

            Array.Copy(reverseCrcArray, reverseCrcArray.GetLowerBound(0), fullData, originalArray.GetUpperBound(0) + 1, reverseCrcArray.Length);

            status.FullDataArray = fullData;

            switch (DataFormat)
            {
                case OriginalDataFormat.ASCII:
                    status.FullDataHexadecimal = Encoding.ASCII.GetString(originalArray) + status.CrcHexadecimal;
                    break;

                case OriginalDataFormat.HEX:
                    status.FullDataHexadecimal = BytesToHexString(originalArray) + status.CrcHexadecimal;
                    break;
            }

            //return status;
        }

        public virtual byte[] HexStringToBytes(string Hex)
        {
            if (string.IsNullOrEmpty(Hex))
            {
                throw new ArgumentNullException("Hex");
            }
            string filter = _symbol.Aggregate(Hex, (current, symbol) => current.Replace(symbol, ""));

            return Enumerable.Range(0, filter.Length)
                              .Where(x => x % 2 == 0)
                              .Select(x => Convert.ToByte(filter.Substring(x, 2), 16))
                              .ToArray();

            //ulong number = ulong.Parse(filter, System.Globalization.NumberStyles.AllowHexSpecifier);
            //return BitConverter.GetBytes(number);
        }

        public virtual string BytesToHexString(byte[] HexArray)
        {
            if (HexArray == null || HexArray.Length <= 0)
            {
                throw new ArgumentNullException("HexArray");
            }

            var result = BitConverter.ToString(HexArray).Replace("-", "");
            return result;
        }
    }
}