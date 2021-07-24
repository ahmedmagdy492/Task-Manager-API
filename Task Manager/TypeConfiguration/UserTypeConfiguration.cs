using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Models;

namespace Task_Manager.TypeConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasIndex(u => u.Email)
                .IsUnique(true);

            builder.HasMany(u => u.MyWorkTasks)
                .WithOne(w => w.Creator);

            builder.HasMany(u => u.AssignedToWorkTasks)
                .WithOne(w => w.AssignedTo);

            builder.HasMany(u => u.GroupUsers)
                .WithOne(g => g.User);
            builder.HasMany(u => u.Messages);
        }
    }
}
