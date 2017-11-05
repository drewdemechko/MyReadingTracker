using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class UserDto
    {
        public UserDto(User User)
        {
            this.Id = User.Id;
            this.Account = User.Account;
            this.Library = User.Library;
            this.WishList = User.WishList;
        }

        public int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual Library Library { get; set; }
        public List<Book> BooksInLibrary { get; set; }
        public List<Book> BooksInWishList { get; set; }
    }
}
