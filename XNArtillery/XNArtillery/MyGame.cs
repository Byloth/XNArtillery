using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

using ByloEngine;
using ByloEngine.Graphics.Effects;
using ByloEngine.Input;

namespace XNArtillery
{
    public class MyGame : Game
    {
        public static Size MaxResolution
        {
            get
            {
                return new Size(3000, 1875);
            }
        }

        private GraphicsDeviceManager graphicsDeviceManager;
        private RenderTarget2D bufferedScreen;

        private Environment environment;

        private Bloom bloom;

        public float ResolutionRatio
        {
            get;
            private set;
        }

        public SpriteBatch SpriteBatch
        {
            get;
            private set;
        }

        public Size Resolution
        {
            get;
            private set;
        }

        public KeyboardManager KeyboardManager
        {
            get;
            private set;
        }

        public Time Time
        {
            get;
            private set;
        }

        private void initializeGraphicsDeviceManager()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            graphicsDeviceManager.IsFullScreen = false;
            graphicsDeviceManager.PreferMultiSampling = true;
            graphicsDeviceManager.PreferredBackBufferWidth = (int)Resolution.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)Resolution.Height;
            graphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Vector4;
            graphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
        }

        public MyGame(int width, int height)
        {
            Resolution = new Size(width, height);
            ResolutionRatio = ((width / MaxResolution.Width) + (height / MaxResolution.Height)) / 2;

            initializeGraphicsDeviceManager();

            base.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            KeyboardManager = new KeyboardManager();

            Time = new Time(this);

            environment = new Environment(this);

            bloom = new Bloom(GraphicsDevice, Resolution, KeyboardManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            bufferedScreen = new RenderTarget2D(GraphicsDevice, (int)Resolution.Width, (int)Resolution.Height);

            Time.LoadContent(this);

            environment.LoadContent(this);

            bloom.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (base.IsActive == true)
            {
                Time.Update(gameTime);

                KeyboardManager.Update();

                environment.Update(this);
            }

            base.Update(gameTime);
        }

        private void drawOnRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(bufferedScreen);

            environment.Draw(this);
        }

        private void applyEffects()
        {
            bloom.Apply(GraphicsDevice, SpriteBatch, bufferedScreen);
        }

        private void drawOnScreen()
        {
            GraphicsDevice.SetRenderTarget(null);

            SpriteBatch.Begin();
            SpriteBatch.Draw(bufferedScreen, Vector2.Zero, Color.White);
            SpriteBatch.End();

            Time.Draw(this);
        }

        protected override void Draw(GameTime gameTime)
        {
            drawOnRenderTarget();
            applyEffects();
            drawOnScreen();

            base.Draw(gameTime);
        }
    }
}
