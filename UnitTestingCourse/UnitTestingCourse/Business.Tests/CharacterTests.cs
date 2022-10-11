using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class CharacterTests
    {
        #region String Tests

        [Test]
        public void ShouldSetName()
        {
            const string expected = "John";
            Character c = new Character(Type.Elf, expected);

            Assert.That(c.Name, Is.EqualTo(expected));
            Assert.That(c.Name, Is.Not.Empty);
            Assert.That(c.Name, Contains.Substring("ohn"));
        }

        [Test]
        public void ShouldSetNameCaseInsensitive()
        {
            const string expectedUpperCase = "JOHN";
            const string expectedLowerCase = "john";
            Character c = new Character(Type.Elf, expectedUpperCase);

            Assert.That(c.Name, Is.EqualTo(expectedLowerCase).IgnoreCase);
        }

        #endregion

        #region Numerical Tests

        [Test]
        public void DefaultHealthIs100()
        {
            Character c = new Character(Type.Elf);

            const int expectedHealth = 100;
            Assert.That(c.Health, Is.EqualTo(expectedHealth));
            //Assert.That(c.Health, Is.Positive);
            //Assert.That(c.Health, Is.Negative);
        }

        [Test]
        public void Elf_SpeedIsCorrect()
        {
            Character c = new Character(Type.Elf);

            const double expectedHealth = 1.7;
            Assert.That(c.Speed, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void Ork_SpeedIsCorrect()
        {
            Character c = new Character(Type.Ork);

            const double expectedHealth = 1.4;
            Assert.That(c.Speed, Is.EqualTo(expectedHealth));
        }

        [Test]
        //[Ignore("")]
        public void Ork_SpeedIsCorrectWithTolerance()
        {
            Character c = new Character(Type.Ork);

            const double expectedHealth = 0.3 + 1.1;
            //Assert.That(c.Speed, Is.EqualTo(expectedHealth));
            Assert.That(c.Speed, Is.EqualTo(expectedHealth).Within(0.5));            
            Assert.That(c.Speed, Is.EqualTo(expectedHealth).Within(1).Percent);

            //ranges of DateTimes
            //var dt = new DateTime(2000, 1, 1);
            //Assert.That(dt, Is.EqualTo(new DateTime(2001, 1, 1)).Within(TimeSpan.FromDays(366)));
            //Assert.That(dt, Is.EqualTo(new DateTime(2001, 1, 1)).Within(366).Days;
        }

        #endregion

        #region Nulls and Booleans

        [Test]
        public void DefaultNameIsNull()
        {
            Character c = new Character(Type.Elf);
            Assert.That(c.Name, Is.Null);
        }

        [Test]
        public void IsDead_KillCharacter_ReturnsTrue()
        {
            Character c = new Character(Type.Elf);
            c.Damage(500);
            Assert.That(c.IsDead, Is.True);
            //Assert.That(c.IsDead, Is.False);
            //Assert.IsTrue(c.IsDead);
            //Assert.IsFalse(c.IsDead);
        }

        #endregion

        #region Collections
        [Test]
        public void CollectionTests()
        {
            var c = new Character(Type.Elf);
            c.Weaponry.Add("Knife");
            c.Weaponry.Add("Pistol");

            Assert.That(c.Weaponry, Is.All.Not.Empty);
            Assert.That(c.Weaponry, Contains.Item("Knife"));
            Assert.That(c.Weaponry, Has.Exactly(2).Length);
            Assert.That(c.Weaponry, Has.Exactly(1).EndsWith("tol"));
            Assert.That(c.Weaponry, Is.Unique);
            Assert.That(c.Weaponry, Is.Ordered);

            var c2 = new Character(Type.Elf);
            c2.Weaponry.Add("Knife");
            c2.Weaponry.Add("Pistol");

            Assert.That(c.Weaponry, Is.EquivalentTo(c2.Weaponry));
        }
        #endregion

        #region Reference Equality

        [Test]
        public void SameCharacters_AreEqualByReference()
        {
            Character c1 = new Character(Type.Elf);
            Character c2 = c1;

            Assert.That(c1, Is.SameAs(c2));
        }

        #endregion

        #region Types

        [Test]
        public void TestObjectOfCharacterType()
        {
            object c = new Character(Type.Elf);

            Assert.That(c, Is.TypeOf<Character>());
        }

        #endregion

        #region Ranges

        [Test]
        public void DefaultCharacterArmorShouldBeGreaterThan30AndLessThan100()
        {
            Character c = new Character(Type.Elf);

            Assert.That(c.Armor, Is.GreaterThan(30).And.LessThan(100));
            //Assert.That(c.Armor, Is.InRange(30, 100));
        }

        #endregion

        #region Exceptions

        [Test]
        public void Damage_1000_ThrowsArgumentOutOfRange()
        {
            var c = new Character(Type.Elf);

            Assert.Throws<ArgumentOutOfRangeException>(() => c.Damage(1001));
            //Assert.That(() => c.Damage(1001), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Damage_1000_ThrowsArgumentOutOfRange_BadWay()
        {
            var c = new Character(Type.Elf);

            Assert.Throws<ArgumentOutOfRangeException>(() => c.Damage(1001));
            //Assert.That(() => c.Damage(1001), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        #endregion
    }
}
