using MyReadingTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyReadingTrackerAPI.Services.Contracts
{
    public interface ILibraryDomainService
    {
        List<Library> Get();
        Library Get(int userId);
        Library Delete(Library Library);
        Library Update(Library Library);
        Library Add(Library Library);
    }
}
