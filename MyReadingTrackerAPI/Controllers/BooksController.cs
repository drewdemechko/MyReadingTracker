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
    public class BooksController : Controller
    {
        private IBookDomainService _bookService;

        public BooksController(IBookDomainService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var books = _bookService.Get();

            if(books.Count == 0)
            {
                return HttpNotFound("No books were found.");
            }

            return new JsonResult(books);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            //not a valid id, should only allow integer values
            if (id == 0)
            {
                return HttpBadRequest("Book could not be retreived. Book id:" + id + " is not a valid book id.");
            }

            var book = _bookService.Get(id);

            if (book == null)
            {
                return HttpNotFound("No book exists with id:" +  id + ".");
            }

            return new JsonResult(book);
        }

        [HttpPost]
        public ActionResult Add([Bind("Title", "GoogleId", "PublishedDate", "Isbn", "PageCount", "Image")] Book NewBook)
        {
            Book newBook = null;

            var existingBookWithIsbn = _bookService.GetByIsbn(NewBook.Isbn);

            if(existingBookWithIsbn != null)
            {
                return HttpBadRequest("A book already exists with isbn:" + NewBook.Isbn.Value + ".");
            }

            var existingBookWithGoogleId = _bookService.GetByGoogleId(NewBook.GoogleId);

            if(existingBookWithGoogleId != null)
            {
                return HttpBadRequest("A book already exists with google id:" + NewBook.GoogleId + ".");
            }

            if (ModelState.IsValid)
            {
                newBook = _bookService.Add(NewBook);

                if (newBook == null)
                {
                    return HttpBadRequest("Book creation failed.");
                }
            }

            return new JsonResult(newBook);
        }
    }
}
