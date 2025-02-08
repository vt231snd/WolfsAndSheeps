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
        private TileType[,] _map;
        private const int ButtonSize = 100;
        private const int MapWidth = 8;
        private const int MapHeight = 8;
        private const int XOffset = 500;
        private const int YOffset = 52;
        public Ground()
        {
            InitializeComponent();
            InitializeMap();
        }

        public Ground(int level, int fenceCount, int targetX, int targetY, int wolfX, int wolfY, string language)
        {
            InitializeComponent();
            _level = level;
            _fenceCount = fenceCount;
            _targetX = targetX;
            _targetY = targetY;
            _wolfX = wolfX;
            _wolfY = wolfY;
            _language = language;
            _remainingFence = fenceCount;

            InitializeMap();
            UpdateLanguage(_language);
            label2.Text = _remainingFence.ToString();
        }

        private void InitializeMap()
        {
            _map = new TileType[MapHeight, MapWidth];

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (IsOnBorder(x, y))
                        _map[y, x] = TileType.Wall;
                    else
                        _map[y, x] = TileType.Empty;
                }
            }

            _map[_wolfY, _wolfX] = TileType.Wolf;
            _map[_targetY, _targetX] = TileType.Target;
        }
        private bool IsOnBorder(int x, int y)
        {
            return x == 0 || x == MapWidth - 1 || y == 0 || y == MapHeight - 1;
        }
        private void Ground_Load(object sender, EventArgs e)
        {
            Generate();
        }

        private void UpdateLanguage(string language)
        {
            if (language == "Ua")
            {
                button3.Text = "Перевірити";
                label2.Text = "Залишилось паркану: " + _remainingFence;
            }
            else if (language == "Eng")
            {
                button3.Text = "Check";
                label2.Text = "Fence left: " + _remainingFence;
            }
        }

        public void Restart()
        {
            InitializeMap();
            _remainingFence = _fenceCount;
            UpdateLanguage(_language);
            Generate();
        }

        public void DisplayLoseScreen()
        {
            SetupControl();
            CreateMapButtons(isVictory: false);
            this.Controls.Add(_control);
        }

        public void DisplayVictoryScreen()
        {
            SetupControl();
            CreateMapButtons(isVictory: true);
            this.Controls.Add(_control);
        }

        private void SetupControl()
        {
            _control.Controls.Clear();
            _control.Dock = DockStyle.Fill;
        }

        private void Generate()
        {
            SetupControl();
            CreateMapButtons(isVictory: false);
            this.Controls.Add(_control);
        }

        private void CreateMapButtons(bool isVictory)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    var button = CreateMapButton(x, y, isVictory);
                    _control.Controls.Add(button);
                }
            }
        }

        private Button CreateMapButton(int x, int y, bool isVictory)
        {
            var button = new Button
            {
                Size = new Size(ButtonSize, ButtonSize),
                Location = new Point(x * ButtonSize + XOffset, y * ButtonSize + YOffset),
                BackColor = Color.Green,
                FlatStyle = FlatStyle.Flat,
                Name = $"{x}{y}",
                Tag = new Point(x, y),
                BackgroundImageLayout = ImageLayout.Zoom,
                Enabled = !isVictory
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += Button_Click;

            switch (_map[y, x])
            {
                case TileType.Wall:
                    button.BackgroundImage = Properties.Resources.R;
                    break;
                case TileType.Target:
                    button.BackgroundImage = isVictory ? Properties.Resources.ov : Properties.Resources.o;
                    break;
                case TileType.Wolf:
                    button.BackgroundImage = isVictory ? Properties.Resources.vo : Properties.Resources.v;
                    break;
                default:
                    break;
            }

            return button;
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


            UpdateLanguage(_language);
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
            if(FindWave(_wolfX, _wolfY, _targetX, _targetY))
            {
                DisplayLoseScreen();
                if (_language == "Ua")
                    MessageBox.Show("Ви програли :(");
                else if(_language == "Eng")
                    MessageBox.Show("Lose :(");
                Restart();
            } else
            {
                DisplayVictoryScreen();
                
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
