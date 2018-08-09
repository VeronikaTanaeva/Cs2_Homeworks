using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    /// <summary>
    /// Исключение для проверки параметров, с которыми создаём астероиды.
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string s)
        {
            MessageBox.Show(s);
        }
    }
}
