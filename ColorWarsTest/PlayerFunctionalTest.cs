using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;


using ColorWars.Services;
using ColorWars.Players;
using ColorWars.Boards;
using ColorWars.PlayerControllers;
using ColorWars.PlayerControllers.States;

namespace ColorWarsTest
{
    [TestClass]
    public class PlayerFunctionalTest
    {
        private readonly BoardField[,] testBoard = BoardTest.CreateTestMap().Board;

        [TestMethod]
        public void NotMoveWhileDeadTest()
        {
            //set up
            var startField = new BoardField(MissingPlayer.Instance, new Point());
            var player = new PlayerModel(new Color(), startField);
            var controller = new PlayerController(player, startField, 0, 0);
            controller.MovingState = new WaitingForRespawnState(controller);
            //test
            Assert.IsTrue(player.Position == startField);

            controller.ChangeNextDirection(Direction.Up);
            controller.Update();

            Assert.IsTrue(player.Position == startField);
        }

        [TestMethod]
        public void StartMovingTest()
        {
            //set up
            var startField = new BoardField(MissingPlayer.Instance, new Point(0,1));
            var endField = new BoardField(MissingPlayer.Instance, new Point(0,0));
            startField.Neighbours.Add(Direction.Up, endField);
            var player = new PlayerModel(new Color(), startField);
            var controller = new PlayerController(player, startField, 0, 0);
            //test
            Assert.IsTrue(player.Direction == Direction.None);

            controller.Update();
            Assert.IsTrue(player.Position == startField);

            controller.ChangeNextDirection(Direction.Up);
            controller.Update();
            Assert.IsTrue(player.Direction == Direction.Up);
            Assert.IsTrue(player.Position == endField);
        }

        [TestMethod]
        public void ChangeNextDirectionTest()
        {
            //set up
            var startField = new BoardField(MissingPlayer.Instance, new Point(0, 1));
            var endField = new BoardField(MissingPlayer.Instance, new Point(0, 0));
            startField.Neighbours.Add(Direction.Up, endField);
            var player = new PlayerModel(new Color(), startField);
            var controller = new PlayerController(player, startField, 0, 0);
            player.Direction = Direction.Up;
            controller.MovingState = new DefensiveState(controller);

            //test
            controller.ChangeNextDirection(Direction.Left);
            Assert.IsTrue(player.Direction == Direction.Up);
            //player will go up because that is his buffered direction and then change it to left
            controller.Update();

            Assert.IsTrue(player.Direction == Direction.Left);
        }

        [TestMethod]
        public void AttackTest()
        {
            //set up
            var startField = new BoardField(MissingPlayer.Instance, new Point(0, 2));
            var midField = new BoardField(MissingPlayer.Instance, new Point(0, 1));
            var endField = new BoardField(MissingPlayer.Instance, new Point(0, 0));
            startField.Neighbours.Add(Direction.Up, midField);
            midField.Neighbours.Add(Direction.Up, endField);

            var player = new PlayerModel(new Color(), startField);
            var controller = new PlayerController(player, startField, 0, 0);

            startField.Owner = player;

            //test
            controller.ChangeNextDirection(Direction.Up);
            controller.Update();
            controller.Update();

            Assert.IsInstanceOfType(controller.MovingState, typeof(AttackingState));
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

            var player = new PlayerModel(new Color(), fields[0,2]);
            var controller = new PlayerController(player, fields[0, 2], 0, 0);
            controller.ChangeNextDirection(Direction.Right);
            //player.State = new AttackingState(player);

            fields[1,2].Owner = player;
            player.Tail.Positions.AddRange(tailFields);

            //test
            controller.Update();
            controller.Update();

            foreach(BoardField field in expectedClaimedFields)
            {
                Assert.IsTrue(field.Owner == player);
            }

            foreach(BoardField field in expectedNotClaimedFields)
            {
                Assert.IsTrue(field.Owner == MissingPlayer.Instance);
            }

            //tear down
            BoardTest.ClearTestBoard(fields);
        }

        [TestMethod]
        public void KillByEatingTailTest()
        {
            //set up
            BoardField[,] fields = this.testBoard;

            var killed = new PlayerModel(new Color(), fields[0, 2]);
            var killer = new PlayerModel(new Color(), fields[1, 2]);
            var killedController = new PlayerController(killed, fields[0, 2], 0, 0);
            var killerController = new PlayerController(killer, fields[1, 2], 0, 0);
            killedController.ChangeNextDirection(Direction.Up);
            killerController.ChangeNextDirection(Direction.Left);

            Assert.IsTrue(killer.Stats.Kills == 0);
            Assert.IsTrue(killer.Stats.Deaths == 0);

            //test
            killedController.Update();
            killerController.Update();

            Assert.IsTrue(killer.Stats.Kills == 1);
            Assert.IsTrue(killed.Stats.Deaths == 1);
            Assert.IsInstanceOfType(killedController.MovingState, typeof(WaitingForRespawnState));

            //tear down
            BoardTest.ClearTestBoard(fields);
        }
    }
}
