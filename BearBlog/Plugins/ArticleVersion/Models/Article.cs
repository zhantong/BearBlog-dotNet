namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public int? ArticleVersionId { get; set; }

        public ArticleVersion.Models.ArticleVersion ArticleVersion { get; set; }
    }
}