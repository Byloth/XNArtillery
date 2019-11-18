using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D
{
    public abstract class String : Drawable
    {
        protected const string basePath = "Fonts/";

        protected string value;
        protected SpriteFont font;

        public String()
        {
            base.color = Color.Black;
        }

        protected virtual void loadFont(ContentManager contentManager, string pathName)
        {
            font = contentManager.Load<SpriteFont>(basePath + pathName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, value, base.position, base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.LayerDepth);
            spriteBatch.End();
        }
    }
}
