using System.Collections.Generic;
using BearBlog.Models;

namespace BearBlog.Plugins.Attachment.Models
{
    public class Events
    {
        public static void Register()
        {
            BearBlog.Models.Events.PatchArticle += OnPatchArticle;
        }

        private static void OnPatchArticle(object? sender, PatchArticleEventArgs e)
        {
            using var db = new BloggingContext();
            if (e.Patch.ArticleAttachments != null)
            {
                var articleAttachments = new List<ArticleAttachment>();
                foreach (var articleAttachment in e.Patch.ArticleAttachments)
                {
                    articleAttachments.Add(new ArticleAttachment
                        {ArticleId = e.TargetArticle.Id, AttachmentId = articleAttachment.AttachmentId});
                }

                e.TargetArticle.ArticleAttachments = articleAttachments;
            }
        }
    }
}