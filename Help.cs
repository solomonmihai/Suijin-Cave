using Love;

using System;

namespace Suijin_cave
{
    public static class Help
    {
        public static Rectangle FromPosition(this Rectangle rectangle, Vector2 position, int width, int height)
        {
            return new Rectangle((int)(position.X - width / 2), (int)(position.Y - height / 2), width, height);
        }

        public static Rectangle FromPosition(this Rectangle rectangle, Vector2 position, int size)
        {
            return new Rectangle((int)(position.X - size / 2), (int)(position.Y - size / 2), size, size);
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }

        public enum CollisionSide
        {
            Left,
            Right,
            Top,
            Bottom,
            None
        }

        public static CollisionSide RectangleCollision(Rectangle r1, Rectangle r2)
        {
            float w = 0.5f * (r1.Width + r2.Width);
            float h = 0.5f * (r1.Height + r2.Height);
            float dx = r1.Center.X - r2.Center.X;
            float dy = r1.Center.Y - r2.Center.Y;

            if (r1.IntersectsWith(r2))
            {
                float wy = w * dy;
                float hx = h * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        return CollisionSide.Top;
                    }
                    else
                    {
                        return CollisionSide.Right;
                    }
                }
                else
                {
                    if (wy > -hx)
                    {
                        return CollisionSide.Left;
                    }
                    else
                    {
                        return CollisionSide.Bottom;
                    }
                }

            }
            else
            {
                return CollisionSide.None;
            }
        }

        public static float MapValue(float value, float start1, float stop1, float start2, float stop2)
        {
            var outgoing = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
            return outgoing;
        }
    }
}