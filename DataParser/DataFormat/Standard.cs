using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataParser.DataFormat
{
    [XmlRoot(ElementName = "Standard")]
    public class Standard
    {
        [Key]
        [XmlElement(ElementName = "GUID")]
        public int GUID { get; set; }

        [XmlElement(ElementName = "Name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "ID")]
        public string? Id { get; set; }

        [XmlElement(ElementName = "IconFilename")]
        public string? IconFilename { get; set; }
    }
}
