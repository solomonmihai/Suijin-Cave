using Love;

namespace Suijin_cave
{
    public abstract class GameObject
    {
        public bool Dead { get; set; } = false;
        public Image Texture { get; set; }
        public virtual Rectangle Rectangle
        {
            get
            {
                return Help.RectangleFromPosition(Position, (int)(Texture.GetWidth() * Scale.X), (int)(Texture.GetHeight() * Scale.Y));
            }
        }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public float Rotation { get; set; } = 0;

        public virtual void Update(float dt) { }
        public virtual void Draw() { }
    }
}