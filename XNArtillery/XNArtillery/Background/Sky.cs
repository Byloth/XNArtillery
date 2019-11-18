using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine.Graphics;

namespace XNArtillery.Background
{
    class Sky
    {
        private const int skyShades = 3;

        private TimedShader[] timedShaders;
        private RenderTarget2D skyTexture;
        private Effect skyShader;

        public Color TopColor
        {
            get;
            private set;
        }
        public Color MiddleColor
        {
            get;
            private set;
        }
        public Color BottomColor
        {
            get;
            private set;
        }

        private void initializeTimedShaders()
        {
            timedShaders = new TimedShader[skyShades];

            timedShaders[0] = new TimedShader //Top
            (
                new TimedColor[]
                {
                    new TimedColor(16200000, new Color(0, 0, 0)),       //Night
                    new TimedColor(21600000, new Color(91, 130, 194)),  //Sunrise
                    new TimedColor(27000000, new Color(83, 153, 255)),  //Day
                    new TimedColor(59400000, new Color(83, 153, 255)),  //Day
                    new TimedColor(64800000, new Color(58, 125, 206)),  //Sunset
                    new TimedColor(70200000, new Color(0, 0, 0))        //Night
                }
            );

            timedShaders[1] = new TimedShader   //Middle
            (
                new TimedColor[]
                {
                    new TimedColor(16200000, new Color(0, 9, 17)),
                    new TimedColor(21600000, new Color(214, 193, 255)),
                    new TimedColor(27000000, new Color(157, 197, 252)),
                    new TimedColor(59400000, new Color(157, 197, 252)),
                    new TimedColor(64800000, new Color(217, 150, 148)),
                    new TimedColor(70200000, new Color(0, 9, 17))
                }
            );

            timedShaders[2] = new TimedShader   //Bottom
            (
                new TimedColor[]
                {
                    new TimedColor(16200000, new Color(0, 13, 25)),
                    new TimedColor(21600000, new Color(255, 183, 185)),
                    new TimedColor(27000000, new Color(195, 219, 255)),
                    new TimedColor(59400000, new Color(195, 219, 255)),
                    new TimedColor(64800000, new Color(255, 102, 0)),
                    new TimedColor(70200000, new Color(0, 13, 25))
                }
            );
        }

        public Sky(MyGame myGame)
        {
            skyTexture = new RenderTarget2D(myGame.GraphicsDevice, (int)myGame.Resolution.Width, (int)myGame.Resolution.Height);

            initializeTimedShaders();
        }

        public void LoadContent(MyGame myGame)
        {
            skyShader = myGame.Content.Load<Effect>("Effects/Sky Shader");
        }

        private void updateSkyShaderColors(int timeValue)
        {
            TopColor = timedShaders[0].Update(timeValue);
            MiddleColor = timedShaders[1].Update(timeValue);
            BottomColor = timedShaders[2].Update(timeValue);

            skyShader.Parameters["topColor"].SetValue(TopColor.ToVector4());
            skyShader.Parameters["middleColor"].SetValue(MiddleColor.ToVector4());
            skyShader.Parameters["bottomColor"].SetValue(BottomColor.ToVector4());
        }

        public void Update(MyGame myGame)
        {
            updateSkyShaderColors(myGame.Time.Value);
        }

        public void Draw(MyGame myGame)
        {
            myGame.SpriteBatch.Begin(0, null, null, null, null, skyShader);
            myGame.SpriteBatch.Draw(skyTexture, Vector2.Zero, Color.White);
            myGame.SpriteBatch.End();
        }
    }
}
