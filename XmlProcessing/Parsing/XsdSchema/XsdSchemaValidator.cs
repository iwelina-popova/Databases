namespace XsdSchema
{
    using System;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class XsdSchemaValidator
    {
        public static void Main()
        {
            var schema = new XmlSchemaSet();
            schema.Add(string.Empty, "../../../catalogue.xsd");

            XDocument doc = XDocument.Load("../../../catalogue.xml");
            XDocument invalidDoc = XDocument.Load("../../invalid.xml");

            Console.WriteLine("Result from valid xml:");
            PrintValidationResult(doc, schema, "catalogue.xml");

            Console.WriteLine("\nResult from invalid xml");
            PrintValidationResult(invalidDoc, schema, "invalid.xml");
        }

        private static void PrintValidationResult(XDocument doc, XmlSchemaSet schema, string file)
        {
            doc.Validate(schema, (sender, args) => Console.WriteLine(args.Message));
        }
    }
}
