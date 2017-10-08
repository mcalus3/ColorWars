using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;


using ColorWars.Services;
using ColorWars.Players;
using ColorWars.Boards;
using ColorWars.Players.States;

namespace ColorWarsTest
{
    [TestClass]
    public class PlayerFunctionalTest
    {
        private BoardField[,] testBoard = BoardTest.CreateTestMap().Board;

        [TestMethod]
        public void NotMoveWhileDeadTest()
        {
            //set up
            var startField = new BoardField(Player.MISSING, new Point());
            var settings = new PlayerSettings() { speed = 0 };
            var player = new Player(new PlayerSettings(), startField);
            player.State = new WaitingForRespawnState(player);
            //test
            Assert.IsTrue(player.Position == startField);

            player.ChangeNextDirection(Direction.UP);
            player.Update();

            Assert.IsTrue(player.Position == startField);
        }

        [TestMethod]
        public void StartMovingTest()
        {
            //set up
            var startField = new BoardField(Player.MISSING, new Point(0,1));
            var endField = new BoardField(Player.MISSING, new Point(0,0));
            startField.Neighbours.Add(Direction.UP, endField);
            var settings = new PlayerSettings() { speed = 0 };
            var player = new Player(settings, startField);
            //test
            Assert.IsTrue(player.BufferedDirection == Direction.NONE);

            player.Update();
            Assert.IsTrue(player.Position == startField);

            player.ChangeNextDirection(Direction.UP);
            player.Update();
            Assert.IsTrue(player.BufferedDirection == Direction.UP);
            Assert.IsTrue(player.Position == endField);
        }

        [TestMethod]
        public void ChangeNextDirectionTest()
        {
            //set up
            var startField = new BoardField(Player.MISSING, new Point(0, 1));
            var endField = new BoardField(Player.MISSING, new Point(0, 0));
            startField.Neighbours.Add(Direction.UP, endField);
            var settings = new PlayerSettings() { speed = 0 };
            var player = new Player(settings, startField);
            player.BufferedDirection = Direction.UP;
            player.State = new DefensiveState(player);

            //test
            player.ChangeNextDirection(Direction.LEFT);
            Assert.IsTrue(player.BufferedDirection == Direction.UP);
            //player will go up because that is his buffered direction and then change it to left
            player.Update();

            Assert.IsTrue(player.BufferedDirection == Direction.LEFT);
        }

        [TestMethod]
        public void AttackTest()
        {
            //set up
            var startField = new BoardField(Player.MISSING, new Point(0, 2));
            var midField = new BoardField(Player.MISSING, new Point(0, 1));
            var endField = new BoardField(Player.MISSING, new Point(0, 0));
            startField.Neighbours.Add(Direction.UP, midField);
            midField.Neighbours.Add(Direction.UP, endField);

            var settings = new PlayerSettings() { speed = 0 };
            var player = new Player(settings, startField);
            
            startField.Owner = player;

            var expectedTailFields = new BoardField[] { midField };
            //test
            player.ChangeNextDirection(Direction.UP);
            player.Update();
            player.Update();

            Assert.IsInstanceOfType(player.State, typeof(AttackingState));
            Assert.IsTrue(player.Tail.Positions.Count == 1);
            Assert.IsTrue(player.Tail.Positions.ToArray()[0] == midField);
            Assert.IsTrue(player.Position == endField);
        }

        [TestMethod]
        public void ReturnTest()
        {
            //set up
            BoardField[,] fields = this.testBoard;
            var tailFields = new BoardField[]
            {
                fields[0, 1],
                fields[0, 0],
                fields[1, 0],
                fields[2, 0],
                fields[2, 1],
                fields[2, 2],
            };
            var expectedClaimedFields = new BoardField[] {
                fields[0, 2],
                fields[0, 1],
                fields[0, 0],
                fields[1, 2],
                fields[1, 1],
                fields[1, 0],
                fields[2, 2],
                fields[2, 1],
                fields[2, 0]
            };
            var expectedNotClaimedFields = new BoardField[] {
                fields[3, 2],
                fields[3, 1],
                fields[3, 0]
            };

            var settings = new PlayerSettings() { speed = 0 };
            var player = new Player(settings, fields[0,2]);
            player.ChangeNextDirection(Direction.RIGHT);
            //player.State = new AttackingState(player);

            fields[1,2].Owner = player;
            player.Tail.Positions.AddRange(tailFields);

            //test
            player.Update();
            player.Update();

            foreach(BoardField field in expectedClaimedFields)
            {
                Assert.IsTrue(field.Owner == player);
            }

            foreach(BoardField field in expectedNotClaimedFields)
            {
                Assert.IsTrue(field.Owner == Player.MISSING);
            }

            //tear down
            BoardTest.ClearTestBoard(fields);
        }

        [TestMethod]
        public void KillByEatingTailTest()
        {
            //set up
            BoardField[,] fields = this.testBoard;

            var settings = new PlayerSettings() { speed = 0 };
            var killed = new Player(settings, fields[0, 2]);
            var killer = new Player(settings, fields[1, 2]);
            killed.ChangeNextDirection(Direction.UP);
            killer.ChangeNextDirection(Direction.LEFT);

            Assert.IsTrue(killer.Stats.Kills == 0);
            Assert.IsTrue(killer.Stats.Deaths == 0);

            //test
            killed.Update();
            killer.Update();

            Assert.IsTrue(killer.Stats.Kills == 1);
            Assert.IsTrue(killed.Stats.Deaths == 1);
            Assert.IsInstanceOfType(killed.State, typeof(WaitingForRespawnState));

            //tear down
            BoardTest.ClearTestBoard(fields);
        }
    }
}
