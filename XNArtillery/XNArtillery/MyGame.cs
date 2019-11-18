//Iniziato a programmare il 22 Maggio 2012: Interrotto per una futura ripresa del progetto!

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

using XNArtillery.Background;
using XNArtillery.Effects;
using XNArtillery.Engine;
using XNArtillery.Game;

using ByloEngine;

namespace XNArtillery
{
    public class MyGame : Microsoft.Xna.Framework.Game
    {
        private Shade shade;
        private Sky sky;
        private Match match;
        private Music music;

        public MyGame()
        {
            IsMouseVisible = true;

            Global.IsRunning = true;
            Global.ThisGame = this;
            Global.MyGraphics = new Graphics();
            Global.SetResolution();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Global.MyGameTime = new Time();

            shade = new Shade(new Color(147, 152, 157), new Color(255, 255, 255), new Color(147, 152, 157), new Color(40, 50, 60));
            sky = new Sky();
            match = new Match();
            music = new Music();

            base.Initialize();
        }

        private void loadPlayers()          
        {                                
            Global.players[0].LoadContent(1, 1);       
            Global.players[1].LoadContent(1, 1);       
        }                                       

        protected override void LoadContent()
        {
            Global.MySpriteBatch = new SpriteBatch(GraphicsDevice);

            sky.LoadContent();
            loadPlayers();
            match.LoadContent(Global.level, 0, Global.players);
            music.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Global.MyGameTime.Update();
            Global.AmbientalLight = shade.Update();

            sky.Update();
            match.Update();
            music.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Global.MySpriteBatch.Begin();

            sky.Draw();
            match.Draw();

            Global.MySpriteBatch.End();
            GraphicsDevice.Textures[0] = null;

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Global.IsRunning = false;

            base.OnExiting(sender, args);
        }
    }
}
