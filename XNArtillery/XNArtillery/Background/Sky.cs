using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery.Background
{
    class Sky
    {
        private SkyShader shader;
        private Stars stars;
        private Asters asters;
        private Clouds clouds;

        public Sky()
        {
            shader = new SkyShader();
            stars = new Stars();
            asters = new Asters();
            clouds = new Clouds();
        }

        public void LoadContent()
        {
            shader.LoadContent();
            stars.LoadContent();
            asters.LoadContent();
            clouds.LoadContent();
        }

        public void Update()
        {
            shader.Update();
            stars.Update();
            asters.Update();
            clouds.Update();
        }

        public void Draw()
        {
            shader.Draw();
            stars.Draw();
            asters.Draw();
            clouds.Draw();
        }
    }
}
