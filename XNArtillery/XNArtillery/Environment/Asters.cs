using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    public class Asters
    {
        private const double UNIT = (Math.PI / 180);

        private Texture2D asters;
        private Rectangle[] position;

        public Asters()
        {
            Global.time = 0;
            position = new Rectangle[2];
        }

        public void LoadContent(MyGame runningGame)
        {
            asters = runningGame.Content.Load<Texture2D>("Images/Environment/Asters");
        }

        public void Update()
        {
            double radian = Global.time * UNIT;
            double[] positionX = new double[2];
            double[] positionY = new double[2];

            positionX[0] = -Math.Cos(radian) * Global.resolution.X / 2 + (Global.resolution.X / 2 - Global.ResizeByX(asters.Width / 4));
            positionY[0] = -Math.Sin(radian) * Global.resolution.X / 2 + (Global.Horizon() - (Global.ResizeByY(asters.Height) / 2));
            positionX[1] = -Math.Cos(radian + Math.PI) * Global.resolution.X / 2 + (Global.resolution.X / 2 - Global.ResizeByX(asters.Width / 4));
            positionY[1] = -Math.Sin(radian + Math.PI) * Global.resolution.X / 2 + (Global.Horizon() - (Global.ResizeByY(asters.Height) / 2));

            position[0] = new Rectangle((int)positionX[0], (int)positionY[0], Global.ResizeByX(asters.Width / 2), Global.ResizeByY(asters.Height));
            position[1] = new Rectangle((int)positionX[1], (int)positionY[1], Global.ResizeByX(asters.Width / 2), Global.ResizeByY(asters.Height));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(asters, position[0], new Rectangle(0, 0, asters.Width / 2, asters.Height), Color.White);
            spriteBatch.Draw(asters, position[1], new Rectangle(asters.Width / 2, 0, asters.Width / 2, asters.Height), Color.White);
        }
    }
}
