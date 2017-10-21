using System;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Services;
using ColorWars.Players;

namespace ColorWarsTest.Mocks
{
    class PlayerControllerMock : PlayerController
    {
        public int moved;
        public int dirChanged { get; private set; }

        public static PlayerModel GetPlayerModel()
        {
            return new PlayerModel(new Color(), null);
        }

        public PlayerControllerMock() : base(new PlayerModel(new Color(), new BoardField(MissingPlayer.Instance, new Point())), new BoardField(MissingPlayer.Instance, new Point()), 0, 0)
        {
        }

        public new void Move(Direction direction)
        {
            this.moved++;
        }

        public override void ChangeNextDirection(Direction direction)
        {
            this.dirChanged++;
        }
    }
}
