using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IBookLibraryDomainService
    {
        BookLibrary Get(int id);
        BookLibrary Delete(BookLibrary BookLibrary);
        List<BookLibrary> DeleteBooksFromLibrary(int libraryId);
    }
}
