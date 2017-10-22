using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Controllers
{
    public class BooksController : Controller
    {
        public BooksController(IBookDomainService bookService)
        {
            //Add to users library (a stored procedure, should this be a put)
            //Add to users wish list (a stored procedure, should this be a put)
        }
    }
}
