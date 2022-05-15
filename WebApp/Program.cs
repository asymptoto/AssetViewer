using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using DataParser.DataFormat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AssetContext>(options =>
{
    options.UseInMemoryDatabase("Assets");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Populate database
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AssetContext>();
var assets = DataParser.Main.ReadDataFile(Path.Combine("Data", "assets.dat"))!;

using (FileStream fs = new FileStream("C:\\Users\\Torben\\Desktop\\rejected.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
using (StreamWriter ws = new StreamWriter(fs))
    foreach (Asset asset in assets.Asset!)
    {
        try
        {
            context.Assets!.Add((DatabaseAsset)asset);
            AssetXmlMap.Assets[asset.Values!.Standard!.GUID] = asset;
        } catch (InvalidOperationException) {
            ws.Write(AssetXmlMap.PrettyPrint(asset)+"\n");
        }
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
