using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class BookLibraryDomainService : IBookLibraryDomainService
    {
        private AppDbContext database;
        private List<BookLibrary> booksInLibraries;

        public BookLibraryDomainService()
        {
            database = new AppDbContext();
            booksInLibraries = database.BookLibrary.AsNoTracking().ToList();
        }

        public BookLibrary Delete(BookLibrary BookLibrary)
        {
            database.BookLibrary.Remove(BookLibrary);
            database.SaveChanges();
            return BookLibrary;
        }

        public List<BookLibrary> DeleteBooksFromLibrary(int libraryId)
        {
            booksInLibraries.Where(bookInLibrary => bookInLibrary.Library.Id == libraryId);

            booksInLibraries.ForEach(bookInLibrary =>
            {
                database.BookLibrary.Remove(bookInLibrary);
            });

            database.SaveChanges();
            return booksInLibraries;
        }

        public BookLibrary Get(int id)
        {
            return booksInLibraries.FirstOrDefault(bookLibrary => bookLibrary.Id == id);
        }
    }
}
