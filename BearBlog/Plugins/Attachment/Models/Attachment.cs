using System;
using System.Collections.Generic;

namespace BearBlog.Plugins.Attachment.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string OriginalFilename { get; set; }
        public string Filename { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }
        public string Mime { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Md5 { get; set; }

        public List<ArticleAttachment> ArticleAttachments { get; set; }

        public Attachment()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}