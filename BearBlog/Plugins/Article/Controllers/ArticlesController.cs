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

        [HttpGet("{id}")]
        public ActionResult<Models.Article> GetArticle(int id)
        {
            var article = _db.Articles
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Include(a => a.ArticleTags)
                .ThenInclude(at => at.Tag)
                .Single(a => a.Id == id);
            return article;
        }

        [HttpPatch("{id}")]
        public IActionResult PatchArticle(int id, Models.Article article)
        {
            Events.OnPatchArticle(new PatchArticleEventArgs() {TargetArticle = _db.Articles.Find(id), Patch = article});
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Models.Article> PostArticle(Models.Article article)
        {
            _db.Articles.Add(article);
            _db.SaveChanges();
            return CreatedAtAction("GetArticle", new {id = article.Id}, article);
        }
    }
}