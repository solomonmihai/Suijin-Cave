using Love;

using Suijin_cave.GameObjects;

namespace Suijin_cave
{
    public class GameScreen : Screen
    {
        public static Camera Camera { get; set; }
        public static Player Player { get; private set; }
        public static Map Map { get; set; }

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

        public override void MousePressed(float x, float y, int button, bool isTouch)
        {
            Player.MousePressed(x, y, button, isTouch);
        }

        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            Player.KeyPressed(key, scancode, isRepeat);
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

            Player.DrawUI();
        }
    }
}