using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BearBlog.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "管理员"
                },
                new Role
                {
                    Id = 2,
                    Name = "访客"
                }
            });
        }
    }
}