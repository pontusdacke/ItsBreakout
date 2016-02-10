using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsBreakout.Source
{
    class Trail
    {
        Texture2D texture;
        List<Vector2> positions;
        const float trailPositionCount = 20;
        public Trail(Game game, Color color)
        {
            positions = new List<Vector2>();
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData<Color>(
                new Color[] { color }); 
        }

        public void AddPosition(Vector2 position)
        {
            positions.Add(position);
            if (positions.Count > trailPositionCount)
                positions.RemoveAt(0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i < positions.Count; i++)
            {
                DrawLine(spriteBatch, positions[i], positions[i - 1], i / trailPositionCount);
            }
        }
        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, float fadeValue)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Black * fadeValue, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

        public void Clear()
        {
            positions.Clear();
        }
    }
}
