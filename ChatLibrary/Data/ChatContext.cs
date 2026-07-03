using ChatLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatLibrary.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserLoginData> UserLoginDataPoints { get; set; }
        public DbSet<ContactEntry> ContactEntries { get; set; }
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(u => u.Chats)
                    .WithMany(c => c.Members);
            modelBuilder.Entity<User>()
                    .HasOne(u => u.LoginData)
                    .WithOne(ld => ld.User)
                    .HasForeignKey<UserLoginData>(ld => ld.UserId);

            modelBuilder.Entity<Chat>()
                    .HasMany(c => c.Messages)
                    .WithOne(m => m.Chat)
                    .HasForeignKey(c => c.ChatId)
                    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ContactEntry>()
            .HasIndex(c => new { c.OwnerId, c.ContactUserId })
            .IsUnique();
            modelBuilder.Entity<ContactEntry>()
                    .HasOne(c => c.Owner)
                    .WithMany()
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ContactEntry>()
                    .HasOne(c => c.ContactUser)
                    .WithMany()
                    .HasForeignKey(c => c.ContactUserId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}