using System;

namespace DesigningTypes.SwitchRefactoring.BeforeRefactoring {
    public enum PaymentDeviceModel {
        CashCode,
        MeiAe2800,
        Nri,
        Lcdm,
        Ecdm,
        Sch2,
        Cube4
    }

    /// <summary>
    /// OCP violation.
    /// </summary>
    public class Config {
        public bool IsCashAccepter(PaymentDeviceModel paymentDeviceModel) {
            switch (paymentDeviceModel) {
                case PaymentDeviceModel.CashCode:
                case PaymentDeviceModel.MeiAe2800:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsCoinAccepter(PaymentDeviceModel paymentDeviceModel) {
            switch (paymentDeviceModel) {
                case PaymentDeviceModel.Nri:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsCashDispenser(PaymentDeviceModel paymentDeviceModel) {
            switch (paymentDeviceModel) {
                case PaymentDeviceModel.Lcdm:
                case PaymentDeviceModel.Ecdm:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsCoinDispenser(PaymentDeviceModel paymentDeviceModel) {
            switch (paymentDeviceModel) {
                case PaymentDeviceModel.Cube4:
                case PaymentDeviceModel.Sch2:
                    return true;
                default:
                    return false;
            }
        }

        public int GetDefaultBaudRate(PaymentDeviceModel paymentDeviceModel) {
            throw new NotImplementedException();
        }

        public string GetDefaultSerialPort(PaymentDeviceModel paymentDeviceModel) {
            throw new NotImplementedException();
        }

        public bool CanDispenseChange(PaymentDeviceModel deviceModel) {
            return IsCashDispenser(deviceModel) || IsCoinDispenser(deviceModel);
        }
    }

    public class UserApi {
        private readonly PaymentDeviceModel deviceModel;

        public UserApi(PaymentDeviceModel deviceModel) {
            this.deviceModel = deviceModel;
        }

        public bool CanDispenseChange() {
            var config = new Config();
            return config.CanDispenseChange(deviceModel);
        }
    }
}