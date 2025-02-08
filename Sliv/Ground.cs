using System;
using System.Collections.Generic;
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
            if (sender is Button clickedButton && clickedButton.Tag is Point coordinates)
            {
                int x = coordinates.X;
                int y = coordinates.Y;

                if (IsOnBorder(x, y))
                {
                    return;
                }

                if (_map[y, x] == TileType.Empty && _remainingFence > 0)
                {
                    PlaceFence(clickedButton, x, y);
                }
                else if (_map[y, x] == TileType.Wall)
                {
                    RemoveFence(clickedButton, x, y);
                }

                UpdateFenceLabel();
            }
        }
        private void PlaceFence(Button button, int x, int y)
        {
            _map[y, x] = TileType.Wall;
            button.BackgroundImage = Properties.Resources.R;
            button.BackgroundImageLayout = ImageLayout.Zoom;
            _remainingFence--;
        }

        private void RemoveFence(Button button, int x, int y)
        {
            _map[y, x] = TileType.Empty;
            button.BackgroundImage = null;
            button.BackColor = Color.Green;
            _remainingFence++;
        }
        private void UpdateFenceLabel()
        {
            if (_language == "Ua")
            {
                label2.Text = $"Залишилось паркану: {_remainingFence}";
            }
            else if (_language == "Eng")
            {
                label2.Text = $"Fence left: {_remainingFence}";
            }
        }
        public bool FindWave(int startX, int startY, int targetX, int targetY)
        {
            int[,] waveMap = InitializeWaveMap();
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(targetX, targetY));
            waveMap[targetY, targetX] = 0;

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                int x = current.X;
                int y = current.Y;
                int step = waveMap[y, x];

                foreach (var neighbor in GetNeighbors(x, y))
                {
                    int nx = neighbor.X;
                    int ny = neighbor.Y;

                    if (waveMap[ny, nx] == -1)
                    {
                        waveMap[ny, nx] = step + 1;
                        queue.Enqueue(new Point(nx, ny));

                        if (nx == startX && ny == startY)
                        {
                            // Path found
                            return true;
                        }
                    }
                }
            }

            // No path found
            return false;
        }

        private int[,] InitializeWaveMap()
        {
            int[,] waveMap = new int[MapHeight, MapWidth];
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    waveMap[y, x] = _map[y, x] == TileType.Wall ? -2 : -1;
                }
            }
            return waveMap;
        }

        private IEnumerable<Point> GetNeighbors(int x, int y)
        {
            var possibleMoves = new List<Point>
            {
                new Point(x, y - 1), // Up
                new Point(x + 1, y), // Right
                new Point(x, y + 1), // Down
                new Point(x - 1, y)  // Left
            };

            foreach (var point in possibleMoves)
            {
                int nx = point.X;
                int ny = point.Y;
                if (nx >= 0 && nx < MapWidth && ny >= 0 && ny < MapHeight && _map[ny, nx] != TileType.Wall)
                {
                    yield return point;
                }
            }
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
