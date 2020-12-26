using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceAPI
{
    /// <summary>
    /// The function of the virtual device
    /// </summary>
    public enum DeviceFunction : byte
    {
        None = 0x00,
        LED = 0x01,
        Button = 0x02,
        Servo = 0x03,
        Thermometer = 0x04,
        Unknown = 0xFF
    }
}
