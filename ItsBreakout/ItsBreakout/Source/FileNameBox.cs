using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ItsBreakout.Source
{
    class TextPopupState : DrawableGameComponent
    {
        SpriteFont font;
        Texture2D bigBox, textBox;
        Rectangle bigBoxRectangle;
        Rectangle textBoxRectangle;
        string currentString;
        KeyboardState lastState;

        public string EnteredString
        {
            get { return currentString; }
        }

        SpriteBatch SpriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        }
        public TextPopupState(Game game, StateEngine stateEngine) : base(game)
        {
            currentString = "";

            bigBox = new Texture2D(game.GraphicsDevice, 1, 1);
            bigBox.SetData(new Color[] { Color.Gray });

            textBox = new Texture2D(game.GraphicsDevice, 1, 1);
            textBox.SetData(new Color[] { Color.DarkGray });

            int bigWidth = 150;
            int bigHeight = 100;
            int textWidth = 80;
            int textHeight = 30;

            bigBoxRectangle = new Rectangle(game.Window.ClientBounds.Width / 2 - bigWidth / 2,
                game.Window.ClientBounds.Height / 2 - bigHeight / 2, bigWidth, bigHeight);
            textBoxRectangle = new Rectangle(bigBoxRectangle.X + bigWidth / 2 - textWidth / 2,
                bigBoxRectangle.Y + bigHeight / 2 - textHeight / 2, textWidth, textHeight);
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("font");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 'A'; i < 'Z'; i++)
            {
                if (Keyboard.GetState().IsKeyDown((Keys)i) && lastState.IsKeyUp((Keys)i))
                {
                    currentString += (char)i;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) 
                && lastState.IsKeyUp(Keys.Back) 
                && currentString.Length > 0)
            {
                currentString = currentString.Remove(currentString.Length - 1);
            }

            lastState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(bigBox, bigBoxRectangle, Color.White);
            SpriteBatch.Draw(textBox, textBoxRectangle, Color.White);
            SpriteBatch.DrawString(font, currentString, new Vector2(textBoxRectangle.X, textBoxRectangle.Y), Color.Black);
            base.Draw(gameTime);
        }
    }
}
