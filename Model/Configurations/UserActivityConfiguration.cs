using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Model.Configurations
{
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BeginOfActivity).IsRequired();
            builder.Property(x => x.EndOfActivity).IsRequired();
            builder.Property(x=> x.UserName).IsRequired();
            builder.HasOne<Activity>(x => x.Activity).WithMany(x => x.UserActivities).HasForeignKey(x=>x.ActivityId);
        }
    }
}
