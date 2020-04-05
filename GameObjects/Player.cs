using Love;

using System;

namespace Suijin_cave.GameObjects
{
    public class Player : GameObject
    {
        int width;

        public Player()
        {
            Position = new Vector2(Graphics.GetWidth() / 2, Love.Graphics.GetHeight() / 2);
            width = 32;
        }

        Vector2 direction;
        int speed = 400;

        private void Move(float dt)
        {
            direction = Vector2.Zero;
            if (Keyboard.IsDown(KeyConstant.W))
            {
                direction.Y = -1;
            }
            if (Keyboard.IsDown(KeyConstant.S))
            {
                direction.Y = 1;
            }
            if (Keyboard.IsDown(KeyConstant.A))
            {
                direction.X = -1;
            }
            if (Keyboard.IsDown(KeyConstant.D))
            {
                direction.X = 1;
            }

            if (direction != Vector2.Zero) direction.Normalize();

            Position += direction * speed * dt;
        }

        void CheckCollisions()
        {
            foreach (var rect in GameScreen.Map.Blocks)
            {
                var collission = Help.RectangleCollision(Rectangle, (Rectangle)rect);
                if (collission != Help.CollisionSide.None)
                {
                    if (collission == Help.CollisionSide.Top)
                    {
                        Position = new Vector2(Position.X, rect.Top + rect.Height + Rectangle.Height / 2);
                    }
                    if (collission == Help.CollisionSide.Bottom)
                    {
                        Position = new Vector2(Position.X, rect.Top - Rectangle.Height / 2);
                    }
                    if (collission == Help.CollisionSide.Right)
                    {
                        Position = new Vector2(rect.Left - Rectangle.Width / 2, Position.Y);
                    }
                    if (collission == Help.CollisionSide.Left)
                    {
                        Position = new Vector2(rect.Left + rect.Width + Rectangle.Width / 2, Position.Y);
                    }
                }
            }
        }

        public override void Update(float dt)
        {
            Move(dt);
            CheckCollisions();
            Rectangle = Rectangle.FromPosition(Position, width);
        }

        public override void Draw()
        {
            Graphics.Rectangle(DrawMode.Fill, Rectangle);
        }
    }
}