/*List<int> files = new()
{
    0,
    10,
    11,
    12,
    13,
    14,
    15,
    16,
    18,
    19,
    21,
    22
};

foreach (int file in files) DataParser.Main.ExportAssets($"maindata\\data{file}.rda", $"assetdata\\data{file}.xml");

Console.WriteLine("Press any button to exit...");
Console.Read();*/

using System.IO.Compression;
using System.Xml.Serialization;
using DataParser.DataFormat;
using System.Text;

XmlSerializer serializer = new XmlSerializer(typeof(Assets));
Assets assets;

/*using (FileStream fs = new FileStream($"C:\\Users\\torbe\\Desktop\\Viewer\\assets.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
using (StreamReader sr = new StreamReader(fs))
{
    using (FileStream ws = new FileStream($"C:\\Users\\torbe\\Desktop\\Viewer\\assets.dat", FileMode.CreateNew, FileAccess.Write, FileShare.Read))
    using (GZipStream gz = new GZipStream(ws, CompressionMode.Compress))
    {
        fs.CopyTo(gz);
    }
}*/


using (FileStream fs = new FileStream($"C:\\Users\\torbe\\Desktop\\Viewer\\assets.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
using (GZipStream sr = new GZipStream(fs, CompressionMode.Decompress))
{
    assets = (Assets)serializer.Deserialize(sr)!;

    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Asset));

    Dictionary<int, Asset> ids = new();

    foreach (var asset in assets.Asset!)
    {
        if (asset.Values?.Standard?.GUID != null)
            ids.Add((int)asset.Values.Standard.GUID, asset);
    }

    using (MemoryStream stream = new MemoryStream())
    {
        xmlSerializer.Serialize(stream, ids[132404]);
        
        Console.WriteLine(Encoding.UTF8.GetString(stream.ToArray()));
    }
    Console.WriteLine(assets.Asset!.Count);
}