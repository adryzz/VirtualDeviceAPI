using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    public class LEDDevice : IVirtualDevice
    {
        public byte Id => id;
        byte id = 0x00;

        public DeviceFunction Function => DeviceFunction.LED;

        public IRootDevice RootDevice => rootDevice;
        IRootDevice rootDevice = null;

        public event EventHandler DataReceived;

        public LEDDevice(byte ID, IRootDevice root)
        {
            id = ID;
            rootDevice = root;
        }

        public void SetState(bool state)
        {
            if (state)
            {
                RootDevice.SendPacket(new byte[] { 0x02, Id, 0x01, 0x00 });
            }
            else
            {
                RootDevice.SendPacket(new byte[] { 0x02, Id, 0x00, 0x00 });
            }
        }

        public void Toggle()
        {
            RootDevice.SendPacket(new byte[] { 0x02, Id, 0x02, 0x00 });
        }

        public void ReceivePacket(byte[] packet)
        {
            DataReceived?.Invoke(this, new EventArgs());
            //do nothing since this is an LED and no packets should arrive
        }
    }
}
