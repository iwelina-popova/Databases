namespace NorthwindDatabase.Clone
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            //// Create a database called NorthwindTwin with the same structure as Northwind using the features from DbContext.
            //// Find for the API for schema generation in MSDN or in Google.
            
            using (var northwindEntities = new NorthwindEntities())
            {
                //// To solve this task you need to change in the app.config file the connection string to:
                //// initial catalog=NorthwindTwin
                var result = northwindEntities.Database.CreateIfNotExists();

                Console.WriteLine("Database NorthWindTwin was created: {0}", result ? "YES!" : "NO!");
            }
        }
    }
}
