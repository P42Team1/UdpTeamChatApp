namespace UdpTeamChatApp.Data
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
        }

        public async Task SaveDbChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}