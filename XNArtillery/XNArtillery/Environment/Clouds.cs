using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNArtillery
{
    public class Clouds
    {
        private enum Directions
        {
            DX,
            SX
        }

        private int counter;

        private Texture2D clouds;
        private List<Rectangle> positions;
        private List<Rectangle> selected;
        private List<Directions> directions;
        private Shade shade;
        private Color light;

        public Clouds(Color[] shadedColors)
        {
            counter = 0;
            positions = new List<Rectangle>();
            selected = new List<Rectangle>();
            directions = new List<Directions>();
            shade = new Shade(shadedColors);
        }

        private void GenerateCloud()
        {
            Point dimension = new Point(Global.Random(Global.resolution.X / 4, Global.resolution.X / 2), Global.Random(Global.resolution.Y / 4, Global.resolution.Y / 2));
            Rectangle cloudPosition;
            Rectangle selectedCloud = new Rectangle();
            Directions cloudDirection;

            counter += 1;

            if (Global.Random(2) == 0)
            {
                cloudDirection = Directions.DX;
                cloudPosition = new Rectangle(-dimension.X, Global.Random(-dimension.Y, Global.resolution.Y - dimension.Y), dimension.X, dimension.Y);
            }
            else
            {
                cloudDirection = Directions.SX;
                cloudPosition = new Rectangle(Global.resolution.X + dimension.X, Global.Random(Global.resolution.Y - dimension.Y), dimension.X, dimension.Y);
            }

            switch (Global.Random(3))
            {
                case 0:
                    selectedCloud = new Rectangle(0, 0, clouds.Width / 3, clouds.Height);
                    break;
                case 1:
                    selectedCloud = new Rectangle(clouds.Width / 3, 0, clouds.Width / 3, clouds.Height);
                    break;
                case 2:
                    selectedCloud = new Rectangle((clouds.Width / 3) * 2, 0, clouds.Width / 3, clouds.Height);
                    break;
            }

            positions.Add(cloudPosition);
            selected.Add(selectedCloud);
            directions.Add(cloudDirection);
        }

        private void RemoveCloud(int index)
        {
            positions.RemoveAt(index);
            selected.RemoveAt(index);
            directions.RemoveAt(index);
            counter -= 1;
        }

        public void LoadContent(MyGame runningGame)
        {
            clouds = runningGame.Content.Load<Texture2D>("Images/Environment/Clouds");
            GenerateCloud();
        }

        public void Update()
        {
            if (counter < 20)
                {
                    if (Global.Random(375) == 0)
                    {
                        GenerateCloud();
                    }
                }

            for (int i = 0; i < counter; i += 1)
            {
                if (directions[i] == Directions.DX)
                {
                    if (positions[i].X + 1 <= Global.resolution.X)
                    {
                        positions[i] = new Rectangle(positions[i].X + 1, positions[i].Y, positions[i].Width, positions[i].Height);
                    }
                    else
                    {
                        RemoveCloud(i);
                    }
                }
                else
                {
                    if (positions[i].X + 1 >= 0 - positions[i].Width)
                    {
                        positions[i] = new Rectangle(positions[i].X - 1, positions[i].Y, positions[i].Width, positions[i].Height);
                    }
                    else
                    {
                        RemoveCloud(i);
                    }
                }
            }

            light = shade.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < counter; i += 1)
            {
                spriteBatch.Draw(clouds, positions[i], selected[i], light);
            }
        }
    }
}
