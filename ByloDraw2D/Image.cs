using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D
{
    public abstract class Image : Drawable
    {
        protected const string basePath = "Images/";

        protected Texture2D texture;

        public Image()
        {
        }

        protected virtual void loadTexture(ContentManager contentManager, string pathName)
        {
            texture = contentManager.Load<Texture2D>(basePath + pathName);

            base.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, base.position, null, base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.LayerDepth);
            spriteBatch.End();
        }
    }
}
