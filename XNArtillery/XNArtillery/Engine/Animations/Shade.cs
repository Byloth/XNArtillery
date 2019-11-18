using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNArtillery
{
    class Shade
    {
        private double[] increments;

        private Color[] colors;
        private Color color;

        public Shade(Color[] shadedColors)
        {
            increments = new double[3];
            colors = shadedColors;
        }

        private void setColor(double elapsedTime)
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
            increments[0] = ((double)nextColor.R - currentColor.R) / 45;
            increments[1] = ((double)nextColor.G - currentColor.G) / 45;
            increments[2] = ((double)nextColor.B - currentColor.B) / 45;
        }

        public Color Update()
        {
            double elapsedTime = 0;

            if (Global.time > 315) // Inizio Alba
            {
                setColor(colors[3]);
                setIncrement(colors[3], colors[0]);
                elapsedTime = Global.time - 315;
            }
            else if (Global.time > 225) //Notte
            {
                setColor(colors[3]);
                setIncrement(colors[3], colors[3]);
                elapsedTime = 0;
            }
            else if (Global.time > 180) // Fine Tramonto
            {
                setColor(colors[2]);
                setIncrement(colors[2], colors[3]);
                elapsedTime = Global.time - 180;
            }
            else if (Global.time > 135)  // Inizio Tramonto
            {
                setColor(colors[1]);
                setIncrement(colors[1], colors[2]);
                elapsedTime = Global.time - 135;
            }
            else if (Global.time > 45) //Giorno
            {
                setColor(colors[1]);
                setIncrement(colors[1], colors[1]);
                elapsedTime = 0;
            }
            else if (Global.time > 0) // Fine Alba
            {
                setColor(colors[0]);
                setIncrement(colors[0], colors[1]);
                elapsedTime = Global.time;
            }

            setColor(elapsedTime);

            return color;
        }
    }
}
