using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D.Particles
{
    public abstract class ImageParticlesManager : ParticlesManager
    {
        protected const string basePath = "Images/";

        protected int types;

        protected Texture2D texture;
        protected Rectangle[] sources;

        public ImageParticlesManager(int particlesTypes)
        {
            if (particlesTypes > 0)
            {
                types = particlesTypes;

                sources = new Rectangle[particlesTypes];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        protected void loadTexture(ContentManager contentManager, string pathName)
        {
            int sourceWidth;

            texture = contentManager.Load<Texture2D>(basePath + pathName);
            sourceWidth = texture.Width / types;
            origin = new Vector2(sourceWidth / 2, texture.Height / 2);

            for (int index = 0; index < types; index += 1)
            {
                sources[index] = new Rectangle(sourceWidth * index, 0, sourceWidth, texture.Height);
            }
        }
    }
}
