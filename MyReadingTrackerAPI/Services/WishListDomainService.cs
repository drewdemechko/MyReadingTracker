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

        public WishListDomainService()
        {
            database = new AppDbContext();
            wishLists = database.WishList.Include(wishList => wishList.BookWishLists).AsNoTracking().ToList();
        }

        public WishList Add(WishList WishList)
        {
            database.WishList.Add(WishList);
            database.SaveChanges();
            return WishList;
        }

        public WishList Delete(WishList WishList)
        {
            throw new NotImplementedException();
        }

        public List<WishList> Get()
        {
            throw new NotImplementedException();
        }

        public WishList Get(int userId)
        {
            throw new NotImplementedException();
        }

        public WishList Update(WishList WishList)
        {
            throw new NotImplementedException();
        }
    }
}
