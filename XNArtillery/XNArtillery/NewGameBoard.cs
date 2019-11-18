using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNArtillery
{
    enum OptionsStates
    {
        Main,
        Single,
        Multi,
        LAN,
        Demo
    }

    class NewGameBoard : Board
    {
        private OptionsStates state;
        private Hypertext[] hypertextes;
        private TextBox textBox;
        private TrackBar[] trackBars;
        private Matcher matcher;
        private Selector selector;
        private MatchSettings settings;

        public NewGameBoard()
        {
            state = OptionsStates.Main;

            hypertextes = new Hypertext[5]
            {
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext()
            };

            textBox = new TextBox();
            trackBars = new TrackBar[2]
            {
                new TrackBar(),
                new TrackBar()
            };

            matcher = new Matcher();
            selector = new Selector();

            settings = new MatchSettings();
        }

        public void LoadContent(MyGame runningGame)
        {
            Point dimension = base.loadContent(runningGame, "MenuBoard", Global.ResizeByX(74), new int[2] { Global.ResizeByY(-1375), Global.ResizeByY(-74) });

            hypertextes[0].LoadContent(runningGame, true, "Giocatore singolo", Global.Resize(76, 514));
            hypertextes[1].LoadContent(runningGame, true, "Multigiocatore", Global.Resize(121, 654));
            //hypertextes[2].LoadContent(runningGame, true, "Rete locale", Global.Resize(175, 794));
            hypertextes[3].LoadContent(runningGame, true, "CPU vs CPU (Demo)", Global.Resize(76, 794)/*Global.Resize(76, 944)*/);
            hypertextes[4].LoadContent(runningGame, false, "BoldLabel", "Inizia la partita!", Global.Resize(552, 1220));
            textBox.LoadContent(runningGame, "Nickname:", Global.Resize(512, 212));
            trackBars[0].LoadContent(runningGame, "Influenza vento:", 1000, Global.Resize(700, 428));
            trackBars[1].LoadContent(runningGame, "Difficolta':", 5, Global.Resize(700, 568));
            selector.LoadContent(runningGame, Global.Resize(700, 600));
            matcher.LoadContent(runningGame, Global.Resize(675, 412));
        }

        public void StopThreads()
        {
            matcher.StopThreads();
        }

        public MatchSettings Update()
        {
            base.update(false);

            hypertextes[0].Update();
            hypertextes[1].Update();
            //hypertextes[2].Update();
            hypertextes[3].Update();
            hypertextes[4].Update();
            textBox.Update();
            trackBars[0].Update();
            trackBars[1].Update();
            selector.Update();
            matcher.Update();

            return new MatchSettings();
        }

        public MatchSettings Update(bool isActive, MouseState mouseState, KeyboardState keyboardState)
        {
            Point position = base.update(isActive);

            if (hypertextes[0].Update(isActive, mouseState, position) == true)
            {
                if (state == OptionsStates.Single)
                {
                    state = OptionsStates.Main;
                }
                else
                {
                    state = OptionsStates.Single;
                    hypertextes[1].Release();
                    //hypertextes[2].Release();
                    hypertextes[3].Release();
                }
            }
            else if (hypertextes[1].Update(isActive, mouseState, position) == true)
            {
                if (state == OptionsStates.Multi)
                {
                    state = OptionsStates.Main;
                }
                else
                {
                    state = OptionsStates.Multi;
                    hypertextes[0].Release();
                    //hypertextes[2].Release();
                    hypertextes[3].Release();
                }
            }
            /*else if (hypertextes[2].Update(isActive, mouseState, position) == true)
            {
                if (state == OptionsStates.LAN)
                {
                    state = OptionsStates.Main;
                }
                else
                {
                    state = OptionsStates.LAN;
                    hypertextes[0].Release();
                    hypertextes[1].Release();
                    hypertextes[3].Release();
                }
            }*/
            else if (hypertextes[3].Update(isActive, mouseState, position) == true)
            {
                if (state == OptionsStates.Demo)
                {
                    state = OptionsStates.Main;
                }
                else
                {
                    state = OptionsStates.Demo;
                    hypertextes[0].Release();
                    hypertextes[1].Release();
                    //hypertextes[2].Release();
                }
            }
            else
            {
                if ((state != OptionsStates.Main) & (state != OptionsStates.LAN))
                {
                    if (hypertextes[4].Update(true, mouseState, position) == true)
                    {
                        switch (state)
                        {
                            case OptionsStates.Single:
                                {
                                    settings.matchType = MatchTypes.PvC;
                                    settings.opponentName = "Il Computer";

                                    break;
                                }
                            case OptionsStates.Multi:
                                {
                                    settings.matchType = MatchTypes.PvP;
                                    settings.opponentName = "L'ospite";

                                    break;
                                }
                            case OptionsStates.Demo:
                                {
                                    settings.matchType = MatchTypes.CvC;
                                    settings.opponentName = "Il Computer";

                                    break;
                                }
                        }
                    }
                    else
                    {
                        settings.windInfluence = trackBars[0].Update(true, mouseState, position);

                        if ((state == OptionsStates.Single) | (state == OptionsStates.Demo))
                        {
                            settings.CPUIntelligence = trackBars[1].Update(true, mouseState, position);
                        }
                        else
                        {
                            trackBars[1].Update();
                        }

                        selector.Update(true, mouseState, position);

                        matcher.Update();
                    }
                }
                else if (state == OptionsStates.LAN)
                {
                    hypertextes[4].Update();
                    trackBars[0].Update();
                    trackBars[1].Update();
                    selector.Update();
                    settings = matcher.Update(true, mouseState, position);
                }
                else
                {
                    hypertextes[4].Update();
                    trackBars[0].Update();
                    trackBars[1].Update();
                    selector.Update();
                    matcher.Update();
                }

                Global.nickname = textBox.Update(isActive, mouseState, keyboardState, position);
            }

            return settings;
        }

        public void Draw(SpriteBatch spriteBatch, Color light)
        {
            Point position = base.draw(spriteBatch, light);

            hypertextes[0].Draw(spriteBatch, position);
            hypertextes[1].Draw(spriteBatch, position);
            //hypertextes[2].Draw(spriteBatch, position);
            hypertextes[3].Draw(spriteBatch, position);
            hypertextes[4].Draw(spriteBatch, position);
            textBox.Draw(spriteBatch, position, light);
            trackBars[0].Draw(spriteBatch, position, light);
            trackBars[1].Draw(spriteBatch, position, light);
            selector.Draw(spriteBatch, position, light);
            matcher.Draw(spriteBatch, position, light);
        }
    }
}
