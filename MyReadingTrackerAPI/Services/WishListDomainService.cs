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
            wishLists = database.WishList.AsNoTracking().ToList();
        }

        public WishList Add(WishList WishList)
        {
            database.WishList.Add(WishList);
            database.SaveChanges();
            return WishList;
        }

        public WishList Delete(int id)
        {
            var wishList = wishLists.Where(wl => wl.Id == id).FirstOrDefault();

            if(wishList == null)
            {
                //cannot implement ApplicationException yet?
                throw new Exception("WishList " + id + " could not be found");
            }

            database.WishList.Remove(wishList);
            database.SaveChanges();
            return wishList;
        }
    }
}
