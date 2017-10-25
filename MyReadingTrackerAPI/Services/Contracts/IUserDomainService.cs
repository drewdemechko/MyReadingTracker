using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface IUserDomainService
    {
        List<User> Get();
        User Get(int id, bool includeBooks);
        User Delete(int id);
        User DeleteByAccountId(int accountId);
        User Add(User User);
    }
}
