using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ByloEngine;

namespace ByloEngine.Graphics
{
    public class TimedColor
    {
        private float _time;
        public float Time
        {
            get
            {
                return _time;
            }
            private set
            {
                if ((value >= 0) & (value < ByloEngine.Time.MaxValue))
                {
                    _time = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Color Color
        {
            get;
            private set;
        }

        public TimedColor(float time, Color color)
        {
            Time = time;
            Color = color;
        }
    }
}
