using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CRUDXml
{
    class Program
    {
        static void Main(string[] args)
        {
            XReadXmlElements();

            XUpdateXmlElement();

            XDeleteXmlElement();

        }

        private static void XReadXmlElements()
        {
            //Read the entire Xml file to console
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\Kevin\Documents\Udemy\exercise\CRUDXml\CRUDXml\Vendor\cd_catalog.xml");
            //bring to console
            xmlDoc.Save(Console.Out);
            Console.ReadLine();

            //Read elements of each node
            XmlTextReader xTextReader = new XmlTextReader
                (@"C:\Users\Kevin\Documents\Udemy\exercise\CRUDXml\CRUDXml\Vendor\cd_catalog.xml");
            while (xTextReader.Read())
            {
                if (xTextReader.NodeType == XmlNodeType.Element && xTextReader.Name == "TITLE")
                {
                    string sTitle = xTextReader.ReadElementContentAsString();
                    Console.WriteLine("TITLE :" + sTitle);
                }
            }
            Console.ReadLine();


            //Read elements or each node using LINQ (using System.Xml.Linq;)
            XDocument xDoc = XDocument.Load(@"C:\Users\Kevin\Documents\Udemy\exercise\CRUDXml\CRUDXml\Vendor\cd_catalog.xml");
            xDoc.Descendants("CD").Select(e => new
            {
                title = e.Element("TITLE").Value,
                artist = e.Element("ARTIST").Value,
                country = e.Element("COUNTRY").Value,
                company = e.Element("COMPANY").Value,
                price = e.Element("PRICE").Value,
                year = e.Element("YEAR").Value
                //fyi if there is an attribute in the element tag eg currency in price
                //currency = e.Element("PRICE").Attribute("CURRENCY").Value
            }).ToList().ForEach(e =>
            {
                Console.WriteLine("Title: " + e.title);
                Console.WriteLine("Artist: " + e.artist);
                Console.WriteLine("Country: " + e.country);
                Console.WriteLine("Company: " + e.company);
                Console.WriteLine("Price: " + e.price);
                //cw("Currency: " + e.currency)
                Console.WriteLine("Year: " + e.year);
                Console.WriteLine();
            });
            Console.ReadLine();
        }

        private static void XUpdateXmlElement()
        {
            //Modify a element and save to MyDocument
            var xSaveDoc = XDocument.Load(@"C:\Users\Kevin\Documents\Udemy\exercise\CRUDXml\CRUDXml\Vendor\cd_catalog.xml");
            //search for title with "Empire" to change price to 30.00
            var node = xSaveDoc.Descendants("CD").FirstOrDefault(cd => cd.Element("TITLE").Value.Contains("Empire"));
            //change price element value
            node.SetElementValue("PRICE", 30.00);
            xSaveDoc.Save(@"C:\Users\Kevin\Documents\CD_Catelogx.xml");
        }

        private static void XDeleteXmlElement()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\Kevin\Documents\Udemy\exercise\CRUDXml\CRUDXml\Vendor\cd_catalog.xml");
            //Delete elements with USA in country
            foreach (XmlNode xNode in xmlDoc.SelectNodes("CATALOG/CD"))
            {
                if (xNode.SelectSingleNode("COUNTRY").InnerText == "USA")                    
                {
                    xNode.ParentNode.RemoveChild(xNode);
                    xmlDoc.Save(@"C:\Users\Kevin\Documents\CD_Catelogx.xml");
                }
            }
        }
    }
}
