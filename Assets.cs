using Love;

namespace Suijin_cave
{
    public static class Assets
    {
        public static Image Tileset { get; private set; }
        public static void Load()
        {
            Tileset = Graphics.NewImage("Assets/tileset.png");
        }
    }
}