using System;
using NKnife.CRC.Abstract;
using NKnife.CRC.Status;

namespace NKnife.CRC.CRCProvider
{
    internal class CRC16 : AbsCRCProvider
    {
        //fields

        private uint _polynomial = 0xA001;
        private uint[] _crcTable = new uint[256];

        //property

        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        public CRC16(uint polynomial = 0xA001)
        {
            this.Polynomial = polynomial;

            uint value;
            uint temp;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                value = 0;
                temp = i;
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

        public override CRCStatus GetCRC(byte[] originalArray)
        {
            CRCStatus status = base.GetCRC(originalArray);
            ushort crc = 0;
            for (uint i = 0; i < originalArray.Length; ++i)
            {
                byte index = (byte)(crc ^ originalArray[i]);
                crc = (ushort)((crc >> 8) ^ this._crcTable[index]);
            }
            var crcArray = BitConverter.GetBytes(crc);

            base.GetCRCStatus(ref status, crc, crcArray, originalArray);
            return status;
        }
    }
}