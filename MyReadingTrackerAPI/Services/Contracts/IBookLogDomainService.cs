using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IBookLogDomainService
    {
        BookLog Get(int id);
        BookLog Delete(BookLog BookLog);
        List<BookLog> DeleteBooksFromLog(int logId);
    }
}
