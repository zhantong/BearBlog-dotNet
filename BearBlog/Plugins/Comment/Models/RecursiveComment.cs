﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace BearBlog.Plugins.Comment.Models
{
    public class RecursiveComment : Comment
    {
        public ICollection<RecursiveComment> Children { get; set; }

        public static ICollection<RecursiveComment> ParseComments(ICollection<Comment> comments)
        {
            static ICollection<RecursiveComment> ProcessChildren(Dictionary<int, ICollection<Comment>> parentDictionary, int parentId)
            {
                var result = new List<RecursiveComment>();
                if (parentDictionary.ContainsKey(parentId))
                {
                    foreach (var comment in parentDictionary[parentId])
                    {
                        var recursiveComment = JsonConvert.DeserializeObject<RecursiveComment>(JsonConvert.SerializeObject(comment));
                        recursiveComment.Children = ProcessChildren(parentDictionary, comment.Id);
                        result.Add(recursiveComment);
                    }
                }

                return result;
            }

            var parentDictionary = new Dictionary<int, ICollection<Comment>>();
            foreach (var comment in comments)
            {
                if (comment.ParentCommentId == null)
                {
                    comment.ParentCommentId = 0;
                }

                if (!parentDictionary.ContainsKey(comment.ParentCommentId.Value))
                {
                    parentDictionary[comment.ParentCommentId.Value] = new List<Comment>();
                }

                parentDictionary[comment.ParentCommentId.Value].Add(comment);
            }

            return ProcessChildren(parentDictionary, 0);
        }
    }
}