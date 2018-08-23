using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplicationServer.Models
{
    public class DataEmps
    {
        private SqlConnection sqlConnection;

        /// <summary>
        /// Конструктор сотрудников
        /// </summary>
        public DataEmps()
        {

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                        Initial Catalog=Cs2_WPF;
                                        Integrated Security=True;
                                        ";

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// Считать из базы всех сотрудников
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmpList()
        {
            List<Employee> list = new List<Employee>();

            string sql = @"SELECT * FROM Employees";

            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new Employee()
                            {
                                name = reader["Name"].ToString(),
                                surname = reader["Surname"].ToString(),
                                depName = reader["Departament"].ToString(),
                                age = int.Parse(reader["Age"].ToString()),
                                salary = int.Parse(reader["Salary"].ToString())
                            });
                    }
                }

            }

            return list;
        }

        /// <summary>
        /// Отредактировать сотрудника с заданным ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Employee EditEmp(int Id)
        {
            string sql = $@"SELECT * FROM Employees WHERE Id={Id}";
            Employee temp = new Employee();
            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        temp = new Employee()
                        {
                            name = reader["Name"].ToString(),
                            surname = reader["Surname"].ToString(),
                            depName = reader["Departament"].ToString(),
                            age = int.Parse(reader["Age"].ToString()),
                            salary = int.Parse(reader["Salary"].ToString())
                        };
                    }
                }

            }
            return temp;
        }

        /// <summary>
        /// Добавить нового сотрудника
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool AddEmp(Employee emp)
        {
            try
            {
                string sqlAdd = $@" INSERT INTO People(Name, Surname, Departament, Age, Salary)
                               VALUES(N'{emp.name}',
                                      N'{emp.surname}',
                                      N'{emp.depName}',
                                      N'{emp.age}',
                                      N'{emp.salary}') ";

                using (var com = new SqlCommand(sqlAdd, sqlConnection))
                {
                    com.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}