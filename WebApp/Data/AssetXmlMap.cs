using DataParser.DataFormat;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace WebApp.Data
{
    public static class AssetXmlMap
    {
        public static readonly Dictionary<int, Asset> Assets = new Dictionary<int, Asset>();

        private static readonly XmlWriterSettings Settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = new UTF8Encoding(false), IndentChars = "\t" };

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Asset));

        public static string PrettyPrint(Asset asset)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var xmlWriter = XmlWriter.Create(ms, Settings);
                Serializer.Serialize(xmlWriter, asset);
                string text = Encoding.UTF8.GetString(ms.ToArray());
                text = text.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                text = Regex.Replace(text, "\t*<[a-zA-Z]* xsi:nil=\"true\" \\/>\n", "");
                text = text.Replace(" xsi:nil=\"true\" ", "");
                return text;
            }
        }

        public static string PrettyPrint(int assetId)
        {
            Asset asset = Assets[assetId];
            return PrettyPrint(asset);
        }

        public static string PrettyPrint(DatabaseAsset asset)
        {
            return PrettyPrint(asset.GUID);
        }

        private static string Trim(string text, int maxChars = 100)
        {
            if (text.Length > maxChars)
            {
                return text.Substring(0, maxChars-3) + "...";
            }
            return text;
        }

        public static string Summary(DatabaseAsset asset)
        {
            return " GUID: " + asset.GUID + 
                 ", Template: " + (asset.Template != null ? asset.Template : "None") +
                 ", ID: " + (asset.Id != null ? asset.Id : "None") +
                 ", Name: " + (asset.Name != null ? asset.Name : "None") +
                 ", Text: " + (asset.Text != null ? Trim(asset.Text) : "None");
        }
    }
}
