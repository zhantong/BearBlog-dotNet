using System.Linq;
using BearBlog.Models;
using BearBlog.Plugins.Article.DataModels;
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
        public PagedResult<ArticleDataModel> Get(int page = 1)
        {
            var query = _db.Articles
                .Include(a => a.Author)
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Include(a => a.CommentCollection)
                .ThenInclude(cc => cc.Comments)
                .OrderByDescending(a => a.Timestamp)
                .AsQueryable();
            var listArticlesEventArgs = new ListArticlesEventArgs {HttpRequest = HttpContext.Request, Query = query};
            Events.OnListArticles(listArticlesEventArgs);
            return listArticlesEventArgs.Query.GetPaged(page, 4, article => new ArticleDataModel(article));
        }

        [HttpGet("slug/{slug}")]
        [VisibilityFilter(Visibility.Full)]
        public ActionResult<ArticleDataModel> GetArticleBySlug(string slug)
        {
            var article = _db.Articles
                .Include(a => a.Author)
                .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
                .Include(a => a.ArticleTags)
                .ThenInclude(at => at.Tag)
                .Include(a => a.CommentCollection)
                .ThenInclude(cc => cc.Comments)
                .Single(a => a.Slug == slug);
            return new ArticleDataModel(article);
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
            Events.OnCreateArticle(new CreateArticleEventArgs {Article = article});
            _db.SaveChanges();
            return CreatedAtAction("GetArticleBySlug", new {slug = article.Slug}, article);
        }
    }
}