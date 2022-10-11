using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningTypes
{
    public class GameService
    {        
        public void Start(int boardSize, string playerName)
        {
            Player player = CreatePlayer(playerName);
            Board board = CreateBoard(boardSize, player);
            StartGame(board);
        }

        private void StartGame(Board board)
        {
            var game = new Game(board);
            game.Start();
        }

        private Player CreatePlayer(string playerName)
        {
            return new Player(playerName);
        }

        private Board CreateBoard(int boardSize, Player player)
        {
            return new Board(boardSize, player);
        }
    }

    internal class Game
    {
        public Game(Board board)
        {
            

        }

        public void Start()
        {
            
        }
    }

    internal class Player
    {
        private string playerName;

        public Player(string playerName)
        {
            this.playerName = playerName;
        }
    }

    internal class Board
    {
        private int boardSize;
        private Player _player;

        public Board(int boardSize, Player player)
        {
            this.boardSize = boardSize;
            _player = player;
        }
    }
}
