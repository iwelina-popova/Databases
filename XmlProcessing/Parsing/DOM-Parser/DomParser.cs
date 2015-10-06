/*
Write program that extracts all different artists which are found in the catalog.xml.
For each author you should print the number of albums in the catalogue.
Use the DOM parser and a hash-table.
*/

namespace DOM_Parser
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DomParser
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalogue.xml");

            XmlElement catalogue = doc.DocumentElement;

            var artistAlbums = CountArtistAlbums(catalogue);
            PrintArtistsAlbums(artistAlbums);
            
        }

        private static void PrintArtistsAlbums(IDictionary<string, int> artistAlbums)
        {
            foreach (var artist in artistAlbums)
            {
                Console.WriteLine("Artist: {0} -> albums: {1}", artist.Key, artist.Value);
            }
        }

        private static IDictionary<string, int> CountArtistAlbums(XmlElement catalogue)
        {
            var albums = catalogue.GetElementsByTagName("album");
            var artistsAlbums = new Dictionary<string, int>();

            foreach (XmlNode album in albums)
            {
                var currentArtist = album["artist"].InnerText;
                if (artistsAlbums.ContainsKey(currentArtist))
                {
                    artistsAlbums[currentArtist]++;
                }
                else
                {
                    artistsAlbums.Add(currentArtist, 1);
                }
            }

            return artistsAlbums;
        }
    }
}
