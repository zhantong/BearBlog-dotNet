using System.Collections.Generic;

namespace BearBlog.Plugins.Comment.Models
{
    public class CommentCollection
    {
        public int Id { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}