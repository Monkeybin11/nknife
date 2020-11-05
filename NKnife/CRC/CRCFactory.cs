using NKnife.CRC.Abstract;
using NKnife.CRC.CRCProvider;
using NKnife.CRC.Enum;

namespace NKnife.CRC
{
    public class CRCFactory
    {
        //property
        private AbsCRCProvider Provider { get; set; }

        public OriginalDataFormat DataFormat { get; set; } = OriginalDataFormat.HEX;

        public AbsCRCProvider CreateProvider(Enum.CRCProvider provider)
        {
            this.Provider = null;
            switch (provider)
            {
                case Enum.CRCProvider.CRC16:
                    this.Provider = new CRC16();
                    break;

                case Enum.CRCProvider.CRC32:
                    this.Provider = new CRC32();
                    break;

                case Enum.CRCProvider.CRC8:
                    this.Provider = new CRC8();
                    break;

                case Enum.CRCProvider.CRC8CCITT:
                    this.Provider = new CRC8(0x07);
                    break;

                case Enum.CRCProvider.CRC8DALLASMAXIM:
                    this.Provider = new CRC8(0x31);
                    break;

                case Enum.CRCProvider.CRC8SAEJ1850:
                    this.Provider = new CRC8(0x1D);
                    break;

                case Enum.CRCProvider.CRC8WCDMA:
                    this.Provider = new CRC8(0x9b);
                    break;

                case Enum.CRCProvider.CRC16Modbus:
                    this.Provider = new CRC16Modbus();
                    break;

                case Enum.CRCProvider.CRC16CCITT_0x0000:
                    this.Provider = new CRC16CCITT(0x0000);
                    break;

                case Enum.CRCProvider.CRC16CCITT_0xFFFF:
                    this.Provider = new CRC16CCITT(0xFFFF);
                    break;

                case Enum.CRCProvider.CRC16CCITT_0x1D0F:
                    this.Provider = new CRC16CCITT(0x1D0F);
                    break;

                case Enum.CRCProvider.CRC16Kermit:
                    this.Provider = new CRC16Kermit();
                    break;
            }
            this.Provider.DataFormat = this.DataFormat;

            return this.Provider;
        }
    }
}