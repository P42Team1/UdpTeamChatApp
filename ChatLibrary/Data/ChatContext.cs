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
            EnsureContactSchema();
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

        private void EnsureContactSchema()
        {
            Database.ExecuteSqlRaw("""
                IF OBJECT_ID(N'[ContactEntries]', N'U') IS NULL
                BEGIN
                    CREATE TABLE [ContactEntries] (
                        [Id] int NOT NULL IDENTITY,
                        [OwnerId] int NOT NULL,
                        [ContactUserId] int NOT NULL,
                        [IsBlacklisted] bit NOT NULL,
                        CONSTRAINT [PK_ContactEntries] PRIMARY KEY ([Id]),
                        CONSTRAINT [FK_ContactEntries_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]),
                        CONSTRAINT [FK_ContactEntries_Users_ContactUserId] FOREIGN KEY ([ContactUserId]) REFERENCES [Users] ([Id])
                    );
                    CREATE UNIQUE INDEX [IX_ContactEntries_OwnerId_ContactUserId] ON [ContactEntries] ([OwnerId], [ContactUserId]);
                    CREATE INDEX [IX_ContactEntries_ContactUserId] ON [ContactEntries] ([ContactUserId]);
                END
                """);
        }
    }
}
