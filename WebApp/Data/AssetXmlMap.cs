using DataParser.DataFormat;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace WebApp.Data
{
    public static class AssetXmlMap
    {
        public static readonly Dictionary<int, Asset> Assets = new Dictionary<int, Asset>();

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Asset));

        public static string PrettyPrint(int assetId)
        {
            Asset asset = Assets[assetId];
            var serializer = new XmlSerializer(typeof(Asset));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = new UTF8Encoding(false), IndentChars = "\t" };
            using (MemoryStream ms = new MemoryStream())
            {
                var xmlWriter = XmlWriter.Create(ms, settings);
                serializer.Serialize(xmlWriter, asset);
                string text = Encoding.UTF8.GetString(ms.ToArray());
                text = text.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                text = Regex.Replace(text, "\t*<[a-zA-Z]* xsi:nil=\"true\" \\/>\n", "");
                return text;
            }
        }

        public static string PrettyPrint(Asset asset)
        {
            var serializer = new XmlSerializer(typeof(Asset));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = new UTF8Encoding(false), IndentChars = "\t" };
            using (MemoryStream ms = new MemoryStream())
            {
                var xmlWriter = XmlWriter.Create(ms, settings);
                serializer.Serialize(xmlWriter, asset);
                string text = Encoding.UTF8.GetString(ms.ToArray());
                text = text.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                text = Regex.Replace(text, "\t*<[a-zA-Z]* xsi:nil=\"true\" \\/>\n", "");
                return text;
            }
        }

        public static string PrettyPrint(Object o)
        {
            var serializer = new XmlSerializer(typeof(Object));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = new UTF8Encoding(false), IndentChars = "\t" };
            using (MemoryStream ms = new MemoryStream())
            {
                var xmlWriter = XmlWriter.Create(ms, settings);
                serializer.Serialize(xmlWriter, o);
                string text = Encoding.UTF8.GetString(ms.ToArray());
                text = text.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                return text;
            }
        }
    }
}
