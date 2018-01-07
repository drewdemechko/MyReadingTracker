using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Models;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookLibrariesController : Controller
    {
        private IBookDomainService _bookService;
        private ILibraryDomainService _libraryService;
        private IBookLibraryDomainService _bookLibraryService;

        public BookLibrariesController(IBookDomainService bookService, ILibraryDomainService libraryService,
            IBookLibraryDomainService bookLibraryService)
        {
            _bookService = bookService;
            _libraryService = libraryService;
            _bookLibraryService = bookLibraryService;
        }

        [HttpPost]
        public ActionResult Add([Bind("BookId", "LibraryId")] BookLibraryDto BookLibrary)
        {
            if (BookLibrary.BookId == 0)
            {
                return HttpBadRequest("Could not add book to library. Book id:" + BookLibrary.BookId + " is not a valid book id.");
            }

            if (BookLibrary.LibraryId == 0)
            {
                return HttpBadRequest("Could not add book to library. Library id:" + BookLibrary.LibraryId + " is not a valid library id.");
            }

            var existingBook = _bookService.Get(BookLibrary.BookId);

            if(existingBook == null)
            {
                return HttpBadRequest("Could not add book to library. Book id:" + BookLibrary.BookId + " does not exist.");
            }

            var existingLibrary = _libraryService.Get(BookLibrary.LibraryId);

            if(existingLibrary == null)
            {
                return HttpBadRequest("Could not add book to library. Library id:" + BookLibrary.LibraryId + " does not exist.");
            }

            BookLibrary addedBookLibrary = null;

            if (ModelState.IsValid)
            {

                var newBookLibrary = new BookLibrary
                {
                    Book = existingBook,
                    Library = existingLibrary
                };

                addedBookLibrary = _bookService.AddToLibrary(newBookLibrary);
            }

            if (addedBookLibrary == null)
            {
                return HttpBadRequest("Could not add book to library.");
            }

            return new JsonResult(addedBookLibrary);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if(id == 0)
            {
                return HttpBadRequest("BookLibrary could not be deleted. BookLibrary id:" + id + " is not a valid BookLibrary id.");
            }

            var existingBookLibrary = _bookLibraryService.Get(id);

            if(existingBookLibrary == null)
            {
                return HttpNotFound("No book library exists with id:" + id + ".");
            }

            var deletedBookLibrary = _bookLibraryService.Delete(existingBookLibrary);

            if(deletedBookLibrary == null)
            {
                return HttpBadRequest("Book Library deletion failed.");
            }

            return new JsonResult(deletedBookLibrary);
        }
    }
}
