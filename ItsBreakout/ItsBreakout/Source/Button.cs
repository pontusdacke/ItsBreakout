using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ItsBreakout.Source
{
    class Button : DrawableGameComponent
    {
        Rectangle rectangle;
        Vector2 position;
        Texture2D texture;
        MouseState lastState;
        float currentSize;
        string texturePath;

        public delegate void OnClickEvent();

        public OnClickEvent OnPress;

        const float normalSize = 1.0f;
        const float hoverSize = 1.05f;
        const float pressedSize = 0.95f;

        SpriteBatch spriteBatch
        {
            get
            {
                return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            }
        }


        public Button(Game game, Vector2 position, string texturePath) : base(game)
        {
            currentSize = normalSize;
            this.texturePath = texturePath;
            this.position = position;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }

        protected override void LoadContent()
        {
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content\\" + texturePath));
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;

            Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);

            if (mouseRect.Intersects(rectangle))
            {
                currentSize = hoverSize;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    currentSize = pressedSize;
                }

                if (lastState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    OnPress?.Invoke();
                }
            }
            else
            {
                currentSize = normalSize;
            }

            lastState = Mouse.GetState();
        }

        public override void Draw(GameTime gameTime)
        {
            float fade = Enabled ? 1.0f : 0.5f;

            if (texture != null)
            {
                spriteBatch.Draw(texture, position, null, Color.White * fade, 0f, Vector2.Zero, currentSize, SpriteEffects.None, 1.0f);
            }

            base.Draw(gameTime);
        }
    }
}
