namespace ParseToJson
{
    using System;
    using Newtonsoft.Json;
    using System.Text;
    using System.Net;
    using System.Xml;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    public class JsonParser
    {
        public static void Main()
        {
            const string rssLink = "https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            const string xmlPath = "videos.xml";
            const string htmlName = "index.html";

            Console.OutputEncoding = Encoding.UTF8;

            DownloadRss(rssLink, xmlPath);
            var xmlDoc = GetXml(xmlPath);
            var jsonObj = GetJsonObject(xmlDoc);
            var titles = GetVideosTitles(jsonObj);
            PrintTitles(titles);

            var videos = GetVideos(jsonObj);
            var html = GetHtmlString(videos);
            SaveHtml(html, htmlName);
        }

        private static XmlDocument GetXml(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            return xmlDoc;
        }

        private static void DownloadRss(string url, string fileName)
        {
            WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };
            webClient.DownloadFile(url, fileName);
        }

        private static IEnumerable<Video> GetVideos(JObject json)
        {
            var videos = json["feed"]["entry"]
                .Select(entry => JsonConvert.DeserializeObject<Video>(entry.ToString()));

            return videos;
        }

        private static string GetHtmlString(IEnumerable<Video> videos)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<!DOCTYPE html><html><body>");
            foreach (var video in videos)
            {
                html.AppendFormat("<div style=\"float:left; width: 420px; height: 450px; padding:10px; " +
                                  "margin:5px; background-color:grey; border-radius:10px\">" +
                                  "<iframe width=\"420\" height=\"345\" " +
                                  "src=\"http://www.youtube.com/embed/{1}?autoplay=0\" " +
                                  "frameborder=\"0\" allowfullscreen></iframe>" +
                                  "<h3>{2}</h3><a href=\"{0}\">Go to YouTube</a></div>",
                                  video.Link.Href, video.Id, video.Title);
            }
            html.Append("</body></html>");

            return html.ToString();
        }

        private static JObject GetJsonObject(XmlDocument xmlDoc)
        {
            string json = JsonConvert.SerializeXmlNode(xmlDoc);
            var jsonObj = JObject.Parse(json);

            return jsonObj;
        }

        private static IEnumerable<JToken> GetVideosTitles(JObject jsonObj)
        {
            return jsonObj["feed"]["entry"]
                .Select(entry => entry["title"]);
        }

        private static void PrintTitles(IEnumerable<JToken> titles)
        {
            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void SaveHtml(string html, string htmlName)
        {
            using (StreamWriter writer = new StreamWriter("../../" + htmlName, false, Encoding.UTF8))
            {
                writer.Write(html);
            }

            var currentDir = Directory.GetCurrentDirectory();
            var htmlDir = currentDir.Substring(0, currentDir.IndexOf("bin\\Debug")) + htmlName;

            Console.WriteLine("Html dir: {0}", htmlDir);
        }
    }
}
