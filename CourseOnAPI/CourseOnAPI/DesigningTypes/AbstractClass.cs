namespace DesigningTypes.Abstract {
    public abstract class Device {        
        protected Device() {
            InitializeStep1();
            InitializeStep2();
        }

        protected virtual void InitializeStep1() {}
        protected virtual void InitializeStep2() {}
    }

    public class PaymentDevice : Device {
        protected string str;

        public PaymentDevice() : base() {
            str = string.Empty;
        }

        protected override void InitializeStep1() {
            string newString = str.ToUpper(); //NullReferenceException
        }

        protected override void InitializeStep2() {}
    }
}