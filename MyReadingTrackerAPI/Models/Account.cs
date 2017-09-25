using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Models
{
    public class Account
    {
        //hash user input and match with database entry (hashed) never store actuals
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
