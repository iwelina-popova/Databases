namespace ExtractPricesForAlbumsWithLinq
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class ExtractPricesForAlbumsBeforeYearWithLinq
    {
        public static void Main()
        {
            XDocument xmlDoc = XDocument.Load("../../../catalogue.xml");

            var yearBefore = DateTime.Now.Year - 5;

            var albumsPrices =
                from album in xmlDoc.Descendants("album")
                where int.Parse(album.Element("year").Value) <= yearBefore
                select new
                {
                    Name = album.Element("name").Value,
                    Year = album.Element("year").Value,
                    Price = album.Element("price").Value
                };

            foreach (var album in albumsPrices)
            {
                Console.WriteLine("Album: {0} - {1} -> price: {2}", 
                    album.Name, album.Year, album.Price);
            }
        }
    }
}
