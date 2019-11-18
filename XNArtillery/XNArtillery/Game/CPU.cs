using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Game
{
    class CPU : Player
    {
        private const float generationProbability = 1;
        private const float minimumTimeValue = 5;
        private const float maximumTimeValue = 25;

        private int ability;

        private Limits timeValues;
        private Vector2[] positions;

        public CPU(int playerIndex, int playerAbility)
            : base(playerIndex)
        {
            ability = playerAbility;

            timeValues = new Limits(minimumTimeValue, maximumTimeValue);

            positions = new Vector2[2]
            {
                new Vector2(),
                new Vector2()
            };
        }

        public override void Position(Vector2[] playersPositions)
        {
            Rectangle rectangle = base.position(playersPositions[0]);

            positions[0] = playersPositions[0];
            positions[1] = playersPositions[1];
            positions[1].Y += (float)rectangle.Height / 2;
        }

        public override void LoadContent(int selectedTower, int selectedShot)
        {
            base.loadContent(selectedTower, selectedShot);
        }

        protected override float[] generateShot(float windPower)
        {
            float[] shootingValues = new float[2]
            {
                float.NaN,
                float.NaN
            };

            if (Randomize.Boolean(generationProbability) == true)
            {
                if (Randomize.Boolean(ability) == true)
                {
                    float time = Randomize.Decimal(timeValues);

                    float shootingPower = ((windPower * (time * time) / 2) + (positions[1].X - positions[0].X)) / time;
                    float shootingAngle = ((Physics.G * (time * time) / 2) - (positions[1].Y - positions[0].Y)) / time;

                    shootingAngle /= shootingPower;
                    shootingAngle = (float)Math.Atan(shootingAngle);

                    shootingPower /= (float)Math.Cos(shootingAngle);

                    shootingValues = new float[2]
                    {
                        shootingAngle,
                        shootingPower
                    };
                }
                else if (positions[0].X > positions[1].Y)
                {
                    return new float[2]
                    {
                        (float)MathHelper.ToRadians(Randomize.Decimal(91, 179)), 
                        (float)Functions.ResizeByWidth(Randomize.Decimal(100, 250))
                    };
                }
                else
                {
                    return new float[2]
                    {
                        (float)MathHelper.ToRadians(Randomize.Decimal(1, 89)), 
                        (float)Functions.ResizeByWidth(Randomize.Decimal(100, 250))
                    };
                }
            }

            return shootingValues;
        }

        public override void Collided(Collider sender, CollisionArgs e)
        {
            base.collided(e.Power, e.Position);
        }

        public override void ItsYourTurn()
        {
        }

        public override void DrawInBackground()
        {
            base.shot.DrawShot();
        }

        public override void DrawInTheMiddle()
        {
            base.Draw();
        }

        public override void DrawInForeground()
        {
            base.shot.DrawExplosion();
        }
    }
}
