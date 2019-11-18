using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;

namespace XNArtillery.Engine
{
    public class DynamicTexture2D : MyTexture2D
    {
        private const float precision = 1;

        protected Color[] colors;

        public DynamicTexture2D()
        {
        }

        protected override void loadTexture(string pathName)
        {
            Texture2D loadedTexture;

            loadedTexture = Global.ThisGame.Content.Load<Texture2D>("Images/" + pathName);
            colors = new Color[loadedTexture.Width * loadedTexture.Height];

            Texture = new Texture2D(Global.ThisGame.GraphicsDevice, loadedTexture.Width, loadedTexture.Height);

            loadedTexture.GetData<Color>(colors);
            Texture.SetData<Color>(colors);
        }

        protected Color pixel(int x, int y)
        {
            if ((x >= 0) & (y >= 0))
            {
                int index = (int)(y * Texture.Width) + x;

                if ((index >= 0) & (index < colors.Length))
                {
                    return colors[index];
                }
            }

            return Color.Transparent;
        }

        protected void pixel(int x, int y, Color newColor)
        {
            if ((x >= 0) & (y >= 0) & (x <= Texture.Width) & (y <= Texture.Height))
            {
                int index = (int)(y * Texture.Width) + x;

                if ((index >= 0) & (index < colors.Length))
                {
                    colors[index] = newColor;
                }
            }
        }

        public void UpdateTexture()
        {
            base.Texture.SetData<Color>(colors);
        }

        public void RemoveCircle(float radius, Vector2 position)
        {
            for (float i = radius; i >= 0; i -= precision)
            {
                for (float j = 0; j <= Mathematics.MaxDegrees; j += 1)
                {
                    float radians = MathHelper.ToRadians(j);
                    int x = (int)((Math.Cos(radians) * i) + position.X);
                    int y = (int)((Math.Sin(radians) * i) + position.Y);

                    pixel(x, y, new Color());
                }
            }

            UpdateTexture();
        }
    }
}
