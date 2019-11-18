//XNArtillery Project.
//Iniziato a programmare Mercoledì 19 Giugno 2013.
//Codice di Bilotta Matteo.
//Copyright © 2013 - Bylothink - Tutti i diritti sono riservati.

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine;
using ByloEngine.Input;

using XNvironment2D;

namespace XNArtillery
{
    public class MyGame : Game
    {
        private const int skyLayers = 3;

        public const int MaxWidth = 3000;
        public const int MaxHeight = 1875;

        private float resolutionRatio;

        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private KeyboardManager keyboardManager;

        private XNvironment2D.Environment environment;

        private Clock clock;

        private void initializeGraphicsDeviceManager(int width, int height)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            graphicsDeviceManager.IsFullScreen = false;
            graphicsDeviceManager.PreferMultiSampling = true;
            graphicsDeviceManager.PreferredBackBufferWidth = width;
            graphicsDeviceManager.PreferredBackBufferHeight = height;
            graphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Vector4;
            graphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
        }

        public MyGame(int width, int height)
        {
            resolutionRatio = (((float)width / MaxWidth) + ((float)height / MaxHeight)) / 2;

            initializeGraphicsDeviceManager(width, height);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            keyboardManager = new KeyboardManager();

            environment = new XNvironment2D.Environment(skyLayers, graphicsDeviceManager, resolutionRatio);

            clock = new Clock(environment.Time, keyboardManager, resolutionRatio);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            environment.Load(base.Content);

            clock.Load(base.Content);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardManager.Update();

            environment.Update(gameTime);

            clock.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            environment.Draw(spriteBatch);

            clock.Draw(spriteBatch);
        }
    }
}
