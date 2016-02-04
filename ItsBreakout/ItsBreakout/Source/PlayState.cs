using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        SpriteFont font;

        public PlayState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
            // TODO get currentLevel from settings
            blockTextures = new Dictionary<int, Texture2D>();
            player = new Player(game);
        }


        public override void Initialize()
        {
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
            font = Game.Content.Load<SpriteFont>("font");
            LoadNewLevel();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Game won
            if (blockCollection.Blocks.Count <= 0)
            {
                StateEngine.PushState(new LevelWonState(Game, StateEngine));
                BreakoutGame.currentLevel++;
                LoadNewLevel();
                return;
            }
            // Game lost
            else if (player.ball.Position.Y > player.Position.Y)
            {
                StateEngine.PushState(new LevelLostState(Game, StateEngine));
                BreakoutGame.Lives--;
                if (BreakoutGame.Lives == 0)
                {
                    BreakoutGame.currentLevel = 1;
                }
                LoadNewLevel();
            }

            // Update player
            player.Update(ref blockCollection);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            // Draw background
            SpriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // Draw blocks
            foreach (BlockData block in blockCollection.Blocks)
            {
                SpriteBatch.Draw(blockTextures[block.HitPoints], block.Position, Color.White);
            }

            // Draw player
            player.Draw(SpriteBatch);

            SpriteBatch.DrawString(font, "Lives left: " + BreakoutGame.Lives.ToString(), Vector2.Zero, Color.Black);

            SpriteBatch.End();
            base.Draw(gameTime);
        }

        void LoadNewLevel()
        {
            blockCollection = BlockCollection.Load(@"map" + BreakoutGame.currentLevel.ToString());
            player.Reset();
        }

    }
}
