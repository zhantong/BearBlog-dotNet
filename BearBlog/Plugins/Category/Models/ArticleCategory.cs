using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BearBlog.Plugins.Category.Models
{
    public class ArticleCategory
    {
        public int ArticleId { get; set; }
        public Article.Models.Article Article { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(ac => new {ac.ArticleId, ac.CategoryId});
            builder
                .HasOne(ac => ac.Article)
                .WithMany(a => a.ArticleCategories)
                .HasForeignKey(ac => ac.ArticleId);

            builder
                .HasOne(ac => ac.Category)
                .WithMany(c => c.ArticleCategories)
                .HasForeignKey(ac => ac.CategoryId);
        }
    }
}