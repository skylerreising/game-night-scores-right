using Microsoft.EntityFrameworkCore;
using GameNightScoresRight.EFDTOs;

namespace GameNightScoresRight.Data
{
    public class GameNightDbContext : DbContext
    {
        public GameNightDbContext(DbContextOptions<GameNightDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Account)
            .WithOne(a => a.User)
            .HasForeignKey<User>(u => u.AccountId)
            .IsRequired();
        }
    }
}
