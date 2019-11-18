using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Label
    {
        private float scale;
        private string text;

        private SpriteFont font;
        private Vector2 position;
        private Vector2 dimension;

        public Label()
        {
        }

        public void LoadContent(MyGame runningGame, string pathName)
        {
            text = "";
            scale = Global.Scale();

            font = runningGame.Content.Load<SpriteFont>("Fonts/" + pathName);
            dimension = Global.ResizeText(font.MeasureString(text));
        }

        public void LoadContent(MyGame runningGame, string pathName, Point textPosition)
        {
            text = "";
            scale = Global.Scale();

            font = runningGame.Content.Load<SpriteFont>("Fonts/" + pathName);
            dimension = Global.ResizeText(font.MeasureString(text));

            position = new Vector2(textPosition.X + Global.ResizeByX(10), textPosition.Y);
        }

        public void LoadContent(MyGame runningGame, string pathName, string selectedText, Point textPosition)
        {
            text = selectedText;
            scale = Global.Scale();

            font = runningGame.Content.Load<SpriteFont>("Fonts/" + pathName);
            dimension = Global.ResizeText(font.MeasureString(text));

            position = new Vector2(textPosition.X + Global.ResizeByX(10), textPosition.Y);
        }

        public void LoadContent(MyGame runningGame, string pathName, string selectedText, Point textPosition, Point dimensionForCentering)
        {
            text = selectedText;
            scale = Global.Scale();

            font = runningGame.Content.Load<SpriteFont>("Fonts/" + pathName);
            dimension = Global.ResizeText(font.MeasureString(text));
            position = new Vector2((textPosition.X + dimensionForCentering.X / 2) - (dimension.X / 2), (textPosition.Y + dimensionForCentering.Y / 2) - (dimension.Y / 2));
        }

        public void NewText(string selectedText)
        {
            text = selectedText;
            dimension = Global.ResizeText(font.MeasureString(text));
        }

        public void NewText(string selectedText, Point textPosition)
        {
            text = selectedText;
            dimension = Global.ResizeText(font.MeasureString(text));
            position = new Vector2(textPosition.X + Global.ResizeByX(10), textPosition.Y);
        }

        public void NewText(string selectedText, Point textPosition, Point dimensionForCentering)
        {
            text = selectedText;
            dimension = Global.ResizeText(font.MeasureString(text));
            position = new Vector2((textPosition.X + dimensionForCentering.X / 2) - (dimension.X / 2), (textPosition.Y + dimensionForCentering.Y / 2) - (dimension.Y / 2));
        }

        public string Text()
        {
            return text;
        }

        public Vector2 Dimension()
        {
            return dimension;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(font, text, position, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color color)
        {
            spriteBatch.DrawString(font, text, Global.AddIncrement(position, positionIncrement), color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
