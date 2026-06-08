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
    }

    public enum UserStatus
    {
        Offline,
        Online
    }
}
