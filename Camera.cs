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
                Viewport = Help.RectangleFromPosition(Position, Viewport.Width, Viewport.Height);
            }
        }

        private float zoom = 1;
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                Viewport = Help.RectangleFromPosition(Position, (int)(Viewport.Width * zoom), (int)(Viewport.Height * zoom));
            }
        }

        public Vector2 MouseInWorld
        {
            get
            {
                var mouse = Love.Mouse.GetPosition();
                mouse *= zoom;
                mouse += new Vector2(Viewport.X, Viewport.Y);
                return mouse;
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
            Love.Graphics.Scale(Zoom);
            Love.Graphics.Rotate(Rotation);
            Love.Graphics.Translate(-Viewport.X, -Viewport.Y);
        }

        public void End()
        {
            Love.Graphics.Pop();
        }
    }
}