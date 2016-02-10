using ItsBreakout;
using ItsBreakout.Engine;
using ItsBreakout.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace MapBuilder
{
    class MapEditor : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool savePressed = false;
        bool loadPressed = false;
        bool leftMousePressed = false;
        bool rightMousePressed = false;

        Dictionary<int, Texture2D> blockTextures;
        BlockCollection map;
        Rectangle windowRectangle = new Rectangle(0, 0, 800, 600);


        public MapEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = windowRectangle.Height;
            graphics.PreferredBackBufferWidth = windowRectangle.Width;
            graphics.ApplyChanges();

            map = new BlockCollection();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            blockTextures = new Dictionary<int, Texture2D>();
            blockTextures.Add(1, Content.Load<Texture2D>("block_green"));
            blockTextures.Add(2, Content.Load<Texture2D>("block_red"));
            blockTextures.Add(3, Content.Load<Texture2D>("block_teal"));
            blockTextures.Add(4, Content.Load<Texture2D>("block_blue"));
            blockTextures.Add(5, Content.Load<Texture2D>("block_pink"));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Dont allow clicks outside the window.
            Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            if (!mouseRect.Intersects(windowRectangle)) return;


            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !leftMousePressed)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                map.AddBlock(mousePos);
                leftMousePressed = true;
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed && !rightMousePressed)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                map.DamageBlock(mousePos);
                rightMousePressed = true;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                leftMousePressed = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Released)
            {
                rightMousePressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.S) && !savePressed)
            {
                savePressed = true;
                int levelNumber = 1;
                string levelString = "map" + levelNumber.ToString();
                while (File.Exists(levelString))
                {
                    levelNumber++;
                    levelString = "map" + levelNumber.ToString();
                }
                map.Save(levelString);

            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl) && Keyboard.GetState().IsKeyUp(Keys.S))
                savePressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.C))
            {
                map.Blocks.Clear();
            }

            // Load file
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.L) && !loadPressed)
            {
                savePressed = true;
                map = BlockCollection.Load(@"map1");
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl) && Keyboard.GetState().IsKeyUp(Keys.L))
                loadPressed = false;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (BlockData b in map.Blocks)
            {
                spriteBatch.Draw(blockTextures[b.HitPoints], b.Rectangle, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
