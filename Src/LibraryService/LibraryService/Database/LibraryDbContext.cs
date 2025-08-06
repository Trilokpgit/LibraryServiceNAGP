
using LibraryService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Database
{
    public class LibraryDbContext :DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("books"); 
        }
    }
}
