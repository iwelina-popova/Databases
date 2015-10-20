namespace MySql
{
    using System;
    using System.Configuration;

    using MySql.Data.MySqlClient;

    public class MySqlStartup
    {
        public static void Main()
        {
            // To test this please go to App.config file and set yours Uid=YOURS;Pwd=YOURS

            string connectionString = ConfigurationManager.ConnectionStrings["Books"].ConnectionString;

            // Task 9
            Console.WriteLine("Task 9:");
            Console.WriteLine("List all books");
            ListAllBooks(connectionString);

            Console.WriteLine("----------------------");
            Console.WriteLine("Book with given title:");
            FindBookByName(connectionString, "Harry Potter and the Philosopher's Stone");

            Console.WriteLine("----------------------");
            Console.WriteLine("Books by given pattern");
            FindBooksByTitlePattern(connectionString, "ring");

            InsertBook(
                connectionString,
                "The Fellowship of the Ring",
                "J. R. R. Tolkien",
                "1954-07-29",
                "0-345-24032-4");

            InsertBook(
                connectionString,
                "The Notebook",
                "Nicholas Sparks",
                "1996-10-1",
                "0-446-52080-2");
        }

        private static void InsertBook(string connectionString, string title, string author, string publishDate, string isbn)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new MySqlCommand(
                    "INSERT INTO Books (Title, Author, PublishDate, ISBN) VALUES (@title, @author, @publishDate, @isbn)",
                    databaseConnection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@publishDate", publishDate);
                command.Parameters.AddWithValue("@isbn", isbn);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Data was inserted successfully.");
                }
                catch (MySqlException exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private static void FindBooksByTitlePattern(string connectionString, string pattern)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new MySqlCommand(
                    string.Format("SELECT Title, Author, PublishDate, ISBN FROM Books WHERE Title LIKE '%{0}%'", pattern),
                    databaseConnection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(
                        "Title: {0}, \nAuthor: {1}, \nPublishDate: {2}, \nISBN: {3}",
                        reader["Title"], 
                        reader["Author"], 
                        (DateTime)reader["PublishDate"], 
                        reader["ISBN"]);
                    Console.WriteLine();
                }
            }
        }

        private static void FindBookByName(string connectionString, string title)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new MySqlCommand(
                    "SELECT Title, Author, PublishDate, ISBN FROM Books WHERE Title = @title",
                    databaseConnection);
                command.Parameters.AddWithValue("@title", title);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(
                        "Title: {0}, \nAuthor: {1}, \nPublishDate: {2}, \nISBN: {3}",
                        reader["Title"], 
                        reader["Author"], 
                        (DateTime)reader["PublishDate"], 
                        reader["ISBN"]);
                    Console.WriteLine();
                }
            }
        }

        private static void ListAllBooks(string connectionString)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new MySqlCommand("SELECT Title, Author, PublishDate, ISBN FROM Books", databaseConnection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(
                        "Title: {0}, \nAuthor: {1}, \nPublishDate: {2}, \nISBN: {3}",
                        reader["Title"], 
                        reader["Author"], 
                        (DateTime)reader["PublishDate"],
                        reader["ISBN"]);
                    Console.WriteLine();
                }
            }
        }
    }
}
