using Love;

namespace Suijin_cave.GameObjects
{
    public class Bullet : GameObject
    {
        protected float Speed { get; set; }
        protected Vector2 Direction { get; set; }

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            Texture = Assets.Bullet;
            Origin = new Vector2(Texture.GetWidth() / 2, Texture.GetHeight() / 2);
            Direction = direction.Normalized();
            Rotation = Mathf.Atan2(Direction.Y, Direction.X);
            Scale = new Vector2(1.8f, 1.8f);
            Speed = 800;
        }

        public override void Update(float dt)
        {
            Position += Direction * Speed * dt;
        }

        public override void Draw()
        {
            Graphics.Draw(Texture, Position.X, Position.Y, Rotation, Scale.X, Scale.Y, Origin.X, Origin.Y);
        }
    }
}