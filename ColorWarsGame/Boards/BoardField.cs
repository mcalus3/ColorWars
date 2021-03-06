﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.Boards
{
    internal class BoardField : ISquareDrawable
    {
        public Point Position { get; set; }
        public IPlayer Owner { get; set; }
        public Dictionary<Direction, BoardField> Neighbours { get; set; }
        public event EventHandler PlayerEntered;

        public BoardField(IPlayer player, Point point)
        {
            this.Owner = player;
            this.Position = point;
            this.Neighbours = new Dictionary<Direction, BoardField>();
        }

        internal void OnPlayerEntered(PlayerModel player)
        {
            PlayerEntered?.Invoke(player, new EventArgs());
        }

        public Point[] GetPoints()
        {
            return new Point[] { this.Position };
        }

        public Color GetColor()
        {
            return ConvertOwnerColor(this.Owner.GetColor());
        }

        private Color ConvertOwnerColor(Color color)
        {
            return ColorTools.TwiceLighter(color);
        }
    }
}
