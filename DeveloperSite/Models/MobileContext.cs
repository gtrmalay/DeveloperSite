using Microsoft.EntityFrameworkCore;


namespace DeveloperSite.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Comment_id);

            modelBuilder.Entity<Developer>()
               .HasKey(d => d.Developer_id);

            modelBuilder.Entity<Game>()
               .HasKey(d => d.Game_id);

            modelBuilder.Entity<User>()
               .HasKey(d => d.User_id);
        }

        
    }
}
