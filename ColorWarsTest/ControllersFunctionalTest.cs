using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

using ColorWars.Services;
using ColorWars.Boards;
using ColorWars.Players;
using ColorWars.Controllers;
using ColorWarsTest.Mocks;

namespace ColorWarsTest
{
    [TestClass]
    public class ControllersFunctionalTest
    {
        [TestMethod]
        public void CreateUseAndRemoveControllersTest()
        {
            var player = new PlayerMock(new PlayerSettings(), new BoardField(Player.MISSING, new Point()));
            var controllers = new ControllerList();
            var mapping = new Dictionary<Direction, Keys>(){ { Direction.UP, Keys.A } };
            var kState = new KeyboardState(new Keys[] { Keys.A });

            controllers.Commands.Add(new PlayerKeyboardController(mapping, player));

            controllers.Commands[0].Execute(kState);
            Assert.IsTrue(player.dirChanged == 1);

            controllers.RemoveCommand(player);
            Assert.IsTrue(controllers.Commands.Count == 0);
        }
    }
}
