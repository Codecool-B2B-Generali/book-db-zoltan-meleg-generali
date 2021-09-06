using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.BookDb.Model
{
    class AuthorDao : IAuthorDao
    {
        private string connectionString;

        public AuthorDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Author author)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO author (first_name, last_name, birth_date) VALUES(@FirstName, @LastName, @BirthDate)", con))
                {
                    command.Parameters.AddWithValue("@FirstName", author.FirstName);
                    command.Parameters.AddWithValue("@LastName", author.LastName);
                    command.Parameters.AddWithValue("@BirthDate", author.BirthDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Author Get(int id)
        {
            var author = new Author();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM author where id=@ID", con))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            author = new Author((string)reader["first_name"], (string)reader["last_name"], (DateTime)reader["birth_date"]);
                            author.Id = (int)reader["id"];
                            break;
                        }
                    }
                }
            }
            return author;
        }

        public List<Author> GetAll()
        {
            var authors = new List<Author>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM author", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            authors.Add(new Author
                            {
                                Id = (int)reader["id"],
                                FirstName = (string)reader["first_name"],
                                LastName = (string)reader["last_name"],
                                BirthDate = (DateTime)reader["birth_date"]
                            });
                        }
                    }
                }
            }
            return authors;
        }

        public void Update(Author author)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("UPDATE author SET first_name=@FirstName, last_name=@LastName, birth_date=@BirthDate where id=@ID", con))
                {
                    command.Parameters.AddWithValue("@FirstName", author.FirstName);
                    command.Parameters.AddWithValue("@LastName", author.LastName);
                    command.Parameters.AddWithValue("@BirthDate", author.BirthDate);
                    command.Parameters.AddWithValue("@ID", author.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
