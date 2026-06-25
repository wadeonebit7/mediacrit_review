using Mediacrit_Review.Models;
using Microsoft.EntityFrameworkCore;

namespace Mediacrit_Review.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
