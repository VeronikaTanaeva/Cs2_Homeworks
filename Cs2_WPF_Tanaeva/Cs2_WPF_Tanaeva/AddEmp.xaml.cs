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
using System.Net;
using System.Net.Http;
using System.Web;

namespace Cs2_WPF_Tanaeva
{
    /// <summary>
    /// Логика взаимодействия для AddEmp.xaml - добавление\редактирование сотрудников
    /// </summary>
    public partial class AddEmp : Window
    {
        string url;

        public AddEmp(string url)
        {
            InitializeComponent();
            this.url = url;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Сохранение ноывх данных о сотруднике, если они были корректно введены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = new Employee
                {
                    name = tb_Name.ToString(),
                    surname = tb_Surname.ToString(),
                    depName = tb_DepName.ToString(),
                    age = int.Parse(tb_Age.ToString()),
                    salary = int.Parse(tb_Salary.ToString())
                };
                string obj = @"{
                            'Name' : {emp.name},
                            'Surname' : {emp.surname}
                            'Departament' : {emp.depName},
                            'Age' : {emp.age},
                            'Salary' : {emp.salary} }";

                HttpClient httpClient = new HttpClient();

                StringContent content = new StringContent(obj,
                                                          Encoding.UTF8,
                                                         "application/json");

                var res = httpClient.PostAsync(url, content).Result;
                this.DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Неправильно введены параметры!");
                this.DialogResult = false;
            }
        }

        /// <summary>
        /// Отмена внесённых изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
