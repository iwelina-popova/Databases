/*
Using the DOM parser write a program to delete from catalog.xml all albums having price > 20.
*/

namespace DOM_Parser
{
    using System;
    using System.Xml;

    public class DeleteWithDom
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalogue.xml");

            XmlElement catalogue = doc.DocumentElement;
            decimal price = 20;

            DeleteAlbumsWithBiggerPrice(catalogue, price);
            doc.Save("../../new-catalogue.xml");

            Console.WriteLine("Modified is ready! You can find it in 'new-catalogue.xml'");
        }

        private static void DeleteAlbumsWithBiggerPrice(XmlElement catalogue, decimal price)
        {
            var albums = catalogue.SelectNodes("album");
            
            foreach (XmlElement album in albums)
            {
                var currentPriceAsString = album["price"].InnerText;
                var currentPriceAsDecimal = decimal.Parse(currentPriceAsString);

                if(currentPriceAsDecimal > price)
                {
                    catalogue.RemoveChild(album);
                }
            }

        }
    }
}
