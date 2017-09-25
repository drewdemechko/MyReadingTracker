using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public Account Account { get; set; }

        public List<Book> Books { get; set; }
    }
}
