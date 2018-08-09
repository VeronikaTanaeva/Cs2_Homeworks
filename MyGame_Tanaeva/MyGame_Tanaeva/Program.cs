using System;
using System.Windows.Forms;

namespace MyGame_Tanaeva
{

    class Program
    {      

        static void Main(string[] args)
        {
            Form1 form = new Form1();
            form.Width = 800;
            form.Height = 600;

            //не сообразила, как перестать отрисовывать заставку, поэтому открываю новое окно с уже в нём рисую игру
            if (!Count())
            {
                SplashScreen.Init(form);
                SplashScreen.Draw();
            }
            form.ShowDialog();
        }

        /// <summary>
        /// Метод, определяющий, открыто ли окно с игрой
        /// </summary>
        /// <returns></returns>
        public static bool Count()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form2")
                    return true;
            }
            return false;
        }
    }
}
