namespace BearBlog.Plugins.Tag.DataModels
{
    public class TagDataModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public TagDataModel(Models.Tag tag)
        {
            Name = tag.Name;
            Slug = tag.Slug;
            Description = tag.Description;
        }
    }
}