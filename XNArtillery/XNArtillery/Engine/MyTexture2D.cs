using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Engine
{
    public enum AnchorageTypes
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    abstract public class MyTexture2D
    {
        public float Rotation;

        public Texture2D Texture;
        public Vector2 Origin;
        public Rectangle Destination;
        public Rectangle Selected;

        public MyTexture2D()
        {
            Rotation = 0;

            Origin = new Vector2();
        }

        public Vector2 Position()
        {
            return new Vector2(Destination.X, Destination.Y);
        }

        public void Position(Vector2 texturePosition)
        {
            Destination.X = (int)texturePosition.X;
            Destination.Y = (int)texturePosition.Y;
        }

        private void setOriginX(AnchorageTypes anchorageType)
        {
            switch (anchorageType)
            {
                case AnchorageTypes.TopLeft:
                case AnchorageTypes.CenterLeft:
                case AnchorageTypes.BottomLeft:

                    Origin.X = 0;

                    break;

                case AnchorageTypes.TopCenter:
                case AnchorageTypes.Center:
                case AnchorageTypes.BottomCenter:

                    Origin.X = Texture.Width / 2;

                    break;

                case AnchorageTypes.TopRight:
                case AnchorageTypes.CenterRight:
                case AnchorageTypes.BottomRight:

                    Origin.X = Texture.Width;

                    break;
            }
        }

        private void setOriginY(AnchorageTypes anchorageType)
        {
            switch (anchorageType)
            {
                case AnchorageTypes.TopLeft:
                case AnchorageTypes.TopCenter:
                case AnchorageTypes.TopRight:

                    Origin.Y = 0;

                    break;

                case AnchorageTypes.CenterLeft:
                case AnchorageTypes.Center:
                case AnchorageTypes.CenterRight:

                    Origin.Y = Texture.Height / 2;

                    break;

                case AnchorageTypes.BottomLeft:
                case AnchorageTypes.BottomCenter:
                case AnchorageTypes.BottomRight:

                    Origin.Y = Texture.Height;

                    break;
            }
        }

        public Rectangle Position(AnchorageTypes anchorageType, Vector2 texturePosition)
        {
            setOriginX(anchorageType);
            setOriginY(anchorageType);

            Destination.X = (int)texturePosition.X;
            Destination.Y = (int)texturePosition.Y;

            return Destination;
        }

        public Vector2 TopLeftPixelPosition()
        {
            Vector2 position = Functions.SizedPosition(Destination);

            position.X -= Origin.X;
            position.Y -= Origin.Y;

            return position;
        }

        public float Scale()
        {
            return (Functions.ResizeByWidth(Texture.Width) / Destination.Width);
        }

        public void Scale(float scale)
        {
            Destination.Width = (int)(Functions.ResizeByWidth(Texture.Width) * scale);
            Destination.Height = (int)(Functions.ResizeByWidth(Texture.Height) * scale);
        }

        abstract protected void loadTexture(string pathName);

        public Size LoadContent(string pathName)
        {
            loadTexture(pathName);
            Destination = Functions.ResizedRectangle(Texture.Width, Texture.Height);
            Selected = new Rectangle(0, 0, Texture.Width, Texture.Height);

            return new Size(Destination.Width, Destination.Height);
        }

        public Rectangle LoadContent(string pathName, AnchorageTypes anchorageType)
        {
            LoadContent(pathName);

            return Position(anchorageType, new Vector2());
        }

        public Rectangle LoadContent(string pathName, AnchorageTypes anchorageType, Vector2 texturePosition)
        {
            LoadContent(pathName);

            return Position(anchorageType, texturePosition);
        }

        protected void draw(Color color)
        {
            Global.MySpriteBatch.Draw(Texture, Destination, Selected, color, Rotation, Origin, SpriteEffects.None, 0);
        }

        public void Draw()
        {
            draw(Global.AmbientalLight);
        }

        public void Draw(Color color)
        {
            draw(color);
        }
    }
}
