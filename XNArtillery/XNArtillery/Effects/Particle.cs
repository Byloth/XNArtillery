using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Utility;

using ByloEngine;
using ByloEngine.Graphics;

namespace XNArtillery.Effects
{
    enum ParticlesTypes
    {
        Fire,
        Smoke,
    }

    class Particle
    {
        private const float minScale = 0.25F;
        private const float maxScale = 0.5F;
        private const float maxShift = 10;
        private const float horizontalSpeed = 0.125F;
        private const float verticalSpeed = 1F;
        private const float scalingSpeed = 0.5F;
        private const float timeIncrement = 0.025F;

        private float time;
        private float rotation;
        private float scale;
        private float startingScale;
        private float alpha;

        private Color color;
        private Vector2 position;
        private Vector2 direction;
        private Vector2 startingDirection;
        private Vector2 origin;
        private Fade fade;

        private void fire()
        {
            const float fireMinXSpeed = -0.5F;
            const float fireMaxXSpeed = 0.5F;
            const float fireMinYSpeed = -0.75F;
            const float fireMaxYSpeed = 0.75F;
            const float fireMinTTL = 75;
            const float fireMaxTTL = 125;

            int green = Randomize.Integer(0, byte.MaxValue);

            color = new Color(byte.MaxValue, green, 0);

            startingDirection.X += Randomize.Decimal(fireMinXSpeed, fireMaxXSpeed);
            startingDirection.Y += Randomize.Decimal(fireMinYSpeed, fireMaxYSpeed);

            fade.Start(FadeTypes.FadeOut, Randomize.Decimal(fireMinTTL, fireMaxTTL));
        }

        private void smoke()
        {
            const byte minGray = 32;
            const byte maxGray = 96;
            const float smokeMinTTL = 125;
            const float smokeMaxTTL = 175;

            int gray = Randomize.Integer(minGray, maxGray);

            color = new Color(gray, gray, gray);

            fade.Start(FadeTypes.FadeOut, Randomize.Decimal(smokeMinTTL, smokeMaxTTL));
        }

        public Particle(ParticlesTypes particleType, Vector2 particlePosition, Vector2 particleDirection, Vector2 particleOrigin)
        {
            float shift = maxShift * Global.MyScale;

            time = 0;

            rotation = Randomize.Angle();

            startingScale = Randomize.Decimal(minScale, maxScale) * Global.MyScale;

            particlePosition.X = Randomize.Decimal(particlePosition.X - shift, particlePosition.X + shift);
            particlePosition.Y = Randomize.Decimal(particlePosition.Y - shift, particlePosition.Y + shift);
            position = particlePosition;

            startingDirection = particleDirection;
            fade = new Fade(Fade.Visible);

            if (particleType == ParticlesTypes.Fire)
            {
                fire();
            }
            else
            {
                smoke();
            }

            direction = startingDirection;
            origin = particleOrigin;
        }

        private void updatePosition()
        {
            if (direction.X != 0)
            {
                if (direction.X < 0)
                {
                    direction.X = Physics.linearAcceleration(time, startingDirection.X, horizontalSpeed);
                }
                else
                {
                    direction.X = Physics.linearDeceleration(time, startingDirection.X, horizontalSpeed);
                }
            }

            direction.Y = Physics.linearDeceleration(time, startingDirection.Y, verticalSpeed);

            position = Functions.Add(position, direction);
        }

        public bool Update()
        {
            alpha = fade.Update();

            if (alpha <= 0)
            {
                return true;
            }

            time += timeIncrement;

            updatePosition();
            scale = Physics.linearAcceleration(time, startingScale, scalingSpeed) * Global.MyScale;

            return false;
        }

        public void Draw(Texture2D particleTexture)
        {
            Global.MySpriteBatch.Draw(particleTexture, position, null, color * alpha, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
