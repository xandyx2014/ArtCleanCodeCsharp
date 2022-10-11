using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Business
{
    public class Device
    {
        private readonly IProtocol _protocol;
        private readonly TaskCompletionSource<string> _deviceSearchingTask;

        public Device(IProtocol protocol)
        {
            _protocol = protocol;
            _deviceSearchingTask = new TaskCompletionSource<string>();
        }

        public string PortName { get; private set; }

        public Task<string> Find()
        {
            _protocol.SearchingFinished += Protocol_SearchingFinished;

            Task.Factory.StartNew(() => { _protocol.SearchForDevice(); });

            return _deviceSearchingTask.Task;
        }

        private void Protocol_SearchingFinished(object sender, DeviceSearchingEventArgs e)
        {
            _deviceSearchingTask.SetResult(e.PortName);
        }

        public bool Connect(string port)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_protocol.Connect(port))
                    return true;
            }
            return false;
        }
    }

    public interface IProtocol
    {
        event EventHandler<DeviceSearchingEventArgs> SearchingFinished;
        void SearchForDevice();
        bool Connect(string port);
    }

    public class DeviceProtocol : IProtocol
    {
        public event EventHandler<DeviceSearchingEventArgs> SearchingFinished;

        public void SearchForDevice()
        {
            var portNames = SerialPort.GetPortNames();
            foreach (var portName in portNames)
            {
                if (Connect(portName))
                {
                    SearchingFinished?.Invoke(this, new DeviceSearchingEventArgs(portName));
                    return;
                }
            }
            SearchingFinished?.Invoke(this, new DeviceSearchingEventArgs(null));
        }

        public bool Connect(string port)
        {
            SerialPort sp = new SerialPort();
            sp.Write("Are you there?");
            string response = sp.ReadLine();

            return response == "I'm here";
        }
    }

    public class DeviceSearchingEventArgs
    {
        public DeviceSearchingEventArgs(string portName)
        {
            PortName = portName;
        }

        public string PortName { get; }
    }
}