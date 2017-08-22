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
        public List<BoardField> positions;
        private Player owner;

        public Tail(Player owner)
        {
            this.owner = owner;
            this.positions = new List<BoardField>();
        }

        public void AddField(BoardField field)
        {
            this.positions.Add(field);
            field.PlayerEntered += this.PlayerEnteredHandler;
        }

        private void PlayerEnteredHandler(object sender, EventArgs e)
        {
            owner.Kill((Player)sender);
        }

        public void Delete()
        {
            foreach(BoardField position in this.positions)
            {
                position.PlayerEntered -= this.PlayerEnteredHandler;
            }
            this.positions = new List<BoardField>();
        }

        public void KillOwner(Player killer)
        {
            this.owner.Kill(killer);
        }

        public Color GetColor()
        {
            return this.ConvertOwnerColor(this.owner.GetColor());
        }

        public Point[] GetPoints()
        {
            return this.positions.Select(f => f.GetPoints()[0]).ToArray();
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
