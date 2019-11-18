using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Loading
    {
        private bool active;
        private float radians;
        private float alpha;

        private Texture2D loading;
        private Vector2 center;
        private Rectangle position;
        private Fade fade;

        public Loading()
        {
            active = false;

            radians = 0;
            alpha = 0;

            fade = new Fade(false);
        }

        public void LoadContent(MyGame runningGame, Point loadingPosition)
        {
            loading = runningGame.Content.Load<Texture2D>("Images/Objectes/Loading");
            center = new Vector2(loading.Width / 2, loading.Height / 2);
            position = new Rectangle(loadingPosition.X, loadingPosition.Y, Global.ResizeByX(loading.Width), Global.ResizeByX(loading.Height));
        }

        public void Update(bool isLoading)
        {
            if (active != isLoading)
            {
                if (isLoading == true)
                {
                    fade.Start(FadeStates.In, false, 12);
                }
                else
                {
                    fade.Start(FadeStates.Out, false, 12);
                }

                active = isLoading;
            }

            alpha = fade.Update();

            if (alpha >= 0)
            {
                if (radians >= 360)
                {
                    radians = 0;
                }

                radians += 0.125F;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            spriteBatch.Draw(loading, Global.AddIncrement(position, positionIncrement), null, light * alpha, radians, center, SpriteEffects.None, 0);
        }
    }
}
