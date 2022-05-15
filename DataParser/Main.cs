using DataParser.DataFormat;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace DataParser
{
    public class Main
    {
        public static Assets ReadDataFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (GZipStream sr = new GZipStream(fs, CompressionMode.Decompress))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Assets));
                var assets = serializer.Deserialize(sr);

                if (assets == null) throw new NullReferenceException();
                return (Assets)assets;
            }
        }

        public static void WriteAssetList(string writePath, IEnumerable<Asset> list)
        {
            XmlSerializer serializer = new XmlSerializer (typeof(Assets));
            Assets assets = new Assets();
            assets.Asset = (List<Asset>?)list;

            using (FileStream fs = new FileStream(writePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (GZipStream ws = new GZipStream(fs, CompressionMode.Compress))
            {
                serializer.Serialize(ws, assets);
            }
        }

#if DEBUG
        public static void ExportAssets(string rdaFile, string outFile)
        {
            using (FileStream ws = new FileStream(outFile, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            using (FileStream fs = new FileStream(rdaFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    if (sr.ReadLine() == "<AssetList>")
                    {
                        ws.Write(Encoding.ASCII.GetBytes("<AssetList>\n"));
                        string line = sr.ReadLine()!;

                        while (!line.StartsWith("</AssetList>"))
                        {
                            ws.Write(Encoding.ASCII.GetBytes(line+"\n"));
                            line = sr.ReadLine()!;
                        }

                        ws.Write(Encoding.ASCII.GetBytes("</AssetList>"));
                    }
                }
            }
        }
    }
#endif
}