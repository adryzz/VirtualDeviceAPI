using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualDeviceAPI.DeviceTypes
{
    public class ThermometerDevice : IVirtualDevice
    {
        public byte Id => id;
        byte id = 0x00;

        public DeviceFunction Function => DeviceFunction.Thermometer;

        public IRootDevice RootDevice => rootDevice;
        IRootDevice rootDevice = null;

        public int Temperature = 0;

        public event EventHandler DataReceived;

        public ThermometerDevice(byte ID, IRootDevice root)
        {
            id = ID;
            rootDevice = root;
        }

        public void ReceivePacket(byte[] packet)
        {
            Temperature = BitConverter.ToInt16(packet, 2);

            DataReceived?.Invoke(this, new EventArgs());
        }
    }
}
