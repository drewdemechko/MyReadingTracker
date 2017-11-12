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
        private IBookLibraryDomainService _bookLibraryService;

        public LibraryDomainService(IBookLibraryDomainService bookLibraryService)
        {
            database = new AppDbContext();
            libraries = database.Library.AsNoTracking().ToList();
            booksInLibraries = database.BookLibrary.Include(bookInLibrary => bookInLibrary.Library)
                .AsNoTracking().ToList();
            _bookLibraryService = bookLibraryService;
        }

        public Library Get(int id)
        {
            return libraries.FirstOrDefault(library => library.Id == id);
        }

        public Library Add(Library Library)
        {
            database.Library.Add(Library);
            database.SaveChanges();
            return Library;
        }

        public Library Delete(Library Library)
        {
            _bookLibraryService.DeleteBooksFromLibrary(Library.Id);

            database.Library.Remove(Library);
            database.SaveChanges();
            return Library;
        }
    }
}
