using Microsoft.EntityFrameworkCore;

namespace UdpTeamChatApp.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Message> Messages {get; set;}
        public DbSet<Chat> Chats {get; set;}

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(u => u.Chats)
                    .WithMany(c => c.Members);
            
            modelBuilder.Entity<Chat>()
                    .HasMany(c => c.Messages)
                    .WithOne(m => m.Chat)
                    .HasForeignKey(c => c.ChatId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}