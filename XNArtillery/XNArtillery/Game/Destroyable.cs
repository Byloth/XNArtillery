using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Engine;
using XNArtillery.Utility;

namespace XNArtillery.Game
{
    abstract public class Destroyable
    {
        protected DynamicTexture2D texture;

        public Destroyable()
        {
            texture = new DynamicTexture2D();
        }

        public Texture2D Texture()
        {
            return texture.Texture;
        }

        public Vector2 TopLeftPixelPosition()
        {
            return texture.TopLeftPixelPosition();
        }

        protected void loadCollision()
        {
            Global.MyCollider.LoadCollision(this);
            Global.MyCollider.Collided += new Collider.CollisionHandler(Collided);
        }

        protected void collided(float damagePower, Vector2 holePosition)
        {
            Vector2 position = texture.TopLeftPixelPosition();

            holePosition = Functions.Subtract(holePosition, position);

            texture.RemoveCircle(damagePower, holePosition);
        }

        abstract public void Collided(Collider sender, CollisionArgs e);

        public void Draw()
        {
            texture.Draw();
        }
    }
}
