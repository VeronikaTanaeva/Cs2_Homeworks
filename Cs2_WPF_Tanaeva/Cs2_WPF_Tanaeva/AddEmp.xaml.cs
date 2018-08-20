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
    /// Логика взаимодействия для AddEmp.xaml - добавление\редактирование сотрудников
    /// </summary>
    public partial class AddEmp : Window
    {
        public DataRow resultRow { get; set; }
        public AddEmp(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_Name.Text = resultRow["Name"].ToString();
            tb_Surname.Text = resultRow["Surame"].ToString();
            tb_DepName.Text = resultRow["Departament"].ToString();
            tb_Age.Text = resultRow["Age"].ToString();
            tb_Salary.Text = resultRow["Salary"].ToString();
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
                resultRow["Name"] = tb_Name.Text;
                resultRow["Surname"] = tb_Surname.Text;
                resultRow["Departament"] = tb_DepName.Text;
                resultRow["Age"] = tb_Age.Text;
                resultRow["Salary"] = tb_Salary.Text;
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
