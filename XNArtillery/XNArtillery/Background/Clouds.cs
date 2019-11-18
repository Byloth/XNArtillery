using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ByloEngine;
using ByloEngine.Graphics;

using ByloDraw2D;
using ByloDraw2D.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery.Background
{
    class Clouds : ParticlesManager
    {
        private const int cloudsTypes = 2;
        private const int maxClouds = 10;
        private const string cloudsPathName = "Background/Cloud";
        private const string cloudsShaderPathName = "Effects/Clouds Shader";

        private Color borderColor;
        private Effect cloudsShader;

        private void initializeStars(Limits limits)
        {
            base.particles = new List<Particle>();

            for (int index = 0; index < maxClouds; index += 1)
            {
                base.particles.Add(new Cloud(cloudsTypes, limits));
            }
        }

        public Clouds(MyGame myGame)
            : base(myGame.ResolutionRatio, cloudsTypes)
        {
            initializeStars(new Limits(myGame.Resolution));
        }

        public void LoadContent(MyGame myGame)
        {
            base.loadContent(myGame.Content, cloudsPathName);

            cloudsShader = myGame.Content.Load<Effect>(cloudsShaderPathName);
        }

        public void Update(Sky sky, Sun sun)
        {
            borderColor = sun.Color;

            foreach (Cloud cloud in base.particles)
            {
                cloud.Color = sky.MiddleColor;

                cloud.Update(sky, sun);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Cloud cloud in particles)
            {
                spriteBatch.Draw(texture, cloud.Position, sources[0], cloud.Color, 0/**/, origin, scale * cloud.Scale, cloud.Effects, Particle.LayerDepth);
            }

            spriteBatch.End();

            foreach (Cloud cloud in particles)
            {
                cloudsShader.Parameters["acceptedAngle"].SetValue(cloud.Angle);
                cloudsShader.Parameters["sunColor"].SetValue(borderColor.ToVector4());

                spriteBatch.Begin(0, null, null, null, null, cloudsShader);
                spriteBatch.Draw(texture, cloud.Position, sources[1], Color.White, 0/**/, origin, scale * cloud.Scale, cloud.Effects, Particle.LayerDepth);
                spriteBatch.End();
            }

        }
    }
}
