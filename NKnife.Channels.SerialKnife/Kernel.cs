using System.Threading;
using NKnife.Channels.Serial;

namespace NKnife.Channels.SerialKnife
{
    internal class Kernel
    {
        public static void Initialize()
        {
            new Thread(SerialUtils.RefreshSerialPorts).Start();
        }
    }
}