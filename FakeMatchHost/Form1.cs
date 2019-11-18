using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using ByloEngine.Net;

namespace FakeMatchHost
{
    public partial class Form1 : Form
    {
        private bool active;

        private UDP udp;
        private Thread listener;

        public Form1()
        {
            active = false;

            udp = new UDP(12345);
            listener = new Thread(new ThreadStart(Listen));

            InitializeComponent();
        }

        delegate void DelegateWrite(string text);

        private void Write(string text)
        {
            if (this.InvokeRequired == true)
            {
                DelegateWrite writer = new DelegateWrite(Write);

                Invoke(writer, new object[] { text });
            }
            else
            {
                listBox1.Items.Add(text);
            }
        }

        private void Listen()
        {
            Packet packet;
            IPEndPoint guestEndPoint = null;

            do
            {
                packet = udp.Receive(ref guestEndPoint);

                if (packet.type == PacketTypes.IMLookingForMatches)
                {
                    udp.Send(new Packet(PacketTypes.IMHostingAMatch, "Fake Match Host"), guestEndPoint);
                }
                else if (packet.type == PacketTypes.IWantToJoin)
                {
                    Write(packet.content + " vuole giocare!");

                    udp.Send(new Packet(PacketTypes.IAcceptedYou), guestEndPoint);
                }
            }
            while (true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (active == false)
            {
                active = true;
                listener = new Thread(new ThreadStart(Listen));
                listener.Start();
                button1.Text = "Disattiva";
            }
            else
            {
                active = false;
                listener.Abort();
                button1.Text = "Attiva";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listener.IsAlive == true)
            {
                listener.Abort();
            }
        }
    }
}
