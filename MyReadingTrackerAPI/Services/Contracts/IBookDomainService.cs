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
        Book Delete(Book Book);
        Book Update(Book Book);
        Book Add(Book Book);
    }
}
