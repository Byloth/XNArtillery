using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine.Net;

namespace XNArtillery
{
    class MatchHost
    {
        private bool active;
        private bool ready;

        private MatchSettings settings;
        private PlayerBoard board;
        private TrackBar trackBar;
        private Hypertext hypertext;
        private Thread host;
        private UDP udp;

        public MatchHost(UDP sharedUDP)
        {
            active = false;
            ready = false;

            settings = new MatchSettings();
            board = new PlayerBoard();
            trackBar = new TrackBar();
            hypertext = new Hypertext();
            host = new Thread(new ThreadStart(Listen));
            udp = sharedUDP;
        }

        public void LoadContent(MyGame runningGame, Point matchFinderPosition)
        {
            board.LoadContent(runningGame);
            trackBar.LoadContent(runningGame, "Influenza vento:", 1000, Global.Resize(700, 568));
            hypertext.LoadContent(runningGame, false, "BoldLabel", "Inizia la partita!", Global.Resize(552, 1220));
        }

        public void Listen()
        {
            Packet packet;
            IPEndPoint guestEndPoint = null;

            do
            {
                packet = udp.Receive(ref guestEndPoint);

                if (Global.IsMyIPAddress(guestEndPoint.Address) == false)
                {
                    if (packet.type == PacketTypes.IMLookingForMatches)
                    {
                        udp.Send(new Packet(PacketTypes.IMHostingAMatch, Global.nickname));
                    }
                    else if (packet.type == PacketTypes.IWantToJoin)
                    {
                        if (ready == false)
                        {
                            board.Active((string)packet.content);
                            settings.opponentEndPoint = guestEndPoint;
                        }
                    }
                }
            }
            while (host.IsAlive == true);
        }

        public void StopThreads()
        {
            if (host.IsAlive == true)
            {
                host.Abort();
            }
        }

        public void Update()
        {
            ready = false;
            active = false;
            board.Update();
            trackBar.Update();
            hypertext.Update();
            StopThreads();
        }


        public MatchSettings Update(bool isActive, MouseState mouseState, Point positionIncrement)
        {
            if (active != isActive)
            {
                if (isActive == true)
                {
                    host = new Thread(new ThreadStart(Listen));
                    host.Start();
                }
                else
                {
                    StopThreads();
                }

                active = isActive;
            }

            if (isActive == true)
            {
                string playerName = board.Update(isActive, mouseState);

                if (playerName != "")
                {
                    if (playerName != "à")
                    {
                        udp.Send(new Packet(PacketTypes.IAcceptedYou));
                        settings.opponentName = playerName;
                        ready = true;
                    }
                    else
                    {
                        udp.Send(new Packet(PacketTypes.IDeclineYou));
                        ready = false;
                    }
                }

                if (hypertext.Update(ready, mouseState, positionIncrement) == true)
                {
                    settings.turn = Global.Random(2);
                    settings.matchType = MatchTypes.Lan;
                    udp.Send(new Packet(PacketTypes.HereYouAre, (object)new MatchSettings(settings, Global.nickname)), settings.opponentEndPoint);
                    settings.turn = Math.Abs(settings.turn - 1);
                }

                settings.windInfluence = trackBar.Update(isActive, mouseState, positionIncrement);
            }
            else
            {
                Update();
            }

            return settings;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            trackBar.Draw(spriteBatch, positionIncrement, light);
            hypertext.Draw(spriteBatch, positionIncrement);
            board.Draw(spriteBatch, light);
        }
    }
}
