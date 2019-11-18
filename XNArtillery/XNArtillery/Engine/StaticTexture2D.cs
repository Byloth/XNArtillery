using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Engine
{
    class StaticTexture2D : MyTexture2D
    {
        public StaticTexture2D()
        {
        }

        protected override void loadTexture(string pathName)
        {
            Texture = Global.ThisGame.Content.Load<Texture2D>("Images/" + pathName);
        }
    }
}