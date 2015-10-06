namespace ExtractSongsWithXDocumentAndLinq
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class ExtractSongsWithXDocAndLinq
    {
        public static void Main()
        {
            XDocument xmlDoc = XDocument.Load("../../../catalogue.xml");
            var songs =
                from song in xmlDoc.Descendants("song")
                select new
                {
                    Title = song.Element("title").Value
                };

            foreach (var song in songs)
            {
                Console.WriteLine("----- {0} -----", song.Title);
            }
        }
    }
}
