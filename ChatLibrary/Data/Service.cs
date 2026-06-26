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

        public async Task AddObjects(params object[] objects)
        {
            await Context.AddRangeAsync(objects);
            await Context.SaveChangesAsync();
        }

        public async Task SaveDbChanges()
        {
            await Context.SaveChangesAsync();
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

        public async Task<UserLoginData?> GetUserLoginAsync(string username, string password)
        {
            return await Context.UserLoginDataPoints.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            return await Context.Chats.ToListAsync();
        }

        public async Task<List<Chat>?> GetAllChatsOfUser(User user)
        {
            User u = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) ?? throw new ArgumentException("User does not exist");
            return await Context.Chats.Include(c => c.Members).Where(c => c.Members.Contains(u)).ToListAsync();
        }

        public async Task<Chat> CreateChatAsync(string name)
        {
            var chat = new Chat
            {
                Name = name,
                IsGroup = true,
            };
            await Context.Chats.AddAsync(chat);
            await Context.SaveChangesAsync();
            return chat;
        }

        public async Task<List<User>?> GetOnlineUsers()
        {
            return await Context.Users.Include(u => u.Status)
                                .Where(u => u.Status == UserStatus.Online)
                                .ToListAsync();
        }

        public async Task<User> SetUserOnline(User user)
        {
            User u = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) ?? throw new ArgumentException("User does not exist");
            u.Status = UserStatus.Online;
            await Context.SaveChangesAsync();
            return u;
        }

        public async Task<User> SetUserOffline(User user)
        {
            User u = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) ?? throw new ArgumentException("User does not exist");
            u.Status = UserStatus.Offline;
            u.OfflineFromTime = DateTime.Now;
            await Context.SaveChangesAsync();
            return u;
        }
    }
}