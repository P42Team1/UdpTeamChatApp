using System.ComponentModel;

namespace CharLibrary
{
    public class User
    {
        public int Id { get; set; }

        public string IPAddress { get; set; }
        public int Port { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string username, string password, string email) 
        {
            Username = username;
            Password = password;
            Email = email;
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public UserStatus Status { get; set; }
        public DateTime OfflineFromTime { get; set; }

    }

    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isOnline { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int ChatId { get; set; }

        public User Author { get; set; }
        public Chat Chat { get; set; }
        public StatusDelivered Status { get; set; }

    }

    public enum StatusDelivered
    {
        Received,
        NotReceived
    }

    public enum UserStatus
    {
        Offline,
        Online
    }


}
