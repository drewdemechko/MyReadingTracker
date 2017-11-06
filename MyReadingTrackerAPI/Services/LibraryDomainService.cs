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
        private List<BookLibrary> booksInLibraries;

        public LibraryDomainService()
        {
            database = new AppDbContext();
            libraries = database.Library.AsNoTracking().ToList();
            booksInLibraries = database.BookLibrary.Include(bookInLibrary => bookInLibrary.Library)
                .AsNoTracking().ToList();
        }

        public Library Add(Library Library)
        {
            database.Library.Add(Library);
            database.SaveChanges();
            return Library;
        }

        public Library Delete(Library Library)
        {
            DeleteBooksFromLibrary(Library.Id);

            database.Library.Remove(Library);
            database.SaveChanges();
            return Library;
        }

        private List<BookLibrary> DeleteBooksFromLibrary(int libraryId)
        {
            booksInLibraries.Where(bookInLibrary => bookInLibrary.Library.Id == libraryId);

            booksInLibraries.ForEach(bookInLibrary =>
            {
                database.BookLibrary.Remove(bookInLibrary);
            });

            database.SaveChanges();
            return booksInLibraries;
        }
    }
}
