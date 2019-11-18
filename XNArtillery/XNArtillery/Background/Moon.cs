using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics.Effects.Core;

using ByloDraw2D;

namespace XNArtillery.Background
{
    class Moon : Image
    {
        private const string moonPathName = "Background/Moon";

        private Vector2 positionIncrement;

        public Moon(MyGame myGame)
            : base(myGame.ResolutionRatio)
        {
            positionIncrement = new Vector2(myGame.Resolution.Width / 2, myGame.Resolution.Height);
        }

        public void LoadContent(MyGame myGame)
        {
            base.LoadContent(myGame.Content, moonPathName);
        }

        public override void LoadContent(ContentManager contentManager, string pathName)
        {
            throw new NotImplementedException();
        }

        public void Update(MyGame myGame)
        {
            float radians = myGame.Time.RadiansValue;

            base.position.X = (Maths.Cosine(radians) * positionIncrement.X) + positionIncrement.X;
            base.position.Y = (Maths.Sine(radians) * positionIncrement.Y) + positionIncrement.Y;
        }
    }
}
