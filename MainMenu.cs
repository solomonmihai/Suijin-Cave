using Love;
using Suijin_cave.UI;

using System;

namespace Suijin_cave
{
    public class MainMenu : Screen
    {
        Button playButton;
        public override void Load()
        {
            playButton = new Button(new Vector2(Graphics.GetWidth() / 2, Graphics.GetHeight() - Graphics.GetHeight() / 4), "Play", () => {
                Game.CurrentScreenName = typeof(GameScreen).Name;
            });
        }

        public override void Update(float dt)
        {
            playButton.Update();
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1, 1);
            Graphics.SetFont(Assets.LiberationMono_Regular_3);
            Graphics.Printf("Suijin Cave", Love.Graphics.GetWidth() / 2, Love.Graphics.GetHeight() / 2, 0, AlignMode.Center);
            playButton.Draw();

            Graphics.Rectangle(DrawMode.Fill, 100, 100, 10, 100);
        }
    }
}