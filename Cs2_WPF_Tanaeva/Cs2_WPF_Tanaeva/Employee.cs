using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs2_WPF_Tanaeva
{
    class Employee
    {
        public string name, surname, departament;
        public int age;
        public double salary;

        public Employee(string name, string surname, string departament, int age, int salary)
        {
            this.name = name;
            this.surname = surname;
            this.departament = departament;
            this.age = age;
            this.salary = salary;
        }

        /// <summary>
        /// Метод, возвращающий всю информацию о сотруднике в строке
        /// </summary>
        /// <returns></returns>
        public string EmpToString()
        {
            return name + surname + departament + age.ToString() + salary.ToString();
        }
    }
}
