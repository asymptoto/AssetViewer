using Microsoft.AspNetCore.Mvc;
using DataParser.DataFormat;
using WebApp.Data;
using System.Text;

namespace WebApp.Controllers
{
    [Route("asset")]
    public class AssetController : Controller
    {
        public IActionResult Index(int guid)
        {
            if (AssetXmlMap.Assets.ContainsKey(guid))
                return File(Encoding.UTF8.GetBytes(AssetXmlMap.PrettyPrint(AssetXmlMap.Assets[guid]).ToArray()), "text/plain");
            else
                return NotFound();
        }
    }
}
