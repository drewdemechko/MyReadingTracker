using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class BookLogDomainService : IBookLogDomainService
    {
        private AppDbContext database;
        private List<BookLog> booksInLogs;
        private ILogEntryDomainService _logEntryService;

        public BookLogDomainService(ILogEntryDomainService logEntryService)
        {
            database = new AppDbContext();
            booksInLogs = database.BookLog.AsNoTracking().ToList();
            _logEntryService = logEntryService;
        }

        public BookLog Delete(BookLog BookLog)
        {
            _logEntryService.DeleteEntriesFromBookLog(BookLog.Id);
            database.BookLog.Remove(BookLog);
            database.SaveChanges();
            return BookLog;
        }

        public List<BookLog> DeleteBooksFromLog(int logId)
        {
            booksInLogs.Where(bookInLog => bookInLog.Log.Id == logId);

            booksInLogs.ForEach(bookInLog =>
            {
                _logEntryService.DeleteEntriesFromBookLog(bookInLog.Id);
                database.BookLog.Remove(bookInLog);
            });

            database.SaveChanges();
            return booksInLogs;
        }

        public BookLog Get(int id)
        {
            return booksInLogs.FirstOrDefault(bookInLog => bookInLog.Id == id);
        }
    }
}
