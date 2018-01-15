using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;

namespace MyReadingTrackerAPI.Services
{
    public class LogEntryDomainService : ILogEntryDomainService
    {
        private AppDbContext database;
        private List<LogEntry> entries;

        public LogEntryDomainService()
        {
            database = new AppDbContext();
            entries = database.LogEntry.AsNoTracking().ToList();
        }

        public LogEntry Delete(LogEntry LogEntry)
        {
            database.LogEntry.Remove(LogEntry);
            database.SaveChanges();
            return LogEntry;
        }

        public List<LogEntry> DeleteEntriesFromBookLog(int bookLogId)
        {
            entries.Where(entry => entry.BookLog.Id == bookLogId);

            entries.ForEach(entry =>
            {
                database.LogEntry.Remove(entry);
            });

            database.SaveChanges();
            return entries;
        }

        public LogEntry Get(int id)
        {
            return entries.FirstOrDefault(entry => entry.Id == id);
        }
    }
}
