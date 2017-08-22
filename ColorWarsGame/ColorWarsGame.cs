using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private GameRenderer graphics;
        private List<Player> playerList;
        private GameBoard gameBoard;
        private ColorWarsSettings settings;
        private KeyboardInputController inputController;
        private int movementAccumulator = 0; //number of refreshes after which game tick runs

        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.playerList = new List<Player>();
            this.gameBoard = new GameBoard(this.settings.startingTerritorySize, this.settings.dimension);
            this.graphics = new GameRenderer(new GraphicsDeviceManager(this), this.settings.dimension, this.settings.windowSize);
            this.inputController = new KeyboardInputController();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.gameBoard.InitializeEmptyBoard();
            BoardField[] startFields = this.gameBoard.GetStartFields();

            for (var i = 0; i < settings.playerSettings.Length; i++)
            {
                var newPlayer = new Player(settings.playerSettings[i].color, startFields[i], this.settings.playerSettings[i].speed);
                this.playerList.Add(newPlayer);
                this.inputController.AddInputCommand(new PlayerMoveCommand(settings.playerSettings[i].keyMapping, newPlayer));
            }

            this.gameBoard.ClaimStartingTerritories(this.playerList.ToArray());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (BoardField field in this.gameBoard.GetFields())
            {
                this.graphics.AddRenderer(field); //TODO: decide whether i need a factory here or leave creation centralized
            }
            foreach (Player player in this.playerList)
            {
                this.graphics.AddRenderer(player);
            }
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.inputController.ExecuteCommands();

            foreach (Player player in this.playerList)
            {
                player.Move();
            }
            
            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.Draw();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
        }
    }
}
