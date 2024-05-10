using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend
{
    public class LibraryBackendContext(DbContextOptions<LibraryBackendContext> options) : DbContext(options)
    {
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Reading> Reading { get; set; }
    }
}
