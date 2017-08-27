using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections;

using ColorWars.Graphics;
using ColorWars.Boards;

namespace ColorWars.Players
{
    class Tail : ISquareDrawable
    {
        public List<BoardField> Positions {get; set;}
        private Player owner;

        public Tail(Player owner)
        {
            this.owner = owner;
            this.Positions = new List<BoardField>();
        }

        public void Delete()
        {
            this.Positions = new List<BoardField>();
        }

        public Color GetColor()
        {
            return this.ConvertOwnerColor(this.owner.GetColor());
        }

        public Point[] GetPoints()
        {
            return this.Positions.Select(f => f.GetPoints()[0]).ToArray();
        }

        private Color ConvertOwnerColor(Color color)
        {
            var newColor = new Color(color.R, color.G, color.B);
            newColor.R = color.R;
            newColor.G = color.G;
            newColor.B = color.B;
            newColor.A = 200;
            return newColor;
        }

    }
}
