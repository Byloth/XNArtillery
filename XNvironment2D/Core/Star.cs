using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ByloEngine;

using ByloDraw2D.Particles;

namespace XNvironment2D.Core
{
    class Star : Particle
    {
        private const float minScale = 0.25F;
        private const float maxScale = 0.75F;
        private const float startShineProbability = 0.02F;
        private const float endShineProbability = 10F;

        private static Color defaultColor
        {
            get
            {
                return new Color(128, 128, 128);
            }
        }

        private bool isShining;

        public int Type;
        public float DefaultModule;

        private float _defaultArgument;
        public float DefaultArgument
        {
            get
            {
                return _defaultArgument;
            }
            set
            {
                if (value < Maths.MinRadians)
                {
                    DefaultArgument = value + Maths.MaxRadians;
                }
                else if (value >= Maths.MaxRadians)
                {
                    DefaultArgument = value - Maths.MaxRadians;
                }
                else
                {
                    _defaultArgument = value;
                }
            }
        }

        private void initializeDefaultAttributes(float timeRadians, Vector2 fulcrum)
        {
            float coordX = base.Position.X - fulcrum.X;
            float coordY = base.Position.Y - fulcrum.Y;

            DefaultModule = Maths.Hypotenuse(coordX, coordY);
            DefaultArgument = Maths.ArcTan(coordX, coordY) - timeRadians;
        }

        public Star(int starsTypes, Time time, Vector2 fulcrum, Limits generationLimits)
            : base(Randomize.Vector2(generationLimits), defaultColor, Randomize.RadiansAngle(), Randomize.Decimal(minScale, maxScale), Randomize.SpriteEffects())
        {
            isShining = false;

            Type = Randomize.Integer(starsTypes);

            initializeDefaultAttributes(time.Radians, fulcrum);
        }

        private void updateColor()
        {
            if (isShining == false)
            {
                if (Randomize.Boolean(startShineProbability) == true)
                {
                    base.Color = Color.White;

                    isShining = true;
                }
            }
            else
            {
                if (Randomize.Boolean(endShineProbability) == true)
                {
                    base.Color = defaultColor;

                    isShining = false;
                }
            }
        }
        private void updatePosition(float timeRadians, Vector2 fulcrum)
        {
            Vector2 newPosition = Vector2.Zero;

            timeRadians += DefaultArgument;

            newPosition.X = Maths.Cosine(timeRadians) * DefaultModule + fulcrum.X;
            newPosition.Y = Maths.Sine(timeRadians) * DefaultModule + fulcrum.Y;

            if (newPosition.Y < 0)
            {
                float argumentIncrement = ((3F / 2) * Maths.PI - timeRadians) * 2;

                DefaultArgument += argumentIncrement;
            }

            base.Position = newPosition;
        }

        public void Update(float timeRadians, Vector2 fulcrum)
        {
            updateColor();
            updatePosition(timeRadians, fulcrum);
        }
    }
}
