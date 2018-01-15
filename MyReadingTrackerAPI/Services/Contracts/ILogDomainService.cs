using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface ILogDomainService
    {
        Log Get(int id);
        Log Delete(Log Log);
        Log Add(Log Log);
    }
}
