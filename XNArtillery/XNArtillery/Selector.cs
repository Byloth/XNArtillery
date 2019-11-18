using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class Selector
    {
        private bool active;
        private int index;
        private float alpha;

        private Arrow[] arrows;
        private Fade fade;

        public Selector()
        {
            index = 1;

            arrows = new Arrow[2]
            {
                new Arrow(),
                new Arrow()
            };

            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame, Point selectorPosition)
        {
            arrows[0].LoadContent(runningGame, true, Global.AddIncrement(selectorPosition, Global.Resize(100, 100)));
            arrows[1].LoadContent(runningGame, false, Global.AddIncrement(selectorPosition, Global.Resize(100, 250)));
        }

        public void Update()
        {
            if (active == true)
            {
                active = false;
                fade.Start(FadeStates.Out, false, 12);
            }

            alpha = fade.Update();
        }

        public int Update(bool isActive, MouseState mouseState, Point positionIncrement)
        {
            if (active != isActive)
            {
                if (isActive == true)
                {
                    fade.Start(FadeStates.In, false, 12);
                }
                else
                {
                    fade.Start(FadeStates.Out, false, 12);
                }

                active = isActive;
            }

            alpha = fade.Update();

            if (isActive == true)
            {
                if (arrows[0].Update(mouseState, positionIncrement) == true)
                {
                    index += 1;
                }
                else if (arrows[1].Update(mouseState, positionIncrement) == true)
                {
                    index -= 1;
                }
            }

            return index;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            arrows[0].Draw(spriteBatch, positionIncrement, light * alpha);
            arrows[1].Draw(spriteBatch, positionIncrement, light * alpha);
        }
    }
}
