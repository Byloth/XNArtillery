using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    enum MenuStates
    {
        Main,
        NewGame,
        Options
    }
    class Menu
    {
        private MyGame game;

        private MenuStates state;
           
        private Point[] positions;
            
        private Texture2D menu;
        private Button[] buttons;
        private NewGameBoard newGame;

        public Menu()
        {
            state = MenuStates.Main;

            buttons = new Button[3]
            {
                new Button(),
                new Button(),
                new Button()
            };

            positions = new Point[3]
            {
                Global.Resize(1995, 355),
                Global.Resize(1995, 682),
                Global.Resize(1995, 1013)
            };

            newGame = new NewGameBoard();
        }

        public void LoadContent(MyGame runningGame)
        {
            game = runningGame;

            menu = game.Content.Load<Texture2D>("Images/Objectes/Menu");

            buttons[0].LoadContent(game, true, "Nuova partita",  positions[0]);
            //buttons[1].LoadContent(game, true, "Opzioni", positions[1]);
            buttons[2].LoadContent(game, false, "Esci", positions[2]);

            newGame.LoadContent(runningGame);
        }

        public void StopThreads()
        {
            newGame.StopThreads();
        }

        public MatchSettings Update(MouseState mouseState, KeyboardState keyboardState)
        {
            MatchSettings matchSettings = new MatchSettings();

            if (state == MenuStates.Main)
            {
                matchSettings = newGame.Update();
            }
            else if (state == MenuStates.NewGame)
            {
                matchSettings = newGame.Update(true, mouseState, keyboardState);
            }

            if (buttons[0].Update(mouseState) == true)
            {
                if (state == MenuStates.NewGame)
                {
                    state = MenuStates.Main;
                }
                else
                {
                    state = MenuStates.NewGame;
                    buttons[1].Release();
                }
            }
            /*else if (buttons[1].Update(mouseState) == true)
            {
                if (state == MenuStates.Options)
                {
                    state = MenuStates.Main;
                }
                else
                {
                    state = MenuStates.Options;
                    buttons[0].Release();
                }
            }*/
            else if (buttons[2].Update(mouseState) == true)
            {
                game.Exit();
            }

            return matchSettings;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            spriteBatch.Draw(menu, new Rectangle(0, 0, Global.resolution.X, Global.resolution.Y), light);

            buttons[0].Draw(spriteBatch, light);
            //buttons[1].Draw(spriteBatch, light);
            buttons[2].Draw(spriteBatch, light);

            newGame.Draw(spriteBatch, light);
        }
    }
}
