namespace MsSql
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class MsSqlStartup
    {
        private const string ConnectionString = "Server=.; Database=Northwind; Integrated Security=true";

        public static void Main()
        {
            // Task 1
            Console.WriteLine("Task 1:");
            CountRowsInCategoriesTable();

            // Task 2
            Console.WriteLine("\nTask 2:");
            GetNameAndDescription();

            // Task 3
            Console.WriteLine("\nTask 3:");
            GetCategoriesAndProductsInEachCategory();

            // Task 4
            Console.WriteLine("\nTask 4:");
            var productId = AddProduct("Something", 5, 2, "10 kg", 20.6m, 6, 4, 2, true);
            Console.WriteLine("\nThe product with id: {0} was added to Northwind database!", productId);

            // Task 5
            Console.WriteLine("\nTask 5:");
            GetImagesFromAllCategoriesAndSaveThem();

            // Task 8
            Console.WriteLine("\nTask 8:");
            FindProductsContainsGivenInput("ku");
        }

        private static void FindProductsContainsGivenInput(string input)
        {
            var inputAfterEscaping = EscapeInputString(input);

            var databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            using (databaseConnection)
            {
                var command = new SqlCommand(
                    @"SELECT ProductName FROM Products
                      WHERE ProductName LIKE '%'+@input+'%'",
                      databaseConnection);
                command.Parameters.AddWithValue("@input", inputAfterEscaping);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["ProductName"]);
                }
            }
        }

        private static string EscapeInputString(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\'')
                {
                    input = input.Substring(0, i) + "'" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '_')
                {
                    input = input.Substring(0, i) + "/" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '%')
                {
                    input = input.Substring(0, i) + "\\" + input.Substring(i, input.Length - i);
                    i++;
                }

                if (input[i] == '&')
                {
                    input = input.Substring(0, i) + "\\" + input.Substring(i, input.Length - i);
                    i++;
                }
            }

            return input;
        }

        private static void GetImagesFromAllCategoriesAndSaveThem()
        {
            //// Write a program that retrieves the images for all categories in the Northwind database 
            //// and stores them as JPG files in the file system.

            SqlConnection databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            SqlCommand cmdImages = new SqlCommand(
                "SELECT CategoryName, Picture FROM Categories", databaseConnection);
            SqlDataReader reader = cmdImages.ExecuteReader();
            using (reader)
            {
                string filePath = "../../images";
                string fileExtention = ".jpg";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    categoryName = categoryName.Replace('/', '_');
                    byte[] fileContent = (byte[])reader["Picture"];
                    string fileName = string.Format(filePath + categoryName + fileExtention);
                    WriteBinaryFile(fileName, fileContent);
                }

                Console.WriteLine("The images was saved!");
            }
        }

        private static void WriteBinaryFile(string fileName, byte[] fileContents)
        {
            FileStream stream = File.OpenWrite(fileName);
            using (stream)
            {
                stream.Write(fileContents, 78, fileContents.Length - 78);
            }
        }

        private static void CountRowsInCategoriesTable()
        {
            //// Write a program that retrieves from the Northwind sample database in 
            //// MS SQL Server the number of rows in the Categories table.

            SqlConnection databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            SqlCommand cmdCount = new SqlCommand(
                "SELECT COUNT(*) FROM Categories", databaseConnection);
            int categoriesCount = (int)cmdCount.ExecuteScalar();
            Console.WriteLine("Categories count: {0} ", categoriesCount);
            Console.WriteLine("----------------------");
        }

        private static void GetNameAndDescription()
        {
            //// Write a program that retrieves the name and description of all categories in the Northwind DB.

            SqlConnection databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            SqlCommand cmdNameAndDescription = new SqlCommand(
                "SELECT CategoryName, Description FROM Categories", databaseConnection);
            SqlDataReader reader = cmdNameAndDescription.ExecuteReader();
            using (reader)
            {
                Console.WriteLine("\nCategory name - Description");
                Console.WriteLine("---------------------------");
                while (reader.Read())
                {
                    string name = (string)reader["CategoryName"];
                    string description = (string)reader["Description"];
                    Console.WriteLine("{0} - {1}", name, description);
                }

                Console.WriteLine("-------------------");
            }
        }

        private static void GetCategoriesAndProductsInEachCategory()
        {
            //// Write a program that retrieves from the Northwind database all product categories 
            //// and the names of the products in each category.
            //// Can you do this with a single SQL query (with table join)?

            SqlConnection databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            SqlCommand cmdCategoriesAndProducts = new SqlCommand(
                "SELECT c.CategoryName, p.ProductName " +
                "FROM Products p " +
                "JOIN Categories c " +
                "ON p.CategoryID = c.CategoryID " +
                "GROUP BY c.CategoryName, p.ProductName",
                databaseConnection);

            SqlDataReader reader = cmdCategoriesAndProducts.ExecuteReader();
            using (reader)
            {
                Console.WriteLine("\nCategory name - Product name");
                Console.WriteLine("------------------------------");
                while (reader.Read())
                {
                    string category = (string)reader["CategoryName"];
                    string product = (string)reader["ProductName"];
                    Console.WriteLine("{0} - {1}", category, product);
                }
            }
        }

        private static int AddProduct(
            string name,
            int supplier,
            int category,
            string quantity,
            decimal unitPrice,
            int unitInStock,
            int unitOnOrder,
            int reorderLevel,
            bool discontinued)
        {
            //// Write a method that adds a new product in the products table in the Northwind database.
            //// Use a parameterized SQL command.

            SqlConnection databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            SqlCommand cmdInsertProduct = new SqlCommand(
            "INSERT INTO Products(ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) " +
            "VALUES (@name, @supplierId, @categoryId, @quantity, @unitPrice, @unitInStock, @unitsOnOrder, @reorderLevel, @discontinued)",
            databaseConnection);

            cmdInsertProduct.Parameters.AddWithValue("@name", name);
            cmdInsertProduct.Parameters.AddWithValue("@supplierId", supplier);
            cmdInsertProduct.Parameters.AddWithValue("@categoryId", category);
            cmdInsertProduct.Parameters.AddWithValue("@quantity", quantity);
            cmdInsertProduct.Parameters.AddWithValue("@unitPrice", unitPrice);
            cmdInsertProduct.Parameters.AddWithValue("@unitInStock", unitInStock);
            cmdInsertProduct.Parameters.AddWithValue("@unitsOnOrder", unitOnOrder);
            cmdInsertProduct.Parameters.AddWithValue("@reorderLevel", reorderLevel);
            cmdInsertProduct.Parameters.AddWithValue("@discontinued", discontinued);

            cmdInsertProduct.ExecuteNonQuery();

            SqlCommand cmdSelectIdentity = new SqlCommand("SELECT @@Identity", databaseConnection);
            int insertedRecordId = (int)(decimal)cmdSelectIdentity.ExecuteScalar();
            return insertedRecordId;
        }
    }
}
