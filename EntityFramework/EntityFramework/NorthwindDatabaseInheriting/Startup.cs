namespace NorthwindDatabaseInheriting
{
    using System;
    using NorthwindDatabase;

    public class Startup
    {
        public static void Main()
        {
            //// By inheriting the Employee entity class create a class which allows employees 
            //// to access their corresponding territories as property of type EntitySet<T>

            using (var northwind = new NorthwindEntities())
            {
                var employee = northwind.Employees.Find(2);

                foreach (var item in employee.TerritoryProperty)
                {
                    Console.WriteLine("Employee:{0} Teritory desctiption: {1}", employee.FirstName, item.TerritoryDescription);
                }
            }
        }
    }
}
