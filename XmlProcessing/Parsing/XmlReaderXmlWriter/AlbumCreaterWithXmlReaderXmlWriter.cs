/*
Write a program, which (using XmlReader and XmlWriter) reads the file catalog.xml
and creates the file album.xml, in which stores in appropriate way the names of
all albums and their authors.
*/

namespace XmlReaderXmlWriter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    public class AlbumCreaterWithXmlReaderXmlWriter
    {
        public static void Main()
        {
            using (XmlTextWriter writer = new XmlTextWriter("../../albums.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("albums");

                using (XmlReader reader = XmlReader.Create("../../../catalogue.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "name")
                            {
                                var name = reader.ReadElementContentAsString();

                                writer.WriteStartElement("album");
                                writer.WriteElementString("name", name);
                            }
                            else if (reader.Name == "artist")
                            {
                                var author = reader.ReadElementContentAsString();

                                writer.WriteElementString("author", author);
                                writer.WriteEndElement();
                            }
                        }
                    }
                }

                writer.WriteEndDocument();
            }

            Console.WriteLine("Albums was saved in albums.xml");
        }
    }
}
