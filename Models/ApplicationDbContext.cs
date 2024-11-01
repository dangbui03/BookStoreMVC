using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}