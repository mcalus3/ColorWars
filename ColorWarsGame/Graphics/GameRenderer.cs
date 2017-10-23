using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using ColorWars.Players;
using ColorWars.Boards;

using ColorWars.Services;
using IDrawable = ColorWars.Services.IDrawable;

namespace ColorWars.Graphics
{
    internal class GameRenderer
    {
        public List<IDrawable> Renderers {get; set;}
        public GraphicsDeviceManager Graphics{get; set;}
        public List<PlayerRenderer> PlayerRenderers { get; }

        private readonly Point mapDimension;
        private SpriteBatch sBatch;

        public GameRenderer(GraphicsDeviceManager gManager, ColorWarsSettings settings)
        {
            this.Graphics = gManager;
            this.Graphics.PreferredBackBufferWidth = settings.WindowSize.X;
            this.Graphics.PreferredBackBufferHeight = settings.WindowSize.Y;
            this.mapDimension = settings.MapDimension;

            this.Renderers = new List<IDrawable>();
            this.PlayerRenderers = new List<PlayerRenderer>();
        }

        public void Initialize()
        {
            this.sBatch = new SpriteBatch(this.Graphics.GraphicsDevice);
        }

        public void DrawBoard()
        {
            this.sBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            foreach (IDrawable renderer in this.Renderers)
            {
                renderer.Draw();

            }

            foreach(IDrawable renderer in this.PlayerRenderers)
            {
                renderer.Draw();
            }

            this.sBatch.End();
        }

        public void CreateFieldRenderers(ISquareDrawable[] drawables)
        {
            foreach (ISquareDrawable drawable in drawables)
            {
                this.Renderers.Add(new FieldRenderer(this.Graphics, drawable, this.mapDimension, this.sBatch));
            }
        }

        public void CreatePlayerRenderers(PlayerModel[] drawables)
        {
            foreach (PlayerModel drawable in drawables)
            {
                this.PlayerRenderers.Add(new PlayerRenderer(this.Graphics, drawable, this.mapDimension, this.sBatch));
            }
        }

        public void CreateScoreboardRenderer(Scoreboard scoreboard)
        {
            this.Renderers.Add(new ScoreboardRenderer(this.Graphics, scoreboard, this.sBatch));
        }

        public void RemovePlayerRenderer(IPlayer player)
        {
            this.PlayerRenderers.RemoveAll(r => r.RenderedPlayer == player);
        }
    }
}
