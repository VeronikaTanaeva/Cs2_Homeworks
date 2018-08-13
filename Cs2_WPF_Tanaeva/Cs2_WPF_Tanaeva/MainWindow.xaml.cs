using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Departament> dList = new ObservableCollection<Departament>();
        ObservableCollection<Employee> eList = new ObservableCollection<Employee>();

        public MainWindow()
        {
            InitializeComponent();

            FillInit();

            foreach (var d in dList)
                lw_Deps.ItemsSource += d.DepToString();
            foreach (var e in eList)
                lw_Emps.ItemsSource += e.EmpToString();
        }

        /// <summary>
        /// Начальное заполнение списков департаментов и сотрудников
        /// </summary>
        private void FillInit()
        {
            Random r = new Random();
            for (int i = 1; i <= dCount; i++)
            {
                Departament d = new Departament("Departament_" + i);
                dList.Add(d);
            }
            for (int i = 0; i <= eCount; i++)
            {
                Employee e = new Employee("Name_" + i, "Surname_" + i, dList[i % 10].depName, r.Next(14, 70), r.Next(20000, 60000));
                eList.Add(e);
            }
        }
        
        /// <summary>
        /// Редактирование выбранного департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_Departaments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //редактирование выбранного департамента
        }

        /// <summary>
        /// редактирование выбранного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_Empoyees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //редактирование выбранного сотрудника
        }

        /// <summary>
        /// Форма добавления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Deps_Click(object sender, RoutedEventArgs e)
        {
            AddDep addDep = new AddDep();
            addDep.Owner = this;
            addDep.Show();
        }

        /// <summary>
        /// Форма добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Emps_Click(object sender, RoutedEventArgs e)
        {
            AddEmp addEmp = new AddEmp();
            addEmp.Owner = this;
            addEmp.Show();
        }
    }
}
