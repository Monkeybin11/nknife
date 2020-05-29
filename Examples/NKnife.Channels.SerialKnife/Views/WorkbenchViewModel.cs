using System.Collections.ObjectModel;
using NKnife.Channels.SerialKnife.Services;

namespace NKnife.Channels.SerialKnife.Views
{
    public class WorkbenchViewModel
    {
        public WorkbenchViewModel(SerialChannelService serialChannelService)
        {
            SerialChannelService = serialChannelService;
        }

        public SerialChannelService SerialChannelService { get; }
        public ObservableCollection<SerialPortViewModel> SerialPortViewModels { get; } = new ObservableCollection<SerialPortViewModel>(); 
    }
}
