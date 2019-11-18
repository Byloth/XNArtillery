using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine;
using ByloEngine.Input;

using ByloDraw2D.ReadyToUse;

namespace XNArtillery
{
    public class Clock : Debuggable
    {
        private const float multiplierTimeSpeed = 60;
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

        private Time time;
        private Text debugText;

        public Clock(Time time, KeyboardManager keyboardManager, float resolutionRatio)
            : base(keyboardManager, debugKey)
        {
            keyboardManager.KeyDown += new KeyEventHandler(timeRewinding);
            keyboardManager.KeyDown += new KeyEventHandler(timeForwarding);

            this.time = time;

            debugText = new Text();
            debugText.Color = Color.Fuchsia;
            debugText.Scale = resolutionRatio;
        }

        private void timeRewinding(Keys eventKey)
        {
            if ((base.IsDebugging == true) & (eventKey == rewindKey))
            {
                time.RewindTime(multiplierTimeSpeed);
            }
        }

        private void timeForwarding(Keys eventKey)
        {
            if ((base.IsDebugging == true) & (eventKey == forwardKey))
            {
                time.SpeedUpTime(multiplierTimeSpeed - 1);
            }
        }

        public void Load(ContentManager contentManager)
        {
            debugText.Load(contentManager, fontName);
        }

        public void Update()
        {
            if (base.IsDebugging == true)
            {
                debugText.Value = timeText + time.ToString();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (base.IsDebugging == true)
            {
                debugText.Draw(spriteBatch);
            }
        }
    }
}
