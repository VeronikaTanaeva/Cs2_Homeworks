using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationServer.Models;
using WebApplicationServer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplicationServer.Controllers
{
    public class DepsController : ApiController
    {
        DataTable depsTable = new DataTable();
        SqlDataAdapter depAdapter = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();

        Departament[] deps = new Departament[Constants.dCount];

        /// <summary>
        /// Заполнение списков департаментов и сотрудников тестовыми данными
        /// </summary>
        public void FillInit(string connectionString)
        {
            try
            {
                Random r = new Random();

                for (int i = 1; i <= Constants.dCount; i++)
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

        private DataDeps data = new DataDeps();

        [Route("getdeplist")]
        public IEnumerable<Departament> Get()
        {
            return data.GetDepList();
        }
        
        [Route("editdepartament")]
        public Departament Get(int id)
        {
            return data.EditDep(id);
        }

        [Route("addDepartament")]
        public HttpResponseMessage Post([FromBody]Departament value)
        {
            if (data.AddDep(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
