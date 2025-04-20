using ChatSystem.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChatSystem.EF.Context
{
    public class ApplicationDbContxt: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContxt(DbContextOptions<ApplicationDbContxt> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Conversation>().HasDiscriminator<String>("ConversationType")
                .HasValue<GroupConversation>("Group")
                .HasValue<PrivateConversation>("Private");

            builder.Entity<UserConversation>().HasKey(uc => new { uc.UserId, uc.ConversationId });

            builder.Entity<UserConversation>().HasOne(uc => uc.User)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserConversation>().HasOne(uc => uc.Conversation)
                .WithMany()
                .HasForeignKey(uc => uc.ConversationId);



            builder.Entity<GroupUser>().HasKey(gu => new { gu.UserId, gu.GroupId });

            builder.Entity<GroupUser>().HasOne(gu => gu.ApplicationUser)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId);

            builder.Entity<GroupUser>().HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupId);

            builder.Entity<GroupConversation>().HasOne(gc => gc.Group)
                .WithOne(g => g.GroupConversation)
                .HasForeignKey<GroupConversation>(gc => gc.GroupId);
                ;

            builder.Entity<FriendShip>().HasKey(f => new { f.FriendID, f.UserId });

            builder.Entity<FriendShip>().HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<FriendShip>().HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendID)
                .OnDelete(DeleteBehavior.Cascade); ;


            base.OnModelCreating(builder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<FriendShip> Friendships { get; set; }
        public DbSet<Connection> Connections { get; set; }

    }
}
