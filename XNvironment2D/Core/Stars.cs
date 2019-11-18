using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;

using ByloDraw2D;
using ByloDraw2D.Particles;

namespace XNvironment2D.Core
{
    class Stars : ImageParticlesManager
    {
        private const int starsTypes = 2;
        private const int maxStars = 5000;
        private const float fulcrumWidthRatio = 1F / 2;
        private const float fulcrumHeightRatio = 1F / 4;
        private const string starsPathName = "Environment/Stars";

        private Vector2 fulcrum;
        private Limits limits;
        private TimedShader starsShader;

        private void initializeGenerationParameters(GraphicsDeviceManager graphicsDeviceManager)
        {
            float radius;

            fulcrum = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth * fulcrumWidthRatio, graphicsDeviceManager.PreferredBackBufferHeight * fulcrumHeightRatio);
            radius = Maths.Hypotenuse(graphicsDeviceManager.PreferredBackBufferWidth - fulcrum.X, graphicsDeviceManager.PreferredBackBufferHeight - fulcrum.Y);

            limits = new Limits(fulcrum, radius);
            limits.LowerY = 0;
        }

        private void initializeStars(Time time)
        {
            for (int index = particles.Count; index < maxStars; index += 1)
            {
                particles.Add(new Star(starsTypes, time, fulcrum, limits));
            }
        }

        private void initializeStarsShader()
        {
            starsShader = new TimedShader
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, Color.White),
                    new TimedColor(Time.SunriseValue, Color.Transparent),
                    new TimedColor(Time.SunsetValue, Color.Transparent),
                    new TimedColor(Time.StartNightValue, Color.White)
                }
            );
        }

        public Stars(GraphicsDeviceManager graphicsDeviceManager, float resolutionRatio, Time time)
            : base(starsTypes)
        {
            base.scale = resolutionRatio;

            initializeGenerationParameters(graphicsDeviceManager);
            initializeStars(time);
            initializeStarsShader();
        }

        public void Load(ContentManager contentManager)
        {
            base.loadTexture(contentManager, starsPathName);
        }

        public void Update(Time time)
        {
            foreach (Star star in particles)
            {
                star.Update(time.Radians, fulcrum);
            }

            initializeStars(time);

            starsShader.Update(time.Value);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float transparency = starsShader.Transparency;

            spriteBatch.Begin();

            foreach (Star star in particles)
            {
                spriteBatch.Draw(base.texture, star.Position, base.sources[star.Type], star.Color * transparency, star.Rotation, base.origin, star.Scale * base.scale, star.Effects, Drawable.LayerDepth);
            }

            spriteBatch.End();
        }
    }
}
