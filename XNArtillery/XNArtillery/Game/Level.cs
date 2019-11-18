using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Utility;
using XNArtillery.Engine;

using ByloEngine;

namespace XNArtillery.Game
{
    public class Level : Destroyable
    {
        private int[][] horizons;

        public Level()
        {
            horizons = new int[4][]
            {
                new int[2]
                {
                    Functions.ResizeByHeight(1663),
                    Functions.ResizeByHeight(1663)
                },
                new int[2]
                {
                    Functions.ResizeByHeight(1663),
                    Functions.ResizeByHeight(1246)
                },
                new int[2]
                {
                    Functions.ResizeByHeight(1246),
                    Functions.ResizeByHeight(1663)
                },
                new int[2]
                {
                    Functions.ResizeByHeight(1663),
                    Functions.ResizeByHeight(1663)
                }
            };
        }

        public int[] LoadContent(int levelNumber)
        {
            texture.LoadContent("Game/Levels/Level" + levelNumber, AnchorageTypes.BottomCenter, new Vector2(Global.MyResolution.Width / 2, Global.MyResolution.Height));

            base.loadCollision();

            return horizons[levelNumber - 1];
        }

        public override void Collided(Collider sender, CollisionArgs e)
        {
            base.collided(e.Power, e.Position);
        }
    }
}
