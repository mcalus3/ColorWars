using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorWars.Players;
using ColorWars.Boards;
using Microsoft.Xna.Framework;
using ColorWars.Services;

namespace ColorWarsTest
{
    [TestClass]
    public class PlayerServicesTest
    {
        [TestMethod]
        public void AddTerritoryTest()
        {
            //set up
            BoardField[,] fields = BoardTest.CreateTestMap().Board;
            var tailFields = new BoardField[]
            {
                fields[0, 2],
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

            var player = new PlayerModel(new Color(), fields[1, 2]);
            fields[1, 2].Owner = player;
            player.Tail.Positions.AddRange(tailFields);
            //test
            PlayerServices.AddTerritory(player);

            foreach(BoardField field in expectedClaimedFields)
            {
                Assert.IsTrue(field.Owner == player);
            }

            foreach(BoardField field in expectedNotClaimedFields)
            {
                Assert.IsTrue(field.Owner == MissingPlayer.Instance);
            }
        }
    }
}
