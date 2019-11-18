using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Engine;
using XNArtillery.Utility;

namespace XNArtillery.Background
{
    class SkyShader
    {
        private Shade[] shades;
        private StaticTexture2D[] skyShades;
        private Color[] colors;

        public SkyShader()
        {
            shades = new Shade[]
            {
                new Shade(new Color(85, 142, 213), new Color(225, 237, 255), new Color(58, 125, 206), new Color(0, 13, 25)),    //SkyUp!
                new Shade(new Color(255, 183, 185), new Color(83, 153, 255), new Color(255, 102, 0), new Color(0, 0, 0)),       //SkyDown!
                new Shade(new Color(214, 193, 255), new Color(83, 153, 255), new Color(217, 150, 148), new Color(0, 9, 17))     //Sky!
            };

            skyShades = new StaticTexture2D[]
            {
                new StaticTexture2D(),
                new StaticTexture2D()
            };

            colors = new Color[]
            {
                new Color(),
                new Color(),
                new Color()
            };
        }

        public void LoadContent()
        {
            skyShades[0].LoadContent("Effects/Sky/SkyUp", AnchorageTypes.TopLeft);
            skyShades[1].LoadContent("Effects/Sky/SkyDown", AnchorageTypes.BottomLeft, new Vector2(0, Global.MyResolution.Height));
        }

        public void Update()
        {
            float time = Global.MyGameTime.Value();

            colors[0] = shades[0].Update(time);
            colors[1] = shades[1].Update(time);
            colors[2] = shades[2].Update(time);
        }

        public void Draw()
        {
            Global.ThisGame.GraphicsDevice.Clear(colors[2]);

            skyShades[0].Draw(colors[0]);
            skyShades[1].Draw(colors[1]);
        }
    }
}
