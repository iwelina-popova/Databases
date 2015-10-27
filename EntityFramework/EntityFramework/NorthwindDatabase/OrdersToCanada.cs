namespace NorthwindDatabase
{
    using System;

    public class OrdersToCanada
    {
        public string Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Customer: {0}\norder date: {1}\n",
                this.Customer, 
                this.OrderDate);
        }
    }
}
