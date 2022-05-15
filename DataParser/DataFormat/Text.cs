using System.Xml;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "Text")]
    public class Text
    {
        [XmlElement(ElementName = "LocaText")]
        public LocaText? LocaText { get; set; }
    }
}