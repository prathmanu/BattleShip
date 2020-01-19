using Battleship.Core.Interfaces;
using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Core.Board
{
    public class BoardFactory : IBoardFactory
    {
        Random _rnd;
        public BoardFactory()
        {
            _rnd = new Random();
        }

        public Dictionary<string, Ship> BuildBoard(List<Ship> ships, int boardSize = 10)
        {
            var board = new Dictionary<string, Ship>();
            foreach (var ship in ships)
            {
                var placed = false;
                while (!placed)
                {
                    List<string> positions = SetShipPositions(boardSize, ship.Length);
                    if (positions.All(p => !board.ContainsKey(p)))
                    {
                        positions.ForEach(p => board.Add(p, ship));
                        placed = true;
                    }
                }
            }

            return board;
        }

        List<string> SetShipPositions(int boardSize, int shipLength)
        {
            var positions = new List<string>();
            var isHorizontal = _rnd.Next(0, 99) < 50;
            Coordinate startPosition = SetStartingPosition(boardSize, shipLength, isHorizontal);

            for (int i = 0; i < shipLength; i++)
            {
                int x = startPosition.X + (isHorizontal ? 0 : i),
                y = startPosition.Y + (isHorizontal ? i : 0);

                positions.Add($"{(char)x}{y}");
            }

            return positions;
        }

        private Coordinate SetStartingPosition(int boardSize, int shipLength, bool isHorizontal)
        {
            return new Coordinate
            {
                Y = _rnd.Next(1, isHorizontal ? boardSize - shipLength : boardSize),
                X = _rnd.Next(1, isHorizontal ? boardSize : boardSize - shipLength) + 96
            };
        }

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
