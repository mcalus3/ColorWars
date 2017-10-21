using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorWars.Players;
using ColorWars.Boards;
using Microsoft.Xna.Framework;

using ColorWars.Services;

namespace ColorWarsTest
{
    [TestClass]
    public class BoardTest
    {
        GameBoard gameBoard = BoardTest.CreateTestMap();

        internal static GameBoard CreateTestMap()
        {
            var board = new GameBoard(1, new Point(4,3));
            board.InitializeEmptyBoard();
            return board;
        }

        internal static void ClearTestBoard(BoardField[,] testBoard)
        {
            foreach(BoardField field in testBoard)
            {
                field.Owner = MissingPlayer.Instance;
            }
        }

        [TestMethod]
        public void BoardFunctionalTest()
        {
            BoardField[,] fields = BoardTest.CreateTestMap().Board;
            Assert.IsTrue(fields.Length == 12);
            Assert.IsTrue(fields[0,0].Neighbours[Direction.UP] == null);
            Assert.IsTrue(fields[0,0].Neighbours[Direction.DOWN] == fields[0,1]);
        }

        [TestMethod]
        public void InitializeBoardTest()
        {
            var points = new Point[]
            {
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 1),
                new Point(2, 2)
            };
            var board = new GameBoard(1, new Point( 4, 4));

            board.InitializeEmptyBoard();

            Assert.IsTrue(board.Board.Length == 16);
            foreach (Point point in points)
            Assert.IsTrue(board.Board[point.X, point.Y].Position == point);

            Assert.IsTrue(board.Board[0, 0].Neighbours[Direction.DOWN] == board.Board[0, 1]);
            Assert.IsTrue(board.Board[0, 0].Neighbours[Direction.RIGHT] == board.Board[1, 0]);
            Assert.IsTrue(board.Board[0, 0].Neighbours[Direction.LEFT] == null);
            Assert.IsTrue(board.Board[0, 0].Neighbours[Direction.UP] == null);
        }

        [TestMethod]
        public void GetStartFieldsTest()
        {
            BoardField[,] fields = new BoardField[,]
            {
                {
                    new BoardField(MissingPlayer.Instance, new Point(0, 0)),
                    new BoardField(MissingPlayer.Instance, new Point(1, 0))
                },
                {
                    new BoardField(MissingPlayer.Instance, new Point(0, 1)),
                    new BoardField(MissingPlayer.Instance, new Point(1, 1))
                }
            };

            var board = new GameBoard(1, new Point(2, 2))
            {
                Board = fields
            };

            var startFields = board.GetStartFields();

            Assert.IsTrue(startFields[0].Position.X == 0);
            Assert.IsTrue(startFields[0].Position.Y == 0);
            Assert.IsTrue(startFields[1].Position.X == 1);
            Assert.IsTrue(startFields[1].Position.Y == 1);
        }

        [TestMethod]
        public void ClaimStartingTerritoriesTest()
        {
            var board = this.gameBoard;
            var player = new PlayerModel(new Color(), board.Board[0, 0]);

            board.ClaimStartingTerritories(new PlayerModel[]{ player });

            Assert.IsTrue(board.Board[0, 0].Owner == player);
            Assert.IsTrue(board.Board[0, 1].Owner == player);
            Assert.IsTrue(board.Board[1, 0].Owner == player);
            Assert.IsTrue(board.Board[1, 1].Owner == MissingPlayer.Instance);
            Assert.IsTrue(board.Board[2, 0].Owner == MissingPlayer.Instance);
            Assert.IsTrue(board.Board[0, 2].Owner == MissingPlayer.Instance);

            BoardTest.ClearTestBoard(board.Board);
        }

        [TestMethod]
        public void UpdatePlayersTerritoryTest()
        {
            var board = this.gameBoard;
            var player = new PlayerModel(new Color(), board.Board[0, 0]);

            board.Board[0, 0].Owner = player;
            board.Board[0, 1].Owner = player;
            board.Board[1, 0].Owner = player;

            board.UpdatePlayersTerritory(new PlayerModel[] { player });

            Assert.IsTrue(player.Stats.Territory == 3);

            BoardTest.ClearTestBoard(board.Board);
        }
    }
}
