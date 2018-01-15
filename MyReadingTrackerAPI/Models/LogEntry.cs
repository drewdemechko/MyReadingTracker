using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public virtual BookLog BookLog { get; set; }
        public DateTime Date { get; set; }
        public int PagesRead { get; set; }
    }
}
