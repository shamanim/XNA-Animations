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
    class Fyrkant : GameObject
    {
        public Fyrkant(GraphicsDevice graphics, int width, int height, Vector2 position, Color color) : base(graphics, position, color) 
        {
            // Create new Texture in shape of rectangle
            Texture = new Texture2D(graphics, width, height); //Size
            Color[] c = new Color[width * height]; //array of colors, pixels

            for (int i = 0; i < width * height; i++)
            {
                c[i] = color; //for every pixel, fill with color
            }

            Texture.SetData(c); //apply to Texture
        }
    }
}
