using Love;
using Suijin_cave.UI;

using System;

namespace Suijin_cave
{
    public class MainMenu : Screen
    {
        Button playButton;
        Button exitButton;

        string title = "Suijin_cave";

        int titleX;

        public override void Load()
        {
            playButton = new Button(new Vector2(Graphics.GetWidth() / 2, Graphics.GetHeight() - Graphics.GetHeight() / 4), "Play", () =>
            {
                Game.CurrentScreenName = typeof(GameScreen).Name;
            });

            exitButton = new Button(new Vector2(Graphics.GetWidth() / 2, playButton.Rectangle.Bottom + playButton.Rectangle.Height), "Exit", () =>
            {
                Event.Quit();
            });

            titleX = Graphics.GetWidth() / 2 - Assets.LiberationMono_Regular_3.GetWidth(title) / 2;
        }

        public override void Update(float dt)
        {
            playButton.Update();
            exitButton.Update();
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1, 1);
            Graphics.SetFont(Assets.LiberationMono_Regular_3);
            Graphics.Printf(title, titleX, Graphics.GetHeight() / 2, 1000, AlignMode.Left);

            playButton.Draw();
            exitButton.Draw();

            Graphics.SetFont();
        }
    }
}