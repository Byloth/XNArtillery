using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNArtillery
{
    class MessageBoard : Board
    {
        private bool active;

        private Label label;

        public MessageBoard()
        {
            active = false;

            label = new Label();
        }

        public void LoadContent(MyGame runningGame)
        {
            base.loadContent(runningGame, "GenericBoard", Global.ResizeByX(925), new int[2] { Global.ResizeByY(-1125), Global.ResizeByY(-74) });

            label.LoadContent(runningGame, "Button");
        }

        public void Deactive()
        {
            active = false;
        }

        public void Active(string message)
        {
            Point dimension = Dimension();

            active = true;
            label.NewText(message, Global.Resize(0, 725), new Point(dimension.X, 0));
        }

        public void Update()
        {
            base.update(false);
        }

        public void Update(bool isActive)
        {
            if (active == true)
            {
                base.update(isActive);
            }
            else
            {
                Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            Point position = base.draw(spriteBatch, light);

            label.Draw(spriteBatch, position, Color.White);
        }
    }
}
