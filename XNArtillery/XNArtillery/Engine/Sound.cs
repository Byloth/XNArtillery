using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using ByloEngine;

namespace XNArtillery.Engine
{
    class Sound
    {
        private const float standardPan = 0.9F;
        private const float maximumPan = 1;
        private const float standardVolume = 1;
        private const float standardPitch = 0;

        private float pan;

        private SoundEffect sound;

        public Sound()
        {
        }

        public void Pan(Vector2 soundPosition)
        {
            if (soundPosition.X >= (Global.MyResolution.Width / 2))
            {
                if (soundPosition.X <= Global.MyResolution.Width)
                {
                    pan = Mathematics.Proportion(Global.MyResolution.Width, standardPan, soundPosition.X);
                }
                else
                {
                    pan = maximumPan;
                }
            }
            else
            {
                if (soundPosition.X >= 0)
                {
                    soundPosition.X = (Global.MyResolution.Width / 2) + ((Global.MyResolution.Width / 2) - soundPosition.X);

                    pan = -Mathematics.Proportion(Global.MyResolution.Width, standardPan, soundPosition.X);
                }
                else
                {
                    pan = -maximumPan;
                }
            }
        }

        public void LoadContent(string pathName)
        {
            pan = 0;

            sound = Global.ThisGame.Content.Load<SoundEffect>("Sounds/" + pathName);
        }

        public void Play()
        {
            sound.Play(standardVolume, standardPitch, pan);
        }

        public void Play(Vector2 soundPosition)
        {
            Pan(soundPosition);

            sound.Play(standardVolume, standardPitch, pan);
        }
    }
}
