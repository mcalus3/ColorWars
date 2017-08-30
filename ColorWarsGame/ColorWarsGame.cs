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
            this.gameRenderer = new GameRenderer(new GraphicsDeviceManager(this), this.settings);
            this.gameController = new KeyboardInputController();
        }

        protected override void Initialize()
        {
            this.gameRenderer.Initialize();

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
            this.gameRenderer.CreateFieldRenderers(this.gameBoard.Board.ToArray());
            this.gameRenderer.CreatePlayerRenderers(this.playerList.ToArray());
            this.gameRenderer.CreateFieldRenderers(this.playerList.Select(p => p.Tail).ToArray());

            this.gameBoard.ClaimStartingTerritories(this.playerList.ToArray());
        }

        protected override void Update(GameTime gameTime)
        {
            //Check for endGame
            if ((int)gameTime.TotalGameTime.TotalSeconds >= this.settings.endTime)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }

                return;
            }

            //Read and evaluate input
            this.gameController.ExecuteCommands();

            //Move players
            foreach (Player player in this.playerList)
            {
                player.Move();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.gameRenderer.DrawBoard();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            this.gameRenderer.Renderers.Clear();
        }
    }
}
