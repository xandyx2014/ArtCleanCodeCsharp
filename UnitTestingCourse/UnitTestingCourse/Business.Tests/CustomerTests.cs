using System;
using Business.TestDouble.Testable;
using Business.Tests.TestDoubles;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class CustomerTestsWithMockingFramework
    {
        [Test]
        public void CalculateWage_HourlyPayed_ReturnsCorrectWage()
        {
            var gateway = Substitute.For<IDbGateway>();
            var workingStatistics = new WorkingStatistics() {PayHourly = true, HourSalary = 100, WorkingHours = 10};
            
            gateway.GetWorkingStatistics(Arg.Any<int>()).ReturnsForAnyArgs(workingStatistics);
            
            const decimal expectedWage = 100 * 10;
            var sut = new Customer(gateway, Substitute.For<ILogger>());

            decimal actual = sut.CalculateWage(Arg.Any<int>());

            Assert.That(actual, Is.EqualTo(expectedWage).Within(0.1));
        }

        [Test]
        public void CalculateWage_ThrowsException_Returns0()
        {
            var gateway = Substitute.For<IDbGateway>();
            gateway.GetWorkingStatistics(Arg.Any<int>()).Throws(new InvalidOperationException());

            var sut = new Customer(gateway, Substitute.For<ILogger>());

            decimal actual = sut.CalculateWage(Arg.Any<int>());
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void CalculateWage_PassesCorrectId()
        {
            const int id = 1;
            var gateway = Substitute.For<IDbGateway>();
            gateway.GetWorkingStatistics(id).ReturnsForAnyArgs(new WorkingStatistics());

            var sut = new Customer(gateway, Substitute.For<ILogger>());
            sut.CalculateWage(id);

            gateway.Received().GetWorkingStatistics(id);
        }
    }

    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void CalculateWage_HourlyPayed_ReturnsCorrectWage()
        {
            DbGatewayStub gateway = new DbGatewayStub();
            gateway.SetWorkingStatistics(new WorkingStatistics() {PayHourly = true, HourSalary = 100, WorkingHours = 10});
            
            var sut = new Customer(gateway, new LoggerDummy());

            const int anyId = 1;
            decimal actual = sut.CalculateWage(anyId);

            const decimal expectedWage = 100 * 10;

            Assert.That(actual, Is.EqualTo(expectedWage).Within(0.1));
        }

        [Test]
        public void CalculateWage_PassesCorrectId()
        {
            const int id = 1;

            var gateway = new DbGatewaySpy();
            gateway.SetWorkingStatistics(new WorkingStatistics());

            var sut = new Customer(gateway, new LoggerDummy());

            sut.CalculateWage(id);

            Assert.That(1, Is.EqualTo(gateway.Id));
        }

        [Test]
        public void CalculateWage_PassesCorrectId2()
        {
            const int id = 1;
            var gateway = new DbGatewayMock();
            gateway.SetWorkingStatistics(new WorkingStatistics());

            var sut = new Customer(gateway, new LoggerDummy());

            sut.CalculateWage(id);

            Assert.IsTrue(gateway.VerifyCalledWithProperId(id));
        }
    }
}
