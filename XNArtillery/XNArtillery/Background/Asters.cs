using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Utility;

namespace XNArtillery.Background
{
    public class Asters
    {
        private const float unit = (float)(Math.PI / 180);

        private Texture2D asters;
        private Rectangle[] position;

        public Asters()
        {
            position = new Rectangle[]
            {
                new Rectangle(),
                new Rectangle()
            };
        }

        public void LoadContent()
        {
            asters = Global.ThisGame.Content.Load<Texture2D>("Images/Background/Asters");
        }

        public void Update()
        {
            float radian = Global.MyGameTime.Value() * unit;
            int[] positionX = new int[2];
            int[] positionY = new int[2];

            positionX[0] = (int)(-Math.Cos(radian) * Global.MyResolution.Width / 2 + (Global.MyResolution.Width / 2 - Functions.ResizeByWidth(asters.Width / 4)));
            positionY[0] = (int)(-Math.Sin(radian) * Global.MyResolution.Width / 2 + (Global.MyResolution.Height - (Functions.ResizeByHeight(asters.Height) / 2)));
            positionX[1] = (int)(-Math.Cos(radian + Math.PI) * Global.MyResolution.Width / 2 + (Global.MyResolution.Width / 2 - Functions.ResizeByHeight(asters.Width / 4)));
            positionY[1] = (int)(-Math.Sin(radian + Math.PI) * Global.MyResolution.Width / 2 + (Global.MyResolution.Height - (Functions.ResizeByHeight(asters.Height) / 2)));

            position[0] = new Rectangle(positionX[0], positionY[0], Functions.ResizeByWidth(asters.Width / 2), Functions.ResizeByHeight(asters.Height));
            position[1] = new Rectangle(positionX[1], positionY[1], Functions.ResizeByWidth(asters.Width / 2), Functions.ResizeByHeight(asters.Height));
        }

        public void Draw()
        {
            Global.MySpriteBatch.Draw(asters, position[0], new Rectangle(0, 0, asters.Width / 2, asters.Height), Color.White);
            Global.MySpriteBatch.Draw(asters, position[1], new Rectangle(asters.Width / 2, 0, asters.Width / 2, asters.Height), Color.White);
        }
    }
}
