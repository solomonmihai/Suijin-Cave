using Love;

namespace Suijin_cave.GameObjects
{
    public class Handgun : Gun
    {
        private Vector2 initialParentOffset;
        public Handgun(GameObject parent)
        {
            Parent = parent;
            Texture = Assets.Handgun;
            Scale = new Vector2(1.8f, 1.8f);

            Origin = new Vector2(Texture.GetWidth() / 2, Texture.GetHeight() / 2);
            ParentOffset = new Vector2(50, 40);
            initialParentOffset = ParentOffset;
        }

        private static Vector2 bulletOffset = new Vector2(0, Mathf.Sin(-Mathf.PI / 2) * 9);
        
        public override void Shoot(Vector2 direction)
        {
            Bullets.Add(new Bullet(Position + bulletOffset, direction));
            // Recoil
            ParentOffset = initialParentOffset * 0.65f;
        }

        public override void Update(float dt, Vector2 target)
        {
            // Reset recoil
            if (ParentOffset.X < initialParentOffset.X) ParentOffset += new Vector2(120 * dt, 0);
            if (ParentOffset.Y < initialParentOffset.Y) ParentOffset += new Vector2(0, 120 * dt);

            RotateToTarget(target);
            FlipToTarget(target);
        }

        public override void Draw()
        {
            Love.Graphics.Draw(Texture, Position.X, Position.Y, Rotation, Scale.X, Scale.Y, Origin.X, Origin.Y);
        }
    }
}