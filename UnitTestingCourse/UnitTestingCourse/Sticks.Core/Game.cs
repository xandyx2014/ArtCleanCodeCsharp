using System;

namespace Sticks.Core
{
    public interface ICanGenerateNumbers
    {
        int Next(int min, int max);
    }
    public class NumbersGenerator: ICanGenerateNumbers
    {
        private readonly Random _generator = new Random();
        public int Next(int min, int max)
        {
            return _generator.Next(min, max);
        }
    }

    public class Game
    {
        private readonly ICanGenerateNumbers _generator;
        public int NumberOfSticks { get; }
        public Player Turn { get; }

        public Game(int numberOfSticks, Player turn):this(numberOfSticks, turn, new NumbersGenerator())
        {            
        }
        public Game(int numberOfSticks, Player turn, ICanGenerateNumbers generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }
            if (numberOfSticks < 10)
            {
                throw new ArgumentException($"Number of sticks has to be >= 10. You passed:{numberOfSticks}");
            }
            _generator = generator;
            NumberOfSticks = numberOfSticks;
            Turn = turn;
        }

        /// <summary>
        /// copy ctor
        /// </summary>        
        private Game(Player turn, int numberOfSticks, ICanGenerateNumbers generator, EventHandler<Move> onMachineMoved, EventHandler<Player> onGameOver)
        {
            NumberOfSticks = numberOfSticks;
            Turn = turn;
            _generator = generator;
            MachineMoved = onMachineMoved;
            GameOver = onGameOver;
        }
        

        public Game HumanMakesMove(int sticksTaken)
        {
            if (Turn == Player.Machine)
            {
                throw new InvalidOperationException("It's turn of machine to make a move!");
            }
            if (sticksTaken < MinToTake || sticksTaken > MaxToTake)
            {
                throw new ArgumentException($"You should take from one to three sticks. You took:{sticksTaken}");
            }
            if (sticksTaken > NumberOfSticks)
            {
                throw new ArgumentException($"You took too many sticks.");
            }

            return MakeMove(sticksTaken);
        }


        public Game MachineMakesMove()
        {
            if (Turn == Player.Human)
            {
                throw new InvalidOperationException($"It's turn of Human to make a move.");
            }
            var sticksTaken = MachineTakes();
            
            MachineMoved?.Invoke(this, new Move(sticksTaken, Remains(sticksTaken)));

            return MakeMove(sticksTaken);
        }

        private int MachineTakes()
        {
            int maxValue = NumberOfSticks >= MaxToTake ? MaxToTake : NumberOfSticks;
            int sticksTaken = _generator.Next(MinToTake, maxValue);
            return sticksTaken;
        }

        private Game MakeMove(int sticksTaken)
        {
            int remains = Remains(sticksTaken);
            if (IsGameOver(remains))
            {
                GameOver?.Invoke(this, Revert(Turn));
            }

            return new Game(Revert(Turn), remains, _generator, MachineMoved, GameOver);
        }

        private int Remains(int sticksTaken)
        {
            return NumberOfSticks - sticksTaken;
        }

        private Player Revert(Player p)
        {
            return p == Player.Machine ? Player.Human : Player.Machine;
        }

        public const int MaxToTake = 3;

        public const int MinToTake = 1;

        public event EventHandler<Move> MachineMoved;

        public bool IsGameOver()
        {
            return IsGameOver(NumberOfSticks);
        }

        private bool IsGameOver(int numberOfSticks)
        {
            return numberOfSticks <= 0;
        }

        public event EventHandler<Player> GameOver;
    }

    public struct Move
    {
        public int Taken { get; }
        public int Remains { get; }

        public Move(int taken, int remains)
        {
            Taken = taken;
            Remains = remains;
        }
    }
}