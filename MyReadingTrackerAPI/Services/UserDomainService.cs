using MyReadingTrackerAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyReadingTrackerAPI.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;

namespace MyReadingTrackerAPI.Services
{
    public class UserDomainService : IUserDomainService
    {
        private AppDbContext database;
        private List<User> users;

        private ILibraryDomainService _libraryService;
        private IWishListDomainService _wishListService;

        public UserDomainService(ILibraryDomainService libraryService, IWishListDomainService wishListService)
        {
            database = new AppDbContext();
            users = database.User.Include(user => user.Account).Include(user => user.WishList)
                .Include(user => user.Library).AsNoTracking().ToList();

            _libraryService = libraryService;
            _wishListService = wishListService;
        }

        public User Add(User User)
        {
            var existingUser = users.FirstOrDefault(user => user.Account.Id == User.Account.Id);

            if(existingUser != null)
            {
                return existingUser;
            }

            User.WishList = _wishListService.Add(new WishList());
            User.Library = _libraryService.Add(new Library());

            database.User.Add(User);
            database.SaveChanges();
            return User;
        }

        public User Delete(User User)
        {
            _libraryService.Delete(User.Library);
            _wishListService.Delete(User.WishList);

            database.User.Remove(User);
            database.SaveChanges();
            return User;
        }

        public List<User> Get()
        {
            return users;
        }

        public User Get(int id, bool includeBooks)
        {
            throw new NotImplementedException();
            //HydrateBooks(id)
        }

        public User GetByAccount(int accountId)
        {
            return users.FirstOrDefault(user => user.Account.Id == accountId);
        }

        private User HydrateBooks(int id)
        {
            throw new NotImplementedException();
        }
    }
}
