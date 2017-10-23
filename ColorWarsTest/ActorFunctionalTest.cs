using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

using ColorWars.Services;
using ColorWars.Actors;

namespace ColorWarsTest
{
    [TestClass]
    public class ActorFunctionalTest
    {
        [TestMethod]
        public void CreateUseAndRemoveActorTest()
        {
            var player = new PlayerControllerMock();
            var actors = new ActorList();
            var mapping = new Dictionary<PlayerCommand, Keys> { { PlayerCommand.Up, Keys.A } };
            var kState = new KeyboardState(Keys.A);

            actors.Actors.Add(new KeyboardActor(mapping, player));

            actors.Actors[0].PollKeyboard(kState);
            Assert.IsTrue(player.DirChanged == 1);

            actors.RemoveActor(player.Player);
            Assert.IsTrue(actors.Actors.Count == 0);
        }
    }
}
