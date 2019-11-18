using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNArtillery
{
    enum MovementStates
    {
        Stop,
        In,
        Out,
    }

    class Movement
    {
        private bool smooth;
        private float unit;
        private float duration;
        private float value;
        private float maxSmooth;
        private float max;

        private MovementStates state;

        public Movement()
        {
            state = MovementStates.Stop;
        }

        public void Start(bool smoothFall, int startingPosition, int endingPosition, float movementDuration)
        {
            float distance = endingPosition - startingPosition;

            smooth = smoothFall;
            unit = distance / movementDuration;
            maxSmooth = startingPosition + ((distance / 2)/* * 3*/);
            duration = movementDuration;

            if (startingPosition < endingPosition)
            {
                state = MovementStates.In;
            }
            else
            {
                state = MovementStates.Out;
            }

            value = startingPosition;
            max = endingPosition;
        }

        public int Update(int objectPosition)
        {
            if (state != MovementStates.Stop)
            {
                value += unit;

                if (state == MovementStates.In)
                {
                    if ((smooth == true) & (value >= maxSmooth))
                    {
                        unit = (max - value) / (duration / 2);
                    }
                    if (value >= max)
                    {
                        value = max;
                        state = MovementStates.Stop;
                    }
                }
                else
                {
                    if ((smooth == true) & (value <= maxSmooth))
                    {
                        unit = (max - value) / (duration / 2);
                    }
                    if (value <= max)
                    {
                        value = max;
                        state = MovementStates.Stop;
                    }
                }

                return (int)value;
            }

            return objectPosition;
        }
    }
}
