using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

using ColorWars.Players;
using ColorWars.Boards;
using ColorWars.Graphics;
using ColorWars.Actors;
using ColorWars.PlayerControllers;
using ColorWars.Services;


namespace ColorWars
{
    internal class ColorWarsGame : Game
    {
        private readonly ColorWarsSettings settings;
        private readonly List<PlayerModel> playerList;
        private readonly List<PlayerController> playerControllersList;
        private readonly GameBoard gameBoard;
        private readonly GameRenderer gameRenderer;
        private readonly ActorList gameActors;
        private readonly Scoreboard scoreboard;

        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.playerList = new List<PlayerModel>();
            this.playerControllersList = new List<PlayerController>();
            this.gameBoard = new GameBoard(this.settings.StartingTerritorySize, this.settings.MapDimension);
            this.gameRenderer = new GameRenderer(new GraphicsDeviceManager(this), this.settings);
            this.gameActors = new ActorList();
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

            for (var i = 0; i < this.settings.PlayersCount; i++)
            {
                PlayerSettings playerSettings = this.settings.Players[i];
                var newPlayer = new PlayerModel(playerSettings.Color, startFields[i]);
                this.playerList.Add(newPlayer);
                var controller = new PlayerController(newPlayer, startFields[i], playerSettings.Speed, playerSettings.DeathPenalty);
                this.playerControllersList.Add(controller);
                this.gameActors.Actors.Add(new KeyboardActor(playerSettings.KeyMapping, controller));
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
            if ((int)gameTime.TotalGameTime.TotalSeconds >= this.settings.EndTime)
            {
                WaitForKeyAndExit();
                return;
            }

            //Read and evaluate input
            this.gameActors.ExecuteActors();

            //Move players
            foreach(PlayerController player in this.playerControllersList.ToArray())
            {
                player.Update();

            }
            //Delete players without territory
            foreach(PlayerModel player in this.playerList.ToArray())
            {
                if(player.Stats.Territory == 0)
                {
                    this.playerList.Remove(player);
                    this.gameRenderer.RemovePlayerRenderer(player);
                    this.gameActors.RemoveActor(player);
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