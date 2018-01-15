using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class BookLog
    {
        public int Id { get; set; }
        public virtual Log Log { get; set; }
        public virtual Book Book { get; set; }
    }
}
