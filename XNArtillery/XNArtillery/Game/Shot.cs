using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XNArtillery.Effects;
using XNArtillery.Engine;
using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Game
{
    public class Shot
    {
        private const int precision = 10;
        private const float timeIncrement = 0.05F;
        private const float positionIncrement = 2.5F;
        private const float divisor = 5;

        private bool fired;
        private bool collided;
        private float time;
        private float angle;
        private float power;
        private float wind;

        private Vector2 startingPosition;
        private Vector2 oldPosition;
        private StaticTexture2D shot;
        private Sound sound;
        private Explosion explosion;
        private FireParticles fire;

        public Shot()
        {
            fired = false;
            collided = false;

            shot = new StaticTexture2D();
            sound = new Sound();
            explosion = new Explosion();
            fire = new FireParticles();
        }

        public void StartingPosition(Vector2 shootingPosition)
        {
            startingPosition = shootingPosition;
            startingPosition.Y -= Functions.ResizeByWidth(positionIncrement);
            sound.Pan(shootingPosition);
        }

        public void LoadContent(int selectedShot)
        {
            shot.LoadContent("Game/Shots/Shot" + selectedShot, AnchorageTypes.Center);
            sound.LoadContent("Effects/Shots/Shot" + selectedShot); 
            fire.LoadContent();
            explosion.LoadContent();
        }

        public void Fire(float[] shootingValues, float windPower)
        {
            fired = true;
            collided = false;
            time = 0;
            angle = shootingValues[0];
            power = shootingValues[1];
            wind = windPower;

            oldPosition = startingPosition;

            shot.Position(startingPosition);
            shot.Rotation = (float)- angle;
            sound.Play();
            fire.Start();

            Global.MyCollider.NewShot = false;
        }

        private float checkPosition()
        {
            Vector2 position = shot.Position();

            return timeIncrement;
        }

        private Vector2 nextPoint()
        {
            float increment = checkPosition();
            float[] point = new float[2];

            for (float i = time; (i < (time + increment)) & (collided == false); i += (increment / precision))
            {
                point = Physics.ParabolicTrajectory(i, angle, power, wind);

                point[0] = startingPosition.X + point[0];
                point[1] = startingPosition.Y - point[1];

                collided = Global.MyCollider.CheckCollision(new Vector2(point[0], point[1]));
            }

            time += increment;

            return new Vector2(point[0], point[1]);
        }

        public void Update()
        {
            if (fired == true)
            {
                if (collided == false)
                {
                    Vector2 position = nextPoint();
                    Vector2 direction;

                    shot.Position(position);

                    direction = new Vector2(position.X - oldPosition.X, position.Y - oldPosition.Y);
                    shot.Rotation = (float)Math.Atan2(direction.Y, direction.X);

                    direction = new Vector2(-direction.X / divisor, -direction.Y / divisor);

                    fire.Update(position, direction);

                    oldPosition = position;
                }
                else
                {
                    fired = false;

                    fire.Stop();

                    explosion.Explode(shot.Position());
                }
            }
            else
            {
                fire.Update(shot.Position());
            }

            explosion.Update();
        }

        public void DrawShot()
        {
            fire.Draw();

            if (fired == true)
            {
                shot.Draw();
            }
        }

        public void DrawExplosion()
        {
            explosion.Draw();
        }
    }
}
