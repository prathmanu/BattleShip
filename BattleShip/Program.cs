using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Core.Board;
using Battleship.Core.Game;
using Battleship.Core.Models;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new BoardFactory().BuildBoard(new List<Ship> { new Ship { Length = 5 }, new Ship { Length = 4 }, new Ship { Length = 4 } });
            var game = new Game(board);
            Console.WriteLine("Game Board Created and placed Ships. Please Enter attacking point(example: a3) and press enter ");
            var lastResult = string.Empty;
            while (lastResult != "end")
            {
                lastResult = game.Shoot(Console.ReadLine());
                Console.WriteLine($">>{lastResult}");
                Console.WriteLine(" Please Enter next attacking point and press enter ");
            }
        }
    }
}
