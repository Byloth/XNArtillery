using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNArtillery
{
    enum ZoomStates
    {
        Stop,
        In,
        Out,
    }

    class Zoom
    {
        private float unit;
        private float value;

        private ZoomStates state;

        public Zoom()
        {
            state = ZoomStates.Stop;
        }

        public void Start(float startingScaleValue, float zoomDuration, ZoomStates zoomType)
        {
            state = zoomType;
            unit = 1 / zoomDuration;
            value = startingScaleValue;

            if ((zoomType == ZoomStates.Out) & (startingScaleValue == 0))
            {
                value = Global.Resize(5);
            }
        }

        public float Update()
        {
            if (state == ZoomStates.In)
            {
                value += unit;

                if (value >= Global.Resize(5))
                {
                    value = Global.Resize(5);
                    state = ZoomStates.Stop;
                }

                return value;
            }
            else if (state == ZoomStates.Out)
            {
                value -= unit;

                if (value <= 0)
                {
                    value = 0;
                    state = ZoomStates.Stop;
                }

                return value;
            }

            return Global.Resize(5);
        }
    }
}
