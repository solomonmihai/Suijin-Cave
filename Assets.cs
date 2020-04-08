using Love;

namespace Suijin_cave
{
    public static class Assets
    {
        public static Image Tileset { get; private set; }
        public static Image Handgun { get; private set; }
        public static Image Bullet { get; private set; }
        public static Font LiberationMono_Regular_1 { get; set; }
        public static Font LiberationMono_Regular_2 { get; set; }
        public static Font LiberationMono_Regular_3 { get; set; }

        public static void Load()
        {
            Tileset = LoadImage("tileset.png");
            Handgun = LoadImage("handgun.png");
            Bullet = LoadImage("bullet.png");

            LiberationMono_Regular_1 = Graphics.NewFont("Assets/font/LiberationMono-Regular.ttf", 12);
            LiberationMono_Regular_2 = Graphics.NewFont("Assets/font/LiberationMono-Regular.ttf", 24);
            LiberationMono_Regular_3 = Graphics.NewFont("Assets/font/LiberationMono-Regular.ttf", 36);
        }

        private static Image LoadImage(string filename) => Graphics.NewImage("Assets/" + filename);
    }
}