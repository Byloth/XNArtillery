using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;

namespace XNArtillery.Background
{
    class Star
    {
        private const float startShineProbability = 0.05F;
        private const float endShineProbability = 25;
        private const float minScale = 0.5F;
        private const float maxScale = 1;

        private bool isShining;
        private float scale;
        private float distance;
        private float rotation;
        private float increment;

        private Vector2 position;
        private Color brightness;

        public Star(float maxDistance)
        {
            isShining = false;
            scale = Randomize.Decimal(minScale, maxScale) * Global.MyScale;
            distance = Randomize.Decimal(maxDistance);
            rotation = Randomize.Decimal(Mathematics.MaxDegrees);
            increment = Randomize.Decimal(Mathematics.MaxDegrees);

            position = new Vector2();
        }

        private void checkPosition(float degrees)
        {
            if (position.X > Global.MyResolution.Width)
            {
                increment = Mathematics.AddDegrees(increment, Mathematics.MaxDegrees / 2);
            }
            if (position.Y > Global.MyResolution.Height)
            {
                increment = Mathematics.AddDegrees(increment, Mathematics.MaxDegrees / 2);
            }
        }

        private void setPosition(float degrees)
        {
            float radians;

            checkPosition(degrees);

            radians = MathHelper.ToRadians(degrees + increment);

            position.X = (int)(Math.Cos(radians) * distance) + (Global.MyResolution.Width / 2);
            position.Y = (int)(Math.Sin(radians) * distance) + (Global.MyResolution.Height);

        }

        private void setBrightness()
        {
            if (isShining == false)
            {
                if (Randomize.Boolean(startShineProbability) == true)
                {
                    isShining = true;

                    brightness = Color.White;
                }
                else
                {
                    brightness = Color.Gray;
                }
            }
            else
            {
                if (Randomize.Boolean(endShineProbability) == true)
                {
                    isShining = false;
                }
            }
        }

        public void Update(float currentGameTime)
        {
            setPosition(currentGameTime);
            setBrightness();
        }

        public void Draw(Texture2D texture, float alpha)
        {
            Global.MySpriteBatch.Draw(texture, position, null, brightness * alpha, MathHelper.ToRadians(rotation), new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
        }
    }
}
