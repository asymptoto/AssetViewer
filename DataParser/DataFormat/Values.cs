using System.Xml;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "Values")]
    public class Values
    {
        [XmlElement(ElementName = "Template")]
        public string? Template { get; set; }

        [XmlElement(ElementName = "Standard")]
        public Standard? Standard { get; set; }

        [XmlAnyElement]
        public XmlElement[]? XmlElements { get; set; }
    }
}