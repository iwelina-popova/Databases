namespace CountArtistAlbumsWithXPath
{
    using System;
    using System.Xml;
    using System.Collections.Generic;

    public class XPathParser
    {
        public static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalogue.xml");
            string xPathQuery = "/catalogue/album";
            
            XmlNodeList albumsList = doc.SelectNodes(xPathQuery);
            var artistsAlbums = CountArtistAlbums(albumsList);

            PrintArtistsAlbums(artistsAlbums);
        }

        private static void PrintArtistsAlbums(IDictionary<string, int> artistAlbums)
        {
            foreach (var artist in artistAlbums)
            {
                Console.WriteLine("Artist: {0} -> albums: {1}", artist.Key, artist.Value);
            }
        }

        private static IDictionary<string, int> CountArtistAlbums(XmlNodeList albumsList)
        {
            var artistsDictionary = new Dictionary<string, int>();

            foreach (XmlNode album in albumsList)
            {
                var currentArtist = album.SelectSingleNode("artist").InnerText;

                if(artistsDictionary.ContainsKey(currentArtist))
                {
                    artistsDictionary[currentArtist]++;
                }
                else
                {
                    artistsDictionary.Add(currentArtist, 1);
                }
            }

            return artistsDictionary;
        }
    }
}
