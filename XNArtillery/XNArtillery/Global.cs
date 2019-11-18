using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNArtillery
{
    public class Global
    {
        public const float G = 9.80665F;

        static public bool fullScreen = false;

        static public int port = 12345;

        static public float time;
        static public float increment = 0.015F;

        static public string nickname = "Giocatore";

        static private Random rndm = new Random();

        static public Point MAX = new Point(3000, 1875);
        static public Point resolution = new Point(1360, 768);

        static public UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, port));

        static public int Horizon()
        {
            return ResizeByY(1663);
        }

        static public int Random(int maxValue)
        {
            return rndm.Next(maxValue);
        }

        static public int Random(int minValue, int maxValue)
        {
            return rndm.Next(minValue, maxValue);
        }

        static public bool IsWideScreen()
        {
            if (((float)resolution.X / resolution.Y) <= 1.5F)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        static public float Scale()
        {
            return ((float)(resolution.X + resolution.Y) / (MAX.X + MAX.Y));
        }

        static public int Size(double value)
        {
            return (int)((SizeByX(value) + SizeByY(value)) / 2);
        }

        static public Point Size(Point values)
        {
            return new Point(SizeByX(values.X), SizeByY(values.Y));
        }

        static public int Resize(double value)
        {
            return (int)((ResizeByX(value) + ResizeByY(value)) / 2);
        }

        static public Point Resize(double valueX, double valueY)
        {
            return (new Point(ResizeByX(valueX), ResizeByY(valueY)));
        }

        static public Vector2 Resize(Vector2 values)
        {
            return (new Vector2(ResizeByX(values.X), ResizeByY(values.Y)));
        }

        static public Vector2 ResizeText(Vector2 values)
        {
            float x;

            if (IsWideScreen() == true)
            {
                x = resolution.X;
            }
            else
            {
                x = resolution.X + (float)resolution.X / 8.5F;
            }

            return (new Vector2(ResizeByX(values.X, (int)x), ResizeByY(values.Y)));
        }

        static public int SizeByX(double value)
        {
            return (int)(value / resolution.X * MAX.X);
        }

        static public int ResizeByX(double value)
        {
            return (int)(value / MAX.X * resolution.X);
        }

        static public int ResizeByX(double value, int selectedResolution)
        {
            return (int)(value / MAX.X * selectedResolution);
        }

        static public int SizeByY(double value)
        {
            return (int)(value / resolution.Y * MAX.Y);
        }

        static public int ResizeByY(double value)
        {
            return (int)(value / MAX.Y * resolution.Y);
        }

        static public Point AddIncrement(Point point, Point increment)
        {
            point.X += increment.X;
            point.Y += increment.Y;

            return point;
        }

        static public Vector2 AddIncrement(Vector2 point, Point increment)
        {
            point.X += increment.X;
            point.Y += increment.Y;

            return point;
        }

        static public Rectangle AddIncrement(Rectangle rectangle, Point increment)
        {
            rectangle.X += increment.X;
            rectangle.Y += increment.Y;

            return rectangle;
        }

        static public char GetText(Keys[] pressedKeys)
        {
            bool toUpper = false;
            bool character = false;
            string text = "?";

            foreach(Keys pressedKey in pressedKeys)
            {
                switch (pressedKey)
                {
                    case Keys.A:
                        {
                            if (character == false)
                            {
                                text = "a";
                                character = true;
                            }

                            break;
                        }
                    case Keys.B:
                        {
                            if (character == false)
                            {
                                text = "b";
                                character = true;
                            }

                            break;
                        }
                    case Keys.C:
                        {
                            if (character == false)
                            {
                                text = "c";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D:
                        {
                            if (character == false)
                            {
                                text = "d";
                                character = true;
                            }

                            break;
                        }
                    case Keys.E:
                        {
                            if (character == false)
                            {
                                text = "e";
                                character = true;
                            }

                            break;
                        }
                    case Keys.F:
                        {
                            if (character == false)
                            {
                                text = "f";
                                character = true;
                            }

                            break;
                        }
                    case Keys.G:
                        {
                            if (character == false)
                            {
                                text = "g";
                                character = true;
                            }

                            break;
                        }
                    case Keys.H:
                        {
                            if (character == false)
                            {
                                text = "h";
                                character = true;
                            }

                            break;
                        }
                    case Keys.I:
                        {
                            if (character == false)
                            {
                                text = "i";
                                character = true;
                            }

                            break;
                        }
                    case Keys.J:
                        {
                            if (character == false)
                            {
                                text = "j";
                                character = true;
                            }

                            break;
                        }
                    case Keys.K:
                        {
                            if (character == false)
                            {
                                text = "k";
                                character = true;
                            }

                            break;
                        }
                    case Keys.L:
                        {
                            if (character == false)
                            {
                                text = "l";
                                character = true;
                            }

                            break;
                        }
                    case Keys.M:
                        {
                            if (character == false)
                            {
                                text = "m";
                                character = true;
                            }

                            break;
                        }
                    case Keys.N:
                        {
                            if (character == false)
                            {
                                text = "n";
                                character = true;
                            }

                            break;
                        }
                    case Keys.O:
                        {
                            if (character == false)
                            {
                                text = "o";
                                character = true;
                            }

                            break;
                        }
                    case Keys.P:
                        {
                            if (character == false)
                            {
                                text = "p";
                                character = true;
                            }

                            break;
                        }
                    case Keys.Q:
                        {
                            if (character == false)
                            {
                                text = "q";
                                character = true;
                            }

                            break;
                        }
                    case Keys.R:
                        {
                            if (character == false)
                            {
                                text = "r";
                                character = true;
                            }

                            break;
                        }
                    case Keys.S:
                        {
                            if (character == false)
                            {
                                text = "s";
                                character = true;
                            }

                            break;
                        }
                    case Keys.T:
                        {
                            if (character == false)
                            {
                                text = "t";
                                character = true;
                            }

                            break;
                        }
                    case Keys.U:
                        {
                            if (character == false)
                            {
                                text = "u";
                                character = true;
                            }

                            break;
                        }
                    case Keys.V:
                        {
                            if (character == false)
                            {
                                text = "v";
                                character = true;
                            }

                            break;
                        }
                    case Keys.W:
                        {
                            if (character == false)
                            {
                                text = "w";
                                character = true;
                            }

                            break;
                        }
                    case Keys.X:
                        {
                            if (character == false)
                            {
                                text = "x";
                                character = true;
                            }

                            break;
                        }
                    case Keys.Y:
                        {
                            if (character == false)
                            {
                                text = "y";
                                character = true;
                            }

                            break;
                        }
                    case Keys.Z:
                        {
                            if (character == false)
                            {
                                text = "z";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D0:
                    case Keys.NumPad0:
                        {
                            if (character == false)
                            {
                                text = "0";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D1:
                    case Keys.NumPad1:
                        {
                            if (character == false)
                            {
                                text = "1";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D2:
                    case Keys.NumPad2:
                        {
                            if (character == false)
                            {
                                text = "2";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D3:
                    case Keys.NumPad3:
                        {
                            if (character == false)
                            {
                                text = "3";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D4:
                    case Keys.NumPad4:
                        {
                            if (character == false)
                            {
                                text = "4";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D5:
                    case Keys.NumPad5:
                        {
                            if (character == false)
                            {
                                text = "5";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D6:
                    case Keys.NumPad6:
                        {
                            if (character == false)
                            {
                                text = "6";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D7:
                    case Keys.NumPad7:
                        {
                            if (character == false)
                            {
                                text = "7";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D8:
                    case Keys.NumPad8:
                        {
                            if (character == false)
                            {
                                text = "8";
                                character = true;
                            }

                            break;
                        }
                    case Keys.D9:
                    case Keys.NumPad9:
                        {
                            if (character == false)
                            {
                                text = "9";
                                character = true;
                            }

                            break;
                        }
                    case Keys.Space:
                        {
                            if (character == false)
                            {
                                text = " ";
                                character = true;
                            }

                            break;
                        }
                    case Keys.LeftShift:
                    case Keys.RightShift:
                        {
                            toUpper = true;

                            break;
                        }
                }
            }

            if (toUpper == true)
            {
                text = text.ToUpper();
            }

            return text.ToCharArray()[0];
        }

        static public Keys GetCommand(Keys[] pressedKeys)
        {
            bool command = false;

            foreach (Keys pressedKey in pressedKeys)
            {
                switch (pressedKey)
                {
                    case Keys.Back:
                    case Keys.Enter:
                    case Keys.Escape:
                        {
                            command = true;

                            break;
                        }
                }

                if (command == true)
                {
                    return pressedKey;
                }
            }

            return Keys.None;
        }

        public static bool IsMyIPAddress(string host)
        {
            IPAddress[] hostIPs = Dns.GetHostAddresses(host);
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress hostIP in hostIPs)
            {
                if (IPAddress.IsLoopback(hostIP))
                {
                    return true;
                }

                foreach (IPAddress localIP in localIPs)
                {
                    if (hostIP.Equals(localIP))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsMyIPAddress(IPAddress hostIP)
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            if (IPAddress.IsLoopback(hostIP))
            {
                return true;
            }

            foreach (IPAddress localIP in localIPs)
            {
                if (hostIP.Equals(localIP))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
