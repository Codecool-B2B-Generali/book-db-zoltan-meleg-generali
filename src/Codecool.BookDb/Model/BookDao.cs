using Codecool.BookDb.Manager;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.BookDb.Model
{
    class BookDao : IBookDao
    {
        private string connectionString;

        public BookDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO book (author_id, title) VALUES(@AuthorID, @Title)", con))
                {
                    command.Parameters.AddWithValue("@AuthorID", book.Author.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Book Get(int id)
        {
            var book = new Book();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM book where id={id}", con))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var authorID = (int)reader["author_id"];
                            book = new Book
                            {
                                Id = (int)reader["id"],
                                Author = AuthorHelper.GetAuthor(authorID),
                                Title = (string)reader["title"]
                            };
                            break;
                        }
                    }
                }
            }
            return book;
        }

        public List<Book> GetAll()
        {
            var books = new List<Book>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM book", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var authorID = (int)reader["author_id"];
                            books.Add(new Book
                            {
                                Id = (int)reader["id"],
                                Author = AuthorHelper.GetAuthor(authorID),
                                Title = (string)reader["title"]
                            });
                        }
                    }
                }
            }
            return books;
        }

        public void Update(Book book)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("UPDATE book SET author_id=@AuthorID, title=@Title where id=@ID", con))
                {
                    command.Parameters.AddWithValue("@AuthorID", book.Author.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@ID", book.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
