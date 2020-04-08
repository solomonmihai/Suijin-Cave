using Love;
using System;

namespace Suijin_cave.UI
{
    public class Button
    {
        public Rectangle Rectangle { get; private set; }
        public Color Color { get; set; } = new Color(255, 255, 255, 255);
        public Color HoverColor { get; set; } = new Color(255, 255, 0, 255);

        public Action Action { get; set; }

        public Font Font { get; set; } = Assets.LiberationMono_Regular_3;

        public string Text { get; set; }

        public Button(Vector2 position, string text, Action action)
        {
            Text = text;
            var width = Font.GetWidth(text);
            var height = Font.GetHeight();
            Rectangle = Help.RectangleFromPosition(position, width, height);
            Action = action;
        }

        public bool Hover => Rectangle.Contains((int)Mouse.GetX(), (int)Mouse.GetY());

        bool lastPressed;

        public void Update()
        {
            if (!Mouse.IsDown(Mouse.LeftButton) && lastPressed && Hover)
            {
                Action();
            }
            lastPressed = Mouse.IsDown(Mouse.LeftButton);
        }

        public void Draw()
        {
            if (Hover)
            {
                Love.Graphics.SetColor(HoverColor);
            }
            else
            {
                Love.Graphics.SetColor(Color);
            }

            Graphics.SetFont(Font);
            Graphics.Rectangle(DrawMode.Line, Rectangle);
            Graphics.Printf(Text, Rectangle.Left, Rectangle.Top, 100, AlignMode.Left);
            Graphics.SetColor(Color.White);
        }
    }
}