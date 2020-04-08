using System;
using System.Collections.Generic;

using Love;

namespace Suijin_cave
{
    public class Game : Scene
    {
        static void Main(string[] args)
        {
            Boot.Init(new BootConfig()
            {
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowTitle = "Suijin Cave",
                WindowVsync = true
            });

            Boot.Run(new Game());
        }

        private static Dictionary<string, Screen> Screens { get; set; }
        private static string currentScreenName;
        public static string CurrentScreenName
        {
            get => currentScreenName;
            set
            {
                if (currentScreenName != null)
                {
                    Screens[currentScreenName].OnExit();
                }

                currentScreenName = value;

                Screens[currentScreenName].OnEnter();
            }
        }

        public override void Load()
        {
            Graphics.SetDefaultFilter(FilterMode.Nearest, FilterMode.Nearest);

            Assets.Load();

            Screens = new Dictionary<string, Screen>()
            {
                { typeof(GameScreen).Name, new GameScreen() },
                { typeof(MainMenu).Name, new MainMenu() }
            };

            foreach (var screen in Screens)
            {
                screen.Value.Load();
            }

            CurrentScreenName = typeof(MainMenu).Name;

            Graphics.SetFont(Assets.LiberationMono_Regular_1);
        }

        public override void Update(float dt)
        {
            if (CurrentScreenName != null)
            {
                Screens[CurrentScreenName].Update(dt);
            }
        }

        public override void Draw()
        {
            if (CurrentScreenName != null)
            {
                Screens[CurrentScreenName].Draw();
            }
            Love.Graphics.Print(Love.Timer.GetFPS().ToString(), 5, 5);
        }

        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            Screens[CurrentScreenName].KeyPressed(key, scancode, isRepeat);
            if (key == KeyConstant.L)
            {
                GameScreen.Map = new Map("Assets/map.json");
            }
        }

        public override void MousePressed(float x, float y, int button, bool isTouch)
        {
            Screens[CurrentScreenName].MousePressed(x, y, button, isTouch);
        }
    }
}
