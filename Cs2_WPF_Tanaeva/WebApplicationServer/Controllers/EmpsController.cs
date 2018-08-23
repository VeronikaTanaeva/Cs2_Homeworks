using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationServer.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebApplicationServer.Controllers
{
    public class EmpsController : ApiController
    {
        DataTable empsTable = new DataTable();
        SqlDataAdapter empAdapter = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();

        Employee[] emps = new Employee[Constants.eCount];

        public void FillInit(string connectionString)
        {
            Random r = new Random();
            int count = 0;
            for (int i = 1; i < Constants.dCount; i++)
            {
                for (int j = 1+count; j <= Constants.EmpsInDep; j++)
                {
                    emps[j - 1] = new Employee()
                    {
                        name = "Name_" + (j+count),
                        surname = "Surname_" + (j+count),
                        depName = "Departament_"+i,
                        age = r.Next(14, 70),
                        salary = (r.Next(20, 60)) * 1000
                    };
                    var sqlEmp = $@"INSERT INTO Employees(Name, Surname, Departament, Age, Salary) 
                          VALUES (N'{emps[j - 1].name}', '{emps[j - 1].surname}', '{emps[j - 1].depName}', '{emps[j - 1].age}', '{emps[j - 1].salary}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlEmp, connection);
                        command.ExecuteNonQuery();
                    }
                }
                count += Constants.EmpsInDep;
            }
        }

        private DataEmps data = new DataEmps();

        [Route("getemplist")]
        public List<Employee> Get()
        {
            return data.GetEmpList();
        }

        [Route("editemployee")]
        public Employee Get(int id)
        {
            return data.EditEmp(id);
        }

        [Route("addemployee")]
        public HttpResponseMessage Post([FromBody]Employee value)
        {
            if (data.AddEmp(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
