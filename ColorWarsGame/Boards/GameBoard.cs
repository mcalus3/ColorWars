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
        private int startingTerritorySize;
        private Point dimension;

        public GameBoard(int startingTerritorySize, Point dimension)
        {
            this.board = new List<BoardField>();
            this.startingTerritorySize = startingTerritorySize;
            this.dimension = dimension;
        }

        public void InitializeEmptyBoard()
        {
            for (int i = 0; i < this.dimension.X; i++)
            {
                for (int j = 0; j < this.dimension.Y; j++)
                {
                    this.board.Add(new BoardField(Player.MISSING, new Point(i, j)));
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
            var fields = new BoardField[]
            {
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X/4),(int)(this.dimension.Y/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X*3/4),(int)(this.dimension.Y*3/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X*3/4),(int)(this.dimension.Y/4))),
                this.board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X/4),(int)(this.dimension.Y*3/4))),
            };
            return fields;
        }

        public void ClaimStartingTerritories(Player[] players)
        {
            foreach (Player player in players)
            {
                foreach(BoardField field in this.board)
                {
                    if (FieldCloserToPlayerThan(field, player, (int)(this.startingTerritorySize * Math.Sqrt(2))) )
                    {
                        field.ChangeOwner(player);
                    }
                }
            }
        }

        internal IPlayer[] GetStatistics()
        {
            return this.board.GroupBy(f => f.owner)
                             .OrderByDescending(gp => gp.Count())
                             .Select(g => g.Key)
                             .Where(p => p.GetColor() != Color.White)
                             .ToArray();
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

        public BoardField[] GetFields()
        {
            return this.board.ToArray();
        }

    }
}
