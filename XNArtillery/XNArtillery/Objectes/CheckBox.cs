using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class CheckBox : Clickable
    {
        private bool active;
        private bool hold;
        private bool clicked;
        private bool highlighted;
        private float alpha;

        private Texture2D checkBoxes;
        private Rectangle position;
        private Rectangle selected;
        private Rectangle area;
        private Hypertext hypertext;
        private Fade fade;

        public CheckBox()
        {
            active = false;
            hold = true;
            clicked = false;
            highlighted = false;

            hypertext = new Hypertext();
            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame, string checkBoxText, Point checkBoxPosition)
        {
            float space = Global.ResizeByX(10);

            Rectangle fontPosition;

            checkBoxes = runningGame.Content.Load<Texture2D>("Images/Objectes/CheckBox");

            position = new Rectangle(checkBoxPosition.X, checkBoxPosition.Y, Global.ResizeByX(checkBoxes.Width), Global.ResizeByY(checkBoxes.Height / 3));
            fontPosition = hypertext.LoadContent(runningGame, true, checkBoxText, new Point((int)(position.X + position.Width + space), (int)(position.Y)));
            area = new Rectangle(position.X, (int)position.Y, (int)(position.Width + fontPosition.Width + space), fontPosition.Height); 
        }

        public bool Update(bool isActive, MouseState mouseState, Point positionIncrement)
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
                ObjectStates objectState = base.GetObjectState(mouseState, Global.AddIncrement(area, positionIncrement));

                highlighted = true;

                switch (objectState)
                {
                    case ObjectStates.UnPressed:
                    case ObjectStates.PressedAway:
                        {
                            highlighted = false;

                            if ((hold == true) & (clicked == true))
                            {
                                selected = new Rectangle(0, checkBoxes.Height / 3, checkBoxes.Width, checkBoxes.Height / 3);
                                hypertext.Click();
                            }
                            else
                            {
                                selected = new Rectangle(0, 0, checkBoxes.Width, checkBoxes.Height / 3);
                                hypertext.Release();
                            }

                            break;
                        }
                    case ObjectStates.Over:
                    case ObjectStates.PressedNotOver:
                        {
                            selected = new Rectangle(0, checkBoxes.Height / 3, checkBoxes.Width, checkBoxes.Height / 3);

                            break;
                        }
                    case ObjectStates.Pressed:
                        {
                            selected = new Rectangle(0, (checkBoxes.Height / 3) * 2, checkBoxes.Width, checkBoxes.Height / 3);

                            break;
                        }
                    case ObjectStates.Released:
                        {
                            clicked = !clicked;

                            selected = new Rectangle(0, checkBoxes.Height / 3, checkBoxes.Width, checkBoxes.Height / 3);

                            return true;
                        }
                }

            }

            hypertext.Update(isActive, mouseState, positionIncrement);

            return false;
        }

        public bool Checked()
        {
            if (clicked == true)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            if (highlighted == true)
            {
                light = Color.White;
            }

            spriteBatch.Draw(checkBoxes, Global.AddIncrement(position, positionIncrement), selected, light * alpha);
            hypertext.Draw(spriteBatch, positionIncrement);
        }
    }
}
