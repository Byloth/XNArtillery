using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine.Input;

namespace ByloEngine.Graphics.Effects
{
    public abstract class Shader : Debuggable
    {
        public const string BasePath = "Effects/";

        private static Keys defaultDebugKey
        {
            get
            {
                return Keys.F5;
            }
        }

        protected Effect effect;
        protected RenderTarget2D renderTarget2D;

        public Shader(GraphicsDeviceManager graphicsDeviceManager, KeyboardManager keyboardManager)
            : base(keyboardManager, defaultDebugKey)
        {
            renderTarget2D = new RenderTarget2D(graphicsDeviceManager.GraphicsDevice, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight);
        }

        protected void loadEffect(ContentManager contentManager, string effectName)
        {
            effect = contentManager.Load<Effect>(BasePath + effectName);
        }
    }
}
