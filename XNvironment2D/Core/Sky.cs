using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;
using ByloEngine.Graphics.Effects;

namespace XNvironment2D.Core
{
    class Sky
    {
        public static Color[] SunriseColors = new Color[]
        {
            new Color(91, 130, 194),
            new Color(214, 193, 255),
            new Color(255, 183, 185)
        };

        public static Color[] DayColors = new Color[]
        {
            new Color(83, 153, 255),
            new Color(157, 197, 252),
            new Color(195, 219, 255)
        };

        public static Color[] SunsetColors = new Color[]
        {
            new Color(58, 125, 206),
            new Color(217, 150, 148),
            new Color(255, 102, 0)
        };

        public static Color[] NightColors = new Color[]
        {
            Color.Black,
            new Color(0, 9, 17),
            new Color(0, 13, 25)
        };

        private const int skyShades = 3;
        private const string skyShaderName = "Sky Shader";

        private Effect skyShader;
        private RenderTarget2D skyTexture;
        private TimedShader[] timedShaders;

        private void initializeTimedShaders()
        {
            timedShaders = new TimedShader[skyShades];

            timedShaders[0] = new TimedShader //Top
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, NightColors[0]),
                    new TimedColor(Time.SunriseValue, SunriseColors[0]),
                    new TimedColor(Time.StartDayValue, DayColors[0]),
                    new TimedColor(Time.EndDayValue, DayColors[0]),
                    new TimedColor(Time.SunsetValue, SunsetColors[0]),
                    new TimedColor(Time.StartNightValue, NightColors[0])
                }
            );

            timedShaders[1] = new TimedShader   //Middle
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, NightColors[1]),
                    new TimedColor(Time.SunriseValue, SunriseColors[1]),
                    new TimedColor(Time.StartDayValue, DayColors[1]),
                    new TimedColor(Time.EndDayValue, DayColors[1]),
                    new TimedColor(Time.SunsetValue, SunsetColors[1]),
                    new TimedColor(Time.StartNightValue, NightColors[1])
                }
            );

            timedShaders[2] = new TimedShader   //Bottom
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, NightColors[2]),
                    new TimedColor(Time.SunriseValue, SunriseColors[2]),
                    new TimedColor(Time.StartDayValue, DayColors[2]),
                    new TimedColor(Time.EndDayValue, DayColors[2]),
                    new TimedColor(Time.SunsetValue, SunsetColors[2]),
                    new TimedColor(Time.StartNightValue, NightColors[2])
                }
            );
        }

        public Sky(GraphicsDeviceManager graphicsDeviceManager)
        {
            skyTexture = new RenderTarget2D(graphicsDeviceManager.GraphicsDevice, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight);

            initializeTimedShaders();
        }

        public void Load(ContentManager contentManager)
        {
            skyShader = contentManager.Load<Effect>(Shader.BasePath + skyShaderName);
        }

        public void Update(Time time)
        {
            timedShaders[0].Update(time.Value);
            timedShaders[1].Update(time.Value);
            timedShaders[2].Update(time.Value);

            skyShader.Parameters["topColor"].SetValue(timedShaders[0].CurrentColor.ToVector4());
            skyShader.Parameters["middleColor"].SetValue(timedShaders[1].CurrentColor.ToVector4());
            skyShader.Parameters["bottomColor"].SetValue(timedShaders[2].CurrentColor.ToVector4());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(0, null, null, null, null, skyShader);
            spriteBatch.Draw(skyTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
