using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sliv
{
    public partial class Form1 : Form
    {
        private Ground _ground;
        private int _currentPage = 0;
        private const string LogDirectory = "Log";
        private const string LevelsFileName = "levels.txt";
        private Levels _levels;
        private string _formLanguage = "Ua";
        private Lang _language;
        public Form1()
        {
            InitializeComponent();
            InitializeForm();
            InitializeLevels();
        }
        private void InitializeForm()
        {
            button1.Visible = false;
            panel3.Visible = false;
            DisableLevelButtons();
            radioButton1.Checked = true;
            radioButton1.BackColor = Color.Green;
            radioButton1.CheckedChanged += LanguageRadioButton_CheckedChanged;
            radioButton2.CheckedChanged += LanguageRadioButton_CheckedChanged;
            b1.Click += LevelButton_Click;
            b2.Click += LevelButton_Click;
            b3.Click += LevelButton_Click;
        }
        private void LevelButton_Click(object sender, EventArgs e)
        {
            _currentPage++;
            if (sender is Button levelButton)
            {
                switch (levelButton.Name)
                {
                    case "b1":
                        _ground = new Ground(1, 4, 2, 5, 4, 6, _formLanguage);
                        break;
                    case "b2":
                        _ground = new Ground(2, 3, 3, 5, 4, 6, _formLanguage);
                        break;
                    case "b3":
                        _ground = new Ground(3, 2, 2, 3, 6, 6, _formLanguage);
                        break;
                }

                panel1.Controls.Add(_ground);
                _ground.Dock = DockStyle.Fill;
                _ground.BringToFront();
                button1.BringToFront();
                button1.Visible = true;
            }
        }

        private void LanguageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SetUkrainianLanguage();
            }
            else if (radioButton2.Checked)
            {
                SetEnglishLanguage();
            }
            _language = new Lang(_formLanguage);
        }
        private void SetUkrainianLanguage()
        {
            groupBox1.Text = "Мова";
            radioButton1.Text = "Українська";
            radioButton2.Text = "Англійська";
            label1.Text = "Рівні";
            button2.Text = "Грати";
            Exit.Text = "Вихід";

            radioButton2.BackColor = SystemColors.Control;
            radioButton1.BackColor = Color.Green;
            _formLanguage = "Ua";
        }

        private void SetEnglishLanguage()
        {
            groupBox1.Text = "Language";
            radioButton1.Text = "Ukrainian";
            radioButton2.Text = "English";
            label1.Text = "Levels";
            button2.Text = "Play";
            Exit.Text = "Exit";

            radioButton1.BackColor = SystemColors.Control;
            radioButton2.BackColor = Color.Green;
            _formLanguage = "Eng";
        }
        private void InitializeLevels()
        {
            _levels = new Levels(LogDirectory, LevelsFileName);
            if (!File.Exists(Path.Combine(LogDirectory, LevelsFileName)))
            {
                _levels.CreateLog();
            }
        }
        private void DisableLevelButtons()
        {
            b2.Enabled = false;
            b3.Enabled = false;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnableLevels(string completedLevels)
        {
            if (completedLevels.Contains('1'))
            {
                b2.Enabled = true;
                b1.BackColor = Color.Green;
            }
            if (completedLevels.Contains('2'))
            {
                b3.Enabled = true;
                b2.BackColor = Color.Green;
            }
            if (completedLevels.Contains('3'))
            {
                b3.BackColor = Color.Green;
                string message = _formLanguage == "Ua"
                    ? "Ви пройшли всі рівні. Чекайте оновлень :)"
                    : "You've completed all levels. Stay tuned for updates :)";
                MessageBox.Show(message);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            _currentPage++;
            panel3.Visible = true;
            panel2.Visible = false;
            button1.BringToFront();
            button1.Visible = true;

            string levelsCompleted = _levels.ReadLog().ToString() ?? string.Empty;
            EnableLevels(levelsCompleted);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _currentPage--;
            if (_currentPage == 0)
            {
                button1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else if (_currentPage == 1)
            {
                string currentLevel = _ground?.CompleteLevelInstance?._level.ToString();
                if (!string.IsNullOrEmpty(currentLevel))
                {
                    _levels.UpdateLog(_levels.ReadLog() + currentLevel);
                }

                string levelsCompleted = _levels.ReadLog().ToString() ?? string.Empty;
                EnableLevels(levelsCompleted);

                panel1.Controls.Remove(_ground);
                panel3.Visible = true;
                panel2.Visible = false;
                button1.BringToFront();
                button1.Visible = true;
            }
        }
    }
}
