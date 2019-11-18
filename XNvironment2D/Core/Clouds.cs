using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics.Effects;

using ByloDraw2D;
using ByloDraw2D.Lights;
using ByloDraw2D.Particles;

namespace XNvironment2D.Core
{
    class Clouds : ImageParticlesManager
    {
        private const int cloudsLayers = 1;
        private const int maxClouds = 10;
        private const string cloudsPathName = "Environment/Cloud";
        private const string cloudsShaderPathName = Shader.BasePath + "Clouds Shader";

        private int layers;

        private Limits limits;
        private Effect cloudsShader;

        private void initializeClouds()
        {
            for (int index = particles.Count; index < maxClouds; index += 1)
            {
                particles.Add(new Cloud(layers, limits));
            }
        }

        public Clouds(int skyLayers, GraphicsDeviceManager graphicsDeviceManager, float resolutionRatio)
            : base(cloudsLayers)
        {
            base.scale = resolutionRatio;

            layers = skyLayers;
            limits = new Limits(graphicsDeviceManager);

            initializeClouds();
        }

        public void Load(ContentManager contentManager)
        {
            base.loadTexture(contentManager, cloudsPathName);

            cloudsShader = contentManager.Load<Effect>(cloudsShaderPathName);
        }

        public void Update(Light[] lights)
        {
            foreach (Cloud cloud in particles)
            {
                cloud.Update(lights);
            }

            cloudsShader.Parameters["ambientalLightColor"].SetValue(lights[0].Color.ToVector4());
            cloudsShader.Parameters["lightColor"].SetValue(lights[1].Color.ToVector4());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cloud cloud in base.particles)
            {
                cloudsShader.Parameters["lightAngle"].SetValue(cloud.LightAngle);

                spriteBatch.Begin(0, null, null, null, null, cloudsShader);
                spriteBatch.Draw(base.texture, cloud.Position, null, cloud.Color, cloud.Rotation, base.origin, cloud.Scale * base.scale, cloud.Effects, Drawable.LayerDepth);
                spriteBatch.End();
            }
        }
    }
}
