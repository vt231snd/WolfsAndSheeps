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
        Ground Ground;
        int page = 0;
        string Path = "Log";
        string FilePath = "levels.txt";
        Levels levels;
        string Formlang = "Ua";
        Lang lang;
        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;
            panel3.Visible = false;
            panel3Vis();
            radioButton1.Checked = true;
            radioButton1.BackColor = Color.Green;


            levels = new Levels(Path, FilePath);
            if (!File.Exists(Path + "/" + FilePath))
            {
                levels.CreateLog();
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
                if(Formlang == "Ua") {
                    MessageBox.Show("Вы пройшли всі рівні. Чекайте оновлень)");
                } else if(Formlang == "Eng")
                {
                    MessageBox.Show("You complete all levels. Wait for updates)");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            page++;
            panel3.Visible = true;
            panel2.Visible = false;
            button1.BringToFront();
            button1.Visible = true;

            if (levels.ReadLog() != 0)
            {
                string s = levels.ReadLog().ToString();
                char[] lvls = s.ToCharArray();
                for (int i = 0; i < lvls.Length; i++)
                {
                    lvlsEn(lvls[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            page--;
            if (page == 0)
            {
                button1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else if (page == 1)
            {
                if (levels.ReadLog() == 0) levels.UpdateLog(Ground.comp.lvl.ToString());
                else levels.UpdateLog(levels.ReadLog().ToString() + Ground.comp.lvl.ToString());
                string s = levels.ReadLog().ToString();
                char[] lvls = s.ToCharArray();

                for (int i = 0; i < lvls.Length; i++)
                {
                    lvlsEn(lvls[i]);
                }
                panel1.Controls.Remove(Ground);
                panel3.Visible = true;
                panel2.Visible = false;
                button1.BringToFront();
                button1.Visible = true;
            }

        }

        private void b1_Click(object sender, EventArgs e)
        {
            page++;
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "b1": Ground = new Ground(1, 4, 2, 5, 4, 6, Formlang); break;
                case "b2": Ground = new Ground(2, 3, 3, 5, 4, 6, Formlang); break;
                case "b3": Ground = new Ground(3, 2, 2, 3, 6, 6, Formlang); break;
            }

            panel1.Controls.Add(Ground);
            Ground.Dock = DockStyle.Fill;
            Ground.BringToFront();
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
                Formlang = "Ua";
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
                Formlang = "Eng";
            }
            lang = new Lang(Formlang);
        }
    }
}
