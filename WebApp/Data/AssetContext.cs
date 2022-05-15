using Microsoft.EntityFrameworkCore;
using DataParser.DataFormat;

namespace WebApp.Data
{
    public class AssetContext : DbContext
    {
        public DbSet<DatabaseAsset>? Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }

        public DbSet<DataParser.DataFormat.Standard>? Standard { get; set; }
    }
}
