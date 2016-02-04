using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout.Engine
{
    public class GameObject
    {
        public Vector2 Position; // Position on screen for the game object
        public Texture2D Texture;
        public Color Color;

        public bool Hide
        {
            get;
            set;
        }
        public int Width
        {
            get { return Texture.Width; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public GameObject(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            Color = Color.White;
        }
        public GameObject(Vector2 position, Texture2D texture, Color color)
        {
            Position = position;
            Texture = texture;
            Color = color;
        }

        public GameObject()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null && !Hide)
                spriteBatch.Draw(Texture, Position, Color);
        }
    }
}
