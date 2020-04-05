using Love;

using Suijin_cave.GameObjects;

namespace Suijin_cave
{
    public class GameScreen : Screen
    {
        public static Camera Camera { get; set; }
        public static Player Player { get; private set; }
        public static Map Map { get; private set; }

        public override void Load()
        {
            Map = new Map("Assets/map.json");
            Player = new Player();
            Camera = new Camera(Player.Position);
        }

        private void BoundCameraToPlayer()
        {
            if (Camera.Viewport.Right > Map.Size.Width * Map.TileWidth)
            {
                Camera.Position = new Vector2(Map.Size.Width * Map.TileWidth - Camera.Viewport.Width / 2, Camera.Position.Y);
            }
            if (Camera.Viewport.Left < 0)
            {
                Camera.Position = new Vector2(Camera.Viewport.Width / 2, Camera.Position.Y);
            }
            if (Camera.Viewport.Top < 0)
            {
                Camera.Position = new Vector2(Camera.Position.X, Camera.Viewport.Height / 2);
            }
            if (Camera.Viewport.Bottom > Map.Size.Height * Map.TileWidth)
            {
                Camera.Position = new Vector2(Camera.Position.X, Map.Size.Height * Map.TileWidth - Camera.Viewport.Height / 2);
            }
        }

        public override void Update(float dt)
        {
            Player.Update(dt);
            Camera.Position = Help.Lerp(Camera.Position, Player.Position, 0.1f);
            BoundCameraToPlayer();
        }

        public override void Draw()
        {
            Camera.Begin();
            Map.Draw();
            Player.Draw();
            Camera.End();
        }
    }
}