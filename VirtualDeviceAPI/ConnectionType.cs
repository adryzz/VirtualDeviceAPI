using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    public enum ConnectionType : byte
    {
        Serial = 0x00,
        WiFi = 0x01,
        Bluetooth = 0x02,
        Unknown = 0xFF
    }
}
