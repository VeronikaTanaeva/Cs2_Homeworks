using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Departament> DepList { get; set; }
        public ObservableCollection<Employee> EmpList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DepList = new ObservableCollection<Departament>();
            EmpList = new ObservableCollection<Employee>();
            FillInit();

            lw_Deps.ItemsSource = DepList;
            lw_Emps.ItemsSource = EmpList;
        }


        /// <summary>
        /// Начальное заполнение списков департаментов и сотрудников
        /// </summary>
        public void FillInit()
        {
            Random r = new Random();


            for (int i = 1; i <= dCount; i++)
            {
                DepList.Add(new Departament() { depName = "Departament_" + i });
            }
            for (int i = 1; i <= eCount; i++)
            {
                EmpList.Add(new Employee() { name = "Name_" + i, surname = "Surname_" + i, depName = DepList[i % 10].depName, age = r.Next(14, 70), salary = (r.Next(20, 60)) * 1000 });

            }
        }

        /// <summary>
        /// Форма добавления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Deps_Click(object sender, RoutedEventArgs e)
        {
            new AddDep(lw_Deps.SelectedItem as Departament).ShowDialog();
        }

        /// <summary>
        /// Форма добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Emps_Click(object sender, RoutedEventArgs e)
        {
            new AddEmp(lw_Deps.SelectedItem as Employee).ShowDialog();
        }

        /// <summary>
        /// Форма редактирования департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwDeps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new EditDep(lw_Deps.SelectedItem as Departament).ShowDialog();
        }

        /// <summary>
        /// Форма редактирования сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwEmps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new EditEmp(lw_Emps.SelectedItem as Employee).ShowDialog();
        }
    }
}
