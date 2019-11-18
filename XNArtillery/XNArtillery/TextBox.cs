using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class TextBox : Clickable
    {
        private bool selected;
        private bool active;
        private char lastChar;
        private float alpha;
        private string text;

        private Label[] labels;
        private Texture2D box;
        private Rectangle position;
        private Point labelPosition;
        private Keys lastKey;
        private Fade fade;

        public TextBox()
        {
            selected = false;
            active = false;
            lastChar = '?';
            text = Global.nickname;

            labels = new Label[2] { new Label(), new Label() };
            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame, Point textBoxPosition)
        {
            box = runningGame.Content.Load<Texture2D>("Images/Objectes/TextBox");
            position = new Rectangle(textBoxPosition.X, textBoxPosition.Y, Global.ResizeByX(box.Width), Global.ResizeByY(box.Height));
            labels[0].LoadContent(runningGame, "Text");
            labels[1].LoadContent(runningGame, "BoldText", "", new Point(position.X, position.Y));
            labelPosition = new Point(position.X + Global.ResizeByX(15), position.Y + Global.ResizeByY(55));
            labels[0].NewText(text, labelPosition, new Point(position.Width, 0));
        }

        public void LoadContent(MyGame runningGame, string textBoxDescription, Point textBoxPosition)
        {
            box = runningGame.Content.Load<Texture2D>("Images/Objectes/TextBox");
            position = new Rectangle(textBoxPosition.X, textBoxPosition.Y, Global.ResizeByX(box.Width), Global.ResizeByY(box.Height));
            labels[0].LoadContent(runningGame, "Text");
            labels[1].LoadContent(runningGame, "BoldText", textBoxDescription, new Point(position.X - Global.ResizeByX(266), position.Y + Global.ResizeByY(30)));
            labelPosition = new Point(position.X + Global.ResizeByX(15), position.Y + Global.ResizeByY(55));
            labels[0].NewText(text, labelPosition, new Point(position.Width, 0));
        }

        public string Update()
        {
            if (active == true)
            {
                active = false;
                fade.Start(FadeStates.Out, false, 12);
            }

            alpha = fade.Update();

            return text;
        }

        public string Update(bool isActive, MouseState mouseState, KeyboardState keyboardState, Point positionIncrement)
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

            if (active == true)
            {
                ObjectStates state = base.GetObjectState(mouseState, Global.AddIncrement(position, positionIncrement));

                alpha = fade.Update();

                if (state == ObjectStates.Released)
                {
                    selected = true;
                }
                else if (state == ObjectStates.PressedAway)
                {
                    selected = false;
                }

                if (selected == true)
                {
                    char character = '?';

                    Keys[] keys = keyboardState.GetPressedKeys();

                    Keys key = Global.GetCommand(keys);

                    if (key == Keys.None)
                    {
                        character = Global.GetText(keys);

                        if ((character != lastChar) && (character != '?') && (text.Length < 12))
                        {
                            text += character;
                        }
                    }
                    else
                    {
                        if (key != lastKey)
                        {
                            switch (key)
                            {
                                case Keys.Back:
                                    {
                                        if (text.Length >= 1)
                                        {
                                            text = text.Remove(text.Length - 1);
                                        }

                                        break;
                                    }
                                case Keys.Enter:
                                    {
                                        //selected = false;

                                        break;
                                    }
                                case Keys.Escape:
                                    {
                                        selected = false;

                                        break;
                                    }
                            }
                        }
                    }

                    lastChar = character;
                    lastKey = key;

                    labels[0].NewText(text + "|", labelPosition, new Point(position.Width, 0));
                }

                if (selected == false)
                {
                    labels[0].NewText(text, labelPosition, new Point(position.Width, 0));
                }

                return text;
            }
            else
            {
                return Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            spriteBatch.Draw(box, Global.AddIncrement(position, positionIncrement), light * alpha);
            labels[0].Draw(spriteBatch, positionIncrement, Color.White * alpha);
            labels[1].Draw(spriteBatch, positionIncrement, Color.White * alpha);
        }
    }
}
