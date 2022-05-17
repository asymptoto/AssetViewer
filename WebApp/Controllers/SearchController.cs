using Microsoft.AspNetCore.Mvc;
using WebApp.Data;

namespace WebApp.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        private AssetContext _context;

        public SearchController(AssetContext context)
        {
            _context = context;
        }

        public IActionResult Index(string query, int? page, int? perPage)
        {
            if (page == null) page = 0;
            if (perPage == null) perPage = 5;

            query = query.ToLower();
            var searchResult = from a in _context.Assets
                               where (a.Name ?? "").ToLower().Contains(query)
                               || (a.Id ?? "").ToLower().Contains(query)
                               || (a.IconFilename ?? "").ToLower().Contains(query)
                               || a.GUID.ToString().Contains(query)
                               || (a.Template ?? "").ToLower().Contains(query)
                               || (a.Text ?? "").ToLower().Contains(query)
                               select a;
            return View(searchResult.OrderBy(asset => asset.GUID));
        }
    }
}
