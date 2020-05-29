﻿using System.Collections.Generic;
using System.IO.Ports;
using GalaSoft.MvvmLight;
using NKnife.Channels.Serial;

namespace NKnife.Channels.SerialKnife.Views.Controls
{
    public class ConfigPanelViewModel : ViewModelBase
    {
        private bool _IsHexShow = true;
        private bool _IsFormatText = false;

        public ConfigPanelViewModel()
        {
            SelectBaudRate = SerialUtils.DefaultBaudRate;
            SelectStopBit = SerialUtils.DefaultStopBit;
            SelectDataBit = SerialUtils.DefaultDataBit;
            SelectParity = SerialUtils.DefaultParity;
        }

        public ushort SelectBaudRate { get; set; }

        public ushort SelectStopBit { get; set; } = 1;

        public ushort SelectParity { get; set; } = 1;

        public ushort SelectDataBit { get; set; } = 3;

        public bool IsDTR { get; set; } = false;

        public bool IsRTS { get; set; } = false;

        public bool IsHexShow
        {
            get { return _IsHexShow; }
            set { Set(() => IsHexShow, ref _IsHexShow, value); }
        }

        public bool IsFormatText
        {
            get { return _IsFormatText; }
            set { Set(() => IsFormatText, ref _IsFormatText, value); }
        }

        public int BufferSpace { get; set; } = 64;

        /// <summary>
        ///     导出Serial配置
        /// </summary>
        public SerialConfig Export(ushort port)
        {
            var config = new SerialConfig(port);

            var list0 = new List<object>();
            list0.AddRange(SerialUtils.BaudRates);
            config.BaudRate = int.Parse(list0[SelectBaudRate].ToString());

            var list1 = new List<object>();
            list1.AddRange(SerialUtils.StopBits);
            config.StopBit = (StopBits)list1[SelectStopBit];

            var list2 = new List<object>();
            list2.AddRange(SerialUtils.Parities);
            config.Parity = (Parity)list2[SelectParity];

            var list3 = new ushort[] { 5, 6, 7, 8 };
            config.DataBit = list3[SelectDataBit];

            config.DtrEnable = IsDTR;
            config.RtsEnable = IsRTS;
            config.ReadBufferSize = BufferSpace;

            return config;
        }
    }
}