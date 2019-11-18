using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    enum MatchOptions
    {
        Null,
        New,
        Exit
    };

    class WinnerBoard : Board
    {
        private bool active;

        private Label label;
        private Hypertext[] buttons;

        public WinnerBoard()
        {
            active = false;

            label = new Label();
            buttons = new Hypertext[2] { new Hypertext(), new Hypertext() };
        }

        public void LoadContent(MyGame runningGame)
        {
            base.loadContent(runningGame, "GenericBoard", Global.ResizeByX(925), new int[2] { Global.ResizeByY(-1125), Global.ResizeByY(-74) });

            label.LoadContent(runningGame, "Button");
            buttons[0].LoadContent(runningGame, false, "Rigioca", Global.Resize(187, 837));
            buttons[1].LoadContent(runningGame, false, "Esci", Global.Resize(815, 837));
        }

        public void Active(string winnerName)
        {
            Point dimension = Dimension();

            active = true;
            label.NewText(winnerName + " ha vinto!", Global.Resize(0, 655), new Point(dimension.X, 0));
        }

        public MatchOptions Update(MouseState mouseState)
        {
            if (active == true)
            {
                Point position = base.update(true);

                if (buttons[0].Update(true, mouseState, position))
                {
                    return MatchOptions.New;
                }
                else if (buttons[1].Update(true, mouseState, position))
                {
                    return MatchOptions.Exit;
                }
            }

            return MatchOptions.Null;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            if (active == true)
            {
                Point position = base.draw(spriteBatch, light);

                label.Draw(spriteBatch, position, Color.White);
                buttons[0].Draw(spriteBatch, position);
                buttons[1].Draw(spriteBatch, position);
            }
        }
    }
}