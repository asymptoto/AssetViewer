using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class AssetContext : DbContext
    {
        public DbSet<DatabaseAsset>? Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }
    }
}
