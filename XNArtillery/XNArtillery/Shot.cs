using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    enum ShotStates
    {
        CollidedWithLevel = 0,
        CollidedWithP1 = 1,
        CollidedWithP2 = 2,
        Ready,
        Fired
    }

    class Shot
    {
        private bool collided;
        private bool exploded;

        private float time;
        private float wind;
        private float power;
        private float angle;

        private float alpha;
        private float scale;
        private float rotation;

        private Texture2D shot;
        private Vector2 position;
        private Point dimension;
        private Point start;
        private Vector2 old;
        private Texture2D explosion;
        private Fade fade;
        private Zoom zoom;

        public Shot()
        {
            collided = false;

            start = new Point();
            old = new Vector2();

            fade = new Fade(false);
            zoom = new Zoom();
        }

        public void LoadContent(MyGame runningGame)
        {
            shot = runningGame.Content.Load<Texture2D>("Images/Shots/Bullet");
            explosion = runningGame.Content.Load<Texture2D>("Images/Effects/Explosion");

            position = new Vector2(-50, -50);
            dimension = new Point(Global.ResizeByX(shot.Width), Global.ResizeByY(shot.Height));
        }

        public void NewShot(float[] shootingValues, float windPower, Point startingPosition)
        {
            collided = false;
            exploded = false;

            time = 0;

            power = shootingValues[0];
            angle = shootingValues[1];
            wind = windPower;

            start = new Point(startingPosition.X, startingPosition.Y - Global.ResizeByY(3));
            position.X = -100;
            position.Y = -100;
        }

        public void Explode()
        {
            collided = true;
        }

        public ShotStates Update(Collider globalCollider)
        {
            if (collided == true)
            {
                if (exploded == false)
                {
                    exploded = true;
                    fade.Start(FadeStates.Out, false, 1, 75, 0);
                    zoom.Start(0.25F, 75, ZoomStates.In);
                }
                else
                {
                    alpha = fade.Update();
                    scale = zoom.Update();

                    if (alpha == 0)
                    {
                        return ShotStates.Ready;
                    }
                }

                return ShotStates.Fired;
            }
            else
            {
                float increment = 0.05F;

                ShotStates state = ShotStates.Fired;

                if ((position.Y < 0) | (position.X < 0) | (position.X > Global.resolution.X))
                {
                    increment = 0.25F;
                }

                for (double i = time; (i < (time + increment)) & (state == ShotStates.Fired); i += (increment / 10))
                {
                    position.X = (float)(start.X + (power * Math.Cos(angle) * i) - (wind * (i * i)) / 2);
                    position.Y = (float)(start.Y - (power * Math.Sin(angle) * i) + (Global.G * (i * i)) / 2);

                    state = globalCollider.CheckCollision(new Point((int)position.X, (int)position.Y));
                }

                if (time == 0)
                {
                    if (angle >= 0)
                    {
                        rotation = -angle;
                    }
                    else
                    {
                        rotation = (float)(Math.PI - angle);
                    }
                }
                else
                {
                    rotation = (float)(Math.Atan2(position.Y - old.Y, position.X - old.X));
                }

                old = new Vector2(position.X, position.Y);
                time += increment;

                return state;
            }
        }

        public Point Position()
        {
            return new Point((int)position.X, (int)position.Y);
        }

        public void DrawShot(SpriteBatch spriteBatch, Color light)
        {
            if (collided == false)
            {
                Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, dimension.X, dimension.Y);

                spriteBatch.Draw(shot, rectangle, null, light, rotation, new Vector2(shot.Width / 2, shot.Height / 2), SpriteEffects.None, 0);
            }
        }

        public void DrawExplosion(SpriteBatch spriteBatch, Color light)
        {
            if (exploded == true)
            {
                spriteBatch.Draw(explosion, position, null, Color.White * alpha, rotation, new Vector2(explosion.Width / 2, explosion.Height / 2), scale, SpriteEffects.None, 0);
            }

        }
    }
}
