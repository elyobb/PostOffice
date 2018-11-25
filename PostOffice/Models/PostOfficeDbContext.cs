using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.Shared.Models
{
    public class PostOfficeDbContext : DbContext
    {
        public PostOfficeDbContext(DbContextOptions<PostOfficeDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Copy> Copy { get; set; }
        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostItem>()
                 .HasIndex(p => p.Url)
                 .IsUnique();
            builder.Entity<PostItem>()
                 .HasMany(p => p.Copy)
                 .WithOne(c => c.PostItem);
            builder.Entity<Copy>()
                .HasOne(c => c.PostItem)
                .WithMany(p => p.Copy);
            builder.Entity<Copy>()
                .HasMany(c => c.Tags)
                .WithOne(t => t.Copy);
            builder.Entity<Tag>()
                 .HasOne(t => t.Copy)
                 .WithMany(c => c.Tags);
            builder.Entity<Account>()
                 .HasIndex(a => a.Label)
                 .IsUnique();
        }
    }
}
