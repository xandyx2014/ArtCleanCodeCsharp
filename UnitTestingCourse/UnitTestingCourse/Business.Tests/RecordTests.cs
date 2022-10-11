using System;
using Business.QuotationProcessor.Refactored;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class RecordTests
    {
        [Test]
        public void ParseString_ValidString_ReturnsCorrectRecord()
        {
            int id = 1;
            string currency = "USD/EUR";
            decimal rate = 1.25m;
            string dt = "2017-01-24";

            string input = $"{id};{currency};{rate};{dt}";

            Record result = Record.ParseString(input);

            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.CurrencyPair, Is.EqualTo(currency));
            Assert.That(result.Rate, Is.EqualTo(rate));
            Assert.That(result.Stamp, Is.EqualTo(DateTime.Parse(dt)));
        }
    }
}
