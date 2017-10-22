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
        List<Book> GetByWishList(int wishListId);
        List<Book> GetByLibrary(int libraryId);
        Book Delete(Book Book);
        Book Update(Book Book);
        Book Add(Book Book);
    }
}
