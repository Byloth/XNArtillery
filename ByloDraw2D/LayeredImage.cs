using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D
{
    public abstract class LayeredImage : Image
    {
        protected int layers;

        protected Rectangle[] sources;

        public LayeredImage(int layersNumber)
        {
            if (layersNumber > 0)
            {
                layers = layersNumber;

                sources = new Rectangle[layersNumber];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        protected override void loadTexture(ContentManager contentManager, string pathName)
        {
            int sourceWidth;

            base.loadTexture(contentManager, pathName);
            base.origin.X /= layers;

            sourceWidth = texture.Width / layers;

            for (int index = 0; index < layers; index += 1)
            {
                sources[index] = new Rectangle(sourceWidth * index, 0, sourceWidth, texture.Height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Rectangle source in sources)
            {
                spriteBatch.Draw(base.texture, base.position, source, base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.LayerDepth);
            }

            spriteBatch.End();
        }
    }
}
