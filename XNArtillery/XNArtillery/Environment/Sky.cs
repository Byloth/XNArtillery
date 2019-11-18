using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Sky
    {
        private double[,] increment;

        private Texture2D[] sky;
        private Rectangle[] position;
        private Shade[] shades;
        private Color[] colors;
        private Stars stars;
        private Asters asters;
        private Clouds clouds;

        public Sky()
        {
            shades = new Shade[3]
            {
                new Shade(new Color[4] {new Color(85, 142, 213), new Color(225, 237, 255), new Color(58, 125, 206), new Color(0, 13, 25)}),
                new Shade(new Color[4] {new Color(214, 193, 255), new Color(83, 153, 255), new Color(217, 150, 148), new Color(0, 9, 17)}),
                new Shade(new Color[4] {new Color(255, 183, 185), new Color(83, 153, 255), new Color(255, 102, 0), new Color(0, 0, 0)})
            };

            increment = new double[3, 3];
            sky = new Texture2D[2];
            position = new Rectangle[2];
            colors = new Color[3];
            stars = new Stars();
            asters = new Asters();
            clouds = new Clouds(new Color[4] { new Color(255, 183, 185), new Color(255, 255, 255), new Color(217, 150, 148), new Color(0, 9, 17) });
        }

        public void LoadContent(MyGame runningGame)
        {
            sky[0] = runningGame.Content.Load<Texture2D>("Images/Effects/SkyUp");
            sky[1] = runningGame.Content.Load<Texture2D>("Images/Effects/SkyDown");
            position[0] = new Rectangle(0, 0, Global.ResizeByX(sky[0].Width), Global.ResizeByY(sky[0].Height));
            position[1] = new Rectangle(0, Global.resolution.Y - Global.ResizeByY(sky[1].Height), Global.ResizeByX(sky[1].Width), Global.ResizeByY(sky[1].Height));
            stars.LoadContent(runningGame);
            asters.LoadContent(runningGame);
            clouds.LoadContent(runningGame);
        }

        public void Update()
        {
            colors[0] = shades[0].Update();
            colors[1] = shades[1].Update();
            colors[2] = shades[2].Update();
            stars.Update();
            asters.Update();
            clouds.Update();
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(colors[1]);

            spriteBatch.Begin();
            spriteBatch.Draw(sky[0], position[0], colors[0]);
            spriteBatch.Draw(sky[1], position[1], colors[2]);
            stars.Draw(spriteBatch);
            asters.Draw(spriteBatch);
            clouds.Draw(spriteBatch);
        }
    }
}
