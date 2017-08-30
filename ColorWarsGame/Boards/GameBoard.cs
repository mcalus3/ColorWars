using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;

namespace ColorWars.Boards
{
    class GameBoard
    {
        public List<BoardField> Board {get; set;}
        private int startingTerritorySize;
        private Point dimension;

        public GameBoard(int startingTerritorySize, Point dimension)
        {
            this.Board = new List<BoardField>();
            this.startingTerritorySize = startingTerritorySize;
            this.dimension = dimension;
        }

        public void InitializeEmptyBoard()
        {
            for (int i = 0; i < this.dimension.X; i++)
            {
                for (int j = 0; j < this.dimension.Y; j++)
                {
                    this.Board.Add(new BoardField(Player.MISSING, new Point(i, j)));
                }
            }
            foreach(BoardField field in this.Board)
            {
                this.FillNeighbors(field);
            }
        }

        private void FillNeighbors(BoardField field)
        {
            BoardField leftNeighbor = this.Board.SingleOrDefault(f => f.GetPoints()[0].X == field.GetPoints()[0].X - 1 && f.GetPoints()[0].Y == field.GetPoints()[0].Y);
            BoardField rightNeighbor = this.Board.SingleOrDefault(f => f.GetPoints()[0].X == field.GetPoints()[0].X + 1 && f.GetPoints()[0].Y == field.GetPoints()[0].Y);
            BoardField upperNeighbor = this.Board.SingleOrDefault(f => f.GetPoints()[0].Y == field.GetPoints()[0].Y - 1 && f.GetPoints()[0].X == field.GetPoints()[0].X);
            BoardField lowerNeighbor = this.Board.SingleOrDefault(f => f.GetPoints()[0].Y == field.GetPoints()[0].Y + 1 && f.GetPoints()[0].X == field.GetPoints()[0].X);
            field.Neighbours[Direction.UP] = upperNeighbor;
            field.Neighbours[Direction.DOWN] = lowerNeighbor;
            field.Neighbours[Direction.LEFT] = leftNeighbor;
            field.Neighbours[Direction.RIGHT] = rightNeighbor;
        }

        public BoardField[] GetStartFields()
        {
            var fields = new BoardField[]
            {
                this.Board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X/4),(int)(this.dimension.Y/4))),
                this.Board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X*3/4),(int)(this.dimension.Y*3/4))),
                this.Board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X*3/4),(int)(this.dimension.Y/4))),
                this.Board.Single(field => field.GetPoints()[0] == new Point((int)(this.dimension.X/4),(int)(this.dimension.Y*3/4))),
            };
            return fields;
        }

        public void ClaimStartingTerritories(Player[] players)
        {
            foreach (Player player in players)
            {
                foreach(BoardField field in this.Board)
                {
                    if (FieldCloserToPlayerThan(field, player, (int)(this.startingTerritorySize * Math.Sqrt(2))) )
                    {
                        field.Owner = player;
                    }
                }
            }
        }

        private bool FieldCloserToPlayerThan(BoardField field, IPlayer player, int distance)
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

        public IPlayer[] GetStatistics()
        {
            return this.Board.GroupBy(f => f.Owner)
                             .OrderByDescending(gp => gp.Count())
                             .Select(g => g.Key)
                             .Where(p => p.GetColor() != Color.White)
                             .ToArray();
        }


        public void UpdatePlayersTerritory(Player[] players)
        {
            foreach (Player player in players)
            {
                player.Stats.Territory = this.Board.Cast<BoardField>().Where(f => f.Owner == player).Count();
            }
        }
    }
}
