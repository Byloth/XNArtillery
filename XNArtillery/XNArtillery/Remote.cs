using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ByloEngine.Net;

namespace XNArtillery
{
    class Remote : Player
    {
        private int ttl;
        private float[] shootingValues;

        private UDP udp;
        private Thread receiver;
        private IPEndPoint endPoint;

        public Remote(IPEndPoint playerEndPoint)
        {
            ttl = 2500;
            shootingValues = new float[2] { float.NaN, float.NaN };

            udp = new UDP(Global.client, Global.port);
            receiver = new Thread(new ThreadStart(Receive));
            endPoint = playerEndPoint;
        }

        public override Point LoadContent(MyGame runningGame, int towerNumber, Point[] playersPositions, Collider globalCollider)
        {
            Point dimension = base.loadContent(runningGame, towerNumber, playersPositions[0], globalCollider);

            return new Point(playersPositions[0].X, playersPositions[0].Y - dimension.Y);
        }

        private void Receive()
        {
            Packet packet;
            IPEndPoint senderEndPoint = null;

            do
            {
                packet = udp.Receive(ref senderEndPoint);

                if (senderEndPoint.Equals(endPoint) == true)
                {
                    if (packet.type == PacketTypes.IShot)
                    {
                        shootingValues = (float[])packet.content;
                    }
                    else if (packet.type == PacketTypes.IMAlive)
                    {
                        ttl = 2500;
                    }
                }
            }
            while (receiver.IsAlive == true);
        }
        
        public override float[] Update(bool newShot, float windPower)
        {
            if (receiver.IsAlive == false)
            {
                receiver.Start();
            }

            ttl -= 1;

            if (ttl <= 0)
            {

            }

            if (newShot == true)
            {
                return shootingValues;
            }
            else
            {
                shootingValues = new float[2] { float.NaN, float.NaN };

                return shootingValues;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color light)
        {
            base.draw(spriteBatch, light);
        }
    }
}
