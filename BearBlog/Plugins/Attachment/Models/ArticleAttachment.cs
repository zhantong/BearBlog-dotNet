using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BearBlog.Plugins.Attachment.Models
{
    public class ArticleAttachment
    {
        public int ArticleId { get; set; }
        public Article.Models.Article Article { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }

    public class ArticleAttachmentConfiguration : IEntityTypeConfiguration<ArticleAttachment>
    {
        public void Configure(EntityTypeBuilder<ArticleAttachment> builder)
        {
            builder.HasKey(ac => new {ac.ArticleId, ac.AttachmentId});
            builder
                .HasOne(aa => aa.Article)
                .WithMany(a => a.ArticleAttachments)
                .HasForeignKey(aa => aa.ArticleId);

            builder
                .HasOne(aa => aa.Attachment)
                .WithMany(a => a.ArticleAttachments)
                .HasForeignKey(aa => aa.AttachmentId);
        }
    }
}