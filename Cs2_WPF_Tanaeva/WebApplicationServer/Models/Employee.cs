using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationServer.Models
{
    public class Employee
    {
        //public int iD { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string depName { get; set; }
        public int age { get; set; }
        public int salary { get; set; }

        //public Employee()
        //{
        //    this.name = name;
        //    this.surname = surname;
        //    this.depName = depName;
        //    this.age = age;
        //    this.salary = salary;
        //}

        //public void LoadData() { }
    }
}