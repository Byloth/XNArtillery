using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Engine;

using ByloEngine;

namespace XNArtillery.Effects
{
    class SmokeParticles : Particles
    {
        private const float generationProbability = 50;

        public SmokeParticles()
        {
        }

        public override void LoadContent()
        {
            base.loadContent("Smoke");
        }

        public override void Start()
        {
            base.isGenerating = true;
        }

        public override void Stop()
        {
            base.isGenerating = false;
        }

        public override void Update(Vector2 particlesPosition, Vector2 particlesDirection)
        {
            if (isGenerating == true)
            {
                int max = Randomize.Integer(1) + 1;
                float speed = (Mathematics.Hypotenuse(particlesDirection.X, particlesDirection.Y) * multiplier) + 1;

                max = (int)(max * speed);

                for (int i = 0; i < max; i += 1)
                {
                    if (Randomize.Boolean(generationProbability) == true)
                    {
                        particles.Add(new Particle(ParticlesTypes.Smoke, particlesPosition, particlesDirection, origin));
                    }
                }
            }
            base.update();
        }

        public override void Draw()
        {
            base.draw();
        }
    }
}
