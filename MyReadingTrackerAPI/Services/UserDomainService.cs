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
        private IBookDomainService _bookService;

        public UserDomainService(ILibraryDomainService libraryService, IWishListDomainService wishListService,
            IBookDomainService bookService)
        {
            database = new AppDbContext();
            users = database.User.Include(user => user.Account).Include(user => user.WishList)
                .Include(user => user.Library).AsNoTracking().ToList();

            _libraryService = libraryService;
            _wishListService = wishListService;
            _bookService = bookService;
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

        public UserDto Get(int id, bool includeBooks)
        {
            UserDto userDto = null;
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                userDto = new UserDto(user);
                userDto = HydrateBooks(userDto);
            }

            return userDto;
        }

        public User GetByAccount(int accountId)
        {
            return users.FirstOrDefault(user => user.Account.Id == accountId);
        }

        private UserDto HydrateBooks(UserDto UserDto)
        {
            UserDto.BooksInLibrary = _bookService.GetByLibrary(UserDto.Library.Id);
            UserDto.BooksInWishList = _bookService.GetByWishList(UserDto.WishList.Id);
            return UserDto;
        }
    }
}
