using System;
using System.Collections.Generic;

using ColorWars.Players;

namespace ColorWars.Boards
{
    internal class Scoreboard
    {
        public List<PlayerModel> Players { get; set; }
        private readonly GameBoard board;
        public int FieldCount => this.board.Board.Length;

        public Scoreboard(List<PlayerModel> playerList, GameBoard board)
        {
            this.Players = playerList;
            this.board = board;
        }

        public void Update(object sender, EventArgs e)
        {
            this.board.UpdatePlayersTerritory(this.Players.ToArray());
            this.Players.Sort((x, y) => y.Stats.Territory.CompareTo(x.Stats.Territory));
        }
    }
}
