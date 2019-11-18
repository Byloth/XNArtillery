using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    public class Button : Clickable
    {
        private bool hold;
        private bool clicked;
        private bool highlighted;

        private Texture2D buttons;
        private Rectangle position;
        private Rectangle selected;
        private Label text;

        public Button()
        {
            clicked = false;

            text = new Label();
        }

        public void LoadContent(MyGame runningGame, bool keepHold, string buttonText, Point buttonPosition)
        {
            Vector2 dimension;

            hold = keepHold;
            buttons = runningGame.Content.Load<Texture2D>("Images/Objectes/Buttons");
            text.LoadContent(runningGame, "Button", buttonText, buttonPosition, Global.Resize(buttons.Width, buttons.Height / 3));

            dimension = text.Dimension();

            position = new Rectangle(buttonPosition.X, buttonPosition.Y, Global.ResizeByX(buttons.Width), Global.ResizeByY(buttons.Height / 3));
        }

        public void Release()
        {
            clicked = false;
        }

        public bool Update(MouseState mouseState)
        {
            ObjectStates objectState = base.GetObjectState(mouseState, position);

            highlighted = true;

            switch (objectState)
            {
                case ObjectStates.UnPressed:
                case ObjectStates.PressedAway:
                {
                    highlighted = false;

                    if ((hold == true) & (clicked == true))
                    {
                        selected = new Rectangle(0, buttons.Height / 3, buttons.Width, buttons.Height / 3);
                    }
                    else
                    {
                        selected = new Rectangle(0, 0, buttons.Width, buttons.Height / 3);
                    }

                    break;
                }
                case ObjectStates.Over:
                case ObjectStates.PressedNotOver:
                {
                    selected = new Rectangle(0, buttons.Height / 3, buttons.Width, buttons.Height / 3);

                    break;
                }
                case ObjectStates.Pressed:
                {
                    selected = new Rectangle(0, (buttons.Height / 3) * 2, buttons.Width, buttons.Height / 3);

                    break;
                }
                case ObjectStates.Released:
                {
                    clicked = !clicked;

                    selected = new Rectangle(0, buttons.Height / 3, buttons.Width, buttons.Height / 3);

                    return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            if (highlighted == true)
            {
                light = Color.White;
            }

            spriteBatch.Draw(buttons, position, selected, light);
            text.Draw(spriteBatch, new Color(255 - light.R, 255 - light.G, 255 - light.B));
        }
    }
}
