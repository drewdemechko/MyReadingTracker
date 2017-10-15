using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserDomainService _userService;

        public UsersController(IUserDomainService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var users = _userService.Get();

            if (users.Count == 0)
            {
                return HttpNotFound("No users were found.");
            }

            return new JsonResult(users);
        }
    }
}
