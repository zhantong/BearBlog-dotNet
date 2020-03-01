using System;

namespace BearBlog.Models
{
    public enum Visibility : int
    {
        Brief,
        Basic,
        Full,
    }

    public class VisibilityAttribute : Attribute
    {
        public Visibility Visibility;

        public VisibilityAttribute(Visibility visibility)
        {
            Visibility = visibility;
        }
    }
}