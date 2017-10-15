using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class LibraryDomainService : ILibraryDomainService
    {
        private AppDbContext database;
        private List<Library> libraries;

        public LibraryDomainService()
        {
            database = new AppDbContext();
            libraries = database.Library.Include(library => library.BookLibraries).AsNoTracking().ToList();
        }

        public Library Add(Library Library)
        {
            database.Library.Add(Library);
            database.SaveChanges();
            return Library;
        }

        public Library Delete(Library Library)
        {
            throw new NotImplementedException();
        }

        public List<Library> Get()
        {
            throw new NotImplementedException();
        }

        public Library Get(int userId)
        {
            throw new NotImplementedException();
        }

        public Library Update(Library Library)
        {
            throw new NotImplementedException();
        }
    }
}
