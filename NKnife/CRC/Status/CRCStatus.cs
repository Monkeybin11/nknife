using System;
using System.ComponentModel;

namespace NKnife.CRC.Status
{
    [Serializable]
    public class CRCStatus : INotifyPropertyChanged
    {
        private byte[] _crcArray;
        private uint _crcDecimal;
        private string _crcHexadecimal;
        private byte[] _fullDataArray;
        private string _fullDataHexadecimal;

        public string CrcHexadecimal
        {
            get => _crcHexadecimal;
            set
            {
                _crcHexadecimal = value;
                OnPropertyChanged(nameof(CrcHexadecimal));
            }
        }

        public uint CrcDecimal
        {
            get => _crcDecimal;
            set
            {
                _crcDecimal = value;
                OnPropertyChanged(nameof(CrcDecimal));
            }
        }

        public byte[] CrcArray
        {
            get => _crcArray;
            set
            {
                _crcArray = value;
                OnPropertyChanged(nameof(CrcArray));
            }
        }

        public byte[] FullDataArray
        {
            get => _fullDataArray;
            internal set
            {
                _fullDataArray = value;
                OnPropertyChanged(nameof(FullDataArray));
            }
        }

        public string FullDataHexadecimal
        {
            get => _fullDataHexadecimal;
            set
            {
                _fullDataHexadecimal = value;
                OnPropertyChanged(nameof(FullDataHexadecimal));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}