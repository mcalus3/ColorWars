using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

using ColorWars.Players;
using ColorWars.Boards;
using ColorWars.Graphics;
using ColorWars.Actors;
using ColorWars.Services;


namespace ColorWars
{
    class ColorWarsGame : Game
    {
        private ColorWarsSettings settings;
        private List<PlayerModel> playerList;
        private List<PlayerController> playerControllersList;
        private GameBoard gameBoard;
        private GameRenderer gameRenderer;
        private ActorList gameActors;
        private Scoreboard scoreboard;

        public ColorWarsGame(ColorWarsSettings settings)
        {
            this.settings = settings;
            this.playerList = new List<PlayerModel>();
            this.playerControllersList = new List<PlayerController>();
            this.gameBoard = new GameBoard(this.settings.startingTerritorySize, this.settings.mapDimension);
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

            for (var i = 0; i < this.settings.playersCount; i++)
            {
                PlayerSettings playerSettings = this.settings.players[i];
                var newPlayer = new PlayerModel(playerSettings.color, startFields[i]);
                this.playerList.Add(newPlayer);
                var controller = new PlayerController(newPlayer, startFields[i], playerSettings.speed, playerSettings.deathPenalty);
                this.playerControllersList.Add(controller);
                this.gameActors.Actors.Add(new KeyboardActor(playerSettings.keyMapping, controller));
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