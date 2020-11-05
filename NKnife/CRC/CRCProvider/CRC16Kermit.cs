using System;
using NKnife.CRC.Abstract;
using NKnife.CRC.Status;

namespace NKnife.CRC.CRCProvider
{
    internal class CRC16Kermit : AbsCRCProvider
    {
        private uint[] _crcTable = new uint[256];
        private uint _polynomial = 0x8408;

        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        public CRC16Kermit(uint Polynomial = 0x8408)
        {
            this.Polynomial = Polynomial;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                uint value = 0;
                uint temp = i;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (value >> 1) ^ this.Polynomial;
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                this.CRCTable[i] = value;
            }
        }

        public override CRCStatus GetCRC(byte[] OriginalArray)
        {
            CRCStatus status = base.GetCRC(OriginalArray);
            ushort crc = 0;

            for (uint i = 0; i < OriginalArray.Length; ++i)
            {
                byte index = (byte)(crc ^ OriginalArray[i]);
                crc = (ushort)((crc >> 8) ^ this.CRCTable[index]);
            }
            var crcArray = BitConverter.GetBytes(crc);

            Array.Reverse(crcArray);

            var crcTemp = Convert.ToUInt32(crcArray[1].ToString("X") + crcArray[0].ToString("X"), 16);
            base.GetCRCStatus(ref status, crcTemp, crcArray, OriginalArray);
            return status;
        }
    }
}