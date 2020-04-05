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
        private string currentScreen;
        public string CurrentScreen
        {
            get => currentScreen;
            set
            {
                if (currentScreen != null)
                {
                    Screens[currentScreen].OnExit();
                }

                currentScreen = value;

                Screens[currentScreen].OnEnter();
            }
        }

        public override void Load()
        {
            Graphics.SetDefaultFilter(FilterMode.Nearest, FilterMode.Nearest);

            Assets.Load();

            Screens = new Dictionary<string, Screen>()
            {
                { typeof(GameScreen).Name, new GameScreen() }
            };

            foreach (var screen in Screens)
            {
                screen.Value.Load();
            }

            CurrentScreen = typeof(GameScreen).Name;
        }

        public override void Update(float dt)
        {
            if (CurrentScreen != null)
            {
                Screens[CurrentScreen].Update(dt);
            }
        }

        public override void Draw()
        {
            if (CurrentScreen != null)
            {
                Screens[CurrentScreen].Draw();
            }
            Love.Graphics.Print(Love.Timer.GetFPS().ToString(), 5, 5);
        }
    }
}
