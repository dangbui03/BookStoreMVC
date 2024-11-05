using BookStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<FileBook> Files { get; set; }
    }
}