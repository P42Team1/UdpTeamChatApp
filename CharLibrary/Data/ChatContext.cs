using ChatLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace UdpTeamChatApp.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ChatAppDB;User Id=user;Password=password;TrustServerCertificate=True;");
        }
    }
}