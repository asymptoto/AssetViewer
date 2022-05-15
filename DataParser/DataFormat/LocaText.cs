using System.Xml;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "LocaText")]
    public class LocaText
    {
        [XmlElement(ElementName = "English")]
        public English? English { get; set; }
    }
}