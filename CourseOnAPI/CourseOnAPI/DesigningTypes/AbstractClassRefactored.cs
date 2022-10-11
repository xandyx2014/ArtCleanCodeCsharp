namespace DesigningTypes.AbstractRefactored {
    public abstract class Device {        
        protected Device() {}

        protected internal void Initialize() {
            InitializeStep1();
            InitializeStep2();
        }

        protected virtual void InitializeStep1() {}
        protected virtual void InitializeStep2() {}
    }

    public class PaymentDevice : Device {
        protected string str;

        protected PaymentDevice() : base() {
            str = string.Empty;
        }

        protected override void InitializeStep1() {
            string newString = str.ToUpper(); //Will not throw NullReferenceException
        }

        protected override void InitializeStep2() {}

        public static PaymentDevice Create() {
            var device = new PaymentDevice();
            device.Initialize();

            return device;
        }
    }
}