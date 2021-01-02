using ItsBreakout.Engine;
using ItsBreakout.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace ItsBreakout
{
    class MapEditorState : GameState
    {
        private bool savePressed = false;
        private bool loadPressed = false;
        private bool leftMousePressed = false;
        private bool rightMousePressed = false;
        private bool showTextBox = false;

        private Dictionary<int, Texture2D> blockTextures;
        private BlockCollection map;
        private Rectangle windowRectangle = new Rectangle(0, 0, 800, 600);

        private readonly TextPopupState textPopup;

        private Texture2D background;

        public MapEditorState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
            textPopup = new TextPopupState(game, stateEngine);
        }

        public override void Initialize()
        {
            map = new BlockCollection();
            textPopup.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            blockTextures = new Dictionary<int, Texture2D>();
            blockTextures.Add(1, Game.Content.Load<Texture2D>("block_green"));
            blockTextures.Add(2, Game.Content.Load<Texture2D>("block_red"));
            blockTextures.Add(3, Game.Content.Load<Texture2D>("block_teal"));
            blockTextures.Add(4, Game.Content.Load<Texture2D>("block_blue"));
            blockTextures.Add(5, Game.Content.Load<Texture2D>("block_pink"));
            background = Game.Content.Load<Texture2D>("background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Dont allow clicks outside the window.
            Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            if (!mouseRect.Intersects(windowRectangle)) return;

            // Popup box
            // TODO it should handle itself somehow.
            if (showTextBox)
            {
                textPopup.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    showTextBox = false;
                    int tempNumber = 1;
                    var enteredText = textPopup.EnteredString;
                    var fileName = enteredText;
                    while (File.Exists(fileName))
                    {
                        tempNumber++;
                        fileName = enteredText + tempNumber.ToString();
                    }
                    map.Save(fileName);
                }

                return;
            }
            
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
                showTextBox = true;

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
                // TODO Load map
                //map = BlockCollection.Load(@"map1");
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl) && Keyboard.GetState().IsKeyUp(Keys.L))
                loadPressed = false;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(background, Vector2.Zero, Color.White);

            foreach (BlockData b in map.Blocks)
            {
                SpriteBatch.Draw(blockTextures[b.HitPoints], b.Rectangle, Color.White);
            }

            if (showTextBox)
            {
                textPopup.Draw(gameTime);
            }

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
