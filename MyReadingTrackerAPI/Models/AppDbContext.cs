﻿using Microsoft.Data.Entity;
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
        }

        //Entities in database
        public DbSet<Account> Account { get; set; } //All entities have an account
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<LogEntry> LogEntry { get; set; }
        public DbSet<BookWishList> BookWishList { get; set; }
        public DbSet<BookLibrary> BookLibrary { get; set; }
        public DbSet<BookLog> BookLog { get; set; }
    }
}
