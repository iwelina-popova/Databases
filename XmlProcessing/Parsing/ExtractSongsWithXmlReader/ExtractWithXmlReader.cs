/*
Write a program, which using XmlReader extracts all song titles from catalog.xml.
*/

namespace ExtractSongsWithXmlReader
{
    using System;
    using System.Xml;

    public class ExtractWithXmlReader
    {
        public static void Main()
        {
            Console.WriteLine("Song titles in te catalog:");
            using(XmlReader reader = XmlReader.Create("../../../catalogue.xml"))
            {
                while (reader.Read())
                {
                    if((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "title"))
                    {
                        Console.WriteLine(reader.ReadElementString());
                    }
                }
            }
        }
    }
}
