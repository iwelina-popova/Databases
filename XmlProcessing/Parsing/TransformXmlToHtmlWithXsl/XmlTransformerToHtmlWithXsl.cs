/*
Create an XSL stylesheet, which transforms the file catalog.xml into HTML document,
formatted for viewing in a standard Web-browser.
*/

namespace TransformXmlToHtmlWithXsl
{
    using System.Xml.Xsl;

    public class XmlTransformerToHtmlWithXsl
    {
        public static void Main()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("../../catalogue.xslt");
            xslt.Transform("../../../catalogue.xml", "../../catalogue.html");
        }
    }
}
