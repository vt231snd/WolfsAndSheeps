using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sliv
{
    public partial class Ground : UserControl
    {
        int lvl;
        int countfence;
        int fence;
        int xtar;
        int xvovk;
        int ytar;
        int yvovk;
        string lang = "Ua";
        Control control = new Control();
        public CompleteLevel comp = new CompleteLevel();
        int[,] Map = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        int MapWidht;
        int MapHeight;
        public Ground()
        {
            InitializeComponent();
        }
        public Ground(int lvl, int countfence, int xtar, int ytar, int xvovk, int yvovk, string lang)
        {
            InitializeComponent();
            this.lvl = lvl;
            this.countfence = countfence;
            this.xtar = xtar;
            this.ytar = ytar;
            this.yvovk = yvovk;
            this.xvovk = xvovk;
            this.lang = lang;
            Map[xvovk, yvovk] = 3;
            Map[xtar, ytar] = 2;
            fence = countfence;
            label2.Text = fence.ToString();

            swichLang(lang);
        }

        public void swichLang(string lang)
        {
            if (lang == "Ua")
            {
                button3.Text = "Перевірити";
                label2.Text = "Залишилось паркану: " + fence;
            } else if (lang == "Eng")
            {
                button3.Text = "Check";
                label2.Text = "fence left: " + fence;
            }
        }
        private void Ground_Load(object sender, EventArgs e)
        {
            Generete();
        }

        public void Restart()
        {
            Map = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1 }
            };
            Map[xvovk, yvovk] = 3;
            Map[xtar, ytar] = 2;
            fence = countfence;
            swichLang(lang);
            Generete();
        }

        public void Lose()
        {
            control.Controls.Clear();
            control.Dock = DockStyle.Fill;
            int rows = Map.GetLength(0);
            int cols = Map.GetLength(1);
            int buttonSize = 100;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(j * buttonSize + 500, i * buttonSize + 52);
                    button.BackColor = Color.Green;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    if (Map[i, j] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.oo;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.vv;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    button.TabIndex = Map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    control.Controls.Add(button);
                }
            }
            this.Controls.Add(control);
        }

        public void Victory()
        {
            control.Controls.Clear();
            control.Dock = DockStyle.Fill;
            int rows = Map.GetLength(0);
            int cols = Map.GetLength(1);
            int buttonSize = 100;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(j * buttonSize + 500, i * buttonSize + 52);
                    button.BackColor = Color.Green;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    if (Map[i, j] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.ov;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.vo;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    button.TabIndex = Map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    button.Enabled = false;
                    control.Controls.Add(button);
                }
            }
            this.Controls.Add(control);
        }

        public void Generete()
        {
            control.Controls.Clear();
            fence = countfence;
            control.Dock = DockStyle.Fill;
            int rows = Map.GetLength(0);
            int cols = Map.GetLength(1);
            int buttonSize = 100;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(j * buttonSize + 500, i * buttonSize + 52);
                    button.BackColor = Color.Green;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    if (Map[j, i] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.o;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (Map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.v;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    } 
                    button.TabIndex = Map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    control.Controls.Add(button);
                }
            }
            this.Controls.Add(control);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Name.Contains("0") || clickedButton.Name.Contains("7"))
            {
                return;
            }
            if (clickedButton.TabIndex == 0 && fence != 0)
            {
                clickedButton.TabIndex = 1;
                clickedButton.BackgroundImage = Properties.Resources.R;
                clickedButton.BackgroundImageLayout = ImageLayout.Zoom;
                Map[Convert.ToInt32(clickedButton.Name) % 10, Convert.ToInt32(clickedButton.Name) / 10] = 1;
                fence--;
            }
            else if (clickedButton.TabIndex == 1)
            {
                clickedButton.TabIndex = 0;
                clickedButton.BackgroundImage = null;
                clickedButton.BackColor = Color.Green;
                Map[Convert.ToInt32(clickedButton.Name) % 10, Convert.ToInt32(clickedButton.Name) / 10] = 0;
                fence++;
            }


            swichLang(lang);
        }

        public bool FindWave(int startX, int startY, int targetX, int targetY)
        {
            bool add = true;
            bool res = true;
            int[,] cMap = new int[MapHeight, MapWidht];
            int x, y, step = 0;
            for (y = 0; y < MapHeight; y++)
                for (x = 0; x < MapWidht; x++)
                {
                    if (Map[y, x] == 1)
                        cMap[y, x] = -2;
                    else
                        cMap[y, x] = -1;
                }
            cMap[targetY, targetX] = 0;
            while (add == true)
            {
                for (y = 0; y < MapWidht; y++)
                    for (x = 0; x < MapHeight; x++)
                    {
                        if (cMap[x, y] == step)
                        {
                            if (y - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (x - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (y + 1 < MapWidht && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                            if (x + 1 < MapHeight && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                        }
                    }
                step++;
                add = true;
                if (cMap[startY, startX] != -1)
                {
                    add = false;
                    res = true;
                }
                if (step > MapWidht * MapHeight)
                {
                    add = false;
                    res = false;
                }
            }
            return res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MapWidht = 8;
            MapHeight = 8;

            if(FindWave(xvovk, yvovk, xtar, ytar))
            {
                Lose();
                if (lang == "Ua")
                    MessageBox.Show("Ви програли :(");
                else if(lang == "Eng")
                    MessageBox.Show("Lose :(");
                Restart();
            } else
            {
                Victory();
                
                if (lang == "Ua")
                    MessageBox.Show("Ви перемогли!");
                else if (lang == "Eng")
                    MessageBox.Show("Victory!");
                comp.lvl = lvl;
                comp.isComp = true;
            }
        }
    }
}
