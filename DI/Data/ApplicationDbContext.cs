using DI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasKey(c => new { c.ID });

            builder.Entity<Location>()
                .HasKey(l => new { l.ID });

            builder.Entity<Status>()
                .HasKey(s => new { s.ID });

            builder.Entity<Task>()
                .HasKey(t => new { t.ID });

            builder.Entity<Category>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Category);

            builder.Entity<Location>()
                .HasMany(l => l.Tasks)
                .WithOne(t => t.Location);

            builder.Entity<Task>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tasks);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DI;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
