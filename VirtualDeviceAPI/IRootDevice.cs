using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    public interface IRootDevice : IDisposable
    {
        public ConnectionType ConnectionType { get; }

        public List<IVirtualDevice> Subdevices { get; }

        public bool IsInitialized { get; }

        public bool IsConnected { get; }

        public void Initialize(params object[] parameters);

        public void Connect();

        public void Disconnect();

        public void RefreshDevices();

        public void SendPacket(byte[] packet);
    }
}
