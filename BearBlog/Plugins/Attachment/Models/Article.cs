using System.Collections.Generic;
using BearBlog.Plugins.Attachment.Models;

namespace BearBlog.Plugins.Article.Models
{
    public partial class Article
    {
        public List<ArticleAttachment> ArticleAttachments { get; set; }
    }
}