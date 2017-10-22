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

        public User Delete(User User)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            return users;
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User User)
        {
            throw new NotImplementedException();
        }
    }
}
