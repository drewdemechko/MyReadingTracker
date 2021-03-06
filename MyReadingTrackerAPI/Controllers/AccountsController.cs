﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyReadingTrackerAPI.Services;
using MyReadingTrackerAPI.Models;
using MyReadingTrackerAPI.Services.Contracts;

namespace MyReadingTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private IAccountDomainService _accountService;
        private IUserDomainService _userService;

        public AccountsController(IAccountDomainService accountService, IUserDomainService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var accounts = _accountService.Get();

            if(accounts.Count == 0)
            {
                return HttpNotFound("No accounts were found.");
            }

            return new JsonResult(accounts);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            //not a valid id, should only allow integer values
            if(id == 0)
            {
                return HttpBadRequest("Account could not be retreived. Account id:" + id + " is not a valid account id.");
            }

            var account = _accountService.Get(id);

            if (account == null)
            {
                return HttpNotFound("No account exists with id:" + id + ".");
            }

            return new JsonResult(account);
        }

        [HttpPost]
        public ActionResult Add([Bind("Username", "Password")]Account NewAccount)
        {
            var isValidUsername = AccountDomainService.isValidUsername(NewAccount.Username);
            //var isValidPassword = AccountDomainService.isValidPassword(NewAccount.Password);

            if(!isValidUsername)
            {
                return HttpBadRequest("The username chosen is not valid.");
            }

            Account newAccount = null;

            var existingAccount = _accountService.Get(NewAccount.Username);

            if (existingAccount != null)
            {
                return HttpBadRequest("An account already exists with that username.");
            }

            if (ModelState.IsValid)
            {
                newAccount = _accountService.Add(NewAccount);

                if (newAccount == null)
                {
                    return HttpBadRequest("Account creation failed.");
                }

                var newUser = new User
                {
                    Account = newAccount
                };

                try
                {
                    _userService.Add(newUser);
                }
                catch (Exception)
                {
                    _accountService.Delete(newAccount);
                    return HttpBadRequest("Account creation failed. Unable to create user.");
                }
            }

            return new JsonResult(newAccount);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Edit([Bind("Username", "Password")]int id, Account UpdatedAccount)
        {
            //not a valid id, should only allow integer values
            if (id == 0)
            {
                return HttpBadRequest("Account could not be edited. Account id:" + id + " is not a valid account id.");
            }

            var existingAccount = _accountService.Get(id);

            if(existingAccount == null)
            {
                return HttpBadRequest("An account with id " + id + " does not exist.");
            }

            var updatedAccount = _accountService.Update(UpdatedAccount);

            if(updatedAccount == null)
            {
                return HttpBadRequest("Account update failed.");
            }

            return new JsonResult(updatedAccount);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return HttpBadRequest("Account could not be deleted. Account id:" + id + " is not a valid account id.");
            }

            var account = _accountService.Get(id);

            if (account == null)
            {
                return HttpNotFound("No account exists with id:" + id + ".");
            }

            var deletedAccount = _accountService.Delete(account);

            if(deletedAccount == null)
            {
                return HttpBadRequest("Account deletion failed.");
            }

            return new JsonResult(deletedAccount);
        }
    }
}
