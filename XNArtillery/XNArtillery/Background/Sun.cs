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
using ByloDraw2D.Core;

namespace XNArtillery.Background
{
    class Sun : LayeredImage
    {
        private const int layerNumber = 2;
        private const float rotationRatio = 50;
        private const string sunPathName = "Background/Sun";

        private Vector2 positionIncrement;
        private TimedShader timedShader;

        public Vector2 Position
        {
            get
            {
                return base.position;
            }
        }

        public Color Color
        {
            get
            {
                return base.color;
            }
        }

        private void initializeTimedShader()
        {
            timedShader = new TimedShader
            (
                new TimedColor[]
                {
                    new TimedColor(16200000, Color.Transparent),     //Night
                    new TimedColor(21600000, new Color(255, 219, 128)),  //Sunrise
                    new TimedColor(27000000, Color.White),  //Day
                    new TimedColor(59400000, Color.White),  //Day
                    new TimedColor(64800000, new Color(255, 179, 51)),  //Sunset
                    new TimedColor(70200000, Color.Transparent)      //Night
                }
            );
        }

        public Sun(MyGame myGame)
            : base(myGame.ResolutionRatio, layerNumber)
        {
            positionIncrement = new Vector2(myGame.Resolution.Width / 2, myGame.Resolution.Height);

            initializeTimedShader();
        }

        public void LoadContent(MyGame myGame)
        {
            base.LoadContent(myGame.Content, sunPathName);
        }

        public override void LoadContent(ContentManager contentManager, string pathName)
        {
            throw new NotImplementedException();
        }

        public void Update(MyGame myGame)
        {
            float radians = myGame.Time.RadiansValue + Maths.PI;
            float verticalSpeed = base.position.Y;

            base.position.X = (Maths.Cosine(radians) * positionIncrement.X) + positionIncrement.X;
            base.position.Y = (Maths.Sine(radians) * positionIncrement.Y) + positionIncrement.Y;

            verticalSpeed -= base.position.Y;

            base.rotation += verticalSpeed / rotationRatio;

            base.color = timedShader.Update(myGame.Time.Value);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(base.texture, base.position, sources[0], base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.layerDepth);
            spriteBatch.Draw(base.texture, base.position, sources[1], Color.White, base.rotation, base.origin, base.scale, base.effects, Drawable.layerDepth);
            spriteBatch.End();
        }

        public override void Draw(SpriteBatch spriteBatch, Effect effect)
        {
            spriteBatch.Begin(0, null, null, null, null, effect);
            spriteBatch.Draw(base.texture, base.position, sources[0], base.color, base.rotation, base.origin, base.scale, base.effects, Drawable.layerDepth);
            spriteBatch.Draw(base.texture, base.position, sources[1], Color.White, base.rotation, base.origin, base.scale, base.effects, Drawable.layerDepth);
            spriteBatch.End();
        }
    }
}
