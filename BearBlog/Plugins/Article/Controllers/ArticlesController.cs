using System.Collections.Generic;
using System.Linq;
using BearBlog.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

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
        [EnableQuery(PageSize = 5, EnsureStableOrdering = false)]
        public IEnumerable<Models.Article> Get()
        {
            return _db.Articles
                .Where(a => a.Status == "published")
                .OrderByDescending(a => a.Timestamp)
                .AsQueryable();
        }
    }
}