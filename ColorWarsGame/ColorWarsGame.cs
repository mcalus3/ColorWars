using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private List<Player> playerList;
        private GameBoard gameBoard;

        private Point mapDimension = new Point(10,10); //To be moved to settings
        private Color[] players = {Color.Blue, Color.Green}; //To be moved to settings
        private int startingTerritorySize = 2; //To be moved to settings

        public ColorWarsGame()
        {
            this.playerList = new List<Player>();
            this.gameBoard = new GameBoard(this.startingTerritorySize, this.mapDimension);
        }

        protected override void Initialize()
        {
            this.gameBoard.InitializeEmptyBoard();
            BoardField[] startFields = this.gameBoard.GetStartFields();

            for (var i = 0; i < this.players.Count(); i++)
            {
                var newPlayer = new Player(this.players[i], startFields[i]);
                this.playerList.Add(newPlayer);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {

        }
    }
}
