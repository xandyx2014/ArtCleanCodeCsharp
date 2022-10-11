using System;

namespace Business.QuotationProcessor.Refactored
{
    public class Record
    {
        public int Id { get; }
        public string CurrencyPair { get; }
        public decimal Rate { get; }
        public DateTime Stamp { get; }

        public Record(int id, string currencyPair, decimal rate, DateTime stamp)
        {
            Id = id;
            CurrencyPair = currencyPair;
            Rate = rate;
            Stamp = stamp;
        }

        public Record(string currencyPair, decimal rate, DateTime stamp)
        {
            CurrencyPair = currencyPair;
            Rate = rate;
            Stamp = stamp;
        }

        public static Record ParseString(string record)
        {
            string[] parts = record.Split(';');
            int index = int.Parse(parts[0]);
            string currency = parts[1];
            decimal rate = decimal.Parse(parts[2]);
            DateTime stamp = DateTime.Parse(parts[3]);

            return new Record(index, currency, rate, stamp);
        }

        public override string ToString()
        {
            return $"{Id};{CurrencyPair};{Rate};{Stamp}";
        }
    }
}
