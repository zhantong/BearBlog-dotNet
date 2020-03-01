using Microsoft.AspNetCore.Mvc.Filters;

namespace BearBlog.Models
{
    public class VisibilityFilterAttribute : ActionFilterAttribute
    {
        public static object HttpContextItemKey { get; } = new object();

        public Visibility Visibility;

        public VisibilityFilterAttribute(Visibility visibility)
        {
            Visibility = visibility;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items[HttpContextItemKey] = this;
        }
    }
}