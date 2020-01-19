using Battleship.Core.Board;
using Battleship.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip.TestMethod
{
    [TestClass]
    public class BoardFactoryTests
    {
        [TestMethod]
        public void WhenRequestingBoardWithoutShipsEmptyBoardIsReturned()
        {
            var subject = new BoardFactory();

            var output = subject.BuildBoard(new List<Ship>());

            output.Count.ShouldBe(0);
        }

        [TestMethod]
        public void WhenRequestingBoardWithSingleShipBoardHasFieldsForEachFieldWithShipAndAllFieldsAreTheSameShip()
        {
            var subject = new BoardFactory();

            var output = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 } });

            output.Count.ShouldBe(5);
            var first = output.Values.First();
            output.Values.ShouldAllBe(x => x == first);
        }

        [TestMethod]
        public void WhenRequestingBoardWithSingleShipTheShipHasNoHoles()
        {
            var subject = new BoardFactory();

            var output = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 } });

            var shipFields = output.Keys
                                   .Select(p => new { x = (int)p[0], y = int.Parse(p.Substring(1)) })
                                   .OrderBy(p => p.x)
                                   .ThenBy(p => p.y)
                                   .ToList();
            var previous = shipFields[0];
            shipFields.RemoveAt(0);
            foreach (var point in shipFields)
            {
                var isXContinous = (previous.x) == (point.x - 1);
                var isYContinous = (previous.y) == (point.y - 1);
                previous = point;
                (isXContinous || isYContinous).ShouldBe(true);
            }
        }

        [TestMethod]
        public void WhenRequestingBoardWithSingleShipTwiceItHasRandomPosition()
        {
            var subject = new BoardFactory();

            var outputFirst = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 } });
            var outputSecond = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 } });

            outputFirst.Keys.First().ShouldNotBe(outputSecond.Keys.First());
        }

        [TestMethod]
        public void WhenRequestingBoardWithSingleShipItNeverIsOutsideOfBoundries()
        {
            var subject = new BoardFactory();
            var boardSize = 10;

            var output = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 } }, boardSize);

            output.Keys.ShouldAllBe(p => int.Parse(p.Substring(1)) <= boardSize);
            output.Keys.ShouldAllBe(p => (p[0] - 96) <= boardSize);
        }

        [TestMethod]
        public void WhenRequestingBoardWithThreeShipsItReturnsBoardWithAllShips()
        {
            var subject = new BoardFactory();
            var boardSize = 10;

            var output = subject.BuildBoard(new List<Ship> { new Ship { Length = 5 }, new Ship { Length = 4 }, new Ship { Length = 4 } }, boardSize);

            output.Count.ShouldBe(13);
           
        }
    }
}
