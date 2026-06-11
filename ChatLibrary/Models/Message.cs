namespace UdpTeamChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public MessageStatus Status { get; set; }
    }

    public enum MessageStatus
    {
        Received,
        NotReceived
    }
}