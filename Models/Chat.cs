namespace UdpTeamChatApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Members {get; set;}
        public bool IsGroup { get; set; }
    } 
}