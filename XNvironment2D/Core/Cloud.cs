using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;

using ByloDraw2D.Lights;
using ByloDraw2D.Particles;

namespace XNvironment2D.Core
{
    class Cloud : Particle
    {
        private const float minScale = 0.5F;
        private const float maxScale = 1;
        private const float minRotation = -Maths.PI / 6;
        private const float maxRotation = Maths.PI / 6;
        private const float baseSpeed = 0.0125F;

        private bool leftToRight;
        private int layer;

        private float _lightAngle;
        public float LightAngle
        {
            get
            {
                return _lightAngle;
            }
            private set
            {
                if (value < Maths.MinRadians)
                {
                    LightAngle = value + Maths.MaxRadians;
                }
                else if (value >= Maths.MaxRadians)
                {
                    LightAngle = value - Maths.MaxRadians;
                }
                else
                {
                    _lightAngle = value;
                }
            }
        }

        private void initializeRotation()
        {
            base.Rotation = Randomize.Decimal(minRotation, maxRotation);

            if (Randomize.Boolean() == true)
            {
                base.Rotation += Maths.PI;
            }
        }

        public Cloud(int skyLayers, Limits generationLimits)
            : base(Randomize.Vector2(generationLimits), Color.White, 0, Randomize.Decimal(minScale, maxScale), Randomize.SpriteEffects())
        {
            initializeRotation();

            leftToRight = Randomize.Boolean();
            layer = Randomize.Integer(skyLayers) + 1;
        }

        private void updatePosition()
        {
            float increment = layer * baseSpeed;

            if (leftToRight == false)
            {
                increment = -increment;
            }

            base.Position.X += increment;
        }

        private void updateLightAngle(Light light)
        {
            float x = light.Position.X - base.Position.X;
            float y = light.Position.Y - base.Position.Y;

            float angle = Maths.ArcTan(x, y);

            angle -= base.Rotation;

            if (base.Effects == SpriteEffects.FlipHorizontally)
            {
                angle = Maths.PI - angle;
            }
            else if (base.Effects == SpriteEffects.FlipVertically)
            {
                angle = Maths.PI + (Maths.PI - angle);
            }

            LightAngle = angle;
        }

        public void Update(Light[] lights)
        {
            updatePosition();
            updateLightAngle(lights[1]);
        }
    }
}
