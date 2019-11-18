using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class Hypertext : Clickable
    {
        private bool active;
        private bool hold;
        private bool clicked;
        private bool highlighted;
        private float alpha;

        private Texture2D buttons;
        private Rectangle position;
        private Rectangle selected;
        private Label label;
        private Fade fade;

        public Hypertext()
        {
            active = false;
            clicked = false;
            highlighted = false;

            label = new Label();
            fade = new Fade(false);
        }

        public Rectangle LoadContent(MyGame runningGame, bool keepHold, Point hypertextPosition, string hypertextType)
        {
            Vector2 dimension;

            hold = keepHold;

            buttons = runningGame.Content.Load<Texture2D>("Images/Objectes/Highlight");
            label.LoadContent(runningGame, hypertextType, hypertextPosition);
            dimension = label.Dimension();
            position = new Rectangle(hypertextPosition.X, hypertextPosition.Y, (int)dimension.X, (int)dimension.Y);

            return position;
        }

        public Rectangle LoadContent(MyGame runningGame, bool keepHold, string hypertextText, Point hypertextPosition)
        {
            Vector2 dimension;

            hold = keepHold;

            buttons = runningGame.Content.Load<Texture2D>("Images/Objectes/Highlight");
            label.LoadContent(runningGame, "Label", hypertextText, hypertextPosition);
            dimension = label.Dimension();
            position = new Rectangle(hypertextPosition.X, hypertextPosition.Y, (int)dimension.X, (int)dimension.Y);

            return position;
        }

        public Rectangle LoadContent(MyGame runningGame, bool keepHold, string hypertextType, string hypertextText, Point hypertextPosition)
        {
            Vector2 dimension;

            hold = keepHold;

            buttons = runningGame.Content.Load<Texture2D>("Images/Objectes/Highlight");
            label.LoadContent(runningGame, hypertextType, hypertextText, hypertextPosition);
            dimension = label.Dimension();
            position = new Rectangle(hypertextPosition.X, hypertextPosition.Y, (int)dimension.X, (int)dimension.Y);

            return position;
        }

        public void Click()
        {
            clicked = true;
        }

        public void Release()
        {
            clicked = false;
        }

        public void NewText(string selectedText)
        {
            label.NewText(selectedText);

            Vector2 dimension = label.Dimension();
            position = new Rectangle(position.X, position.Y, (int)dimension.X, (int)dimension.Y);
        }

        public void Update()
        {
            clicked = false;

            if (active == true)
            {
                active = false;
                fade.Start(FadeStates.Out, false, 12);
            }

            alpha = fade.Update();
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

            if ((active == true) & (label.Text() != ""))
            {
                ObjectStates objectState = base.GetObjectState(mouseState, Global.AddIncrement(position, positionIncrement));

                highlighted = true;

                alpha = fade.Update();

                switch (objectState)
                {
                    case ObjectStates.UnPressed:
                    case ObjectStates.PressedAway:
                        {
                            highlighted = false;

                            if ((hold == true) & (clicked == true))
                            {
                                selected = new Rectangle(0, buttons.Height / 2, buttons.Width, buttons.Height / 2);
                            }
                            else
                            {
                                selected = new Rectangle(0, 0, 0, 0);
                            }

                            break;
                        }
                    case ObjectStates.Over:
                    case ObjectStates.PressedNotOver:
                        {
                            selected = new Rectangle(0, buttons.Height / 2, buttons.Width, buttons.Height / 2);

                            break;
                        }
                    case ObjectStates.Pressed:
                        {
                            selected = new Rectangle(0, 0, buttons.Width, buttons.Height / 2);

                            break;
                        }
                    case ObjectStates.Released:
                        {
                            clicked = !clicked;

                            selected = new Rectangle(0, buttons.Height / 2, buttons.Width, buttons.Height / 2);

                            return true;
                        }
                }
            }
            else
            {
                Update();
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement)
        {
            Color color = Color.White;

            spriteBatch.Draw(buttons, Global.AddIncrement(position, positionIncrement), selected, color * alpha);

            if ((highlighted == true) | ((clicked == true) & (hold == true)))
            {
                color = Color.Black;
            }

            label.Draw(spriteBatch, positionIncrement, color * alpha);
        }
    }
}
