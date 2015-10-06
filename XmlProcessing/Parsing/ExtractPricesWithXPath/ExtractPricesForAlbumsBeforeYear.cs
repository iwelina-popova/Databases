/*
Write a program, which extract from the file catalog.xml the prices for all albums,
published 5 years ago or earlier.
Use XPath query.
*/

namespace ExtractPricesWithXPath
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class ExtractPricesForAlbumsBeforeYear
    {
        public static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../../catalogue.xml");

            string xPathQuery = "/catalogue/album";
            XmlNodeList albumsList = xmlDoc.SelectNodes(xPathQuery);

            int beforeFiveYears = DateTime.Now.Year - 5;
            Dictionary<string, string> extractedPrices = ExtractAlbumsYearsBefore(albumsList, beforeFiveYears);

            PrintYears(extractedPrices);
        }

        private static void PrintYears(Dictionary<string, string> albumsPrices)
        {
            foreach (var album in albumsPrices)
            {
                Console.WriteLine("Album: {0} -> price: {1}",  album.Key, album.Value);
            }
        }

        private static Dictionary<string, string> ExtractAlbumsYearsBefore(XmlNodeList albumsList, int year)
        {
            Dictionary<string, string> albumsPrices = new Dictionary<string, string>();

            foreach (XmlNode album in albumsList)
            {
                string albumDateAsString = album.SelectSingleNode("year").InnerText;
                int albumDate = int.Parse(albumDateAsString);

                if(albumDate <= year)
                {
                    var name = album.SelectSingleNode("name").InnerText;
                    var price = album.SelectSingleNode("price").InnerText;
                    albumsPrices.Add(name, price);
                }
            }

            return albumsPrices;
        }
    }
}
