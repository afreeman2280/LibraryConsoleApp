using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using DataAcessClassLibrary;
using System.Configuration;

namespace DataAccessClassLibrary
{

    public class DARole
    {
        public int ID;
       public string RoleName;
        public DARole role;
        //string connectionString = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        string connectionString = "Data Source=GDC-LAPTOP-308;Initial Catalog=Libary;Integrated Security=True";


        public DARole()
        {
            this.ID = 0;
            this.RoleName = "guest";
        }
        public DARole(int id, string role)
        {
            this.ID = id;
            this.RoleName = role;
        }
        public DARole GetRole(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("getRole", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

                        cmd.CommandTimeout = 30;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                role = new DARole
                                {
                                    ID = (int)reader["Id"],
                                    RoleName = (string)reader["Rolename"],


                                };

                            }
                        }

                    }
                }

                return role;
            }
            catch (Exception ex)
            {
                insertErrorLog(ex);
                return new DARole();
            }

        }
        public void insertErrorLog(Exception ex)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddErrorLogging", con))
                {
                    con.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@StackTrace", SqlDbType.VarChar)).Value = ex.StackTrace;
                    command.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar)).Value = ex.Message;
                    command.Parameters.Add(new SqlParameter("@Source", SqlDbType.VarChar)).Value = ex.Source;
                    command.Parameters.Add(new SqlParameter("@Url", SqlDbType.VarChar)).Value = "";
                    command.Parameters.Add(new SqlParameter("@LogDate", SqlDbType.DateTime)).Value = System.DateTime.Now;
                    command.ExecuteNonQuery();
                }
            }

        }

    }
}
