using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class CPU : Player
    {
        private int intelligence;

        private Point timeValues;
        private Point[] positions;

        public CPU(int CPUIntelligence)
        {
            intelligence = 5 - CPUIntelligence;

            timeValues = new Point(4, 25);
        }

        public override Point LoadContent(MyGame runningGame, int towerNumber, Point[] playersPositions, Collider globalCollider)
        {
            Point dimension = base.loadContent(runningGame, towerNumber, playersPositions[0], globalCollider);

            positions = new Point[2] { new Point(playersPositions[0].X, playersPositions[0].Y - dimension.Y), new Point(playersPositions[1].X, playersPositions[1].Y - (dimension.Y / 2)) };

            return positions[0];
        }
        
        public override float[] Update(bool newShot, float windPower)
        {
            if (newShot == true)
            {
                if (Global.Random(intelligence) == 0)
                {
                    float time = Global.Random(timeValues.X, timeValues.Y);

                    double shootingPower = 0;
                    double shootingAngle = 0;
                    double cosine;

                    shootingPower = (windPower * (time * time) / 2) + (positions[1].X - positions[0].X);
                    shootingAngle = (Global.G * (time * time) / 2) - (positions[1].Y - positions[0].Y);

                    shootingPower /= time;
                    shootingAngle /= time;

                    shootingAngle /= shootingPower;
                    shootingAngle = Math.Atan(shootingAngle);

                    cosine = Math.Cos(shootingAngle);

                    shootingPower = shootingPower / cosine;

                    return new float[2] { (float)shootingPower, (float)shootingAngle };
                }
                else if (positions[0].X > positions[1].Y)
                {
                    return new float[2] { (float)Global.ResizeByX(Global.Random(10000, 25000)) / 100, (float)MathHelper.ToRadians(Global.Random(9000, 18100) / 100) };
                }
                else
                {
                    return new float[2] { (float)Global.ResizeByX(Global.Random(10000, 25000)) / 100, (float)MathHelper.ToRadians(Global.Random(9100) / 100) };
                }
            }
            else
            {
                return new float[2] { float.NaN, float.NaN };
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color light)
        {
            base.draw(spriteBatch, light);
        }
    }
}
