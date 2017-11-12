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
        private IBookWishListDomainService _bookWishListService;

        public WishListDomainService(IBookWishListDomainService bookWishListService)
        {
            database = new AppDbContext();
            wishLists = database.WishList.AsNoTracking().ToList();
            booksInWishlists = database.BookWishList.Include(bookInWishList => bookInWishList.WishList)
                .AsNoTracking().ToList();
            _bookWishListService = bookWishListService;
        }

        public WishList Get(int id)
        {
            return wishLists.FirstOrDefault(wishList => wishList.Id == id);
        }

        public WishList Add(WishList WishList)
        {
            database.WishList.Add(WishList);
            database.SaveChanges();
            return WishList;
        }

        public WishList Delete(WishList WishList)
        {
            _bookWishListService.DeleteBooksFromWishList(WishList.Id);

            database.WishList.Remove(WishList);
            database.SaveChanges();
            return WishList;
        }
    }
}
