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
    /// Логика взаимодействия для AddEmp.xaml
    /// </summary>
    public partial class AddEmp : Window
    {
        public AddEmp(Employee e)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Добавление нового сотрудника по нажатию соответствующей кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddEmp_Click(object sender, RoutedEventArgs ev)
        {
            //if (tb_Age != null && tb_Name != null && tb_Surname != null && tb_Salary != null) //проверка, что не создаём сотрудника с пустыми параметрами
            //    foreach (var e in MainWindow.eList) //проверка, что не пытаемся добавить сотрудника в несуществующий департамент
            //        if (tb_Salary == e.depName)
            //        {
            //            Employee e = new Employee(tb_Name.ToString(), tb_Surname.ToString(), tb_DepName.ToString(), int.Parse(tb_Age.ToString()), int.Parse(tb_Salary.ToString()));
            //            eList.Add(e);
            //        }
        }
    }
}
