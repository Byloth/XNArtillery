using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class Arrow : Clickable
    {
        private bool clicked;

        private Texture2D arrow;
        private Rectangle position;
        private Rectangle selected;
        private SpriteEffects effect;

        public Arrow()
        {
            clicked = false;
        }

        public void LoadContent(MyGame runningGame, bool leftToRight, Point arrowPosition)
        {
            arrow = runningGame.Content.Load<Texture2D>("Images/Objectes/Arrows");
            position = new Rectangle(arrowPosition.X, arrowPosition.Y, Global.ResizeByX(arrow.Width), Global.ResizeByY(arrow.Height) / 2);

            if (leftToRight == true)
            {
                effect = SpriteEffects.None;
            }
            else
            {
                effect = SpriteEffects.FlipHorizontally;
            }
        }

        public bool Update(MouseState mouseState, Point positionIncrement)
        {
            ObjectStates objectState = base.GetObjectState(mouseState, Global.AddIncrement(position, positionIncrement));

            switch (objectState)
            {
                case ObjectStates.Pressed:

                    clicked = true;
                    selected = new Rectangle(0, arrow.Height / 2, arrow.Width, arrow.Height / 2);

                    break;

                case ObjectStates.Released:

                    clicked = false;
                    selected = new Rectangle(0, 0, arrow.Width, arrow.Height / 2);

                    return true;

                default:

                    selected = new Rectangle(0, 0, arrow.Width, arrow.Height / 2);

                    break;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            spriteBatch.Draw(arrow, Global.AddIncrement(position, positionIncrement), selected, light, 0, new Vector2(), effect, 0);
        }
    }
}
