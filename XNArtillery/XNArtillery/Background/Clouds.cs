using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Engine;

using ByloEngine;

namespace XNArtillery.Background
{
    class Clouds
    {
        private const int maximumClouds = 20;
        private const float generationProbability = 0.5F;

        private List<Cloud> clouds;
        private Texture2D cloudsTexture;
        private Size size;
        private Color color;
        private Shade shade;

        public Clouds()
        {
            clouds = new List<Cloud>();
            shade = new Shade(new Color(255, 183, 185), new Color(255, 255, 255), new Color(217, 150, 148), new Color(0, 9, 17));
        }

        public void LoadContent()
        {
            cloudsTexture = Global.ThisGame.Content.Load<Texture2D>("Images/Background/Clouds");
            size = new Size(cloudsTexture.Width, cloudsTexture.Height);
        }

        public void Update()
        {
            if (clouds.Count < maximumClouds)
            {
                if (Randomize.Boolean(generationProbability) == true)
                {
                    clouds.Add(new Cloud(size));
                }
            }

            for (int i = 0; i < clouds.Count; i += 1)
            {
                if (clouds[i].Update() == true)
                {
                    clouds.RemoveAt(i);
                }
            }

            color = shade.Update();
        }

        public void Draw()
        {
            foreach (Cloud cloud in clouds)
            {
                cloud.Draw(cloudsTexture, color);
            }
        }
    }
}
