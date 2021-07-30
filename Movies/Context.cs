using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder entityBuilder)
        {
         
            entityBuilder.Entity<Category>(p =>
            {
                p.HasKey(q => q.Id);
                p.HasMany(q => q.Movies).WithOne(q => q.Category).HasForeignKey(q => q.CategoryId).OnDelete(DeleteBehavior.NoAction);
            });

            entityBuilder.Entity<Movie>(p =>
            {
                p.HasKey(q => q.Id);
                p.Property(q => q.CategoryId).IsRequired();
                p.HasOne(q=> q.Category).WithMany(q => q.Movies).HasForeignKey(q => q.CategoryId).OnDelete(DeleteBehavior.NoAction);
            });

        }
       

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
