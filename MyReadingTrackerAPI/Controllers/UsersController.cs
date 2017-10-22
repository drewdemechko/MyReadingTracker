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

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id, bool includeBooks)
        {
            //not a valid id, should only allow integer values
            if (id == 0)
            {
                return HttpBadRequest("User could not be retreived. User id:" + id + " is not a valid user id.");
            }

            var user = _userService.Get(id, includeBooks);

            if (user == null)
            {
                return HttpNotFound("No user exists with id:" + id + ".");
            }

            return new JsonResult(user);
        }
    }
}
