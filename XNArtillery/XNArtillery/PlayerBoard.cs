using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    class PlayerBoard : Board
    {
        private bool active;
        private string name;

        private Label label;
        private Hypertext[] buttons;

        public PlayerBoard()
        {
            active = false;

            label = new Label();
            buttons = new Hypertext[2] { new Hypertext(), new Hypertext() };
        }

        public void LoadContent(MyGame runningGame)
        {
            base.loadContent(runningGame, "GenericBoard", Global.ResizeByX(925), new int[2] { Global.ResizeByY(-1125), Global.ResizeByY(-74) });

            label.LoadContent(runningGame, "Button");
            buttons[0].LoadContent(runningGame, false, "Accetta", Global.Resize(187, 837));
            buttons[1].LoadContent(runningGame, false, "Rifiuta", Global.Resize(775, 837));
        }

        public void Active(string playerName)
        {
            Point dimension = Dimension();

            active = true;
            name = playerName;
            label.NewText(playerName + " vuole giocare!", Global.Resize(0, 655), new Point(dimension.X, 0));
        }

        public void Update()
        {
            base.update(false);

            buttons[0].Update();
            buttons[1].Update();
        }

        public string Update(bool isActive, MouseState mouseState)
        {
            if (active == true)
            {
                Point position = base.update(isActive);

                if (isActive == true)
                {
                    if (buttons[0].Update(isActive, mouseState, position) == true)
                    {
                        active = false;

                        return name;
                    }
                    else if (buttons[1].Update(isActive, mouseState, position) == true)
                    {
                        active = false;

                        return "à";
                    }
                }
            }
            else
            {
                Update();
            }

            return "";
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            Point position = base.draw(spriteBatch, light);

            label.Draw(spriteBatch, position, Color.White);
            buttons[0].Draw(spriteBatch, position);
            buttons[1].Draw(spriteBatch, position);
        }
    }
}
