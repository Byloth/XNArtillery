using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloDraw2D.Particles
{
    public abstract class ParticlesManager
    {
        protected float scale;

        protected Vector2 origin;
        protected List<Particle> particles;

        public ParticlesManager()
        {
            scale = 1;

            origin = Vector2.Zero;
            particles = new List<Particle>();
        }
    }
}
