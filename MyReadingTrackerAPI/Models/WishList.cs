using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class WishList
    {
        public virtual User User { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
