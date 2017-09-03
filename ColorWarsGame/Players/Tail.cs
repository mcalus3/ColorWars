using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections;

using ColorWars.Services;
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

        public void AddField(BoardField field)
        {
            this.Positions.Add(field);
            field.PlayerEntered += this.PlayerEnteredHandler;
        }

        private void PlayerEnteredHandler(object sender, EventArgs e)
        {
            owner.Kill((Player)sender);
        }

        public void Delete()
        {
            foreach (BoardField position in this.Positions)
            {
                position.PlayerEntered -= this.PlayerEnteredHandler;
            }
            this.Positions.Clear();
        }

        public Color GetColor()
        {
            return ColorTools.HalfTransparent(this.owner.GetColor());
        }

        public Point[] GetPoints()
        {
            return this.Positions.Select(f => f.GetPoints()[0]).ToArray();
        }
    }
}
