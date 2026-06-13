using ChatLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatLibrary.Data
{
    public class Service
    {
        private readonly ChatContext Context;

        public Service(ChatContext context)
        {
            Context = context;
        }
        public async Task<bool> RegisterUserAsync(UserLoginData user)
        {
            bool exists = await Context.UserLoginDataPoints.AnyAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (exists) return false;
            var _user = new User
            {
                IPAddress = "0",
                Status = UserStatus.Offline
            };
            await Context.Users.AddAsync(_user);
            await Context.SaveChangesAsync();

            var loginData = new UserLoginData(user.Username, user.Password, user.Email)
            {
                UserId = _user.Id
            };
            await Context.UserLoginDataPoints.AddAsync(loginData);
            await Context.SaveChangesAsync();


            return true;
        }

        public async Task<UserLoginData?> UserLoginAsync(string username, string password)
        {
            return await Context.UserLoginDataPoints.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task AddObjects(params object[] objects)
        {
            await Context.AddRangeAsync(objects);
        }

        public async Task SaveDbChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}