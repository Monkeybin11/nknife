using NKnife.CRC.Abstract;
using NKnife.CRC.Status;

namespace NKnife.CRC.CRCProvider
{
    internal class CRC8 : AbsCRCProvider
    {
        private uint[] _crcTable = new uint[256];
        private uint _polynomial = 0xd5;

        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        public CRC8(uint Polynomial = 0xd5)
        {
            this.Polynomial = Polynomial;

            for (int i = 0; i < 256; ++i)
            {
                int curr = i;

                for (int j = 0; j < 8; ++j)
                {
                    if ((curr & 0x80) != 0)
                    {
                        curr = (curr << 1) ^ (byte)this.Polynomial;
                    }
                    else
                    {
                        curr <<= 1;
                    }
                }

                this.CRCTable[i] = (byte)curr;
            }
        }

        public override CRCStatus GetCRC(byte[] OriginalArray)
        {
            CRCStatus status = base.GetCRC(OriginalArray);
            byte crc = 0;

            foreach (byte b in OriginalArray)
            {
                crc = (byte)this.CRCTable[crc ^ b];
            }

            byte[] crcArray = new byte[1] { crc };

            base.GetCRCStatus(ref status, crc, crcArray, OriginalArray);
            return status;
        }
    }
}