using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class BookWishListDomainService : IBookWishListDomainService
    {
        private AppDbContext database;
        private List<BookWishList> booksInWishLists;

        public BookWishListDomainService()
        {
            database = new AppDbContext();
            booksInWishLists = database.BookWishList.AsNoTracking().ToList();
        }

        public BookWishList Delete(BookWishList BookWishList)
        {
            database.BookWishList.Remove(BookWishList);
            database.SaveChanges();
            return BookWishList;
        }

        public List<BookWishList> DeleteBooksFromWishList(int wishListId)
        {
            booksInWishLists.Where(bookInWishList => bookInWishList.WishList.Id == wishListId);

            booksInWishLists.ForEach(bookInWishList =>
            {
                database.BookWishList.Remove(bookInWishList);
            });

            database.SaveChanges();
            return booksInWishLists;
        }

        public BookWishList Get(int id)
        {
            return booksInWishLists.FirstOrDefault(bookWishList => bookWishList.Id == id);
        }
    }
}
