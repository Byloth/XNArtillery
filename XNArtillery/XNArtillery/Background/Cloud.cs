using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Background
{
    class Cloud
    {
        private const int maximumCloudsType = 3;
        private const int maximumDenominator = 2;
        private const float minimumSpeed = 0.50F;
        private const float maximumSpeed = 1.50F;

        private bool leftToRight;
        private float speed;
        private float positionX;

        private Rectangle rectangle;
        private Rectangle selected;

        public Cloud(Size cloudSize)
        {
            Size size = new Size();

            size.Width = Functions.ResizeByWidth(Randomize.Decimal((cloudSize.Width / maximumCloudsType) / maximumDenominator, cloudSize.Width / maximumCloudsType));
            size.Height = Functions.ResizeByHeight(Randomize.Decimal(cloudSize.Height / maximumDenominator, cloudSize.Height));

            leftToRight = Randomize.Boolean();

            if (leftToRight == true)
            {
                rectangle = new Rectangle((int)-size.Width, Randomize.Integer(-size.Height, Global.MyResolution.Height - size.Height), (int)size.Width, (int)size.Height);
            }
            else
            {
                rectangle = new Rectangle((int)(Global.MyResolution.Width + size.Width), Randomize.Integer(-size.Height, Global.MyResolution.Height - size.Height), (int)size.Width, (int)size.Height);
            }

            positionX = rectangle.X;

            speed = Randomize.Decimal(minimumSpeed, maximumSpeed);

            switch (Randomize.Integer(3))
            {
                case 0:

                    selected = new Rectangle(0, 0, (int)(cloudSize.Width / maximumCloudsType), (int)cloudSize.Height);

                    break;

                case 1:

                    selected = new Rectangle((int)(cloudSize.Width / maximumCloudsType), 0, (int)(cloudSize.Width / maximumCloudsType), (int)cloudSize.Height);
                    
                    break;

                case 2:

                    selected = new Rectangle((int)((cloudSize.Width / maximumCloudsType) * 2), 0, (int)(cloudSize.Width / maximumCloudsType), (int)(cloudSize.Height));
                    
                    break;
            }
        }

        public bool Update()
        {
            if (leftToRight == true)
            {
                if ((positionX + speed) <= Global.MyResolution.Width)
                {
                    positionX += speed;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if ((positionX - speed) >= -rectangle.Width)
                {
                    positionX -= speed;
                }
                else
                {
                    return true;
                }
            }

            rectangle.X = (int)positionX;

            return false;
        }

        public void Draw(Texture2D clouds, Color color)
        {
            Global.MySpriteBatch.Draw(clouds, rectangle, selected, color);
        }
    }
}
