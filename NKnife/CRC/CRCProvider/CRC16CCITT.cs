using System;
using NKnife.CRC.Abstract;
using NKnife.CRC.Status;

namespace NKnife.CRC.CRCProvider
{
    internal class CRC16CCITT : AbsCRCProvider
    {
        private const uint initail = 4129;
        private uint[] _crcTable = new uint[256];
        private uint _polynomial = 0;

        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        public CRC16CCITT(uint Polynomial = 0)
        {
            this.Polynomial = Polynomial;

            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                uint temp = 0;
                uint value = i << 8;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((temp ^ value) & 0x8000) != 0)
                    {
                        temp = (temp << 1) ^ initail;
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    value <<= 1;
                }

                this.CRCTable[i] = temp;
            }
        }

        public override CRCStatus GetCRC(byte[] OriginalArray)
        {
            CRCStatus status = base.GetCRC(OriginalArray);
            ushort crc = (ushort)this.Polynomial;
            for (int i = 0; i < OriginalArray.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ _crcTable[((crc >> 8) ^ (0xff & OriginalArray[i]))]);
            }
            var crcArray = BitConverter.GetBytes(crc);
            base.GetCRCStatus(ref status, crc, crcArray, OriginalArray);
            return status;
        }
    }
}