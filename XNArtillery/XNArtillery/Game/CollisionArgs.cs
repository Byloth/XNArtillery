using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNArtillery.Game
{
    public class CollisionArgs : EventArgs
    {
        public float Power;
        public Vector2 Position;

        public CollisionArgs(float power, Vector2 position)
        {
            Power = power;
            Position = position;
        }
    }
}
