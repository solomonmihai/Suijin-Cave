using Love;

namespace Suijin_cave.GameObjects
{
    public class M4A1 : Gun
    {
        private Vector2 initialParentOffset;

        public M4A1(GameObject parent)
        {
            Type = GunType.Auto;

            Parent = parent;
            Texture = Assets.M4A1;
            Scale = new Vector2(2);
            ParentOffset = new Vector2(50, 50);
            initialParentOffset = ParentOffset;
            Origin = new Vector2(Texture.GetWidth() / 2, Texture.GetHeight() / 2);

            Ammmo = 90;

            InitParticleSystem();
        }

        protected override void InitParticleSystem()
        {
            particleSystem = Graphics.NewParticleSystem(Help.CirclePrimitve, 32);
            particleSystem.SetParticleLifetime(0.1f, 0.2f);
            particleSystem.SetColors(new Vector4(1, 0.7f, 0, 1), new Vector4(1, 0.6f, 0, 1));
            particleSystem.SetSizes(0.6f, 0f);
            particleSystem.SetRadialAcceleration(1, 10);
            particleSystem.SetSpeed(30, 70);
            particleSystem.SetSpread(Mathf.PI * 2);
            particleSystem.SetLinearAcceleration(-10, 10, -10, 10);
        }

        private float shootingRate;
        private float _shootingRate = 0.12f;

        private static Vector2 bulletOffset = new Vector2(20, 20);

        public override void Shoot(Vector2 direction)
        {
            if (shootingRate <= 0 && Ammmo > 0)
            {
                var pos = GetBulletPosition(direction, bulletOffset);
                Bullets.Add(new Bullet(pos, direction));

                particleSystem.SetPosition(pos.X, pos.Y);
                particleSystem.Emit(10);

                // Recoil
                ParentOffset = initialParentOffset * 0.65f;
                shootingRate = _shootingRate;
                Ammmo--;
            }
        }

        public override void Update(float dt, Vector2 target)
        {
            if (shootingRate > 0)
            {
                shootingRate -= dt;
            }

            if (ParentOffset.X < initialParentOffset.X) ParentOffset += new Vector2(120 * dt, 0);
            if (ParentOffset.Y < initialParentOffset.Y) ParentOffset += new Vector2(0, 120 * dt);

            FlipToTarget(target);
            RotateToTarget(target);
        }
    }
}