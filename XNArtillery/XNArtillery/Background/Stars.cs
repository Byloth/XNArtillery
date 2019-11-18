using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;

using ByloDraw2D;
using ByloDraw2D.Core;

namespace XNArtillery.Background
{
    class Stars : ParticlesManager
    {
        private const int starsTypes = 2;
        private const int maxStars = 2500;
        private const string starsPathName = "Background/Stars";

        private bool areVisible;
        private float transparency;

        private TimedShader timedShader;
        private Vector2 center;

        private void initializeTimedShader()
        {
            timedShader = new TimedShader
            (
                   new TimedColor[]
                {
                    new TimedColor(16200000, Color.White),       //Night
                    new TimedColor(27000000, Color.Transparent),  //Day
                    new TimedColor(59400000, Color.Transparent),  //Day
                    new TimedColor(70200000, Color.White)        //Night
                }
            );
        }

        private Limits generateLimits(int width, int height)
        {
            float maxRange;

            center = new Vector2(width / 2, 0);
            maxRange = Maths.Hypotenuse(center.X, height);

            return new Limits(center.X - maxRange, 0, center.X + maxRange, maxRange);
        }

        private void initializeStars(Limits limits)
        {
            base.particles = new List<Particle>();

            for (int index = 0; index < maxStars; index += 1)
            {
                base.particles.Add(new Star(starsTypes, center, limits));
            }
        }

        public Stars(MyGame myGame)
            : base(myGame.ResolutionRatio, starsTypes)
        {
            Limits limits;

            initializeTimedShader();
            limits = generateLimits((int)myGame.Resolution.Width, (int)myGame.Resolution.Height);
            initializeStars(limits);
        }

        public void LoadContent(MyGame myGame)
        {
            base.loadContent(myGame.Content, starsPathName);
        }

        private void updateStars(Time time)
        {
            foreach (Star star in base.particles)
            {
                star.Update(time.RadiansValue);
            }
        }

        public void Update(MyGame myGame)
        {
            transparency = (float)timedShader.Update(myGame.Time.Value).A / byte.MaxValue;

            if (transparency <= 0)
            {
                areVisible = false;
            }
            else
            {
                areVisible = true;

                updateStars(myGame.Time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (areVisible == true)
            {
                Particle particle;
                Vector2 realPosition;

                spriteBatch.Begin();

                for (int index = 0; index < particles.Count; index += 1)
                {
                    particle = particles[index];
                    realPosition = new Vector2(particle.Position.X + center.X, particle.Position.Y + center.Y);

                    spriteBatch.Draw(texture, particle.Position, sources[particle.ID], particle.Color * transparency, particle.Rotation, origin, scale * particle.Scale, particle.Effects, Particle.LayerDepth);
                }

                spriteBatch.End();
            }
        }
    }
}
