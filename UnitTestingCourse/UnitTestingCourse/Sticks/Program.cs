using System;
using Sticks.Core;

namespace Sticks
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int initialNumberOfSticks = RequestNumberOfSticks();
                Player starts = RequestWhoStartsGame();

                WriteHowManySticks(initialNumberOfSticks, starts);

                Game game = new Game(initialNumberOfSticks, starts);

                game.MachineMoved += (s, e) =>
                    Console.WriteLine($"Machine took {e.Taken} sticks.\n" +
                                      $"Remains {e.Remains} sticks in the game.");
                game.GameOver += (s, player) => Console.WriteLine($"{player} won.");

                Polling(game);

                if (!RequestContinuation())
                    break;
            }
        }

        private static bool RequestContinuation()
        {
            bool? result = null;

            while (result == null)
            {
                Console.WriteLine("Want another game?");
                string response = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(response))
                {
                    string r = response.ToUpper();
                    if (r == "Y" || r == "N")
                        result = r == "Y";
                }
            }
            return result.Value;
        }

        private static void Polling(Game g)
        {
            while (!g.IsGameOver())
            {
                if (g.Turn == Player.Machine)
                {
                    g = g.MachineMakesMove();
                }
                else
                {
                    int humanMove = RequestMove(g.NumberOfSticks);
                    g = g.HumanMakesMove(humanMove);

                    WriteHowManySticks(g.NumberOfSticks, g.Turn);
                }
            }
        }

        private static int RequestMove(int sticksInGame)
        {
            int result = 0;
            while (result == 0)
            {
                Console.WriteLine("How many sticks?");
                string sticks = Console.ReadLine();
                const int maxToTake = 3;
                const int minToTake = 1;
                if (!int.TryParse(sticks, out int taken)
                    || taken > maxToTake || taken < minToTake
                    || taken > sticksInGame)
                {
                    Console.WriteLine($"You entered={sticks}. In the game={sticksInGame}");
                }
                else
                {
                    result = taken;
                }
            }
            return result;
        }

        private static int RequestNumberOfSticks()
        {
            int result = 0;
            while (result == 0)
            {
                Console.WriteLine("How many sticks? Enter a number >= 10");
                string sticks = Console.ReadLine();
                if (!int.TryParse(sticks, out int number) || number < 10)
                {
                    Console.WriteLine($"You entered={number}");
                }
                else
                {
                    result = number;
                }
            }
            return result;
        }

        private static Player RequestWhoStartsGame()
        {
            char who = 'u';
            while (who == 'u')
            {
                Console.WriteLine(@"Who will start the game? Enter 'h' for human or 'm' for machine.");

                string whoStarts = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(whoStarts))
                {
                    continue;
                }
                char c = whoStarts.ToUpper()[0];
                if (c != 'H' && c != 'M')
                {
                    continue;
                }
                who = c;
            }
            return who == 'H' ? Player.Human : Player.Machine;
        }

        static void WriteHowManySticks(int sticks, Player turn)
        {
            Console.WriteLine($"Sticks in the game:{sticks}. Turn: {turn}");
        }
    }
}
