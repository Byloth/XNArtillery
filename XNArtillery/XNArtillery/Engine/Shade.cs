using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNArtillery.Engine
{
    class Shade
    {
        private float[] increments;

        private Color color;
        private Color[] colors;

        public Shade(Color sunriseColor, Color dayColor, Color sunsetColor, Color nightColor)
        {
            increments = new float[3];
            colors = new Color[]
            {
                sunriseColor,
                dayColor,
                sunsetColor,
                nightColor
            };
        }

        private void setColor(float elapsedTime)
        {
            color.R += (byte)(increments[0] * elapsedTime);
            color.G += (byte)(increments[1] * elapsedTime);
            color.B += (byte)(increments[2] * elapsedTime);
        }

        private void setColor(Color currentColor)
        {
            color = currentColor;
        }

        private void setIncrement(Color currentColor, Color nextColor)
        {
            increments[0] = ((float)nextColor.R - currentColor.R) / 45;
            increments[1] = ((float)nextColor.G - currentColor.G) / 45;
            increments[2] = ((float)nextColor.B - currentColor.B) / 45;
        }

        public Color Update()
        {
            return Update(Global.MyGameTime.Value());
        }

        public Color Update(float gameTime)
        {
            float elapsedTime = 0;

            if (gameTime > Time.SunriseStart)
            {
                setColor(colors[3]);
                setIncrement(colors[3], colors[0]);
                elapsedTime = gameTime - Time.SunriseStart;
            }
            else if (gameTime > Time.Night)
            {
                setColor(colors[3]);
                setIncrement(colors[3], colors[3]);
                elapsedTime = 0;
            }
            else if (gameTime > Time.SunsetEnd)
            {
                setColor(colors[2]);
                setIncrement(colors[2], colors[3]);
                elapsedTime = gameTime - Time.SunsetEnd;
            }
            else if (gameTime > Time.SunsetStart)
            {
                setColor(colors[1]);
                setIncrement(colors[1], colors[2]);
                elapsedTime = gameTime - Time.SunsetStart;
            }
            else if (gameTime > Time.Day)
            {
                setColor(colors[1]);
                setIncrement(colors[1], colors[1]);
                elapsedTime = 0;
            }
            else if (gameTime > Time.SunriseEnd)
            {
                setColor(colors[0]);
                setIncrement(colors[0], colors[1]);
                elapsedTime = gameTime;
            }

            setColor(elapsedTime);

            return color;
        }
    }
}
