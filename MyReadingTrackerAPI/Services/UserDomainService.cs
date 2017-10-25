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
            var existingUserWithMatchingAccount = users.Find(user => user.Account.Id == User.Account.Id);

            if(existingUserWithMatchingAccount != null)
            {
                return existingUserWithMatchingAccount;
            }

            User.WishList = _wishListService.Add(new WishList());
            User.Library = _libraryService.Add(new Library());

            database.User.Add(User);
            database.SaveChanges();
            return User;
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User DeleteByAccountId(int accountId)
        {
            var userToDelete = users.Where(user => user.Account.Id == accountId).FirstOrDefault();

            _libraryService.Delete(userToDelete.Library.Id);
            _wishListService.Delete(userToDelete.WishList.Id);

            database.User.Remove(userToDelete);
            database.SaveChanges();
            return userToDelete;
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

        private User HydrateBooks(int id)
        {
            throw new NotImplementedException();
        }
    }
}
