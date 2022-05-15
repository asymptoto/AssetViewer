using System.Xml;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "Asset")]
    public class Asset
    {
        [XmlElement(ElementName = "Template")]
        public string? Template { get; set; }

        [XmlElement(ElementName = "Values")]
        public Values? Values { get; set; }

        [XmlAnyElement]
        public XmlElement[]? XmlElements { get; set; }
    }
}