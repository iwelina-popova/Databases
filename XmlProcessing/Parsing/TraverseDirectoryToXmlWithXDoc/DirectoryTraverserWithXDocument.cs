namespace TraverseDirectoryToXmlWithXDoc
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class DirectoryTraverserWithXDocument
    {
        public static void Main()
        {
            var path = "../../../TraverseDirectoryToXml";
            var traversedDirectory = TraverseDirectory(path);
            traversedDirectory.Save("../../directory.xml");

            Console.WriteLine("Traversed was successfully saved in directory.xml!");
        }

        private static XElement TraverseDirectory(string path)
        {
            var element = new XElement("dir", new XAttribute("path", path));

            foreach (var dir in Directory.GetDirectories(path))
            {
                element.Add(TraverseDirectory(dir));
            }

            foreach (var file in Directory.GetFiles(path))
            {
                element.Add(new XElement("file",
                    new XAttribute("name", Path.GetFileNameWithoutExtension(file))));
            }

            return element;
        }
    }
}
