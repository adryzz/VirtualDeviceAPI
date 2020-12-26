using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    public class NullDevice : IVirtualDevice
    {
        public byte Id => id;
        byte id = 0x00;

        public DeviceFunction Function => DeviceFunction.None;

        public IRootDevice RootDevice => rootDevice;
        IRootDevice rootDevice = null;

        public event EventHandler DataReceived;

        public NullDevice(byte ID, IRootDevice root)
        {
            id = ID;
            rootDevice = root;
        }

        public void ReceivePacket(byte[] packet)
        {
            DataReceived?.Invoke(this, new EventArgs());
            //do nothing since this is a device with no function
        }
    }
}
