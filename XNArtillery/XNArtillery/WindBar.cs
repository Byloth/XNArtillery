using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class WindBar
    {
        private Bar[] bars;

        public WindBar()
        {
            bars = new Bar[2]
            {
                new Bar(),
                new Bar()
            };
        }

        public void LoadContent(MyGame runningGame, Point windBarPosition)
        {
            bars[0].LoadContent(runningGame, false, false, 0, windBarPosition);
            bars[1].LoadContent(runningGame, true, false, 0, windBarPosition);
        }

        public void NewWindPower(float value)
        {
            value = (value * 100) / 12;

            if (value >= 0)
            {
                bars[0].NewValue(0);
                bars[1].NewValue((int)value);
            }
            else
            {
                bars[0].NewValue((int)-value);
                bars[1].NewValue(0);
            }
        }

        public void Update()
        {
            bars[0].Update();
            bars[1].Update();
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            bars[0].Draw(spriteBatch, light);
            bars[1].Draw(spriteBatch, light);
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            bars[0].Draw(spriteBatch, positionIncrement, light);
            bars[1].Draw(spriteBatch, positionIncrement, light);
        }
    }
}
