using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using XNArtillery.Engine;

namespace XNArtillery.Game
{
    class Match
    {
        private int turn;

        private Level level;
        private Player[] players;

        public Match()
        {
            level = new Level();

            Global.MyCollider = new Collider();
        }

        private void setPlayersPositions(int[] horizons)
        {
            Vector2[] playersPositions = new Vector2[2]
            {
                new Vector2(Global.MyResolution.Width / 8, horizons[0]),
                new Vector2(Global.MyResolution.Width - (Global.MyResolution.Width / 8), horizons[1])
            };

            players[0].Position(playersPositions);
            players[1].Position(new Vector2[2] { playersPositions[1], playersPositions[0] });
        }

        public void LoadContent(int selectedLevel, int startingPlayer, Player[] partecipatingPlayers)
        {
            Global.MyCollider.LoadContent(null, Engine.AnchorageTypes.TopLeft);
            Global.MyCollider.Collided += new Collider.CollisionHandler(changeTurn);

            turn = startingPlayer;
            players = partecipatingPlayers;

            setPlayersPositions(level.LoadContent(selectedLevel));

            players[0].ItsYourTurn();
        }

        private void changeTurn(Collider sender, EventArgs e)
        {
            turn = (int)Math.Abs(turn - 1);

            players[turn].ItsYourTurn();
        }

        public void Update()
        {
            players[0].Update(turn, 0);
            players[1].Update(turn, 0);
        }

        private void drawBackground()
        {
            players[0].DrawInBackground();
            players[1].DrawInBackground();
        }

        private void drawMiddle()
        {
            players[0].DrawInTheMiddle();
            players[1].DrawInTheMiddle();
        }

        private void drawForeground()
        {
            players[0].DrawInForeground();
            players[1].DrawInForeground();
        }

        public void Draw()
        {
            drawBackground();
            level.Draw();
            drawMiddle();
            drawForeground();

            Global.MyCollider.Draw();
        }
    }
}
