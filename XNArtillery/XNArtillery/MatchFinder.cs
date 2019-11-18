using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ByloEngine.Net;

namespace XNArtillery
{
    struct Host
    {
        public int ttl;
        public string nickname;
        public IPEndPoint endPoint;
    }

    class MatchFinder
    {
        private bool active;
        private bool isSending;
        private bool updated;
        private int count;
        private float alpha;

        private Texture2D board;
        private Rectangle position;
        private Loading loading;
        private Hypertext[] hypertextes;
        private Fade fade;
        private UDP udp;
        private Thread finder;
        private Thread listener;
        private Host[] hosts;
        private MessageBoard message;
        private MatchSettings settings;

        public MatchFinder(UDP sharedUDP)
        {
            updated = true;
            active = false;
            count = 0;

            loading = new Loading();
            fade = new Fade(false);
            hypertextes = new Hypertext[10]
            {
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext(),
                new Hypertext()
            };

            udp = sharedUDP;
            finder = new Thread(new ThreadStart(Find));
            listener = new Thread(new ThreadStart(Listen));
            hosts = new Host[10];
            message = new MessageBoard();
            settings = new MatchSettings();
        }

        public void LoadContent(MyGame runningGame, Point matchFinderPosition)
        {
            board = runningGame.Content.Load<Texture2D>("Images/Objectes/MatchFinder");
            position = new Rectangle (matchFinderPosition.X, matchFinderPosition.Y, Global.ResizeByX(board.Width), Global.ResizeByY(board.Height));
            loading.LoadContent(runningGame, new Point(matchFinderPosition.X + Global.ResizeByX(775), matchFinderPosition.Y - Global.ResizeByY(150)));
            
            for (int i = 0; i < hypertextes.Length; i += 1)
            {
                hypertextes[i].LoadContent(runningGame, true, Global.Resize(733, 466 + (i * 75)), "Text");
            }

            message.LoadContent(runningGame);
        }

        private void Listen()
        {
            Host host;
            Packet packet;
            IPEndPoint senderEndPoint = null;

            do
            {
                packet = udp.Receive(ref senderEndPoint);

                if (Global.IsMyIPAddress(senderEndPoint.Address) == false)
                {
                    if (packet.type == PacketTypes.IMHostingAMatch)
                    {
                        bool alreadyKnown = false;

                        host.ttl = 200;
                        host.nickname = (string)packet.content;
                        host.endPoint = senderEndPoint;

                        for (int i = 0; i < count; i += 1)
                        {
                            if (hosts[i].Equals(host) == true)
                            {
                                hosts[i].ttl = 200;
                                alreadyKnown = true;

                                break;
                            }
                            else if (hosts[i].endPoint.Equals(host.endPoint) == true)
                            {
                                hosts[i] = host;
                                alreadyKnown = true;
                                updated = true;

                                break;
                            }
                        }

                        if ((count < 10) & (alreadyKnown == false))
                        {
                            hosts[count] = host;
                            count += 1;
                            updated = true;
                        }
                    }
                    else if (packet.type == PacketTypes.IAcceptedYou)
                    {
                        message.Active("Avvio della partita in corso...");
                    }
                    else if (packet.type == PacketTypes.IDeclineYou)
                    {
                        foreach (Hypertext hypertext in hypertextes)
                        {
                            hypertext.Release();
                        }
                    }
                    else if (packet.type == PacketTypes.HereYouAre)
                    {
                        settings = (MatchSettings)packet.content;
                        settings.opponentEndPoint = senderEndPoint;
                    }
                }
            }
            while (listener.IsAlive == true);
        }

        private void Decrease()
        {
            for (int i = 0; i < count; i += 1)
            {
                hosts[i].ttl -= 1;
            }
        }

        private void Find()
        {
            listener = new Thread(new ThreadStart(Listen));
            listener.Start();

            do
            {
                isSending = true;

                for (int i = 0; i < 5; i += 1)
                {
                    udp.Send(new Packet(PacketTypes.IMLookingForMatches, i));

                    Thread.Sleep(1000);
                }

                isSending = false;

                Thread.Sleep(5000);
            }
            while (finder.IsAlive == true);
        }

        public void StopThreads()
        {
            if (finder.IsAlive == true)
            {
                finder.Abort();
            }
            if (listener.IsAlive == true)
            {
                listener.Abort();
            }
        }

        public void Update()
        {
            if (active == true)
            {
                active = false;
                fade.Start(FadeStates.Out, false, 12);
                StopThreads();
            }

            alpha = fade.Update();

            loading.Update(false);

            foreach (Hypertext hypertext in hypertextes)
            {
                hypertext.Update();
            }

            message.Update();
        }

        private void Shift(int startingIndex)
        {
            for (int i = startingIndex; i < count; i += 1)
            {
                hosts[i] = hosts[i + 1];
            }
        }

        private void Filtrate()
        {
            for (int i = 0; i < count; i += 1)
            {
                if (hosts[i].ttl <= 0)
                {
                    Shift(i);
                    count -= 1;
                    updated = true;
                }
            }
        }

        public MatchSettings Update(bool isActive, MouseState mouseState, Point positionIncrement)
        {
            if (active != isActive)
            {
                if (isActive == true)
                {
                    fade.Start(FadeStates.In, false, 12);
                    finder = new Thread(new ThreadStart(Find));
                    finder.Start();
                }
                else
                {
                    fade.Start(FadeStates.Out, false, 12);
                    StopThreads();
                }

                active = isActive;
            }

            if (isActive == true)
            {
                alpha = fade.Update();
                loading.Update(isSending);

                if (isSending == true)
                {
                    Decrease();
                    Filtrate();
                }

                if (updated == true)
                {
                    for (int i = 0; i < hypertextes.Length; i += 1)
                    {
                        if (i < count)
                        {
                            hypertextes[i].NewText(hosts[i].nickname + " (IP: " + hosts[i].endPoint.Address.ToString() + ")");
                        }
                        else
                        {
                            hypertextes[i].NewText("");
                        }
                    }

                    updated = false;
                }

                for (int i = 0; i < hypertextes.Length; i += 1)
                {
                    if (hypertextes[i].Update(isActive, mouseState, positionIncrement) == true)
                    {
                        udp.Send(new Packet(PacketTypes.IWantToJoin, Global.nickname), hosts[i].endPoint);
                    }
                }

                message.Update(isActive);
            }
            else
            {
                Update();
            }

            return settings;
        }

        public void Draw(SpriteBatch spriteBatch, Point positionIncrement, Color light)
        {
            spriteBatch.Draw(board, Global.AddIncrement(position, positionIncrement), light * alpha);
            loading.Draw(spriteBatch, positionIncrement, light);

            foreach (Hypertext hypertext in hypertextes)
            {
                hypertext.Draw(spriteBatch, positionIncrement);
            }

            message.Draw(spriteBatch, light);
        }
    }
}
