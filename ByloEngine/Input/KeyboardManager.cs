using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace ByloEngine.Input
{
    public delegate void KeyEventHandler(Keys eventKey);

    public class KeyboardManager
    {
        private Keys[] lastKeysDown;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyPressed;
        public event KeyEventHandler KeyReleased;

        public KeyboardManager()
        {
            lastKeysDown = new Keys[0];
        }

        private void Riase(KeyEventHandler keyEvent, Keys eventKey)
        {
            if (keyEvent != null)
            {
                keyEvent(eventKey);
            }
        }

        public void Update()
        {
            bool wasDown;

            Keys[] keysDown = Keyboard.GetState().GetPressedKeys();

            foreach (Keys keyDown in keysDown)
            {
                wasDown = false;

                foreach (Keys lastKeyDown in lastKeysDown)
                {
                    if (keyDown == lastKeyDown)
                    {
                        wasDown = true;

                        break;
                    }
                }

                Riase(KeyDown, keyDown);

                if (wasDown == false)
                {
                    Riase(KeyPressed, keyDown);
                }
            }


            foreach (Keys lastKeyDown in lastKeysDown)
            {
                wasDown = false;

                foreach (Keys keyDown in keysDown)
                {
                    if (lastKeyDown == keyDown)
                    {
                        wasDown = true;

                        break;
                    }
                }

                if (wasDown == false)
                {
                    Riase(KeyReleased, lastKeyDown);
                }
            }

            lastKeysDown = keysDown;
        }
    }
}
