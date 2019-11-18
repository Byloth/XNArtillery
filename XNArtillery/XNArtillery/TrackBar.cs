using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class TrackBar : Clickable
    {
        private bool active;
        private int max;
        private float alpha;

        private Point maxX;
        private Texture2D track;
        private Texture2D cursor;
        private Rectangle trackPosition;
        private Rectangle cursorPosition;
        private Label label;
        private Fade fade;

        public TrackBar()
        {
            active = false;

            label = new Label();
            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame, int maxValue, Point trackBarPosition)
        {
            max = maxValue;

            track = runningGame.Content.Load<Texture2D>("Images/Objectes/Track");
            cursor = runningGame.Content.Load<Texture2D>("Images/Objectes/Cursor");
            trackPosition = new Rectangle(trackBarPosition.X, trackBarPosition.Y, Global.ResizeByX(track.Width), Global.ResizeByY(track.Height));
            cursorPosition = new Rectangle(trackBarPosition.X + Global.ResizeByX(17), trackBarPosition.Y - Global.ResizeByY(18), Global.ResizeByX(cursor.Width), Global.ResizeByY(cursor.Height));
            maxX = new Point(cursorPosition.X, (cursorPosition.X + trackPosition.Width) - Global.ResizeByX(66));
            label.LoadContent(runningGame, "Text", "", new Point(trackBarPosition.X, trackBarPosition.Y));
        }

        public void LoadContent(MyGame runningGame, string trackBarDescription, int maxValue, Point trackBarPosition)
        {
            max = maxValue;

            track = runningGame.Content.Load<Texture2D>("Images/Objectes/Track");
            cursor = runningGame.Content.Load<Texture2D>("Images/Objectes/Cursor");
            trackPosition = new Rectangle(trackBarPosition.X, trackBarPosition.Y, Global.ResizeByX(track.Width), Global.ResizeByY(track.Height));
            cursorPosition = new Rectangle(trackBarPosition.X + Global.ResizeByX(17), trackBarPosition.Y - Global.ResizeByY(18), Global.ResizeByX(cursor.Width), Global.ResizeByY(cursor.Height));
            maxX = new Point(cursorPosition.X, (cursorPosition.X + trackPosition.Width) - Global.ResizeByX(66));
            label.LoadContent(runningGame, "BoldText", trackBarDescription, new Point(trackBarPosition.X + Global.ResizeByX(15), trackBarPosition.Y - Global.ResizeByY(50)), new Point(trackPosition.Width, 0));
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

            if (isActive == true)
            {
                ObjectStates state = base.GetObjectState(mouseState, Global.AddIncrement(cursorPosition, positionIncrement));

                if ((state == ObjectStates.Pressed) | (state == ObjectStates.PressedNotOver))
                {
                    cursorPosition.X = (mouseState.X - positionIncrement.X) - (cursorPosition.Width / 2);

                    if (cursorPosition.X < maxX.X)
                    {
                        cursorPosition.X = maxX.X;
                    }
                    else if (cursorPosition.X > maxX.Y)
                    {
                        cursorPosition.X = maxX.Y;
                    }
                }

                alpha = fade.Update();

                return ((max * (cursorPosition.X - maxX.X)) / (maxX.Y - maxX.X));
            }
            else
            {
                Update();

                return 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            label.Draw(spriteBatch, positionIncrement, Color.White * alpha);
            spriteBatch.Draw(track, Global.AddIncrement(trackPosition, positionIncrement), light * alpha);
            spriteBatch.Draw(cursor, Global.AddIncrement(cursorPosition, positionIncrement), light * alpha);
        }
    }
}
