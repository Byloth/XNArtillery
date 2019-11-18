using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;

using ByloDraw2D.Core;

namespace XNArtillery.Background
{
    class Star : Particle
    {
        private const float minScale = 0.5F;
        private const float maxScale = 1;

        private static Color defaultColor
        {
            get
            {
                return new Color(191, 191, 191);
            }
        }

        private float defaultArgument;

        private Vector2 center;
        private Limits limits;

        public PolarCoords Coords
        {
            get
            {
                float coordX = base.Position.X - center.X;
                float coordY = base.Position.Y - center.Y;

                PolarCoords coords = new PolarCoords();

                coords.Module = Maths.Hypotenuse(coordX, coordY);
                coords.Argument = Maths.ArcTan(coordX, coordY);

                return coords;
            }

            set
            {
                base.Position.X = Maths.Cosine(value.Argument) * value.Module + center.X;
                base.Position.Y = Maths.Sine(value.Argument) * value.Module + center.Y;
            }
        }

        public Star(int starsTypes, Vector2 skyCenter, Limits generationLimits)
            : base(Randomize.Integer(starsTypes), Randomize.Vector2(generationLimits), defaultColor, Randomize.RadiansAngle().Value, Randomize.Decimal(minScale, maxScale), SpriteEffects.None)
        {
            center = skyCenter;
            limits = generationLimits;

            defaultArgument = Coords.Argument;
        }

        public void Update(float timeValue)
        {
            PolarCoords coords = Coords;

            Coords = new PolarCoords(coords.Module, defaultArgument + timeValue);

            if (limits.IsOutOfLimits(base.Position) == true)
            {
                defaultArgument += Maths.PI;
            }
        }
    }
}
