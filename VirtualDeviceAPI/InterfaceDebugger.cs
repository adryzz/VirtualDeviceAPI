using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VirtualDeviceAPI
{
    static class InterfaceDebugger
    {
        public static void DataSent(byte[] packet)
        {
            string pack = BitConverter.ToString(packet);
            Debug.WriteLine("Sent packet: " + pack);
        }

        public static  void DataReceived(byte[] packet)
        {
            string pack = BitConverter.ToString(packet);
            Debug.WriteLine("Received packet: " + pack);
        }
    }
}
