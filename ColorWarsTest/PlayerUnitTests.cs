﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

using ColorWars.Services;
using ColorWars.Players;
using ColorWars.Boards;
using System;

namespace ColorWarsTest
{
    [TestClass]
    public class PlayerUnitTest
    {
        private readonly BoardField startField = new BoardField(MissingPlayer.Instance, new Point())
        {
            Neighbours = new Dictionary<Direction, BoardField>()
            {
                {
                    Direction.Up,
                    new BoardField(MissingPlayer.Instance, new Point())
                }
            }
        };
        private int killed;

        [TestMethod]
        public void MoveTest()
        {
            //set up
            var player = new PlayerModel(new Color(), startField);
            var endField = this.startField.Neighbours[Direction.Up];
            //test
            player.Move(Direction.Up);

            Assert.IsTrue(player.Position == endField);
        }

        [TestMethod]
        public void KillTest()
        {
            //set up
            var player = new PlayerModel(new Color(), startField);
            var killer = new PlayerModel(new Color(), startField);
            player.KilledEvent += this.OnKillHandler;
            //test
            player.Kill(killer);
            
            Assert.IsTrue(player.Stats.Deaths == 1);
            Assert.IsTrue(killer.Stats.Kills == 1);
            Assert.IsTrue(this.killed == 1);
        }

        private void OnKillHandler(object sender, EventArgs e)
        {
            this.killed++;
        }
    }
}
