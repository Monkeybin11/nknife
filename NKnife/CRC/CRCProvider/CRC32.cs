using System;
using NKnife.CRC.Abstract;
using NKnife.CRC.Status;

namespace NKnife.CRC.CRCProvider
{
    internal class CRC32 : AbsCRCProvider
    {
        private uint[] _crcTable = new uint[256];
        private uint _polynomial = 0xedb88320;

        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        public CRC32(uint Polynomial = 0xedb88320)
        {
            this.Polynomial = Polynomial;
            uint temp = 0;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                temp = i;
                for (uint j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ this.Polynomial;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                this.CRCTable[i] = temp;
            }
        }

        public override CRCStatus GetCRC(byte[] OriginalArray)
        {
            CRCStatus status = base.GetCRC(OriginalArray);
            uint crc = 0xffffffff;
            for (int i = 0; i < OriginalArray.Length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ OriginalArray[i]);
                crc = (uint)((crc >> 8) ^ _crcTable[index]);
            }
            var crcTemp = ~crc;
            var crcArray = BitConverter.GetBytes(crcTemp);

            base.GetCRCStatus(ref status, crcTemp, crcArray, OriginalArray);
            return status;
        }
    }
}