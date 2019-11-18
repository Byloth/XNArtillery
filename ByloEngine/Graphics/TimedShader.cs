using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloEngine.Graphics
{
    public class TimedShader
    {
        private TimedColor[] colors;

        public float Transparency
        {
            get
            {
                return (float)CurrentColor.A / byte.MaxValue;
            }
        }

        public Color CurrentColor
        {
            get;
            private set;
        }

        public TimedShader(TimedColor[] timedColors)
        {
            if (timedColors.Length >= 2)
            {
                colors = timedColors;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private int startingColorIndex(float currentTime)
        {
            int index = -1;

            while ((index + 1) < colors.Length)
            {
                if (colors[index + 1].Time < currentTime)
                {
                    index += 1;
                }
                else
                {
                    if (index < 0)
                    {
                        return (colors.Length - 1);
                    }
                    else
                    {
                        return index;
                    }
                }
            }

            return index;
        }

        public Color Update(float currentTime)
        {
            bool endToBeginning = false;

            int firstIndex = startingColorIndex(currentTime);
            int secondIndex = firstIndex + 1;

            TimedColor firstTimedColor;
            TimedColor secondTimedColor;

            if (secondIndex >= colors.Length)
            {
                endToBeginning = true;
                secondIndex = 0;
            }

            firstTimedColor = colors[firstIndex];
            secondTimedColor = colors[secondIndex];

            if ((firstTimedColor.Color.Equals(secondTimedColor.Color) == true) | (firstTimedColor.Time == currentTime))
            {
                CurrentColor = firstTimedColor.Color;
            }
            else
            {
                float totalTimeSpan = secondTimedColor.Time - firstTimedColor.Time;
                float currentTimeSpan = currentTime - firstTimedColor.Time;

                float shadingRatio;

                if (endToBeginning == true)
                {
                    totalTimeSpan = Time.MaxValue + totalTimeSpan;

                    if (currentTimeSpan < 0)
                    {
                        currentTimeSpan = Time.MaxValue + currentTimeSpan;
                    }
                }

                shadingRatio = Maths.Proportion(totalTimeSpan, ColorsShaders.MaxShadingRatio, currentTimeSpan);
                CurrentColor = ColorsShaders.AlphaBlendShader(firstTimedColor.Color, shadingRatio, secondTimedColor.Color);
            }

            return CurrentColor;
        }
    }
}
