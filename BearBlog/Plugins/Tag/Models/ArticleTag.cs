using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BearBlog.Plugins.Tag.Models
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }
        public Article.Models.Article Article { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

    public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(ac => new {ac.ArticleId, ac.TagId});
            builder
                .HasOne(ac => ac.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(ac => ac.ArticleId);

            builder
                .HasOne(ac => ac.Tag)
                .WithMany(c => c.ArticleTags)
                .HasForeignKey(ac => ac.TagId);
        }
    }
}