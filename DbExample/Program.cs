using DbExample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbExample
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var booksRepository = new BooksRepository();
             GetAllBooks(booksRepository);
             Book book = GetBook(booksRepository);
             AddBook(booksRepository);
             DeleteBook(booksRepository,10);
             UpdateBook(booksRepository, 2);*/
            var repository = new Repository<Visitor>();
            var visitor = new Visitor()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Rating = 10.35M,
                BirthDate = new DateTime(2020, 11, 20)
            };
            repository.Add(visitor);
            var results = repository.Get();
            /*foreach(var result in results)
            {
                Console.WriteLine($"{result.Author}, {result.PagesCount}");
            }*/
            var visitorToUpdate = repository.Get(1);


            visitorToUpdate.FirstName = "Petro";
            visitorToUpdate.LastName = "Petrov";
            visitorToUpdate.Rating = 5.35M;
            visitorToUpdate.BirthDate = new DateTime(2001, 10, 25);

            

            repository.Update(visitorToUpdate);
            /* Console.WriteLine($"{result.Author}, {result.PagesCount}");

             var book = new Book
             {
                 Title = "Паполо или как я путешевствовал",
                 Author = "Дмитрий Быков",
                 PagesCount = 200
             };*/
            /*result.Title = "Книга2";
            result.Author = "Пароходов";
            booksRepository.Update(result);*/


        }

        private static void DeleteBook(BooksRepository booksRepository, int id)
        {
            booksRepository.Delete(id);
        }

        private static void UpdateBook(BooksRepository booksRepository, int bookId)
        {
            var bookToUpdate = new Book
            {
                Id = bookId,
                Title = "Дурная кровь",
                Author = "Robert Gilbert",
                PagesCount = 900
            };

            booksRepository.Update(bookToUpdate);
        }

        private static void AddBook(BooksRepository booksRepository)
        {
            var bookToAdd = new Book { Author = "Достоевский", Title = "Преступление и наказание", PagesCount = 400 };
            booksRepository.Add(bookToAdd);
        }

        private static Book GetBook(BooksRepository booksRepository)
        {
            var book = booksRepository.Get(2);
            Console.WriteLine($"{book.Title}: {book.PagesCount}");
            return book;
        }

        private static void GetAllBooks(BooksRepository booksRepository)
        {
            var books = booksRepository.Get();
            foreach (var bookItem in books)
            {
                Console.WriteLine($"{bookItem.Title}: {bookItem.PagesCount}");
            }
        }
        /* static void Main(string[] args)
{
    var connection = GetConnection();
    connection.Open();


    var insertCommand = InsertBookCommand(connection, "Смерть в облаках", "Агата Кристи", 1000);
    var executionResult = insertCommand.ExecuteNonQuery();

    Console.WriteLine($"insert affected {executionResult} rows");

    GetAllBooks(connection);
}

private static void GetAllBooks(SqlConnection connection)
{
    var booksReader = GetAllBooksCommand(connection).ExecuteReader();
    if (!booksReader.HasRows) return;
    var books = ReadBookRows(booksReader);
    foreach (var book in books)
    {
        Console.WriteLine($"{book.Title}, {book.Author}, {book.Id}, {book.PagesCount}");
    }
}


private static SqlCommand GetAllBooksCommand(SqlConnection connection)
{
    var commandText = "SELECT * FROM [Library].[dbo].[Books]";
    var getAllBooks = new SqlCommand(commandText, connection);
    return getAllBooks;
}

private static SqlCommand InsertBookCommand(SqlConnection connection, string title, string author, int pagesCount)
{
    var commandText = "INSERT INTO [Books] (Title, Author, PagesCount) VALUES (@title, @author, @pagesCount)";
    var command = new SqlCommand(commandText, connection);

    command.Parameters.AddWithValue("title", title);
    command.Parameters.AddWithValue("author", author);
    command.Parameters.AddWithValue("pagesCount", pagesCount);
    return command;
}
private static SqlConnection GetConnection()
{
    var connectionString = @"Data Source = USER-PC; Initial Catalog = Library; Integrated Security = True;";
    var connection = new SqlConnection(connectionString);
    return connection;
}
private static IEnumerable<Book> ReadBookRows(SqlDataReader booksReader)
{
    while (booksReader.Read())
    {
        yield return ReadBook(booksReader);
    }
}
private static Book ReadBook(SqlDataReader booksReader)
{
    return new Book
    {
        Id = (int)booksReader[0],
        Title = (string)booksReader[1],
        Author = (string)booksReader[2],
        PagesCount = (int)booksReader[3]
    };
}*/
    }
}

