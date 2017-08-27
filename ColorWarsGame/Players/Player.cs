using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using ColorWars.Boards;

namespace ColorWars.Players
{
    class Player : IPlayer
    {
        public static readonly IPlayer MISSING = new MissingPlayer();

        private Color color;
        private BoardField position;
        private readonly BoardField startField;

        public Player(Color color, BoardField startField)
        {
            this.color = color;
            this.position = startField;
            this.startField = startField;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public Point[] GetPoints()
        {
            return new Point[] {this.position.GetPoints()[0]};
        }
    }
}
