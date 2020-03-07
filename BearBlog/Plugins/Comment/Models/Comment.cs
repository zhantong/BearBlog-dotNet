using System;
using BearBlog.Models;

namespace BearBlog.Plugins.Comment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public DateTime Timestamp { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Ip { get; set; }
        public string Agent { get; set; }
        public int? ParentId { get; set; }
        public Comment ParentComment { get; set; }

        public int CommentCollectionId { get; set; }
        public CommentCollection CommentCollection { get; set; }
    }
}