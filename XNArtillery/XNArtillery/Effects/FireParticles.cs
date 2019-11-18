using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Engine;

using ByloEngine;

namespace XNArtillery.Effects
{
    class FireParticles : Particles
    {
        private const float generation = 0.75F;
        private const float timeIncrement = 0.025F;
        private const float acceleration = 1F;

        private float time;

        private Vector2 position;
        private Vector2 startingDirection;
        private Vector2 direction;

        private SmokeParticles smoke;

        public FireParticles()
        {
            smoke = new SmokeParticles();
        }

        public override void LoadContent()
        {
            base.loadContent("Fire");

            smoke.LoadContent();
        }

        public void Start(Vector2 firePosition)
        {
            time = 0;

            position = firePosition;
            startingDirection = new Vector2(Randomize.Integer(-generation, generation), Randomize.Integer(-generation, generation));

            base.isGenerating = true;

            smoke.Start();
        }

        public override void Start()
        {
            base.isGenerating = true;

            smoke.Start();
        }

        public override void Stop()
        {
            base.isGenerating = false;

            smoke.Stop();
        }

        public void Update()
        {
            if (isGenerating == true)
            {
                int max = Randomize.Integer(25);
                float speed = (Mathematics.Hypotenuse(startingDirection.X, startingDirection.Y) * multiplier) + 1;

                max = (int)(max * speed);

                time += timeIncrement;

                direction.Y = Physics.linearAcceleration(time, startingDirection.Y, acceleration);

                position.X += startingDirection.X;
                position.Y += direction.Y;


                for (int i = 0; i < max; i += 1)
                {
                    particles.Add(new Particle(ParticlesTypes.Fire, position, startingDirection, origin));
                }
            }

            smoke.Update(position, startingDirection);

            base.update();
        }

        public override void Update(Vector2 particlesPosition, Vector2 particlesDirection)
        {
            if (isGenerating == true)
            {
                int max = Randomize.Integer(25);
                float speed = (Mathematics.Hypotenuse(particlesDirection.X, particlesDirection.Y) * multiplier) + 1;

                max = (int)(max * speed);

                for (int i = 0; i < max; i += 1)
                {
                    particles.Add(new Particle(ParticlesTypes.Fire, particlesPosition, particlesDirection, origin));
                }
            }

            smoke.Update(particlesPosition, particlesDirection);

            base.update();
        }

        public override void Draw()
        {
            smoke.Draw();

            base.draw();
        }
    }
}
