using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Cs2_WPF_Tanaeva
{
    public class Employee: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string name { get; set; }
        public string surname { get; set; }
        public string depName { get; set; }
        public int age { get; set; }        
        public double salary { get; set; }

        public Employee()
        {
            this.name = name;
            this.surname = surname;
            this.depName = depName;
            this.age = age;
            this.salary = salary;
        }

        public void LoadData()
        {

        }

        /// <summary>
        /// Метод, возвращающий всю информацию о сотруднике в строке
        /// </summary>
        /// <returns></returns>
        //public string EmpToString()
        //{
        //    return name + surname + departament + age.ToString() + salary.ToString();
        //}
    }
}
