using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XNArtillery.Background;

namespace XNArtillery
{
    class Environment
    {
        private Sky sky;
        private Stars stars;
        private Sun sun;
        private Moon moon;
        private Clouds clouds;

        public Environment(MyGame myGame)
        {
            sky = new Sky(myGame);
            stars = new Stars(myGame);
            sun = new Sun(myGame);
            moon = new Moon(myGame);
            clouds = new Clouds(myGame);
        }

        public void LoadContent(MyGame myGame)
        {
            sky.LoadContent(myGame);
            stars.LoadContent(myGame);
            sun.LoadContent(myGame);
            moon.LoadContent(myGame);
            clouds.LoadContent(myGame);
        }

        public void Update(MyGame myGame)
        {
            sky.Update(myGame);
            stars.Update(myGame);
            sun.Update(myGame);
            moon.Update(myGame);
            clouds.Update(sky, sun);
        }

        public void Draw(MyGame myGame)
        {
            sky.Draw(myGame);
            stars.Draw(myGame.SpriteBatch);
            sun.Draw(myGame.SpriteBatch);
            moon.Draw(myGame.SpriteBatch);
            clouds.Draw(myGame.SpriteBatch);
        }
    }
}
