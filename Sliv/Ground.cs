using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sliv
{
    public partial class Ground : UserControl
    {
        private int _level;
        private int _fenceCount;
        private int _remainingFence;
        private int _targetX;
        private int _targetY;
        private int _wolfX;
        private int _wolfY;
        private string _language = "Ua";
        private readonly Control _control = new Control();
        public CompleteLevel CompleteLevelInstance { get; } = new CompleteLevel();
        private int[,] _map;
        private const int ButtonSize = 100;
        private const int MapWidth = 8;
        private const int MapHeight = 8;
        public Ground()
        {
            InitializeComponent();
        }
        public Ground(int _level, int _fenceCount, int _targetX, int _targetY, int _wolfX, int _wolfY, string _language)
        {
            InitializeComponent();
            this._level = _level;
            this._fenceCount = _fenceCount;
            this._targetX = _targetX;
            this._targetY = _targetY;
            this._wolfY = _wolfY;
            this._wolfX = _wolfX;
            this._language = _language;
            _map[_wolfX, _wolfY] = 3;
            _map[_targetX, _targetY] = 2;
            _remainingFence = _fenceCount;
            label2.Text = _remainingFence.ToString();

            swichLang(_language);
        }

        public void swichLang(string _language)
        {
            if (_language == "Ua")
            {
                button3.Text = "Перевірити";
                label2.Text = "Залишилось паркану: " + _remainingFence;
            } else if (_language == "Eng")
            {
                button3.Text = "Check";
                label2.Text = "_remainingFence left: " + _remainingFence;
            }
        }
        private void Ground_Load(object sender, EventArgs e)
        {
            Generete();
        }

        public void Restart()
        {
            _map = new int[,]
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
            _map[_wolfX, _wolfY] = 3;
            _map[_targetX, _targetY] = 2;
            _remainingFence = _fenceCount;
            swichLang(_language);
            Generete();
        }

        public void Lose()
        {
            _control.Controls.Clear();
            _control.Dock = DockStyle.Fill;
            int rows = _map.GetLength(0);
            int cols = _map.GetLength(1);
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
                    if (_map[i, j] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.oo;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.vv;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    button.TabIndex = _map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    _control.Controls.Add(button);
                }
            }
            this.Controls.Add(_control);
        }

        public void Victory()
        {
            _control.Controls.Clear();
            _control.Dock = DockStyle.Fill;
            int rows = _map.GetLength(0);
            int cols = _map.GetLength(1);
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
                    if (_map[i, j] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.ov;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.vo;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    button.TabIndex = _map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    button.Enabled = false;
                    _control.Controls.Add(button);
                }
            }
            this.Controls.Add(_control);
        }

        public void Generete()
        {
            _control.Controls.Clear();
            _remainingFence = _fenceCount;
            _control.Dock = DockStyle.Fill;
            int rows = _map.GetLength(0);
            int cols = _map.GetLength(1);
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
                    if (_map[j, i] == 1)
                    {
                        button.BackgroundImage = Properties.Resources.R;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 2)
                    {
                        button.BackgroundImage = Properties.Resources.o;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (_map[j, i] == 3)
                    {
                        button.BackgroundImage = Properties.Resources.v;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                    } 
                    button.TabIndex = _map[j, i];
                    button.Click += new EventHandler(Button_Click);
                    button.Name = $"{j}{i}";
                    _control.Controls.Add(button);
                }
            }
            this.Controls.Add(_control);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Name.Contains("0") || clickedButton.Name.Contains("7"))
            {
                return;
            }
            if (clickedButton.TabIndex == 0 && _remainingFence != 0)
            {
                clickedButton.TabIndex = 1;
                clickedButton.BackgroundImage = Properties.Resources.R;
                clickedButton.BackgroundImageLayout = ImageLayout.Zoom;
                _map[Convert.ToInt32(clickedButton.Name) % 10, Convert.ToInt32(clickedButton.Name) / 10] = 1;
                _remainingFence--;
            }
            else if (clickedButton.TabIndex == 1)
            {
                clickedButton.TabIndex = 0;
                clickedButton.BackgroundImage = null;
                clickedButton.BackColor = Color.Green;
                _map[Convert.ToInt32(clickedButton.Name) % 10, Convert.ToInt32(clickedButton.Name) / 10] = 0;
                _remainingFence++;
            }


            swichLang(_language);
        }

        public bool FindWave(int startX, int startY, int targetX, int targetY)
        {
            bool add = true;
            bool res = true;
            int[,] cMap = new int[MapHeight, MapWidth];
            int x, y, step = 0;
            for (y = 0; y < MapHeight; y++)
                for (x = 0; x < MapWidth; x++)
                {
                    if (_map[y, x] == 1)
                        cMap[y, x] = -2;
                    else
                        cMap[y, x] = -1;
                }
            cMap[targetY, targetX] = 0;
            while (add == true)
            {
                for (y = 0; y < MapWidth; y++)
                    for (x = 0; x < MapHeight; x++)
                    {
                        if (cMap[x, y] == step)
                        {
                            if (y - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (x - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (y + 1 < MapWidth && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
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
                if (step > MapWidth * MapHeight)
                {
                    add = false;
                    res = false;
                }
            }
            return res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MapWidth = 8;
            MapHeight = 8;

            if(FindWave(_wolfX, _wolfY, _targetX, _targetY))
            {
                Lose();
                if (_language == "Ua")
                    MessageBox.Show("Ви програли :(");
                else if(_language == "Eng")
                    MessageBox.Show("Lose :(");
                Restart();
            } else
            {
                Victory();
                
                if (_language == "Ua")
                    MessageBox.Show("Ви перемогли!");
                else if (_language == "Eng")
                    MessageBox.Show("Victory!");
                CompleteLevelInstance._level = _level;
                CompleteLevelInstance.isComp = true;
            }
        }
    }
}
