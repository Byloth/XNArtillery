using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class Board
    {
        private bool active;
        private int[] positions;

        private Texture2D board;
        private Rectangle position;
        private Movement fall;

        public Board()
        {
            active = false;

            fall = new Movement();
        }

        protected Point loadContent(MyGame runningGame, string boardName, int positionX, int[] positionsY)
        {
            positions = positionsY;

            board = runningGame.Content.Load<Texture2D>("Images/Objectes/" + boardName);
            position = new Rectangle(positionX, positions[0], Global.ResizeByX(board.Width), Global.ResizeByY(board.Height));

            return new Point(position.Width, position.Height);
        }

        protected Point update(bool isActive)
        {
            if (active != isActive)
            {
                if (isActive == true)
                {
                    fall.Start(true, position.Y, positions[1], 25);
                }
                else
                {
                    fall.Start(false, position.Y, positions[0], 25);
                }

                active = isActive;
            }

            position.Y = fall.Update(position.Y);

            return new Point(position.X, position.Y);
        }

        public Point Dimension()
        {
            return new Point(position.Width, position.Height);
        }

        protected Point draw(SpriteBatch spriteBatch, Color light)
        {
            spriteBatch.Draw(board, position, light);

            return new Point(position.X, position.Y);
        }
    }
}
