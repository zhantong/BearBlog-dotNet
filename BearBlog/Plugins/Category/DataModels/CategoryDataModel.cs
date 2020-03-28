namespace BearBlog.Plugins.Category.DataModels
{
    public class CategoryDataModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public CategoryDataModel(Models.Category category)
        {
            Name = category.Name;
            Slug = category.Slug;
            Description = category.Description;
        }
    }
}