using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;

namespace ByloDraw2D
{
    public abstract class Drawable
    {
        public const int LayerDepth = 0;

        protected Vector2 position;
        protected Color color;

        private float _rotation;
        protected float rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                if (value < Maths.MinRadians)
                {
                    rotation = value + Maths.MaxRadians;
                }
                else if (value >= Maths.MaxRadians)
                {
                    rotation = value - Maths.MaxRadians;
                }
                else
                {
                    _rotation = value;
                }
            }
        }

        protected Vector2 origin;
        protected float scale;
        protected SpriteEffects effects;

        public Drawable()
        {
            position = Vector2.Zero;
            color = Color.White;
            rotation = 0;
            origin = Vector2.Zero;
            scale = 1;
            effects = SpriteEffects.None;
        }
    }
}
