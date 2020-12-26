using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualDeviceAPI.DeviceTypes
{
    public class ButtonDevice : IVirtualDevice
    {
        public byte Id => id;
        byte id = 0x00;

        public DeviceFunction Function => DeviceFunction.Button;

        public IRootDevice RootDevice => rootDevice;
        IRootDevice rootDevice = null;

        public bool Pressed = false;

        public event EventHandler DataReceived;

        public ButtonDevice(byte ID, IRootDevice root)
        {
            id = ID;
            rootDevice = root;
        }

        public void ReceivePacket(byte[] packet)
        {
            Pressed = BitConverter.ToBoolean(packet, 2);

            DataReceived?.Invoke(this, new EventArgs());
        }
    }
}
