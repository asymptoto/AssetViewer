using System.Text;
using System.Xml.Linq;

namespace WebApp.Misc
{
    public class XmlToHtmlSerializationSettings
    {
        // Attributes to set on any generated <detail> elements. Default ""
        public string DetailAttributes { get; set; } = "";
        // Attributes to set on any generated <summary> elements. Default ""
        public string SummaryAttributes { get; set; } = "";
        // Attributes to set on any generated <ul> elements. Default ""
        public string UlAttributes { get; set; } = "";
        // Attributes to set on any generated <li> elements. Default ""
        public string LiAttributes { get; set; } = "";
        // Whether to line-break after an element. Default false
        public bool LineBreak { get; set; } = false;
        // Whether to indent the html code. Default false
        public bool Indent { get; set; } = false;
        // The character used to indent. Default '\t'
        public char IndentCharacter { get; set; } = '\t';
        // Encoding to encode the html string with. Default UTF-8
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }

    public static class HtmlTreeConverter
    {
        private static void _ConvertToHtmlDetails(XElement element, Stream writer, XmlToHtmlSerializationSettings settings, int depth=2)
        {
            if (element.Descendants().Count() == 0)
            {
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 1) : "") + element.Value + (settings.LineBreak ? "\n" : "")));
                return;
            }
            foreach (XElement item in element.Elements())
            {
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth) : "") + "<li" + (settings.LiAttributes == "" ? "" : " ") + settings.LiAttributes + ">" + (settings.LineBreak ? "\n" : "")));
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 1) : "") + "<details" + (settings.DetailAttributes == "" ? "" : " ") + settings.DetailAttributes + ">" + (settings.LineBreak ? "\n" : "")));
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 2) : "") + "<summary" + (settings.SummaryAttributes == "" ? "" : " ") + settings.SummaryAttributes + $">{item.Name}</summary>" + (settings.LineBreak ? "\n" : "")));
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 2) : "") + "<ul" + (settings.UlAttributes == "" ? "" : " ") + settings.UlAttributes + ">" + (settings.LineBreak ? "\n" : "")));
                _ConvertToHtmlDetails(item, writer, settings, depth + 2);
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 2) : "") + "</ul>" + (settings.LineBreak ? "\n" : "")));
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth + 1) : "") + "</details>" + (settings.LineBreak ? "\n" : "")));
                writer.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, depth) : "") + "</li>" + (settings.LineBreak ? "\n" : "")));
            }
        }

        /// <summary>
        /// Returns an HTML5 tree representation of an XML document
        /// </summary>
        /// <param name="document">The XDocument to be converted</param>
        /// <param name="settings">An instance of HtmlSerializerSettings with formatting options.
        /// If none is given, the default settings will be used</param>
        /// <returns>The generated HTML code</returns>
        /// <exception cref="FormatException"></exception>
        public static string XmlToHtmlConvert(XDocument document, XmlToHtmlSerializationSettings? settings = null)
        {
            if (settings == null) settings = new XmlToHtmlSerializationSettings();

            using (MemoryStream ms = new MemoryStream())
            {
                XmlToHtmlConvert(document, ms, settings);
                return settings.Encoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Returns an HTML5 tree representation of an XML string
        /// </summary>
        /// <param name="xmlString">A valid XML formatted string to be converted</param>
        /// <param name="settings">An instance of HtmlSerializerSettings with formatting options.
        /// If none is given, the default settings will be used</param>
        /// <returns>The generated HTML code</returns>
        /// <exception cref="FormatException"></exception>
        public static string XmlToHtmlConvert(string xmlString, XmlToHtmlSerializationSettings? settings = null)
        {
            return XmlToHtmlConvert(XDocument.Parse(xmlString), settings);
        }

        /// <summary>
        /// Writes an HTML5 tree representation of an XML string to the given Stream
        /// </summary>
        /// <param name="xmlString">A valid XML formatted string to be converted</param>
        /// <param name="writeStream">A writable Stream object to write the data to</param>
        /// <param name="settings">An instance of HtmlSerializerSettings with formatting options.
        /// If none is given, the default settings will be used</param>
        public static void XmlToHtmlConvert(string xmlString, Stream writeStream, XmlToHtmlSerializationSettings? settings = null)
        {
            XmlToHtmlConvert(XDocument.Parse(xmlString), writeStream, settings);
        }

        /// <summary>
        /// Writes an HTML5 tree representation of an XML document to the given Stream
        /// </summary>
        /// <param name="document">The XDocument to be converted</param>
        /// <param name="writeStream">A writable Stream object to write the data to</param>
        /// <param name="settings">An instance of HtmlSerializerSettings with formatting options.
        /// If none is given, the default settings will be used</param>
        public static void XmlToHtmlConvert(XDocument document, Stream writeStream, XmlToHtmlSerializationSettings? settings = null)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (document.Root == null) throw new FormatException(nameof(settings));
            if (writeStream == null) throw new ArgumentNullException(nameof(writeStream));
            if (!writeStream.CanWrite) throw new NotSupportedException(nameof(writeStream));

            if (settings == null) settings = new XmlToHtmlSerializationSettings();

            writeStream.Write(settings.Encoding.GetBytes("<details" + (settings.DetailAttributes == "" ? "" : " ") + settings.DetailAttributes + ">" + (settings.LineBreak ? "\n" : "")));
            writeStream.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, 1) : "") + "<summary" + (settings.SummaryAttributes == "" ? "" : " ") + settings.SummaryAttributes + $">{document.Root.Name}</summary>" + (settings.LineBreak ? "\n" : "")));
            writeStream.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, 1) : "") + "<ul>" + (settings.LineBreak ? "\n" : "")));
            _ConvertToHtmlDetails(document.Root, writeStream, settings);
            writeStream.Write(settings.Encoding.GetBytes((settings.Indent ? new string(settings.IndentCharacter, 1) : "") + "<ul" + (settings.UlAttributes == "" ? "" : " ") + settings.UlAttributes + ">" + (settings.LineBreak ? "\n" : "")));
            writeStream.Write(settings.Encoding.GetBytes("</details>"));
        }
    }
}
