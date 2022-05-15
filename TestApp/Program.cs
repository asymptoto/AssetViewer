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

using (FileStream rfs = new FileStream($"C:\\Users\\torbe\\Desktop\\Viewer\\assets.minimal.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
using (FileStream wfs = new FileStream($"C:\\Users\\torbe\\Desktop\\Viewer\\assets1.dat", FileMode.Create, FileAccess.Write, FileShare.Read))
using (GZipStream ws = new(wfs, CompressionMode.Compress))
rfs.CopyTo(ws);
