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
            booksInLibraries = database.BookLibrary.Include(bookInLibrary => bookInLibrary.Library).AsNoTracking().ToList();
        }

        public Library Add(Library Library)
        {
            database.Library.Add(Library);
            database.SaveChanges();
            return Library;
        }

        public Library Delete(int id)
        {
            var library = libraries.Where(l => l.Id == id).FirstOrDefault();

            if (library == null)
            {
                //cannot implement ApplicationException yet?
                throw new Exception("Library " + id + " could not be found");
            }

            DeleteBooksFromLibrary(id);

            database.Library.Remove(library);
            database.SaveChanges();
            return library;
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
