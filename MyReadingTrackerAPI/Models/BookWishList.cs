using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class BookWishList
    {
        public int Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual WishList WishList { get; set; }
    }
}
