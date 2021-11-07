using Microsoft.EntityFrameworkCore;

namespace MyBlog_API.Entities
{
    public class DataContext : DbContext
    {
       
            public DataContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<AppUser> Users { get; set; }
        
    }
}
