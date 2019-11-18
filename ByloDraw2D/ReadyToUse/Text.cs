using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ByloDraw2D.ReadyToUse
{
    public sealed class Text : ByloDraw2D.String
    {
        public string Value
        {
            get
            {
                return base.value;
            }
            set
            {
                base.value = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return base.position;
            }
            set
            {
                base.position = value;
            }
        }
        public Color Color
        {
            get
            {
                return base.color;
            }
            set
            {
                base.color = value;
            }
        }
        public float Rotation
        {
            get
            {
                return base.rotation;
            }
            set
            {
                base.rotation = value;
            }
        }
        public Vector2 Origin
        {
            get
            {
                return base.origin;
            }
            set
            {
                base.origin = value;
            }
        }
        public float Scale
        {
            get
            {
                return base.scale;
            }
            set
            {
                base.scale = value;
            }
        }
        public SpriteEffects Effects
        {
            get
            {
                return base.effects;
            }
            set
            {
                base.effects = value;
            }
        }

        public Text()
        {
        }

        public void Load(ContentManager contentManager, string pathName)
        {
            base.loadFont(contentManager, pathName);
        }
    }
}
