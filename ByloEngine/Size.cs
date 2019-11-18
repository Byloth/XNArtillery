using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Size
    {
        public float Width;
        public float Height;

        public Size()
        {
            Width = 0;
            Height = 0;
        }

        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
