using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void FromKopecs_ComparingByLiterals()
        {
            Money m0 = Money.FromKopecs(500);
            Assert.AreEqual(500, m0.AmountInKopecs);
            Assert.AreEqual(5, m0.AmountInRubles);

            Money m1 = Money.FromKopecs(100);
            Assert.AreEqual(100, m1.AmountInKopecs);
            Assert.AreEqual(1, m1.AmountInRubles);

            Money m2 = Money.FromKopecs(50);
            Assert.AreEqual(50, m2.AmountInKopecs);
            Assert.AreEqual(0.5m, m2.AmountInRubles);

            Money m3 = Money.FromKopecs(0);
            Assert.AreEqual(0, m3.AmountInKopecs);
            Assert.AreEqual(0, m3.AmountInRubles);

            Money m4 = Money.FromKopecs(250);
            Assert.AreEqual(250, m4.AmountInKopecs);
            Assert.AreEqual(2.5m, m4.AmountInRubles);
        }

        [Test]
        public void FromRubles_ComparingByLiterals()
        {
            Money m0 = Money.FromRubles(5);
            Assert.AreEqual(500, m0.AmountInKopecs);
            Assert.AreEqual(5, m0.AmountInRubles);

            Money m1 = Money.FromRubles(1);
            Assert.AreEqual(100, m1.AmountInKopecs);
            Assert.AreEqual(1, m1.AmountInRubles);

            Money m2 = Money.FromRubles(0.5m);
            Assert.AreEqual(50, m2.AmountInKopecs);
            Assert.AreEqual(0.5m, m2.AmountInRubles);

            Money m3 = Money.FromRubles(0);
            Assert.AreEqual(0, m3.AmountInKopecs);
            Assert.AreEqual(0, m3.AmountInRubles);

            Money m4 = Money.FromRubles(2.5m);
            Assert.AreEqual(250, m4.AmountInKopecs);
            Assert.AreEqual(2.5m, m4.AmountInRubles);
        }

        [Test]
        public void CompareTo_CorrectComparing()
        {
            Money x1 = Money.FromKopecs(200);
            Money y1 = Money.FromKopecs(200);

            Assert.IsTrue(x1.CompareTo(y1) == 0);

            Money x2 = Money.FromKopecs(100);
            Money y2 = Money.FromKopecs(200);

            Assert.IsTrue(x2.CompareTo(y2) == -1);

            Money x3 = Money.FromKopecs(200);
            Money y3 = Money.FromKopecs(100);

            Assert.IsTrue(x3.CompareTo(y3) == 1);
        }

        [Test]
        public void EqualityOperator_CorrectChecking()
        {
            //equal money
            Money x1 = Money.FromKopecs(100);
            Money y1 = Money.FromKopecs(100);

            Assert.IsTrue(x1 == y1);

            //unequal money
            Money x2 = Money.FromKopecs(50);
            Money y2 = Money.FromKopecs(100);
            Money z2 = Money.FromKopecs(200);

            Assert.IsFalse(x2 == y2);
            Assert.IsFalse(y2 == z2);
        }

        [Test]
        public void InequalityOperator_CorrectChecking()
        {
            //equal money
            Money x1 = Money.FromKopecs(100);
            Money y1 = Money.FromKopecs(100);

            Assert.IsFalse(x1 != y1);

            //unequal money
            Money x2 = Money.FromKopecs(50);
            Money y2 = Money.FromKopecs(100);
            Money z2 = Money.FromKopecs(200);

            Assert.IsTrue(x2 != y2);
            Assert.IsTrue(y2 != z2);
        }

        [Test]
        public void Equals_CorrectChecking()
        {
            Money x1 = Money.FromKopecs(100);
            Money y1 = Money.FromKopecs(100);

            Assert.IsTrue(x1.Equals(y1));

            Money x2 = Money.FromKopecs(100);
            Money y2 = Money.FromKopecs(200);

            Assert.IsFalse(x2.Equals(y2));
        }

        [Test]
        public void PlusOperator_ReturnsCorrectResult()
        {
            Money x = Money.FromKopecs(100);
            Money y = Money.FromKopecs(100);

            Assert.AreEqual(Money.FromKopecs(200), x + y);
        }

        [Test]
        public void MinusOperator_ReturnsCorrectResult()
        {
            Money x = Money.FromKopecs(200);
            Money y = Money.FromKopecs(100);

            Assert.AreEqual(Money.FromKopecs(100), x - y);
        }

        [Test]
        public void Multiply_ReturnsCorrectResult()
        {
            Money x = Money.FromKopecs(250);
            Money y = Money.FromKopecs(300);

            Assert.AreEqual(Money.FromKopecs(750), x * y);
        }

        [Test]
        public void DivideOperator_ReturnsCorrectResult()
        {
            Money x = Money.FromKopecs(60000);
            Money y = Money.FromKopecs(300);

            Assert.AreEqual(Money.FromKopecs(20000), x / y);
        }

        [Test]
        public void GreaterAndGreaterOrEqual_CorrectChecking()
        {
            Money x1 = Money.FromKopecs(200);
            Money y1 = Money.FromKopecs(100);

            Assert.IsTrue(x1 > y1);
            Assert.IsTrue(x1 >= y1);

            Money x2 = Money.FromKopecs(200);
            Money y2 = Money.FromKopecs(200);

            Assert.IsFalse(x2 > y2);
            Assert.IsTrue(x2 >= y2);
        }

        [Test]
        public void LessAndLessOrEqual_CorrectChecking()
        {
            Money x1 = Money.FromKopecs(100);
            Money y1 = Money.FromKopecs(200);

            Assert.IsTrue(x1 < y1);
            Assert.IsTrue(x1 <= y1);

            Money x2 = Money.FromKopecs(200);
            Money y2 = Money.FromKopecs(200);

            Assert.IsFalse(x2 < y2);
            Assert.IsTrue(x2 <= y2);
        }

        [Test]
        public void GetHashCode_DifferentForDifferentValuesAndGoodDistribution()
        {
            var hashes = new Dictionary<int, object>(1000000);
            for (int i = 0; i < 1000000; i++)
            {
                Money x = Money.FromKopecs(i * 100);
                Money y = Money.FromRubles(i);

                int expected = x.GetHashCode();
                int actual = y.GetHashCode();
                Assert.IsTrue(expected == actual, string.Format("Expected:{0}, Actual:{1}", expected, actual));

                if (hashes.ContainsKey(expected))
                {
                    Assert.Fail("Equal hashes generated for unequal values.");
                }
                hashes.Add(expected, null);
            }
        }
    }
}
