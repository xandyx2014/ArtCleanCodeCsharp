namespace DesigningTypes.SwitchRefactoring.AfterRefactoring {
    /// <summary>
    /// OCP.
    /// Хорошо при добавлении новых типов,
    /// плохо при добавлении операций
    /// </summary>
    public abstract class PaymentDeviceModel {
        public abstract bool IsCashAccepter();
        public abstract bool IsCoinAccepter();
        public abstract bool IsCashDispenser();
        public abstract bool IsCoinDispenser();
        public abstract bool CanDispenseChange();
        public abstract int DefaultBaudRate { get; }
        public abstract string DefaultSerialPort { get; }
    }

    public class CashCodePaymentDeviceModel : PaymentDeviceModel {
        public override bool IsCashAccepter() {
            return true;
        }

        public override bool IsCoinAccepter() {
            return false;
        }

        public override bool IsCashDispenser() {
            return false;
        }

        public override bool IsCoinDispenser() {
            return false;
        }

        public override bool CanDispenseChange() {
            return IsCashDispenser() || IsCoinDispenser();
        }

        public override int DefaultBaudRate {
            get { return 9600; }
        }

        public override string DefaultSerialPort {
            get { return "COM1"; }
        }
    }

    public class UserApi {
        private readonly PaymentDeviceModel deviceModel;

        public UserApi(PaymentDeviceModel deviceModel) {
            this.deviceModel = deviceModel;
        }

        public bool CanDispenseChange() {
            return deviceModel.CanDispenseChange();
        }
    }
}