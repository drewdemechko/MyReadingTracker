using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class LogDomainService : ILogDomainService
    {
        private AppDbContext database;
        private List<Log> logs;
        private List<BookLog> booksInLogs;
        private IBookLogDomainService _bookLogService;
        private ILogEntryDomainService _logEntryService;

        public LogDomainService(IBookLogDomainService bookLogService, ILogEntryDomainService logEntryService)
        {
            database = new AppDbContext();
            logs = database.Log.AsNoTracking().ToList();
            booksInLogs = database.BookLog.Include(bookInLog => bookInLog.Log).AsNoTracking().ToList();
            _bookLogService = bookLogService;
            _logEntryService = logEntryService;
        }

        public Log Add(Log Log)
        {
            database.Log.Add(Log);
            database.SaveChanges();
            return Log;
        }

        public Log Delete(Log Log)
        {
            _bookLogService.DeleteBooksFromLog(Log.Id);

            database.Log.Remove(Log);
            database.SaveChanges();
            return Log;
        }

        public Log Get(int id)
        {
            return logs.FirstOrDefault(log => log.Id == id);
        }
    }
}
