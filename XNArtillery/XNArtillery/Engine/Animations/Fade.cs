using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNArtillery
{
    enum FadeStates
    {
        Stop,
        In,
        Out,
    }

    class Fade
    {
        private bool visible;
        private float unit;
        private float value;
        private float min;
        private float max;

        private FadeStates state;

        public Fade(bool startVisible)
        {
            visible = startVisible;
            min = 0;
            max = 1;

            state = FadeStates.Stop;
        }

        public void Start(FadeStates fadeType, bool setByTime, float fadeDuration)
        {
            state = fadeType;
            unit = 1 / fadeDuration;

            if (setByTime == true)
            {
                unit *= Global.increment;
            }

            if (fadeType == FadeStates.In)
            {
                if (value < 1)
                {
                    visible = true;
                    value = 0;
                    max = 1;
                    min = value;
                }
                else
                {
                    state = FadeStates.Stop;
                }
            }
            else if (fadeType == FadeStates.Out)
            {
                if (value > 0)
                {
                    visible = false;
                    value = 1;
                    min = 0;
                    max = value;
                }
                else
                {
                    state = FadeStates.Stop;
                }
            }
        }

        public void Start(FadeStates fadeType, bool setByTime, float startFadeValue, float fadeDuration, float endFadeValue)
        {
            state = fadeType;
            unit = 1 / fadeDuration;

            if (setByTime == true)
            {
                unit *= Global.increment;
            }

            if (fadeType == FadeStates.In)
            {
                visible = true;
                value = startFadeValue;
                max = endFadeValue;
            }
            else if (fadeType == FadeStates.Out)
            {
                visible = false;
                value = startFadeValue;
                min = endFadeValue;
            }
        }

        public float Update()
        {
            if (state == FadeStates.In)
            {
                value += unit;

                if (value >= max)
                {
                    value = max;
                    state = FadeStates.Stop;
                }

                return value;
            }
            else if (state == FadeStates.Out)
            {
                value -= unit;

                if (value <= min)
                {
                    value = min;
                    state = FadeStates.Stop;
                }

                return value;
            }
            else
            {
                if (visible == true)
                {
                    return max;
                }
                else
                {
                    return min;
                }
            }
        }
    }
}
