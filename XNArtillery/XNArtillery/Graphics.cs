using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    public class Graphics
    {
        public GraphicsDeviceManager DeviceManager;

        public Graphics()
        {
            DeviceManager = new GraphicsDeviceManager(Global.ThisGame);

            DeviceManager.PreferMultiSampling = true;               //Anti-aliasing.
            DeviceManager.SynchronizeWithVerticalRetrace = true;    //V-Sync.
        }
    }
}
