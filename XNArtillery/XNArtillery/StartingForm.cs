using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

using XNArtillery.Game;

using LoLSize = ByloEngine.Size;

namespace XNArtillery
{
    public partial class StartingForm : Form
    {
        private Thread myGame;

        public StartingForm()
        {
            myGame = new Thread(new ThreadStart(runGame));

            InitializeComponent();
        }

        private int checkX()
        {
            switch (resolution.SelectedIndex)
            {
                case 0:
                    return 800;

                case 1:
                    return 1024;

                case 2:
                    return 1152;

                case 3:
                    return 1176;

                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return 1280;

                case 9:
                    return 1360;

                case 10:
                    return 1366;

                case 11:
                case 12:
                    return 1600;

                case 13:
                    return 1680;

                default:
                    return 0;
            }
        }
        private int checkY()
        {
            switch (resolution.SelectedIndex)
            {
                case 0:
                    return 600;

                case 1:
                case 5:
                case 9:
                case 10:
                    return 768;

                case 2:
                    return 864;

                case 3:
                    return 664;

                case 4:
                    return 720;

                case 6:
                    return 800;

                case 7:
                    return 960;

                case 8:
                case 12:
                    return 1024;

                case 11:
                    return 900;

                case 13:
                    return 1050;

                default:
                    return 0;
            }
        }

        private void setGraphics()
        {
            Global.IsFullScreen = isFullScreen.Checked;

            Global.MyResolution = new LoLSize();
            Global.MyResolution.Width = checkX();
            Global.MyResolution.Height = checkY();
        }

        private void checkFirstPlayer()
        {
            if ((single.Checked == true) | (multi.Checked == true))
            {
                Global.players[0] = new Human(0);
            }
            else
            {
                Global.players[0] = new CPU(0, ability1.Value);
            }
        }
        private void checkSecondPlayer()
        {
            if ((single.Checked == true) | (demo.Checked == true))
            {
                Global.players[1] = new CPU(1, ability2.Value);
            }
            else
            {
                Global.players[1] = new Human(1);
            }
        }

        private void setPlayers()
        {
            Global.players = new Player[2];
            checkFirstPlayer();
            checkSecondPlayer();
        }

        private void setPlayersNames()
        {
            Global.playersNames = new string[]
            {
                name1.Text,
                name2.Text
            };
        }

        private void setLevel()
        {
            Global.level = level.SelectedIndex + 1;
        }

        private void setWind()
        {
            Global.windInfluence = (float)windPower.Value / 100;
        }

        private void runGame()
        {
            using (MyGame game = new MyGame())
            {
                game.Run();
            }
        }

        private void startGame(object sender, EventArgs e)
        {
            setGraphics();
            setPlayers();
            setPlayersNames();
            setLevel();
            //setWind();

            myGame.Start();

            Close();
        }

        private void StartingForm_Load(object sender, EventArgs e)
        {
            level.SelectedIndex = 0;
            resolution.SelectedIndex = 1;
        }

        private void single_CheckedChanged(object sender, EventArgs e)
        {
            name1.Enabled = true;
            name1.Text = "Giocatore";
            name2.Enabled = false;
            name2.Text = "CPU";
            ability1.Enabled = false;
            ability2.Enabled = true;
        }

        private void multi_CheckedChanged(object sender, EventArgs e)
        {
            name1.Enabled = true;
            name1.Text = "Giocatore 1";
            name2.Enabled = true;
            name2.Text = "Giocatore 2";
            ability1.Enabled = false;
            ability2.Enabled = false;
        }

        private void demo_CheckedChanged(object sender, EventArgs e)
        {
            name1.Enabled = false;
            name1.Text = "CPU 1";
            name2.Enabled = false;
            name2.Text = "CPU 2";
            ability1.Enabled = true;
            ability2.Enabled = true;
        }
    }
}

