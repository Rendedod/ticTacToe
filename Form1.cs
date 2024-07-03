using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ticTacToe
{
    public partial class Form1 : Form
    {
        private bool rightWay = true;
        private bool bot = false;
        bool winerGet = false;
        private Image cross;
        private Image zero;
        private PictureBox[,] field = new PictureBox[3, 3];
        private Figurs[,] figursField;
        PlayField playField = new PlayField();

        public Form1()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            Button rebuild = new Button {Parent = this, Size = new Size(100, 40), Top = 20, Left = 350, BackColor = Color.Black, Text = "Rebuild", ForeColor = Color.White, Cursor = Cursors.Hand };
            rebuild.Click += rebuildAll;

            Button bot = new Button { Parent = this, Size = new Size(100, 40), Top = 100, Left = 350, BackColor = Color.Black, Text = "Bot", ForeColor = Color.White, Cursor = Cursors.Hand };
            bot.Click += StartBot;

            WebClient wc = new WebClient();
            cross = Image.FromStream(wc.OpenRead("https://cdn.onlinewebfonts.com/svg/download_26540.png"));
            zero = Image.FromStream(wc.OpenRead("https://cdn.onlinewebfonts.com/svg/download_441992.png"));
            figursField = playField.Field;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = new PictureBox { Parent = this, Size = new Size(100, 100), Top = i * 100, Left = j * 100, BorderStyle = BorderStyle.FixedSingle, Tag = new Point(i, j), Cursor = Cursors.Hand, SizeMode = PictureBoxSizeMode.StretchImage };
                    field[i, j].Click += figureClick;
                }
        }

        private void StartBot(object sender, EventArgs e)
        {
            winerGet = false;
            bot = !bot;
            rebuildAll(null, null);
        }
        private void botStap()
        {
            Random random = new Random();
            while (checkViner() == Figurs.empty && bot && coutEmpty() != 1)
            {
                int x = random.Next(3);
                int y = random.Next(3);
                if (field[x, y].Image == null )
                {
                    if (rightWay)
                    {
                        figursField[x, y] = Figurs.cross;
                        field[x, y].Image = cross;
                    }
                    else
                    {
                        figursField[x, y] = Figurs.zero;
                        field[x, y].Image = zero;
                    }
                    rightWay = !rightWay;
                    break;
                }
            }
            if (checkViner() != Figurs.empty || coutEmpty() == 1)
            {
                printWiner();
            }

        }

        private void printWiner()
        {
            if (checkViner() != Figurs.empty)
            {
                MessageBox.Show($"Win : {checkViner()}");
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        field[i, j].Enabled = false;
            }

        }

        private int coutEmpty()
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (figursField[i, j] == Figurs.empty)
                    {
                        count++;
                    }
                }
            return count;
        }

        private void figureClick(object sender, EventArgs e)
        {
            PictureBox item = (PictureBox)sender;
            if (item.Image == null && checkViner() == Figurs.empty)
            {
                int x = item.Location.X / 100;
                int y = item.Location.Y / 100;
                if (rightWay)
                {
                    figursField[x, y] = Figurs.cross;
                    item.Image = cross;
                }
                else
                {
                    figursField[x, y] = Figurs.zero;
                    item.Image = zero;
                }
                rightWay = !rightWay;
                if (checkViner() == Figurs.empty)
                {
                    botStap();
                }
                else
                {
                    printWiner();
                }
            }
        }

        private Figurs checkViner()
        {
            return playField.chekcViner(figursField);

        }

        private void rebuildAll(object sender, EventArgs e)
        {
            winerGet = false;
            Controls.Clear();
            init();
        }
    }
}
