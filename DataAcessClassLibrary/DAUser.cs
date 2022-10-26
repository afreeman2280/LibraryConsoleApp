using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAcessClassLibrary
{
    public class DAUser
    {
        public enum Roles
        {
            guest,
            administrator,
            librarian,
            patron
        }
       public int ID;
      public  string UserName { get; set; }
  public      string password { get; set; }
        public int Role { get; set; }
        DAUser User;
        public DAUser()
        {
            ID = 0;
            UserName = string.Empty;
            password = string.Empty;
            Role = 1;
        }
        public DAUser(int iD, string userName, int role, string password)
        {
            this.ID = iD;
            this.UserName = userName;
            this.password = password;
            this.Role = role;
        }
      //  string connectionString = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        string connectionString = "Data Source=GDC-LAPTOP-308;Initial Catalog=Libary;Integrated Security=True";

        public List<DAUser> GetAllUser()
        {
            List<DAUser> list = new List<DAUser>();


            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUsers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 30;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DAUser user;

                            while (reader.Read())
                            {
                                user = new DAUser
                                {
                                    ID = reader["Id"] is DBNull ? 0: (int)reader["Id"],
                                    UserName = (string)reader["Username"],
                                    password = (string)reader["Password"],
                                    Role = reader["RoleID"] is DBNull ? 1 : (int)reader["RoleID"],


                                };
                                list.Add(user);

                            }
                        }

                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                insertErrorLog(ex);
                return new List<DAUser>();
            }

        }
        public DAUser GetUser(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("GetUser", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

                        cmd.CommandTimeout = 30;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                User = new DAUser
                                {
                                    ID = (int)reader["Id"],
                                    UserName = (string)reader["Username"],
                                    password = (string)reader["Password"],
                                    Role = reader["RoleID"] is DBNull ? 1 : (int)reader["RoleID"],


                                };

                            }
                        }

                    }
                }

                return User;
            }
            catch (Exception ex)
            {
                insertErrorLog(ex);
                return new DAUser();
            }

        }
        public void AddUser(int Role,string Username,string Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddUser", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int)).Value = Role;
                        command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = Username;
                        command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = Password;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
            }

        }
        public void RemoveUser(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("RemoveUser", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
            }

        }
        public void UpdateUsername(int id,string newUsername)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateUserName", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                        command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = newUsername;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
            }

        }
        public void UpadatePassword(int id, string newPassword)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdatePasswrd", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                        command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = newPassword;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
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

