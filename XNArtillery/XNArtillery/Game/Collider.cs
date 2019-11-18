using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Engine;
using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Game
{
    public class Collider : DynamicTexture2D
    {
        private const int maxCollision = 3;
        private const float transparency = 0.25F;
        private const float damagePower = 30;

        private bool show;
        private int index;

        private Color[] collisionsColors;

        public bool NewShot;

        public event CollisionHandler Collided;

        public Collider()
        {
            show = false;
            index = 0;

            collisionsColors = new Color[]
            {
                new Color(0, 255, 0),
                new Color(0, 0, 255),
                new Color(255, 0, 0)
            };

            NewShot = true;
        }

        public delegate void CollisionHandler(Collider sender, CollisionArgs e);

        protected override void loadTexture(string pathName)
        {
            Texture = new Texture2D(Global.ThisGame.GraphicsDevice, Global.MaxWidth, Global.MaxHeight);
            colors = new Color[Texture.Width * Texture.Height];

            UpdateTexture();
        }

        private Color pixel(int x, int y, Color[] otherTexture, Size otherTextureSize)
        {
            if ((x >= 0) & (y >= 0))
            {
                int index = (int)(y * otherTextureSize.Width) + x;

                if ((index >= 0) & (index < otherTexture.Length))
                {
                    return otherTexture[index];
                }
            }

            return new Color();
        }

        protected void addTexture(Texture2D collisionTexture, Vector2 texturePosition, Color collisionColor)
        {
            Color[] textureColors = new Color[collisionTexture.Width * collisionTexture.Height];
            Size collisionSize = new Size(collisionTexture.Width, collisionTexture.Height);

            collisionTexture.GetData<Color>(textureColors);

            for (int i = 0; i < (collisionTexture.Height - 1); i += 1)
            {
                for (int j = 0; j < collisionTexture.Width; j += 1)
                {
                    if (pixel(j, i, textureColors, collisionSize) != new Color())
                    {
                        pixel((int)(j + texturePosition.X), (int)(i + texturePosition.Y), collisionColor);
                    }
                }
            }
        }

        public void LoadCollision(Destroyable dynamicTexture)
        {
            addTexture(dynamicTexture.Texture(), dynamicTexture.TopLeftPixelPosition(), collisionsColors[index]);
            index += 1;

            if (index >= maxCollision)
            {
                base.UpdateTexture();
            }
        }

        private void generateCollision(Vector2 shotPosition)
        {
            NewShot = true;

            Collided(this, new CollisionArgs(damagePower, shotPosition));
        }

        public bool CheckCollision(Vector2 shotPosition)
        { 
            if (shotPosition.Y > Global.MyResolution.Height)
            {
                generateCollision(shotPosition);

                return true;
            }
            else if (((shotPosition.X >= 0) & (shotPosition.X <= Global.MyResolution.Width)) & ((shotPosition.Y >= 0)))
            {
                Color color;

                shotPosition = Functions.Size(shotPosition);
                color = pixel((int)shotPosition.X, (int)shotPosition.Y);

                if (color != new Color())
                {
                    for (int i = 0; (i < maxCollision); i += 1)
                    {
                        if (color == collisionsColors[i])
                        {
                            base.RemoveCircle(damagePower, shotPosition);

                            generateCollision(shotPosition);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void showCollision()
        {
            show = !show;
        }

        new public void Draw()
        {
            if (show == true)
            {
                base.Draw(Color.White * transparency);
            }
        }
    }
}
