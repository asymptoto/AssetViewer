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

        [XmlElement(ElementName = "BaseAssetGUID")]
        public int? BaseAssetGUID { get; set; }
    }
}