using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IBookDomainService
    {
        List<Book> Get();
        Book Get(int id);
        Book GetByIsbn(int? isbn);
        Book GetByGoogleId(string googleId);
        List<Book> GetByWishList(int wishListId);
        List<Book> GetByLibrary(int libraryId);
        Book Delete(Book Book);
        BookWishList DeleteFromWishList(BookWishList BookWishList);
        BookLibrary DeleteFromLibrary(BookLibrary BookLibrary);
        Book Update(Book Book);
        Book Add(Book Book);
        BookWishList AddToWishList(BookWishList BookWishList);
        BookLibrary AddToLibrary(BookLibrary BookLibrary);
    }
}
