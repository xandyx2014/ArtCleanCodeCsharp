using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.TestDouble.Testable;

namespace Business.Tests.TestDoubles
{
    public class DbGatewayFake:IDbGateway
    {
        private Dictionary<int, WorkingStatistics> _storage = new Dictionary<int, WorkingStatistics>()
        {
            {1, new WorkingStatistics() {PayHourly = true, WorkingHours = 5, HourSalary = 10}},
            {2, new WorkingStatistics() {PayHourly = false, MonthSalary = 500}},
            {3, new WorkingStatistics() {PayHourly = true, WorkingHours = 8, HourSalary = 100}},
        };
        public WorkingStatistics GetWorkingStatistics(int id)
        {
            return _storage[id];
        }

        public bool Connected { get; }
    }
}
