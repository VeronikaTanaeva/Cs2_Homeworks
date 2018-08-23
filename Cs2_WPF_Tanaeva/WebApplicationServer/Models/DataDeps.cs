using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplicationServer.Models
{
    public class DataDeps
    {
        private SqlConnection sqlConnection;

        /// <summary>
        /// Конструктор департаментов
        /// </summary>
        public DataDeps()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                        Initial Catalog=Cs2_WPF;
                                        Integrated Security=True;
                                        ";

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// Считать из базы все департаменты
        /// </summary>
        /// <returns></returns>
        public List<Departament> GetDepList()
        {
            List<Departament> list = new List<Departament>();

            string sql = @"SELECT * FROM Departaments";

            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new Departament()
                            {
                                depName=reader["Name"].ToString()
                            });
                    }
                }

            }

            return list;
        }
        
        /// <summary>
        /// Отредактировать департамент с заданым ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Departament EditDep(int Id)
        {
            string sql = $@"SELECT * FROM People WHERE Id={Id}";
            Departament temp = new Departament();
            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        temp = new Departament()
                        {
                            depName = reader["Name"].ToString()
                        };
                    }
                }

            }
            return temp;
        }

        /// <summary>
        /// Добавить новый департамент
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool AddDep(Departament dep)
        {
            try
            {
                string sqlAdd = $@" INSERT INTO Departament(Name)
                               VALUES(N'{dep.depName}) ";                

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