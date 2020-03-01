using BearBlog.Plugins.Article.Models;
using BearBlog.Plugins.ArticleVersion.Models;
using Microsoft.EntityFrameworkCore;

namespace BearBlog.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleVersion> ArticleVersions { get; set; }

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