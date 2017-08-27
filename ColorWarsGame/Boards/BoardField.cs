using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using ColorWars.Players;
using ColorWars.Graphics;

namespace ColorWars.Boards
{
    class BoardField : ISquareDrawable
    {
        public Point Position { get; set; }
        public IPlayer Owner {get; set;}
        public Dictionary<Direction, BoardField> Neighbours{get; set;}
        public event EventHandler PlayerEntered;

        public BoardField(IPlayer player, Point point)
        {
            this.Owner = player;
            this.Position = point;
            this.Neighbours = new Dictionary<Direction, BoardField>();

        }

        internal void OnPlayerEntered(Player player)
        {
            if (this.PlayerEntered != null)
                this.PlayerEntered(player, new EventArgs());
        }

        public Point[] GetPoints()
        {
            return new Point[] {this.Position};
        }

        public Color GetColor()
        {
            return ConvertOwnerColor(this.Owner.GetColor());
        }

        private Color ConvertOwnerColor(Color color)
        {
            var newColor = new Color(color.R, color.G, color.B);
            newColor.R = (byte)Math.Floor(color.R * 0.5);
            newColor.G = (byte)Math.Floor(color.G * 0.5);
            newColor.B = (byte)Math.Floor(color.B * 0.5);
            newColor.A = 0;
            return newColor;
        }
    }
}
