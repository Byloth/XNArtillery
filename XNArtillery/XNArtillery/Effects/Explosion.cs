using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

using XNArtillery.Engine;

using ByloEngine;

namespace XNArtillery.Effects
{
    class Explosion
    {
        private const int minFires = 5;
        private const int maxFires = 10;

        private bool exploded;

        private Vector2 position;
        private List<FireParticles> fires;
        private Thread countdown;
        private ManualResetEvent flag;
        private Sound sound;

        public Explosion()
        {
            exploded = false;

            fires = new List<FireParticles>();
            countdown = new Thread(new ThreadStart(count));
            countdown.Start();
            flag = new ManualResetEvent(false);
            sound = new Sound();
        }

        public void LoadContent()
        {
            sound.LoadContent("Effects/Explosions/Explosion1");
        }

        private void generate(Vector2 explosionPosition)
        {
            int max = Randomize.Integer(minFires, maxFires);

            for (int i = 0; i < max; i += 1)
            {
                fires.Add(new FireParticles());
                fires[i].LoadContent();
                fires[i].Start(explosionPosition);
            }
        }

        public void Explode(Vector2 explosionPosition)
        {
            position = explosionPosition;

            generate(explosionPosition);

            exploded = true;
            flag.Set();
            sound.Play(explosionPosition);
        }

        private void count()
        {
            do
            {
                flag.WaitOne(1000);

                if (exploded == true)
                {
                    Thread.Sleep(500);

                    foreach (FireParticles fire in fires)
                    {
                        fire.Stop();
                    }

                    exploded = false;
                    flag.Reset();
                }
            }
            while (Global.IsRunning == true);
        }

        public void Update()
        {
            foreach (FireParticles fire in fires)
            {
                fire.Update();
            }
        }

        public void Draw()
        {
            foreach (FireParticles fire in fires)
            {
                fire.Draw();
            }
        }
    }
}
