using Battleship.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Core.Interfaces
{
    public interface IBoardFactory
    {
        Dictionary<string, Ship> BuildBoard(List<Ship> ships, int boardSize);
    }
}
