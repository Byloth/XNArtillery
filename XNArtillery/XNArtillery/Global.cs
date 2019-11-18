using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Game;
using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery
{
    public class Global
    {
        public const int MaxWidth = 3000;
        public const int MaxHeight = 1875;

        public static bool IsFullScreen;
        public static bool IsRunning;
        public static int level;
        public static float windInfluence;
        public static float MyScale;

        public static MyGame ThisGame;
        public static Graphics MyGraphics;
        public static SpriteBatch MySpriteBatch;
        public static Color AmbientalLight;

        public static Size MyResolution;

        public static Time MyGameTime;

        public static Collider MyCollider;

        public static string[] playersNames;
        public static Player[] players;

        public static void SetResolution()
        {
            MyGraphics.DeviceManager.IsFullScreen = IsFullScreen;
            MyGraphics.DeviceManager.PreferredBackBufferWidth = (int)MyResolution.Width;
            MyGraphics.DeviceManager.PreferredBackBufferHeight = (int)MyResolution.Height;

            MyScale = (MyResolution.Width + MyResolution.Height) / (MaxWidth + MaxHeight);
        }
    }
}
