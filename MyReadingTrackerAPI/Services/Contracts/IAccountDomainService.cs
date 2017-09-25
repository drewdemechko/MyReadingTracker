using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services
{
    public interface IAccountDomainService
    {
        List<Account> Get();
        Account Get(int id);
        Account Get(string username);
        Account Delete(Account Account);
        Account Update(Account Account);
        Account Add(Account Account);
        Account Login(string username, string hashedPassword);
    }
}
