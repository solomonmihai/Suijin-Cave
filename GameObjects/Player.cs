using Love;
using System;
using System.Collections.Generic;

namespace Suijin_cave.GameObjects
{
    public class Player : GameObject
    {
        int width;

        public List<Gun> Guns { get; private set; }
        public int CurrentGunIndex { get; private set; } = 0;

        public override Rectangle Rectangle
        {
            get
            {
                return Help.RectangleFromPosition(Position, width);
            }
        }

        public Player()
        {
            Position = new Vector2(Graphics.GetWidth() / 2, Love.Graphics.GetHeight() / 2);
            width = 32;

            Guns = new List<Gun>()
            {
                new Handgun(this)
            };
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

        // Including bullets
        private void CheckCollisions()
        {
            foreach (var rect in GameScreen.Map.Blocks)
            {
                // Bullet collision 
                foreach (var bullet in Guns[CurrentGunIndex].Bullets)
                {
                    if (bullet.Rectangle.IntersectsWith(rect))
                    {
                        bullet.Dead = true;
                    }
                }

                // Player collisions
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

        private void UpdateGuns(float dt)
        {
            foreach (var gun in Guns)
            {
                gun.UpdateBullets(dt);
            }

            Guns[CurrentGunIndex].Update(dt, GameScreen.Camera.MouseInWorld);
        }

        public void MousePressed(float x, float y, int button, bool isTouch)
        {
            var direction = new Vector2(GameScreen.Camera.MouseInWorld.X - Position.X, GameScreen.Camera.MouseInWorld.Y - Position.Y);
            Guns[CurrentGunIndex].Shoot(direction);
        }

        public override void Update(float dt)
        {
            Move(dt);
            CheckCollisions();
            UpdateGuns(dt);
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1);
            Graphics.Rectangle(DrawMode.Fill, Rectangle);
            foreach (var g in Guns)
            {
                g.DrawBulelts();
            }
            Guns[CurrentGunIndex].Draw();
        }
    }
}