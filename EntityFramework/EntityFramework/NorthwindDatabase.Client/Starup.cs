namespace NorthwindDatabase.Client
{
    using System;
    using System.Linq;

    public class Starup
    {
        public static void Main()
        {
            //// Create new customer
            // string customerId = CreateNewCustomer("some", "Neastle");
            // Console.WriteLine("Created customer id is: {0}", customerId);

            //// Modify customer with ID = OTTIK
            // NorthwindEntities northwindEntities = new NorthwindEntities();
            // var id = "OTTIK";
            // var customer = GetCustomerById(northwindEntities, id);
            // Console.WriteLine("\nOld company name: {0}", customer.CompanyName);
            // customer = ModifyCustomerName(id, "Some name");
            // Console.WriteLine("New company name: {0}", customer.CompanyName);
            // northwindEntities.Dispose();

            //// Delete customer with ID = some
            // DeleteCustomer("some");
            // Console.WriteLine("Customer with id 'some' was deleted!");

            //// Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
            // FindCustomersWithOrder(1997, "Canada");

            ////Implement previous by using native SQL query and executing it through the DbContext.
            // FindCustomerWithOrderWithNativeSql(1997, "Canada");

            //// Write a method that finds all the sales by specified region and period(start / end dates).
            // FindSalesByRegionAndPeriod("RJ", new DateTime(1997, 1, 1), new DateTime(2005, 5, 5));
        }

        private static void FindSalesByRegionAndPeriod(string region, DateTime startDate, DateTime endDate)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                var sales = northwindEntities
                    .Orders
                    .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                    .Where(o => o.ShipRegion == region)
                    .ToList();

                foreach (var sale in sales)
                {
                    var customer = sale.Customer.CompanyName;
                    var date = sale.OrderDate;
                    var currentRegion = sale.ShipRegion;

                    Console.WriteLine("Customer: {0}\nOrder date: {1}\nShip region: {2}\n", customer, date, currentRegion);
                }
            }
        }

        private static void FindCustomerWithOrderWithNativeSql(int year, string country)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                string minDate = year + "-01-01";
                string maxDate = year + "-12-31";

                string query = string.Format(
                    @"SELECT c.CompanyName AS [Customer], o.OrderDate FROM Orders o
                    JOIN Customers c ON o.CustomerID = c.CustomerID
                    WHERE YEAR(o.OrderDate) = {0} AND o.ShipCountry = '{1}'",
                    year,
                    country);

                var orders = northwindEntities.Database.SqlQuery<OrdersToCanada>(query);

                foreach (var order in orders)
                {
                    Console.WriteLine(order.ToString());
                }
            }
        }

        private static void FindCustomersWithOrder(int year, string country)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                DateTime minDate = new DateTime(year, 01, 01);
                DateTime maxDate = new DateTime(year, 12, 31);
                var orders = northwindEntities
                    .Orders
                    .Where(o => o.OrderDate >= minDate && o.OrderDate <= maxDate)
                    .Where(o => o.ShipCountry == country)
                    .ToList();

                foreach (var order in orders)
                {
                    var customer = order.Customer.CompanyName;
                    var date = order.OrderDate;

                    Console.WriteLine("Customer: {0}, date: {1}", customer, date);
                }
            }
        }

        private static string CreateNewCustomer(string id, string companyName)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                Customer newCustomer = new Customer
                {
                    CustomerID = id,
                    CompanyName = companyName
                };

                northwindEntities.Customers.Add(newCustomer);
                northwindEntities.SaveChanges();
                return newCustomer.CustomerID;
            }
        }

        private static Customer ModifyCustomerName(string customerId, string newName)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                Customer customer = GetCustomerById(northwindEntities, customerId);
                customer.CompanyName = newName;
                northwindEntities.SaveChanges();
                return customer;
            }
        }

        private static Customer GetCustomerById(NorthwindEntities northwindEntities, string customerId)
        {
            Customer foundedCustomer = northwindEntities.Customers.FirstOrDefault(
                c => c.CustomerID == customerId);
            return foundedCustomer;
        }

        private static void DeleteCustomer(string customerId)
        {
            using (NorthwindEntities northwindEntities = new NorthwindEntities())
            {
                Customer customer = GetCustomerById(northwindEntities, customerId);
                northwindEntities.Customers.Remove(customer);
                northwindEntities.SaveChanges();
            }
        }
    }
}
