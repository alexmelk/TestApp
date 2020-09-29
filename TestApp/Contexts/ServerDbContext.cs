using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using TestApp.Models.ServerDbContextModels;

namespace TestApp.Context
{
    public class ServerDbContext : IdentityDbContext<User>
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<User> User { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
