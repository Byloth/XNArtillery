using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Level
    {
        private DynamicTexture2D level;

        public Level()
        {
            level = new DynamicTexture2D();
        }

        public Point[] LoadContent(MyGame runningGame, int levelNumber, Collider globalCollider)
        {
            Point dimension;
            Point[] positions;

            dimension = level.LoadContent(runningGame, "Levels/Level" + levelNumber);
            level.Position(new Point(0, Global.resolution.Y - dimension.Y));

            globalCollider.SetCollision(level.Texture2D(), Global.Size(level.Position()));

            positions = new Point[2]
            {
                new Point(Global.resolution.X / 8, Global.Horizon()),
                new Point((Global.resolution.X / 8) * 7, Global.Horizon())
            };

            return positions;
        }

        public void Hit(Point collisionPosition)
        {
            level.Hit(30, collisionPosition);
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            level.Draw(spriteBatch, light);
        }
    }
}
