using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Breaker
    {
        private Color[] colors;
        private Point dimension;

        public Breaker(Point textureDimension)
        {
            dimension = textureDimension;
            colors = new Color[textureDimension.X * textureDimension.Y];
        }

        public Breaker(Texture2D texture)
        {
            dimension = new Point(texture.Width, texture.Height);
            colors = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors);
        }

        public void InsertPixels(Texture2D texture, Point texturePosition, Color pixelsColor)
        {
            Color[] textureColors = new Color[texture.Width * texture.Height];

            texture.GetData<Color>(textureColors);

            for (int i = 0; i < (texture.Height - 1); i += 1)
            {
                for (int j = 0; j < texture.Width; j += 1)
                {
                    if (textureColors[(i * texture.Width) + j] != new Color())
                    {
                        colors[((i + texturePosition.Y) * dimension.X) + (j + texturePosition.X)] = pixelsColor;
                    }
                }
            }
        }

        public Color GetPixel(Point selectedPixel)
        {
            return colors[(selectedPixel.Y * dimension.X) + selectedPixel.X];
        }

        public Color[] GetColors()
        {
            return colors;
        }

        private void SetPixel(Point selectedPixel)
        {
            if ((selectedPixel.X >= 0) & (selectedPixel.X <= dimension.X) & (selectedPixel.Y >= 0) & (selectedPixel.Y <= dimension.Y))
            {
                try
                {
                    colors[(selectedPixel.Y * dimension.X) + selectedPixel.X] = new Color();
                }
                catch
                {
                }
            }
        }

        private void SetPixel(Point selectedPixel, Color collisionColor)
        {
            if ((selectedPixel.X >= 0) & (selectedPixel.X <= dimension.X) & (selectedPixel.Y >= 0) & (selectedPixel.Y <= dimension.Y))
            {
                int i = (selectedPixel.Y * dimension.X) + selectedPixel.X;

                if (colors[i] == collisionColor)
                {
                    colors[i] = new Color();
                }
            }
        }

        public Color[] Break(int damage, Point shotPosition)
        {
            for (float i = damage; i >= 0; i -= 1)
            {
                for (float j = 0; j <= 360; j += 1F)
                {
                    float radians = MathHelper.ToRadians(j);
                    int X = (int)(Math.Cos(radians) * i) + shotPosition.X;
                    int Y = (int)(Math.Sin(radians) * i) + shotPosition.Y;

                    SetPixel(new Point(X, Y));
                }
            }

            return colors;
        }

        public Color[] Break(int damage, Point shotPosition, Color collisionColor)
        {
            for (float i = damage; i >= 0; i -= 1)
            {
                for (float j = 0; j <= 360; j += 1F)
                {
                    float radians = MathHelper.ToRadians(j);
                    int X = (int)(Math.Cos(radians) * i) + shotPosition.X;
                    int Y = (int)(Math.Sin(radians) * i) + shotPosition.Y;

                    SetPixel(new Point(X, Y), collisionColor);
                }
            }

            return colors;
        }
    }
}
