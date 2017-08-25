using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private GameRenderer graphics;
        private List<Player> playerList;
        private GameBoard gameBoard;
        private ColorWarsSettings settings;
        private KeyboardInputController inputController;
        private bool paused = false;

        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.graphics = new GameRenderer(new GraphicsDeviceManager(this), this.settings.mapDimension, this.settings.windowSize, Content);


        }

        protected override void Initialize()
        {
            this.playerList = new List<Player>();
            this.gameBoard = new GameBoard(this.settings.startingTerritorySize, this.settings.mapDimension);
            this.inputController = new KeyboardInputController();

            this.gameBoard.InitializeEmptyBoard();
            BoardField[] startFields = this.gameBoard.GetStartFields();

            for (var i = 0; i < settings.playerSettings.Length; i++)
            {
                var newPlayer = new Player(settings.playerSettings[i].color, startFields[i], this.settings.playerSettings[i].speed);
                newPlayer.setGameBoard(this.gameBoard);
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
                this.graphics.AddRenderer(player.tail);
            }
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!this.paused)
            {
                this.inputController.ExecuteCommands();

                foreach (Player player in this.playerList)
                {
                    player.Move();
                }
            }
            if (this.EndGameCondition(gameTime))
            {
                EndGame();
            }

            base.Update(gameTime);
        }

        private bool EndGameCondition(GameTime gameTime)
        {
            return (int)gameTime.TotalGameTime.TotalSeconds > this.settings.gameTime;
        }
        private void EndGame()
        {
            this.paused = true;

            var keyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                this.Exit();
            }
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.DrawBoard();
            graphics.DrawScoreboard(this.gameBoard.GetStatistics());

            base.Draw(gameTime);
            //if (this.EndGameCondition(gameTime))
            //{
            //    this.endGame = true;
            //    List<BoardField> fields = new List<BoardField>(this.gameBoard.GetFields());
            //    IPlayer[] winners = this.gameBoard.GetStatistics();
            //    this.graphics.DrawEndGameMessage(winners);
            //    this.EndDraw();
            //}

        }

        protected override void UnloadContent()
        {
            this.graphics.RemoveAllRenderers();
        }
    }
}
