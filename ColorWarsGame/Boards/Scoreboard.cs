using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;
using ColorWars.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ColorWars.Boards
{
    class Scoreboard
    {
        private GraphicsDevice gDevice;
        public List<Player> Players { get; set; }
        private GameBoard board;
        public int FieldCount;

        public Scoreboard(List<Player> playerList, GameBoard board)
        {
            this.Players = playerList;
            this.board = board;
            this.FieldCount = this.board.Board.Count();
        }        

        public void Update(object sender, EventArgs e)
        {
            this.board.UpdatePlayersTerritory(this.Players.ToArray());
            this.Players.Sort((x, y) => y.Stats.Territory.CompareTo(x.Stats.Territory));
        }
    }
}
