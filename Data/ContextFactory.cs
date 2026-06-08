using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UdpTeamChatApp.Data
{
    public class MailingContextFactory : IDesignTimeDbContextFactory<MailingContext>
    {
        public MailingContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MailingContext> optionsBuilder = new DbContextOptionsBuilder<MailingContext>();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appSettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            string connStr = configurationRoot.GetConnectionString("Default") ?? throw new InvalidOperationException("Default connection string not found");
            optionsBuilder.UseSqlServer(connStr);
            DbContextOptions<MailingContext> options = optionsBuilder.Options;
            MailingContext mailingContext = new MailingContext(options);
            return mailingContext;
        }
    }
}