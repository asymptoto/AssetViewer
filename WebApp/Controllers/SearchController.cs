using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
            if (perPage > 50) return BadRequest();
            if (page == null) page = 1;
            if (perPage == null || perPage == 0) perPage = 10;
            ViewData["SearchString"] = query;
            ViewData["PerPage"] = perPage;
            ViewData["Page"] = page;
            query = query.ToLower();

            string? template = null;
            string? name = null;
            string? guid = null;
            string? text = null;

            Match match = Regex.Match(query, "(?<full>(#template:\"(?<string>[a-zA-Z0-9]*)\" ?))");
            if (match.Success)
            {
                template = match.Groups["string"].Value;
                query = query.Replace(match.Groups["full"].Value, "");
            }
            match = Regex.Match(query, "(?<full>(#name:\"(?<string>[a-zA-Z0-9]*)\" ?))");
            if (match.Success)
            {
                name = match.Groups["string"].Value;
                query = query.Replace(match.Groups["full"].Value, "");
            }
            match = Regex.Match(query, "(?<full>(#guid:\"(?<string>[0-9]*)\" ?))");
            if (match.Success)
            {
                guid = match.Groups["string"].Value;
                query = query.Replace(match.Groups["full"].Value, "");
            }
            match = Regex.Match(query, "(?<full>(#text:\"(?<string>[a-zA-Z0-9]*)\" ?))");
            if (match.Success)
            {
                Console.WriteLine(match.Groups["full"]);
                text = match.Groups["string"].Value;
                query = query.Replace(match.Groups["full"].Value, "");
            }

            var candidates = from a in _context.Assets
                             select a;

            if (template != null)
                candidates = from a in candidates
                             where a.Template != null && a.Template.ToLower().Contains(template)
                             select a;
            if (name != null)
                candidates = from a in candidates
                             where a.Name != null && a.Name.ToLower().Contains(name)
                             select a;
            if (guid != null)
                candidates = from a in candidates
                             where a.GUID.ToString().Contains(guid)
                             select a;
            if (text != null)
                candidates = from a in candidates
                             where a.Text != null && a.Text.ToLower().Contains(text)
                             select a;

            var searchResult = from a in candidates
                               where (a.Name ?? "").ToLower().Contains(query)
                               || (a.Id ?? "").ToLower().Contains(query)
                               || (a.IconFilename ?? "").ToLower().Contains(query)
                               || a.GUID.ToString().Contains(query)
                               || (a.Template ?? "").ToLower().Contains(query)
                               || (a.Text ?? "").ToLower().Contains(query)
                               orderby a.GUID ascending
                               select a;

            
            
            int skip = (int)((page! - 1) * perPage!);
            ViewData["TotalPages"] = (int)Math.Ceiling((double)(searchResult.Count() / (int)perPage)) + 1;
            GC.Collect();
            return View(searchResult.Skip(skip).Take((int)perPage));
        }
    }
}
