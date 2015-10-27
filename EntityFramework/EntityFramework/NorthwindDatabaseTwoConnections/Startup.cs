namespace NorthwindDatabaseTwoConnections
{
    using System;
    using System.Linq;
    using NorthwindDatabase;

    public class Startup
    {
        public static void Main()
        {
            //// Try to open two different data contexts and perform concurrent changes on the same records.
            //// What will happen at SaveChanges() ?
            //// How to deal with it ?

            using (NorthwindEntities firstNorthwindEntities = new NorthwindEntities())
            {
                var customer = firstNorthwindEntities
                    .Customers
                    .FirstOrDefault();
                Console.WriteLine("Old customer country: {0}", customer.Country);
                customer.Country = "USA";
                Console.WriteLine("New customer country: {0}", customer.Country);

                using (NorthwindEntities secondNorthwindEntities = new NorthwindEntities())
                {
                    var sameCustomer = secondNorthwindEntities
                        .Customers
                        .FirstOrDefault();
                    Console.WriteLine("Old customer country: {0}", sameCustomer.Country);
                    sameCustomer.Country = "Korea";
                    Console.WriteLine("New customer country: {0}", sameCustomer.Country);

                    firstNorthwindEntities.SaveChanges();
                    secondNorthwindEntities.SaveChanges();
                }
            }

            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                var customer = northwindEntities
                    .Customers
                    .FirstOrDefault();
                Console.WriteLine("Result from both SaveChanges() -> country: {0}", customer.Country);
            }
        }
    }
}
