using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Model;

namespace WebCore.Infra
{
    public partial class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Startup.Configuration["ConnectionStrings:BlogConnection"].ToString();
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(x => x.Id);
            modelBuilder.Entity<Post>().Property(x => x.Id);
            modelBuilder.Entity<Post>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<Post>().Property(x => x.Content).IsRequired();

            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().Property(x => x.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Comment>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(x => x.CreateDate).ForSqlServerHasDefaultValue<DateTime>(DateTime.Now);

            modelBuilder.Entity<Post>().HasMany<Comment>(x => x.Comments).WithOne();
        }
    }
}
