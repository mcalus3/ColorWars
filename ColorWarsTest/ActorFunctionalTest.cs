using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

using ColorWars.Services;
using ColorWars.Players;
using ColorWars.Actors;
using ColorWarsTest.Mocks;

namespace ColorWarsTest
{
    [TestClass]
    public class ActorFunctionalTest
    {
        [TestMethod]
        public void CreateUseAndRemoveActorTest()
        {
            var player = new PlayerControllerMock();
            var Actors = new ActorList();
            var mapping = new Dictionary<PlayerCommand, Keys>(){ { PlayerCommand.UP, Keys.A } };
            var kState = new KeyboardState(new Keys[] { Keys.A });

            Actors.Actors.Add(new KeyboardActor(mapping, player));

            Actors.Actors[0].PollKeyboard(kState);
            Assert.IsTrue(player.dirChanged == 1);

            Actors.RemoveActor(player.Player);
            Assert.IsTrue(Actors.Actors.Count == 0);
        }
    }
}
