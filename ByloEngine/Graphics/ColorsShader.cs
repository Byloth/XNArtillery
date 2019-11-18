using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloEngine.Graphics
{
    public class ColorsShaders
    {
        public const int MinShadingRatio = 0;
        public const int MaxShadingRatio = 1;

        public static Color AlphaBlendShader(Color startingColor, float shadingRatio, Color finalColor)
        {
            if ((shadingRatio >= MinShadingRatio) & (shadingRatio <= MaxShadingRatio))
            {
                float[] colorUnits = new float[]
                {
                    finalColor.R - startingColor.R,
                    finalColor.G - startingColor.G,
                    finalColor.B - startingColor.B,
                    finalColor.A - startingColor.A
                };

                Color color = new Color();

                color.R = (byte)(startingColor.R + (colorUnits[0] * shadingRatio));
                color.G = (byte)(startingColor.G + (colorUnits[1] * shadingRatio));
                color.B = (byte)(startingColor.B + (colorUnits[2] * shadingRatio));
                color.A = (byte)(startingColor.A + (colorUnits[3] * shadingRatio));

                return color * ((float)color.A / byte.MaxValue);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
