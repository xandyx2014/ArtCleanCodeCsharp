using System;
using System.IO;

namespace Business.TestDouble.Untestable
{
    public class Customer
    {
        private readonly Logger _logger = new Logger();

        public decimal CalculateWage(int id)
        {
            DbGateway gateway = new DbGateway();
            WorkingStatistics ws = gateway.GetWorkingStatistics(id);

            decimal wage;
            if (ws.PayHourly)
            {
                wage = ws.WorkingHours * ws.HourSalary;
            }
            else
            {
                wage = ws.MonthSalary;
            }
            _logger.Info($"Customer ID={id}, Wage:{wage}");

            return wage;
        }
    }

    internal class Logger
    {
        public void Info(string s)
        {
            File.WriteAllText(@"C:\tmp:\log.txt", s);
        }
    }

    public class DbGateway
    {
        public WorkingStatistics GetWorkingStatistics(int id)
        {
            //a real gateway can experience any possible problems
            //like "no connection" throwing an exception
            throw new NoConnection();
        }
    }

    public class NoConnection : Exception
    {
    }
}
