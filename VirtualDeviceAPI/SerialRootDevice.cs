using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using VirtualDeviceAPI.DeviceTypes;

namespace VirtualDeviceAPI
{
    public class SerialRootDevice : IRootDevice
    {
        public ConnectionType ConnectionType => ConnectionType.Serial;

        public bool IsInitialized => isInitialized;

        bool isInitialized = false;

        public bool IsConnected => isConnected;

        bool isConnected = false;

        public List<IVirtualDevice> Subdevices => devices;

        List<IVirtualDevice> devices = null;

        SerialPort port;

        public event EventHandler<VirtualDeviceEventArgs> DeviceAttached;

        public void Initialize(params object[] parameters)
        {
            devices = new List<IVirtualDevice>();
            port = new SerialPort(parameters[0].ToString(), 115200);
            port.DataReceived += Port_DataReceived;
            isInitialized = true;
        }

        public void Connect()
        {
            port.Open();
            isConnected = true;
        }

        public void Disconnect()
        {
            port.Close();
        }

        public void Dispose()
        {
            port.Dispose();
        }

        public void RefreshDevices()
        {
            byte[] packet = { 0x00, 0x00, 0x00, 0x00 };
            port.Write(packet, 0, 4);
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while(port.BytesToRead >= 4 && port.BytesToRead % 4 == 0)
            {
                byte[] packet = new byte[4];
                int read = port.Read(packet, 0, 4);
                InterfaceDebugger.DataReceived(packet);
                switch(packet[0])
                {
                    case 0x00:
                        {
                            break;
                        }
                    case 0x01:
                        {
                            IVirtualDevice device = GetDevice(packet);
                            bool found = false;
                            for(int i = 0; i < Subdevices.Count; i++)
                            {
                                if (Subdevices[i].Id == packet[2]) //if the device already exists, update it
                                {
                                    Subdevices[i] = device;
                                    found = true;
                                }
                            }
                            //if the device doesn't already exist, add it
                            if (!found)
                            {
                                devices.Add(device);
                            }

                            //sort the list on the device ID
                            devices = devices.OrderBy(o => o.Id).ToList();
                            DeviceAttached?.Invoke(this, new VirtualDeviceEventArgs(device));
                            break;
                        }
                    case 0x02:
                        {
                            //device sends a report
                            if (packet[1] > devices.Count - 1)
                            {
                                break;
                            }
                            devices[packet[1]].ReceivePacket(packet);
                            break;
                        }
                }
            }
        }

        private IVirtualDevice GetDevice(byte[] packet)
        {
            DeviceFunction function = (DeviceFunction)packet[1];
            switch (function)
            {
                case DeviceFunction.None:
                    {
                        return new NullDevice(packet[2], this);
                    }
                case DeviceFunction.Unknown:
                    {
                        return new NullDevice(packet[2], this);
                    }
                case DeviceFunction.LED:
                    {
                        return new LEDDevice(packet[2], this);
                    }
                case DeviceFunction.Button:
                    {
                        return new ButtonDevice(packet[2], this);
                    }
                case DeviceFunction.Servo:
                    {
                        return new ServoDevice(packet[2], this);
                    }
                case DeviceFunction.Thermometer:
                    {
                        return new ThermometerDevice(packet[2], this);
                    }
            }

            return new NullDevice(packet[2], this);
        }

        public void SendPacket(byte[] packet)
        {
            InterfaceDebugger.DataSent(packet);
            port.Write(packet, 0, 4);
        }
    }
}
