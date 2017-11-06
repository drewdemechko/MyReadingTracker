using Microsoft.Data.Entity;
using MyReadingTrackerAPI.Models;
using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services
{
    public class AccountDomainService : IAccountDomainService
    {
        private IUserDomainService _userService;
        private AppDbContext database;
        private List<Account> accounts;

        public AccountDomainService(IUserDomainService userService)
        {
            _userService = userService;
            database = new AppDbContext();
            accounts = database.Account.AsNoTracking().ToList();
        }

        public Account Add(Account Account)
        {
            database.Account.Add(Account);
            database.SaveChanges();
            return Account;
        }

        public Account Delete(Account Account)
        {
            var user = _userService.GetByAccount(Account.Id);

            if (user != null)
            {
                _userService.Delete(user);
            }

            database.Account.Remove(Account);
            database.SaveChanges();
            return Account;
        }

        public Account Update(Account Account)
        {
            var oldAccount = Get(Account.Id);
            var newAccount = Account;

            if (areEqual(oldAccount, newAccount))
            {
                return oldAccount;
            }

            database.Account.Update(Account);
            database.SaveChanges();
            return Account;
        }

        public List<Account> Get()
        {
            return accounts;
        }

        public Account Get(int id)
        {
            return accounts.FirstOrDefault(account => account.Id == id);
        }

        public Account Get(string username)
        {
            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public Account Login(string username, string hashedPassword)
        {
            //always hash password on client before sending to server. Never read unhashed password.
            throw new NotImplementedException();
        }

        public static bool isValidUsername(string username)
        {
            //valid username violations
            return true;
        }

        private static bool areEqual(Account a, Account b)
        {
            return a.Username == b.Username && a.Password == b.Password;
        }
    }
}
