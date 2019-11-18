using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine.Graphics
{
    public enum FadeTypes
    {
        Null,
        FadeIn,
        FadeOut
    }

    public class Fade
    {
        public const int Invisible = 0;
        public const int Visible = 1;

        private FadeTypes state;

        private float increment;
        private float value;

        public Fade(float startFadeValue)
        {
            state = FadeTypes.Null;
            value = startFadeValue;
        }

        public void Start(FadeTypes fadeType, float fadeSpan)
        {
            state = fadeType;
            increment = 1 / fadeSpan;
        }

        public void Start(FadeTypes fadeType, float startingValue, float fadeSpan)
        {
            state = fadeType;
            increment = 1 / fadeSpan;
            value = startingValue;
        }

        public float Update()
        {
            if (state != FadeTypes.Null)
            {
                if (state == FadeTypes.FadeIn)
                {
                    value += increment;

                    if (value > Visible)
                    {
                        value = Visible;
                        state = FadeTypes.Null;
                    }
                }
                else
                {
                    value -= increment;

                    if (value < Invisible)
                    {
                        value = Invisible;
                        state = FadeTypes.Null;
                    }
                }
            }

            return value;
        }
    }
}
