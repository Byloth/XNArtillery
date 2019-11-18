using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Physics
    {
        public const float G = 9.80665F;

        public static float linearAcceleration(float time, float startingSpeed, float acceleration)
        {
            return startingSpeed + (acceleration * time);
        }

        public static float linearDeceleration(float time, float startingSpeed, float deceleration)
        {
            return linearAcceleration(time, startingSpeed, -deceleration);
        }

        public static float[] ParabolicTrajectory(float time, float shootingAngle, float shootingPower, float windPower)
        {
            float[] point = new float[2];

            point[0] = (float)((shootingPower * Math.Cos(shootingAngle) * time) - (windPower * (time * time)) / 2);
            point[1] = (float)((shootingPower * Math.Sin(shootingAngle) * time) - (G * (time * time)) / 2);

            return point;
        }
    }
}
