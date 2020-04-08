using Love;
using System.Collections.Generic;

namespace Suijin_cave.GameObjects
{
    public abstract class Gun : GameObject
    {
        public enum GunType
        {
            Pistol,
            Auto,
            Once
        }

        public GunType Type { get; protected set; }

        public List<Bullet> Bullets { get; private set; } = new List<Bullet>();
        public GameObject Parent { get; set; }
        protected Vector2 ParentOffset { get; set; } = new Vector2();

        public abstract void Update(float dt, Vector2 target);

        public abstract void Shoot(Vector2 direction);

        protected ParticleSystem particleSystem;

        protected abstract void InitParticleSystem();

        public int Ammmo { get; set; }

        protected bool flipped = false;
        protected virtual void FlipToTarget(Vector2 target)
        {
            // Flip the sprite according to target
            if (target.X > Parent.Position.X && flipped)
            {
                Scale = new Vector2(Scale.X, Scale.Y * -1);
                flipped = false;
            }
            else if (target.X < Parent.Position.X && !flipped)
            {
                Scale = new Vector2(Scale.X, Scale.Y * -1);
                flipped = true;
            }
        }

        protected virtual void RotateToTarget(Vector2 target)
        {
            Position = Parent.Position;
            var newPos = Vector2.Zero;
            newPos.X = Parent.Position.X + Mathf.Cos(Rotation) * ParentOffset.X;
            newPos.Y = Parent.Position.Y + Mathf.Sin(Rotation) * ParentOffset.Y;
            Position = newPos;
            Rotation = Mathf.Atan2(target.Y - Parent.Position.Y, target.X - Parent.Position.X);
        }

        protected virtual Vector2 GetBulletPosition(Vector2 direction, Vector2 bulletOffset)
        {
            var pos = Parent.Position;
            pos.X += Mathf.Cos(Rotation) * (bulletOffset.X + ParentOffset.X);
            pos.Y += Mathf.Sin(Rotation) * (bulletOffset.Y + ParentOffset.Y);
            return pos;
        }

        public virtual void UpdateBullets(float dt)
        {
            if (particleSystem != null)
            {
                particleSystem.Update(dt);
            }

            foreach (var b in Bullets.ToArray())
            {
                b.Update(dt);
                if (b.Dead)
                {
                    Bullets.Remove(b);
                }
            }
        }

        public virtual void DrawBulelts()
        {
            if (particleSystem != null)
            {
                Graphics.Draw(particleSystem);
            }

            foreach (var b in Bullets)
            {
                b.Draw();
            }
        }
    }
}