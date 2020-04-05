using Love;

namespace Suijin_cave
{
    public class Camera
    {
        public Rectangle Viewport { get; private set; }
        private Vector2 position;
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                Viewport = Viewport.FromPosition(Position, Viewport.Width, Viewport.Height);
            }
        }

        private float zoom = 1;
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                Viewport = Viewport.FromPosition(Position, (int)(Viewport.Width * zoom), (int)(Viewport.Height * zoom));
            }
        }

        public float Rotation { get; set; } = 0;

        public Camera(Vector2 pos)
        {
            Viewport = new Rectangle(0, 0, Love.Graphics.GetWidth(), Love.Graphics.GetHeight());
            Position = pos;
        }

        public void Begin()
        {
            Love.Graphics.Push();
            Love.Graphics.Translate(-Viewport.X, -Viewport.Y);
            Love.Graphics.Scale(Zoom);
            Love.Graphics.Rotate(Rotation);
        }

        public void End()
        {
            Love.Graphics.Pop();
        }
    }
}