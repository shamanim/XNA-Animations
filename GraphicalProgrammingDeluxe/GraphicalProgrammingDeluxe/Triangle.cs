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
    class Triangle : GameObject
    {
        public float rotation;

        public Triangle(GraphicsDevice graphics, int size, Vector2 position, Color color, float rotation) : base(graphics, position, color)
        {
            // Create new Texture 
            Texture = new Texture2D(graphics, size, size); 
            Color[] c = new Color[size * size];

            for (int i = size - 1; i >= 0; i--)
            {
                for (int j = 0; j < size; j++)
                {
                    c[i + j * size] = color;

                    if (j > i) { c[i + j * size] = Color.Transparent; }
                }
            }

            Texture.SetData(c);

            this.rotation = rotation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, (float)(7 * Math.PI/4 + MathHelper.ToRadians(rotation)), Center - Position, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
