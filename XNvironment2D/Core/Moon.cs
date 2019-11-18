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
    class Moon : Image, ILightSource
    {
        private const float synchronizationValue = 5.105088F; // = Time.ToRadians((19 * Time.OneHour) + (30 * Time.OneMinute));
        private const string moonPathName = "Environment/Moon";

        private Vector2 fulcrum;
        private TimedShader moonShader;

        public Light Light
        {
            get
            {
                return new Light(base.position, base.color, moonShader.Transparency);
            }
        }

        private void initializeMoonShader()
        {
            moonShader = new TimedShader
            (
                new TimedColor[]
                {
                    new TimedColor(Time.EndNightValue, Color.White),
                    new TimedColor(Time.StartDayValue, Color.Transparent),
                    new TimedColor(Time.EndDayValue, Color.Transparent),
                    new TimedColor(Time.StartNightValue, Color.White)
                }
            );
        }

        public Moon(GraphicsDeviceManager graphicsDeviceManager, float resolutionRatio)
        {
            base.scale = resolutionRatio;

            fulcrum = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, graphicsDeviceManager.PreferredBackBufferHeight);

            initializeMoonShader();
        }

        public void Load(ContentManager contentManager)
        {
            base.loadTexture(contentManager, moonPathName);
        }

        public void Update(Time time)
        {
            float radians = time.Radians - synchronizationValue;

            base.position.X = (Maths.Cosine(radians) * fulcrum.X) + fulcrum.X;
            base.position.Y = (Maths.Sine(radians) * fulcrum.Y) + fulcrum.Y;

            base.color = moonShader.Update(time.Value);
        }
    }
}
