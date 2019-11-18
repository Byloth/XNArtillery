using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ByloEngine;
using ByloEngine.Core;
using ByloEngine.Graphics;
using ByloEngine.Input;

using ByloDraw2D;

namespace XNArtillery
{
    public class Time : Debuggable
    {
        private const float elapsedTimeWeight = 60;
        private const float multiplierTimeSpeed = 100;
        private const string timeText = "Time: ";
        private const string fontName = "Default";

        private static Keys debugKey
        {
            get
            {
                return Keys.F2;
            }
        }     //F2
        private static Keys rewindKey
        {
            get
            {
                return Keys.F3;
            }
        }    //F3
        private static Keys forwardKey
        {
            get
            {
                return Keys.F4;
            }
        }   //F4

        private Text debugText;

        public int LastIncrement
        {
            get;
            private set;
        }

        public float LastRadiansIncrement
        {
            get
            {
                return Maths.Proportion(Day.MaxValue, Radians.MaxValue, LastIncrement);
            }
        }

        private Day _day;
        public int Value
        {
            get
            {
                return _day.Milliseconds;
            }

            set
            {
                LastIncrement = _day.Milliseconds - value;
                _day.Milliseconds = value;
            }
        }

        public float RadiansValue
        {
            get
            {
                return -Maths.Proportion(Day.MaxValue, Radians.MaxValue, _day.Milliseconds + (Day.MaxValue / 4));
            }
        }

        public Time(MyGame myGame)
            : base(debugKey, myGame.KeyboardManager)
        {
            myGame.KeyboardManager.KeyDown += new KeyEventHandler(timeRewinding);
            myGame.KeyboardManager.KeyDown += new KeyEventHandler(timeForwarding);

            LastIncrement = 0;

            debugText = new Text(myGame.ResolutionRatio);
            debugText.TextColor = Color.Fuchsia;

            _day = Randomize.DayTime();
        }

        private void timeRewinding(Keys eventKey)
        {
            if (eventKey == rewindKey)
            {
                Value += (int)(LastIncrement * (multiplierTimeSpeed - 1));
            }
        }

        private void timeForwarding(Keys eventKey)
        {
            if (eventKey == forwardKey)
            {
                Value -= (int)(LastIncrement * (multiplierTimeSpeed));
            }
        }

        public void LoadContent(MyGame myGame)
        {
            debugText.LoadContent(myGame.Content, fontName);
        }

        public void Update(GameTime gameTime)
        {
            Value += (int)(gameTime.ElapsedGameTime.TotalMilliseconds * elapsedTimeWeight);

            if (base.IsDebugging == true)
            {
                debugText.Value = timeText + _day.ToString();
            }
        }

        public void Draw(MyGame myGame)
        {
            if (base.IsDebugging == true)
            {
                debugText.Draw(myGame.SpriteBatch);
            }
        }
    }
}
