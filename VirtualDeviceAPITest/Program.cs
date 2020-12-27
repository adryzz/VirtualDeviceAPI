using System;
using VirtualDeviceAPI;
using VirtualDeviceAPI.DeviceTypes;

namespace VirtualDeviceAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialRootDevice dev = new SerialRootDevice();
            dev.Initialize("COM8");
            dev.Connect();
            dev.DeviceAttached += Dev_DeviceAttached;
            dev.RefreshDevices();
            Console.ReadKey();
            while (true)
            {
                Console.WriteLine("Devices found: " + dev.Subdevices.Count);
                foreach(IVirtualDevice d in dev.Subdevices)
                {
                    Console.WriteLine("Device " + d.Id + ": Function " + d.Function.ToString());
                    d.DataReceived += D_DataReceived;
                    if (d.Function == DeviceFunction.LED)
                    {
                        ((LEDDevice)d).Toggle();
                    }
                }
                Console.ReadKey();
            }
        }

        private static void Dev_DeviceAttached(object sender, VirtualDeviceEventArgs e)
        {
            Console.WriteLine("Device attached");
        }

        private static void D_DataReceived(object sender, EventArgs e)
        {
            if (((IVirtualDevice)sender).Function == DeviceFunction.Thermometer)
            {
                int temp = ((ThermometerDevice)sender).Temperature;
                Console.WriteLine("Temperature: " + temp + "°C");
            }
        }
    }
}
