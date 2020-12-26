using System;

namespace VirtualDeviceAPI
{
    public interface IVirtualDevice
    {
        public byte Id { get; }

        public DeviceFunction Function { get; }

        public IRootDevice RootDevice { get; }

        public void ReceivePacket(byte[] packet);

        event EventHandler DataReceived;
    }
}
