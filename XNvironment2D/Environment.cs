using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;
using ByloEngine.Input;

using ByloDraw2D.Lights;

using XNvironment2D.Core;

namespace XNvironment2D
{
    public class Environment
    {
        private TimedShader ambientalLightShader;

        private Sky sky;
        private Stars stars;
        private Moon moon;
        private Sun sun;
        private Clouds clouds;

        public Time Time
        {
            get;
            private set;
        }

        public Light[] Lights
        {
            get
            {
                return new Light[]
                {
                    new Light(ambientalLightShader.CurrentColor),
                    sun.Light,
                    moon.Light
                };
            }
        }

        private void initializeAmbientalLightShader()
        {
            ambientalLightShader = new TimedShader
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, Sky.NightColors[2]),
                    new TimedColor(Time.SunriseValue, Sky.SunriseColors[2]),
                    new TimedColor(Time.StartDayValue, new Color(191, 191, 191)),
                    new TimedColor(Time.EndDayValue, new Color(191, 191, 191)),
                    new TimedColor(Time.SunsetValue, Sky.SunsetColors[1]),
                    new TimedColor(Time.StartNightValue, Sky.NightColors[2])
                }
            );
        }

        public Environment(int skyLayers, GraphicsDeviceManager graphicsDeviceManager, float resolutionRatio)
        {
            Time = new Time();

            initializeAmbientalLightShader();

            sky = new Sky(graphicsDeviceManager);
            stars = new Stars(graphicsDeviceManager, resolutionRatio, Time);
            moon = new Moon(graphicsDeviceManager, resolutionRatio);
            sun = new Sun(graphicsDeviceManager, resolutionRatio);

            clouds = new Clouds(skyLayers, graphicsDeviceManager, resolutionRatio);
        }

        public void Load(ContentManager contentManager)
        {
            sky.Load(contentManager);
            stars.Load(contentManager);
            moon.Load(contentManager);
            sun.Load(contentManager);
            clouds.Load(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            Time.Update(gameTime);

            ambientalLightShader.Update(Time.Value);

            sky.Update(Time);
            stars.Update(Time);
            moon.Update(Time);
            sun.Update(Time);

            clouds.Update(Lights);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sky.Draw(spriteBatch);
            stars.Draw(spriteBatch);
            moon.Draw(spriteBatch);
            sun.Draw(spriteBatch);

            clouds.Draw(spriteBatch);
        }
    }
}
