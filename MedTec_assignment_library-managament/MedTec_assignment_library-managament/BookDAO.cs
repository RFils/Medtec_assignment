using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTec_assignment_library_managament
{
    public class BookDAO
    {
        string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        public DataTable GetBookList()
        {
            DataTable dtBook = new DataTable();

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string SelectCommand = "Select * from Books";
                SqlCommand select = new SqlCommand(SelectCommand, con);

                SqlDataReader reader = select.ExecuteReader();
                dtBook.Load(reader);
            }
            con.Close();

            return dtBook;
        }

        public void Save(Book book)
        {
            try
            {
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string InsertCommand = $"Insert into Books (Title, Author, NrPages, Notes) VALUES ('{book.Title}','{book.Author}',{book.TotalPages},'{book.Notes}')";
                    SqlCommand insert = new SqlCommand(InsertCommand, con);
                    insert.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Book book, bool _delete = false)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open && book != null)
            {
                if (_delete)
                {
                    string DeleteCommand = $"DELETE FROM Books WHERE Id = {book.Id}";
                    SqlCommand delete = new SqlCommand(DeleteCommand, con);
                    delete.ExecuteNonQuery();
                }
                else
                {
                    string UpdateCommand = $"UPDATE Books SET Title = '{book.Title}', Author = '{book.Author}', NrPages = {book.TotalPages}, Notes = '{book.Notes}' WHERE Id = {book.Id}";
                    SqlCommand update = new SqlCommand(UpdateCommand, con);
                    update.ExecuteNonQuery();
                }
            }
            con.Close();
        }
    }
}
