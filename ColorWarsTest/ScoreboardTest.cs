using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWarsTest
{
    [TestClass]
    public class ScoreboardTest
    {
        [TestMethod]
        public void UpdateTest()
        {
            PlayerModel player = PlayerControllerMock.GetPlayerModel();
            GameBoard board = BoardTest.CreateTestMap();
            var scoreboard = new Scoreboard(new System.Collections.Generic.List<PlayerModel>() { player}, board);
            board.Board[0, 0].Owner = player;

            scoreboard.Update(null, null);

            Assert.IsTrue(player.Stats.Territory == 1);
        }
    }
}
