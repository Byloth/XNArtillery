using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Graphics;

namespace XNArtillery.Background
{
    class Stars
    {
        private const int starNumber = 2500;
        private const float fadeDuration = 22.5F;
        private const float fadeInTime = 180;
        private const float fadeOutTime = 337.5F;

        private float visibility;
        private Fade fade;

        private Texture2D starTexture;
        private Star[] stars;

        public Stars()
        {
            float hypotenuse;

            if (Global.MyGameTime.Value() >= Time.Night)
            {
                visibility = Fade.Visible;
                fade = new Fade(Fade.Visible);
            }
            else
            {
                visibility = Fade.Invisible;
                fade = new Fade(Fade.Invisible);
            }

            stars = new Star[starNumber];
            hypotenuse = Mathematics.Hypotenuse(Global.MyResolution.Width, Global.MyResolution.Height);
            for (int i = 0; i < stars.Length; i += 1)
            {
                stars[i] = new Star(hypotenuse);
            }
        }

        public void LoadContent()
        {
            starTexture = Global.ThisGame.Content.Load<Texture2D>("Images/Background/Star");
        }

        private void checkStartFade(float time)
        {
            float increment = fadeDuration / Time.TimeIncrement;

            if (time >= fadeOutTime)
            {
                fade.Start(FadeTypes.FadeOut, increment);
            }
            else if (time >= fadeInTime)
            {
                fade.Start(FadeTypes.FadeIn, increment);
            }
        }

        public void Update()
        {
            float timeValue = Global.MyGameTime.Value();

            checkStartFade(timeValue);
            visibility = fade.Update();

            foreach (Star star in stars)
            {
                star.Update(timeValue);
            }
        }

        public void Draw()
        {
            foreach (Star star in stars)
            {
                star.Draw(starTexture, visibility);
            }
        }
    }
}
