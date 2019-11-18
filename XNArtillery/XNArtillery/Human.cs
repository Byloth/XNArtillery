using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine.Net;

namespace XNArtillery
{
    class Human : Player
    {
        private bool pressed;
        private int count;
        private float rotation;

        private Texture2D bar;
        private Rectangle position;
        private Rectangle selected;
        private UDP udp;
        private IPEndPoint opponent;

        public Human()
        {
            pressed = false;
            count = 0;
            udp = new UDP(Global.client, Global.port);
            opponent = null;
        }

        public Human(IPEndPoint opponentEndPoint)
        {
            pressed = false;
            count = 0;
            udp = new UDP(Global.client, Global.port);
            opponent = opponentEndPoint;
        }

        public override Point LoadContent(MyGame runningGame, int towerNumber, Point[] playersPositions, Collider globalCollision)
        {
            Point dimension = base.loadContent(runningGame, towerNumber, playersPositions[0], globalCollision);

            bar = runningGame.Content.Load<Texture2D>("Images/Effects/ShotBar");
            position = new Rectangle(playersPositions[0].X, playersPositions[0].Y - dimension.Y, Global.ResizeByX(bar.Width), Global.ResizeByY(bar.Height));
            selected = new Rectangle(0, 0, bar.Width, bar.Height);

            return new Point(position.X, position.Y);
        }

        public override float[] Update(bool newShot, float windPower)
        {
            if (newShot == true)
            {
                MouseState mouseState = Mouse.GetState();
                KeyboardState keyboardState = Keyboard.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    int X = mouseState.X - position.X;
                    int Y = mouseState.Y - position.Y;

                    int power = (int)Math.Sqrt((X * X) + (Y * Y));

                    pressed = true;

                    if (Global.SizeByX(power) > bar.Width)
                    {
                        power = Global.ResizeByX(bar.Width);
                    }

                    position.Width = power;
                    selected.Width = Global.SizeByX(power);

                    if (keyboardState.IsKeyDown(Keys.LeftShift) == false)
                    {
                        rotation = (float)Math.Atan2(Y, X);

                        if (X >= 0)
                        {
                            if (rotation > 0)
                            {
                                rotation = 0;
                            }
                        }
                        else
                        {
                            if (rotation > MathHelper.PiOver2)
                            {
                                rotation = (float)Math.PI;
                            }
                        }
                    }
                    else
                    {
                        rotation = (float)(Math.PI - Math.Atan2(Y, X));

                        if (X >= 0)
                        {
                            if (rotation < Math.PI)
                            {
                                rotation = (float)Math.PI;
                            }
                        }
                        else
                        {
                            if (rotation < MathHelper.PiOver2)
                            {
                                rotation = 0;
                            }
                        }
                    }
                }
                else if (pressed == true)
                {
                    float[] shootingValues;

                    pressed = false;

                    Mouse.SetPosition(Global.resolution.X / 2, Global.resolution.Y / 2);

                    shootingValues = new float[2]{position.Width / 3, (float)((2 * Math.PI) - rotation)};

                    if (opponent != null)
                    {
                        udp.Send(new Packet(PacketTypes.IShot, shootingValues), opponent);
                    }

                    return shootingValues;
                }
            }

            if (opponent != null)
            {
                count += 1;

                if (count >= 1000)
                {
                    count = 0;

                    udp.Send(new Packet(PacketTypes.IMAlive), opponent);
                }
            }

            return new float[2]{float.NaN, float.NaN};
        }

        public override void Draw(SpriteBatch spriteBatch, Color light)
        {
            base.draw(spriteBatch, light);

            if (pressed == true)
            {
                spriteBatch.Draw(bar, position, selected, Color.White, rotation, new Vector2(15, 51), SpriteEffects.None, 0);
            }
        }
    }
}
