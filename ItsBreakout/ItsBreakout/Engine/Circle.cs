using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Breakout
{
    class Circle
    {
        public int Radius;
        public Vector2 Center;

        public Circle(int radius, Vector2 center)
        {
            Radius = radius;
            Center = center;
        }

        public bool Intersects(Rectangle Rectangle)
        {
            Vector2 distance;
            distance.X = Math.Abs((Center.X) - Rectangle.Center.X);
            distance.Y = Math.Abs((Center.Y) - Rectangle.Center.Y);

            if (distance.X > (Rectangle.Width / 2 + Radius)) return false;
            if (distance.Y > (Rectangle.Height / 2 + Radius)) return false;

            if (distance.X <= (Rectangle.Width / 2)) return true;
            if (distance.Y <= (Rectangle.Height / 2)) return true;

            double cdistance = Math.Pow((distance.X - Rectangle.Width / 2), 2) +
                            Math.Pow((distance.Y - Rectangle.Height / 2), 2);

            return (cdistance <= (Math.Pow(Radius, 2)));
        }
    }
}
