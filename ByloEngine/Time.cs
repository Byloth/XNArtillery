using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ByloEngine
{
    public class Time
    {
        private const float elapsedTimeWeight = 60;
        private const float hoursUnit = MaxValue / 24;
        private const float basicUnit = 1F / 60;
        private const float radiansUnit = (2 * Maths.PI) / MaxValue;

        public const int MinValue = 0;
        public const int OneSecond = 1000;
        public const int OneMinute = OneSecond * 60;
        public const int OneHour = OneMinute * 60;
        public const int MaxValue = 86400000;

        public const int SunriseValue = 27000000;
        public const int StartDayValue = 32400000;
        public const int EndDayValue = 64800000;
        public const int SunsetValue = 70200000;
        public const int StartNightValue = 75600000;
        public const int EndNightValue = 21600000;

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (value < MinValue)
                {
                    Value = value + MaxValue;
                }
                else if (value >= MaxValue)
                {
                    Value = value - MaxValue;
                }
                else
                {
                    LastIncrement = value - _value;

                    if (LastIncrement <= -(MaxValue / 2))
                    {
                        LastIncrement += MaxValue;
                    }
                    else if (LastIncrement >= (MaxValue / 2))
                    {
                        LastIncrement -= MaxValue;
                    }

                    _value = value;
                }
            }
        }
        public float Radians
        {
            get
            {
                float radians = Maths.Proportion(MaxValue, _value, Maths.MaxRadians) + Maths.PI;

                if (radians >= Maths.MaxRadians)
                {
                    radians -= Maths.MaxRadians;
                }

                return radians;
            }
        }

        public float LastIncrement
        {
            get;
            private set;
        }

        public static float ToRadians(float milliseconds)
        {
            return (milliseconds * radiansUnit);
        }

        public Time()
        {
            _value = 21600000;
        }

        public Time(float timeValue)
        {
            if ((timeValue >= MinValue) & (timeValue < MaxValue))
            {
                _value = timeValue;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(GameTime gameTime)
        {
            Value += (float)gameTime.ElapsedGameTime.TotalMilliseconds * elapsedTimeWeight;
        }

        public void RewindTime(float multiplierSpeed)
        {
            _value -= LastIncrement * multiplierSpeed;
        }

        public void SpeedUpTime(float multiplierSpeed)
        {
            _value += LastIncrement * multiplierSpeed;
        }

        public override string ToString()
        {
            float hours;
            float minutes;
            float seconds;
            float decimalPart;

            hours = (_value + 1) / hoursUnit;
            decimalPart = hours % 1;
            hours -= decimalPart;
            minutes = decimalPart / basicUnit;
            decimalPart = minutes % 1;
            minutes -= decimalPart;
            seconds = decimalPart / basicUnit;
            decimalPart = seconds % 1;
            seconds -= decimalPart;

            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
}
