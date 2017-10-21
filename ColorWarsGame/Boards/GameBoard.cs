using Microsoft.Xna.Framework;
using System.Linq;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.Boards
{
    class GameBoard
    {
        public BoardField[,] Board { get; set; }
        private int startingTerritorySize;
        private Point dimension;

        public GameBoard(int startingTerritorySize, Point dimension)
        {
            this.Board = new BoardField[dimension.X, dimension.Y];
            this.startingTerritorySize = startingTerritorySize;
            this.dimension = dimension;
        }

        public void InitializeEmptyBoard()
        {
            for (int i = 0; i < this.dimension.X; i++)
            {
                for (int j = 0; j < this.dimension.Y; j++)
                {
                    this.Board[i, j] = new BoardField(MissingPlayer.Instance, new Point(i, j));
                }
            }

            foreach (BoardField field in this.Board)
            {
                this.FillNeighbors(field);
            }
        }

        private void FillNeighbors(BoardField field)
        {
            Point cords = field.Position;
            BoardField upperNeighbor = null;
            BoardField lowerNeighbor = null;
            BoardField leftNeighbor = null;
            BoardField rightNeighbor = null;

            if (cords.Y > 0)
                upperNeighbor = this.Board[cords.X, cords.Y - 1];
            if (cords.Y < this.dimension.Y - 1)
                lowerNeighbor = this.Board[cords.X, cords.Y + 1];
            if (cords.X > 0)
                leftNeighbor = this.Board[cords.X - 1, cords.Y];
            if (cords.X < this.dimension.X - 1)
                rightNeighbor = this.Board[cords.X + 1, cords.Y];

            field.Neighbours[Direction.UP] = upperNeighbor;
            field.Neighbours[Direction.DOWN] = lowerNeighbor;
            field.Neighbours[Direction.LEFT] = leftNeighbor;
            field.Neighbours[Direction.RIGHT] = rightNeighbor;
        }

        public BoardField[] GetStartFields()
        {
            var fields = new BoardField[]
            {
                this.Board[(int)(this.dimension.X/4), (int)(this.dimension.Y/4)],
                this.Board[(int)(this.dimension.X*3/4), (int)(this.dimension.Y*3/4)],
                this.Board[(int)(this.dimension.X*3/4), (int)(this.dimension.Y/4)],
                this.Board[(int)(this.dimension.X/4), (int)(this.dimension.Y*3/4)],

                this.Board[(int)(this.dimension.X/2), (int)(this.dimension.Y*3/4)],
                this.Board[(int)(this.dimension.X/2), (int)(this.dimension.Y/4)],
                this.Board[(int)(this.dimension.X*3/4), (int)(this.dimension.Y/2)],
                this.Board[(int)(this.dimension.X/4), (int)(this.dimension.Y/2)],
            };
            return fields;
        }

        public void ClaimStartingTerritories(PlayerModel[] players)
        {
            foreach (PlayerModel player in players)
            {
                int depth = this.startingTerritorySize;
                this.RecursiveTerritoryClaiming(player, player.Position, depth);
            }
        }

        private void RecursiveTerritoryClaiming(PlayerModel player, BoardField field, int depth)
        {

            if (field == null)
            {
                return;
            }
            field.Owner = player;
            if (depth == 0)
            {
                return;
            }
            depth--;
            foreach (BoardField nextField in field.Neighbours.Values)
            {
                this.RecursiveTerritoryClaiming(player, nextField, depth);
            }
        }

        public void UpdatePlayersTerritory(PlayerModel[] players)
        {
            foreach (PlayerModel player in players)
            {
                player.Stats.Territory = this.Board.Cast<BoardField>().Where(f => f.Owner == player).Count();
            }
        }
    }
}
