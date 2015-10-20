namespace SQLite
{
    using System;
    using System.Data.SQLite;

    public class SQLiteStarup
    {
        public static void Main()
        {
            var connectionString = "Data Source=../../dataBase/library.db;Version=3;";

            // Task 10
            Console.WriteLine("Task 10:");
            Console.WriteLine("List all books:");
            ListAllBooks(connectionString);

            Console.WriteLine("----------------------");
            Console.WriteLine("Books by given pattern");
            FindBooksByTitlePattern(connectionString, "intro");

            InsertBook(
                connectionString,
                "The Fellowship of the Ring",
                "J. R. R. Tolkien",
                "1954-07-29",
                "0-345-24032-4");
        }

        private static void FindBooksByTitlePattern(string connectionString, string pattern)
        {
            SQLiteConnection databaseConnection = new SQLiteConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new SQLiteCommand(
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

        private static void ListAllBooks(string connectionString)
        {
            SQLiteConnection databaseConnection = new SQLiteConnection(connectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                string sqlStringCommand = "SELECT * FROM Books";

                SQLiteCommand allBooks = new SQLiteCommand(sqlStringCommand, databaseConnection);
                SQLiteDataReader reader = allBooks.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            "Title: {0}, \nAuthor: {1}, \nPublishDate: {2}, \nISBN: {3}",
                            reader["Title"],
                            reader["Author"],
                            (DateTime) reader["PublishDate"],
                            reader["ISBN"]);
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void InsertBook(string connectionString, string title, string author, string publishDate, string isbn)
        {
            SQLiteConnection dbCon = new SQLiteConnection(connectionString);
            dbCon.Open();

            var command = new SQLiteCommand("INSERT INTO books(Title,Author, PublishDate, ISBN) VALUES (@title,@author,@publishDate,@isbn)", dbCon);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@publishDate", publishDate);
            command.Parameters.AddWithValue("@isbn", isbn);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Data was inserted successfully.");
            }
            catch (SQLiteException exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
