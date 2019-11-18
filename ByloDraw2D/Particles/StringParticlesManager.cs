using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D.Particles
{
    class StringParticlesManager : ParticlesManager
    {
        protected const string basePath = "Fonts/";

        protected SpriteFont font;

        public StringParticlesManager()
        {
        }

        protected void loadFont(ContentManager contentManager, string pathName)
        {
            font = contentManager.Load<SpriteFont>(basePath + pathName);
        }
    }
}
