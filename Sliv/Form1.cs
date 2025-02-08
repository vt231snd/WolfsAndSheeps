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
            button1.Visible = false;
            panel3.Visible = false;
            panel3Vis();
            radioButton1.Checked = true;
            radioButton1.BackColor = Color.Green;


            _levels = new Levels(LogDirectory, LevelsFileName);
            if (!File.Exists(LogDirectory + "/" + LevelsFileName))
            {
                _levels.CreateLog();
            }
        }
        private void panel3Vis()
        {
            b2.Enabled = false;
            b3.Enabled = false;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvlsEn(char lvls)
        {
            if (Char.GetNumericValue(lvls) == 1) { b2.Enabled = true; b1.BackColor = Color.Green; }
            else if (Char.GetNumericValue(lvls) == 2) { b3.Enabled = true; b2.BackColor = Color.Green; }
            else if (Char.GetNumericValue(lvls) == 3)
            {
                b3.BackColor = Color.Green;
                if(_formLanguage == "Ua") {
                    MessageBox.Show("Вы пройшли всі рівні. Чекайте оновлень)");
                } else if(_formLanguage == "Eng")
                {
                    MessageBox.Show("You complete all levels. Wait for updates)");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _currentPage++;
            panel3.Visible = true;
            panel2.Visible = false;
            button1.BringToFront();
            button1.Visible = true;

            if (_levels.ReadLog() != 0)
            {
                string s = _levels.ReadLog().ToString();
                char[] lvls = s.ToCharArray();
                for (int i = 0; i < lvls.Length; i++)
                {
                    lvlsEn(lvls[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                if (_levels.ReadLog() == 0) _levels.UpdateLog(_ground.CompleteLevelInstance._level.ToString());
                else _levels.UpdateLog(_levels.ReadLog().ToString() + _ground.CompleteLevelInstance._level.ToString());
                string s = _levels.ReadLog().ToString();
                char[] lvls = s.ToCharArray();

                for (int i = 0; i < lvls.Length; i++)
                {
                    lvlsEn(lvls[i]);
                }
                panel1.Controls.Remove(_ground);
                panel3.Visible = true;
                panel2.Visible = false;
                button1.BringToFront();
                button1.Visible = true;
            }

        }

        private void b1_Click(object sender, EventArgs e)
        {
            _currentPage++;
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "b1": _ground = new Ground(1, 4, 2, 5, 4, 6, _formLanguage); break;
                case "b2": _ground = new Ground(2, 3, 3, 5, 4, 6, _formLanguage); break;
                case "b3": _ground = new Ground(3, 2, 2, 3, 6, 6, _formLanguage); break;
            }

            panel1.Controls.Add(_ground);
            _ground.Dock = DockStyle.Fill;
            _ground.BringToFront();
            button1.BringToFront();
            button1.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
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
            else if (radioButton2.Checked)
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
            _language = new Lang(_formLanguage);
        }
    }
}
