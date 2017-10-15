using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Controllers
{
    public class WishListsController : Controller
    {
        private IWishListDomainService _wishListService;

        public WishListsController(IWishListDomainService wishListService)
        {
            _wishListService = wishListService;
        }
    }
}
