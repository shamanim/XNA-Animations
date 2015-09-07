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
    abstract class GameObject
    {
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Center { get { return new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2); } }
        public Color Color { get; set; }
        public float Duration { get; set; }

        public Vector2 startPosition;

        public GraphicsDevice graphics;
        public Random random;

        public bool dead;
        public bool hasDuration;

        public double aliveTime;
        double deltaTime;

        public GameObject(GraphicsDevice graphics, Vector2 position, Color color)
        {
            this.Name = this.GetType().ToString().Split('.')[1];
            this.Position = position;
            this.Color = color;

            startPosition = Position;

            this.graphics = graphics;
            random = new Random();
        }

        public GameObject(GraphicsDevice graphics, Vector2 position, Color color, float duration)
        {
            this.Position = position;
            this.Color = color;
            this.Duration = duration;
            this.hasDuration = true;

            startPosition = position;

            this.graphics = graphics;
            random = new Random();


        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            aliveTime += deltaTime;

            if (hasDuration)
            {
                Duration -= gameTime.ElapsedGameTime.Seconds;
                if (Duration <= 0) { dead = true; }
            }

            if (Color.A < 2) { dead = true; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null) { spriteBatch.Draw(Texture, Position, null, Color, 0, Center - Position, 1.0f, SpriteEffects.None, 1.0f); }
        }

        public virtual void Scroll(float radius, float speed, bool horizontally, bool vertically)
        {
            this.Position += new Vector2((horizontally) ? speed : 0, (vertically) ? speed : 0);

            if (speed > 0)
            {
                if (this.Position.X > graphics.Viewport.Width) { this.Position = new Vector2(Texture.Width / 2, Position.Y); }
                if (this.Position.Y > graphics.Viewport.Height) { this.Position = new Vector2(Position.X, Texture.Height / 2); }
            }
            else
            {
                if (this.Position.X < 0) { this.Position = new Vector2(graphics.Viewport.Width - Texture.Width / 2, Position.Y); }
                if (this.Position.Y < 0) { this.Position = new Vector2(Position.X, graphics.Viewport.Height - Texture.Height / 2); }
            }
        }

        public virtual void VerticalBob(float radius, float speed)
        {
            float deltaY = (float)(Math.Sin(aliveTime * speed + deltaTime * speed) - Math.Sin(aliveTime * speed)) * radius;
            Position += new Vector2(0, deltaY);         
        }

        public virtual void HorizontalBob(float radius, float speed)
        {
            float deltaX = (float)(Math.Sin(aliveTime * speed + deltaTime * speed) - Math.Sin(aliveTime * speed)) * radius;
            Position += new Vector2(deltaX, 0);
        }

        public virtual void RevolveAroundPoint(Vector2 origin, float radius, float speed)
        {
            Position = origin + new Vector2((float)Math.Cos((aliveTime) * speed) * radius, (float)Math.Sin((aliveTime) * speed) * radius);
        }

        public virtual void RevolveAroundPoint(Vector2 origin, float radius, int circlePlacement, float speed)
        {
            Position = GetLerpToPoint(origin + new Vector2((float)Math.Cos((aliveTime + MathHelper.ToRadians(circlePlacement)) * speed) * radius,
                (float)Math.Sin((aliveTime + circlePlacement) * speed) * radius), 0.02f);
        }

        public virtual void RevolveSeveralAroundPoint(Vector2 origin, float radius, float circlePlacement, float speed)
        {
            Position = GetLerpToPoint(origin + new Vector2((float)Math.Cos((aliveTime + MathHelper.ToRadians(circlePlacement)) * speed) * radius,
                (float)Math.Sin((aliveTime + MathHelper.ToRadians(circlePlacement)) * speed) * radius), 0.02f);
        }

        public virtual void SmoothStepToPoint(Vector2 targetPosition, float amount)
        {
            this.Position = Vector2.SmoothStep(Position, targetPosition, amount);
        }

        public virtual void LerpToPoint(Vector2 targetPosition, float amount)
        {
            this.Position = Vector2.Lerp(Position, targetPosition, amount);
        }

        public virtual Vector2 GetLerpToPoint(Vector2 targetPosition, float amount)
        {
            return Vector2.Lerp(Position, targetPosition, amount);
        }

        public virtual void StutterRandomly(int intensity)
        {
            this.Position += new Vector2(random.Next(0, intensity), random.Next(0, intensity));
        }

        public virtual void ExplodeRadially(float speed, int index)
        {

        }

        public virtual void DestroyIfNearing(Vector2 targetPosition)
        {
            if (Vector2.Distance(Position, targetPosition) < (Texture.Width * Texture.Height) / 10) { dead = true; }
        }
    }
}
