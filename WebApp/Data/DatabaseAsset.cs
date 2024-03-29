﻿using DataParser.DataFormat;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class DatabaseAsset
    {
        [Key]
        public int GUID { get; set; }
        public string? Name { get; set; }
        public string? Id { get; set; }
        public string? IconFilename { get; set; }
        public string? Text { get; set; }
        public string? Template { get; set; }

        public static explicit operator DatabaseAsset(Asset asset)
        {
            DatabaseAsset dbAsset = new();
            dbAsset.GUID = (int)asset.Values?.Standard?.GUID;
            dbAsset.Name = asset.Values?.Standard?.Name;
            dbAsset.Id = asset.Values?.Standard?.Id;
            dbAsset.IconFilename = asset.Values?.Standard?.IconFilename;
            dbAsset.Text = asset.Values?.Text?.LocaText?.English?.Text;
            dbAsset.Template = asset.Template;
            if (asset.Template == null)
            {
                var query = from a in asset.Values.XmlElements
                            where a.Name == "Template"
                            select a;
                if (query.Any()) dbAsset.Template = query.First().InnerText;
            }
            return dbAsset;
        }
    }
}
