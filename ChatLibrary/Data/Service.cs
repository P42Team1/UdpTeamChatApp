namespace UdpTeamChatApp.Data
{
    public class Service
    {
        private readonly ChatContext Context;

        public Service(ChatContext context)
        {
            Context = context;
        }
    }
}