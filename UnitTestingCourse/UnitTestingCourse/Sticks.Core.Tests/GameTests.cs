using System;
using NUnit.Framework;

namespace Sticks.Core.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Ctor_LessThen10Sticks_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Game(9, Player.Machine));
        }

        [Test]
        public void Ctor_ValidParams_GameIsInCorrectState()
        {
            int sticks = 10;
            Player player = Player.Machine;

            var sut = new Game(sticks, player);

            Assert.That(sut.NumberOfSticks, Is.EqualTo(sticks));
            Assert.That(sut.Turn, Is.EqualTo(player));
        }

        [Test]
        [TestCase(0)]
        [TestCase(4)]
        public void HumanMakesMove_InvalidNumberOfSticks_Throws(int take)
        {
            var sut = new Game(10, Player.Human);
            Assert.Throws<ArgumentException>(() => sut.HumanMakesMove(take));
        }

        [Test]
        public void HumanMakesMove_TurnOfMachine_Throws()
        {
            var sut = new Game(10, Player.Machine);
            Assert.Throws<InvalidOperationException>(() => sut.HumanMakesMove(1));
        }

        [Test]
        [TestCase(1, 9)]
        [TestCase(2, 8)]
        [TestCase(3, 7)]
        public void HumanMakesMove_CorrectGameState(int takes, int remains)
        {
            var sut = new Game(10, Player.Human);
            sut = sut.HumanMakesMove(takes);

            Assert.That(sut.NumberOfSticks, Is.EqualTo(remains));
            Assert.That(sut.Turn, Is.EqualTo(Player.Machine));
        }

        [Test]
        public void MachineMakesMove_TurnOfHuman_Throws()
        {
            var sut = new Game(10, Player.Human);
            Assert.Throws<InvalidOperationException>(() => sut.MachineMakesMove());
        }

        [Test]
        public void HumanMakesMove_TakesTheLast2_HumanLost()
        {
            Player winner = Player.Human;

            Game sut = ReduceTo2SticksStartingWithHuman();
            sut.GameOver += (sender, player) => winner = player;

            sut = sut.HumanMakesMove(2);

            Assert.That(sut.IsGameOver, Is.EqualTo(true));
            Assert.That(winner, Is.EqualTo(Player.Machine));
        }

        [Test]
        public void MachineMakesMove_TakesTheLast2_MachineLost()
        {
            Player winner = Player.Machine;

            Game sut = ReduceTo3SticksStartingWithMachine();
            sut.GameOver += (sender, player) => winner = player;

            sut = sut.MachineMakesMove();

            Assert.That(sut.IsGameOver, Is.EqualTo(true));
            Assert.That(winner, Is.EqualTo(Player.Human));
        }

        private Game ReduceTo3SticksStartingWithMachine()
        {
            var gen = new PredictableGenerator();
            gen.SetNumber(Game.MaxToTake);

            var sut = new Game(11, Player.Machine, gen);
            sut = sut.MachineMakesMove(); //8
            sut = sut.HumanMakesMove(Game.MinToTake); //7
            sut = sut.MachineMakesMove(); //4
            sut = sut.HumanMakesMove(Game.MinToTake); //3
            return sut;
        }

        [Test]
        [TestCase(1, false)]
        [TestCase(2, true)]
        public void IsGameOver_SomeSticks_ReturnsCorrectResults(
                        int takeWhenTwoSticksInGame,
                        bool isOver)
        {
            Game sut = ReduceTo2SticksStartingWithHuman();
            sut = sut.HumanMakesMove(takeWhenTwoSticksInGame);

            bool result = sut.IsGameOver();

            Assert.That(result, Is.EqualTo(isOver));
        }

        [Test]
        public void HumanMakesMove_TakeSticksMoreThanInTheGame_Throws()
        {
            Game sut = ReduceTo2SticksStartingWithHuman();
            Assert.Throws<ArgumentException>(() => sut.HumanMakesMove(Game.MaxToTake));
        }

        private static Game ReduceTo2SticksStartingWithHuman()
        {
            var gen = new PredictableGenerator();
            gen.SetNumber(Game.MinToTake);

            var sut = new Game(10, Player.Human, gen);
            sut = sut.HumanMakesMove(Game.MaxToTake); //7
            sut = sut.MachineMakesMove(); //6
            sut = sut.HumanMakesMove(Game.MaxToTake); //3
            sut = sut.MachineMakesMove(); //2
            return sut;
        }

        [Test]
        [TestCase(1, 9)]
        [TestCase(2, 8)]
        [TestCase(3, 7)]
        public void MachineMakesMove_CorrectGameState(int takes, int remains)
        {
            var gen = new PredictableGenerator();
            gen.SetNumber(takes);

            int taken = 0;
            var sut = new Game(10, Player.Machine, gen);
            sut.MachineMoved += (s, args) => taken = args.Taken;

            sut = sut.MachineMakesMove();

            Assert.That(sut.NumberOfSticks, Is.EqualTo(remains));
            Assert.That(takes, Is.EqualTo(taken));
            Assert.That(sut.Turn, Is.EqualTo(Player.Human));
        }

        [Test]
        public void Ctor_NullGenerator_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(10, Player.Machine, null));
        }
    }

    public class PredictableGenerator : ICanGenerateNumbers
    {
        private int _number;

        public int Next(int min, int max)
        {
            return _number;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}
