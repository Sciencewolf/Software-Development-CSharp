using LibraryBackend.Classes;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Contexts
{
    public class LibraryBackendContext : DbContext
    {
        public LibraryBackendContext(DbContextOptions<LibraryBackendContext> options) : base(options) { }

        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Reading> Reading { get; set; }
    }
}
