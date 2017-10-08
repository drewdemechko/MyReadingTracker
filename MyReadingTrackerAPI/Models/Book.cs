using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        //id given by the google books api to find information
        public int GoogleId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        //ISBN-13
        public int Isbn { get; set; }
        public int PageCount { get; set; }
        public string Image { get; set; }
        public ICollection<BookLibrary> BookLibraries { get; set; }
        public ICollection<BookWishList> BookWishLists { get; set; }
    }
}
