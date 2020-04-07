using ChatAppV1.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatAppV1.DataAccess.Context
{
    public class ChatAppContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ChatAppContext() {}

        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) {}

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ChatAppDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
