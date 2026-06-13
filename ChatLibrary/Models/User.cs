using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public UserStatus Status { get; set; }
        public DateTime OfflineFromTime { get; set; }
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public int LoginDataId { get; set; }
        public UserLoginData LoginData { get; set; }
    }

    public enum UserStatus
    {
        Offline,
        Online
    }

    public class UserLoginData
    {
        public int Id {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
    }
}
