using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloEngine
{
    public class Limits
    {
        private float _lowerX;
        public float LowerX
        {
            get
            {
                return _lowerX;
            }
            set
            {
                if (value <= _upperX)
                {
                    _lowerX = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private float _lowerY;
        public float LowerY
        {
            get
            {
                return _lowerY;
            }
            set
            {
                if (value <= _upperY)
                {
                    _lowerY = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private float _upperX;
        public float UpperX
        {
            get
            {
                return _upperX;
            }
            set
            {
                if (value >= _lowerX)
                {
                    _upperX = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private float _upperY;
        public float UpperY
        {
            get
            {
                return _upperY;
            }
            set
            {
                if (value >= _lowerY)
                {
                    _upperY = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public Limits()
        {
            _lowerX = 0;
            _lowerY = 0;
            _upperX = 1;
            _upperY = 1;
        }

        public Limits(GraphicsDeviceManager graphicsDeviceManager)
        {
            _lowerX = 0;
            _lowerY = 0;
            _upperX = graphicsDeviceManager.PreferredBackBufferWidth;
            _upperY = graphicsDeviceManager.PreferredBackBufferHeight;
        }

        public Limits(Vector2 center, float radius)
        {
            _lowerX = center.X - radius;
            _lowerY = center.Y - radius;
            _upperX = center.X + radius;
            _upperY = center.Y + radius;
        }
    }
}
