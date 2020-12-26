using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    public class ServoDevice : IVirtualDevice
    {
        public byte Id => id;
        byte id = 0x00;

        public DeviceFunction Function => DeviceFunction.Servo;

        public IRootDevice RootDevice => rootDevice;
        IRootDevice rootDevice = null;

        public event EventHandler DataReceived;

        public ServoDevice(byte ID, IRootDevice root)
        {
            id = ID;
            rootDevice = root;
        }

        public void SetRotation(byte value)
        {
            RootDevice.SendPacket(new byte[] { 0x02, Id, value, 0x00 });
        }

        public void ReceivePacket(byte[] packet)
        {
            DataReceived?.Invoke(this, new EventArgs());
            //do nothing since this is a passive device and no packets should arrive
        }
    }
}
