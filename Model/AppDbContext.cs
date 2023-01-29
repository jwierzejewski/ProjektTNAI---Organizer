using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Model
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        
        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sport",
                },
                new Category
                {
                    Id = 2,
                    Name = "Nauka",
                },
                new Category
                {
                    Id = 3,
                    Name = "Jedzenie",
                }
            );
            modelBuilder.Entity<Activity>().HasData(
                new Activity
                {
                    Id = 1,
                    Name = "Piłka Nożna",
                    CategoryId= 1,
                },
                new Activity
                {
                    Id = 2,
                    Name = "Siatkówka",
                    CategoryId = 1,
                },
                new Activity
                {
                    Id = 3,
                    Name = "Siłownia",
                    CategoryId = 1,
                },
                new Activity
                {
                    Id = 4,
                    Name = "Wykład",
                    CategoryId = 2,
                },
                new Activity
                {
                    Id = 5,
                    Name = "Ćwiczenia",
                    CategoryId = 2,
                }
            );
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });

        }

        public static AppDbContext Create()
        {
            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().Options);
        }

    }
}
