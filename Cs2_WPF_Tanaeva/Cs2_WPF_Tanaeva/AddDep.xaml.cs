using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для AddDep.xaml
    /// </summary>
    public partial class AddDep : Window
    {
        public AddDep()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Добавление нового департамента по нажатию соответствующей кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (tb_DepName.Text != null) //проверка, что не создаём департамент с пустыми параметрами
            //{
            //    Departament d = new Departament(tb_DepName.Text.ToString());
            //    MainWindow.dList.Add(d);
            //}
        }

    }
}
