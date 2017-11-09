using System;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock.Models;

namespace EmbeddedStock.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryComponentType> CategoryComponentType { get; set; }
        public DbSet<ESImage> ESImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryComponentType>()
                        .HasKey(c => new { c.CategoryId, c.ComponentTypeId });
        }
    }
}