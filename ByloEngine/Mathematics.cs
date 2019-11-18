using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Mathematics
    {
        public const int MaxDegrees = 360;

        public static float AddDegrees(float degrees, float increment)
        {
            degrees += increment;

            if (degrees >= MaxDegrees)
            {
                degrees -= MaxDegrees;
            }
            else if (degrees < 0)
            {
                degrees += MaxDegrees;
            }

            return degrees;
        }
    }
}
