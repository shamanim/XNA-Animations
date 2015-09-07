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
    class RoundedRectangle : Fyrkant
    {
        public RoundedRectangle(GraphicsDevice graphics, int width, int height, int thickness, int radius, Vector2 position, Color color, Color borderColor) 
            : base(graphics, width, height, position, color) 
        {
            Texture = CreateRoundedRectangleTexture(graphics, width, height, thickness, radius, color, borderColor);
        }
        
        public Texture2D CreateRoundedRectangleTexture(GraphicsDevice graphics, int width, int height, int borderThickness, int borderRadius, Color backgroundColor, Color borderColor)
        {
            if (backgroundColor == null) throw new ArgumentException("Must define at least one background color (up to four).");
            if (borderColor == null) throw new ArgumentException("Must define at least one border color (up to three).");
            if (borderRadius < 1) throw new ArgumentException("Must define a border radius (rounds off edges).");
            if (borderThickness < 1) throw new ArgumentException("Must define border thikness.");
            if (borderThickness + borderRadius > height / 2 || borderThickness + borderRadius > width / 2) throw new ArgumentException("Border will be too thick and/or rounded to fit on the texture.");

            Texture2D texture = new Texture2D(graphics, width, height, false, SurfaceFormat.Color);
            Color[] color = new Color[width * height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    
                    color[x + width * y] = backgroundColor;

                    color[x + width * y] = ColorBorder(x, y, width, height, borderThickness, borderRadius, color[x + width * y], borderColor);
                }
            }

            texture.SetData<Color>(color);
            return texture;
        }

        private Color ColorBorder(int x, int y, int width, int height, int borderThickness, int borderRadius, Color initialColor, Color borderColor)
        {
            width -= 1;
            height -= 1;
            Rectangle internalRectangle = new Rectangle((borderThickness + borderRadius), (borderThickness + borderRadius), width - 2 * (borderThickness + borderRadius), height - 2 * (borderThickness + borderRadius));

            if (internalRectangle.Contains(x, y)) return initialColor;

            Vector2 origin = Vector2.Zero;
            Vector2 point = new Vector2(x, y);

            if (x < borderThickness + borderRadius)
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(borderRadius + borderThickness, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(borderRadius + borderThickness, height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(borderRadius + borderThickness, y);
            }
            else if (x > width - (borderRadius + borderThickness))
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(width - (borderRadius + borderThickness), borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(width - (borderRadius + borderThickness), height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(width - (borderRadius + borderThickness), y);
            }
            else
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(x, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(x, height - (borderRadius + borderThickness));
            }

            if (!origin.Equals(Vector2.Zero))
            {
                float distance = Vector2.Distance(point, origin);

                if (distance > borderRadius + borderThickness + 1)
                {
                    return Color.Transparent;
                }
                else if (distance > borderRadius + 1)
                {
                    return borderColor;
                }
            }

            return initialColor;
        }
    }
}
