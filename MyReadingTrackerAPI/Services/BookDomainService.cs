using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class BookDomainService : IBookDomainService
    {
        private AppDbContext database;
        private List<Book> books;
        private List<BookLibrary> booksInLibrary;
        private List<BookWishList> booksInWishList;

        public BookDomainService()
        {
            database = new AppDbContext();
            books = database.Book.AsNoTracking().ToList();
            booksInLibrary = database.BookLibrary.Include(book => book.Book).Include(book => book.Library)
                .AsNoTracking().ToList();
            booksInWishList = database.BookWishList.Include(book => book.Book).Include(book => book.WishList)
                .AsNoTracking().ToList();
        }

        public Book Add(Book Book)
        {
            database.Book.Add(Book);
            database.SaveChanges();
            return Book;
        }

        public Book Delete(Book Book)
        {
            database.Book.Remove(Book);
            database.SaveChanges();
            return Book;
        }

        public List<Book> Get()
        {
            return books;
        }

        private List<Book> Get(List<int> ids)
        {
            return books.Where(book => ids.Contains(book.Id)).ToList();
        }

        public Book Get(int id)
        {
            return books.FirstOrDefault(book => book.Id == id);
        }

        public Book GetByIsbn(int? isbn)
        {
            if(isbn == null)
            {
                return null;
            }

            return books.FirstOrDefault(book => book.Isbn == isbn);
        }

        public Book GetByGoogleId(string googleId)
        {
            if(googleId == string.Empty)
            {
                return null;
            }

            return books.FirstOrDefault(book => book.GoogleId == googleId);
        }

        public List<Book> GetByWishList(int wishListId)
        {
            var bookIds = booksInWishList.Where(book => book.WishList.Id == wishListId).Select(book => book.Id).ToList();
            return Get(bookIds);
        }

        public List<Book> GetByLibrary(int libraryId)
        {
            var bookIds = booksInLibrary.Where(book => book.Library.Id == libraryId).Select(book => book.Id).ToList();
            return Get(bookIds);
        }

        public BookLibrary AddToLibrary(BookLibrary BookLibrary)
        {
            database.BookLibrary.Add(BookLibrary);
            database.SaveChanges();
            return BookLibrary;
        }

        public BookLibrary DeleteFromLibrary(BookLibrary BookLibrary)
        {
            database.BookLibrary.Remove(BookLibrary);
            database.SaveChanges();
            return BookLibrary;
        }

        public BookWishList AddToWishList(BookWishList BookWishList)
        {
            database.BookWishList.Add(BookWishList);
            database.SaveChanges();
            return BookWishList;
        }

        public BookWishList DeleteFromWishList(BookWishList BookWishList)
        {
            database.BookWishList.Remove(BookWishList);
            database.SaveChanges();
            return BookWishList;
        }

        public Book Update(Book Book)
        {
            database.Book.Update(Book);
            database.SaveChanges();
            return Book;
        }
    }
}
