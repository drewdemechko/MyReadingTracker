using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface ILibraryDomainService
    {
        Library Delete(Library Library);
        Library Add(Library Library);
    }
}
