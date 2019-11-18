using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    enum PauseStates
    {
        Pause,
        Resume,
        Restart,
        Exit
    }

    class PauseBoard : Board
    {
        private Label label;
        private Hypertext[] buttons;

        public PauseBoard()
        {
            label = new Label();
            buttons = new Hypertext[3] { new Hypertext(), new Hypertext(), new Hypertext() };
        }

        public void LoadContent(MyGame runningGame)
        {
            base.loadContent(runningGame, "GenericBoard", Global.ResizeByX(925), new int[2] { Global.ResizeByY(-1125), Global.ResizeByY(-74) });

            label.LoadContent(runningGame, "Button", "Gioco in pausa...", Global.Resize(280, 540));
            buttons[0].LoadContent(runningGame, false, "Riprendi", Global.Resize(450, 690));
            buttons[1].LoadContent(runningGame, false, "Ricomincia", Global.Resize(187, 837));
            buttons[2].LoadContent(runningGame, false, "Esci", Global.Resize(815, 837));
        }

        public PauseStates Update(bool isActive, MouseState mouseState)
        {
            Point position = base.update(isActive);
            
            if (isActive == true)
            {
                if (buttons[0].Update(isActive, mouseState, position) == true)
                {
                    return PauseStates.Resume;
                }
                else if (buttons[1].Update(isActive, mouseState, position) == true)
                {
                    return PauseStates.Restart;
                }
                else if (buttons[2].Update(isActive, mouseState, position) == true)
                {
                    return PauseStates.Exit;
                }

                return PauseStates.Pause;
            }

            return PauseStates.Resume;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            Point position = base.draw(spriteBatch, light);

            label.Draw(spriteBatch, position, Color.White);
            buttons[0].Draw(spriteBatch, position);
            buttons[1].Draw(spriteBatch, position);
            buttons[2].Draw(spriteBatch, position);
        }
    }
}
