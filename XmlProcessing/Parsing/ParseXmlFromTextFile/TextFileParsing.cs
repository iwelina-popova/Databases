namespace ParseXmlFromTextFile
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class TextFileParsing
    {
        public static void Main()
        {
            var person = new Person();

            using(StreamReader reader = new StreamReader("../../person.txt"))
            {
                person.Name = reader.ReadLine();
                person.Address = reader.ReadLine();
                person.Phone = reader.ReadLine();
            }

            var studentElement = new XElement("person",
                new XElement("name", person.Name),
                new XElement("address", person.Address),
                new XElement("phone", person.Phone));

            studentElement.Save("../../person.xml");

            Console.WriteLine("Person was saved to person.xml");
        }
    }
}
