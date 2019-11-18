using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNArtillery.Engine;
using XNArtillery.Effects;

namespace XNArtillery.Game
{
    abstract public class Player : Destroyable
    {
        protected int index;

        protected Shot shot;

        public event ShootHandler Shoot;

        public Player(int playerIndex)
        {
            index = playerIndex;

            shot = new Shot();
        }

        public delegate void ShootHandler(Player sender, EventArgs e);

        protected Rectangle position(Vector2 playerPosition)
        {
            Rectangle rectangle = base.texture.Position(AnchorageTypes.BottomCenter, playerPosition);

            base.loadCollision();

            shot.StartingPosition(new Vector2(rectangle.X, rectangle.Y - rectangle.Height));

            return rectangle;
        }

        abstract public void Position(Vector2[] playersPositions);

        protected void loadContent(int selectedTower, int selectedShot)
        {
            texture.LoadContent("Game/Towers/Tower" + selectedTower);

            shot.LoadContent(selectedShot);
        }

        abstract public void LoadContent(int selectedTower, int selectedShot);

        abstract protected float[] generateShot(float windPower);

        public void Update(int turnIndex, float windPower)
        {
            shot.Update();

            if ((index == turnIndex) & (Global.MyCollider.NewShot == true))
            {
                float[] shootingValues = generateShot(windPower);

                if (float.IsNaN(shootingValues[0]) == false)
                {
                    shot.Fire(shootingValues, windPower);
                    //Shoot(this, null);
                }
            }
        }

        abstract public void ItsYourTurn();

        abstract public void DrawInBackground();
        abstract public void DrawInTheMiddle();
        abstract public void DrawInForeground();
    }
}
