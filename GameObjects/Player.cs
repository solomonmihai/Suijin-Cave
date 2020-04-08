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
                new Handgun(this),
                new M4A1(this)
            };

            CurrentGunIndex = 0;
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

            if (Guns[CurrentGunIndex].Type == Gun.GunType.Auto)
            {
                if (Mouse.IsDown(MouseButton.LeftButton))
                {
                    Guns[CurrentGunIndex].Shoot(MouseDirection);
                }
            }

            Guns[CurrentGunIndex].Update(dt, GameScreen.Camera.MouseInWorld);
        }

        public void MousePressed(float x, float y, int button, bool isTouch)
        {
            if (Guns[CurrentGunIndex].Type == Gun.GunType.Pistol)
            {
                Guns[CurrentGunIndex].Shoot(MouseDirection);
            }
        }

        public void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            switch (scancode)
            {
                case Scancode.Number1:
                    CurrentGunIndex = 0;
                    break;
                case Scancode.Number2:
                    CurrentGunIndex = 1;
                    break;
            }
        }

        public Vector2 MouseDirection => new Vector2(GameScreen.Camera.MouseInWorld.X - Position.X, GameScreen.Camera.MouseInWorld.Y - Position.Y);

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
            Guns[CurrentGunIndex].Draw();

            foreach (var g in Guns)
            {
                g.DrawBulelts();
            }
        }

        public void DrawUI()
        {
            DrawGunInventory();
        }

        int gunCellWidth = 60;

        private void DrawGunInventory()
        {
            Graphics.SetColor(1, 1, 1);

            for (int i = 0; i < Guns.Count; i++)
            {
                Graphics.Rectangle(DrawMode.Line, 200 + i * gunCellWidth, Graphics.GetHeight() - gunCellWidth * 1.2f, gunCellWidth, gunCellWidth);

                if (CurrentGunIndex == i)
                {
                    Graphics.Rectangle(DrawMode.Line, 200 + gunCellWidth * 0.1f + i * gunCellWidth, Graphics.GetHeight() - gunCellWidth * 1.1f, gunCellWidth * 0.8f, gunCellWidth * 0.8f);
                }

                Graphics.Draw(Guns[i].Texture, 200 + i * gunCellWidth + (gunCellWidth / 2), Graphics.GetHeight() - gunCellWidth * 0.6f, -Mathf.PI / 6, 1.4f, 1.4f, Guns[i].Origin.X, Guns[i].Origin.Y);
            
                Graphics.SetFont();
                Graphics.Print(Guns[i].Ammmo.ToString(), 200 + (i * gunCellWidth) + gunCellWidth * 0.25f, Graphics.GetHeight() - gunCellWidth * 1.5f);
            }
        }
    }
}