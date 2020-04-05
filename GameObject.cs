using Love;

namespace Suijin_cave
{
    public abstract class GameObject
    {
        public bool Dead { get; set; } = false;
        public Image Texture { get; set; }
        public Rectangle Rectangle { get; protected set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public virtual void Update(float dt) { }
        public virtual void Draw() { }
    }
}