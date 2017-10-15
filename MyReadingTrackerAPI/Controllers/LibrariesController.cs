using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Models;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Controllers
{
    public class LibrariesController : Controller
    {
        private ILibraryDomainService _libraryService;
        public LibrariesController(ILibraryDomainService libraryService)
        {
            _libraryService = libraryService;
        }
    }
}
