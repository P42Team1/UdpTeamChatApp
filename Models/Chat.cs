namespace UdpTeamChatApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<User> Members {get; set;} = new List<User>();
        public ICollection<Message> Messages {get; set;} = new List<Message>();
        public bool IsGroup { get; set; }
    } 
}