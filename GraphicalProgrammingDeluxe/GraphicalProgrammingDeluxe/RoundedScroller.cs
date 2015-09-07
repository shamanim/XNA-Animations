﻿using System;
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
    class RoundedScroller : RoundedRectangle
    {
        private bool horizontally;
        private bool vertically;
        private float speed;

        public RoundedScroller(GraphicsDevice graphics, int width, int height, int thickness, int radius, Vector2 position, Color color, Color borderColor,
            bool horizontally, bool vertically, float speed) : base(graphics, width, height, thickness, radius, position, color, borderColor)
        {
            this.horizontally = horizontally;
            this.vertically = vertically;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Scroll(0, speed, horizontally, vertically);
        }
    }
}
