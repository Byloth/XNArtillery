using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Randomize
    {
        private const int multiplier = 1000000;

        private static Random rndm = new Random();

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
            else
            {
                return false;
            }
        }

        public static int Integer(float maxValue)
        { 
            if (maxValue == 0)
            {
                return 0;
            }
            else if (maxValue < 0)
            {
                return -Integer(-maxValue);
            }
            else
            {
                return rndm.Next((int)maxValue);
            }
        }
        public static int Integer(float minValue, float maxValue)
        {
            if (minValue == maxValue)
            {
                return (int)minValue;
            }
            else if (minValue < 0)
            {
                if (maxValue < 0)
                {
                    return -Integer(-minValue, -maxValue);
                }
                else
                {
                    return (int)(Integer(maxValue - minValue) + minValue);
                }
            }
            else
            {
                minValue = (int)minValue;
                maxValue = (int)maxValue;

                if (minValue < maxValue)
                {
                    return rndm.Next((int)minValue, (int)maxValue);
                }
                else
                {
                    return rndm.Next((int)maxValue, (int)minValue);
                }
            }
        }
        public static int Integer(Limits randomizationLimits)
        {
            return Integer(randomizationLimits.Minimum, randomizationLimits.Maximum);
        }

        public static float Decimal(float maxValue)
        {
            if (maxValue == 0)
            {
                return (float)rndm.NextDouble();
            }
            else if (maxValue < 0)
            {
                return -Decimal(-maxValue);
            }
            else
            {
                return (float)Integer(maxValue * multiplier) / multiplier;
            }
        }
        public static float Decimal(float minValue, float maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }
            else
            {
                return (float)Integer(minValue * multiplier, maxValue * multiplier) / multiplier;
            }
        }
        public static float Decimal(Limits randomizationLimits)
        {
            return Decimal(randomizationLimits.Minimum, randomizationLimits.Maximum);
        }

        public static float Angle()
        {
            return Decimal(0, (float)(2 * Math.PI));
        }
    }
}
