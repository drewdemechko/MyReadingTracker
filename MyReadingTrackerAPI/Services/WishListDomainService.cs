using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class WishListDomainService : IWishListDomainService
    {
        private AppDbContext database;
        private List<WishList> wishLists;
        private List<BookWishList> booksInWishlists;

        public WishListDomainService()
        {
            database = new AppDbContext();
            wishLists = database.WishList.AsNoTracking().ToList();
            booksInWishlists = database.BookWishList.Include(bookInWishList => bookInWishList.WishList).AsNoTracking().ToList();
        }

        public WishList Add(WishList WishList)
        {
            database.WishList.Add(WishList);
            database.SaveChanges();
            return WishList;
        }

        public WishList Delete(int id)
        {
            var wishList = wishLists.FirstOrDefault(wl => wl.Id == id);

            if(wishList == null)
            {
                //cannot implement ApplicationException yet?
                throw new Exception("WishList " + id + " could not be found");
            }

            DeleteBooksFromWishList(id);

            database.WishList.Remove(wishList);
            database.SaveChanges();
            return wishList;
        }

        private List<BookWishList> DeleteBooksFromWishList(int wishListId)
        {
            booksInWishlists.Where(bookInWishList => bookInWishList.WishList.Id == wishListId);

            booksInWishlists.ForEach(bookInWishList =>
            {
                database.BookWishList.Remove(bookInWishList);
            });

            database.SaveChanges();
            return booksInWishlists;
        }
    }
}
