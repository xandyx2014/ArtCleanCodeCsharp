using System;
using System.Net.Mime;

namespace DesigningTypes.PropVsMethod {
    public enum Mode {
        AlarmOn,
        AlarmOff
    }
    
    /// <summary>
    /// класс должен обладать высокой связностью,
    /// иначе непонятно какие свойства надо устанавливать для методов
    /// </summary>
    public class Device {
        private int baudRate;
        private TimeSpan timeout;
        private Mode mode;

        public int BaudRate {
            get { return baudRate; }
            set { baudRate = value; }
        }

        public TimeSpan Timeout {
            get { return timeout; }
            set { timeout = value; }
        }

        public Mode Mode {
            get { return mode; }
            set { mode = value; }
        }

        public Device() {
            BaudRate = 9600;
            Timeout = TimeSpan.FromMinutes(1);
            Mode = Mode.AlarmOn;
        }

        public void Connect(string serialPort) {}
    }
    
    /// <summary>
    /// если необходим только один параметр,
    /// а остальные опциональные
    /// </summary>
    public class Device2 {
        public void Connect(string serialPort) { }
        public void Connect(string serialPort, int baudRate) { }
        public void Connect(string serialPort, int baudRate, TimeSpan timeout) { }
        public void Connect(string serialPort, int baudRate, TimeSpan timeout, Mode mode) { }
        public void Connect(string serialPort, int baudRate, Mode mode) { }
        public void Connect(string serialPort, TimeSpan timeout) { }
        public void Connect(string serialPort, TimeSpan timeout, Mode mode) { }
        public void Connect(string serialPort, Mode mode) { }
    }

    /// <summary>
    /// если поменять дефолт, то без рекомпиляции на клиенте - дефолт останется тем же
    /// </summary>
    public class Device3 {
        /// <summary>
        /// если добавить сюда новый аргумент - поломка в runtime без рекомпиляции
        /// </summary>        
        public void Connect(string serialPort, int baudRate = 9600, int timeoutInSeconds = 60, Mode mode = Mode.AlarmOn) {

        }

        
        public void Connect(string serialPort, int baudRate = 9600, int timeoutInSeconds = 60, Mode mode = Mode.AlarmOn, 
                            string newParameter = "") {

        }
    }

    public class Client {
        public void Connect() {
            Device3 d = new Device3();
            d.Connect("COM1");
        }
    }
}