using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Stars
    {
        private bool generated;
        private float alpha;

        private Texture2D star;
        private List<Rectangle> positions;
        private List<float> rotations;
        private Fade fade;

        public Stars()
        {
            generated = false;
            alpha = 0;
            positions = new List<Rectangle>();
            rotations = new List<float>();
            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame)
        {
            star = runningGame.Content.Load<Texture2D>("Images/Environment/Star");
        }

        public void Update()
        {
            if (Global.time >= 359)
            {
                positions = new List<Rectangle>();
                generated = false;
            }
            else if ((Global.time >= 180) & (generated == false))
            {
                for (int i = 0; i < 250; i++)
                {
                    int dimension = Global.Random(Global.ResizeByX(star.Width / 2), Global.ResizeByX(star.Width));
                    Rectangle starPosition = new Rectangle(Global.Random(0, Global.resolution.X), Global.Random(0, Global.Horizon()), dimension, dimension);

                    positions.Add(starPosition);
                    rotations.Add(Global.Random(0, (int)Math.PI * 100) / 100);
                }
                generated = true;
            }
            else
            {
                if ((Global.time > 180) & (Global.time < 181))
                {
                    fade.Start(FadeStates.In, true, 45);
                }
                else if ((Global.time > 314) & (Global.time < 315))
                {
                    fade.Start(FadeStates.Out, true, 45);
                }

                alpha = fade.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Count; i ++)
            {
                spriteBatch.Draw(star, positions[i], new Rectangle(0, 0, star.Width, star.Height), Color.White * alpha, rotations[i], new Vector2(positions[i].Width / 2, positions[i].Height / 2), SpriteEffects.None, 0);
            }
        }
    }
}
