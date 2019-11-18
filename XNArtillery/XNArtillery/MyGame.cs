//Codice di Bilotta Matteo; Copyright© 2011; Iniziato a programmare il 29 Settembre 2011.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Media;

namespace XNArtillery
{
    enum GameStates
    {
        Menu,
        NewMatch,
        Match
    }

    public class MyGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private MouseState mouseState;
        private KeyboardState keyboardState;

        private GameStates state;

        private Sky sky;

        private Shade shade;
        private Color light;

        private Menu menu;
        private Match match;
        private MatchSettings lastMatchSettings;

        public MyGame()
        {
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Global.resolution.X;
            graphicsDeviceManager.PreferredBackBufferHeight = Global.resolution.Y;
            graphicsDeviceManager.IsFullScreen = Global.fullScreen;

            state = GameStates.Menu;
        }

        protected override void Initialize()
        {
            sky = new Sky();

            shade = new Shade(new Color[4] {new Color(147, 152, 157), new Color(255, 255, 255), new Color(147, 152, 157), new Color(40, 50, 60)});

            menu = new Menu();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sky.LoadContent(this);

            menu.LoadContent(this);
        }

        private void NewMatch(MatchSettings matchSettings, MouseState mouseState)
        {
            state = GameStates.Match;
            lastMatchSettings = matchSettings;

            match = new Match();
            match.LoadContent(this, matchSettings);

            state = match.Update(mouseState);
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            Global.time += Global.increment;

            if (Global.time >= 360)
            {
                Global.time = 0;
            }

            sky.Update();

            light = shade.Update();

            if (state == GameStates.Menu)
            {
                MatchSettings matchSettings = menu.Update(mouseState, keyboardState);

                if (matchSettings.matchType != MatchTypes.Null)
                {
                    NewMatch(matchSettings, mouseState);
                }
            }
            else
            {
                state = match.Update(mouseState);

                if (state == GameStates.Menu)
                {
                    menu = new Menu();
                    menu.LoadContent(this);
                }
                else if (state == GameStates.NewMatch)
                {
                    NewMatch(lastMatchSettings, mouseState);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sky.Draw(GraphicsDevice, spriteBatch);

            if (state == GameStates.Menu)
            {
                menu.Draw(spriteBatch, light);
            }
            else if(state == GameStates.Match)
            {
                match.Draw(spriteBatch, light);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            menu.StopThreads();
        } 

    }
}
