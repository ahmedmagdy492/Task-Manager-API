using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Models;

namespace Task_Manager.TypeConfiguration
{
    public class GroupTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasIndex(g => g.Id);

            builder.HasMany(g => g.WorkTasks)
                .WithOne(w => w.Group);
            builder.HasMany(g => g.GroupUsers)
                .WithOne(u => u.Group);
            builder.HasMany(g => g.GroupMessages)
                .WithOne(m => m.Group);
        }
    }
}
