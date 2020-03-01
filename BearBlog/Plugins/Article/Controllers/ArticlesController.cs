using System.Linq;
using BearBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BearBlog.Plugins.Article.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly BloggingContext _db;

        public ArticlesController(BloggingContext context)
        {
            _db = context;
        }

        // GET: api/Articles
        [HttpGet]
        [VisibilityFilter(Visibility.Brief)]
        public PagedResult<Models.Article> Get(int page = 0)
        {
            var result = _db.Articles
                .Where(a => a.Status == "published")
                .Include(a => a.Author)
                .OrderByDescending(a => a.Timestamp)
                .AsQueryable()
                .GetPaged(page, 4);
            return result;
        }
    }
}