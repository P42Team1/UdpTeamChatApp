namespace UdpTeamChatApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public UserStatus Status { get; set; }
        public DateTime OfflineFromTime { get; set; }

    }

    public enum UserStatus
    {
        Offline,
        Online
    }
}