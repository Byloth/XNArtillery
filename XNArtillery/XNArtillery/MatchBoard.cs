using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class MatchBoard : Board
    {
        private bool influenced;

        private Label[] names;
        private Bar[] bars;
        private WindBar windBar;

        public MatchBoard()
        {
            names = new Label[2] { new Label(), new Label() };
            bars = new Bar[2] { new Bar(), new Bar() };
            windBar = new WindBar();
        }

        public void LoadContent(MyGame runningGame, bool windInfluence, string opponentName)
        {
            int X = Global.ResizeByY(90);

            Point dimension = base.loadContent(runningGame, "MatchBoard", X, new int[] { Global.ResizeByY(-375), Global.ResizeByY(-74) });

            influenced = windInfluence;

            names[0].LoadContent(runningGame, "Label", Global.nickname, Global.Resize(100, 237), new Point(dimension.X / 4, 0));
            names[1].LoadContent(runningGame, "Label", opponentName, Global.Resize(2083, 237), new Point(dimension.X / 4, 0));
            bars[0].LoadContent(runningGame, false, false, 100, Global.Resize(192, 275));
            bars[1].LoadContent(runningGame, false, false, 100, Global.Resize(2180, 275));

            if (windInfluence == true)
            {
                windBar.LoadContent(runningGame, new Point((Global.resolution.X / 2) - X, Global.ResizeByY(237)));
            }
        }

        public void Update()
        {
            base.update(true);

            bars[0].Update();
            bars[1].Update();

            if (influenced == true)
            {
                windBar.Update();
            }
        }

        public void NewWindPower(float value)
        {
            windBar.NewWindPower(value);
        }

        public bool Hit(int playerHit)
        {
            int percentage;

            percentage = bars[playerHit].AddValue(-34);

            if (percentage <= 0)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {/*
            Point position = base.draw(spriteBatch, light);

            names[0].Draw(spriteBatch, position, Color.White);
            names[1].Draw(spriteBatch, position, Color.White);
            bars[0].Draw(spriteBatch, position, light);
            bars[1].Draw(spriteBatch, position, light);

            if (influenced == true)
            {
                windBar.Draw(spriteBatch, position, light);
            }
          */
        }
    }
}
