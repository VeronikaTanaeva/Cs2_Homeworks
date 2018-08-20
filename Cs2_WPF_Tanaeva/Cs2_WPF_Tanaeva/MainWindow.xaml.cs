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
using System.Windows.Shapes;

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable depsTable = new DataTable();
        DataTable empsTable = new DataTable();
        SqlDataAdapter depAdapter = new SqlDataAdapter();
        SqlDataAdapter empAdapter = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();

        public MainWindow()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //заполняем таблицы начальными данными
            //FillInit(connectionString);            

            #region Считываем департаменты

            SqlCommand commandDep = new SqlCommand(
                "SELECT ID, Name FROM Departaments",
                connection);

            depAdapter.SelectCommand = commandDep;

            
            depAdapter.Fill(depsTable);

            depsDataGrid.DataContext = depsTable.DefaultView;
            #endregion

            #region Считываем сотрудников
            SqlCommand commandEmp = new SqlCommand(@"SELECT ID, Name, Surname, Departament, Age, Salary 
                                                   FROM Employees",
                                                   connection);

            empAdapter.SelectCommand = commandEmp;

            empAdapter.Fill(empsTable);

            lw_Emps.DataContext = empsTable.DefaultView;
            #endregion

            #region Учим depAdapter командам
            
            //insert
            command = new SqlCommand(@"INSERT INTO Departaments (Name) 
                                    VALUES (@Name); SET @ID = @@IDENTITY;",
                                    connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "name");
            SqlParameter depParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            depParam.Direction = ParameterDirection.Output;
            depAdapter.InsertCommand = command;


            // update
            command = new SqlCommand(@"UPDATE Departaments SET Name=@Name
                                     WHERE ID = @ID", 
                                     connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            depParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            depAdapter.UpdateCommand = command;

            //delete
            command = new SqlCommand(@"DELETE FROM Departaments 
                                     WHERE ID = @ID",  
                                     connection);
            depParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            depParam.SourceVersion = DataRowVersion.Original;
            depAdapter.DeleteCommand = command;
            #endregion

            #region Учим emppAdapter командам

            //insert
            command = new SqlCommand(@"INSERT INTO Employees (Name, Surname, Departament, Age, Salary) 
                                     VALUES (@Name, @Surname, @Departament, @Age, @Salary); SET @ID = @@IDENTITY;",
                                     connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Departament", SqlDbType.NVarChar, 58, "Departament");
            command.Parameters.Add("@Age", SqlDbType.Int, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Int, -1, "Salary");
            SqlParameter empParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            empParam.Direction = ParameterDirection.Output;
            empAdapter.InsertCommand = command;

            // update
            command = new SqlCommand(@"UPDATE Employees SET Name=@Name, Surname=@Surname, Departament=@Departament, Age=@Age, Salary=@Salary
                                     WHERE ID = @ID",
                                     connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Departament", SqlDbType.NVarChar, 58, "Departament");
            command.Parameters.Add("@Age", SqlDbType.Int, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Int, -1, "Salary");
            command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            empParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            empAdapter.UpdateCommand = command;

            //delete
            command = new SqlCommand(@"DELETE FROM Employees 
                                     WHERE ID = @ID",
                                     connection);
            empParam = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            empParam.SourceVersion = DataRowVersion.Original;
            depAdapter.DeleteCommand = command;
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
            DataRow newRow = depsTable.NewRow();
            AddDep addWindow = new AddDep(newRow);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.Value)
            {
                depsTable.Rows.Add(addWindow.resultRow);
                depAdapter.Update(depsTable);
            }
        }

        /// <summary>
        /// Форма добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Emps_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = empsTable.NewRow();
            AddEmp addWindow = new AddEmp(newRow);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.Value)
            {
                empsTable.Rows.Add(addWindow.resultRow);
                empAdapter.Update(empsTable);
            }
        }

        /// <summary>
        /// Форма редактирования департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwDeps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView newRow = (DataRowView)depsDataGrid.SelectedItem;
            newRow.BeginEdit();

            AddDep addWindow = new AddDep(newRow.Row);
            addWindow.ShowDialog();
            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                depAdapter.Update(depsTable);
            }
            else
            {
                newRow.CancelEdit();
            }
        }

        /// <summary>
        /// Форма редактирования сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwEmps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow newRow = empsTable.NewRow();
            AddEmp addWindow = new AddEmp(newRow);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.Value)
            {
                empsTable.Rows.Add(addWindow.resultRow);
                empAdapter.Update(empsTable);
            }
        }
    }
}
