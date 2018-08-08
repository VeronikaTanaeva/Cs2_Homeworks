using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    static partial class Game
    {
        /// <summary>
        /// Ведение журнала событий
        /// </summary>
        public static StreamWriter sw;
        public static Action<string, StreamWriter> actionTarget = new Action<string, StreamWriter>(JournalMessage);

        /// <summary>
        /// Описание журнала событий (столкновений объектов)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sw"></param>
        static void JournalMessage(string msg, StreamWriter sw)
        {
            sw.WriteLine(msg);
            sw.Flush();
        }
    }
}
