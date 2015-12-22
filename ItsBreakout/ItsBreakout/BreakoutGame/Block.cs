using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout
{
    public class Block : GameObject
    {
        public int HitPoints
        {
            get;
            set;
        }
        public Block()
        {

        }
        public Block(int hitPoints, Vector2 position, Texture2D texture) : base(position, texture)
        {
            this.HitPoints = hitPoints;
            Color = Color.Red;
        }
        public void Hit()
        {
            HitPoints--;
            switch (HitPoints)
            {
                case 1: Color = Color.Red; break;
                case 2: Color = Color.Blue; break;
                case 3: Color = Color.Green; break;
                case 4: Color = Color.Orange; break;
                case 5: Color = Color.Purple; break;
            }
        }
        public void Heal()
        {
            HitPoints++;
            switch (HitPoints)
            {
                case 1: Color = Color.Red; break;
                case 2: Color = Color.Blue; break;
                case 3: Color = Color.Green; break;
                case 4: Color = Color.Orange; break;
                case 5: Color = Color.Purple; break;
            }
        }
    }
}
