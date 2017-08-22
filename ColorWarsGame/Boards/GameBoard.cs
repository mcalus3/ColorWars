using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class GameBoard
    {
        private List<BoardField> board;
        private Point[] startPoints;
        private int StartingTerritorySize = 2; //TODO: Move to settings

        public GameBoard()
        {
            this.board = new List<BoardField>();
        }

        public void InitializeEmptyBoard(Point dimension)
        {
            for (int i = 0; i < dimension.X; i++)
            {
                for (int j = 0; j < dimension.Y; j++)
                {
                    this.board.Add(new BoardField(Player.EMPTY, new Point(i, j)));
                }
            }
            foreach(BoardField field in this.board)
            {
                this.FillNeighbors(field);
            }
        }

        private void FillNeighbors(BoardField field)
        {
            BoardField leftNeighbor = this.board.SingleOrDefault(f => f.GetPoints()[0].X == field.GetPoints()[0].X - 1 && f.GetPoints()[0].Y == field.GetPoints()[0].Y);
            BoardField rightNeighbor = this.board.SingleOrDefault(f => f.GetPoints()[0].X == field.GetPoints()[0].X + 1 && f.GetPoints()[0].Y == field.GetPoints()[0].Y);
            BoardField upperNeighbor = this.board.SingleOrDefault(f => f.GetPoints()[0].Y == field.GetPoints()[0].Y - 1 && f.GetPoints()[0].X == field.GetPoints()[0].X);
            BoardField lowerNeighbor = this.board.SingleOrDefault(f => f.GetPoints()[0].Y == field.GetPoints()[0].Y + 1 && f.GetPoints()[0].X == field.GetPoints()[0].X);
            field.AddNeighbor(upperNeighbor, Direction.UP);
            field.AddNeighbor(lowerNeighbor, Direction.DOWN);
            field.AddNeighbor(leftNeighbor, Direction.LEFT);
            field.AddNeighbor(rightNeighbor, Direction.RIGHT);
        }

        public BoardField[] GetStartFields()
        {
            //TODO: Change to calculating even distance on a circle or random player placing
            Point dimension = this.board.Last().GetPoints()[0]; //TODO: Change to more expressive LINQ query - get dimension of board
            var fields = new BoardField[]
            {
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(dimension.X/4),(int)(dimension.Y/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(dimension.X*3/4),(int)(dimension.Y*3/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(dimension.X*3/4),(int)(dimension.Y/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(dimension.X/4),(int)(dimension.Y*3/4))),
            };
            return fields;
        }

        public void ClaimStartingTerritories(Player[] players)
        {
            foreach (Player player in players)
            {
                foreach(BoardField field in this.board)
                {
                    if (FieldCloserToPlayerThan(field, player, (int)(this.StartingTerritorySize * Math.Sqrt(2))) )
                    {
                        field.ChangeOwner(player);
                    }
                }
            }
        }

        private bool FieldCloserToPlayerThan(ISquareDrawable field, ISquareDrawable player, int distance)
        {
            Vector2 fieldVector = field.GetPoints()[0].ToVector2();
            Vector2 playerVector = player.GetPoints()[0].ToVector2();
            Vector2 diffVector = (fieldVector - playerVector);

            if (diffVector.Length() <= distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<BoardField> GetFields()
        {
            return this.board;
        }

    }
}
