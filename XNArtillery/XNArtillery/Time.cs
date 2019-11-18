using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ByloEngine;

namespace XNArtillery
{
    public class Time
    {
        public const int SunriseEnd = 0;
        public const int Day = 45;
        public const int SunsetStart = 135;
        public const int SunsetEnd = 180;
        public const int Night = 225;
        public const int SunriseStart = 315;
        public const float TimeIncrement = 0.025F;

        private float value;

        public Time()
        {
            int[] times = new int[]
            {
                SunriseEnd,
                Day,
                SunsetStart,
                SunsetEnd,
                Night,
                SunriseStart 
            };

            value = times[Randomize.Integer(times.Length)];
        }

        public float Value()
        {
            return value;
        }

        public float Update()
        {
            value = Mathematics.AddDegrees(value, TimeIncrement);

            return value;
        }
    }
}
