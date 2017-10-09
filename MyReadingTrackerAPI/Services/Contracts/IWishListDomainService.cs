using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IWishListDomainService
    {
        List<WishList> Get();
        WishList Get(int userId);
        WishList Delete(WishList WishList);
        WishList Update(WishList WishList);
        WishList Add(WishList WishList);
    }
}
