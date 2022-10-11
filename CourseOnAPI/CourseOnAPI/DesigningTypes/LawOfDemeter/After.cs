namespace DesigningTypes.LawOfDemeter.After {
    public class Customer {
        public Customer(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
            _wallet = new Wallet(1000m);
        }

        public string FirstName { get; }
        public string LastName { get; }

        private readonly Wallet _wallet;

        public decimal GetPayment(decimal amount) {
            if (_wallet.MoneyAmount >= amount) {
                _wallet.WithdrawMoney(amount);
                return amount;
            }
            return 0;
        }
    }

    public class PaperBoy {
        /// <summary>
        /// 1. Можно менять логику GetPayment независимо от PaperBoy
        /// 2. PaperBoy не имеет прямого доступа к Wallet
        /// 3. Wallet можно менять независимо от PaperBoy и Customer
        /// </summary>
        /// <param name="costOfMagazine"></param>
        /// <param name="customer"></param>
        public void DeliverMagazine(decimal costOfMagazine, Customer customer) {
            decimal payment = customer.GetPayment(costOfMagazine);            
            if (payment == costOfMagazine) {
                //say thank you and get out
            }
            else {
                //come back later
            }
        }
    }
}