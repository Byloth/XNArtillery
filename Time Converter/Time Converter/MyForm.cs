using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Time_Converter
{
    public enum HowToConvert
    {
        Nothing,
        Milliseconds,
        Time,
        Degrees,
        Radians
    }

    public partial class MyForm : Form
    {
        private const int millisecondsMaxValue = 86400;
        private const int hoursMaxValue = 24;
        private const int basicMaxValue = 60;
        private const int degreesMaxValue = 360;
        private const float radiansMaxValue = (float)(2 * Math.PI);

        private HowToConvert howToConvert;

        public MyForm()
        {
            InitializeComponent();

            howToConvert = HowToConvert.Nothing;
        }

        private void milliseconds_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Milliseconds;

            if (milliseconds.Text == "")
            {
                milliseconds.Text = "0";
            }
        }

        private void hours_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Time;

            if (hours.Text== "")
            {
                hours.Text = "0";
            }
        }

        private void minutes_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Time;

            if (minutes.Text == "")
            {
                minutes.Text = "0";
            }
        }

        private void seconds_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Time;

            if (seconds.Text == "")
            {
                seconds.Text = "0";
            }
        }

        private void degrees_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Degrees;

            if (degrees.Text == "")
            {
                degrees.Text = "0";
            }
        }

        private void radians_TextChanged(object sender, EventArgs e)
        {
            howToConvert = HowToConvert.Radians;

            if (radians.Text == "")
            {
                radians.Text = "0";
            }

        }

        private void convert_Click(object sender, EventArgs e)
        {
            float millisecondsValue = float.Parse(milliseconds.Text);
            float hoursValue = float.Parse(hours.Text);
            float minutesValue = float.Parse(minutes.Text);
            float secondsValue = float.Parse(seconds.Text);
            float degreesValue = float.Parse(degrees.Text);
            float radiansValue = float.Parse(radians.Text);

            float decimalPart;

            switch (howToConvert)
            {
                case HowToConvert.Milliseconds:

                    hoursValue = (millisecondsValue + 1) / (millisecondsMaxValue / hoursMaxValue);
                    decimalPart = hoursValue % 1;
                    hoursValue -= decimalPart;
                    minutesValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = minutesValue % 1;
                    minutesValue -= decimalPart;
                    secondsValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = secondsValue % 1;
                    secondsValue -= decimalPart;

                    degreesValue = (degreesMaxValue * millisecondsValue) / millisecondsMaxValue;

                    radiansValue = (radiansMaxValue * degreesValue) / degreesMaxValue;

                    break;
                case HowToConvert.Time:

                    millisecondsValue = secondsValue;
                    millisecondsValue += minutesValue * basicMaxValue;
                    millisecondsValue += hoursValue * basicMaxValue * basicMaxValue;

                    degreesValue = (degreesMaxValue * millisecondsValue) / millisecondsMaxValue;

                    radiansValue = (radiansMaxValue * degreesValue) / degreesMaxValue;

                    break;
                case HowToConvert.Degrees:

                    millisecondsValue = (millisecondsMaxValue * degreesValue) / degreesMaxValue;

                    hoursValue = (millisecondsValue + 1) / (millisecondsMaxValue / hoursMaxValue);
                    decimalPart = hoursValue % 1;
                    hoursValue -= decimalPart;
                    minutesValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = minutesValue % 1;
                    minutesValue -= decimalPart;
                    secondsValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = secondsValue % 1;
                    secondsValue -= decimalPart;

                    radiansValue = (radiansMaxValue * degreesValue) / degreesMaxValue;

                    break;
                case HowToConvert.Radians:

                    degreesValue = (degreesMaxValue * radiansValue) / radiansMaxValue;

                    millisecondsValue = (millisecondsMaxValue * degreesValue) / degreesMaxValue;

                    hoursValue = (millisecondsValue + 1) / (millisecondsMaxValue / hoursMaxValue);
                    decimalPart = hoursValue % 1;
                    hoursValue -= decimalPart;
                    minutesValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = minutesValue % 1;
                    minutesValue -= decimalPart;
                    secondsValue = decimalPart / (1F / basicMaxValue);
                    decimalPart = secondsValue % 1;
                    secondsValue -= decimalPart;

                    break;
            }

            milliseconds.Text = "" + millisecondsValue + "";
            hours.Text = "" + hoursValue + "";
            minutes.Text = "" + minutesValue + "";
            seconds.Text = "" + secondsValue + "";
            degrees.Text = "" + degreesValue + "";
            radians.Text = "" + radiansValue + "";
        }
    }
}
