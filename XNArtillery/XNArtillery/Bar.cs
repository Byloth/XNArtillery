using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class Bar
    {
        private bool inverted;
        private int percentage;
        private float unit;

        private Texture2D bar;
        private Texture2D border;
        private Rectangle position;
        private Rectangle selected;
        private Rectangle borderPosition;
        private Movement slide;

        public Bar()
        {
            slide = new Movement();
        }

        public void LoadContent(MyGame runningGame, bool rightToLeft, bool centered, byte startingPercentage, Point barPosition)
        {
            Point dimension;

            inverted = rightToLeft;
            bar = runningGame.Content.Load<Texture2D>("Images/Objectes/Bar");
            border = runningGame.Content.Load<Texture2D>("Images/Objectes/BarBorder");
            dimension = Global.Resize(bar.Width / 2, bar.Height);
            unit = (float)dimension.X / Global.ResizeByX(100);
            position = new Rectangle(barPosition.X + Global.ResizeByX(5), barPosition.Y + Global.ResizeByY(2), dimension.X, dimension.Y);
            selected = new Rectangle(0, 0, bar.Width / 2, bar.Height);
            borderPosition = new Rectangle(barPosition.X, barPosition.Y, Global.ResizeByX(border.Width), Global.ResizeByY(border.Height));

            if (centered == true)
            {
                position.X -= dimension.X / 2;
                borderPosition.X -= dimension.X / 2;

                position.Y -= dimension.Y / 2;
                borderPosition.Y -= dimension.Y / 2;
            }
            else if (rightToLeft == true)
            {
                position.X -= dimension.X + Global.ResizeByX(5);
                borderPosition.X -= Global.ResizeByX(border.Width - 5);
            }

            NewValue(startingPercentage);
        }

        public void NewValue(int newPercentage)
        {
            percentage = newPercentage;
            slide.Start(true, selected.X, (int)((100 - percentage) * unit), 10);
        }

        public int AddValue(int value)
        {
            percentage += value;
            slide.Start(true, selected.X, (int)((100 - percentage) * unit), 10);

            return percentage;
        }

        public void Update()
        {
            selected.X = slide.Update(selected.X);
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            if (inverted == false)
            {
                spriteBatch.Draw(bar, position, selected, light);
            }
            else
            {
                spriteBatch.Draw(bar, position, selected, light, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
            }

            spriteBatch.Draw(border, borderPosition, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            if (inverted == false)
            {
                spriteBatch.Draw(bar, Global.AddIncrement(position, positionIncrement), selected, light);
            }
            else
            {
                spriteBatch.Draw(bar, Global.AddIncrement(position, positionIncrement), selected, light, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
            }

            spriteBatch.Draw(border, Global.AddIncrement(borderPosition, positionIncrement), light);
        }
    }
}
