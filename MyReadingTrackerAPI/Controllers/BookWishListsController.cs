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
    public class BookWishListsController : Controller
    {
        private IBookDomainService _bookService;
        private IWishListDomainService _wishListService;
        private IBookWishListDomainService _bookWishListService;

        public BookWishListsController(IBookDomainService bookService, IWishListDomainService wishListService,
            IBookWishListDomainService bookWishListService)
        {
            _bookService = bookService;
            _wishListService = wishListService;
            _bookWishListService = bookWishListService;
        }

        [HttpPost]
        public ActionResult Add([Bind("BookId", "WishListId")] BookWishListDto BookWishList)
        {
            if (BookWishList.BookId == 0)
            {
                return HttpBadRequest("Could not add book to wishlist. Book id:" + BookWishList.BookId + " is not a valid book id.");
            }

            if (BookWishList.WishListId == 0)
            {
                return HttpBadRequest("Could not add book to wishlist. WishList id:" + BookWishList.WishListId + " is not a valid wishlist id.");
            }

            var existingBook = _bookService.Get(BookWishList.BookId);

            if (existingBook == null)
            {
                return HttpBadRequest("Could not add book to wishlist. Book id:" + BookWishList.BookId + " does not exist.");
            }

            var existingWishList = _wishListService.Get(BookWishList.WishListId);

            if (existingWishList == null)
            {
                return HttpBadRequest("Could not add book to wishlist. WishList id:" + BookWishList.WishListId + " does not exist.");
            }

            BookWishList addedBookWishList = null;

            if (ModelState.IsValid)
            {

                var newBookWishList = new BookWishList
                {
                    Book = existingBook,
                    WishList = existingWishList
                };

                addedBookWishList = _bookService.AddToWishList(newBookWishList);
            }

            if (addedBookWishList == null)
            {
                return HttpBadRequest("Could not add book to wishlist.");
            }

            return new JsonResult(addedBookWishList);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return HttpBadRequest("BookWishList could not be deleted. BookWishList id:" + id + " is not a valid BookWishList id.");
            }

            var existingBookWishList = _bookWishListService.Get(id);

            if (existingBookWishList == null)
            {
                return HttpNotFound("No book wishlist exists with id:" + id + ".");
            }

            var deletedBookWishList = _bookWishListService.Delete(existingBookWishList);

            if (deletedBookWishList == null)
            {
                return HttpBadRequest("Book WishList deletion failed.");
            }

            return new JsonResult(deletedBookWishList);
        }
    }
}
