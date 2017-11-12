using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IBookWishListDomainService
    {
        BookWishList Get(int id);
        BookWishList Delete(BookWishList BookWishList);
        List<BookWishList> DeleteBooksFromWishList(int wishListId);
    }
}
