using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAcessClassLibrary
{
    public class DABook
    {
      
       public int ID;
      public  string BookName { get; set; }
       public  string Author { get; set; }
        public int Role { get; set; }
        DABook Book;
        string connectionString = "Data Source=GDC-LAPTOP-308;Initial Catalog=Libary;Integrated Security=True";

      //  string connectionString = ConfigurationManager.ConnectionStrings["DBCONN"].ConnectionString;
        List<DABook> list = new List<DABook>();


        public DABook()
        {
            ID = 0;
            BookName = string.Empty;
            Author = string.Empty;
        }
        public DABook(int iD, string BookName, string Author)
        {
            this.ID = iD;
            this.BookName = BookName;
            this.Author = Author;
        }

        public List<DABook> GetAllBook()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetBooks", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 30;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DABook Book;

                            while (reader.Read())
                            {
                                Book = new DABook
                                {
                                    ID = (int)reader["Id"],
                                    BookName = (string)reader["Bookname"],
                                    Author = (string)reader["Author"],
                                   // Role = reader["RoleID"] is DBNull ? 1 : (int)reader["RoleID"],


                                };
                                list.Add(Book);

                            }
                        }

                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                insertErrorLog(ex); 
                return new List<DABook>();
            }

        }
        public DABook GetBook(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("GetBook", con))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

                        cmd.CommandTimeout = 30;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Book = new DABook
                                {
                                    ID = (int)reader["Id"],
                                    BookName = (string)reader["Bookname"],
                                    Author = (string)reader["Author"],
                                   // Role = reader["RoleID"] is DBNull ? 1 : (int)reader["RoleID"],


                                };

                            }
                        }

                    }
                }

                return Book;
            }
            catch (Exception ex)
            {
                insertErrorLog(ex);
                return new DABook();
            }

        }
        public void AddBook(string Bookname,string Author)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddBook", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Bookname", SqlDbType.VarChar)).Value = Bookname;
                        command.Parameters.Add(new SqlParameter("@Author", SqlDbType.VarChar)).Value = Author;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
            }

        }
        public void RemoveBook(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("RemoveBook", con))
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
        public void UpdateBookname(int id,string newBookname)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateBookName", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                        command.Parameters.Add(new SqlParameter("@Bookname", SqlDbType.VarChar)).Value = newBookname;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                insertErrorLog(ex);
            }

        }
        public void UpadateAuthor(int id, string newAuthor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateAuthor", con))
                    {
                        con.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                        command.Parameters.Add(new SqlParameter("@Author", SqlDbType.VarChar)).Value = newAuthor;
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

