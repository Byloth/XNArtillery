using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine.Net;

namespace XNArtillery
{
    enum MatcherStates
    {
        Stop,
        Host,
        Search
    };

    class Matcher
    {
        private MatcherStates state;
        private Hypertext[] buttons;
        private UDP udp;
        private MatchHost host;
        private MatchFinder finder;

        public Matcher()
        {
            state = MatcherStates.Stop;

            buttons = new Hypertext[2]
            {
                new Hypertext(),
                new Hypertext()
            };

            udp = new UDP(Global.client, Global.port);

            finder = new MatchFinder(udp);
            host = new MatchHost(udp);
        }

        public void LoadContent(MyGame runningGame, Point matcherPosition)
        {
            buttons[0].LoadContent(runningGame, true, "BoldText", "Partecipa a partita", new Point(matcherPosition.X - Global.ResizeByX(25), matcherPosition.Y - Global.ResizeByX(50)));
            buttons[1].LoadContent(runningGame, true, "BoldText", "Ospita partita", new Point(matcherPosition.X + Global.ResizeByX(475), matcherPosition.Y - Global.ResizeByX(50)));
            finder.LoadContent(runningGame, matcherPosition);
            host.LoadContent(runningGame, matcherPosition);
        }

        public void StopThreads()
        {
            finder.StopThreads();
            host.StopThreads();
        }

        public void Update()
        {
            state = MatcherStates.Stop;

            buttons[0].Update();
            buttons[1].Update();
            finder.Update();
            host.Update();
        }

        public MatchSettings Update(bool isActive, MouseState mouseState, Point positionIncrement)
        {
            MatchSettings settings = new MatchSettings();

            if (isActive == true)
            {
                if (buttons[0].Update(isActive, mouseState, positionIncrement) == true)
                {
                    if (state != MatcherStates.Search)
                    {
                        state = MatcherStates.Search;
                    }
                    else
                    {
                        state = MatcherStates.Stop;
                    }

                    buttons[1].Release();
                }
                else if (buttons[1].Update(isActive, mouseState, positionIncrement) == true)
                {
                    if (state != MatcherStates.Host)
                    {
                        state = MatcherStates.Host;
                    }
                    else
                    {
                        state = MatcherStates.Stop;
                    }

                    buttons[0].Release();
                }

                if (state == MatcherStates.Search)
                {
                    settings = finder.Update(true, mouseState, positionIncrement);
                    host.Update();
                }
                else if (state == MatcherStates.Host)
                {
                    settings = host.Update(true, mouseState, positionIncrement);
                    finder.Update();
                }
                else
                {
                    finder.Update();
                    host.Update();
                }
            }
            else
            {
                Update();
            }

            if (settings.matchType != MatchTypes.Null)
            {
                StopThreads();
            }

            return settings;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            buttons[0].Draw(spriteBatch, positionIncrement);
            buttons[1].Draw(spriteBatch, positionIncrement);
            finder.Draw(spriteBatch, positionIncrement, light);
            host.Draw(spriteBatch, positionIncrement, light);
        }
    }
}
