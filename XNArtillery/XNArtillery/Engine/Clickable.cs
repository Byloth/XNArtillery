using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    public enum ObjectStates
    {
        Pressed,
        PressedNotOver,
        PressedAway,
        Released,
        Over,
        UnPressed
    }

    public class Clickable
    {
        private bool wasClicked;
        private bool clickedOnMe;

        public Clickable()
        {
            wasClicked = false;
            clickedOnMe = false;
        }

        public ObjectStates GetObjectState(MouseState mouseState, Rectangle objectPosition)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if ((mouseState.X >= objectPosition.X) & (mouseState.X <= (objectPosition.X + objectPosition.Width)) & (mouseState.Y >= objectPosition.Y) & (mouseState.Y <= (objectPosition.Y + objectPosition.Height)))
                {
                    if (wasClicked == false)
                    {
                        wasClicked = true;
                        clickedOnMe = true;

                        return ObjectStates.Pressed;
                    }
                    else if (clickedOnMe == true)
                    {
                        wasClicked = true;

                        return ObjectStates.Pressed;
                    }
                    else
                    {
                        return ObjectStates.PressedAway;
                    }
                }
                else
                {
                    if (wasClicked == false)
                    {
                        wasClicked = true;
                        clickedOnMe = false;

                        return ObjectStates.PressedAway;
                    }
                    else if (clickedOnMe == true)
                    {
                        wasClicked = true;

                        return ObjectStates.PressedNotOver;
                    }
                    else
                    {
                        return ObjectStates.UnPressed;
                    }
                }
            }
            else
            {
                if ((mouseState.X >= objectPosition.X) & (mouseState.X <= (objectPosition.X + objectPosition.Width)) & (mouseState.Y >= objectPosition.Y) & (mouseState.Y <= (objectPosition.Y + objectPosition.Height)))
                {
                    if ((wasClicked == true) & (clickedOnMe == true))
                    {
                        wasClicked = false;
                        clickedOnMe = false;

                        return ObjectStates.Released;
                    }
                    else
                    {
                        wasClicked = false;

                        return ObjectStates.Over;
                    }
                }
                else
                {
                    wasClicked = false;

                    return ObjectStates.UnPressed;
                }
            }

        }
    }
}
