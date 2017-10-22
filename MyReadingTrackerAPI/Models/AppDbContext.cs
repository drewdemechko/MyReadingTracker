using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;

namespace MyReadingTrackerAPI.Models
{
    public class AppDbContext : DbContext
    {
        private static bool _created = false;

        public AppDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json");
            Configuration = configuration.Build();
            optionsBuilder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<BookLibrary>().HasKey(bl => new { bl.BookId, bl.LibraryId });

            //builder.Entity<BookLibrary>()
            //    .HasOne(bl => bl.Book)
            //    .WithMany(b => b.BookLibraries)
            //    .HasForeignKey(bl => bl.BookId);

            //builder.Entity<BookLibrary>()
            //    .HasOne(bl => bl.Library)
            //    .WithMany(b => b.BookLibraries)
            //    .HasForeignKey(bl => bl.LibraryId);

            //builder.Entity<BookWishList>().HasKey(bwl => new { bwl.BookId, bwl.WishListId });

            //builder.Entity<BookWishList>()
            //    .HasOne(bwl => bwl.Book)
            //    .WithMany(bwl => bwl.BookWishLists)
            //    .HasForeignKey(bwl => bwl.BookId);

            //builder.Entity<BookWishList>()
            //    .HasOne(bwl => bwl.WishList)
            //    .WithMany(bwl => bwl.BookWishLists)
            //    .HasForeignKey(bwl => bwl.WishListId);
        }

        //Entities in database
        public DbSet<Account> Account { get; set; } //All entities have an account
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<BookWishList> BookWishList { get; set; }
        public DbSet<BookLibrary> BookLibrary { get; set; }
    }
}
