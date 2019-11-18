using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNArtillery.Effects
{
    abstract class Particles
    {
        protected const int multiplier = 3;

        protected bool isGenerating;

        private Texture2D texture;
        protected Vector2 origin;
        protected List<Particle> particles;

        public Particles()
        {
            particles = new List<Particle>();
        }

        protected void loadContent(string particlesName)
        {
            texture = Global.ThisGame.Content.Load<Texture2D>("Images/Effects/Particles/" + particlesName);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        abstract public void LoadContent();

        abstract public void Start();
        abstract public void Stop();

        protected void update()
        {
            for (int i = 0; i < particles.Count; i += 1)
            {
                if (particles[i].Update() == true)
                {
                    particles.RemoveAt(i);
                }
            }
        }

        public void Update(Vector2 particlesPosition)
        {
            Update(particlesPosition, new Vector2());
        }

        abstract public void Update(Vector2 particlesPosition, Vector2 particlesDirection);

        protected void draw()
        {
            for (int i = particles.Count - 1; i >= 0; i -= 1)
            {
                particles[i].Draw(texture);
            }
        }

        abstract public void Draw();
    }
}
