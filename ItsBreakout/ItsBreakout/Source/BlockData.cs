using Microsoft.Xna.Framework;

namespace ItsBreakout.Source
{
    public class BlockData
    {
        public Vector2 Position;
        public int HitPoints;
        public Color Color;
        public Rectangle Rectangle;

        public BlockData(int hitPoints, Vector2 position, Vector2 blockSize)
        {
            Position = position;
            HitPoints = hitPoints;
            Color = Color.Red;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)blockSize.X, (int)blockSize.Y);
        }
        public BlockData()
        {

        }
        public void IncreaseHitPoints()
        {
            if (HitPoints < 5)
                HitPoints++;
        }

    }
}
