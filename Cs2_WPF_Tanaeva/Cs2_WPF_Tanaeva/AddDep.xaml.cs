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
        public AddDep(Departament d)
        {
            InitializeComponent();
            d = new Departament();
            if (tb_DepName.Text != null)
                d.depName = tb_DepName.ToString();            
        }

        /// <summary>
        /// Добавление нового департамента по нажатию соответствующей кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_DepName.Text != null) //проверка, что не создаём департамент с пустыми параметрами
            {
                //DepList.Add(new Departament { depName = tb_DepName.Text.ToString() });
            }
        }

    }
}
