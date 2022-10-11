using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class SerialPortParserTests
    {
        [Test]
        public void ParsePort_COM1_Returns1()
        {
            int result = SerialPortParser.ParsePort("COM1");
            Assert.That(result, Is.EqualTo(1));

            //older style of Asserts in NUnit
            //Assert.AreEqual(1, result);
        }

        [Test]
        public void ParsePort_InvalidFormat_ThrowsInvalidFormatException()
        {
            TestDelegate action = () => SerialPortParser.ParsePort("1");
            Assert.Throws<FormatException>(action);
        }
    }
}
