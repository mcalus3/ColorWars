using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

using ColorWars.Players;
using ColorWars.Boards;
using ColorWars.Graphics;
using ColorWars.Controllers;

namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private ColorWarsSettings settings;
        private List<Player> playerList;
        private GameBoard gameBoard;
        private GameRenderer gameRenderer;
        private KeyboardInputController gameController;


        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.playerList = new List<Player>();
            this.gameBoard = new GameBoard(this.settings.startingTerritorySize, this.settings.mapDimension);
            this.gameRenderer = new GameRenderer(new GraphicsDeviceManager(this), this.settings.mapDimension, this.settings.windowSize);
            this.gameController = new KeyboardInputController();
        }

        protected override void Initialize()
        {
            this.gameBoard.InitializeEmptyBoard();
            BoardField[] startFields = this.gameBoard.GetStartFields();

            for (var i = 0; i < this.settings.players.Count(); i++)
            {
                var newPlayer = new Player(this.settings.players[i], startFields[i]);
                this.playerList.Add(newPlayer);
                this.gameController.Commands.Add(new PlayerMoveCommand(settings.players[i].keyMapping, this.playerList[i]));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (BoardField field in this.gameBoard.Board)
            {
                this.gameRenderer.AddRenderer(field);
            }
            foreach (Player player in this.playerList)
            {
                this.gameRenderer.AddRenderer(player);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            this.gameController.ExecuteCommands();

            foreach (Player player in this.playerList)
            {
                player.Move();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.gameRenderer.DrawBoard();
            this.gameRenderer.DrawScoreboard(this.gameBoard.GetStatistics());

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            this.gameRenderer.Renderers.Clear();
        }
    }
}
