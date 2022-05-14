using DataParser.DataFormat;
using System.Text;
using System.Xml.Serialization;

namespace WebApp.Data
{
    public static class AssetXmlMap
    {
        public static readonly Dictionary<int, Asset> Assets = new Dictionary<int, Asset>();

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Asset));

        public static string SerializedAsset(int guid)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, Assets[guid]);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
