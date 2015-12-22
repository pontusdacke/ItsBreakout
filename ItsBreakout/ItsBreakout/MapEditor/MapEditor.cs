using ItsBreakout;
using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        Texture2D blockTexture;
        Map map;

        public MapEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            map = new Map();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockTexture = Content.Load<Texture2D>("block");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !leftMousePressed)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                map.AddBlock(mousePos, blockTexture);
                leftMousePressed = true;
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed && !rightMousePressed)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                map.RemoveBlock(mousePos);
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
                map.Save(@"map1");

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
                map = Map.Load(@"map1", blockTexture);
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl) && Keyboard.GetState().IsKeyUp(Keys.L))
                loadPressed = false;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Block b in map.Blocks)
            {
                spriteBatch.Draw(b.Texture, b.Rectangle, b.Color);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
