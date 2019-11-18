using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    abstract class Player
    {
        private DynamicTexture2D tower;

        public Player()
        {
            tower = new DynamicTexture2D();
        }

        protected Point loadContent(MyGame runningGame, int towerNumber, Point playerPosition, Collider globalCollider)
        {
            Point dimension;

            dimension = tower.LoadContent(runningGame, "Towers/Tower" + towerNumber);
            tower.Position(new Point(playerPosition.X - (dimension.X / 2), playerPosition.Y - dimension.Y));
            globalCollider.SetCollision(tower.Texture2D(), Global.Size(tower.Position()));

            return dimension;
        }

        abstract public Point LoadContent(MyGame runningGame, int towerNumber, Point[] playersPositions, Collider globalCollider);

        public void Hit(Point collisionPosition)
        {
            tower.Hit(30, collisionPosition);
        }

        abstract public float[] Update(bool newShot, float windPower);

        protected void draw(SpriteBatch spriteBatch, Color light)
        {
            tower.Draw(spriteBatch, light);
        }

        abstract public void Draw(SpriteBatch spriteBatch, Color light);
    }
}
