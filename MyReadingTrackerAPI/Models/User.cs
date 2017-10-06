using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual Library Library { get; set; }
        public virtual WishList WishList { get; set; }
    }
}
