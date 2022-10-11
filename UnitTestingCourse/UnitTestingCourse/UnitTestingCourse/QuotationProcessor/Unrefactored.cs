using System;
using System.IO;

namespace Business.QuotationProcessor
{
    public class QuotationsProcessor
    {
        private readonly string _filePath;

        public QuotationsProcessor(string filePath)
        {
            _filePath = filePath;
        }

        public decimal GetAmount(string currencyPair, decimal amount, DateTime when)
        {
            string[] lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(';');
                string currency = parts[1];
                decimal rate = decimal.Parse(parts[2]);
                DateTime dt = DateTime.Parse(parts[3]);
                if (dt == when && currency == currencyPair)
                {
                    return amount * rate;
                }
            }
            return -1;
        }

        public void AddQuotation(string currencyPair, decimal rate, DateTime timestamp)
        {
            string[] lines = File.ReadAllLines(_filePath);
            if (lines.Length > 0)
            {
                int lastIndex = int.Parse(lines[lines.Length - 1].Split(';')[0]);
                string newRecord = $"{lastIndex};{currencyPair};{rate};{timestamp}";
                File.AppendAllLines(_filePath, new[] {newRecord});
            }
            else
            {
                string newRecord = $"{1};{currencyPair};{rate};{timestamp}";
                File.WriteAllLines(_filePath, new[] {newRecord});
            }
        }
    }
}