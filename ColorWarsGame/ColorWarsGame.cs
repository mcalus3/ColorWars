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
using ColorWars.Services;


namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private ColorWarsSettings settings;
        private List<Player> playerList;
        private GameBoard gameBoard;
        private GameRenderer gameRenderer;
        private KeyboardInputController gameController;
        private Scoreboard scoreboard;

        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.playerList = new List<Player>();
            this.gameBoard = new GameBoard(this.settings.startingTerritorySize, this.settings.mapDimension);
            this.gameRenderer = new GameRenderer(new GraphicsDeviceManager(this), this.settings);
            this.gameController = new KeyboardInputController();
            this.scoreboard = new Scoreboard(this.playerList, this.gameBoard);
        }

        protected override void Initialize()
        {
            this.gameRenderer.Initialize();

            this.gameBoard.InitializeEmptyBoard();

            this.AddPlayers();

            base.Initialize();
        }

        private void AddPlayers()
        {
            BoardField[] startFields = this.gameBoard.GetStartFields();

            for (var i = 0; i < this.settings.playersCount; i++)
            {
                var newPlayer = new Player(this.settings.players[i], startFields[i]);
                this.playerList.Add(newPlayer);
                this.gameController.Commands.Add(new PlayerMoveCommand(settings.players[i].keyMapping, this.playerList[i]));
                newPlayer.TerritoryAddedEvent += this.scoreboard.Update;
            }
        }

        protected override void LoadContent()
        {
            this.gameRenderer.CreateFieldRenderers(this.gameBoard.Board.Cast<ISquareDrawable>().ToArray());
            this.gameRenderer.CreatePlayerRenderers(this.playerList.ToArray());
            this.gameRenderer.CreateFieldRenderers(this.playerList.Select(p => p.Tail).Cast<ISquareDrawable>().ToArray());
            this.gameRenderer.CreateScoreboardRenderer(this.scoreboard);

            this.gameBoard.ClaimStartingTerritories(this.playerList.ToArray());
        }

        protected override void Update(GameTime gameTime)
        {
            //Check for endGame
            if ((int)gameTime.TotalGameTime.TotalSeconds >= this.settings.endTime)
            {
                WaitForKeyAndExit();
                return;
            }

            //Read and evaluate input
            this.gameController.ExecuteCommands();

            //Move players
            foreach (Player player in this.playerList.ToArray())
            {
                player.Move();

                //Delete players without territory
                if (player.Stats.Territory == 0)
                {
                    player.RemoveFromGame();
                    this.playerList.Remove(player);
                    //TODO: Remove renderer and scoreboard token
                    //this.gameRenderer.Renderers.Remove(this.gameRenderer.Renderers.Single(r => r.Stats.Territory == 0));
                    //this.Scoreboard
                }
            }

            base.Update(gameTime);
        }

        private void WaitForKeyAndExit()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter) || state.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            this.gameRenderer.DrawBoard();
        }

        protected override void UnloadContent()
        {
            this.gameRenderer.Renderers.Clear();
        }
    }
}