using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    static partial class Game
    {

        /// <summary>
        /// Отрисовка экрана заставки и игры
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(bgRed, bgGreen, bgBlue));

            foreach (BaseObject obj in _objs)
                obj.Draw();

            foreach (Asteroid a in _asteroids) a.Draw();

            foreach (Bullet b in _bullets) b.Draw();
            _ship?.Draw();
            _medkit.Draw();

            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy + "\tScore:" + score, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            if (Program.Count()) //не перерисовываем окно с игрой, если оно закрыто
                Buffer.Render();
        }
    }
}
