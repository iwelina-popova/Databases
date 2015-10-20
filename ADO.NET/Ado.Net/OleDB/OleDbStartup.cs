namespace OleDB
{
    using System;
    using System.Data.OleDb;

    public class OleDbStartup
    {
        private const string ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source = ..\\..\\Excel\\sample.xlsx;" +
                                                "Extended Properties = \"Excel 12.0;HDR=Yes;\";";

        public static void Main()
        {
            // Task 6
            Console.WriteLine("\nTask 6:");
            ReadExcelFile();

            // Task 7
            Console.WriteLine("\nTask 7:");
            AppendNewRows("Pena Peneva", 50.5);
        }

        private static void ReadExcelFile()
        {
            //// Write a program that reads your MS Excel file through the OLE DB data provider 
            //// and displays the name and score row by row.

            OleDbConnection oleDbCon = new OleDbConnection(ConnectionString);
            oleDbCon.Open();
            using (oleDbCon)
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Score$]", oleDbCon);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["Name"];
                    var score = reader["Score"];

                    Console.WriteLine("Name: {0} \nScore: {1}\n", name, score);
                }
            }
        }

        private static void AppendNewRows(string name, double score)
        {
            //// Implement appending new rows to the Excel file.

            OleDbConnection oleDbCon = new OleDbConnection(ConnectionString);
            oleDbCon.Open();

            using (oleDbCon)
            {
                OleDbCommand cmd = new OleDbCommand(
                "INSERT INTO [Score$] (Name, Score) VALUES (@name, @score)",
                oleDbCon);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@score", score);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Row was inserted successfully.");
                }
                catch (OleDbException exception)
                {
                    Console.WriteLine("Excel Error occured: " + exception);
                }
            }
        }
    }
}
