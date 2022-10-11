using System;
using Business.TestDouble.Testable;

namespace Business
{
    public class AppEntryPoint
    {
        public static void MegaMain()
        {
            CreditCardProcessor.Instance.SetConnection();
            Database.Instance.SetConnection();
            CustomerRepository.Instance.SetConnection();
        }
    }

    public class CreditCard
    {
        private readonly string _number;

        public CreditCard(string number)
        {
            _number = number;
        }

        public void Charge(decimal amount)
        {
            Database.Instance.ValidateCard(_number);
            Customer c = Database.Instance.GetCustomer(_number);
            CustomerRepository.Instance.Validate(c);
            CreditCardProcessor.Instance.Charge(_number);
        }
    }

    public sealed class CreditCardProcessor
    {
        private static volatile CreditCardProcessor instance;
        private static object syncRoot = new object();

        private CreditCardProcessor()
        {
        }

        public static CreditCardProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CreditCardProcessor();
                    }
                }
                return instance;
            }
        }

        private bool connectionEstablished;

        public void SetConnection()
        {
            connectionEstablished = true;
        }

        public void Validate(Customer customer)
        {
        }

        public void Charge(string number)
        {
            if (!connectionEstablished)
                throw new InvalidOperationException();
        }
    }


    public sealed class CustomerRepository
    {
        private static volatile CustomerRepository instance;
        private static object syncRoot = new object();

        private CustomerRepository()
        {
        }

        public static CustomerRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CustomerRepository();
                    }
                }
                return instance;
            }
        }

        public void Validate(Customer customer)
        {
            if (!connectionEstablished)
                throw new InvalidOperationException();
        }

        private bool connectionEstablished;

        public void SetConnection()
        {
            connectionEstablished = true;
        }
    }

    public sealed class Database
    {
        private static volatile Database instance;
        private static object syncRoot = new object();

        private Database()
        {
        }

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Database();
                    }
                }
                return instance;
            }
        }

        public void ValidateCard(string cardNumber)
        {
            if (!connectionEstablished)
                throw new InvalidOperationException();
        }

        private bool connectionEstablished;

        public void SetConnection()
        {
            connectionEstablished = true;
        }

        public Customer GetCustomer(string number)
        {
            return new Customer(new DbGateway(), new Logger());
        }
    }
}