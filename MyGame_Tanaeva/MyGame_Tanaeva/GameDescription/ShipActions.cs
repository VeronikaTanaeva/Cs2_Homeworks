using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{

    /// <summary>
    /// Делегат для обработки смерти корабля
    /// </summary>
    public delegate void Message();

    static partial class Game
    {
        /// <summary>
        /// Управление кораблём
        /// Движение - стрелки вверх и вниз
        /// Стрельба - ctrl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey && _bullets.Capacity <= bMax)
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(40, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Left) _ship.Left();
            if (e.KeyCode == Keys.Right) _ship.Right();
        }
        /// <summary>
        /// Выгрузка игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop(); //останавливаем таймер, вызывающий рисование на форме и обновление состояния объектов
            sw.Close(); //закрываем поток логирования
            Buffer.Graphics.DrawString("Конец!\nВы набрали " + score + " очков,\nсбивая астероиды!", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}
