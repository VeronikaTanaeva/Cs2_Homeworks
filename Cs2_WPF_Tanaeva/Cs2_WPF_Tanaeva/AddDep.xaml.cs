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
    /// Логика взаимодействия для AddDep.xaml - формы добавления\редактирования департаментов
    /// </summary>
    public partial class AddDep : Window
    {
        public DataRow resultRow { get; set; }
        string url;
        public AddDep(string url)
        {
            InitializeComponent();
            this.url = url;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Сохранение ноывх данных о департаменте, если они были корректно введены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Departament dep = new Departament { depName = tb_DepName.Text.ToString() };
                string obj = @"{'Name' : {dep.depName}}";

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
