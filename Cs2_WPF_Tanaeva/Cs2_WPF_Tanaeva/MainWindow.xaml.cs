using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;using System.Net.Http;
using System.Net.Http.Headers;

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //заполняем таблицы начальными данными
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection connection = new SqlConnection(connectionString);
            //FillInit(connectionString);       

            #region Считываем департаменты            

            string urlDep = @"http://localhost:52969/getdeplist";
            depsDataGrid.DataContext = urlDep;
            #endregion

            #region Считываем сотрудников

            string urlEmp = @"http://localhost:52969/GetEmpList";
            empsDataGrid.DataContext = urlEmp;
            #endregion
        }


        /// <summary>
        /// Заполнение списков департаментов и сотрудников тестовыми данными
        /// </summary>
        public void FillInit(string connectionString)
        {
            try
            {
                Random r = new Random();
                
                for (int i = 1; i <= dCount; i++)
                {
                    var dep = new Departament()
                    {
                        depName = "Departament_" + i
                    };
                    var sqlDep = $@"INSERT INTO Departaments(Name) 
                          VALUES (N'{dep.depName}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlDep, connection);
                        command.ExecuteNonQuery();
                    }

                    for (int j = 1; j <= EmpsInDep; j++)
                    {
                        var emp = new Employee()
                        {
                            name = "Name_" + j,
                            surname = "Surname_" + j,
                            depName = dep.depName,
                            age = r.Next(14, 70),
                            salary = (r.Next(20, 60)) * 1000
                        };
                        var sqlEmp = $@"INSERT INTO Employees(Name, Surname, Departament, Age, Salary) 
                          VALUES (N'{emp.name}', '{emp.surname}', '{emp.depName}', '{emp.age}', '{emp.salary}')";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlEmp, connection);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("exit");
            }                     

        }

        /// <summary>
        /// Форма добавления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Deps_Click(object sender, RoutedEventArgs e)
        {
            string url = @"http://localhost:33796/adddepartament";
            AddDep addWindow = new AddDep(url);
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Форма добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Emps_Click(object sender, RoutedEventArgs e)
        {
            string url = @"http://localhost:33796/addemployee";
            AddEmp addWindow = new AddEmp(url);
            addWindow.ShowDialog();            
        }

        /// <summary>
        /// Форма редактирования департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwDeps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string url = @"http://localhost:33796/editdepartament";
            AddDep addWindow = new AddDep(url);
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Форма редактирования сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwEmps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string url = @"http://localhost:33796/editemployee";
            AddEmp addWindow = new AddEmp(url);
            addWindow.ShowDialog();
        }
    }
}
