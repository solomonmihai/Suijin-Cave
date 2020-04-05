using Love;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Suijin_cave
{
    public class Map
    {
        public List<Rectangle> Blocks { get; private set; }
        public Size Size { get; private set; }
        public int TileWidth => 32;
        private int TileSetGrid => 16;

        Image tileset;
        private SpriteBatch spriteBatch;

        public Map(string filename)
        {
            tileset = Assets.Tileset;
            var jsonString = System.IO.File.ReadAllText(filename);
            var json = JObject.Parse(jsonString);
            var map = json["layers"][0];
            Size = new Size((int)map["gridCellsX"], (int)(map["gridCellsY"]));
            var coords = map["data2D"];

            float tileScale = TileWidth / TileSetGrid;

            spriteBatch = Love.Graphics.NewSpriteBatch(tileset, Size.Width * Size.Height, SpriteBatchUsage.Static);

            spriteBatch.Clear();
            var blockQuad = Graphics.NewQuad(0, TileSetGrid, TileSetGrid, TileSetGrid, TileSetGrid * 4, TileSetGrid * 4);
            var grassQuads = new Quad[]
            {
                Graphics.NewQuad(0, 0, TileSetGrid, TileSetGrid, TileSetGrid * 4, TileSetGrid * 4),
                Graphics.NewQuad(TileSetGrid, 0, TileSetGrid, TileSetGrid, TileSetGrid * 4, TileSetGrid * 4),
                Graphics.NewQuad(TileSetGrid * 2, 0, TileSetGrid, TileSetGrid, TileSetGrid * 4, TileSetGrid * 4)
            };

            Blocks = new List<Rectangle>();

            for (int i = 0; i < Size.Width; i++)
            {
                for (int j = 0; j < Size.Height; j++)
                {
                    int coord = (int)coords[j][i];

                    switch (coord)
                    {
                        case 3:
                            spriteBatch.Add(blockQuad, i * TileWidth, j * TileWidth, 0, tileScale, tileScale);
                            Blocks.Add(new Rectangle(i * TileWidth, j * TileWidth, TileWidth, TileWidth));
                            break;

                        case 2:
                            var index = 2;
                            var rand = Mathf.Random(0, 100);
                            if (rand < 10)
                            {
                                index = 0;
                                if (rand < 5)
                                {
                                    index = 1;
                                }
                            }
                            spriteBatch.Add(grassQuads[index], i * TileWidth, j * TileWidth, 0, tileScale, tileScale);
                            break;
                    }
                }
            }
        }

        public void Draw()
        {
            Graphics.Draw(spriteBatch);
        }
    }
}