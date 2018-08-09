using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame_Tanaeva
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Форма с заставкой и меню
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// По нажатию кнопки "Начать игру" открывается новое окно с игрой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, EventArgs e)
        {

            Form2 form = new Form2();
            form.Width = 800;
            form.Height = 600;
            Game.Init(form);
            Game.Draw();
            form.ShowDialog();
        }

        /// <summary>
        /// Прототип таблицы лидеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            StreamReader records = new StreamReader(@"Docs\records.txt", true);
            string str="";
            do
            {
                str += records.ReadLine() + "\n\r";
            } while (!records.EndOfStream);

            MessageBox.Show(str);
            records.Close();
        }

        /// <summary>
        /// По нажатию кнопки "Выход" приложение закрывается
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {            
            Application.Exit();            
        }
    }
}
