using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Collider
    {
        private bool show;
        private int count;

        private Texture2D collisions;
        private Rectangle position;
        private Color[] standardColors;
        private Breaker breaker;

        public Collider()
        {
            count = 0;

            standardColors = new Color[3]
            {
                new Color(0, 255, 0, 255),
                new Color(0, 0, 255, 255),
                new Color(255, 0, 0, 255)
            };

            breaker = new Breaker(Global.MAX);
        }

        public void LoadContent(MyGame runningGame, bool showCollisions)
        {
            show = showCollisions;

            if (showCollisions == true)
            {
                collisions = new Texture2D(runningGame.GraphicsDevice, Global.MAX.X, Global.MAX.Y);
                position = new Rectangle(0, 0, Global.ResizeByX(collisions.Width), Global.ResizeByY(collisions.Height));
            }
        }

        public void SetCollision(Texture2D texture, Point texturePosition)
        {
            breaker.InsertPixels(texture, texturePosition, standardColors[count]);

            if (show == true)
            {
                collisions.SetData<Color>(breaker.GetColors());
            }

            count += 1;
        }

        private void Hit(Point shotPosition, Color collisionColor)
        {
            breaker.Break(30, shotPosition, collisionColor);
        }

        public ShotStates CheckCollision(Point shotPosition)
        {
            if (shotPosition.Y > Global.resolution.Y)
            {
                return ShotStates.CollidedWithLevel;
            }
            else if ((shotPosition.X >= 0) & (shotPosition.Y >= 0) & (shotPosition.X <= Global.resolution.X))
            {
                Color pixel = breaker.GetPixel(Global.Size(new Point(shotPosition.X, shotPosition.Y)));

                if (pixel != new Color())
                {
                    for (int i = 0; i < 3; i += 1)
                    {
                        if (pixel == standardColors[i])
                        {
                            Hit(Global.Size(new Point(shotPosition.X, shotPosition.Y)), standardColors[i]);

                            return (ShotStates)i;
                        }
                    }
                }
            }

            return ShotStates.Fired;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (show == true)
            {
                spriteBatch.Draw(collisions, position, Color.White * 0.5F);
            }
        }
    }
}
