﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class Library
    {
        public int Id { get; set; }
        public ICollection<BookLibrary> BookLibraries {get;set;}
    }
}
