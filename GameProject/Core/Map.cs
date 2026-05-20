using System;

namespace GameProject.Core
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsInsideBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}