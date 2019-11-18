using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class DynamicTexture2D
    {
        private Texture2D texture;
        private Rectangle position;
        private Breaker breaker;

        public DynamicTexture2D()
        {
        }

        private Texture2D LoadTexture2D(MyGame runningGame, string pathName)
        {
            Texture2D loadedTexture = runningGame.Content.Load<Texture2D>("Images/" + pathName);
            texture = new Texture2D(runningGame.GraphicsDevice, loadedTexture.Width, loadedTexture.Height);
            Color[] textureColors = new Color[loadedTexture.Width * loadedTexture.Height];

            loadedTexture.GetData<Color>(textureColors);
            texture.SetData<Color>(textureColors);

            return texture;
        }

        public Point LoadContent(MyGame runningGame, string pathName)
        {
            LoadTexture2D(runningGame, pathName);
            breaker = new Breaker(texture);

            return Global.Resize(texture.Width, texture.Height);
        }

        public Texture2D Texture2D()
        {
            return texture;
        }

        public Point Position()
        {
            return new Point(position.X, position.Y);
        }

        public void Position(Point newImagePosition)
        {
            position = new Rectangle(newImagePosition.X, newImagePosition.Y, Global.ResizeByX(texture.Width), Global.ResizeByY(texture.Height));
        }

        public Point Dimension()
        {
            return new Point(position.Width, position.Height);
        }

        public void Hit(int damage, Point holePosition)
        {
            Color[] textureColors = breaker.Break(damage, Global.Size(new Point(holePosition.X - position.X, holePosition.Y - position.Y)));

            texture.SetData<Color>(textureColors);
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            spriteBatch.Draw(texture, position, light);
        }
    }
}
