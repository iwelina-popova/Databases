namespace TraverseDirectoryToXml
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class DirectioryTraverserToXml
    {
        public static void Main()
        {
            using(XmlTextWriter writer = new XmlTextWriter("../../directory.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;

                string path = "../../../TraverseDirectoryToXml";

                writer.WriteStartDocument();
                writer.WriteStartElement("directories");
                TraverseDirectory(path, writer);
                writer.WriteEndDocument();

                Console.WriteLine("Traversed was successfully saved in directory.xml!");
            }
        }

        private static void TraverseDirectory(string path, XmlTextWriter writer)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                writer.WriteStartElement("dir");
                writer.WriteAttributeString("paht", dir);
                TraverseDirectory(dir, writer);
                writer.WriteEndElement();
            }

            foreach (var file in Directory.GetFiles(path))
            {
                writer.WriteStartElement("file");
                writer.WriteAttributeString("name", Path.GetFileNameWithoutExtension(file));
                writer.WriteEndElement();
            }
        }
    }
}
