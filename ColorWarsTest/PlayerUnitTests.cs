using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

using ColorWars.Services;
using ColorWars.Players.States;
using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWarsTest
{
    [TestClass]
    public class PlayerUnitTest
    {
        BoardField startField = new BoardField(Player.MISSING, new Point())
        {
            Neighbours = new Dictionary<Direction, BoardField>()
            {
                {
                    Direction.UP,
                    new BoardField(Player.MISSING, new Point())
                }
            }
        };

        [TestMethod]
        public void MoveTest()
        {
            //set up
            var player = new Player(new PlayerSettings(), startField);
            var endField = this.startField.Neighbours[Direction.UP];
            //test
            player.Move(Direction.UP);

            Assert.IsTrue(player.Position == endField);
        }

        [TestMethod]
        public void KillTest()
        {
            //set up
            var player = new Player(new PlayerSettings(), startField);
            var killer = new Player(new PlayerSettings(), startField);
            //test
            player.Kill(killer);

            Assert.IsTrue(player.Stats.Deaths == 1);
            Assert.IsTrue(killer.Stats.Kills == 1);
            Assert.IsInstanceOfType(player.State, typeof(WaitingForRespawnState));
        }
    }
}
