using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloDraw2D.Lights
{
    public class Light
    {
        private float _intensity;
        public float Intensity
        {
            get
            {
                return _intensity;
            }
            private set
            {
                if ((value >= 0) & (value <= 1))
                {
                    _intensity = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Vector2 Position;
        public Color Color;

        public Light(Color color)
        {
            Color = color;
        }

        public Light(Vector2 position, Color color, float intensity)
        {
            Position = position;
            Color = color;
            Intensity = intensity;
        }
    }
}
