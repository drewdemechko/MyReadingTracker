using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface ILogEntryDomainService
    {
        LogEntry Get(int id);
        LogEntry Delete(LogEntry LogEntry);
        List<LogEntry> DeleteEntriesFromBookLog(int bookLogId);
    }
}
