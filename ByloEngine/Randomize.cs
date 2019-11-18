using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ByloEngine
{
    public sealed class Randomize
    {
        private const int spriteEffectsTypes = 3;

        private static Random random = new Random();

        public static bool Boolean()
        {
            return Boolean(50);
        }

        public static bool Boolean(float trueProbability)
        {
            if (Integer(100 / trueProbability) == 0)
            {
                return true;
            }

            return false;
        }

        public static int Integer(float maxValue)
        {
            int value = (int)maxValue;

            if (value == 0)
            {
                return 0;
            }
            else if (value > 0)
            {
                return random.Next(value);
            }
            else
            {
                return -random.Next(-value);
            }
        }

        public static int Integer(float minValue, float maxValue)
        {
            maxValue -= minValue;

            return (int)(Integer(maxValue) + minValue);
        }

        public static float Decimal(float maxValue)
        {
            maxValue *= Maths.FloatPrecision;

            return (float)Integer(maxValue) / Maths.FloatPrecision;
        }

        public static float Decimal(float minValue, float maxValue)
        {
            maxValue -= minValue;

            return Decimal(maxValue) + minValue;
        }

        public static float RadiansAngle()
        {
            return Decimal(Maths.MaxRadians);
        }

        public static float DegreesAngle()
        {
            return Decimal(Maths.MaxDegrees);
        }

        public static Vector2 Vector2(float maxX, float maxY)
        {
            return new Vector2(Decimal(maxX), Decimal(maxY));
        }

        public static Vector2 Vector2(Limits randomizationRange)
        {
            return new Vector2(Decimal(randomizationRange.LowerX, randomizationRange.UpperX), Decimal(randomizationRange.LowerY, randomizationRange.UpperY));
        }

        public static SpriteEffects SpriteEffects()
        {
            return (SpriteEffects)Integer(spriteEffectsTypes);
        }
    }
}
