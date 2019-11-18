using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    enum MatchStates
    {
        Paused,
        Resumed,
        Finished,
    }

    class Match
    {
        private bool influenced;
        private bool generated;
        private int turn;
        private int windPower;
        private string name;
        private float wind;

        private MatchStates state;
        private ShotStates shotState;
        private Level level;
        private Player[] players;
        private Point[] positions;
        private Shot shot;
        private Collider collider;
        private MatchBoard board;
        private PauseBoard pause;
        private WinnerBoard winner;

        public Match()
        {
            influenced = true;
            generated = false;
            wind = 0;

            state = MatchStates.Resumed;
            shotState = ShotStates.Ready;
            level = new Level();
            players = new Player[2];
            shot = new Shot();
            collider = new Collider();
            board = new MatchBoard();
            pause = new PauseBoard();
            winner = new WinnerBoard();
        }

        public void LoadContent(MyGame runningGame, MatchSettings matchSettings)
        {
            turn = matchSettings.turn;

            if (matchSettings.windInfluence == 0)
            {
                influenced = false;
            }

            windPower = matchSettings.windInfluence;

            name = matchSettings.opponentName;

            collider.LoadContent(runningGame, false);
            positions = level.LoadContent(runningGame, 1, collider);

            if ((matchSettings.matchType == MatchTypes.PvC) | (matchSettings.matchType == MatchTypes.PvP))
            {
                players[0] = new Human();
            }
            else if (matchSettings.matchType == MatchTypes.Lan)
            {
                players[0] = new Human(matchSettings.opponentEndPoint);
            }
            else
            {
                players[0] = new CPU(matchSettings.CPUIntelligence);
            }

            if ((matchSettings.matchType == MatchTypes.PvC) | (matchSettings.matchType == MatchTypes.CvC))
            {
                players[1] = new CPU(matchSettings.CPUIntelligence);
            }
            else if (matchSettings.matchType == MatchTypes.Lan)
            {
                players[1] = new Remote(matchSettings.opponentEndPoint);
            }
            else
            {
                players[1] = new Human();
            }

            positions = new Point[2]
            {
                players[0].LoadContent(runningGame, 1, positions, collider),
                players[1].LoadContent(runningGame, 1, new Point[2] { positions[1], positions[0] }, collider)
            };

            shot.LoadContent(runningGame);

            board.LoadContent(runningGame, influenced, matchSettings.opponentName);
            pause.LoadContent(runningGame);
            winner.LoadContent(runningGame);
        }

        private void Finish(string winnerName)
        {
            state = MatchStates.Finished;
            winner.Active(winnerName);
        }

        public GameStates Update(MouseState mouseState)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (state == MatchStates.Resumed)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) == true)
                {
                    state = MatchStates.Paused;
                }

                if (shotState == ShotStates.Ready)
                {
                    float[] shootingValues;

                    if ((generated == false) & (influenced == true))
                    {
                        generated = true;

                        wind = Global.Random(-windPower, windPower) / 100;
                        board.NewWindPower(wind);
                    }

                    shootingValues = players[turn].Update(true, wind);

                    if (float.IsNaN(shootingValues[0]) == false)
                    {
                        shotState = ShotStates.Fired;

                        shot.NewShot(shootingValues, wind, positions[turn]);

                        if (turn == 0)
                        {
                            turn = 1;
                        }
                        else
                        {
                            turn = 0;
                        }
                    }
                }
                else
                {
                    players[0].Update(false, wind);
                    players[1].Update(false, wind);

                    shotState = shot.Update(collider);

                    if ((shotState != ShotStates.Fired) & (shotState != ShotStates.Ready))
                    {
                        generated = false;

                        switch (shotState)
                        {
                            case ShotStates.CollidedWithLevel:
                                {
                                    level.Hit(shot.Position());

                                    break;
                                }
                            case ShotStates.CollidedWithP1:
                                {
                                    players[0].Hit(shot.Position());

                                    if (board.Hit(0) == true)
                                    {
                                        Finish(name);
                                    }

                                    break;
                                }
                            case ShotStates.CollidedWithP2:
                                {
                                    players[1].Hit(shot.Position());

                                    if (board.Hit(1) == true)
                                    {
                                        Finish(Global.nickname);
                                    }
                                    break;
                                }
                        }

                        shot.Explode();
                    }
                }

                board.Update();
                pause.Update(false, mouseState);
            }
            else if (state == MatchStates.Paused)
            {
                PauseStates pauseState = pause.Update(true, mouseState);

                if (pauseState == PauseStates.Exit)
                {
                    return GameStates.Menu;
                }
                else if (pauseState == PauseStates.Restart)
                {
                    return GameStates.NewMatch;
                }
                else if (pauseState == PauseStates.Resume)
                {
                    state = MatchStates.Resumed;
                }
            }
            else
            {
                MatchOptions options = winner.Update(mouseState);

                if (shotState != ShotStates.Ready)
                {
                    shotState = shot.Update(collider);
                }
                board.Update();

                if (options == MatchOptions.New)
                {
                    return GameStates.NewMatch;
                }
                else if (options == MatchOptions.Exit)
                {
                    return GameStates.Menu;
                }
            }

            return GameStates.Match;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            shot.DrawShot(spriteBatch, light);

            level.Draw(spriteBatch, light);

            players[0].Draw(spriteBatch, light);
            players[1].Draw(spriteBatch, light);

            collider.Draw(spriteBatch);

            shot.DrawExplosion(spriteBatch, light);

            board.Draw(spriteBatch, light);
            pause.Draw(spriteBatch, light);
            winner.Draw(spriteBatch, light);
        }
    }
}
