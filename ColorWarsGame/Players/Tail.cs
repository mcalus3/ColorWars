using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections;

namespace ColorWars
{
    class Tail : ISquareDrawable
    {
        private List<BoardField> positions;
        private IPlayer player;

        public void KillOwner(Player killer)
        {
            throw new NotImplementedException();
        }

        public Color GetColor()
        {
            throw new NotImplementedException();
        }

        public Point[] GetPoints()
        {
            throw new NotImplementedException();
        }
    }
}
