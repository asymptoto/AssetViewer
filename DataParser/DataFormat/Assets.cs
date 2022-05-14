using System.Xml.Serialization;

namespace DataParser.DataFormat
{
	[XmlRoot(ElementName = "Assets")]
	public class Assets
	{
		[XmlElement(ElementName = "Asset")]
		public List<Asset>? Asset { get; set; }
	}
}
