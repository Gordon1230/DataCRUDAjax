using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace DataCRUDAjax.Models
{
    public class EmployeeDB
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //Return list of all Employees
        public List<Employee> ListAll()
        {
            List<Employee> lst = new List<Employee>();
            using(SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Employee
                    {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeId"]),
                        Name = rdr["Name"].ToString(),
                        Age = Convert.ToInt32(rdr["Age"]),
                        State = rdr["State"].ToString(),
                        Country = rdr["Country"].ToString(),
                    });
                }
            }
            return lst;
        }

        public int Add(Employee emp) {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@Age", emp.Age);
                i = com.ExecuteNonQuery();
            }
                return i;
        }
    }
}