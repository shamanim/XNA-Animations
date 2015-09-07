using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GraphicalProgrammingDeluxe
{
    class Circle : GameObject
    {
        public float Radius { get { return Texture.Width; } }

        public Circle(GraphicsDevice graphics, int radius, Vector2 position, Color color) : base(graphics, position, color)
        {
            // Create new Texture 
            int diameter = radius * 2;
            Texture = new Texture2D(graphics, diameter, diameter);
            Color[] c = new Color[radius * radius * 4];

            for (int i = 0; i < diameter; i++)
            {
                for (int j = 0; j < diameter; j++)
                {
                    if (Vector2.Distance(new Vector2(radius, radius), new Vector2(i, j)) < radius)
                    {
                        c[i + j * diameter] = color;
                    }

                    else { c[i + j * diameter] = Color.Transparent; }
                }
            }

            Texture.SetData(c);
        }
    }
}
