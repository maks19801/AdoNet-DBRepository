using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DbExample
{
    
    public class BooksRepository : IRepository<Book>, IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly string _tableName = $"[{nameof(Book)}s]";

        public BooksRepository()
        {
            var connectionString = @"Data Source = USER-PC; Initial Catalog = Library; Integrated Security = True;";
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
        public void Add(Book book)
        {
            var commandText = $"INSERT INTO {_tableName} (Title, Author, PagesCount) VALUES (@title, @author, @pagesCount)";
            var command = GetCommand(commandText);

            command.Parameters.AddWithValue("title", book.Title);
            command.Parameters.AddWithValue("author", book.Author);
            command.Parameters.AddWithValue("pagesCount", book.PagesCount);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var commandText = $"DELETE FROM {_tableName} WHERE Id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public Book Get(int id)
        {
            var commandText = $"SELECT * FROM {_tableName} WHERE Id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", id);
            using var booksReader = command.ExecuteReader();
           
            if (!booksReader.Read()) throw new ArgumentOutOfRangeException();
            {
                return new Book
                {
                    Id = (int)booksReader[0],
                    Title = (string)booksReader[1],
                    Author = (string)booksReader[2],
                    PagesCount = (int)booksReader[3]
                };
            }
        }

        public IEnumerable<Book> Get()
        {
            var commandText = $"SELECT * FROM {_tableName}";
            var command = GetCommand(commandText);
            using var booksReader = command.ExecuteReader();
            if (!booksReader.HasRows) yield break;

            while (booksReader.Read())
            {
                yield return new Book
                {
                    Id = (int)booksReader[0],
                    Title = (string)booksReader[1],
                    Author = (string)booksReader[2],
                    PagesCount = (int)booksReader[3]
                };
            }
        }

        public void Update(Book book)
        {
            var commandText = $"UPDATE {_tableName} " +
                              "SET Title = @title, Author = @author, PagesCount = @pagesCount " +
                              "WHERE Id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", book.Id);
            command.Parameters.AddWithValue("title", book.Title);
            command.Parameters.AddWithValue("author", book.Author);
            command.Parameters.AddWithValue("pagesCount", book.PagesCount);
            command.ExecuteNonQuery();
        }

        private SqlCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

    }
}
