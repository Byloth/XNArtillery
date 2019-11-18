using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XNArtillery.Engine;
using XNArtillery.Utility;

using ByloEngine;

namespace XNArtillery.Game
{
    class Human : Player
    {
        private const int powerRiducer = 4;
        private const int vibrateDuration = 1000; 
        private const float multiplier = 100;
        private const float maxSpeed = 1;

        private bool doVibration;
        private bool visible;
        private bool pressedByKeyboard;
        private bool pressedByJoystick;
        private int maxWidth;

        private PlayerIndex gamePadIndex;
        private MouseState mouseState;
        private GamePadState gamePadState;
        private Vector2 playerPosition;
        private StaticTexture2D shotBar;
        private Thread vibration;
        private ManualResetEvent flag;


        public Human(int playerIndex)
            : base(playerIndex)
        {
            doVibration = true;

            shotBar = new StaticTexture2D();
            gamePadIndex = PlayerIndex.One;
            vibration = new Thread(new ThreadStart(vibrate));
            vibration.Start();

            flag = new ManualResetEvent(true);
        }

        public override void Position(Vector2[] playersPositions)
        {
            playerPosition = playersPositions[0];

            base.position(playerPosition);

            playerPosition.Y -= base.texture.Destination.Height;
            shotBar.Position(AnchorageTypes.CenterLeft, playerPosition);
        }

        public override void LoadContent(int selectedTower, int selectedShot)
        {
            Size textureSize = shotBar.LoadContent("Effects/ShotBars/ShotBar" + selectedShot);

            maxWidth = (int)textureSize.Width;

            base.loadContent(selectedTower, selectedShot);
        }

        private float checkRotationByMouse(Vector2 mousePosition)
        {
            float rotation = (float)Math.Atan2(mousePosition.Y, mousePosition.X);

            if (mousePosition.X >= 0)
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

            return rotation;
        }

        private float checkWidthByMouse(float rotation, Vector2 mousePosition)
        {
            float width;

            if ((rotation != 0) && (rotation != (float)Math.PI))
            {
                width = Mathematics.Hypotenuse(mousePosition.X, mousePosition.Y);
            }
            else
            {
                width = Math.Abs(mousePosition.X);
            }

            if (width > maxWidth)
            {
                width = maxWidth;
            }

            return width;
        }

        private void assignValues(float rotation, float width)
        {
            shotBar.Rotation = rotation;
            shotBar.Destination.Width = (int)width;
            shotBar.Selected.Width = (int)Functions.SizeByWidth(width);
        }

        private bool generateByMouse(float[] shootingValues, float windPower)
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float rotation;
                float width;

                Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

                mousePosition.X -= playerPosition.X;
                mousePosition.Y -= playerPosition.Y;

                rotation = checkRotationByMouse(mousePosition);
                width = checkWidthByMouse(rotation, mousePosition);

                assignValues(rotation, width);

                pressedByKeyboard = true;

                return true;
            }
            else if (pressedByKeyboard == true)
            {
                shoot(shootingValues);

                return true;
            }
            else
            {
                return false;
            }
        }

        private float checkRotationByJoystick()
        {
            Vector2 stickPosition = gamePadState.ThumbSticks.Left;

            stickPosition.Y = -stickPosition.Y;

            return checkRotationByMouse(stickPosition);
        }

        private float checkWidthByJoystick()
        {
            float width = maxWidth * gamePadState.Triggers.Right;
            float additionalWidth = multiplier * gamePadState.Triggers.Left;

            width += additionalWidth;

            if (width > maxWidth)
            {
                width = maxWidth;
            }

            return width;
        }

        private void generateByPad(float[] shootingValues, float windPower)
        {
            float rotation;
            float width;

            gamePadState = GamePad.GetState(gamePadIndex);

            rotation = checkRotationByJoystick();
            width = checkWidthByJoystick();

            assignValues(rotation, width);

            pressedByJoystick = true;

            if (gamePadState.Buttons.A ==  ButtonState.Pressed)
            {
                shoot(shootingValues);
            }
        }

        private void shoot(float[] shootingValues)
        {
            shootingValues[0] = (float)((2 * Math.PI) - shotBar.Rotation);
            shootingValues[1] = shotBar.Destination.Width / powerRiducer;

            Mouse.SetPosition((int)playerPosition.X, (int)playerPosition.Y);

            visible = false;

            pressedByKeyboard = false;
            pressedByJoystick = false;
        }

        protected override float[] generateShot(float windPower)
        {
            float[] shootingValues = new float[2]
            {
                float.NaN,
                float.NaN
            };

            if (generateByMouse(shootingValues, windPower) == false)
            {
                generateByPad(shootingValues, windPower);
            }

            return shootingValues;
        }

        private void vibrate()
        {
            do
            {
                flag.WaitOne(1000);

                if (doVibration == true)
                {
                    GamePad.SetVibration(gamePadIndex, maxSpeed, maxSpeed);
                    Thread.Sleep(vibrateDuration / 4);
                    GamePad.SetVibration(gamePadIndex, maxSpeed / 2, maxSpeed / 2);
                    Thread.Sleep(vibrateDuration / 4);
                    GamePad.SetVibration(gamePadIndex, maxSpeed / 4, maxSpeed / 4);
                    Thread.Sleep(vibrateDuration / 4);
                    GamePad.SetVibration(gamePadIndex, maxSpeed / 8, maxSpeed / 8);
                    Thread.Sleep(vibrateDuration / 4);
                    GamePad.SetVibration(gamePadIndex, 0, 0);

                    doVibration = false;
                }

                flag.Reset();
            }
            while (Global.IsRunning == true);
        }

        public override void Collided(Collider sender, CollisionArgs e)
        {
            doVibration = true;
            flag.Set();

            base.collided(e.Power, e.Position);
        }

        public override void ItsYourTurn()
        {
            visible = true;
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
            if (((pressedByKeyboard == true) | (pressedByJoystick == true)) & (visible == true))
            {
                shotBar.Draw(Color.White);
            }

            base.shot.DrawExplosion();
        }
    }
}
