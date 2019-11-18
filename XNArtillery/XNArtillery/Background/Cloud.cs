using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ByloEngine;

using ByloDraw2D.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery.Background
{
    class Cloud : Particle
    {
        private const float minScale = 0.5F;
        private const float maxScale = 1;
        private const float speed = 0.5F;

        private Radians _angle;
        public float Angle
        {
            get
            {
                return _angle.Value;
            }

            set
            {
                _angle.Value = value;
            }
        }

        public Cloud(int cloudsTypes, Limits generationLimits)
            : base(Randomize.Integer(cloudsTypes), Randomize.Vector2(generationLimits), Color.White, Randomize.RadiansAngle().Value, Randomize.Decimal(minScale, maxScale), (SpriteEffects)Randomize.Integer(3))
        {
            _angle = new Radians();
        }

        private void updateAngle(Vector2 sunPosition)
        {
            float x = sunPosition.X - base.Position.X;
            float y = sunPosition.Y - base.Position.Y;

            Angle = Maths.ArcTan(x, y);

            if (base.Effects == SpriteEffects.FlipHorizontally)
            {
                Angle = Maths.PI - Angle;
            }
            else if (base.Effects == SpriteEffects.FlipVertically)
            {
                Angle = Maths.PI + (Maths.PI - Angle);
            }
        }

        private void updatePosition()
        {
            switch (base.ID)
            {
                case 0:
                    base.Position.X += speed;
                    break;
                case 1:
                    base.Position.X -= speed;
                    break;
            }
        }

        public void Update(Sky sky, Sun sun)
        {
            base.Color = sky.MiddleColor;

            updateAngle(sun.Position);
            //updatePosition();
        }
    }
}
