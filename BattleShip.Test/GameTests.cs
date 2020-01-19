using Battleship.Core.Game;
using Battleship.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.TestMethod
{
    [TestClass]
   public class GameTests
    {
        [TestMethod]
        public void WhenShootingAtEmptyItsAMiss()
        {
            var board = new Dictionary<string, Ship>();
            var subject = new Game(board);

            var result = subject.Shoot("a1");

            result.ShouldMatch("miss");
        }

        [TestMethod]
        public void WhenShootingAtShipItsAHit()
        {
            var ship = new Ship { Length = 3 };
            var board = new Dictionary<string, Ship>()
            {
                {"a1", ship},
                {"a2", ship}
            };
            var subject = new Game(board);

            var result = subject.Shoot("a1");

            result.ShouldMatch("hit");
        }

        [TestMethod]
        public void WhenShootingAtSameShipLocationSecondTimeItsAMiss()
        {
            var board = new Dictionary<string, Ship>()
            {
                {"a1", new Ship {Length = 3}}
            };
            var subject = new Game(board);

            subject.Shoot("a1");
            var result = subject.Shoot("a1");

            result.ShouldMatch("miss");
        }

        [TestMethod]
        public void WhenShootingAtLastShipPointItsASink()
        {
            var board = new Dictionary<string, Ship>()
            {
                {"a1", new Ship { Length = 1}},
                {"a2", new Ship { Length = 1}}
            };
            var subject = new Game(board);

            var result = subject.Shoot("a1");

            result.ShouldMatch("sink");
        }

        [TestMethod]
        public void WhenSinkingLastShipTheGameEnds()
        {
            var board = new Dictionary<string, Ship>()
            {
                {"a1", new Ship { Length = 1}}
            };
            var subject = new Game(board);

            var result = subject.Shoot("a1");

            result.ShouldMatch("end");
        }
    }
}
