using System.Xml;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "English")]
    public class English
    {
        [XmlElement(ElementName = "Text")]
        public string? Text { get; set; }

        [XmlElement(ElementName = "Status")]
        public string? Exported { get; set; }

        [XmlElement(ElementName = "ExportCount")]
        public int? ExportCount { get; set; }
    }
}