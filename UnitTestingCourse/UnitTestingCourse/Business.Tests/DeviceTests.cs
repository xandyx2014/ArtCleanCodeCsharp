using System;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Business.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        [Test]
        public void Connect_FailedThrice_ThreeTries()
        {
            IProtocol provider = Substitute.For<IProtocol>();
            provider.Connect(Arg.Any<string>()).ReturnsForAnyArgs(false);

//            provider.Connect(Arg.Is("COM1")).Returns(true);
//            provider.Connect(Arg.Is<string>(x=>x.StartsWith("COM"))).Returns(true);

            var sut = new Device(provider);
            sut.Connect(string.Empty);

            provider.Received(3).Connect(Arg.Any<string>());
        }

        [Test]
        public void Find_FoundOnCOM1_ReturnsCOM1()
        {
            IProtocol provider = Substitute.For<IProtocol>();
            var sut = new Device(provider);
            Task<string> task = sut.Find();

            const string portName = "COM1";

            provider.SearchingFinished += Raise.Event<EventHandler<DeviceSearchingEventArgs>>
                                            (provider, new DeviceSearchingEventArgs(portName));

            Assert.That(task.Result, Is.EqualTo(portName));
        }
    }
}
