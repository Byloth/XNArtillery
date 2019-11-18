using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D.Particles
{
    public class Particle
    {
        public Vector2 Position;
        public Color Color;
        public float Rotation;
        public float Scale;
        public SpriteEffects Effects;

        public Particle()
        {
            Position = Vector2.Zero;
            Color = Color.White;
            Rotation = 0;
            Scale = 1;
            Effects = SpriteEffects.None;
        }

        public Particle(Vector2 position, Color color, float rotation, float scale, SpriteEffects effects)
        {
            Position = position;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            Effects = effects;
        }
    }
}
