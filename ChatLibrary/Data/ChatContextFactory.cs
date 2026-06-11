using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UdpTeamChatApp.Data
{
    public class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
    {
        public ChatContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ChatContext> optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appSettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            string connStr = configurationRoot.GetConnectionString("Default") ?? throw new InvalidOperationException("Default connection string not found");
            optionsBuilder.UseSqlServer(connStr);
            DbContextOptions<ChatContext> options = optionsBuilder.Options;
            ChatContext chatContext = new ChatContext(options);
            return chatContext;
        }
    }
}