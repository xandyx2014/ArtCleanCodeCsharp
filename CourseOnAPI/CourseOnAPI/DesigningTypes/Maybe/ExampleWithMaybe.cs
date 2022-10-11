using System;
using System.Collections.Generic;
using System.Linq;

namespace DesigningTypes.Maybe {
    public class Example {
        readonly CustomerProxy1 _noMaybeProxy = new CustomerProxy1();
        readonly CustomerProxy2 _maybeProxy = new CustomerProxy2();

        public void ProcessCustomerNoMaybe() {
            Customer c = _noMaybeProxy.GetCustomer(customer => customer.Age > 18);
        }

        public void ProcessCustomerMaybe() {
            Maybe<Customer> c = _maybeProxy.GetCustomer(customer => customer.Age > 18);
        }
    }

    public class CustomerProxy1 {
        public Customer GetCustomer(Func<Customer, bool> predicate) {
            var customers = new List<Customer>();
            var result = customers.First(predicate);
            return result;
        }
    }

    public class CustomerProxy2 {
        public Maybe<Customer> GetCustomer(Func<Customer, bool> predicate) {
            var customers = new List<Customer>();
            var result = customers.First(predicate);
            return result;
        }
    }

    public class Customer {
        public int Age { get; set; }
    }
}