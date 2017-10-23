using ColorWars.Boards;
using ColorWars.PlayerControllers;
using ColorWars.Players;
using ColorWars.Services;
using Microsoft.Xna.Framework;

namespace ColorWarsTest
{
    internal class PlayerControllerMock : PlayerController
    {
        public int Moved;
        public int DirChanged { get; private set; }

        public static PlayerModel GetPlayerModel()
        {
            return new PlayerModel(new Color(), null);
        }

        public PlayerControllerMock() : base(new PlayerModel(new Color(), new BoardField(MissingPlayer.Instance, new Point())), new BoardField(MissingPlayer.Instance, new Point()), 0, 0)
        {
        }

        public new void Move(Direction direction)
        {
            this.Moved++;
        }

        public override void ChangeNextDirection(Direction direction)
        {
            this.DirChanged++;
        }
    }
}
