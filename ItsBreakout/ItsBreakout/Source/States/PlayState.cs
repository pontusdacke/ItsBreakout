using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ItsBreakout.Engine;

namespace ItsBreakout.Source
{
    class PlayState : GameState
    {
        Player player;
        BlockCollection blockCollection;
        Texture2D backgroundTexture;
        Dictionary<int, Texture2D> blockTextures;
        bool hasStarted;

        // Life bar
        Texture2D lifeTexture;
        const string lifeBarText = "Lives left: ";

        public PlayState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
            // TODO get currentLevel from settings
            blockTextures = new Dictionary<int, Texture2D>();
            player = new Player(game);
        }


        public override void Initialize()
        {
            Game.IsMouseVisible = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            blockTextures.Add(1, Game.Content.Load<Texture2D>("block_green"));
            blockTextures.Add(2, Game.Content.Load<Texture2D>("block_red"));
            blockTextures.Add(3, Game.Content.Load<Texture2D>("block_teal"));
            blockTextures.Add(4, Game.Content.Load<Texture2D>("block_blue"));
            blockTextures.Add(5, Game.Content.Load<Texture2D>("block_pink"));
            backgroundTexture = Game.Content.Load<Texture2D>("background");
            lifeTexture = Game.Content.Load<Texture2D>("life");
            LoadNewLevel();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Update player
            player.Update(ref blockCollection);

            // Game won
            if (blockCollection.Blocks.Count <= 0)
            {
                // Check if all levels are won
                if (BreakoutGame.currentLevel == BreakoutGame.MaxLevel)
                {
                    StateEngine.PushState(new WinState(Game, StateEngine));
                }
                else // Else continue to next level
                {
                    StateEngine.PushState(new FadeInState(Game, StateEngine, new LevelCompletedState(Game, StateEngine)));
                }
            }
            // Game lost
            else if (player.ball.Position.Y > player.Position.Y)
            {
                StateEngine.PushState(new FadeInState(Game, StateEngine, new LevelLostState(Game, StateEngine)));
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            // Draw background
            SpriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // Draw blocks
            foreach (BlockData block in blockCollection.Blocks)
                SpriteBatch.Draw(blockTextures[block.HitPoints], block.Position, Color.White);

            // Draw player
            player.Draw(SpriteBatch);

            // Draw Life
            for (int i = 0; i < BreakoutGame.Lives; i++)
                SpriteBatch.Draw(lifeTexture, new Vector2(10 + (lifeTexture.Width+10) * i, 20), Color.White);

            SpriteBatch.End();
            base.Draw(gameTime);
        }

        void LoadNewLevel()
        {
            if (BreakoutGame.currentLevel > BreakoutGame.MaxLevel)
            {
                StateEngine.PushState(new WinState(Game, StateEngine));
            }

            blockCollection = BlockCollection.Load(@"map" + BreakoutGame.currentLevel.ToString());
            player.Reset();
        }
        public override void Resume()
        {
            LoadNewLevel();
            base.Resume();
        }

    }
}
