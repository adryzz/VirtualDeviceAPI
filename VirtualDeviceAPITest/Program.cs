using System;
using VirtualDeviceAPI;

namespace VirtualDeviceAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialRootDevice dev = new SerialRootDevice();
            dev.Initialize("COM4");
            dev.Connect();
            Console.ReadKey();
            dev.RefreshDevices();
            while(true)
            {
                Console.WriteLine("Devices found: " + dev.Subdevices.Count);
                foreach(IVirtualDevice d in dev.Subdevices)
                {
                    Console.WriteLine("Device " + d.Id + ": Function " + d.Function.ToString());
                    d.DataReceived += D_DataReceived;
                }
                Console.ReadKey();
            }
        }

        private static void D_DataReceived(object sender, EventArgs e)
        {
            Console.WriteLine("Data received");
        }
    }
}
