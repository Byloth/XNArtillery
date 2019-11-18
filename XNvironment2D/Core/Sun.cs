using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;

using ByloDraw2D;
using ByloDraw2D.Lights;

namespace XNvironment2D.Core
{
    class Sun : LayeredImage, ILightSource
    {
        private const int layerNumber = 2;
        private const float rotationRatio = 100;
        private const float synchronizationValue = 1.963495F; // = Time.ToRadians((7 * Time.OneHour) + (30 * Time.OneMinute));
        private const string sunPathName = "Environment/Sun";

        public static Color SunriseColor
        {
            get 
            {
                return new Color(255, 219, 128);
            }
        }
        public static Color DayColor
        {
            get 
            {
                return Color.White;
            }
        }
        public static Color SunsetColor
        {
            get 
            {
                return new Color(255, 179, 51);
            }
        }
        public static Color NightColor
        {
            get
            {
                return Sky.NightColors[2];
            }
        }

        private Vector2 fulcrum;
        private TimedShader sunShader;

        public Light Light
        {
            get
            {
                return new Light(base.position, base.color, sunShader.Transparency);
            }
        }

        private void initializeSunShader()
        {
            sunShader = new TimedShader
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, NightColor),
                    new TimedColor(Time.SunriseValue, SunriseColor),
                    new TimedColor(Time.StartDayValue, DayColor),
                    new TimedColor(Time.EndDayValue, DayColor),
                    new TimedColor(Time.SunsetValue, SunsetColor),
                    new TimedColor(Time.StartNightValue, NightColor)
                }
            );
        }

        public Sun(GraphicsDeviceManager graphicsDeviceManager, float resolutionRatio)
            : base(layerNumber)
        {
            base.scale = resolutionRatio;

            fulcrum = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, graphicsDeviceManager.PreferredBackBufferHeight);

            initializeSunShader();
        }

        public void Load(ContentManager contentManager)
        {
            base.loadTexture(contentManager, sunPathName);
        }

        public void Update(Time time)
        {
            float radians = time.Radians - synchronizationValue;
            float rotationSpeed = base.position.Y;

            base.position.X = (Maths.Cosine(radians) * fulcrum.X) + fulcrum.X;
            base.position.Y = (Maths.Sine(radians) * fulcrum.Y) + fulcrum.Y;

            rotationSpeed -= base.position.Y;

            base.color = sunShader.Update(time.Value);
            base.rotation += rotationSpeed / rotationRatio;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(base.texture, base.position, sources[0], base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.LayerDepth);
            spriteBatch.Draw(base.texture, base.position, sources[1], Color.White, 0, base.origin, base.scale, base.effects, Drawable.LayerDepth);
            spriteBatch.End();
        }
    }
}
