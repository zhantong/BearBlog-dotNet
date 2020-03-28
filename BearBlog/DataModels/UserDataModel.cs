using BearBlog.Models;

namespace BearBlog.DataModels
{
    public class UserDataModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        public UserDataModel(User user)
        {
            Username = user.Username;
            Name = user.Name;
            Role = user.Role;
        }
    }
}