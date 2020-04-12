using BearBlog.Plugins.Article.Models;
using BearBlog.Plugins.ArticleVersion.Models;
using BearBlog.Plugins.Attachment.Models;
using Microsoft.EntityFrameworkCore;
using BearBlog.Plugins.Category.Models;
using BearBlog.Plugins.Comment.Models;
using BearBlog.Plugins.Tag.Models;

namespace BearBlog.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleVersion> ArticleVersions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentCollection> CommentCollections { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ArticleAttachment> ArticleAttachments { get; set; }

        public BloggingContext()
        {
            ChangeTracker.Tracked += Article.OnEntityTracked;
            ChangeTracker.StateChanged += Article.OnEntityStateChanged;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\zhant\source\repos\BearBlog\BearBlog\blogging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}